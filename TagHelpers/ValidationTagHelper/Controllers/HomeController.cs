using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ValidationTagHelper.Models;

namespace ValidationTagHelper.Controllers
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
[HttpPost]
public object Save(UserViewModel user)
{
    if (!ModelState.IsValid)
    {
        //Model has Error(s)
        return View("index1", user);
    }
    return null;
}
    }
}
