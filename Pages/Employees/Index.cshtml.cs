﻿using System;
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
            EmployeeModel = await _context.Employees.ToListAsync();
            EmployeeModel = EmployeeModel.Select(a =>{ a.Employee = EmployeeModel.SingleOrDefault(b => b.Id == a.ChangedBy); return a; }).ToList();
            EmployeeModel = EmployeeModel.Select(a => { a.Location = _context.Locations.Find(new object[] { a.LocationId });return a; }).ToList();
            EmployeeModel = EmployeeModel.Select(a => { a.Right = _context.Rights.Find(new object[] {  a.RightId }); return a; }).ToList();
        }

        /// <summary>
        /// Gets or sets the list of Employees
        /// </summary>
        public IList<EmployeeModel> EmployeeModel { get; set; }
    }
}
