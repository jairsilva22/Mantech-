using System.Security.Policy;

namespace AppMantenimiento.Entidades
{
    public class Mantenimiento
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string Observaciones { get; set; }
        public int MaquinaId { get; set; }

        public Maquina Maquina { get; set; }
        public ICollection<ChecklistItemRealizado> ChecklistItemsRealizados { get; set; }
        public ICollection<MantenimientoProducto> ProductosUsados { get; set; }
        public ICollection<Evidencia> Evidencias { get; set; }
    }

}
