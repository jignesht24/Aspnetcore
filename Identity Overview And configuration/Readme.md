# Introduction
It is membership system that allows us to add login functionality to our application. A user may create account and log in using credentials or can use external login providers, such as Google, Microsoft Account, Twitter, Facebook etc.
 
We can configure ASP.NET Core Identity to use SQL Server database to store the user profile data or we can use our own persistent store, such as Azure Table Storage.
 
We can either create an application using Visual Studio or .NET Core CLI.
 
In VS, select File => New => Project, select ASP.NET Core Web Application from the popup.
 
After that, select Web Application (Model-View-Controller) for framework 2.x with Individual User Account as an authentication mode.
 
Using .NET Core CLI, we can create a new project using "donet new mvc --auth Individual" command. This will create a new project with the same template as mentioned above, using Visual Studio.
 
#### Command
```
> donet new mvc --auth Individual  
```

### Configure Identity Services
 
The ConfigureServices method of startup class contains configuration for the Identity Services. The "services.AddIdentity" method adds the default identity system configuration for specific user and role. These services are available to the application using DI (dependency injection).

```
public void ConfigureServices(IServiceCollection services)  
{  
    services.AddDbContext<ApplicationDbContext>(options =>  
        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  
  
    services.AddIdentity<ApplicationUser, IdentityRole>()  
        .AddEntityFrameworkStores<ApplicationDbContext>()  
        .AddDefaultTokenProviders();  
  
  
…  
…  
… 
}
```
Identity Services are enabled for the application by calling "UseAuthentication" method on the Configure method of startup class. This method adds authentication middleware to the request pipeline.
```
public void Configure(IApplicationBuilder app, IHostingEnvironment env)  
{  
…  
…  
app.UseAuthentication();  
  
app.UseMvc(routes =>  
{  
    routes.MapRoute(  
        name: "default",  
        template: "{controller=Home}/{action=Index}/{id?}");  
});  
} 
```
The template creates classes to represent role, user, and database context that, from the framework classes, inherits IdentityRole, IdentityUser, and IdentityDbContext.
```
public class ApplicationUser : IdentityUser
{  
}  
  
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>  
{  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)  
            : base(options)  
        {  
        }  
  
        protected override void OnModelCreating(ModelBuilder builder)  
        {  
            base.OnModelCreating(builder);  
        }  
}  
public class AppIdentityRole : IdentityRole  
{  
} 
```

This template also added a default connection string to the appsetting.json file. In this connection string, the default database name is "aspnet-{project name}-{guid}". For demonstration, I have changed it to "IdentityTest"
#### appSettings.json
```
{  
  "ConnectionStrings": {  
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=IdentityTest;Trusted_Connection=True;MultipleActiveResultSets=true"  
  },  
  "Logging": {  
    "IncludeScopes": false,  
    "LogLevel": {  
      "Default": "Warning"  
    }  
  }  
}
```

Now, I am launching the application and clicking the Register link. It will display the following page. Insert the required information and click on Register button. If this is the first time, your system will ask for database migrations
By clicking on "Apply Migrations", you can migrate the database. Alternatively, we can also use command line to perform the migration. Just, use the following command.

```
> dotnet ef database update
```
We can also test ASP.NET Core Identity with our application without a persistent database by using an in-memory database. To use an in-memory database, we need to add Microsoft.EntityFrameworkCore.InMemory package to our application and modify "AddDbContext" call with the following code in ConfigureServices method of Startup class.
```
services.AddDbContext<ApplicationDbContext>(options =>  
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
```
The "Register" action of AccountController is invoked when user click on "Register" link. This action method create user by calling "CreateAsync" method of UserManager class. The UserManager class provided to the controller class by using dependency injection.
```
public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)  
{  
    ViewData["ReturnUrl"] = returnUrl;  
    if (ModelState.IsValid)  
    {  
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };  
        var result = await _userManager.CreateAsync(user, model.Password);  
        if (result.Succeeded)  
        {  
            _logger.LogInformation("User created a new account with password.");  
  
            await _signInManager.SignInAsync(user, isPersistent: false);  
            _logger.LogInformation("User created a new account with password.");  
            return RedirectToLocal(returnUrl);  
        }  
        AddErrors(result);  
    }  
  
    // If we got this far, something failed, redisplay form  
    return View(model);  
}  
```
### SignIn and SignOut
 
The ASP.net core Identity provides a class called "SignInManager" that is used to authenticate the user and sign-in (login) or sign-out user. This "SignInManager" class provided to the controller by using dependency injection.
 
The "SignInManager" class has various methods for Sign In, such as PasswordSignInAsync, SignInAsync etc. It also contains the method for external login, such as ExternalLoginSignInAsync. This class contains the method "SignOutAsync" for signing out the user from system. Apart from that, this class has methods for checking user lock status, reset lock, lock user, refresh token etc.
```
//For Sign in  
await _signInManager.SignInAsync(user, isPersistent: false);  
//For sign out  
await _signInManager.SignOutAsync(); 
```
### Override default Configuration
 
We can also override the default behavior of the Identity class. There is no configuration required if we want to use default configuration. The configuration for Identity can be change by defining IdentityOption in ConfigureServices method of startup class.
There are some default behaviors that can be overridden easily in our application in ConfigureService method of startup class.
 
Followings are the options that can be overridden.
## PasswordOptions (Password Policy)
 
By default, Identity has some restrictions in a password, such as a password contains the uppercase and lowercase character, special character, digit etc. If we want to simplify the password restriction, we can override the behavior in configureServices method of startup class by setting up PasswordOptions class properties.
 
Following are the properties of PasswordOptions class
##### RequireDigit
It is a Boolean type property. If it is set to true, user needs to enter a number between 0-9 in the password. By default, it is set to true.

##### RequiredLength
It is integer type property. It denotes the minimum length of the password. By default, the value is 6.

##### RequireNonAlphanumeric
It is Boolen type property. If it is set to true, user needs to enter a non-alphanumeric character in the password. By default, it is set to true.

##### RequireUppercase
It is Boolen type property. If it is set to true, user needs to enter an upper case character in the password. By default, it is set to true.

##### RequireLowercase
It is Boolen type property. If it is set to true, user needs to enter a lower case character in the password. By default, it is set to true.

##### RequiredUniqueChars
It is integer type of property. It denotes the number of distinct characters in the password. By default, it is set to 1.

ASP.NET Core 1.x contains all the properties except "RequiredUniqueChars" property.
Example
```
services.Configure<IdentityOptions>(options =>  
{  
    // Password settings  
    options.Password.RequireDigit = true;  
    options.Password.RequiredLength = 8;  
    options.Password.RequireNonAlphanumeric = false;  
    options.Password.RequireUppercase = true;  
    options.Password.RequireLowercase = false;  
    options.Password.RequiredUniqueChars = 1;  
})
```
## LockoutOptions(User's lockout)
 
It contains the options for configuring user lockout. It has the following properties.
##### DefaultLockoutTimeSpan
It is the amount of time for which user is locked out when a lockout occurs. By default, the value is 5 minutes.

##### MaxFailedAccessAttempts
It is number of failed access attempts until a user is locked out if lockout is enabled. By default, the value is 5.

AllowedForNewUsers
It is Boolean type property and determines if a new user can be locked out. By default, the value is true.
Example
```
services.Configure<IdentityOptions>(options =>  
{  
    // Lockout settings  
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);  
    options.Lockout.MaxFailedAccessAttempts = 5;  
    options.Lockout.AllowedForNewUsers = true;  
});  
```
## SignInOptions(Sign in settings)
 
It contains the options for configuring sign in. It has following properties.
##### RequireConfirmedEmail
It is Boolean type property. This flag indicates whether a confirmed email address is required. By Default, it is set to false.

##### RequireConfirmedPhoneNumber
It is Boolean type property. This flag indicates whether a confirmed phone number is required. By Default, it is set to false.
Example
```
services.Configure<IdentityOptions>(options =>  
{     
    //Sign in settings  
    options.SignIn.RequireConfirmedEmail = false;  
    options.SignIn.RequireConfirmedPhoneNumber = false;  
});
```

## UserOptions (User validation settings)
 
It contains the options for user validation. It has following properties.
##### RequireUniqueEmail
It is Boolean type Property. This flag is indicating whether the application requires unique emails for its users. Default it is set to false

##### AllowedUserNameCharacters
It contains list of allowed characters in the username. Default value is "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
Example
```
services.Configure<IdentityOptions>(options =>  
{  
    //User settings  
    options.User.RequireUniqueEmail = true;  
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";  
}); 
```
## ConfigureApplicationCookie(Cookie settings for Application)
 
It contains setting options related to application's cookie. It has following properties.
##### Cookie.Name
It is a name of the cookie. Default value is "AspNetCore.Cookies".

##### Cookie.HttpOnly
It is Boolean type Property. If it is set to true, the cookie is not accessible from client-side scripts. Default it is set to true.

##### ExpireTimeSpan
It is timespan that indicate how much time the authentication ticket stored in the cookie and it will remain valid from the time it is created. Defaults to 14 days.

##### LoginPath
It is a login page path. If a user is unauthorized, they will be redirected to this path. Default value is "/Account/Login".

##### LogoutPath
It is logout page path. If a user is logged out, they will be redirected to this path. Default value is "/Account/Logout".

##### AccessDeniedPath
It is path on that user will redirected When a user fails an authorization check. Default value is "/Account/AccessDenied".

##### SlidingExpiration
It is Boolean type Property. If it is set to true, a new cookie will be issued with a new expiration time when the current cookie is more than halfway through the expiration window. Default it is set to true.

##### ReturnUrlParameter
It is a URL (determines the name of the query string parameter) that is appended by the middleware when a 401 Unauthorized status code is changed to a 302 redirect onto the login path.
The properties AuthenticationScheme and AutomaticAuthenticate are depreciated in 2.x.
 
Example
```
services.ConfigureApplicationCookie(options =>  
{  
    // Cookie settings  
    options.Cookie.Name = "IdentityOverview";  
    options.Cookie.HttpOnly = true;  
    options.Cookie.Expiration = TimeSpan.FromDays(60);  
    options.LoginPath = "/Account/Login";  
    options.LogoutPath = "/Account/Logout";  
    options.AccessDeniedPath = "/Account/AccessDenied";  
    options.SlidingExpiration = true;  
    options.ReturnUrlParameter = "/Home/Index";  
    options.ExpireTimeSpan = TimeSpan.FromDays(60);  
});  
```




