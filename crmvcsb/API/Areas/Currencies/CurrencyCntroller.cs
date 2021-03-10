

namespace crmvcsb.Areas.TestArea.Controllers
{
    using crmvcsb.Universal.DomainSpecific.Currency;
    using crmvcsb.Universal.DomainSpecific.Currency.API;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
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
        public async Task<IActionResult> Post(CurrencyAPI currency)
        {
            var item = await _Currencyservice.AddCurrency(currency);
            if(item == null)
            {
                return StatusCode(500,"Object not created");
            }
            return Created("Object created", item);
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

        [HttpGet("HttpResponse")]
        public ActionResult HttpResponse()
        {
            return Created("",1);
        }
    }
}
