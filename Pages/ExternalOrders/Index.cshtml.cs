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
using System.ComponentModel.DataAnnotations;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;
        private string filterText;
        private DateTime _from = DateTime.MinValue;
        private DateTime _to = DateTime.Now.AddDays(1);

        public IndexModel(AWO_Orders.Data.ExternalOrdersContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? pageIndex,string searchString,string from,string to)
        {

            if (!string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                _from = DateTime.Parse(from);
                _to = DateTime.Parse(to);
            }

            dtFrom = _from;
            dtTo = _to;
            IQueryable<ExternalOrderModel> models = null;
            filterText = searchString;

            if (_from > DateTime.MinValue && _to > DateTime.MinValue)
            {
                models = (!String.IsNullOrWhiteSpace(searchString)) ? from e in _context.ExternalOrders
                                                                      where (e.Manager.SureName.ToLower().Contains(searchString.ToLower()) ||
                                                                             e.Notes.ToLower().Contains(searchString.ToLower())) && e.ProcessedAt.Date >= _from.Date && e.ProcessedAt.Date <= _to.Date
                                                                      select e :
                                                                    from e in _context.ExternalOrders where e.ProcessedAt.Date >= _from.Date && e.ProcessedAt.Date <= _to.Date select e;
            }
            else
            {    
                models = (!String.IsNullOrWhiteSpace(searchString)) ? from e in _context.ExternalOrders
                                                                          where e.Manager.SureName.ToLower().Contains(searchString.ToLower()) ||
                                                                                 e.Notes.ToLower().Contains(searchString.ToLower())
                                                                          select e :
                                                                            _context.ExternalOrders;
            }
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
        public string From { get => _from.ToShortDateString(); set => _from = DateTime.Parse(value); }
        public string To { get => _to.ToShortDateString(); set => _to = DateTime.Parse(value); }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtFrom { get => _from; set => _from = value; }
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dtTo { get => _to; set => _to = value; }
    }
}
