using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClaimBasedPolicyBasedAuthorization.Controllers
{
    public class DemoController : Controller
    {
        [Authorize(Policy = "IsAdminClaimAccess")]
        public IActionResult TestMethod1()
        {
            return View("MyPage");
        }
        [Authorize(Policy = "IsAdminClaimAccess")]
        [Authorize(Policy = "NonAdminAccess")]
        public IActionResult TestMethod2()
        {
            return View("MyPage");
        }

        [Authorize(Policy = "RoleBasedClaim")]
        public IActionResult TestMethod3()
        {
            return View("MyPage");
        }
        [Authorize(Policy = "Morethan365DaysClaim")]
        public IActionResult TestMethod4()
        {
            return View("MyPage");
        }

        [Authorize(Policy = "AccessPageTestMethod5")]
        public IActionResult TestMethod5()
        {
            return View("MyPage");
        }
        [Authorize(Policy = "AccessPageTestMethod6")]
        public IActionResult TestMethod6()
        {
            return View("MyPage");
        }
    }
}