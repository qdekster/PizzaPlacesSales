using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSalesAPI.Context;
using PPSalesAPI.Models;

namespace PPSalesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController(SalesContext context) : ControllerBase
    {
        private readonly SalesContext _context = context;

        // GET: api/orders
        /// <summary>
        /// For First 100
        /// GET /api/orders?limit=100&offset=0
        /// 
        /// For the Next 100
        /// GET /api/orders?limit=100&offset=100
        /// </summary>
        /// <param name="limit">Length of data to return</param>
        /// <param name="offset">No of data to skip</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders(int limit = 100, int offset = 0)
        {
            var orders = await _context.Orders            
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            return orders;
        }

        // GET: api/orders/byYear/2023/100/0
        [HttpGet("byYear/{year}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByYear(int year, int limit = 100, int offset = 0)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate.Year == year)               
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }

            return orders;
        }


        // GET: api/orders/byYear/2023/100/0
        [HttpGet("byYear/{year}/{month}")]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByYearAndMonth(int year, int month, int limit = 100, int offset = 0)
        {
            var orders = await _context.Orders
                .Where(o => o.OrderDate.Year == year && o.OrderDate.Month == month)               
                .Skip(offset)
                .Take(limit)
                .ToListAsync();

            if (orders == null || orders.Count == 0)
            {
                return NotFound();
            }

            return orders;
        }


        // GET: api/orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/orders
        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOrder), new { id = order.OrderId }, order);
        }

        // PUT: api/orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, Order order)
        {
            if (id != order.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // DELETE: api/orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }


    }
}
