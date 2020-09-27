using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.OrderLog
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderLogEntriesContext _context;

        public IndexModel(AWO_Orders.Data.OrderLogEntriesContext context)
        {
            _context = context;
        }

        public IList<OrderLogEntriesModel> OrderLogEntriesModel { get; set; }

        public async Task OnGetAsync()
        {
            OrderLogEntriesModel = await _context.OrderLogEntries.ToListAsync();
        }
    }
}