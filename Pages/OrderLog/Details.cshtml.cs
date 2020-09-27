using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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