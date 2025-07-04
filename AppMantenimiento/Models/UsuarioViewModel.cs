using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppMantenimiento.Models
{
    public class UsuarioViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

   
        public string PhoneNumber { get; set; }

        public string  Rol { get; set; }


        public List<SelectListItem>  Roles { get; set; }
    }
}
