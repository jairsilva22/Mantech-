using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AddCamposMaquina : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodigoInterno",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notas",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Maquinas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodigoInterno",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "Notas",
                table: "Maquinas");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Maquinas");
        }
    }
}
