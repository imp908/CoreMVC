using Microsoft.AspNetCore.Mvc;

namespace crmvcsb.Areas.TestArea.Controllers
{
    //while named routing in startup.cs 
    //attribute can be omitted
    //[Area("TestArea")]
    public class NewHomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }

        //redirect to another view
        public IActionResult OldHomeIndex()
        {
            return View("../Home/Index");
        }
    }
}
