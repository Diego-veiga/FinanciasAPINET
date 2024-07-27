using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class SpellingCorrectionBankAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBancksAccounts");

            migrationBuilder.DropTable(
                name: "BanckAccounts");

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Type = table.Column<int>(type: "int", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBanksAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBanksAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBanksAccounts_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBanksAccounts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 27, 14, 22, 56, 366, DateTimeKind.Local).AddTicks(5800));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 27, 14, 22, 56, 366, DateTimeKind.Local).AddTicks(5799));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 27, 14, 22, 56, 366, DateTimeKind.Local).AddTicks(5787));

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankId",
                table: "BankAccounts",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBanksAccounts_BankAccountId",
                table: "UserBanksAccounts",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBanksAccounts_UserId",
                table: "UserBanksAccounts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBanksAccounts");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.CreateTable(
                name: "BanckAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BanckId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Balance = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BanckAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BanckAccounts_Banks_BanckId",
                        column: x => x.BanckId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserBancksAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BanckAccountId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBancksAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserBancksAccounts_BanckAccounts_BanckAccountId",
                        column: x => x.BanckAccountId,
                        principalTable: "BanckAccounts",
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
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4646));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4644));

            migrationBuilder.UpdateData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 7, 23, 22, 36, 36, 714, DateTimeKind.Local).AddTicks(4628));

            migrationBuilder.CreateIndex(
                name: "IX_BanckAccounts_BanckId",
                table: "BanckAccounts",
                column: "BanckId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBancksAccounts_BanckAccountId",
                table: "UserBancksAccounts",
                column: "BanckAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserBancksAccounts_UserId",
                table: "UserBancksAccounts",
                column: "UserId");
        }
    }
}
