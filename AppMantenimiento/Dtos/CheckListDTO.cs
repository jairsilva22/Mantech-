using System.ComponentModel.DataAnnotations;

namespace AppMantenimiento.Dtos
{
    public class CheckListDTO
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }
    }
}
