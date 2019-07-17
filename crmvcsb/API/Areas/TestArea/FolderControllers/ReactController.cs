
using Microsoft.AspNetCore.Mvc;


using Newtonsoft.Json;

namespace crmvcsb.TestArea.Controllers
{
    /** while mapping in startup.completionlist exists no custom attribute needed */
    [Area("TestArea")]
    public class ReactController : Controller
    {
        public IActionResult CheckShoppingList()
        {
            return View("../ReactCheck/ReactCheck");
        }

    }
}