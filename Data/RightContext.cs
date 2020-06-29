using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class RightContext : DbContext
    {
        public RightContext (DbContextOptions<RightContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.RightModel> Rights { get; set; }
    }
}
