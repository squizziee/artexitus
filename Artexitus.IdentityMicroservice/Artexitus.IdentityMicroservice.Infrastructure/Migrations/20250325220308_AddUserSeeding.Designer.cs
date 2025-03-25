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
    [Migration("20250325220308_AddUserSeeding")]
    partial class AddUserSeeding
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("beb64ea2-2827-4286-95be-291e71ba4e60"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 26, 1, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "admin0@artexitus.com",
                            IsActivated = true,
                            PasswordHash = "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y",
                            ProfileId = new Guid("11111111-1111-1111-1111-111111111111"),
                            RefreshToken = "0000"
                        },
                        new
                        {
                            Id = new Guid("ecafbe1f-5c66-46a6-93fa-f58ec056660a"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 26, 1, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(739), new TimeSpan(0, 3, 0, 0, 0)),
                            Email = "sys@artexitus.com",
                            IsActivated = true,
                            PasswordHash = "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y",
                            ProfileId = new Guid("22222222-2222-2222-2222-222222222222"),
                            RefreshToken = "0000"
                        });
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

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("UserProfiles");

                    b.HasData(
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            LastUpdatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            RoleId = new Guid("11111111-1111-1111-1111-111111111111"),
                            Username = "sirgideon"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            LastUpdatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            RoleId = new Guid("22222222-2222-2222-2222-222222222222"),
                            Username = "sys"
                        });
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
                            Id = new Guid("fcc47d49-27a2-4187-923f-07ae300a1367"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Normal user",
                            Name = "Basic"
                        },
                        new
                        {
                            Id = new Guid("e04caec9-c13c-4991-9269-b8bebf2556a3"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Problem author. Has every right of the normal user and can create problems",
                            Name = "Author"
                        },
                        new
                        {
                            Id = new Guid("11111111-1111-1111-1111-111111111111"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
                            Description = "Has right to every action possible except those that are dangerous to system integrity",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = new Guid("22222222-2222-2222-2222-222222222222"),
                            CreatedAt = new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)),
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
                        .HasForeignKey("RoleId")
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
