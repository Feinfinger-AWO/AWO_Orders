using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;
using Newtonsoft.Json;

namespace AWO_Orders.Data
{
    public class VOrderStatusEmplContext : DbContext
    {
        public VOrderStatusEmplContext(DbContextOptions<VOrderStatusEmplContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<V_Order_Status_EmplModel>()
                .HasOne(p => p.Empl)
                .WithMany()
                .HasForeignKey(p => p.EmplId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.V_Order_Status_EmplModel> V_Order_Status_Empl { get; set; }
        public DbSet<AWO_Orders.Models.EmployeeModel> Employees { get; set; }
    }
}
