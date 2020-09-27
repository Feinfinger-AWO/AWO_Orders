using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Location
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.LocationContext _context;

        public IndexModel(AWO_Orders.Data.LocationContext context)
        {
            _context = context;
        }

        public IList<LocationModel> LocationModel { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            FilterText = searchString;

            LocationModel = await _context.Locations.ToListAsync();

            if (!String.IsNullOrWhiteSpace(FilterText))
            {
                LocationModel = LocationModel.Where(a => a.Ident.ToLower().Contains(FilterText.ToLower())).ToList();
            }

            LocationModel = LocationModel.Select(a => { a.Employee = _context.Employees.SingleOrDefault(b => b.Id == a.ChangedBy); return a; }).ToList();
            LocationModel = LocationModel.OrderBy(a => a.Ident).ToList();
        }

        public string FilterText { get; set; }
    }
}