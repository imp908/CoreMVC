

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace crmvcsb.API.Areas.TestArea.FolderControllers
{

    using System;
    using System.Threading.Tasks;
    using crmvcsb.Domain.NewOrder;

    using Microsoft.AspNetCore.Mvc;

    public class NewOrderController : Controller
    {
        private INewOrderService _service;

        public NewOrderController(INewOrderService service)
        {
            this._service = service;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
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
