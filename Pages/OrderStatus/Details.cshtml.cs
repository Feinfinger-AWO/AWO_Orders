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
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderStatusContext _context;

        public DetailsModel(AWO_Orders.Data.OrderStatusContext context)
        {
            _context = context;
        }

        public OrderStatusModel OrderStatusModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderStatusModel = await _context.OrderStatus
                .Include(o => o.Employee).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderStatusModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
