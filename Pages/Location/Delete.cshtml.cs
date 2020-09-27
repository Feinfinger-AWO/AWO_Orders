using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWO_Orders.Location
{
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.LocationContext _context;

        public DeleteModel(AWO_Orders.Data.LocationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LocationModel LocationModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LocationModel = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (LocationModel == null)
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

            LocationModel = await _context.Locations.FindAsync(id);

            if (LocationModel != null)
            {
                _context.Locations.Remove(LocationModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}