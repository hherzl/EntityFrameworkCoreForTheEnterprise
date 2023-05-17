using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Identity.Data.Models;

namespace RothschildHouse.Identity.Data;

public class RothschildHouseDbContext : IdentityDbContext<RothschildHouseUser>
{
    public RothschildHouseDbContext(DbContextOptions<RothschildHouseDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
