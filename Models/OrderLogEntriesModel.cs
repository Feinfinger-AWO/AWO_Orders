using System;

namespace AWO_Orders.Models
{
    public class OrderLogEntriesModel
    {
        public int Id { get; set; }
        public LogChangeTypesEnum ChangeType { get; set; }
        public int OrderId { get; set; }
        public int EmplId { get; set; }
        public int OrderPositionId { get; set; }
        public DateTime ChangeDateTime { get; set; }
    }
}