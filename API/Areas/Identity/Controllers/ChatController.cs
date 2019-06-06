using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using mvccoresb.Domain.Interfaces;

using mvccoresb.Domain.TestModels;

using Newtonsoft.Json;

namespace chat.API.Controllers
{

    public class ChatController : Controller
    {
      

        public ChatController()
        {
            
        }

        [HttpGet]
        public IActionResult Room()
        {

            return View();
        }
    }
}
