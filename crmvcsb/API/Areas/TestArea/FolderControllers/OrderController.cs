
using Microsoft.AspNetCore.Mvc;

using order.Domain.Models;
using order.Domain.Interfaces;

using Newtonsoft.Json;

namespace orders.Default.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {

        private readonly IDeliverer _deliverer;

        public OrderController( IDeliverer deliverer)
        {
            _deliverer = deliverer;
        }


        [HttpPost("AddOrder")]
        public JsonResult AddOrder([FromBody] OrderCreateAPI query)
        {
            JsonResult result=new JsonResult(string.Empty);

            if(query.ServiceType == "Bird"){
                return Json(_deliverer.AddOrderBirdService(query));
            }
            if (query.ServiceType == "Tortise")
            {
                return Json(_deliverer.AddOrderTortiseService(query));
            }

            return Json(result);
        }
       
    }
}
