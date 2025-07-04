using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace AppMantenimiento.Servicios
{

    public interface IQrGeneratorService
    {
        byte[] GenerarQr(string texto);
        void GuardarQrComoImagen(string texto, string rutaArchivo);
    }
    public class QrGeneratorService : IQrGeneratorService
    {
        public byte[] GenerarQr(string texto)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(texto, QRCodeGenerator.ECCLevel.Q);

            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            return qrCode.GetGraphic(20);
        }

        public void GuardarQrComoImagen(string texto, string rutaArchivo)
        {
            var imagenBytes = GenerarQr(texto);
            File.WriteAllBytes(rutaArchivo, imagenBytes);
        }
    }
}
