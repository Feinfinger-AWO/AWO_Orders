using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Employees
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.EmployeeContext _context;

        public EditModel(AWO_Orders.Data.EmployeeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public EmployeeModel EmployeeModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            EmployeeModel = await _context.Employees.FirstOrDefaultAsync(m => m.Id == id);

            if (EmployeeModel == null)
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

            SetBaseProbertiesOnPost(EmployeeModel);

            _context.Attach(EmployeeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(EmployeeModel.Id))
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

        private bool EmployeeModelExists(int id)
        {
            return _context.Employees.Any(e => e.Id == id);
        }

        /// <summary>
        /// Auswahl Liste für Rechte
        /// </summary>
        public List<SelectListItem> RightList
        {
            get
            {
                return _context.Rights.Select
                (
                    a => new SelectListItem { Value = a.Id.ToString(), Text = a.Ident }
                ).ToList();
            }
        }

        /// <summary>
        /// Auswahl Liste für Niederlassungen
        /// </summary>
        public List<SelectListItem> LocationList
        {
            get
            {
                return _context.Locations.Select
                (
                    a => new SelectListItem { Value = a.Id.ToString(), Text = a.Ident }
                ).ToList();
            }
        }
    }
}