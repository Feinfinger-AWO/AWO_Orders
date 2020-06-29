using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Location
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.LocationContext _context;

        public IndexModel(AWO_Orders.Data.LocationContext context)
        {
            _context = context;
        }

        public IList<LocationModel> LocationModel { get;set; }

        public async Task OnGetAsync()
        {
            LocationModel = await _context.Locations.ToListAsync();
        }
    }
}
