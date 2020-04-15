
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crmvcsb.Domain.NewOrder;
using crmvcsb.Domain.Currency;
using crmvcsb.Domain.Currency.API;

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {

        private ICurrencyService _service;

        public CurrencyController(ICurrencyService manager) 
        {
            _service = manager;
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

        [HttpGet("GetDefault/{IsoCode}")]
        public async Task<IActionResult> Get([FromRoute]string IsoCode)
        {
            try
            {
                IGetCurrencyCommand cmd = new GetCurrencyCommand(){FromCurrency=IsoCode};
                var result = await _service.GetCurrencyCrossRates(cmd);
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
                var result = await _service.GetCurrencyCrossRates(command);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}
