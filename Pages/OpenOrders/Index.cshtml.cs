using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OpenOrders
{
    public class OpenOrdersModel : PageModel
    {
        private readonly AWO_Orders.Data.OpenOrdersContext _context;
        private readonly OrdersContext _orderContext;

        public OpenOrdersModel(AWO_Orders.Data.OpenOrdersContext context, AWO_Orders.Data.OrdersContext orderContext)
        {
            _context = context;
            _orderContext = orderContext;
        }

        [BindProperty]
        public IList<V_OrdersModel> V_OrdersModel { get; set; }

        public async Task OnGetAsync()
        {
            var models = from s in _context.V_Orders
                         where
                    (s.BaseStatus == OrderBaseStatusEnum.Okay || s.BaseStatus == OrderBaseStatusEnum.InProcess) &&
                    s.PosStatus == PositionStatusEnum.Open
                         orderby s.Id, s.PosNumber
                         select s;

            V_OrdersModel = await models.ToListAsync();
        }

        public void OnPost(IList<V_OrdersModel> items)
        {

        }
    }
}

