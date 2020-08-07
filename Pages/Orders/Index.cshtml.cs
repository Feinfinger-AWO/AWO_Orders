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
using AWO_Orders.Components;

namespace AWO_Orders.Pages.Orders
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;
        private readonly OrderStatusContext _orderStatusContext;
        private string filterText;
        private int filterStatusId;
        private bool pagingEnabled;

        public IndexModel(AWO_Orders.Data.OrdersContext context, OrderStatusContext orderStatusContext)
        {
            _context = context;
            _orderStatusContext = orderStatusContext;
        }

        public async Task OnGetAsync(string searchString, int? filterStatusId, int? id,int? pageIndex)
        {
            var loginItem = GetLogin();

            FilterStatusId = filterStatusId ?? 1;
            FilterText = searchString;

            if (id.HasValue)
            {
                await SetReady(id.Value);
            }

            IQueryable<OrderModel> orders = null;

            if (!String.IsNullOrWhiteSpace(FilterText))
            {

                orders = (loginItem.Right.CanProcess) ?
                        from s in _context.Orders where s.StatusId == FilterStatusId &&
                        (s.Employee.SureName.ToLower().Contains(FilterText.ToLower()) || s.Number.ToLower().Contains(FilterText.ToLower()))
                        select s :
                        from s in _context.Orders where s.StatusId == FilterStatusId && s.EmplId == loginItem.EmployeeId &&
                                                (s.Employee.SureName.ToLower().Contains(FilterText.ToLower()) || s.Number.ToLower().Contains(FilterText.ToLower()))
                        select s;
            }
            else
            {
                orders = (loginItem.Right.CanProcess) ?
                        from s in _context.Orders where s.StatusId == FilterStatusId select s :
                          from s in _context.Orders where s.StatusId == FilterStatusId && s.EmplId == loginItem.EmployeeId select s;
            }

            if (PagingEnabled)
            {
                if (orders.Any())
                {
                    POrderModel = await PaginatedList<OrderModel>.CreateAsync(
                        orders.Include(o => o.Employee).Include(o => o.Status)
                        .AsNoTracking(), pageIndex ?? 1, 10);
                }
                else
                {
                    POrderModel = await PaginatedList<OrderModel>.CreateAsync(orders.AsNoTracking(), 1, 0);
                }
            }
            else
            {
                OrderModel = await orders
                    .Include(o => o.Employee)
                     .Include(o => o.Status).ToListAsync();
                OrderModel = OrderModel.OrderBy(a => a.PlaceDate).ToList();
            }

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
        public PaginatedList<OrderModel> POrderModel { get; set; }
        public bool PagingEnabled 
        { 
            get
            {
                var status = (from s in _context.OrderStatus where s.Id == FilterStatusId select s).First();
                pagingEnabled = status.BaseStatus == OrderBaseStatusEnum.Canceled || status.BaseStatus == OrderBaseStatusEnum.Delivered;
                return pagingEnabled;
            }
            set 
            { 
                pagingEnabled = value; 
            } 
        }
    }
}
