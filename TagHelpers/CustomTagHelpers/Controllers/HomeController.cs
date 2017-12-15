namespace CustomTagHelper.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using CustomTagHelper.Models;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            EmployeeViewModel e = new EmployeeViewModel();
            e.Name = "Jignesh Trivedi";
            e.Designation = "Senior Consultant";
            return View(e);
        }
    }
}
