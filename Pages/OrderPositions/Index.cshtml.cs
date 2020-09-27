using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.OrderPositions
{
    public class IndexModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        public IndexModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int id)
        {
            var positions = from s in _context.OrderPositions where s.OrderId == id select s;

            OrderPosition = await positions
                .Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(o => o.Order).Where(a => a.OrderId == id).ToListAsync();

            var order = from o in _context.Orders where o.Id == id select o;
            Order = await order.Include(o => o.Status).SingleOrDefaultAsync();
        }

        public IList<OrderPositionModel> OrderPosition { get; set; }

        public OrderModel Order { get; set; }
    }
}