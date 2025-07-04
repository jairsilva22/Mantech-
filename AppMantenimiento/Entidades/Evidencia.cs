namespace AppMantenimiento.Entidades
{
    public class Evidencia
    {
        public int Id { get; set; }
        public string UrlFoto { get; set; }

        public int MantenimientoId { get; set; }
        public Mantenimiento Mantenimiento { get; set; }
    }

}
