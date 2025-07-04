using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppMantenimiento
{
    public class Constantes
    {
        public const string RolAdministrador = "admin";
        public const string RolUsuario = "Usuario";
        public const string RolModerador = "Moderador";


        public static readonly SelectListItem[] CulturasUISoportadas = new SelectListItem[]
       {
            new SelectListItem { Value = "es", Text = "Español" },
            new SelectListItem { Value = "en", Text = "English" },

       };
    }
}
