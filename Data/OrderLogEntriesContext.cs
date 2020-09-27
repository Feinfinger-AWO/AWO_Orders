using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AWO_Orders.Data
{
    public class OrderLogEntriesContext : DbContext
    {
        public OrderLogEntriesContext(DbContextOptions<OrderLogEntriesContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Schreibt Änderungen an der Bestellung
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="changeType"></param>
        public async Task WriteLog(int OrderId, LogChangeTypesEnum changeType, int employeeId, int? PosId)
        {
            var log = new OrderLogEntriesModel()
            {
                OrderId = OrderId,
                EmplId = employeeId,
                ChangeType = changeType,
                ChangeDateTime = DateTime.Now,
                OrderPositionId = (PosId.HasValue) ? PosId.Value : -1
            };

            OrderLogEntries.Add(log);
            await SaveChangesAsync();
        }

        public DbSet<AWO_Orders.Models.OrderLogEntriesModel> OrderLogEntries { get; set; }
    }
}