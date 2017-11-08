using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using testCore.Models;

namespace testCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index1()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult Index3()
        {
            return ViewComponent("EmployeeList", new { noOfEmployee = 3 });
        }
        public IActionResult Index4()
        {
            return ViewComponent("EmployeeList1", new { noOfEmployee = 3 });
        }
    }
}
