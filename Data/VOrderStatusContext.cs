using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class VOrderStatusContext : DbContext
    {
        public VOrderStatusContext (DbContextOptions<VOrderStatusContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.V_Order_StatusModel> V_Order_Status { get; set; }
    }
}
