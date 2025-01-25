using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Identity;
using MindSpace.Domain.Entities.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestResponseConfiguration : IEntityTypeConfiguration<TestResponse>
    {
        public void Configure(EntityTypeBuilder<TestResponse> builder)
        { 
            builder.Property(tr => tr.TotalScore).IsRequired();

            //Relationships
            builder
                .HasOne(tr => tr.Student)
                .WithMany(st => st.TestResponses)
                .HasForeignKey(tr => tr.StudentId);

            builder
                .HasOne(tr => tr.Test)
                .WithMany(t => t.TestResponses)
                .HasForeignKey(tr => tr.TestId);
        }
    }
}
