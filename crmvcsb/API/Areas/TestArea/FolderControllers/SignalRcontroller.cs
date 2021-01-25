
using Microsoft.AspNetCore.Mvc;


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

        public JsonResult FakeCurrentUser()
        {
            return Json(new { userName = "FakeUserName" });
        }
    }
}