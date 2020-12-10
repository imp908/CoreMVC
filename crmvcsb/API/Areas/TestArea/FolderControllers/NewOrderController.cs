

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{

    using System;
    using System.Threading.Tasks;
    using crmvcsb.Universal.DomainSpecific.NewOrder;
    using crmvcsb.Universal.DomainSpecific.Currency.API;

    using Microsoft.AspNetCore.Mvc;
    using Autofac;

    //No api, no newarea Url paths
    //http://localhost:5002/NewOrder
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : Controller
    {
        private INewOrderService _service;
        private NewOrderManager _manager;

        public NewOrderController(INewOrderService service, IComponentContext context)
        {
            this._service = service;   
            _manager = context.Resolve<NewOrderManager>();
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        // GET: /<controller>/
        [HttpGet("ReInitialize")]
        public IActionResult ReInitialize()
        {
            _manager.ReInitialize();
            return Ok();
        }


        [HttpGet("GetDbName")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = this._service.GetDbName();                
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("GetCrossRates")]
        public async Task<IActionResult> GetCrossRates([FromBody] GetCurrencyCommand command)
        {
            try
            {
                var result = await this._manager.GetCurrencyCrossRatesAsync(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
