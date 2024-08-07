using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfEntityFromBanckToBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanckAccounts_Bancks_BanckId",
                table: "BanckAccounts");

            migrationBuilder.DropTable(
                name: "Bancks");

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Banks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Active", "Cnpj", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"), true, "90400888000142", new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4646), "Santander", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"), true, "60746948000112", new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4644), "Bradesco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"), true, "60701190000104", new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4628), "Itau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Banks_UserId",
                table: "Banks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanckAccounts_Banks_BanckId",
                table: "BanckAccounts",
                column: "BanckId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanckAccounts_Banks_BanckId",
                table: "BanckAccounts");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.CreateTable(
                name: "Bancks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bancks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bancks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Bancks",
                columns: new[] { "Id", "Active", "Cnpj", "CreatedAt", "Name", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"), true, "90400888000142", new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2208), "Santander", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"), true, "60746948000112", new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2205), "Bradesco", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null },
                    { new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"), true, "60701190000104", new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2191), "Itau", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bancks_UserId",
                table: "Bancks",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanckAccounts_Bancks_BanckId",
                table: "BanckAccounts",
                column: "BanckId",
                principalTable: "Bancks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
