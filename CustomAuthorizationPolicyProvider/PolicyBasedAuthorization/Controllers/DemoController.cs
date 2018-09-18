using Microsoft.AspNetCore.Mvc;

using PolicyBasedAuthorization.Policy;

namespace PolicyBasedAuthorization.Controllers
{
    public class DemoController : Controller
    {
        [MinimumTimeSpendAuthorize(180)]
        public IActionResult TestMethod1()
        {
            return View("MyPage");
        }
        [MinimumTimeSpendAuthorize(365)]
        public IActionResult TestMethod2()
        {
            return View("MyPage");
        }
        [MinimumTimeSpendAuthorize(10)]
        public IActionResult TestMethod3()
        {
            return View("MyPage");
        }
    }
}