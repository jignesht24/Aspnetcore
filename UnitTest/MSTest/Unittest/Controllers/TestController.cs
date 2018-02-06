using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Unittest.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string GetMessage()
        {
           _logger.LogError("Index Method Called!!!");
            _logger.LogInformation("Index Method Called!!!");
            return "Hi! Reader";
        }
    }
}