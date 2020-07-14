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

        public DbSet<AWO_Orders.Models.OrderPosition> OrderPositions { get; set; }
    }
}
