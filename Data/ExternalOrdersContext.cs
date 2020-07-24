using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Models;

namespace AWO_Orders.Data
{
    public class ExternalOrdersContext : DbContext
    {
        public ExternalOrdersContext (DbContextOptions<ExternalOrdersContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.ExternalOrderModel> ExternalOrders { get; set; }
        public DbSet<AWO_Orders.Models.EmployeeModel> Employees { get; set; }
    }
}
