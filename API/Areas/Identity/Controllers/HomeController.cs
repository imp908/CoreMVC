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

namespace chat.API.Controllers
{
    [Area("Identity")]
    public class HomeController : Controller
    {
     
        // GET: api/Account
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}
