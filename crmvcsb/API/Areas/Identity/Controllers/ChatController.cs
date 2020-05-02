using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Newtonsoft.Json;


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
        
      
        [HttpGet]
        [Authorize]
        public IActionResult RoomP()
        {
            return View("../Chat/RoomPrivate");
        }
    }
}
