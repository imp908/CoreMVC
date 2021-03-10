using Microsoft.AspNetCore.Mvc;

namespace crmvcsb.TestArea.Controllers
{
    /** while mapping in startup.completionlist exists no custom attribute needed */
    [Area("TestArea")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewHomeIndex()
        {
            return View("../NewHome/Index");
        }
    }
}
