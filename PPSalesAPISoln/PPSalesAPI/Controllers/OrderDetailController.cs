using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PPSalesAPI.Context;
using PPSalesAPI.Models;

namespace PPSalesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderDetailController(SalesContext context) : Controller
    {
        private readonly SalesContext _context = context;

        // GET: api/orderDetails
        /// <summary>
        /// For First 100
        /// GET /api/orderDetails?limit=100&offset=0
        /// 
        /// For the Next 100
        /// GET /api/orderDetails?limit=100&offset=100
        /// </summary>
        /// <param name="limit">Length of data to return</param>
        /// <param name="offset">No of data to skip</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetOrderDetails(int limit = 100, int offset = 0)
        {
            var orderDetails = await _context.OrderDetails
            .Skip(offset)
            .Take(limit)
            .ToListAsync();

            return orderDetails;
        }

        // GET: api/orderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetail>> GetOrderDetai(int id)
        {
            var order = await _context.OrderDetails.FirstOrDefaultAsync(o => o.OrderDetailsId == id);

            if (order == null)
            {
                return NotFound();
            }

            return order;
        }

        // POST: api/orderDetails
        [HttpPost]
        public async Task<ActionResult<OrderDetail>> PostOrderDetai(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(OrderDetail), new { id = orderDetail.OrderId }, orderDetail);
        }

        // PUT: api/orderDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetai(int id, OrderDetail orderDetail)
        {
            if (id != orderDetail.OrderDetailsId)
            {
                return BadRequest();
            }

            _context.Entry(orderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailExists(id))
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

        // DELETE: api/orderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailExists(int id)
        {
            return _context.OrderDetails.Any(e => e.OrderDetailsId == id);
        }
    }
}
