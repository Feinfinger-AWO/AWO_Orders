using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OrderLog
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderLogEntriesContext _context;

        public DetailsModel(AWO_Orders.Data.OrderLogEntriesContext context)
        {
            _context = context;
        }

        public OrderLogEntriesModel OrderLogEntriesModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderLogEntriesModel = await _context.OrderLogEntries.FirstOrDefaultAsync(m => m.Id == id);

            if (OrderLogEntriesModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
