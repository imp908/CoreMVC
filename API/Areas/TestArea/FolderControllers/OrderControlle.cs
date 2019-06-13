using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using mvccoresb.Domain.Interfaces;
using order.Domain.Models.Ordering;
using mvccoresb.Domain.TestModels;
using order.Domain.Interfaces;

using Newtonsoft.Json;

namespace mvccoresb.Default.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        IRepository _repo;
        IOrdersManagerWrite _writeManager;
        

        public OrderController(IOrdersManagerWrite writeManager)
        {
            _writeManager=writeManager;
        }


        [HttpPost("AddOrder")]
        public JsonResult AddOrder([FromBody] OrderCreateAPI query)
        {
            var result = _writeManager.AddOrder(query);
            return Json(result);
        }
       
    }
}
