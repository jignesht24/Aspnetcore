using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace RolebaseAuthorization.Controllers
{
    public class TestController : Controller
    {
        [Authorize(Roles = "Admin")]
        public IActionResult OnlyAdminAccess()
        {
            ViewData["role"] = "Admin";
            return View("MyPage");
        }
        [Authorize(Roles = "User")]
        public IActionResult OnlyUserAccess()
        {
            ViewData["role"] = "User";
            return View("MyPage");
        }
        [Authorize(Roles = "HR")]
        public IActionResult OnlyHRAccess()
        {
            ViewData["role"] = "HR";
            return View("MyPage");
        }

        [Authorize(Policy = "OnlyAdminAccess")]
        public IActionResult PolicyExample()
        {
            ViewData["role"] = "Admin";
            return View("MyPage");
        }
    }
}