using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRolesUsuarioModerador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Crear rol Usuario Regular
            migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT Id FROM AspNetRoles WHERE Id = '2c3195bc-0239-50c2-9162-7074fb148c67')
                BEGIN 
                    INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES ('2c3195bc-0239-50c2-9162-7074fb148c67', 'Usuario', 'USUARIO', NEWID())
                END");

                    // Crear rol Moderador (opcional)
                    migrationBuilder.Sql(@"
                IF NOT EXISTS(SELECT Id FROM AspNetRoles WHERE Id = '3d4296cd-0349-61d3-a273-8185gc259d78')
                BEGIN 
                    INSERT INTO AspNetRoles(Id, Name, NormalizedName, ConcurrencyStamp) 
                    VALUES ('3d4296cd-0349-61d3-a273-8185gc259d78', 'Moderador', 'MODERADOR', NEWID())
                END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id IN ('1b2085ab-0129-40b1-8051-6963ea037b56', '2c3195bc-0239-50c2-9162-7074fb148c67', '3d4296cd-0349-61d3-a273-8185gc259d78')");


        }
    }
}
