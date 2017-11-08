
# Introduction
 
View component is newly introduced feature in asp.net core MVC. It is very similar to partial view but it very powerful compare to partial view. It does not use model binding but work only with the data we provide when calling into it. View Component has following features
•   It also includes SOC (separation-of-concerns) and testability benefit
•   It can have business logic as well as parameter
•   It invoked from layout page
•   It renders chunk rather than a whole response

The View Component includes two parts: the class that derived from ViewComponent abstract class and the result returns i.e. view in most cases. The View Component class similar to controller class, it might have POCO.

A View Component class can be created by following ways
•   Class deriving from ViewComponent abstract class
•   Decorating a class with ViewComponent attribute or attribute that derived from ViewComponent attribute
•   Creating class with suffix ViewComponent (same as Controller class)

Same as Controller class, View Component class must be non-abstract, public and non-nested. This class fully supports for constructor dependency injection. It does not take part in to the controller life cycle, so that we cannot use filters in the view component.

The view component defines logic in InvokeAsync method which returns IViewComponentResult. Required parameters directly comes from invocation of the view component. It never handles a request directly. The view component initialized model and passes to the view using "View" method. They are not directly reachable as a HTTP end point. They must invoked from the code.

Example of ViewComponent class
namespace testCore
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    
    public class EmployeeList : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int noOfEmployee)
        {
            List<Employee> items = new List<Employee>();
            for (var i = 0; i <= noOfEmployee; i++)
            {
                items.Add(new Employee { Id = i, Name = "Emp " + i.ToString() });
            }
            return View(items);
        }
    }
}

ViewComponent attribute can be used to change name of view component. For example, if we have class named "TestViewComponent" and want to set view component name to "EmployeeList1", this can be done using ViewComponent attribute.

[ViewComponent(Name="EmployeeList1")]
public class TestViewComponent : ViewComponent
{

}


Same as controller class, view component searches for the view runtime in following paths
•  View/<Controller Name>/ Components/ <View Component name > / <View Name>
•  Views/Shared/Components/<View Component Name>/ <View Name>

The default view name is "Default" for the view component. It means that our view name must be "Default.cshtml". We can assigned different name to view of view component and this name must be pass to view method.

How to invoke View Component from View
The InvokeAsync method used to invoke view component. This method accept two parameter: name of the view component and parameter. The parameter anonymous type.

Syntax
@Component.InvokeAsync("Name of view component", <parameters: anonymous type >)

Example
@await Component.InvokeAsync("EmployeeList", new {  noOfEmployee = 4  })

Invoking a view component in View as a Tag Helper
Tag Helper is introduced in asp.net core 1.1 and higher. For tag helper, Pascal cased class and method parameter are translated in to lower kebab case. We have to use "vc" element (<vc></vc>) to invoke view component as a tag helper. It can be specify as following

<vc:[view component name]  parameter1="value"
  parameter2="value">
</vc:[view component name]>

Example:
<div class="row">
    <vc:employee-list no-of-employee="5">
    </vc:employee-list>
</div>

We must register the assembly that contain the view component using @addTagHelper directive if we use view component as a tag helper. For example, if our view component is assembly named "ViewComponent", add the following code to _viewImports.cshtml file

@addTagHelper *, ViewComponent

Invoking a view component from a Controller class
Mostly, view component are invoked from a view, but we can also invoke them from the controller method. View component do not have endpoint as controller class, so we can implement controller action that returns ViewComponentResult.

Example
public IActionResult Index3()
{
    return ViewComponent("EmployeeList", new { noOfEmployee = 3});
}

Specifying a view name in ViewComponent
The complex view component might require to return non default view based on some condition. In the following example, ViewComponent will return EmployeeList view if paramter noOfEmployee value is greater than 5.

public async Task<IViewComponentResult> InvokeAsync(int noOfEmployee)
{
    List<Employee> items = new List<Employee>();
    for (var i = 0; i <= noOfEmployee; i++)
    {
        items.Add(new Employee { Id = i, Name = "Emp " + i.ToString() });
    }
    if (noOfEmployee > 5)
    {
        return View("EmployeeList", items);
    }
    return View(items);
}

EmployeeList.cshtml
@model IEnumerable<testCore.Employee>

<h3>Employee List</h3>
<ul>
    @foreach (var emp in Model)
    {
        <li>@emp.Name</li>
    }
</ul>

Output:
 

If view is not found in desired location, system will throw exception. This is same behaviour as controller class have. In above example, I have change view name to “EmployeeList1” and try to execute, system will throw error.
