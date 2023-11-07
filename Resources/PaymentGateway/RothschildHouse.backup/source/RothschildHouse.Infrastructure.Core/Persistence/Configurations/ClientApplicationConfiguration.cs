using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Core.Entities;
using RothschildHouse.Infrastructure.Core.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class ClientApplicationConfiguration : AuditableEntityConfiguration<ClientApplication>
    {
        public override void Configure(EntityTypeBuilder<ClientApplication> builder)
        {
            base.Configure(builder);

            // Set configuration for entity
            builder.ToTable("ClientApplication", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("uniqueidentifier")
                .IsRequired()
                ;

            builder
                .Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(100)
                .IsRequired()
                ;

            builder
                .Property(p => p.Url)
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                ;

            // Add configuration for uniques

            builder
                .HasIndex(p => p.Name)
                .IsUnique()
                .HasDatabaseName("UQ_dbo_ClientApplication_Name")
                ;
        }
    }
}
