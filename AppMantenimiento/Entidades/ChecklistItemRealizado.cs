namespace AppMantenimiento.Entidades
{
    public class ChecklistItemRealizado
    {
        public int Id { get; set; }

        public int MantenimientoId { get; set; }
        public Mantenimiento Mantenimiento { get; set; }

        public int ItemChecklistId { get; set; }
        public ItemChecklist ItemChecklist { get; set; }

        public bool Realizado { get; set; }
        public string Observaciones { get; set; }
    }

}
