using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class OrderLogEntriesContext : DbContext
    {
        public OrderLogEntriesContext (DbContextOptions<OrderLogEntriesContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.OrderLogEntriesModel> OrderLogEntries { get; set; }
    }
}
