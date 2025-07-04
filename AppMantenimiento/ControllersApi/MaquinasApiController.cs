using AppMantenimiento.Dtos;
using AppMantenimiento.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMantenimiento.ControllersApi
{
    [Route("api/checkList")]
    public class MaquinasApiController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public MaquinasApiController(ApplicationDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Checklist>>> Get()
        {
            return await context.Checklists.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Checklist>> Get(int id)
        {
            var checklist = await context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }
            return checklist;
        }
    

        [HttpPost]
         public async Task<ActionResult<Checklist>> Post([FromBody] CheckListDTO checkListDTO)
        {
            var nuevoCheckList = new Checklist();
            nuevoCheckList.Nombre = checkListDTO.Nombre;
            context.Checklists.Add(nuevoCheckList);
            await context.SaveChangesAsync();       
            return nuevoCheckList;
        }


        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] CheckListDTO checkListDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var checkList = await context.Checklists.FindAsync(id);
            if(checkList == null)
            {
                return NotFound();
            }

            checkList.Nombre = checkListDTO.Nombre;
            await context.SaveChangesAsync();
            return Ok();

        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var checkList = await context.Checklists.FindAsync(id);
            if (checkList == null)
            {
                return NotFound();
            }
            context.Checklists.Remove(checkList);
            await context.SaveChangesAsync();
            return NoContent();
        }


    }
}
