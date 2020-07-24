using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OpenOrders
{
    public class OpenOrdersModel : PageModel
    {
        private readonly AWO_Orders.Data.OpenOrdersContext _context;
        private readonly OrdersContext _orderContext;
        private readonly OrderPositionContext _orderPositionContext;

        public OpenOrdersModel(AWO_Orders.Data.OpenOrdersContext context, AWO_Orders.Data.OrdersContext orderContext, OrderPositionContext orderPositionContext)
        {
            _context = context;
            _orderContext = orderContext;
            _orderPositionContext = orderPositionContext;
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
            await SetPositionStatus(items);
            await RefreshOrderStatus(items);
        }

        private async Task SetPositionStatus(IList<V_OrdersModel> items)
        {
            foreach(var item in items)
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
            }
            await _orderPositionContext.SaveChangesAsync();
        }

        private async Task RefreshOrderStatus(IList<V_OrdersModel> items)
        {
            foreach(var id in items.Select(a => a.Id))
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

