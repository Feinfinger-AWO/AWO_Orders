using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;

        public IndexModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        public IList<ExternalOrderModel> ExternalOrderModel { get;set; }

        public async Task OnGetAsync()
        {
            ExternalOrderModel = await _context.ExternalOrders
                .Include(e => e.Employee)
                .Include(e => e.Manager).ToListAsync();
        }
    }
}
