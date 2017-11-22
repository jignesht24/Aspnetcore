namespace AnchorTagHelper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using AnchorTagHelper.Models;
    public class HomeController : Controller
    {
        public List<UserViewModel> user =
          new List<UserViewModel>
          {
                new UserViewModel { Id = 10, Name="Jignesh"},
                new UserViewModel {Id = 11, Name="Rakesh"},
                new UserViewModel {Id = 12, Name="Tejas"}
          };

        public IActionResult Index()
        {
            return View();
        }
        [Route("Home1/{id:int}")]
        public IActionResult Detail(int id)
        {
            var u = user.Where(p => p.Id == id).FirstOrDefault();
            return View(u);
        }

        [Route("/Home1/values", Name = "values")]
        public IActionResult Values()
        {
            return View();
        }

        [Route("/Home1/userCurrent",  Name = "userCurrent")]
        public IActionResult UserCurrent(string id,  string name)
        {
            return View();
        }
    }
}
