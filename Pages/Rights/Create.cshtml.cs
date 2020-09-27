using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Rights
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.RightContext _context;

        public CreateModel(AWO_Orders.Data.RightContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public RightModel RightModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(RightModel);

            _context.Rights.Add(RightModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}