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
        private string filterText;
        private readonly AWO_Orders.Data.EmployeeContext _context;
        private IList<EmployeeModel> models;

        /// <summary>
        /// Setzt den aktuellen Datencontext
        /// </summary>
        /// <param name="context"></param>
        public IndexModel(AWO_Orders.Data.EmployeeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Mitarbeiter liste mit Fremdschlüsseln werden gefüllt
        /// </summary>
        /// <returns></returns>
        public async Task OnGetAsync()
        {
            models = await _context.Employees.ToListAsync();
            models = models.Select(a => { a.Employee = models.SingleOrDefault(b => b.Id == a.ChangedBy); return a; }).ToList();
            models = models.Select(a => { a.Location = _context.Locations.Find(new object[] { a.LocationId });return a; }).ToList();
            models = models.Select(a => { a.Right = _context.Rights.Find(new object[] {  a.RightId }); return a; }).ToList();
            EmployeeModel = models;
        }

        /// <summary>
        /// Gets or sets the list of Employees
        /// </summary>
        public IList<EmployeeModel> EmployeeModel { get; set; }

        /// <summary>
        /// Gets or sets the text to filter by surename and forename
        /// </summary>
        public string FilterText
        {
            get{ return filterText; }
            set 
            {
                filterText = value;
                if (!String.IsNullOrWhiteSpace(value))
                {
                    EmployeeModel = models.Where(a => a.Forename.ToLower().Contains(value.ToLower()) ||
                                        a.SureName.ToLower().Contains(value.ToLower())).ToList();
                }
                else
                {
                    EmployeeModel = models;
                }
            } 
        }
    }
}
