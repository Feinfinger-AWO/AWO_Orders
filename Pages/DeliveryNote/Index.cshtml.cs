using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.DeliveryNote
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.OpenOrdersContext _context;

        public IndexModel(AWO_Orders.Data.OpenOrdersContext context)
        {
            _context = context;
        }

        public IList<V_OrdersModel> V_OrdersModel { get; set; }

        public async Task OnGetAsync(IEnumerable<int> idList)
        {
            V_OrdersModel = new List<V_OrdersModel>();

            foreach (var id in idList)
            {
                var position = from p in _context.V_Orders where p.PosId == id select p;
                V_OrdersModel.Add(position.First());
            }
        }
    }
}