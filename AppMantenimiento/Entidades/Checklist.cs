namespace AppMantenimiento.Entidades
{
    public class Checklist
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        public ICollection<ItemChecklist> Items { get; set; }
        public ICollection<MaquinaChecklist> MaquinaChecklists { get; set; }
    }

}
