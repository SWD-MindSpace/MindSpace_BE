using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    internal class TestTestQuestionConfiguration : IEntityTypeConfiguration<TestTestQuestion>
    {
        public void Configure(EntityTypeBuilder<TestTestQuestion> builder)
        {
            //Indexing
            builder.HasKey(ttq => new { ttq.TestId, ttq.TestQuestionId }); // Composite primary key

            //Relationships
            builder
                .HasOne(ttq => ttq.Test)
                .WithMany(t => t.TestTestQuestions)
                .HasForeignKey(ttq => ttq.TestId)
                .IsRequired();

            builder
                .HasOne(ttq => ttq.TestQuestion)
                .WithMany(tq => tq.TestTestQuestions)
                .HasForeignKey(ttq => ttq.TestQuestionId)
                .IsRequired();
        }
    }
}
