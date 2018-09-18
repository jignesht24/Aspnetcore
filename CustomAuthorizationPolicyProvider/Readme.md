### Creating Custom Authorization Policy Provider in ASP.net code

In [my previous Article](https://github.com/jignesht24/Aspnetcore/tree/master/ClaimBased%20and%20PolicyBased%20Authorization), I have talk about Policy-based authorization. Here, we can register all the required policies using AddPolicy method of AuthorizationOptions class. If we have large number of policies, this is not desirable way to register all policies in this way. In such case, we can use custom policy provider (IAuthorizationPolicyProvider).

Following scenarios may be candidate for Custom Authorization Policy Provider
* Policy evalution provided by the external service
* For large range of policies (i.e. verity of policies are to provide access it doesn't make any sense to add every policy to authorization options)
* Create Policy that use runtime information to provide access 

Example:
In [my previous Article](https://github.com/jignesht24/Aspnetcore/tree/master/ClaimBased%20and%20PolicyBased%20Authorization), I have explain about to add minimum time spend policy example. Here I have added policy that allows to access for the employee who completed 365 days in the company.If the the different controller action available for different days completed in the company employee, it does not make any sense to add many policies to AuthorizationOptions.AddPolicy method. In such case, we can use IAuthorizationPolicyProvider (custom policy provider).

The IAuthorizationPolicyProvider interface is used to retrieve authorization policies. The DefaultAuthorizationPolicyProvider is register and used to retrieve authorization policies that are provided by the AuthorizationOptions in AddAuthorization method. We can customize the behaviour by doing different implementation of IAuthorizationPolicyProvider interface. This interface contains following two API
* GetPolicyAsync : This method returns authorization police for a provided name
* GetDefaultPolicyAsync: This method returns the default authorization policy that used by "Authorize" attribute without specifying any policy.

Example
I have different action methods in controller and these actions can be accessed by different group of user i.e. "Method1" can be accessed by user who complated 365 or more days in company, "Method2" is accessed by the user who complated 180 or more days in company and "Method3" is accessed by the user who completed 10 or more days in company. This requirement can be achieved by creating custom authorization policy provider using IAuthorizationPolicyProvider.

Here, I will create policy run time and assigned this policy to customize authorize attribute. The authorization policy is identified by the name and policy name will be generated based on the custom authorize attribute parameter. 

I can achieve my requirement using following four steps

#### Step:1 Create custom Authorize filter
The first step to create custom authorize attribute that accept no of days as input and based on the input value generate policy name and assign to "Policy" property of based class. So, when execute this filter, it will consider policy rules that is provided to validate the user's access.

In the following example code, I have created "MinimumTimeSpendAuthorize" attribute and based on input it dynamically created policy name i.e. "MinimumTimeSpend.{input value}" and assign to Policy property. This policy can be created and register in next step.
```
using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Policy
{
    public class MinimumTimeSpendAuthorize : AuthorizeAttribute
    {
        public MinimumTimeSpendAuthorize(int days)
        {
            NoOfDays = days;
        }

        int days;

        public int NoOfDays
        {
            get
            {
                return days;
            }
            set
            {
                days = value;
                Policy = $"{"MinimumTimeSpend"}.{value.ToString()}";
            }
        }
    }
}
```
#### Step 2: Create Authorization Requirement and Authorization handler
The Authorization Requirement is the collection of data which can be used to evaluate the user principal and the Authorization handler contains evaluation mechanism for properties of requirement. One required may associated with multiple handler.

Here, I have created the requirement for minimum time spent for the organization and created the handler that calculate no of days for employee by subtract today date from claim "DateOfJoing" and if the result is grater than or equal to supplied data then the user is authorized to access. 

##### Authorization Requirement
```
using Microsoft.AspNetCore.Authorization;

namespace PolicyBasedAuthorization.Policy
{
    public class MinimumTimeSpendRequirement : IAuthorizationRequirement
    {
        public MinimumTimeSpendRequirement(int noOfDays)
        {
            TimeSpendInDays = noOfDays;
        }

        public int TimeSpendInDays { get; private set; }
    }
}
```
##### Authorization handler
```
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace PolicyBasedAuthorization.Policy
{
    public class MinimumTimeSpendHandler : AuthorizationHandler<MinimumTimeSpendRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumTimeSpendRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "DateOfJoining"))
            {
                return Task.FromResult(0);
            }

            var dateOfJoining = Convert.ToDateTime(context.User.FindFirst(
                c => c.Type == "DateOfJoining").Value);

            double calculatedTimeSpend = (DateTime.Now.Date - dateOfJoining.Date).TotalDays;

            if (calculatedTimeSpend >= requirement.TimeSpendInDays)
            {
                context.Succeed(requirement);
            }
            return Task.FromResult(0);
        }
    }
}
```
#### Step 3: Create custom policy provider
In this step, we will create the policy by implementing IAuthorizationPolicyProvider. This interface has two abstract methods: GetDefaultPolicyAsync and GetPolicyAsync. In GetDefaultPolicyAsync method, I will return all the policies that provided by the default provider. In GetPolicyAsync method, I will check the policy name pattern is following pattern (i.e. MinimumTimeSpend.{input value}) generated in custom authorize attribute. If the pattern match, then I will extract the no of days from policy name and supplied to requirement. 
```
using System;
using System.Threading.Tasks;
using ClaimBasedPolicyBasedAuthorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace PolicyBasedAuthorization.Policy
{
    public class MinimumTimeSpendPolicy : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider defaultPolicyProvider { get; }
        public MinimumTimeSpendPolicy(IOptions<AuthorizationOptions> options)
        {
            defaultPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return defaultPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            string[] subStringPolicy = policyName.Split(new char[] { '.' });
            if (subStringPolicy.Length > 1 && subStringPolicy[0].Equals("MinimumTimeSpend", StringComparison.OrdinalIgnoreCase) && int.TryParse(subStringPolicy[1], out var days))
            {
                var policy = new AuthorizationPolicyBuilder();
                policy.AddRequirements(new MinimumTimeSpendRequirement(days));
                return Task.FromResult(policy.Build());
            }
            return defaultPolicyProvider.GetPolicyAsync(policyName);
        }
    }
}
```
#### Step 4: Register Handler and Policy Provider
The next and final step is to register authorization handler and policy provider. To use custom policies, we must register authorization handler that associated requirement used in custom policy and also need to register custom policy provider in ConfigureServices method of startup class.
```
public void ConfigureServices(IServiceCollection services)
{
	...
	....
	services.AddTransient<IAuthorizationPolicyProvider, MinimumTimeSpendPolicy>();
    services.AddSingleton<IAuthorizationHandler, MinimumTimeSpendHandler>();

    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    services.AddAuthorization();
}
```
To get the desired result, we need to apply custom authorize filter to controller action method. In following example code, "Method1" can be accessed by user who completed 365 or more days in company, "Method2" is accessed by the user who completed 180 or more days in company and "Method3" is accessed by the user who completed 10 or more days in company.
```
public class DemoController : Controller
{
    [MinimumTimeSpendAuthorize(180)]
    public IActionResult TestMethod2()
    {
        return View("MyPage");
    }
    [MinimumTimeSpendAuthorize(365)]
    public IActionResult TestMethod1()
    {
        return View("MyPage");
    }
    [MinimumTimeSpendAuthorize(10)]
    public IActionResult TestMethod3()
    {
        return View("MyPage");
    }
}
```
#### Summary
Using the method describe in this article, we can create custom authorization policy providers using IAuthorizationPolicyProvider. It is very useful when we have large range of policies or policy created based on runtime information.