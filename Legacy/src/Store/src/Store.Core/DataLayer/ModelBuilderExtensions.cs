using Microsoft.EntityFrameworkCore;
using Store.Core.DataLayer.Mapping;

namespace Store.Core.DataLayer
{
    // todo: Remove this class for EF Core 2
    public static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConfiguration<TEntity>(this ModelBuilder modelBuilder, IEntityTypeConfiguration<TEntity> entityMap) where TEntity : class, new()
        {
            entityMap.Configure(modelBuilder.Entity<TEntity>());

            return modelBuilder;
        }
    }
}
