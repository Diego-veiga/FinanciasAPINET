using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace financias.database.Migrations
{
    /// <inheritdoc />
    public partial class AlterFielActive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "Users",
                newName: "Active");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Active",
                table: "Users",
                newName: "Ativo");
        }
    }
}
