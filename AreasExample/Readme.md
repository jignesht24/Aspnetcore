# Introduction

Area is most beautiful feature of ASP.net MVC to organize related functionality into a group. We can create a hierarchy for the physical grouping of the functionality or business component. In other word, The Area is a smaller functional unit which has its own set of Models, Views, and controllers. ASp.net MVC application may have one or more areas. So, Using Areas, we can divide our large application into smaller functional group. The logical components such as Model, View and Controller are kept in different folder and MVC framework use naming conventions to create relation between these components.

Everything in MVC application is organized with Controllers and Views. Normally, the controller name determines the first part of our URL and action name becomes second part. By default views those are rendered have same name as action method name.

In older ASp.net MVC application, it is very easy to create Areas. Default it has option to create area by right clicking on project and go to "Add >> Area". The folder and content with in area folder is automatically create by the template.

# How to create "Area" in ASP.net Core   

As we aware, there is no option to create area by right clicking on project folder. So if we want to create area in Asp.net Core MVC application, Create new folder and named it to area. With in this folder we can create another new folder and give it to any logical name. Here we need to Model, View and Controller folder manually.

To demonstrate the example, I have create folder "Test" under "Areas" folder and within this folder I have create three folders: Models, Views and Controllers.
At the time of render the view, by Default MVC tries to find out the views within an Area. If it is not find, it tried in other location. Following are the locations in which MVC tries to find view.

/Areas/<Area Name>/Views/<Controller Name>/<Action Name>.cshtml
/Areas/<Area Name>/Views/Shared/<Action Name>.cshtml
/Views/Shared/<Action Name>.cshtml

These default location can be change using AreaViewLocationFormats of RazorViewEngineOptions.

In this example, I have given name of the folder is "Areas", but it can be anything. It is not necessary area folder name should be "Areas". This can be change using MVC option. 

In this structure, only View folder is in consideration because rest of the content like controllers and models are the classes and it can be compiled within single dll whereas the content of views is compiled individually.

Once we have defined the folder hierarchy, we have to tell MVC that which controller is associated with which area. We can do this thing by decorating controller with "Area" attribute.

namespace AreasExample.Areas.Test.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    [Area("Test")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
     }
}

Next step is to setup a route definition which work with our newly created areas. To demonstrate the example, I have use a conventional route that define in "Configure" method of Startup class.

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    app.UseStaticFiles();

    app.UseMvc(routes =>
    {
        routes.MapRoute(
            name: "areaRoute",
            template: "{area:exists}/{controller=Home}/{action=Index}");

        routes.MapRoute(
            name: "default",
            template: "{controller=Home}/{action=Index}/{id?}");
    });
}

The views under our area do not share the _ViewImports.cshtml and _ViewStart.cshtml. This means that layout page of our site will not apply automatically to the views those are under Areas. To apply common layout page that located at root Views folder to our views under area, we need to copy both files _ViewImports.cshtml and _ViewStart.cshtml to view folder of each area. The _ViewStart.cshtml file contain the location of the layout page, so we need to correct layout page path after coping the file.

_ViewStart.cshtml
@{
    Layout = "~/Views/Shared/_Layout.cshtml";
}


# Generating links from an action
Generating links from an action method (which controller is under area) to another action on different controller and different area, we need to pass area name with route argument.
@Html.ActionLink("Go to Home Page of Test Area“, "Index", "Home", new { area = "Test" })

We can also generating the link for the action (which controller is under area) to another action on different controller which is not under any area by passing blank area value to the route argument.

@Html.ActionLink("Go to Home Page", "Index", "Home", new { area = "" })

# Summary
Areas in asp.net core MVC provide following features
 •  Application may have one or more areas
 •  Every area has its own Model, View Controller
 •  It allow us to organize large MVC application into multiple component which works independently
 •  We can have same name controller within different areas

Currently nested areas (areas within area) are not directly supported by Asp.net core application but it can be done using IViewLocationExpander. It use to modify view locations and how the view engine searches for the path.


