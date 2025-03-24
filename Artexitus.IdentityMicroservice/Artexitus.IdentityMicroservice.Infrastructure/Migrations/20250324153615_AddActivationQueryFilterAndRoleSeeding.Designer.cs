﻿// <auto-generated />
using System;
using Artexitus.IdentityMicroservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    [DbContext(typeof(IdentityDatabaseContext))]
    [Migration("20250324153615_AddActivationQueryFilterAndRoleSeeding")]
    partial class AddActivationQueryFilterAndRoleSeeding
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ActivationToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<bool>("IsActivated")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LastUpdatedAt")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RefreshToken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("ProfileId")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.UserProfile", b =>
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

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserProfile")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserProfile");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.UserRole", b =>
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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("UserRoles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d222b881-4310-47c5-8ca3-3ed5aebb3154"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Normal user",
                            Name = "Basic"
                        },
                        new
                        {
                            Id = new Guid("6326ea7f-b665-4919-a02e-853dbd584bbe"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Problem author. Has every right of the normal user and can create problems",
                            Name = "Author"
                        },
                        new
                        {
                            Id = new Guid("aba006b7-6262-44dc-8e2d-adf5a6a9b04c"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Has right to every action possible except those that are dangerous to system integrity",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("a9f7b329-c8a6-41d3-af2f-ac28ae185b0e"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Preferred not to use directly. Should be used as an authorization blocker to certain endpoints",
                            Name = "ARTSYS"
                        });
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.User", b =>
                {
                    b.HasOne("Artexitus.IdentityMicroservice.Domain.Entities.UserProfile", "Profile")
                        .WithOne("User")
                        .HasForeignKey("Artexitus.IdentityMicroservice.Domain.Entities.User", "ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.UserProfile", b =>
                {
                    b.HasOne("Artexitus.IdentityMicroservice.Domain.Entities.UserRole", "Role")
                        .WithMany("UserProfiles")
                        .HasForeignKey("UserProfile")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.UserProfile", b =>
                {
                    b.Navigation("User")
                        .IsRequired();
                });

            modelBuilder.Entity("Artexitus.IdentityMicroservice.Domain.Entities.UserRole", b =>
                {
                    b.Navigation("UserProfiles");
                });
#pragma warning restore 612, 618
        }
    }
}
