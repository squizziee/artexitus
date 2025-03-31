using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewColossus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 7000, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "UserRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastUpdatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    DeletedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_UserProfiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0dedb8f4-d8c7-4c90-86fc-84022d3450f7"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Has right to every action possible except those that are dangerous to system integrity", null, "Admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Preferred not to use directly. Should be used as an authorization blocker to certain endpoints", null, "ARTSYS" },
                    { new Guid("9a0bccbf-62f8-4cbc-b591-cbd67b617300"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "LastUpdatedAt", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), new Guid("11111111-1111-1111-1111-111111111111"), "sirgideon" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22222222-2222-2222-2222-222222222222"), "sys" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("14ffcecf-64be-4011-8cd2-a72735af245e"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(4092), new TimeSpan(0, 3, 0, 0, 0)), null, "sys@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" },
                    { new Guid("172981d2-5e56-483d-ba32-969ad1bed452"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(4061), new TimeSpan(0, 3, 0, 0, 0)), null, "admin0@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_RoleId",
                table: "UserProfiles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_Username",
                table: "UserProfiles",
                column: "Username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_Name",
                table: "UserRoles",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ProfileId",
                table: "Users",
                column: "ProfileId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "UserRoles");
        }
    }
}
