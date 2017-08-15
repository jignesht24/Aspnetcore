using Microsoft.AspNetCore.Mvc;
using ModelBinder.Model;

namespace ModelBinder.Controllers
{
    public class HomeController : Controller
    {
        //[HttpGet]
        //[Route("test")]
        //public IActionResult Index([ModelBinder(BinderType = typeof(CustomModelBinder))]User u)
        //{

        //    return View();
        //}

        [HttpGet]
        [Route("test")]
        public IActionResult Index(User u)
        {
            return Json(u);
        }

        [HttpPost]
        [Route("test1")]
        public IActionResult Index1([ModelBinder(BinderType = typeof(CustomModelBinder1))]User1 u)
        {
            return Json(u);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
