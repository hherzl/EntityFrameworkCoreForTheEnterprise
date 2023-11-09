using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Infrastructure.Persistence.Configurations.Common;

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
