using AppMantenimiento.Entidades;
using AppMantenimiento.Models;
using AppMantenimiento.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace AppMantenimiento.Controllers
{
    public class MaquinasController : Controller
    {
        private readonly IMaquinasRepositorio maquinasRepositorio;
        private readonly IQrGeneratorService qrGeneratorService;

        public MaquinasController(IMaquinasRepositorio maquinasRepositorio, IQrGeneratorService qrGeneratorService)
        {
            this.maquinasRepositorio = maquinasRepositorio;
            this.qrGeneratorService = qrGeneratorService;
        }
        public IActionResult Agregar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Agregar(MaquinaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Aquí puedes generar el código QR y guardarlo en el modelo


            await maquinasRepositorio.AddMaquinaAsync(model);

            return RedirectToAction("Index"); // O adonde quieras redirigir
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var maquina = await maquinasRepositorio.GetAllMaquinasAsync();
            if (maquina == null)
            {
                return NotFound();
            }
            return View(maquina);

        }



        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var dto = await maquinasRepositorio.ObtenerParaEdicion(id);
            if (dto == null)
            {
                return NotFound();
            }
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EditarMaquina dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            try
            {
                var resultado = await maquinasRepositorio.Editar(dto);
                if (resultado == null)
                {
                    return NotFound();
                }

                // Redirigir después de editar exitosamente
                return RedirectToAction("Index"); // o donde quieras redirigir
            }
            catch (Exception ex)
            {
                // Manejar errores
                ModelState.AddModelError("", "Error al editar la máquina: " + ex.Message);
                return View(dto);
            }
        }



        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var maquina = await maquinasRepositorio.GetMaquinaByIdAsync(id);
            if (maquina == null)
            {
                return NotFound();
            }
            // Aquí puedes pasar el modelo a la vista
            var model = new MaquinaViewModel
            {
                Id = maquina.Id,
                Nombre = maquina.Nombre,           
            };
            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmarEliminacion(int id)
        {
            var resultado = await maquinasRepositorio.Eliminar(id);
            if (!resultado)
            {
                return NotFound();
            }

            return RedirectToAction("Index");
        }





    }
}
