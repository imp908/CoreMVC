
using Microsoft.AspNetCore.Mvc;


using Newtonsoft.Json;


namespace crmvcsb.TestArea.Controllers
{
    /** while mapping in startup.completionlist exists no custom attribute needed */
    [Area("TestArea")]
    public class SignalRcontroller : Controller
    {
        public IActionResult Hub()
        {
            return View("../SignalR/hub");
        }

    }
}