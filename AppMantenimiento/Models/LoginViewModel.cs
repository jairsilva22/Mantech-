using System.ComponentModel.DataAnnotations;

namespace AppMantenimiento.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser un correo electrónico válido.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [DataType(DataType.Password)]
        public string Password { get; set; } 

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; } = false;
    }
}
