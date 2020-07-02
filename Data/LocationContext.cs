using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class LocationContext : DbContext
    {
        private DbSet<AWO_Orders.Models.LocationModel> locations;

        public LocationContext (DbContextOptions<LocationContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocationModel>()
                .HasOne(p => p.Employee)
                .WithMany()
                .HasForeignKey(p => p.ChangedBy);
            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<AWO_Orders.Models.LocationModel> Locations 
        { 
            get 
            {
                return locations;
            }
            set 
            {
                locations = value;
            }
        }

        public DbSet<EmployeeModel> Employees { get; set; }
    }
}
