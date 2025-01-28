﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindSpace.Domain.Entities.Tests;

namespace MindSpace.Infrastructure.Configurations
{
    public class TestScoreRankConfiguration : IEntityTypeConfiguration<TestScoreRank>
    {
        public void Configure(EntityTypeBuilder<TestScoreRank> builder)
        {
            builder.ToTable("TestScoreRanks", t =>
            {
                t.HasCheckConstraint("CK_TestScore_ScoreOrder", "[MaxScore] >= [MinScore]");
            });

            // 1 Test - M TestScoreRank
            builder.HasOne(ts => ts.Specialization)
                .WithMany(s => s.TestScoreRanks)
                .HasForeignKey(ts => ts.SpecilizationId);

            // Scores must be >= 0 and <= 999
            builder.Property(ts => ts.MinScore)
                .IsRequired()
                .HasAnnotation("Range", new[] { "0", "999" });

            builder.Property(ts => ts.MaxScore)
                .IsRequired()
                .HasAnnotation("Range", new[] { "0", "999" });

            builder.Property(ts => ts.Result).IsUnicode().HasMaxLength(500);
        }
    }
}
