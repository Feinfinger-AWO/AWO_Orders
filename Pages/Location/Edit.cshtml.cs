using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Location
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.LocationContext _context;

        public EditModel(AWO_Orders.Data.LocationContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(LocationModel);

            _context.Attach(LocationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationModelExists(LocationModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LocationModelExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}