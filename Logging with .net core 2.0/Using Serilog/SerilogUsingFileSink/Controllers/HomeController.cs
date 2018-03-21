using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using SerilogDemo.Models;

namespace SerilogDemo.Controllers
{
    public class HomeController : Controller
    {
        
        public HomeController()
        {
            
        }
        public IActionResult Index()
        {
            Log.Information("Index method called!!!");
            return View();
        }
    
    }
}
