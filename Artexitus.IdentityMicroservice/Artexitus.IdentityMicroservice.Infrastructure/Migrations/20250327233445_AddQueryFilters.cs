using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddQueryFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("0dedb8f4-d8c7-4c90-86fc-84022d3450f7"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("9a0bccbf-62f8-4cbc-b591-cbd67b617300"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("14ffcecf-64be-4011-8cd2-a72735af245e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("172981d2-5e56-483d-ba32-969ad1bed452"));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("3a1a3eaf-c319-49d4-9a87-0eeca943c62c"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("5a48b26c-7564-45ff-8fd1-559ded8910f8"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9360), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("2d627231-67f3-42b4-9a0b-f1dda64df119"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9547), new TimeSpan(0, 3, 0, 0, 0)), null, "admin0@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" },
                    { new Guid("c1a25a35-0683-42fe-ba1a-2b6b9fe05d09"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 34, 45, 171, DateTimeKind.Unspecified).AddTicks(9582), new TimeSpan(0, 3, 0, 0, 0)), null, "sys@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("3a1a3eaf-c319-49d4-9a87-0eeca943c62c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("5a48b26c-7564-45ff-8fd1-559ded8910f8"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2d627231-67f3-42b4-9a0b-f1dda64df119"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c1a25a35-0683-42fe-ba1a-2b6b9fe05d09"));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0dedb8f4-d8c7-4c90-86fc-84022d3450f7"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("9a0bccbf-62f8-4cbc-b591-cbd67b617300"), new DateTimeOffset(new DateTime(2025, 3, 27, 23, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(3845), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("14ffcecf-64be-4011-8cd2-a72735af245e"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(4092), new TimeSpan(0, 3, 0, 0, 0)), null, "sys@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" },
                    { new Guid("172981d2-5e56-483d-ba32-969ad1bed452"), null, new DateTimeOffset(new DateTime(2025, 3, 28, 2, 18, 9, 837, DateTimeKind.Unspecified).AddTicks(4061), new TimeSpan(0, 3, 0, 0, 0)), null, "admin0@artexitus.com", true, null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" }
                });
        }
    }
}
