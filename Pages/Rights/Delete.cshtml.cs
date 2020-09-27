using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Rights
{
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.RightContext _context;

        public DeleteModel(AWO_Orders.Data.RightContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RightModel RightModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            RightModel = await _context.Rights.FirstOrDefaultAsync(m => m.Id == id);

            if (RightModel == null)
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

            RightModel = await _context.Rights.FindAsync(id);

            if (RightModel != null)
            {
                _context.Rights.Remove(RightModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}