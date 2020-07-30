using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AWO_Orders.Pages.Orders
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;
        private readonly OrderStatusContext _orderStatusContext;
        private string filterText;
        private int filterStatusId;

        public IndexModel(AWO_Orders.Data.OrdersContext context, OrderStatusContext orderStatusContext)
        {
            _context = context;
            _orderStatusContext = orderStatusContext;
        }

        public async Task OnGetAsync(string searchString, int? filterStatusId,int? id)
        {
            var loginItem = GetLogin();

            FilterStatusId = filterStatusId?? 1;
            FilterText = searchString;

            if (id.HasValue)
            {
                await SetReady(id.Value);
            }

            var orders = (loginItem.Right.CanProcess) ?
                    from s in _context.Orders where s.StatusId == FilterStatusId select s :
                      from s in _context.Orders where s.StatusId == FilterStatusId && s.EmplId == loginItem.EmployeeId select s;

            OrderModel = await orders
                .Include(o => o.Employee)
                .Include(o => o.Status).ToListAsync();

            if (!String.IsNullOrWhiteSpace(FilterText))
            {
                OrderModel = OrderModel.Where(a => a.Employee.SureName.ToLower().Contains(FilterText.ToLower()) || a.Number.ToLower().Contains(FilterText.ToLower())).ToList();
            }

            OrderModel = OrderModel.OrderBy(a => a.PlaceDate).ToList();
            ViewData["StatusId"] = new SelectList(_context.Set<OrderStatusModel>(), "Id", "Ident");
        }

        private async Task SetReady(int orderId)
        {
            var order = (from s in _context.Orders where s.Id == orderId select s).First();
            order.StatusId = (from s in _orderStatusContext.OrderStatus where s.BaseStatus == OrderBaseStatusEnum.Okay select s).First().Id;
            FilterStatusId = order.StatusId;
            await _context.SaveChangesAsync();
        }

        public string FilterText { get => filterText; set => filterText = value; }

        public IList<OrderModel> OrderModel { get; set; }
        public int FilterStatusId { get => filterStatusId; set => filterStatusId = value; }
    }
}
