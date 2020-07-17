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
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        private void ReorderPositions(int orderId)
        {
            var positions = from s in _context.OrderPositions where s.OrderId == orderId orderby s.Number select s;
            int n = 1;

            foreach(var position in positions)
            {
                position.Number = n;
                n = n + 1;
            }
        }

        public DeleteModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

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

            OrderId = OrderPosition.OrderId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderPosition = await _context.OrderPositions.FindAsync(id);

            if (OrderPosition != null)
            {
                _context.OrderPositions.Remove(OrderPosition);
                await _context.SaveChangesAsync();
                ReorderPositions(OrderPosition.OrderId);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index", new { id = OrderPosition.OrderId });
        }

        [BindProperty]
        public OrderPositionModel OrderPosition { get; set; }

        public int OrderId { get; set; }
    }
}
