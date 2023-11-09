﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RothschildHouse.Domain.Entities;
using RothschildHouse.Infrastructure.Persistence.Configurations.Common;

namespace RothschildHouse.Infrastructure.Persistence.Configurations;

internal class EnumDescriptionConfiguration : EntityConfiguration<EnumDescription>
{
    public override void Configure(EntityTypeBuilder<EnumDescription> builder)
    {
        base.Configure(builder);

        // Set configuration for entity
        builder.ToTable("EnumDescription", "dbo");

        // Set key for entity
        builder.HasKey(p => p.Id);

        // Set configuration for columns
        builder
            .Property(p => p.Id)
            .HasColumnType("smallint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Type)
            .HasColumnType("nvarchar")
            .HasMaxLength(511)
            .IsRequired()
            ;

        builder
            .Property(p => p.Value)
            .HasColumnType("bigint")
            .IsRequired()
            ;

        builder
            .Property(p => p.Description)
            .HasColumnType("nvarchar")
            .HasMaxLength(200)
            .IsRequired()
            ;
    }
}
