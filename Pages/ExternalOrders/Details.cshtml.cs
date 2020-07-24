using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;

        public DetailsModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        public ExternalOrderModel ExternalOrderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExternalOrderModel = await _context.ExternalOrders
                .Include(e => e.Employee)
                .Include(e => e.Manager).FirstOrDefaultAsync(m => m.Id == id);

            if (ExternalOrderModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
