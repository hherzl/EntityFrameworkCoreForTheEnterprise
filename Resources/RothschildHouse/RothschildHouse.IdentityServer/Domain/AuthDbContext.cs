using Microsoft.EntityFrameworkCore;

namespace RothschildHouse.IdentityServer.Domain
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
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
