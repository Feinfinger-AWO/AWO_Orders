using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using AWO_Orders.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;
        private string filterText;

        public IndexModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? pageIndex,string searchString)
        {
            filterText = searchString;
            var models = (!String.IsNullOrWhiteSpace(searchString)) ? from e in _context.ExternalOrders
                                                                      where e.Manager.SureName.ToLower().Contains(searchString.ToLower()) ||
                                                                             e.Notes.ToLower().Contains(searchString.ToLower())
                                                                      select e :
                                                                        _context.ExternalOrders;
            if (models.Any())
            {
                ExternalOrderModel = await PaginatedList<ExternalOrderModel>.CreateAsync(
                                             models.Include(e => e.Employee)
                    .Include(e => e.Manager).AsNoTracking(), pageIndex ?? 1, 10);
            }
            else
            {
                ExternalOrderModel = await PaginatedList<ExternalOrderModel>.CreateAsync(models.AsNoTracking(), 1, 0);
            }
        }

        public PaginatedList<ExternalOrderModel> ExternalOrderModel { get; set; }
        public string FilterText { get => filterText; set => filterText = value; }
    }
}
