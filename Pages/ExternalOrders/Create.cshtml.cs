using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class CreateModel : PageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;

        public CreateModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ChangedBy"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
            ViewData["ManagerId"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
            return Page();
        }

        [BindProperty]
        public ExternalOrderModel ExternalOrderModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.ExternalOrders.Add(ExternalOrderModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}