using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace crmvcsb.Default.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }



        /// <summary>
        /// Example of multiple Get with different params per controller
        /// </summary>
        /// <returns>OK</returns>
        
        //http://localhost:5002/api/Values/GetDbName
        [HttpGet("GetDbName")]
        public async Task<IActionResult> GetDbName()
        {
            return Ok();
        }
        //http://localhost:5002/api/Values/GetDefault/USD
        [HttpGet("GetDefault/{IsoCode}")]
        public async Task<IActionResult> Get([FromRoute]string IsoCode)
        {
            return Ok();
        }
        //http://localhost:5002/api/Values/GetCurrency?USD
        [HttpGet("GetCurrency")]
        public async Task<IActionResult> GetCurrencyParam(string IsoCode)
        {
            return Ok();
        }
        //http://localhost:5002/api/Values/GetCurrencyParam?USD
        [HttpGet("GetCurrencyParam")]
        public async Task<IActionResult> GetNoParam([FromQuery]string IsoCode)
        {
            return Ok();
        }
    }
}
