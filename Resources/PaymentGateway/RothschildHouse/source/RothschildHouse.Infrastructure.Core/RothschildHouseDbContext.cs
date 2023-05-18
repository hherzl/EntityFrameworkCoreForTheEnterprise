using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RothschildHouse.Application.Core.Common.Contracts;
using RothschildHouse.Domain.Core.Entities;

namespace RothschildHouse.Infrastructure.Core
{
    public class RothschildHouseDbContext : DbContext, IRothschildHouseDbContext
    {
        public RothschildHouseDbContext(DbContextOptions<RothschildHouseDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly())
                ;

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Country> Country { get; set; }
    }
}
