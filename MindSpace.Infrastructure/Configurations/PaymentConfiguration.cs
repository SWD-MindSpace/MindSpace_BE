﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MindSpace.Domain.Entities.Appointments;
using MindSpace.Domain.Entities.Constants;

namespace MindSpace.Infrastructure.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            // 1 user (student) - M payments
            builder.HasOne(p => p.User)
                .WithMany(s => s.Payments)
                .HasForeignKey(a => a.UserId);

            // 1 appointment - M payments
            builder.HasOne(p => p.Appointment)
                .WithMany(a => a.Payments)
                .HasForeignKey(a => a.AppointmentId);

            builder.Property(p => p.AccountNo).HasMaxLength(50);
            
            builder.Property(p => p.TransactionCode).HasMaxLength(50);
            
            builder.Property(p => p.Provider).HasMaxLength(50);
            
            builder.Property(p => p.PaymentDescription).HasMaxLength(1000);
            
            builder.Property(p => p.TransactionTime).HasColumnType("datetime");
            
            builder.Property(builder => builder.PaymentType)
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<PaymentType>(s));
            
            builder.Property(builder => builder.Status)
                .HasConversion(
                    s => s.ToString(),
                    s => Enum.Parse<PaymentStatus>(s));
        }
    }
}
