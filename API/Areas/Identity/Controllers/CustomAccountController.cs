using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using chat.Domain.Models;
using chat.Domain.APImodels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

using System.Security.Claims;

using Newtonsoft.Json;

namespace chat.API.Controllers
{
    [Area("Identity")]
    public class CustomAccountController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public CustomAccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager){
                _userManager = userManager;
                _signInManager = signInManager;
        }

        // GET: api/Account
        [HttpGet]
        public IActionResult Register()
        {
            return View("../Account/Register");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserAPI model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser {UserName  = model.Email, Email = model.Email};
                
                var userExists = await _userManager.FindByNameAsync(model.Email);

                if(userExists != null){
                    return View("../Account/Register");
                }
                        
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var url = Url.Action("LogIn", "Identity/Account")
                       .Replace("%2F", "/");
                    return Redirect(url);

                    //return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LoginUserAPI model)
        {
            if(ModelState.IsValid)
            {
                IdentityUser user = new IdentityUser {UserName  = model.Email, Email = model.Email};
                var userExists = await _userManager.FindByNameAsync(model.Email);

                if (userExists == null)
                {
                    var url = Url.Action("Register", "Identity/Account")
                        .Replace("%2F","/");
                    return Redirect(url);
                    //return RedirectToAction("Register", "Identity/Account");
                }

                if(userExists != null){
                 
                    await Authenticate(model.Email);
                    var url = Url.Action("roomP", "Identity/Chat")
                        .Replace("%2F", "/");
                    return Redirect(url);
                    //return RedirectToAction("RoomP", "Chat", new { Area = "Identity" });
                }
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext
            //.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            .SignOutAsync(IdentityConstants.ApplicationScheme);
            return RedirectToAction("Login", "Account");
        }

        
        [HttpGet]
        [Authorize]
        public async Task<JsonResult> GetCurrentUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(new { UserName=user.UserName});
        }
        

        // [HttpGet]
        // public async Task<IActionResult> LogOut()
        // {
        //     return View("../Account/LogIn");
        // }
    }
}
