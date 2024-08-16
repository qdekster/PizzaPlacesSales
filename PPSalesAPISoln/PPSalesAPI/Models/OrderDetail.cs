using System.ComponentModel.DataAnnotations.Schema;

namespace PPSalesAPI.Models
{
    public class OrderDetail
    {
        public int OrderDetailsId { get; set; }
        public int OrderId { get; set; }
        public string? PizzaId { get; set; }
        public int Quantity { get; set; }
     
    }
}
