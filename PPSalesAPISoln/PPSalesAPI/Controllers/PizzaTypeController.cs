using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSalesAPI.Context;
using PPSalesAPI.Models;

namespace PPSalesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaTypeController(SalesContext context) : Controller
    {
        private readonly SalesContext _context = context;

        // GET: api/pizzaTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PizzaType>>> GetPizzaTypes()
        {
            var pizzaTypes = await _context.PizzaTypes.ToListAsync();

            return pizzaTypes;
        }


        // GET: api/pizzaTypes/bbq_ckn
        [HttpGet("{id}")]
        public async Task<ActionResult<PizzaType>> GetPizzaType(string id)
        {
            var pizzaTypes = await _context.PizzaTypes.FirstOrDefaultAsync(o => o.PizzaTypeId == id);

            if (pizzaTypes == null)
            {
                return NotFound();
            }

            return pizzaTypes;

        }

        // POST: api/pizzaTypes
        [HttpPost]
        public async Task<ActionResult<Order>> PostPizzaType(PizzaType pizzaType)
        {
            _context.PizzaTypes.Add(pizzaType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizzaType), new { id = pizzaType.PizzaTypeId }, pizzaType);
        }

        // PUT: api/pizzaTypes/bbq_ckn
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizzaType(string id, PizzaType pizzaType)
        {
            if (id != pizzaType.PizzaTypeId)
            {
                return BadRequest();
            }

            _context.Entry(pizzaType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/pizzaTypes/bbq_ckn
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizzaType(string id)
        {
            var pizzaType = await _context.PizzaTypes.FindAsync(id);
            if (pizzaType == null)
            {
                return NotFound();
            }

            _context.PizzaTypes.Remove(pizzaType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaTypeExists(string id)
        {
            return _context.PizzaTypes.Any(e => e.PizzaTypeId == id);
        }
    }
}
