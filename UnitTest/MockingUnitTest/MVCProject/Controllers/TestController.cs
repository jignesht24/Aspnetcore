using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MVCproject.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _logger;

        public TestController(ILogger logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public string GetMessage()
        {
            _logger.LogDebug("Test Method Called!!!");
            return "Hi! Reader";
        }
    }
}