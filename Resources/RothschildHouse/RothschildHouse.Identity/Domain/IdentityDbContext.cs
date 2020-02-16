using Microsoft.EntityFrameworkCore;

namespace RothschildHouse.Identity.Domain
{
    public class IdentityDbContext : DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<UserClaim> UserClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Add configuration for keys

            modelBuilder
                .Entity<User>(builder => builder.HasKey(p => p.UserID));

            modelBuilder
                .Entity<UserClaim>(builder => builder.HasKey(p => p.UserClaimID));

            base.OnModelCreating(modelBuilder);
        }
    }
}
