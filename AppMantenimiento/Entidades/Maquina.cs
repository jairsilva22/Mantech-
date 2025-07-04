namespace AppMantenimiento.Entidades
{
    public class Maquina
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string CodigoQR { get; set; }

        public string QrImagePath { get; set; }

        public string Ubicacion { get; set; }
        public string CodigoInterno { get; set; }
        public string Notas { get; set; }
        public ICollection<MaquinaChecklist> MaquinaChecklists { get; set; }
        public ICollection<Mantenimiento> Mantenimientos { get; set; }

        public ICollection<ChecklistItemRealizado> ChecklistItemsRealizados { get; set; }

       
    }

}
