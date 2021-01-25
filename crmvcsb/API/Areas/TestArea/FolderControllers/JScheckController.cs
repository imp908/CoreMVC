using Microsoft.AspNetCore.Mvc;

namespace crmvcsb.TestArea.Controllers
{
    /** while mapping in startup.completionlist exists no custom attribute needed */
    [Area("TestArea")]
    public class JScheckController : Controller
    {
        public IActionResult CheckAppOne()
        {
            return View("../JScheck/CheckAppOne");
        }

        public IActionResult CheckAppTwo()
        {
            return View("../JScheck/CheckAppTwo");
        }
    }
}