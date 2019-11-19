
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Newtonsoft.Json;

using crmvcsb.Infrastructure.EF;
using crmvcsb.Infrastructure.EF.newOrder;
using crmvcsb.Domain.Interfaces;
using AutoMapper;
using crmvcsb.Domain.NewOrder.API;

using crmvcsb.Domain.NewOrder;


namespace crmvcsb.API.Areas.TestArea.FolderControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewOrderController : Controller
    {

        private INewOrderManager _manager;

        public NewOrderController(INewOrderManager manager) 
        {
            _manager = manager;
        }

        [HttpGet("GetDefault/{IsoCode}")]
        public async Task<IActionResult> Get([FromRoute]string IsoCode)
        {
            try
            {
                var cmd = new GetCurrencyCommand(){FromCurrency=IsoCode};
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
