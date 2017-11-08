
# Introduction
 
ASP.net Code has built-in support for dependency injection. The dependency injection in ASP.net core is not limited to middleware, controller and models. It also support dependency injection into view. This is very useful in view specific services like localization. Using view dependency, we can bypass the controller for fatching data. As good practice, all the data must pass from the controller.  

There are many way to provide the data to view e.g. ViewBag, ViewData, and View Model. From ASp.net core, one way to supplied data ie. Custom service that used via DI (dependency injection). However it is not a best practice. 

# @inject directive
The @inject directive is used to inject dependency in to the view. This directive has following syntax.

@inject <type> <instance name>

•	@inject is the directive that used to inject dependencies
•	<type> is service class type
•	<instance name> is the instance name of service by which we can access service methods

There is three easy steps to achieve this.
•	Create service
•	Register the service in Startup class
•	Inject service in to view 

# Create Service
Here service is nothing but C# Class that contains public methods.

To demonstrate the example, I have created service class and created method (GetCities) that return list of city name. This method returns List<string> ().

namespace DIinView.Models
{
    using System.Collections.Generic;
    public class CityModelService
    {
        public List<string> GetCities()
        {
            return new List<string>()
            {
                "Ahmedabad", "Gandhinagar", "Bhavnagar", "Surat", "Bhuj"
            };
        }
    }
}

# Register the service in Startup Class
To use created service in to the view, we need to register this service into ConfigureServices method of Startup class.  There are three ways to register service within Startup class. Each method has their own scope and base on the requirement we can register.

We can register service using AddTransient method. This method is used to map abstract types to concrete services which are in instantiated separately for every object that requires this service. The life of the instance of service is limited to that view only, if we register the service using AddTransient method. In this method, Transient lifetime services are created each time when they requested. It can be best to use for lightweight, stateless services.

public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddTransient<CityModelService>();
}

We can also register service as Singleton that create single instance throughout the application. It will create the instance of service when it called first time and reuse this same object in all the calls.

public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc();
    services.AddSingleton<CityModelService>();
}

Another approach to register service is scoped. In this approach service is created once per request within scope. It is equivalent to Singleton in current scope for request. It create one instance on every http request.

public void ConfigureServices(IServiceCollection services)
{
    services.AddMvc
    services.AddScoped<CityModelService>();
            
}

If we not register the service in Startup class and try to use in view, it will throw an Invalid Operation Exception: "No Service for type xxx has been register. This error occurred as we did not register the service in a dependency injection container.

# Inject the service in view and use its method

The final step is to inject service in to view and use service method. We can inject the dependency using @inject directive.  

@{
    ViewData["Title"] = "City List";
}

@inject DIinView.Models.CityModelService cityService

<h3>My nearest City List</h3>
<ul>
    @foreach (var name in cityService.GetCities())
    {
        <li>@name</li>
    }
</ul>



# Summary

ASP.net core MVC allow us to inject dependency in to the view. This can be useful in for populating UI element such as dropdown list. However the best practice say that "all the data render by View is come from controller class". View injection is help us to minimizing the amount of code required on Controllers.

