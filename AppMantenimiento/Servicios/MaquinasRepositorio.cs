using AppMantenimiento.Entidades;
using AppMantenimiento.Models;
using Microsoft.EntityFrameworkCore;

namespace AppMantenimiento.Servicios
{

    public interface IMaquinasRepositorio
    {
        Task AddMaquinaAsync(MaquinaViewModel model);


        Task<MaquinaViewModel> Editar(EditarMaquina dto);
        Task<bool> Eliminar(int id);
        Task<MaquinaListadoViewModel> GetAllMaquinasAsync();
        Task<Maquina> GetMaquinaByIdAsync(int id);
        Task<EditarMaquina> ObtenerParaEdicion(int id);
    }
    public class MaquinasRepositorio : IMaquinasRepositorio
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IQrGeneratorService qrGeneratorService;

        public MaquinasRepositorio(ApplicationDbContext applicationDbContext, IQrGeneratorService qrGeneratorService)
        {
            this.applicationDbContext = applicationDbContext;
            this.qrGeneratorService = qrGeneratorService;
        }


        public async Task AddMaquinaAsync(MaquinaViewModel model)
        {
            // 1. Crear máquina sin QR
            var maquina = new Maquina
            {
                Nombre = model.Nombre,
                Ubicacion = model.Ubicacion,
                CodigoInterno = model.CodigoInterno,
                Notas = model.Notas
            };

            applicationDbContext.Maquinas.Add(maquina);
            await applicationDbContext.SaveChangesAsync(); // Aquí se genera el ID

            // 2. Generar la URL para el QR
            var url = $"https://tusitio.com/Maquinas/Detalle/{maquina.Id}"; // o localhost en pruebas

            // 3. Generar QR

            var qrBytes = qrGeneratorService.GenerarQr(url);

            // 4. Guardar imagen del QR en carpeta
            var nombreArchivo = $"qr_maquina_{maquina.Id}.png";
            var rutaCarpeta = Path.Combine("wwwroot", "qrcodes"); // crea esta carpeta si no existe
            Directory.CreateDirectory(rutaCarpeta);
            var rutaCompleta = Path.Combine(rutaCarpeta, nombreArchivo);
            await File.WriteAllBytesAsync(rutaCompleta, qrBytes);

            // 5. Actualizar la máquina con la ruta del QR
            maquina.CodigoQR = url;
            maquina.QrImagePath = $"/qrcodes/{nombreArchivo}"; // Ruta  para mostrar en la vista dd

            applicationDbContext.Maquinas.Update(maquina);
            await applicationDbContext.SaveChangesAsync();
        }


        public async Task<Maquina> GetMaquinaByIdAsync(int id)
        {
            return await applicationDbContext.Maquinas.FindAsync(id);
        }

        public async Task<MaquinaListadoViewModel> GetAllMaquinasAsync()
        {
            var maquinas = await applicationDbContext.Maquinas
                .Select(m => new MaquinaViewModel
                {
                    Id = m.Id,
                    Nombre = m.Nombre,
                    Ubicacion = m.Ubicacion,
                    CodigoInterno = m.CodigoInterno,
                    Notas = m.Notas,
                    Qr = m.CodigoQR,
                    QrImagePath = m.QrImagePath
                })
                .ToListAsync();

            return new MaquinaListadoViewModel
            {
                maquinas = maquinas,
                mensaje = ""
            };


        }


        public async Task<EditarMaquina> ObtenerParaEdicion(int id)
        {
            var maquina = await applicationDbContext.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return null;
            }

            return new EditarMaquina
            {
                Id = maquina.Id,
                Nombre = maquina.Nombre,
                Ubicacion = maquina.Ubicacion,
                CodigoInterno = maquina.CodigoInterno,
                Notas = maquina.Notas
            };
        }

        public async Task<MaquinaViewModel> Editar(EditarMaquina dto)
        {
            var maquinaExistente = await applicationDbContext.Maquinas.FindAsync(dto.Id);
            if (maquinaExistente == null)
            {
                return null;
            }

            // Actualizar solo los campos editables
            maquinaExistente.Nombre = dto.Nombre;
            maquinaExistente.Ubicacion = dto.Ubicacion;
            maquinaExistente.CodigoInterno = dto.CodigoInterno;
            maquinaExistente.Notas = dto.Notas;

            await applicationDbContext.SaveChangesAsync();

            return new MaquinaViewModel
            {
                Id = maquinaExistente.Id,
                Nombre = maquinaExistente.Nombre,
                Ubicacion = maquinaExistente.Ubicacion,
                CodigoInterno = maquinaExistente.CodigoInterno,
                Notas = maquinaExistente.Notas,
                Qr = maquinaExistente.CodigoQR,
                QrImagePath = maquinaExistente.QrImagePath
            };
        }




        public async Task<bool> Eliminar(int id)
        {
            var maquina = await applicationDbContext.Maquinas.FindAsync(id);
            if (maquina == null)
            {
                return false;
            }

            applicationDbContext.Maquinas.Remove(maquina);
            await applicationDbContext.SaveChangesAsync();
            return true;
        }


    }
}
