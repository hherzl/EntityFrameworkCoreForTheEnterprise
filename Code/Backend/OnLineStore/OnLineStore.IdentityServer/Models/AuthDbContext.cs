using Microsoft.EntityFrameworkCore;

namespace OnlineStore.IdentityServer.Models
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
            // Set key for entities

            modelBuilder
                .Entity<User>(builder => builder.HasKey(e => e.UserID));

            modelBuilder
                .Entity<UserClaim>(builder => builder.HasKey(e => e.UserClaimID));

            base.OnModelCreating(modelBuilder);
        }
    }
}
