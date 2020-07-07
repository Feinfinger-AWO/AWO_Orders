using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class OrderStatusContext : DbContext
    {
        public OrderStatusContext (DbContextOptions<OrderStatusContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderStatusModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.OrderStatusModel> OrderStatus { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
