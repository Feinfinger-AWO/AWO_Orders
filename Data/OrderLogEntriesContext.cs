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

        /// <summary>
        /// Schreibt Änderungen an der Bestellung
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="changeType"></param>
        public void WriteLog(OrderModel Order,LogChangeTypesEnum changeType)
        {
            var log = new OrderLogEntriesModel()
            {
                OrderId = Order.Id,
                EmplId = LoginItem.EmployeeId,
                ChangeType = changeType,
                ChangeDateTime = DateTime.Now
            };

            OrderLogEntries.Add(log);
            SaveChanges();
        }

        public DbSet<AWO_Orders.Models.OrderLogEntriesModel> OrderLogEntries { get; set; }
    }
}
