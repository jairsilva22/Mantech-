using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AddCampQr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "QrImagePath",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QrImagePath",
                table: "Maquinas");
        }
    }
}
