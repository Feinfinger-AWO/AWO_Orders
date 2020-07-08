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
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;

        public DeleteModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderModel OrderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderModel = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Status).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderModel = await _context.Orders.FindAsync(id);

            if (OrderModel != null)
            {
                _context.Orders.Remove(OrderModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
