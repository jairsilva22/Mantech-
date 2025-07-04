using AppMantenimiento.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppMantenimiento
{
    public class ApplicationDbContext : IdentityDbContext
    {


        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Maquina> Maquinas { get; set; }
        public DbSet<Checklist> Checklists { get; set; }
        public DbSet<ItemChecklist> ItemsChecklist { get; set; }
        public DbSet<MaquinaChecklist> MaquinaChecklists { get; set; }
        public DbSet<Mantenimiento> Mantenimientos { get; set; }
        public DbSet<ChecklistItemRealizado> ChecklistItemsRealizados { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<MantenimientoProducto> MantenimientoProductos { get; set; }
        public DbSet<Evidencia> Evidencias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // MaquinaChecklist: clave compuesta y relaciones muchos a muchos
            modelBuilder.Entity<MaquinaChecklist>()
                .HasKey(mc => new { mc.MaquinaId, mc.ChecklistId });

            modelBuilder.Entity<MaquinaChecklist>()
                .HasOne(mc => mc.Maquina)
                .WithMany(m => m.MaquinaChecklists)
                .HasForeignKey(mc => mc.MaquinaId);

            modelBuilder.Entity<MaquinaChecklist>()
                .HasOne(mc => mc.Checklist)
                .WithMany(c => c.MaquinaChecklists)
                .HasForeignKey(mc => mc.ChecklistId);

            // ItemChecklist: relación con Checklist
            modelBuilder.Entity<ItemChecklist>()
                .HasOne(i => i.Checklist)
                .WithMany(c => c.Items)
                .HasForeignKey(i => i.ChecklistId);

            // ChecklistItemRealizado: relaciones con Mantenimiento e ItemChecklist
            modelBuilder.Entity<ChecklistItemRealizado>()
                .HasOne(cir => cir.Mantenimiento)
                .WithMany(m => m.ChecklistItemsRealizados)
                .HasForeignKey(cir => cir.MantenimientoId);

            modelBuilder.Entity<ChecklistItemRealizado>()
                .HasOne(cir => cir.ItemChecklist)
                .WithMany(i => i.ChecklistItemsRealizados)
                .HasForeignKey(cir => cir.ItemChecklistId);

            // MantenimientoProducto: relaciones con Mantenimiento y Producto
            modelBuilder.Entity<MantenimientoProducto>()
                .HasOne(mp => mp.Mantenimiento)
                .WithMany(m => m.ProductosUsados)
                .HasForeignKey(mp => mp.MantenimientoId);

            modelBuilder.Entity<MantenimientoProducto>()
                .HasOne(mp => mp.Producto)
                .WithMany(p => p.MantenimientosUsados)
                .HasForeignKey(mp => mp.ProductoId);

            // Evidencia: relación con Mantenimiento
            modelBuilder.Entity<Evidencia>()
                .HasOne(e => e.Mantenimiento)
                .WithMany(m => m.Evidencias)
                .HasForeignKey(e => e.MantenimientoId);

            // Maquina - Mantenimientos relación uno a muchos
            modelBuilder.Entity<Mantenimiento>()
                .HasOne(m => m.Maquina)
                .WithMany(ma => ma.Mantenimientos)
                .HasForeignKey(m => m.MaquinaId);
        }



    }
}
