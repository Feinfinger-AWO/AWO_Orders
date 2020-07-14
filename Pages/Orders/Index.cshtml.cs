using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Orders
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;
        private string filterText;

        public IndexModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(string searchString)
        {
            FilterText = searchString;
            OrderModel = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Status).ToListAsync();

            if (!String.IsNullOrWhiteSpace(FilterText))
            {
                OrderModel = OrderModel.Where(a => a.Employee.SureName.ToLower().Contains(FilterText.ToLower()) || a.Number.ToLower().Contains(FilterText.ToLower())).ToList();
            }

            OrderModel = OrderModel.OrderBy(a => a.PlaceDate).ToList();
        }

        public string FilterText { get => filterText; set => filterText = value; }

        public IList<OrderModel> OrderModel { get; set; }
    }
}
