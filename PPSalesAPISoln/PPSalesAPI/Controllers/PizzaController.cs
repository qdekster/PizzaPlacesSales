using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSalesAPI.Context;
using PPSalesAPI.Models;
using System.Diagnostics.Metrics;

namespace PPSalesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PizzaController(SalesContext context) : Controller
    {
        private readonly SalesContext _context = context;

        // GET: api/pizzas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pizza>>> GetPizzas()
        {
            var pizzas = await _context.Pizzas.ToListAsync();

            return pizzas;
        }


        // GET: api/pizzas/bbq_ckn_s
        [HttpGet("{id}")]
        public async Task<ActionResult<Pizza>> GetPizza(string id)
        {
            var pizza = await _context.Pizzas.FirstOrDefaultAsync(o => o.PizzaId == id);

            if (pizza == null)
            {
                return NotFound();
            }

            return pizza;
        }


        // POST: api/pizzas
        [HttpPost]
        public async Task<ActionResult<Pizza>> PostPizza(Pizza pizza)
        {
            _context.Pizzas.Add(pizza);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPizza), new { id = pizza.PizzaId }, pizza);
        }

        // PUT: api/pizzas/bbq_ckn_s
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPizza(string id, Pizza pizza)
        {
            if (id != pizza.PizzaId)
            {
                return BadRequest();
            }

            _context.Entry(pizza).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PizzaExists(id))
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

        // DELETE: api/pizzas/bbq_ckn_s
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePizza(string id)
        {
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                return NotFound();
            }

            _context.Pizzas.Remove(pizza);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PizzaExists(string id)
        {
            return _context.Pizzas.Any(e => e.PizzaId == id);
        }
    }
}
