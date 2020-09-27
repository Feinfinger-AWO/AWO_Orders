using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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