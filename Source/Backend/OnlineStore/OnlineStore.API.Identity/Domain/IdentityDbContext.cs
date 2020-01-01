using Microsoft.EntityFrameworkCore;

namespace OnlineStore.API.Identity.Domain
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
            // Set key for entities

            modelBuilder
                .Entity<User>(builder => builder.HasKey(e => e.UserID));

            modelBuilder
                .Entity<UserClaim>(builder => builder.HasKey(e => e.UserClaimID));

            base.OnModelCreating(modelBuilder);
        }
    }
}
