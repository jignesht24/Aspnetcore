using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Unittest.Models;

namespace Unittest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public string GetEmployeeName(int empId)
        {
            string name;
            if (empId == 1)
            {
                name = "Jignesh";
            }
            else if (empId == 2)
            {
                name = "Rakesh";
            }
            else
            {
                name = "Not Found";
            }
            return name;
        }

    }
}
