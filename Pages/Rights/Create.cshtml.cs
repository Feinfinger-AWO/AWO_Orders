using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Rights
{
    public class CreateModel : PageModel
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

            RightModel.Changed = DateTime.Now;
            RightModel.ChangedBy = LoginItem.EmployeeId;

            _context.Rights.Add(RightModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
