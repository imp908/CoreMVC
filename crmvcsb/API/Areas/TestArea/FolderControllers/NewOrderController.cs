

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{

    using System;
    using System.Threading.Tasks;
    using crmvcsb.Domain.DomainSpecific.NewOrder;
    using crmvcsb.Domain.DomainSpecific;

    using Microsoft.AspNetCore.Mvc;
    using Autofac;

    //No api, no newarea Url paths
    //http://localhost:5002/NewOrder
    public class NewOrderController : Controller
    {
        private INewOrderService _service;
        private DomainManager _manager;

        public NewOrderController(INewOrderService service, IComponentContext context)
        {
            this._service = service;   
            _manager = context.Resolve<DomainManager>();
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            _manager.ReInitialize();
            return Ok();
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
    }
}
