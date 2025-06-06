﻿// <auto-generated />
using System;
using BlindW.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BlindW.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BlindW.Data.Models.Leaderboard", b =>
                {
                    b.Property<int>("LeaderboardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LeaderboardId"));

                    b.Property<string>("RankingType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TestResultId")
                        .HasColumnType("integer");

                    b.HasKey("LeaderboardId");

                    b.HasIndex("TestResultId");

                    b.ToTable("Leaderboards");
                });

            modelBuilder.Entity("BlindW.Data.Models.Lesson", b =>
                {
                    b.Property<int>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LessonId"));

                    b.Property<string>("LessonText")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("LevelId")
                        .HasColumnType("integer");

                    b.HasKey("LessonId");

                    b.HasIndex("LevelId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("BlindW.Data.Models.Level", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("LevelId"));

                    b.Property<string>("LevelDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LevelName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LevelId");

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestDuration", b =>
                {
                    b.Property<int>("TestDurationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TestDurationId"));

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.HasKey("TestDurationId");

                    b.ToTable("TestDurations");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestResult", b =>
                {
                    b.Property<int>("TestResultId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TestResultId"));

                    b.Property<double>("Accuracy")
                        .HasColumnType("double precision");

                    b.Property<int>("CountCharacters")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TestDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("TestSettingId")
                        .HasColumnType("integer");

                    b.Property<double>("TotalTime")
                        .HasColumnType("double precision");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Wpm")
                        .HasColumnType("double precision");

                    b.HasKey("TestResultId");

                    b.HasIndex("TestSettingId");

                    b.HasIndex("UserId");

                    b.ToTable("TestResults");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestSetting", b =>
                {
                    b.Property<int>("TestSettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TestSettingId"));

                    b.Property<bool>("IsNumbersEnabled")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPunctuationEnabled")
                        .HasColumnType("boolean");

                    b.Property<int>("TestDurationId")
                        .HasColumnType("integer");

                    b.Property<int>("TestTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("WordCountId")
                        .HasColumnType("integer");

                    b.HasKey("TestSettingId");

                    b.HasIndex("TestDurationId");

                    b.HasIndex("TestTypeId");

                    b.HasIndex("WordCountId");

                    b.ToTable("TestSettings");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestType", b =>
                {
                    b.Property<int>("TestTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("TestTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("TestTypeId");

                    b.ToTable("TestTyps");
                });

            modelBuilder.Entity("BlindW.Data.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult10Words")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult15s")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult25Words")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult30s")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult50Words")
                        .HasColumnType("integer");

                    b.Property<int>("BestResult60s")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<int>("TestsCompleted")
                        .HasColumnType("integer");

                    b.Property<int>("TestsTaken")
                        .HasColumnType("integer");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BlindW.Data.Models.WordCount", b =>
                {
                    b.Property<int>("WordCountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("WordCountId"));

                    b.Property<int>("Count")
                        .HasColumnType("integer");

                    b.HasKey("WordCountId");

                    b.ToTable("WordCounts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BlindW.Data.Models.Leaderboard", b =>
                {
                    b.HasOne("BlindW.Data.Models.TestResult", "TestResult")
                        .WithMany()
                        .HasForeignKey("TestResultId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("BlindW.Data.Models.Lesson", b =>
                {
                    b.HasOne("BlindW.Data.Models.Level", "Level")
                        .WithMany("Lessons")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestResult", b =>
                {
                    b.HasOne("BlindW.Data.Models.TestSetting", "TestSetting")
                        .WithMany("TestResult")
                        .HasForeignKey("TestSettingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindW.Data.Models.User", "User")
                        .WithMany("TestResults")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestSetting");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestSetting", b =>
                {
                    b.HasOne("BlindW.Data.Models.TestDuration", "TestDuration")
                        .WithMany("TestSetting")
                        .HasForeignKey("TestDurationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindW.Data.Models.TestType", "TestType")
                        .WithMany("TestSetting")
                        .HasForeignKey("TestTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindW.Data.Models.WordCount", "WordCount")
                        .WithMany("TestSetting")
                        .HasForeignKey("WordCountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TestDuration");

                    b.Navigation("TestType");

                    b.Navigation("WordCount");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BlindW.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BlindW.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BlindW.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BlindW.Data.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BlindW.Data.Models.Level", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestDuration", b =>
                {
                    b.Navigation("TestSetting");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestSetting", b =>
                {
                    b.Navigation("TestResult");
                });

            modelBuilder.Entity("BlindW.Data.Models.TestType", b =>
                {
                    b.Navigation("TestSetting");
                });

            modelBuilder.Entity("BlindW.Data.Models.User", b =>
                {
                    b.Navigation("TestResults");
                });

            modelBuilder.Entity("BlindW.Data.Models.WordCount", b =>
                {
                    b.Navigation("TestSetting");
                });
#pragma warning restore 612, 618
        }
    }
}
