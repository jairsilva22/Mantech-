using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class AdminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("IF NOT EXISTS(SELECT ID FROM AspNetRoles WHERE Id= '1b2085ab-0129-40b1-8051-6963ea037b56')" +
                                 "BEGIN Insert into AspNetRoles(Id,[Name],[NormalizedName])" +
                                 " values ('1b2085ab-0129-40b1-8051-6963ea037b56','admin','ADMIN') END");


       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM AspNetRoles WHERE Id = '1b2085ab-0129-40b1-8051-6963ea037b56'");
        }
    }
}
