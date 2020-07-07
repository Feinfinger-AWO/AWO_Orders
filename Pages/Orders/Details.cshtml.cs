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
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;

        public DetailsModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        public OrderModel OrderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderModel = await _context.Order
                .Include(o => o.Employee)
                .Include(o => o.Status).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
