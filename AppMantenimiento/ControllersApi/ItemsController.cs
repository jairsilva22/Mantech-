using AppMantenimiento.Dtos;
using AppMantenimiento.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppMantenimiento.ControllersApi
{

    [Route("api/Items")]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public ItemsController(ApplicationDbContext context)
        {
            this.context = context;
        }



        [HttpPost]
        public async Task<ActionResult<ItemChecklist>> Post([FromBody] ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var nuevoItem = new ItemChecklist
            {
                ChecklistId = itemDTO.CheckListId,
                Descripcion = itemDTO.Descripcion
            };
            context.ItemsChecklist.Add(nuevoItem);
            await context.SaveChangesAsync();
            return nuevoItem;
        }


        [HttpGet("{checklistId:int}")]
        public async Task<ActionResult<List<ItemChecklist>>> Get(int checklistId)
        {
            var items = await context.ItemsChecklist
                .Where(x => x.ChecklistId == checklistId)
                .ToListAsync();
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }
            return items;
        }



        [HttpPut("{id:int}")]   
        public async Task<ActionResult<ItemChecklist>> Put(int id, [FromBody] ItemDTO itemDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var item = await context.ItemsChecklist.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            item.Descripcion = itemDTO.Descripcion;
            item.ChecklistId = itemDTO.CheckListId;
            context.ItemsChecklist.Update(item);
            await context.SaveChangesAsync();
            return item;
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await context.ItemsChecklist.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            context.ItemsChecklist.Remove(item);
            await context.SaveChangesAsync();
            return NoContent();
        }   

    }
}
