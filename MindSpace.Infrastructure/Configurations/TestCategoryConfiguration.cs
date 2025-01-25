using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    public class TestCategoryConfiguration : IEntityTypeConfiguration<TestCategory>
    {
        public void Configure(EntityTypeBuilder<TestCategory> builder)
        {
            //Properties
            builder.Property(tc => tc.Name).IsRequired().HasMaxLength(100);
        }
    }
}
