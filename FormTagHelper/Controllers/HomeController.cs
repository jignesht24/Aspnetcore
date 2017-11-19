using FormTagHelper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace FormTagHelper.Controllers
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

        public IActionResult SelectExample()
        {
            FruitViewModel fruit = new FruitViewModel();
            fruit.Fruits = new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text = "Apple" },
                new SelectListItem { Value = "2", Text = "Banana" },
                new SelectListItem { Value = "3", Text = "Mango"  },
                new SelectListItem { Value = "4", Text = "Orange"  },
            };
            fruit.Fruit = "2";
            return View(fruit);
        }
        public IActionResult SelectWithEnum()
        {
            return View();
        }

        public IActionResult SelectOptionGroup()
        {
            var gujaratGroup = new SelectListGroup { Name = "Gujarat" };
            var maharastraGroup = new SelectListGroup { Name = "Maharastra" };

            CityViewModel city = new CityViewModel();
            city.CityList = new List<SelectListItem>
    {
        new SelectListItem
        {
            Value = "1",
            Text = "Ahmedabad",
            Group = gujaratGroup
        },
        new SelectListItem
        {
            Value = "2",
            Text = "Gandhinagar",
            Group = gujaratGroup
        },
        new SelectListItem
        {
            Value = "3",
            Text = "Bhavangar",
            Group = gujaratGroup
        },
        new SelectListItem
        {
            Value = "4",
            Text = "Mumbai",
            Group = maharastraGroup
        },
        new SelectListItem
        {
            Value = "5",
            Text = "Pune",
            Group = maharastraGroup
        },
        new SelectListItem
        {
            Value = "6",
            Text = "Nasik",
            Group = maharastraGroup
        }
    };
            return View(city);
        }
    }
}
