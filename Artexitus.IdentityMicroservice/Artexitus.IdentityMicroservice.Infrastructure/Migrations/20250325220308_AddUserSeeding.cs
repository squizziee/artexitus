using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_UserRoles_UserProfile",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_UserProfile",
                table: "UserProfiles");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("6326ea7f-b665-4919-a02e-853dbd584bbe"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9f7b329-c8a6-41d3-af2f-ac28ae185b0e"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("aba006b7-6262-44dc-8e2d-adf5a6a9b04c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("d222b881-4310-47c5-8ca3-3ed5aebb3154"));

            migrationBuilder.DropColumn(
                name: "UserProfile",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, "Has right to every action possible except those that are dangerous to system integrity", null, "Admin" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, "Preferred not to use directly. Should be used as an authorization blocker to certain endpoints", null, "ARTSYS" },
                    { new Guid("e04caec9-c13c-4991-9269-b8bebf2556a3"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("fcc47d49-27a2-4187-923f-07ae300a1367"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "UserProfiles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "LastUpdatedAt", "RoleId", "Username" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), new Guid("11111111-1111-1111-1111-111111111111"), "sirgideon" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), null, new DateTimeOffset(new DateTime(2025, 3, 25, 22, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(475), new TimeSpan(0, 0, 0, 0, 0)), new Guid("22222222-2222-2222-2222-222222222222"), "sys" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("beb64ea2-2827-4286-95be-291e71ba4e60"), null, new DateTimeOffset(new DateTime(2025, 3, 26, 1, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(691), new TimeSpan(0, 3, 0, 0, 0)), null, "admin0@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" },
                    { new Guid("ecafbe1f-5c66-46a6-93fa-f58ec056660a"), null, new DateTimeOffset(new DateTime(2025, 3, 26, 1, 3, 7, 163, DateTimeKind.Unspecified).AddTicks(739), new TimeSpan(0, 3, 0, 0, 0)), null, "sys@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_RoleId",
                table: "UserProfiles",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_UserRoles_RoleId",
                table: "UserProfiles",
                column: "RoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_UserRoles_RoleId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_RoleId",
                table: "UserProfiles");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("e04caec9-c13c-4991-9269-b8bebf2556a3"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("fcc47d49-27a2-4187-923f-07ae300a1367"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("beb64ea2-2827-4286-95be-291e71ba4e60"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ecafbe1f-5c66-46a6-93fa-f58ec056660a"));

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfile",
                table: "UserProfiles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("6326ea7f-b665-4919-a02e-853dbd584bbe"), new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("a9f7b329-c8a6-41d3-af2f-ac28ae185b0e"), new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)), null, "Preferred not to use directly. Should be used as an authorization blocker to certain endpoints", null, "ARTSYS" },
                    { new Guid("aba006b7-6262-44dc-8e2d-adf5a6a9b04c"), new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)), null, "Has right to every action possible except those that are dangerous to system integrity", null, "Admin" },
                    { new Guid("d222b881-4310-47c5-8ca3-3ed5aebb3154"), new DateTimeOffset(new DateTime(2025, 3, 24, 15, 36, 14, 684, DateTimeKind.Unspecified).AddTicks(3424), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserProfile",
                table: "UserProfiles",
                column: "UserProfile");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_UserRoles_UserProfile",
                table: "UserProfiles",
                column: "UserProfile",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
