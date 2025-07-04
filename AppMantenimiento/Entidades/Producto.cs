namespace AppMantenimiento.Entidades
{
    public class Producto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Unidad { get; set; } // Ejemplo: litros, ml, piezas

        public ICollection<MantenimientoProducto> MantenimientosUsados { get; set; }
    }

}
