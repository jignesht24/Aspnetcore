using Microsoft.AspNetCore.Mvc;
using MVCproject.Core;

namespace MVCProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IGetDataRepository _data;

        public HomeController(IGetDataRepository data)
        {
            _data = data;
        }

        public IActionResult Index()
        {
            return View();
        }

        public string GetNameById(int id)
        {
            return _data.GetNameById(id);
        }
    }
}
