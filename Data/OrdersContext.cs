using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext(DbContextOptions<OrdersContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.OrderModel> Orders { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<OrderStatusModel> OrderStatus { get; set; }
    }
}