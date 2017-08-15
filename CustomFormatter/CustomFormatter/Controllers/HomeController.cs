using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CustomFormatter.Controllers
{
    public class HomeController : Controller
    { 
        [Route("Home/index")]
        public IActionResult Index()
        {
            Employee emp = new Employee();
            emp.Id = 1;
            emp.EmployeeCode = "ABC";
            emp.FirstName = "Jignesh";
            emp.LastName = "Trivedi";
            
            return Ok(emp);
        }

        [HttpPost]
        [Route("Home/save")]
        public IActionResult Save([FromBody]Employee emp)
        {
            return Ok(true);
        }

        [Route("Home/about")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }
    }
}
