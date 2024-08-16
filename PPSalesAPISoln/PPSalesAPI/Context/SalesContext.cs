using Microsoft.EntityFrameworkCore;
using PPSalesAPI.Models;

namespace PPSalesAPI.Context
{
    public class SalesContext(DbContextOptions<SalesContext> options) : DbContext(options)
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
              .HasKey(od => od.OrderId); // Define primary key

            modelBuilder.Entity<Pizza>()
             .HasKey(od => od.PizzaId); // Define primary key


            modelBuilder.Entity<PizzaType>()
             .HasKey(od => od.PizzaTypeId); // Define primary key

            modelBuilder.Entity<OrderDetail>()
                .HasKey(od => od.OrderDetailsId); // Define primary key

          
        }
    }
}
