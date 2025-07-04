namespace AppMantenimiento.Entidades
{
    public class MantenimientoProducto
    {
        public int Id { get; set; }

        public int MantenimientoId { get; set; }
        public Mantenimiento Mantenimiento { get; set; }

        public int ProductoId { get; set; }
        public Producto Producto { get; set; }

        public decimal Cantidad { get; set; }
    }

}
