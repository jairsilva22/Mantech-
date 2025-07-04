using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AppMantenimiento.Models
{
    public class RegistroUsuarioViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

       
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Debes seleccionar un rol")]
        public string RolSeleccionado { get; set; }



      
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
    }
}
