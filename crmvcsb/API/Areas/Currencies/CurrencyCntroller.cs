

namespace crmvcsb.Areas.TestArea.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using crmvcsb.Universal.Models;
    using crmvcsb.Universal.DomainSpecific.Currency.API;

    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.Currency.DAL;

    //while named routing in startup.cs 
    //attribute can be omitted
    //[Area("TestArea")]
    [Route("api/currency")]
    [ApiController]
    public class CurrencyCntroller : ControllerBase
    {
        ICurrencyServiceEF _Currencyservice;
        public CurrencyCntroller(ICurrencyServiceEF Currencyservice)
        {
            _Currencyservice = Currencyservice;
        }

        //conventional CRUD API
        [HttpGet("{iso}")]
        public ActionResult<string> Get(string iso)
        {
            return $"Get: {iso}";
        }
        [HttpPost]
        public ActionResult<string> Post(CurrencyAPI currency)
        {
            _Currencyservice.AddCurrency(currency);
            return $"Post: {currency.Name}";
        }
        [HttpPut]
        public ActionResult<string> Put(CurrencyUpdateAPI currency)
        {
            _Currencyservice.UpdateCurrency(currency);
            return $"Put: {currency.Name}";
        }
        [HttpDelete("{isoOrName}")]
        public ActionResult<string> Delete(string isoOrName)
        {

            _Currencyservice.DeleteCurrency(isoOrName);
            return $"Delete: {isoOrName}";
        }


        [HttpGet("{isoOrName}")]
        public ActionResult<string> Query(string isoOrName)
        {
            return $"Get: {isoOrName}";
        }
        [HttpPost("{isoOrName}")]
        public ActionResult<string> Command(string isoOrName)
        {
            return $"Get: {isoOrName}";
        }

        [HttpGet("HealthCheck")]
        public ActionResult<string> HealthCheck()
        {
            return "HealthCheck";
        }
    }
}
