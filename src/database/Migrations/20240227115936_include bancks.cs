using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class includebancks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bancks",
                columns: new[] { "Id", "Active", "Cnpj", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"), true, "90400888000142", new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9855), "Santander", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"), true, "60746948000112", new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9852), "Bradesco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"), true, "60701190000104", new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9841), "Itau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"));

            migrationBuilder.DeleteData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"));

            migrationBuilder.DeleteData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"));
        }
    }
}
