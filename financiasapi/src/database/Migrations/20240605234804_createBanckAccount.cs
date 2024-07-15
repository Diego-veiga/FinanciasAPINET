using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class createBanckAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BanckAccount",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BanckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanckAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanckAccount_Bancks_BanckId",
                        column: x => x.BanckId,
                        principalTable: "Bancks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBancksAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BanckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BanckAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBancksAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBancksAccounts_BanckAccount_BanckAccountId",
                        column: x => x.BanckAccountId,
                        principalTable: "BanckAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserBancksAccounts_Bancks_BanckId",
                        column: x => x.BanckId,
                        principalTable: "Bancks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBancksAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 20, 48, 4, 545, DateTimeKind.Local).AddTicks(369));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 20, 48, 4, 545, DateTimeKind.Local).AddTicks(368));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 20, 48, 4, 545, DateTimeKind.Local).AddTicks(355));

            migrationBuilder.CreateIndex(
                name: "IX_BanckAccount_BanckId",
                table: "BanckAccount",
                column: "BanckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBancksAccounts_BanckAccountId",
                table: "UserBancksAccounts",
                column: "BanckAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBancksAccounts_BanckId",
                table: "UserBancksAccounts",
                column: "BanckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBancksAccounts_UserId",
                table: "UserBancksAccounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBancksAccounts");

            migrationBuilder.DropTable(
                name: "BanckAccount");

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8057));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8055));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 3, 12, 11, 36, 41, 330, DateTimeKind.Local).AddTicks(8043));
        }
    }
}
