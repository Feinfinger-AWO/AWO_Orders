using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class ExternalOrdersContext : DbContext
    {
        public ExternalOrdersContext(DbContextOptions<ExternalOrdersContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.ExternalOrderModel> ExternalOrders { get; set; }
        public DbSet<AWO_Orders.Models.EmployeeModel> Employees { get; set; }
    }
}