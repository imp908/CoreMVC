using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crmvcsb.Universal.Models;
using crmvcsb.Universal.DomainSpecific.Currency.API;

namespace crmvcsb.Areas.TestArea.Controllers
{
    //while named routing in startup.cs 
    //attribute can be omitted
    //[Area("TestArea")]
    [Route("api/currency")]
    [ApiController]
    public class CurrencyCntroller : ControllerBase
    {
        [HttpGet("{iso}")]
        public ActionResult<string> Get(string iso)
        {
            return $"Get: {iso}";
        }
        [HttpPost]
        public ActionResult<string> Post(CurrencyAPI currency)
        {
            return $"Post: {currency.Name}";
        }
        [HttpPut]
        public ActionResult<string> Put(CurrencyAPI currency)
        {
            return $"Put: {currency.Name}";
        }
        [HttpDelete("{isoOrName}")]
        public ActionResult<string> Delete(string isoOrName)
        {
            return $"Delete: {isoOrName}";
        }

        [HttpGet("HealthCheck")]
        public ActionResult<string> HealthCheck()
        {
            return "HealthCheck";
        }
    }
}
