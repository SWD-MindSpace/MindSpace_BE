using Microsoft.EntityFrameworkCore;
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
            // Create TPT for Psychologist
            builder.ToTable("Managers");

            builder
                .HasOne(m => m.User)
                .WithOne(au => au.Manager)
                .HasForeignKey<Manager>(m => m.Id);
            builder
                .HasOne(m => m.School)
                .WithOne(sc => sc.SchoolManager)
                .HasForeignKey<Manager>(m => m.SchoolId);
        }
    }
}
