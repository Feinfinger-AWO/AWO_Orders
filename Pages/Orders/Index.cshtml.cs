using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;

        public IndexModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        public IList<OrderModel> OrderModel { get;set; }

        public async Task OnGetAsync()
        {
            OrderModel = await _context.Order
                .Include(o => o.Employee)
                .Include(o => o.Status).ToListAsync();
            OrderModel = OrderModel.OrderBy(a => a.PlaceDate).ToList();
        }
    }
}
