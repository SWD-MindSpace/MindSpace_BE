﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    internal class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.ToTable("Manager").HasBaseType<ApplicationUser>();

            // 1 Manager - 1 User 
            builder.HasOne(m => m.User)
                .WithOne(u => u.Manager)
                .HasForeignKey<Manager>(u => u.Id);
        }
    }
}
