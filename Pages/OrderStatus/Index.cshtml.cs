using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OrderStatus
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderStatusContext _context;

        public IndexModel(AWO_Orders.Data.OrderStatusContext context)
        {
            _context = context;
        }

        public IList<OrderStatusModel> OrderStatusModel { get;set; }

        public async Task OnGetAsync()
        {
            OrderStatusModel = await _context.OrderStatus
                .Include(o => o.Employee).ToListAsync();
            OrderStatusModel = OrderStatusModel.OrderBy(a => a.Ident).ToList();
        }
    }
}
