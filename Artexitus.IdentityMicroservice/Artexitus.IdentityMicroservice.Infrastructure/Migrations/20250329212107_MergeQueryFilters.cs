using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MergeQueryFilters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("0a5e8040-e6e7-47e2-955a-47e28f4430a7"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("75d9396b-2ca8-4ed1-b9ad-1ab75343dd8c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6fa9cd2a-89e0-47e2-8ba2-4672962bbdd6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cde8357f-9186-4f68-a6fd-4089f94943b5"));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("acb1753b-9681-4411-aabf-5338d2c17a3e"), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("d99fa601-7fa5-494a-a5c4-dd74c34edf50"), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "ActivationTokenValidTo", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastRefresh", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("2fac5936-2b85-4274-99cc-3b39a0cf77ad"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), null, "sys@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" },
                    { new Guid("e15f59f4-5cec-4031-a77c-ed9653974d5a"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 21, 21, 6, 27, DateTimeKind.Unspecified).AddTicks(2867), new TimeSpan(0, 0, 0, 0, 0)), null, "admin0@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("acb1753b-9681-4411-aabf-5338d2c17a3e"));

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("d99fa601-7fa5-494a-a5c4-dd74c34edf50"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2fac5936-2b85-4274-99cc-3b39a0cf77ad"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e15f59f4-5cec-4031-a77c-ed9653974d5a"));

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserProfiles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "CreatedAt", "LastUpdatedAt" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "CreatedAt",
                value: new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "LastUpdatedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("0a5e8040-e6e7-47e2-955a-47e28f4430a7"), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), null, "Problem author. Has every right of the normal user and can create problems", null, "Author" },
                    { new Guid("75d9396b-2ca8-4ed1-b9ad-1ab75343dd8c"), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), null, "Normal user", null, "Basic" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActivationToken", "ActivationTokenValidTo", "CreatedAt", "DeletedAt", "Email", "IsActivated", "LastRefresh", "LastUpdatedAt", "PasswordHash", "ProfileId", "RefreshToken" },
                values: new object[,]
                {
                    { new Guid("6fa9cd2a-89e0-47e2-8ba2-4672962bbdd6"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), null, "sys@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("22222222-2222-2222-2222-222222222222"), "0000" },
                    { new Guid("cde8357f-9186-4f68-a6fd-4089f94943b5"), null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 29, 15, 57, 39, 697, DateTimeKind.Unspecified).AddTicks(9390), new TimeSpan(0, 0, 0, 0, 0)), null, "admin0@artexitus.com", true, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "$2a$11$WnauqdqfU6OCCum52F2fUO/X9UwEZlv5Nc7zOf66MfPHHbutyqI7y", new Guid("11111111-1111-1111-1111-111111111111"), "0000" }
                });
        }
    }
}
