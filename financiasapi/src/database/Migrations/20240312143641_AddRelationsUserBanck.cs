using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationsUserBanck : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Bancks",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8057), null });

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8055), null });

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                columns: new[] { "CreatedAt", "UserId" },
                values: new object[] { new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8043), null });

            migrationBuilder.CreateIndex(
                name: "IX_Bancks_UserId",
                table: "Bancks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bancks_Users_UserId",
                table: "Bancks",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bancks_Users_UserId",
                table: "Bancks");

            migrationBuilder.DropIndex(
                name: "IX_Bancks_UserId",
                table: "Bancks");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bancks");

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9855));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9852));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 2, 27, 8, 59, 36, 719, DateTimeKind.Local).AddTicks(9841));
        }
    }
}
