using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class RightContext : DbContext
    {
        public RightContext(DbContextOptions<RightContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RightModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.RightModel> Rights { get; set; }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}