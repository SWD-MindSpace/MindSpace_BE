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
    internal class PsychologistConfiguration : IEntityTypeConfiguration<Psychologist>
    {
        public void Configure(EntityTypeBuilder<Psychologist> builder)
        {
            // Create TPT for Psychologist
            builder.ToTable("Psychologists");

            // Fields
            builder.Property(p => p.Bio)
                .HasMaxLength(1000).IsUnicode(true);

            builder.Property(p => p.AverageRating)
                .HasColumnType("float");

            builder.Property(p => p.SessionPrice)
                .HasColumnType("decimal(18, 2)");

            builder.Property(p => p.ComissionRate)
                .HasColumnType("decimal(5, 2)");


            // 1 Specification - 1 User
            builder.HasOne(p => p.User)
                .WithOne(u => u.Psychologist)
                .HasForeignKey<Psychologist>(p => p.Id)
                .OnDelete(DeleteBehavior.ClientCascade);

            // 1 Specification - M Psychologists
            builder.HasOne(p => p.Specification)
                .WithMany(s => s.Psychologists)
                .HasForeignKey(p => p.SpecificationId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
