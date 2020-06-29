using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Employees
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.EmployeeContext _context;

        public DetailsModel(AWO_Orders.Data.EmployeeContext context)
        {
            _context = context;
        }

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
    }
}
