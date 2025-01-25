﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MindSpace.Infrastructure.Persistence;

#nullable disable

namespace MindSpace.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
<<<<<<<< HEAD:MindSpace.Infrastructure/Migrations/20250125073336_AddData.Designer.cs
    [Migration("20250125073336_AddData")]
    partial class AddData
========
    [Migration("20250125060707_InitDb")]
    partial class InitDb
>>>>>>>> 37255f149271ebc5dd941b85c4a221131b1f85fc:MindSpace.Infrastructure/Migrations/20250125060707_InitDb.Designer.cs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.ApplicationRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ImageUrl")
                        .HasMaxLength(-1)
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Enabled");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhoneNumber")
                        .IsUnique()
                        .HasFilter("[PhoneNumber] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.UseTptMappingStrategy();
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ContactEmail")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("JoinDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("SchoolManagerId")
                        .HasColumnType("int");

                    b.Property<string>("SchoolName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ContactEmail")
                        .IsUnique();

                    b.HasIndex("PhoneNumber")
                        .IsUnique();

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Specification", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(64)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("Specifications");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.SupportingProgram", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<int>("MaxQuantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<string>("PdffileUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("PsychologistId")
                        .HasColumnType("int");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("ThumbnailUrl")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("PsychologistId");

                    b.HasIndex("SchoolId");

                    b.ToTable("SupportingPrograms");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.Test", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValue(0m);

                    b.Property<int>("QuestionCount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("TestCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("TestStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Enabled");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.HasIndex("TestCategoryId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Test");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("TestCategory");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestQuestion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Description")
                        .HasMaxLength(100)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("QuestionFormat")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.ToTable("TestQuestion");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestResponse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<int>("TotalScore")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.HasIndex("TestId");

                    b.ToTable("TestResponse");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestTestQuestion", b =>
                {
                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<int>("TestQuestionId")
                        .HasColumnType("int");

                    b.HasKey("TestId", "TestQuestionId");

                    b.HasIndex("TestQuestionId");

                    b.ToTable("TestTestQuestion");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.Psychologist", b =>
                {
                    b.HasBaseType("MindSpace.Domain.Entities.Identity.ApplicationUser");

                    b.Property<double>("AverageRating")
                        .HasColumnType("float");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<decimal>("ComissionRate")
                        .HasColumnType("decimal(5, 2)");

                    b.Property<decimal>("SessionPrice")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int>("SpecificationId")
                        .HasColumnType("int");

                    b.HasIndex("SpecificationId");

                    b.ToTable("Psychologists", (string)null);
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.SchoolManager", b =>
                {
                    b.HasBaseType("MindSpace.Domain.Entities.Identity.ApplicationUser");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasIndex("SchoolId")
                        .IsUnique()
                        .HasFilter("[SchoolId] IS NOT NULL");

                    b.ToTable("SchoolManager", (string)null);
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.Student", b =>
                {
                    b.HasBaseType("MindSpace.Domain.Entities.Identity.ApplicationUser");

                    b.Property<int>("SchoolId")
                        .HasColumnType("int");

                    b.HasIndex("SchoolId");

                    b.ToTable("Students", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.School", b =>
                {
                    b.OwnsOne("MindSpace.Domain.Entities.Address", "Address", b1 =>
                        {
                            b1.Property<int>("SchoolId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Province")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Ward")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("SchoolId");

                            b1.ToTable("Schools");

                            b1.WithOwner()
                                .HasForeignKey("SchoolId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.SupportingProgram", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.SchoolManager", "SchoolManager")
                        .WithMany("SupportingPrograms")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Identity.Psychologist", "Psychologist")
                        .WithMany("SupportingPrograms")
                        .HasForeignKey("PsychologistId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

<<<<<<<< HEAD:MindSpace.Infrastructure/Migrations/20250125073336_AddData.Designer.cs
                    b.HasOne("MindSpace.Domain.Entities.School", "School")
                        .WithMany("SupportingPrograms")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.OwnsOne("MindSpace.Domain.Entities.Address", "Address", b1 =>
========
                    b.OwnsOne("MindSpace.Domain.Entities.Owned.Address", "Address", b1 =>
>>>>>>>> 37255f149271ebc5dd941b85c4a221131b1f85fc:MindSpace.Infrastructure/Migrations/20250125060707_InitDb.Designer.cs
                        {
                            b1.Property<int>("SupportingProgramId")
                                .HasColumnType("int");

                            b1.Property<string>("City")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("PostalCode")
                                .HasMaxLength(10)
                                .HasColumnType("nvarchar(10)");

                            b1.Property<string>("Province")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.Property<string>("Street")
                                .HasMaxLength(100)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(100)");

                            b1.Property<string>("Ward")
                                .HasMaxLength(50)
                                .IsUnicode(true)
                                .HasColumnType("nvarchar(50)");

                            b1.HasKey("SupportingProgramId");

<<<<<<<< HEAD:MindSpace.Infrastructure/Migrations/20250125073336_AddData.Designer.cs
                            b1.ToTable("SupportingPrograms");
========
                            b1.ToTable("SupportingProgram");
>>>>>>>> 37255f149271ebc5dd941b85c4a221131b1f85fc:MindSpace.Infrastructure/Migrations/20250125060707_InitDb.Designer.cs

                            b1.WithOwner()
                                .HasForeignKey("SupportingProgramId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
<<<<<<<< HEAD:MindSpace.Infrastructure/Migrations/20250125073336_AddData.Designer.cs
========

                    b.Navigation("Manager");
>>>>>>>> 37255f149271ebc5dd941b85c4a221131b1f85fc:MindSpace.Infrastructure/Migrations/20250125060707_InitDb.Designer.cs

                    b.Navigation("Psychologist");

                    b.Navigation("School");

                    b.Navigation("SchoolManager");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.Test", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", "Manager")
                        .WithMany("Tests")
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Tests.TestCategory", "TestCategory")
                        .WithMany("Tests")
                        .HasForeignKey("TestCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manager");

                    b.Navigation("TestCategory");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestResponse", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", "Student")
                        .WithMany("TestResponses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Tests.Test", "Test")
                        .WithMany("TestResponses")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");

                    b.Navigation("Test");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestTestQuestion", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Tests.Test", "Test")
                        .WithMany("TestTestQuestions")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Tests.TestQuestion", "TestQuestion")
                        .WithMany("TestTestQuestions")
                        .HasForeignKey("TestQuestionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");

                    b.Navigation("TestQuestion");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.Psychologist", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", "User")
                        .WithOne("Psychologist")
                        .HasForeignKey("MindSpace.Domain.Entities.Identity.Psychologist", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.Specification", "Specification")
                        .WithMany("Psychologists")
                        .HasForeignKey("SpecificationId")
                        .IsRequired();

                    b.Navigation("Specification");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.SchoolManager", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", "User")
                        .WithOne("Manager")
                        .HasForeignKey("MindSpace.Domain.Entities.Identity.SchoolManager", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.School", "School")
                        .WithOne("SchoolManager")
                        .HasForeignKey("MindSpace.Domain.Entities.Identity.SchoolManager", "SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.Student", b =>
                {
                    b.HasOne("MindSpace.Domain.Entities.Identity.ApplicationUser", "User")
                        .WithOne("Student")
                        .HasForeignKey("MindSpace.Domain.Entities.Identity.Student", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MindSpace.Domain.Entities.School", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.ApplicationUser", b =>
                {
                    b.Navigation("Manager")
                        .IsRequired();

                    b.Navigation("Psychologist")
                        .IsRequired();

                    b.Navigation("Student")
                        .IsRequired();

                    b.Navigation("TestResponses");

                    b.Navigation("Tests");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.School", b =>
                {
                    b.Navigation("SchoolManager")
                        .IsRequired();

                    b.Navigation("Students");

                    b.Navigation("SupportingPrograms");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Specification", b =>
                {
                    b.Navigation("Psychologists");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.Test", b =>
                {
                    b.Navigation("TestResponses");

                    b.Navigation("TestTestQuestions");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestCategory", b =>
                {
                    b.Navigation("Tests");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Tests.TestQuestion", b =>
                {
                    b.Navigation("TestTestQuestions");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.Psychologist", b =>
                {
                    b.Navigation("SupportingPrograms");
                });

            modelBuilder.Entity("MindSpace.Domain.Entities.Identity.SchoolManager", b =>
                {
                    b.Navigation("SupportingPrograms");
                });
#pragma warning restore 612, 618
        }
    }
}
