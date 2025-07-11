using AppMantenimiento.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SkiaSharp;

namespace AppMantenimiento.ControllersApi
{
    [Route("api/MaquinasCheckLists")]
    public class MaquinasCheckListController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MaquinasCheckListController(ApplicationDbContext context)
        {
            this.context = context;
        }



        [HttpGet]
        public async Task<IActionResult> GetCheckList()
        {
            var checkList = await context.Checklists.Select(c => new
            {
                c.Id,
                c.Nombre,
            }).ToListAsync(); ;

            if (checkList == null || !checkList.Any())
            {
                return NotFound();
            }
            return Ok(checkList);
        }

        [HttpGet("maquina/{maquinaId}")]
        public async Task<IActionResult> GetAsignados(int maquinaId)
        {
            var asignados = await context.MaquinaChecklists
                .Where(mc => mc.MaquinaId == maquinaId)
                .Select(mc => mc.ChecklistId)
                .ToListAsync();

            return Ok(asignados); // Lista de IDs
        }


        [HttpPost("maquina/{maquinaId}/asignar")]
        public async Task<IActionResult> AsignarChecklists(int maquinaId, [FromBody] List<int> checklistIds)
        {
            // Elimina los actuales
            var actuales = context.MaquinaChecklists
                .Where(mc => mc.MaquinaId == maquinaId);

            context.MaquinaChecklists.RemoveRange(actuales);

            // Agrega los nuevos
            foreach (var id in checklistIds)
            {
                context.MaquinaChecklists.Add(new MaquinaChecklist
                {
                    MaquinaId = maquinaId,
                    ChecklistId = id
                });
            }

            await context.SaveChangesAsync();

            return Ok(new { success = true });
        }



    }
}
