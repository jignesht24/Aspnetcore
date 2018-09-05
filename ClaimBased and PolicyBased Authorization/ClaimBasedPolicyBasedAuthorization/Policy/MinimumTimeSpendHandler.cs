using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace ClaimBasedPolicyBasedAuthorization.Policy
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
