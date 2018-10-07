using Microsoft.AspNetCore.Mvc;
using DepedencyInjectionExample.Service;

namespace DepedencyInjectionExample.Controllers
{
    public class HomeController : Controller
    {
        IHelloWorldService _helloWorldService;
        public HomeController(IHelloWorldService helloWorldService)
        {
            _helloWorldService = helloWorldService;
        }

        //public HomeController(IHelloWorldService helloWorldService, string test)
        //{

        //}

        public IActionResult Index()
        {
            ViewData["MyText"] = _helloWorldService.SaysHello() + "Jignesh!";
            return View();
        }
    }
}
