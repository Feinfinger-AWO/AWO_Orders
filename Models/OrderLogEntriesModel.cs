using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public enum LogChangeTypes
    {
        CreateOrder = 0,
        EditOrder = 1,
        AddPosition = 2,
        RemovePosition = 3,
        EditPosition = 4
    }

    public class OrderLogEntriesModel
    {
        public int Id { get; set; }
        public LogChangeTypes ChangeType { get; set; }
        public int OrderId { get; set; }
        public int EmplId { get; set; }
        public int OrderPositionId { get; set; }
    }
}
