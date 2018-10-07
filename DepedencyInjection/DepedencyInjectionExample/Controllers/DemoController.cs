using DepedencyInjectionExample.Service;
using Microsoft.AspNetCore.Mvc;

namespace DepedencyInjectionExample.Controllers
{
    public class DemoController : Controller
    {
        public IActionResult Index([FromServices] IHelloWorldService helloWorldService)
        {
            ViewData["MyText"] = helloWorldService.SaysHello() + "Jignesh!";
            return View();
        }

        public IActionResult Index1()
        {
            var helloWorldService = (IHelloWorldService)this.HttpContext.RequestServices.GetService(typeof(IHelloWorldService));
            ViewData["MyText"] = helloWorldService.SaysHello() + "Jignesh Trivedi!";
            return View("index");
        }

        public IActionResult DIToView()
        {
            return View();
        }
    }
}