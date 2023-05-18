﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Infrastructure.Core.Persistence.QueryModels;

namespace RothschildHouse.Infrastructure.Core.Persistence.Configurations
{
    internal class VCardTypeConfiguration : IEntityTypeConfiguration<VCardType>
    {
        public void Configure(EntityTypeBuilder<VCardType> builder)
        {
            // Set configuration for entity
            builder.ToTable("VCardType", "dbo");

            // Set key for entity
            builder.HasKey(p => p.Id);

            // Set configuration for columns
            builder
                .Property(p => p.Id)
                .HasColumnType("bigint")
                .IsRequired()
                ;

            builder
                .Property(p => p.Name)
                .HasColumnType("nvarchar")
                .HasMaxLength(200)
                .IsRequired()
                ;
        }
    }
}
