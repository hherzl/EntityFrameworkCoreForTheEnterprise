using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common
{
    internal abstract class EntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder
                .Property(p => p.Active)
                .HasColumnType("bit")
                .IsRequired()
                ;
        }
    }
}
