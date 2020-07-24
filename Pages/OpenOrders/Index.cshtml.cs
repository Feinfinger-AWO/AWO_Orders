using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.Data.SqlClient;

namespace AWO_Orders.Pages.OpenOrders
{
    public class OpenOrdersModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OpenOrdersContext _context;
        private readonly OrdersContext _orderContext;
        private readonly OrderPositionContext _orderPositionContext;
        private readonly ExternalOrdersContext _externalOrdersContext;

        public OpenOrdersModel(AWO_Orders.Data.OpenOrdersContext context,
            AWO_Orders.Data.OrdersContext orderContext,
            ExternalOrdersContext externalOrdersContext,
            OrderPositionContext orderPositionContext)
        {
            _context = context;
            _orderContext = orderContext;
            _orderPositionContext = orderPositionContext;
            _externalOrdersContext = externalOrdersContext;
        }

        [BindProperty]
        public IList<V_OrdersModel> V_OrdersModel { get; set; }

        public async Task OnGetAsync()
        {
            var models = from s in _context.V_Orders
                         where
                    (s.BaseStatus == OrderBaseStatusEnum.Okay || s.BaseStatus == OrderBaseStatusEnum.InProcess) &&
                    s.PosStatus == PositionStatusEnum.Open
                         orderby s.Id, s.PosNumber
                         select s;

            V_OrdersModel = await models.ToListAsync();
        }

        public async void OnPostAsync(IList<V_OrdersModel> items)
        {

            var connection = _externalOrdersContext.Database.GetDbConnection();
            connection.Open();
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT NEXT VALUE FOR SequenzExternOrders";
            var externId = cmd.ExecuteScalar();

            var externalOrder = new ExternalOrderModel()
            {
                Id = (long)externId,
                ManagerId = SessionLoginItem.EmployeeId,
                ProcessedAt = DateTime.Now,
                Changed = DateTime.Now,
                ChangedBy = SessionLoginItem.EmployeeId
            };

            _externalOrdersContext.Add(externalOrder);
            await _externalOrdersContext.SaveChangesAsync();

            await SetPositionStatus(items, (long)externId);     
        }

        private async Task SetPositionStatus(IList<V_OrdersModel> items, long externId)
        {
            var changedOrders = new List<int>();
            foreach(var item in items)
            {
                if (item.Selected || item.Rejected)
                {
                    var position = (from s in _orderPositionContext.OrderPositions where s.Id == item.PosId select s).Single();
                    if (item.Selected)
                    {
                        position.Status = PositionStatusEnum.Ordered;
                    }
                    if (item.Rejected)
                    {
                        position.Status = PositionStatusEnum.Rejected;
                    }
                    position.ExternId = externId;
                    
                    if(!changedOrders.Contains(position.OrderId))
                        changedOrders.Add(position.OrderId);
                }
            }
            await _orderPositionContext.SaveChangesAsync();
            if(changedOrders.Any())
                await RefreshOrderStatus(changedOrders);
        }

        private async Task RefreshOrderStatus(IList<int> Ids)
        {
            foreach(var id in Ids)
            {
                var order = (from o in _orderContext.Orders where o.Id == id select o).First();
                var Pos = from p in _orderPositionContext.OrderPositions where p.OrderId == id && p.Status == PositionStatusEnum.Open select p;
                if (Pos.Any())
                {
                    order.StatusId = (from s in _orderContext.OrderStatus where s.BaseStatus == OrderBaseStatusEnum.InProcess select s).First().Id;
                }
                else
                {
                    order.StatusId = (from s in _orderContext.OrderStatus where s.BaseStatus == OrderBaseStatusEnum.Ordered select s).First().Id;
                }
            }

            await _orderContext.SaveChangesAsync();
        }

        private async Task SendInfoMail()
        {

        }
    }
}

