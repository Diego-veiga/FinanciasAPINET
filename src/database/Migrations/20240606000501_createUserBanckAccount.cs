using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class createUserBanckAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanckAccount_Bancks_BanckId",
                table: "BanckAccount");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBancksAccounts_BanckAccount_BanckAccountId",
                table: "UserBancksAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBancksAccounts_Bancks_BanckId",
                table: "UserBancksAccounts");

            migrationBuilder.DropIndex(
                name: "IX_UserBancksAccounts_BanckId",
                table: "UserBancksAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BanckAccount",
                table: "BanckAccount");

            migrationBuilder.DropColumn(
                name: "BanckId",
                table: "UserBancksAccounts");

            migrationBuilder.RenameTable(
                name: "BanckAccount",
                newName: "BanckAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_BanckAccount_BanckId",
                table: "BanckAccounts",
                newName: "IX_BanckAccounts_BanckId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BanckAccountId",
                table: "UserBancksAccounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "UserBancksAccounts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "BanckAccounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanckAccounts",
                table: "BanckAccounts",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("16d9075f-2ced-4358-be10-4cfeefac8647"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2208));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("81a3902a-89b7-459e-a665-2a346ae829e1"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2205));

            migrationBuilder.UpdateData(
                table: "Bancks",
                keyColumn: "Id",
                keyValue: new Guid("aff79997-841c-4cb4-a4e6-84d4f0e00e8d"),
                column: "CreatedAt",
                value: new DateTime(2024, 6, 5, 21, 5, 0, 844, DateTimeKind.Local).AddTicks(2191));

            migrationBuilder.AddForeignKey(
                name: "FK_BanckAccounts_Bancks_BanckId",
                table: "BanckAccounts",
                column: "BanckId",
                principalTable: "Bancks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBancksAccounts_BanckAccounts_BanckAccountId",
                table: "UserBancksAccounts",
                column: "BanckAccountId",
                principalTable: "BanckAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BanckAccounts_Bancks_BanckId",
                table: "BanckAccounts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserBancksAccounts_BanckAccounts_BanckAccountId",
                table: "UserBancksAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BanckAccounts",
                table: "BanckAccounts");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "UserBancksAccounts");

            migrationBuilder.RenameTable(
                name: "BanckAccounts",
                newName: "BanckAccount");

            migrationBuilder.RenameIndex(
                name: "IX_BanckAccounts_BanckId",
                table: "BanckAccount",
                newName: "IX_BanckAccount_BanckId");

            migrationBuilder.AlterColumn<Guid>(
                name: "BanckAccountId",
                table: "UserBancksAccounts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BanckId",
                table: "UserBancksAccounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<double>(
                name: "Balance",
                table: "BanckAccount",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BanckAccount",
                table: "BanckAccount",
                column: "Id");

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
                name: "IX_UserBancksAccounts_BanckId",
                table: "UserBancksAccounts",
                column: "BanckId");

            migrationBuilder.AddForeignKey(
                name: "FK_BanckAccount_Bancks_BanckId",
                table: "BanckAccount",
                column: "BanckId",
                principalTable: "Bancks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserBancksAccounts_BanckAccount_BanckAccountId",
                table: "UserBancksAccounts",
                column: "BanckAccountId",
                principalTable: "BanckAccount",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserBancksAccounts_Bancks_BanckId",
                table: "UserBancksAccounts",
                column: "BanckId",
                principalTable: "Bancks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
