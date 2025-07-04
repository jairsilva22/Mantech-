using System.Globalization;

namespace AppMantenimiento.Models
{
    public class MaquinaViewModel
    {

        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Qr { get; set; }
        public string QrImagePath { get; set; }
        public string Ubicacion { get; set; }
        public string CodigoInterno { get; set; }
        public string Notas { get; set; }

    }
}
