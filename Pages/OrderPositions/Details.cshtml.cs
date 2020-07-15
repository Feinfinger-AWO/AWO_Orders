using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OrderPositions
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        public DetailsModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        public OrderPositionModel OrderPosition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderPosition = await _context.OrderPositions
                .Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(o => o.Order).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderPosition == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
