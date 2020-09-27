using Microsoft.EntityFrameworkCore;

namespace AWO_Orders.Data
{
    public class VOrderStatusContext : DbContext
    {
        public VOrderStatusContext(DbContextOptions<VOrderStatusContext> options)
            : base(options)
        {
        }

        public DbSet<AWO_Orders.Models.V_Order_StatusModel> V_Order_Status { get; set; }
    }
}