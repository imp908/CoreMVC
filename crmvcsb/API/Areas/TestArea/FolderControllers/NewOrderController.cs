

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{

    using System;
    using System.Threading.Tasks;
    using crmvcsb.Universal.DomainSpecific.NewOrder;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Infrastructure.EF;
    using crmvcsb.Universal;
    using Microsoft.AspNetCore.Mvc;
    using Autofac;

    //No api, no newarea Url paths
    //http://localhost:5002/NewOrder
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : ControllerBase
    {
        public ICurrencyServiceEF _currencyService;
        public INewOrderServiceEF _newOrderService;

        public INewOrderManager _newOrderManager;

        public NewOrderController(ICurrencyServiceEF currencyService, INewOrderServiceEF newOrderService, INewOrderManager newOrderManager)
        {
            this._currencyService = currencyService;
            this._newOrderService = newOrderService;

            this._newOrderManager = newOrderManager;
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
            _newOrderManager.ReInitialize();
            return Ok();
        }


        [HttpGet("GetDbName")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var result = this._newOrderManager.GetDbName();                
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
                var result = await this._currencyService.GetCurrencyCrossRatesAsync(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("AddCurrency")]
        public async Task<IActionResult> AddCurrency([FromBody] CurrencyAPI currency)
        {
            try {
                var result = await this._currencyService.AddCurrency(currency);
                return Ok(result);
            }
            catch(Exception e)
            {
                return BadRequest();
            }
        }
    }
}
