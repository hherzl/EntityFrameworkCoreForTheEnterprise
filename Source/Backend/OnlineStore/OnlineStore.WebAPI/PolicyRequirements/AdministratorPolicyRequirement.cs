using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.WebAPI.PolicyRequirements
{
#pragma warning disable CS1591
    public class AdministratorPolicyRequirement : AuthorizationHandler<AdministratorPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdministratorPolicyRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == "role" && claim.Value == "Administrator"))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.FromResult(0);
        }
    }
#pragma warning restore CS1591
}
