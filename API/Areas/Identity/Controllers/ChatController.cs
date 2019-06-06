using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using mvccoresb.Domain.Interfaces;

using mvccoresb.Domain.TestModels;

using Newtonsoft.Json;

using Microsoft.AspNetCore.Authorization;

namespace chat.API.Controllers
{
    [Area("Identity")]
    public class ChatController : Controller
    {      
        public ChatController()
        {
            
        }

        [HttpGet]
        public IActionResult Room()
        {
            return View("../Chat/RoomPublic");
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult RoomP()
        {
            return View("../Chat/RoomPrivate");
        }
    }
}
