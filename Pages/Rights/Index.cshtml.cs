using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Rights
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.RightContext _context;

        public IndexModel(AWO_Orders.Data.RightContext context)
        {
            _context = context;
        }

        public IList<RightModel> RightModel { get; set; }

        public async Task OnGetAsync()
        {
            RightModel = await _context.Rights.Include(o => o.Employee).ToListAsync();
            RightModel = RightModel.OrderBy(a => a.Ident).ToList();
        }
    }
}