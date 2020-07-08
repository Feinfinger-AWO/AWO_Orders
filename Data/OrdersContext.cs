using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class OrdersContext : DbContext
    {
        public OrdersContext (DbContextOptions<OrdersContext> options)
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

        /// <summary>
        /// Erstellt eine neue Auftragsnummer
        /// </summary>
        /// <returns></returns>
        public string CreateNumber()
        {
            var number = DateTime.Now.Year.ToString();



            return number;
        }

        public DbSet<AWO_Orders.Models.OrderModel> Orders { get; set; }
        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<OrderStatusModel> OrderStatus { get; set; }
    }
}
