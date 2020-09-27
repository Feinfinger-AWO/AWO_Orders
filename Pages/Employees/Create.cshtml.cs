using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Employees
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.EmployeeContext _context;

        public CreateModel(AWO_Orders.Data.EmployeeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public EmployeeModel EmployeeModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int Next)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(EmployeeModel);

            _context.Employees.Add(EmployeeModel);
            await _context.SaveChangesAsync();

            if (Next == 0)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("./Create");
            }
        }

        /// <summary>
        /// Auswahl Liste für Niederlassungen
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