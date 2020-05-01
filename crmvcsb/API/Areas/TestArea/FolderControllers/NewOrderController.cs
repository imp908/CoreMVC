

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{

    using System;
    using System.Threading.Tasks;
    using crmvcsb.Domain.NewOrder;

    using Microsoft.AspNetCore.Mvc;
    using Autofac;
    using Domain;

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
            DomainManager.ReInitialize();
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
