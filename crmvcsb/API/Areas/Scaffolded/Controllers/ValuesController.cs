
/*
    net core responses to http codes
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase?view=aspnetcore-5.0
    https://docs.microsoft.com/en-us/dotnet/api/system.net.httpstatuscode?view=net-5.0
    https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.http.statuscodes?view=aspnetcore-5.0

    return Accepted(); //202
    return BadRequest(); //400
    return Conflict(); //409
    return Created("Object created", item); //201
    return File("","",""); //200 OK
    return Forbid(); //403
    return NoContent();  //204
    return Ok() //200
    return NotFound(); //404
                
    return StatusCode(500,"Object not created");
*/

namespace crmvcsb.Default.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

        /// <summary>
        /// Async controller methods to check async
        /// </summary>
        /// <returns></returns>
        // GET api/values
        // /api/values/GetAsync
        [HttpGet("GetAsync")]
        public async Task<ActionResult<string>> GetAsync()
        {
            await SandBox.GOasync();
            return "Async executed";
        }
        // GET api/values
        // api/values/GetasyncFromSinc
        [HttpGet("GetasyncFromSinc")]
        public ActionResult<string> GetasyncFromSinc()
        {
            SandBox.GOsync();
            return "Sync from async executed";
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
        public async Task<IActionResult> Get([FromRoute] string IsoCode)
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
        public async Task<IActionResult> GetNoParam([FromQuery] string IsoCode)
        {
            return Ok();
        }
    }
}
