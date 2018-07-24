using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JWTAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get()
        {
            var currentUser = HttpContext.User;
            int spendingTimeWithCompany = 0;

            if (currentUser.HasClaim(c => c.Type == "DateOfJoing"))
            {
                DateTime date = DateTime.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "DateOfJoing").Value);
                spendingTimeWithCompany = DateTime.Today.Year - date.Year;
            }

            if (spendingTimeWithCompany > 5)
            {
                return new string[] { "High Time1", "High Time2", "High Time3", "High Time4", "High Time5" };
            }
            else
            {
                return new string[] { "value1", "value2", "value3", "value4", "value5" };
            }
        }

    }
}
