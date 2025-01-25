using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Constants;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            //Indexing
            builder.HasIndex(t => t.Title).IsUnique();

            //Properties
            builder.Property(t => t.Title).IsUnicode().IsRequired().HasMaxLength(100);
            builder.Property(t => t.Description).IsUnicode().HasMaxLength(100);
            builder.Property(t => t.QuestionCount).HasDefaultValue(0);
            builder.Property(t => t.Price).HasPrecision(18, 2).HasDefaultValue(0m); //Allow 18 digits (integer + fractional) and 2 digits after decimal
            builder.Property(t => t.TestStatus)
            .HasConversion(
                v => v.ToString(),
                v => (TestStatus)Enum.Parse(typeof(TestStatus), v)) // convert status to string for db
            .HasDefaultValue(TestStatus.Enabled); // set defaults to 'Enabled'
            //Relationships
            builder
                .HasOne(t => t.TestCategory)
                .WithMany(tc => tc.Tests)
                .HasForeignKey(t => t.TestCategoryId);
            builder
                .HasOne(t => t.Manager)
                .WithMany(tc => tc.Tests)
                .HasForeignKey(t => t.ManagerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
