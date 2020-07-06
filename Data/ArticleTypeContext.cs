using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class ArticleTypeContext : DbContext
    {
        public ArticleTypeContext (DbContextOptions<ArticleTypeContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.ArticleTypeModel> ArticleTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTypeModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
