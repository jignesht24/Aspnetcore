using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RazorClassLibrary.Model;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    //[Area("MyFeature")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            TestModel testMode = new TestModel();
            testMode.Name = "Jignesh";
            ViewData["testMode"] = testMode;
           return View();
        }
    }
}
