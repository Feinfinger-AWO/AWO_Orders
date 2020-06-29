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
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.LocationContext _context;

        public DetailsModel(AWO_Orders.Data.LocationContext context)
        {
            _context = context;
        }

        public LocationModel LocationModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            LocationModel = await _context.Locations.FirstOrDefaultAsync(m => m.Id == id);

            if (LocationModel == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
