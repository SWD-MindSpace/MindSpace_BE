﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            //Indexing
            builder.HasIndex(u => u.PhoneNumber).IsUnique();
            builder.HasIndex(u => u.Email).IsUnique();

            //Properties
            builder.Property(u => u.CreatedAt).ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
            builder.Property(u => u.UpdatedAt).ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            builder.Property(u => u.Status)
                .HasConversion(
                    v => v.ToString(),
                    v => (UserStatus)Enum.Parse(typeof(UserStatus), v))
                .HasDefaultValue(UserStatus.Enabled);
            builder.Property(u => u.ImageUrl).HasMaxLength(-1);
            builder.Property(u => u.FullName).HasMaxLength(256).IsUnicode();
        }
    }
}