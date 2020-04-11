
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crmvcsb.Domain.NewOrder;
using crmvcsb.Domain.Currencies;
using crmvcsb.Domain.Currencies.API;

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : Controller
    {

        private INewOrderService _manager;

        public NewOrderController(INewOrderService manager) 
        {
            _manager = manager;
        }

        [HttpGet("GetDefault/{IsoCode}")]
        public async Task<IActionResult> Get([FromRoute]string IsoCode)
        {
            try
            {
                IGetCurrencyCommand cmd = new GetCurrencyCommand(){FromCurrency=IsoCode};
                var result = await _manager.GetCurrencyCrossRates(cmd);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCurrency")]
        public async Task<IActionResult> GetCurrencyParam(string IsoCode)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
        [HttpGet("GetCurrencyParam")]
        public async Task<IActionResult> GetNoParam([FromQuery]string IsoCode)
        {
            try
            {
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        [HttpPost("CrossRates")]
        public async Task<IActionResult> GetCrossRates([FromBody] GetCurrencyCommand command)
        {
            try
            {
                var result = await _manager.GetCurrencyCrossRates(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
