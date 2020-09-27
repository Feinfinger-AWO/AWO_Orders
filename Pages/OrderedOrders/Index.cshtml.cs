using AWO_Orders.Data;
using AWO_Orders.Interface;
using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.OrderedOrders
{
    public class IndexModel : BasePageModel
    {
        private readonly OrdersContext _orderContext;
        private readonly OrderPositionContext _orderPositionContext;
        private readonly ExternalOrdersContext _externalOrdersContext;
        private readonly IMailer _mailer;
        private string filterText;
        private readonly AWO_Orders.Data.OpenOrdersContext _context;

        public IndexModel(AWO_Orders.Data.OpenOrdersContext context,
               AWO_Orders.Data.OrdersContext orderContext,
            ExternalOrdersContext externalOrdersContext,
            OrderPositionContext orderPositionContext,
            IMailer mailer)
        {
            _context = context;
            _orderContext = orderContext;
            _orderPositionContext = orderPositionContext;
            _externalOrdersContext = externalOrdersContext;
            _mailer = mailer;
        }

        public IList<V_OrdersModel> V_OrdersModel { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            FilterText = searchString;
            var models = from s in _context.V_Orders
                         where
                    (s.BaseStatus == OrderBaseStatusEnum.Ordered || s.BaseStatus == OrderBaseStatusEnum.InProcess) &&
                    s.PosStatus == PositionStatusEnum.Ordered
                         orderby s.Id, s.PosNumber
                         select s;

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                V_OrdersModel = await models.Where(m => m.Number.ToLower().Contains(searchString.ToLower()) ||
                    m.Description.ToLower().Contains(searchString.ToLower()) ||
                     m.Employee.ToLower().Contains(searchString.ToLower())).ToListAsync();
            }
            else
            {
                V_OrdersModel = await models.ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostAsync(IList<V_OrdersModel> items)
        {
            var incoming = items.Where(i => i.Selected == true || i.Rejected == true).Any();
            LastError = null;
            if (incoming)
            {
                var positions = new List<OrderPositionModel>();
                var changedOrders = new List<int>();

                foreach (var item in items)
                {
                    if (item.Selected || item.Rejected)
                    {
                        var position = (from s in _orderPositionContext.OrderPositions where s.Id == item.PosId select s).Single();

                        if (item.Selected)
                        {
                            position.Status = PositionStatusEnum.Delivered;
                        }

                        if (item.Rejected)
                        {
                            position.Status = PositionStatusEnum.Rejected;
                        }

                        positions.Add(position);

                        if (!changedOrders.Contains(position.OrderId))
                            changedOrders.Add(position.OrderId);
                    }
                }

                await _orderPositionContext.SaveChangesAsync();

                if (changedOrders.Any())
                    await RefreshOrderStatus(changedOrders, positions);

                return RedirectToPage("/DeliveryNote/Index", new { idList = positions.Select(i => i.Id) });
            }

            return RedirectToPage("/Info", new { subject = "Keine Auswahl vorhanden!", nextPage = "/Index" });
        }

        private async Task RefreshOrderStatus(IList<int> ids, List<OrderPositionModel> positions)
        {
            var orders = new List<OrderModel>();
            foreach (var id in ids)
            {
                var order = (from o in _orderContext.Orders where o.Id == id select o).Include(e => e.Empl).Include(e => e.Status).First();
                var Pos = from p in _orderPositionContext.OrderPositions where p.OrderId == id && (p.Status == PositionStatusEnum.Open || p.Status == PositionStatusEnum.Ordered) select p;

                if (!Pos.Any())
                {
                    order.StatusId = (from s in _orderContext.OrderStatus where s.BaseStatus == OrderBaseStatusEnum.Delivered select s).First().Id;
                }

                orders.Add(order);
            }

            await _orderContext.SaveChangesAsync();
            await SendInfoMail(orders, positions);
        }

        private async Task SendInfoMail(IList<OrderModel> orders, List<OrderPositionModel> positions)
        {
            foreach (var group in orders.GroupBy(g => g.EmplId))
            {
                var mail = group.First().Empl.EMail;

                var builder = new StringBuilder();

                if (!String.IsNullOrWhiteSpace(mail))
                {
                    foreach (var order in group)
                    {
                        var posList = positions.Where(p => p.OrderId == order.Id);
                        builder.Append("<p>Bestellung: " + order.Number + " Status: " + order.Status.Ident + " </p>");
                        foreach (var position in posList)
                        {
                            builder.Append("Position: " + position.Number + " Beschreibung: " + position.Description + " Status: " + position.Status.ToString() + " <br>");
                        }
                    }
                    await _mailer.SendPdfAsync(mail, null, builder.ToString());

                    if (_mailer.LastError != null)
                    {
                        LastError = _mailer.LastError;
                    }
                }
            }
        }

        public Exception LastError { get; set; }
        public string FilterText { get => filterText; set => filterText = value; }
    }
}