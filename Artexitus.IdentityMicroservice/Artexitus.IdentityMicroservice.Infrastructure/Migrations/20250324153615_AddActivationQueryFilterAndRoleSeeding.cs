using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Artexitus.IdentityMicroservice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddActivationQueryFilterAndRoleSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
