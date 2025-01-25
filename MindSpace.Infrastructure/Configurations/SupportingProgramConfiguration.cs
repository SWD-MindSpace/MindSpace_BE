using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    internal class SupportingProgramConfiguration : IEntityTypeConfiguration<SupportingProgram>
    {
        public void Configure(EntityTypeBuilder<SupportingProgram> builder)
        {
            //Properties
            builder.OwnsOne(s => s.Address, address =>
            {
                address.Property(a => a.Street).IsUnicode().HasMaxLength(100);
                address.Property(a => a.City).IsUnicode().HasMaxLength(50);
                address.Property(a => a.Ward).IsUnicode().HasMaxLength(50);
                address.Property(a => a.Province).IsUnicode().HasMaxLength(50);
                address.Property(a => a.PostalCode).HasMaxLength(10);
            });

            //Relationships
            builder
                .HasOne(sp => sp.Manager)
                .WithMany(m => m.SupportingPrograms)
                .HasForeignKey(sp => sp.ManagerId);

            builder
                .HasOne(sp => sp.Psychologist)
                .WithMany(m => m.SupportingPrograms)
                .HasForeignKey(sp => sp.PsychologistId);
        }
    }
}
