using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SerilogDemo.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            //Log.Logger.ForContext("OtherData", "Test Data");
            //((Serilog.ILogger)logger).ForContext("OtherData", "Test Data");
            //_logger = logger;

        }
        public IActionResult Index()
        {
            Log.Logger.ForContext("OtherData", "Test Data").Information("Index method called!!!");
            return View();
        }
    
    }
}
