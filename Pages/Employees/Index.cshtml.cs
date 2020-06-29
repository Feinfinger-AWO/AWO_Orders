using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace AWO_Orders.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.EmployeeContext _context;

        public IndexModel(AWO_Orders.Data.EmployeeContext context)
        {
            _context = context;
        }

        public IList<EmployeeModel> EmployeeModel { get;set; }

        public async Task OnGetAsync()
        {
            EmployeeModel = await _context.Employees.ToListAsync();
            EmployeeModel = EmployeeModel.Select(a => a.Employee = EmployeeModel.SingleOrDefault(b => b.Id == a.ChangedBy)).ToList();
        }
    }
}
