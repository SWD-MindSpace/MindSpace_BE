﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;

namespace MindSpace.Infrastructure.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder
                .HasOne(s => s.User)
                .WithOne(au => au.Student)
                .HasForeignKey<Student>(s => s.Id);
            builder
                .HasOne(st => st.School)
                .WithMany(sc => sc.Students)
                .HasForeignKey(st => st.SchoolId);
        }
    }
}
