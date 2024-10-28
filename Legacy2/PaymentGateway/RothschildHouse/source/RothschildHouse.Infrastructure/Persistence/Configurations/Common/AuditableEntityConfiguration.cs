using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Common;

namespace RothschildHouse.Infrastructure.Persistence.Configurations.Common;

internal abstract class AuditableEntityConfiguration<TAuditableEntity> : IEntityTypeConfiguration<TAuditableEntity> where TAuditableEntity : AuditableEntity
{
    public virtual void Configure(EntityTypeBuilder<TAuditableEntity> builder)
    {
        builder
            .Property(p => p.Active)
            .HasColumnType("bit")
            .IsRequired()
            ;

        builder
            .Property(p => p.CreationUser)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            .IsRequired()
            ;

        builder
            .Property(p => p.CreationDateTime)
            .HasColumnType("datetime")
            .IsRequired()
            ;

        builder
            .Property(p => p.LastUpdateUser)
            .HasColumnType("nvarchar")
            .HasMaxLength(50)
            ;

        builder
            .Property(p => p.LastUpdateDateTime)
            .HasColumnType("datetime")
            ;

        builder
            .Property(p => p.Version)
            .HasColumnType("rowversion")
            ;

        // Set row version token for entity

        builder
            .Property(p => p.Version)
            .ValueGeneratedOnAddOrUpdate()
            .IsRowVersion()
            ;
    }
}
