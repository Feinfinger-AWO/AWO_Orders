using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class EditModel : PageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;

        public EditModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        [BindProperty]
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
            ViewData["ChangedBy"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
            ViewData["ManagerId"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
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

            _context.Attach(ExternalOrderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExternalOrderModelExists(ExternalOrderModel.Id))
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

        private bool ExternalOrderModelExists(long id)
        {
            return _context.ExternalOrders.Any(e => e.Id == id);
        }
    }
}