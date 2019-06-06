using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using chat.Domain.Models;
using chat.Domain.APImodels;

namespace chat.API.Controllers
{
    [Area("Identity")]
    public class AccountController : Controller
    {

        private readonly UserManager<UserAuth> _userManager;
        private readonly SignInManager<UserAuth> _signInManager;

        public AccountController(
            UserManager<UserAuth> userManager,
            SignInManager<UserAuth> signInManager){
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
                UserAuth user = new UserAuth {UserName  = model.Email, Email = model.Email};
                
                var userExists = await _userManager.FindByNameAsync(model.Email);

                if(userExists != null){
                    return View("../Account/Register");
                }
                        
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {                   
                    var url = Url.Action("RoomP","Chat",new {area="Identity"});
                    return Redirect(url);
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
    }
}
