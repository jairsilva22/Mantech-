namespace AppMantenimiento.Entidades
{
    public class ItemChecklist
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int ChecklistId { get; set; }

        public Checklist Checklist { get; set; }
        public ICollection<ChecklistItemRealizado> ChecklistItemsRealizados { get; set; }
    }

}
