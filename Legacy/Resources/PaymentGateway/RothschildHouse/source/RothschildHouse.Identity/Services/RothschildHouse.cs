using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using RothschildHouse.Identity.Data.Models;

namespace RothschildHouse.Identity.Services;

public class RothschildHouseProfileService : IProfileService
{
    private readonly UserManager<RothschildHouseUser> _userManager;

    public RothschildHouseProfileService(UserManager<RothschildHouseUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var subjectId = context.Subject.GetSubjectId();

        var user = await _userManager.FindByIdAsync(subjectId);

        var claims = await _userManager.GetClaimsAsync(user);

        foreach (var claim in claims)
        {
            context.IssuedClaims.Add(new Claim(claim.Type, claim.Value));
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var subjectId = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(subjectId);
        context.IsActive = user != null;
    }
}
