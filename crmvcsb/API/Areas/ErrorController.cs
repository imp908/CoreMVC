

namespace crmvcsb.API.Areas
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    //controller scope or method scope
    //[Route("error")]
    [ApiController]
    //??
    //[ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : ControllerBase
    {
        //controller scope or method scope
        [Route("errordev")]
        public ActionResult ErrorDev()
        {
            return BadRequest();
        }
        
        [Route("errorprod")]
        public ActionResult ErrorProd()
        {
            return BadRequest();
        }
    }
}
