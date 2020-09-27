using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Fremdschlüssel werden gesetzt
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasOne(p => p.Right)
                .WithMany()
                .HasForeignKey(p => p.RightId);

            modelBuilder.Entity<EmployeeModel>()
            .HasOne(p => p.Location)
            .WithMany()
            .HasForeignKey(p => p.LocationId);

            modelBuilder.Entity<EmployeeModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.EmployeeModel> Employees { get; set; }
        public DbSet<RightModel> Rights { get; set; }
        public DbSet<LocationModel> Locations { get; set; }
    }
}