using System.Threading.Tasks;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using OnlineStore.Common.Security;

namespace OnlineStore.WebAPI.PolicyRequirements
{
#pragma warning disable CS1591
    public class WarehouseManagerPolicyRequirement : AuthorizationHandler<WarehouseManagerPolicyRequirement>, IAuthorizationRequirement
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, WarehouseManagerPolicyRequirement requirement)
        {
            if (context.User.HasClaim(claim => claim.Type == JwtClaimTypes.Role && claim.Value == Roles.WarehouseManager))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.FromResult(0);
        }
    }
#pragma warning restore CS1591
}
