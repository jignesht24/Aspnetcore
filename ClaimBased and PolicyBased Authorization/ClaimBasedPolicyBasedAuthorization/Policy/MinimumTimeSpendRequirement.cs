using Microsoft.AspNetCore.Authorization;

namespace ClaimBasedPolicyBasedAuthorization.Policy
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
