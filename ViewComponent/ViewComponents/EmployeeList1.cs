namespace testCore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    //[ViewComponent(Name="EmployeeList1")]
    public class EmployeeList1 : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int noOfEmployee)
        {
            List<Employee> items = new List<Employee>();
            for (var i = 0; i <= noOfEmployee; i++)
            {
                items.Add(new Employee { Id = i, Name = "Emp " + i.ToString() });
            }
            if (noOfEmployee > 5)
            {
                return View("EmployeeList1", items);
            }
            return View(items);
        }
    }
}