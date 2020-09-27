using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class ArticleTypeContext : DbContext
    {
        public ArticleTypeContext(DbContextOptions<ArticleTypeContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ArticleTypeModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<EmployeeModel> Employees { get; set; }
        public DbSet<AWO_Orders.Models.ArticleTypeModel> ArticleTypes { get; set; }
    }
}