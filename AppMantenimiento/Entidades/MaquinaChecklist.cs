namespace AppMantenimiento.Entidades
{
    public class MaquinaChecklist
    {
        public int MaquinaId { get; set; }
        public Maquina Maquina { get; set; }

        public int ChecklistId { get; set; }
        public Checklist Checklist { get; set; }
    }

}
