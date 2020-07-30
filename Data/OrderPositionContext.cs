using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class OrderPositionContext : DbContext
    {
        public OrderPositionContext (DbContextOptions<OrderPositionContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderPositionModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            modelBuilder.Entity<OrderPositionModel>()
                .HasOne(p => p.ArticleType)
                .WithMany()
                .HasForeignKey(p => p.ArticleTypeId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.OrderPositionModel> OrderPositions { get; set; }

        public DbSet<AWO_Orders.Models.OrderModel> Orders { get; set; }
        public DbSet<AWO_Orders.Models.ArticleTypeModel> ArticleTypes { get; set; }
        public DbSet<AWO_Orders.Models.EmployeeModel> Employees { get; set; }
        public DbSet<AWO_Orders.Models.OrderStatusModel> OrderStatus { get; set; }
    }
}
