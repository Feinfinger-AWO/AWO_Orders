using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.OrderStatus
{
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderStatusContext _context;

        public DeleteModel(AWO_Orders.Data.OrderStatusContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderStatusModel = await _context.OrderStatus.FindAsync(id);

            if (OrderStatusModel != null)
            {
                _context.OrderStatus.Remove(OrderStatusModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}