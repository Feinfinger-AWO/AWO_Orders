using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Rights
{
    public class EditModel : PageModel
    {
        private readonly AWO_Orders.Data.RightContext _context;

        public EditModel(AWO_Orders.Data.RightContext context)
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

            _context.Attach(RightModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RightModelExists(RightModel.Id))
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

        private bool RightModelExists(int id)
        {
            return _context.Rights.Any(e => e.Id == id);
        }
    }
}
