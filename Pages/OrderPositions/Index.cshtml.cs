using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OrderPositions
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        public IndexModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        public IList<OrderPosition> OrderPosition { get;set; }

        public async Task OnGetAsync(int id)
        {

            var positions = from s in _context.OrderPositions where s.OrderId == id select s ;

            OrderPosition = await positions
                .Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(o => o.Order).Where(a=>a.OrderId == id).ToListAsync();
        }
    }
}
