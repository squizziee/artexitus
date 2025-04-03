﻿// <auto-generated />
using System;
using Artexitus.ProblemMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Artexitus.ProblemMicroservice.Infrastructure.Migrations
{
    [DbContext(typeof(ProblemDatabaseContext))]
    partial class ProblemDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.GeneralProblemStatistics", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SuccessfulSubmissionCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalSubmissionCount")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId")
                        .IsUnique();

                    b.ToTable("GeneralProblemStatistics");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Difficulty")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("MarkdownDescription")
                        .IsRequired()
                        .HasMaxLength(7000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("SequenceNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Problems");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStarterCode", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SourceCode")
                        .IsRequired()
                        .HasMaxLength(7000)
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("LanguageId", "ProblemId");

                    b.HasIndex("ProblemId");

                    b.ToTable("ProblemStarterCodeCollection");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStatistics", b =>
                {
                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("SuccessfulSubmissionCount")
                        .HasColumnType("int");

                    b.Property<int>("TotalSubmissionCount")
                        .HasColumnType("int");

                    b.HasKey("LanguageId", "ProblemId");

                    b.HasIndex("ProblemId");

                    b.ToTable("ProblemStatistics");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProgrammingLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(7000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.Submission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("SourceCode")
                        .IsRequired()
                        .HasMaxLength(7000)
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LanguageId");

                    b.HasIndex("ProblemId");

                    b.ToTable("Submissions");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.SuccesfulSubmission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<long>("ElapsedSpaceInBytes")
                        .HasColumnType("bigint");

                    b.Property<long>("ElapsedTimeInNanoseconds")
                        .HasColumnType("bigint");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<Guid?>("ProblemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("SubmissionId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProblemId");

                    b.HasIndex("SubmissionId")
                        .IsUnique();

                    b.ToTable("SuccesfulSubmissions");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.GeneralProblemStatistics", b =>
                {
                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", "Problem")
                        .WithOne("GeneralStatistics")
                        .HasForeignKey("Artexitus.ProblemMicroservice.Infrastructure.Entities.GeneralProblemStatistics", "ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStarterCode", b =>
                {
                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProgrammingLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", "Problem")
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStatistics", b =>
                {
                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProgrammingLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", "Problem")
                        .WithMany()
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStatisticsBin", "DistributionBySpaceElapsed", b1 =>
                        {
                            b1.Property<Guid>("ProblemStatisticsLanguageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ProblemStatisticsProblemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("CountOfSubmissions")
                                .HasColumnType("int");

                            b1.Property<double>("PercentageOfSubmissions")
                                .HasColumnType("float");

                            b1.Property<long>("RangeLowerBound")
                                .HasColumnType("bigint");

                            b1.Property<long>("RangeUpperBound")
                                .HasColumnType("bigint");

                            b1.Property<Guid>("SubmissionSampleId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ProblemStatisticsLanguageId", "ProblemStatisticsProblemId", "Id");

                            b1.ToTable("ProblemStatistics");

                            b1.ToJson("DistributionBySpaceElapsed");

                            b1.WithOwner()
                                .HasForeignKey("ProblemStatisticsLanguageId", "ProblemStatisticsProblemId");
                        });

                    b.OwnsMany("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProblemStatisticsBin", "DistributionByTimeElapsed", b1 =>
                        {
                            b1.Property<Guid>("ProblemStatisticsLanguageId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<Guid>("ProblemStatisticsProblemId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<int>("CountOfSubmissions")
                                .HasColumnType("int");

                            b1.Property<double>("PercentageOfSubmissions")
                                .HasColumnType("float");

                            b1.Property<long>("RangeLowerBound")
                                .HasColumnType("bigint");

                            b1.Property<long>("RangeUpperBound")
                                .HasColumnType("bigint");

                            b1.Property<Guid>("SubmissionSampleId")
                                .HasColumnType("uniqueidentifier");

                            b1.HasKey("ProblemStatisticsLanguageId", "ProblemStatisticsProblemId", "Id");

                            b1.ToTable("ProblemStatistics");

                            b1.ToJson("DistributionByTimeElapsed");

                            b1.WithOwner()
                                .HasForeignKey("ProblemStatisticsLanguageId", "ProblemStatisticsProblemId");
                        });

                    b.Navigation("DistributionBySpaceElapsed");

                    b.Navigation("DistributionByTimeElapsed");

                    b.Navigation("Language");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.Submission", b =>
                {
                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProgrammingLanguage", "Language")
                        .WithMany("Submissions")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", "Problem")
                        .WithMany("Submissions")
                        .HasForeignKey("ProblemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Language");

                    b.Navigation("Problem");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.SuccesfulSubmission", b =>
                {
                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", null)
                        .WithMany("SuccessfulSubmissions")
                        .HasForeignKey("ProblemId");

                    b.HasOne("Artexitus.ProblemMicroservice.Infrastructure.Entities.Submission", "Submission")
                        .WithOne()
                        .HasForeignKey("Artexitus.ProblemMicroservice.Infrastructure.Entities.SuccesfulSubmission", "SubmissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Submission");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.Problem", b =>
                {
                    b.Navigation("GeneralStatistics")
                        .IsRequired();

                    b.Navigation("Submissions");

                    b.Navigation("SuccessfulSubmissions");
                });

            modelBuilder.Entity("Artexitus.ProblemMicroservice.Infrastructure.Entities.ProgrammingLanguage", b =>
                {
                    b.Navigation("Submissions");
                });
#pragma warning restore 612, 618
        }
    }
}
