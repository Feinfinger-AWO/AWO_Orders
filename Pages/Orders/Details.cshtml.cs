using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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

            OrderModel = await _context.Orders
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