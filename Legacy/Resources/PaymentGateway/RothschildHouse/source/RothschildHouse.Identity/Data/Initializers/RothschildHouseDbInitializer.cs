using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Identity.Data.Models;
using RothschildHouse.Identity.Tokens;
using Serilog;

namespace RothschildHouse.Identity.Data.Initializers;

public class RothschildHouseDbInitializer
{
    private readonly RothschildHouseDbContext _dbContext;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<RothschildHouseUser> _userManager;

    public RothschildHouseDbInitializer(RothschildHouseDbContext dbContext, RoleManager<IdentityRole> roleManager, UserManager<RothschildHouseUser> userManager)
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task InitializeAsync()
    {
        _dbContext.Database.Migrate();

        var result = await _roleManager.CreateAsync(new IdentityRole
        {
            Id = Guid.NewGuid().ToString(),
            Name = RothschildHouseRoles.Admin
        });

        if (result.Succeeded)
            Log.Debug($"Role '{RothschildHouseRoles.Admin}' was created successfully");
        else
            throw new Exception(result.Errors.First().Description);

        var hherzl = await _userManager.FindByNameAsync("hherzl");
        if (hherzl == null)
        {
            hherzl = new RothschildHouseUser
            {
                UserName = "hherzl",
                Email = "hherzl@rothschildhouse.com",
                EmailConfirmed = true,
            };

            result = await _userManager.CreateAsync(hherzl, "Pass123$");
            if (!result.Succeeded)
                throw new Exception(result.Errors.First().Description);

            result = await _userManager.AddClaimsAsync(hherzl, new Claim[]
            {
                    new Claim(JwtClaimTypes.Name, "hherzl"),
                    new Claim(JwtClaimTypes.GivenName, "Carlo"),
                    new Claim(JwtClaimTypes.MiddleName, "H"),
                    new Claim(JwtClaimTypes.FamilyName, "Herzl"),
                    new Claim(JwtClaimTypes.WebSite, "https://github.com/hherzl"),
                    new Claim(RothschildHouseClaimTypes.Language, "EN")
            });

            if (result.Succeeded)
            {
                Log.Debug($"User '{hherzl.UserName}' was created successfully");

                result = await _userManager.AddToRoleAsync(hherzl, RothschildHouseRoles.Admin);

                Log.Debug($" '{hherzl.UserName}' was added to '{RothschildHouseRoles.Admin}' role");
            }
            else
            {
                throw new Exception(result.Errors.First().Description);
            }
        }
        else
        {
            Log.Debug($"'{hherzl.UserName}' already exists");
        }
    }
}
