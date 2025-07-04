using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppMantenimiento.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checklists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checklists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maquinas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CodigoQR = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Unidad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemsChecklist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemsChecklist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemsChecklist_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mantenimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaquinaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mantenimientos_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MaquinaChecklists",
                columns: table => new
                {
                    MaquinaId = table.Column<int>(type: "int", nullable: false),
                    ChecklistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinaChecklists", x => new { x.MaquinaId, x.ChecklistId });
                    table.ForeignKey(
                        name: "FK_MaquinaChecklists_Checklists_ChecklistId",
                        column: x => x.ChecklistId,
                        principalTable: "Checklists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaquinaChecklists_Maquinas_MaquinaId",
                        column: x => x.MaquinaId,
                        principalTable: "Maquinas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChecklistItemsRealizados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MantenimientoId = table.Column<int>(type: "int", nullable: false),
                    ItemChecklistId = table.Column<int>(type: "int", nullable: false),
                    Realizado = table.Column<bool>(type: "bit", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChecklistItemsRealizados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChecklistItemsRealizados_ItemsChecklist_ItemChecklistId",
                        column: x => x.ItemChecklistId,
                        principalTable: "ItemsChecklist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChecklistItemsRealizados_Mantenimientos_MantenimientoId",
                        column: x => x.MantenimientoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Evidencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MantenimientoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evidencias_Mantenimientos_MantenimientoId",
                        column: x => x.MantenimientoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MantenimientoProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MantenimientoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MantenimientoProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MantenimientoProductos_Mantenimientos_MantenimientoId",
                        column: x => x.MantenimientoId,
                        principalTable: "Mantenimientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MantenimientoProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemsRealizados_ItemChecklistId",
                table: "ChecklistItemsRealizados",
                column: "ItemChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_ChecklistItemsRealizados_MantenimientoId",
                table: "ChecklistItemsRealizados",
                column: "MantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evidencias_MantenimientoId",
                table: "Evidencias",
                column: "MantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemsChecklist_ChecklistId",
                table: "ItemsChecklist",
                column: "ChecklistId");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoProductos_MantenimientoId",
                table: "MantenimientoProductos",
                column: "MantenimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MantenimientoProductos_ProductoId",
                table: "MantenimientoProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Mantenimientos_MaquinaId",
                table: "Mantenimientos",
                column: "MaquinaId");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinaChecklists_ChecklistId",
                table: "MaquinaChecklists",
                column: "ChecklistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChecklistItemsRealizados");

            migrationBuilder.DropTable(
                name: "Evidencias");

            migrationBuilder.DropTable(
                name: "MantenimientoProductos");

            migrationBuilder.DropTable(
                name: "MaquinaChecklists");

            migrationBuilder.DropTable(
                name: "ItemsChecklist");

            migrationBuilder.DropTable(
                name: "Mantenimientos");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Checklists");

            migrationBuilder.DropTable(
                name: "Maquinas");
        }
    }
}
