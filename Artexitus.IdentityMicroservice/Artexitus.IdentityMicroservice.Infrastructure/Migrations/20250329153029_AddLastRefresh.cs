using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddLastRefresh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastRefresh",
                table: "Users",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("50424413-0fa9-4656-9534-56dff6557f7c"), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("db44b36c-071b-4f1b-9dcc-ae3e57d2ca42"), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastRefresh", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("40d9ae85-2029-43e2-9d98-f035f29a0ad7"), null, new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), null, "admin0@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" },
                    { new Guid("dca3d185-b0b0-468d-b513-6e56217e9243"), null, new DateTimeOffset(new DateTime(2025, 3, 29, 15, 30, 27, 917, DateTimeKind.Unspecified).AddTicks(1798), new TimeSpan(0, 0, 0, 0, 0)), null, "sys@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("50424413-0fa9-4656-9534-56dff6557f7c"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("db44b36c-071b-4f1b-9dcc-ae3e57d2ca42"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("40d9ae85-2029-43e2-9d98-f035f29a0ad7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("dca3d185-b0b0-468d-b513-6e56217e9243"));

            migrationBuilder.DropColumn(
                name: "LastRefresh",
                table: "Users");

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
    }
}
