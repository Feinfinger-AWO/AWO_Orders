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
    }
}
