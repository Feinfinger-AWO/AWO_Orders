using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.Orders
{
    public class CreateModel : BasePageModel
    {
        private OrderModel orderModel;
        private readonly AWO_Orders.Data.OrdersContext _context;

        public CreateModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["StatusId"] = new SelectList(_context.Set<OrderStatusModel>().OrderBy(s => s.SortNumber), "Id", "Ident");
            return Page();
        }

        [BindProperty]
        public OrderModel OrderModel
        {
            get => orderModel;
            set
            {
                orderModel = value;
                orderModel.StatusId = 1;
                orderModel.PlaceDate = DateTime.Now;
                orderModel.Number = DateTime.Now.Year.ToString();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                SetBaseProbertiesOnPost(OrderModel);
                OrderModel.EmplId = SessionLoginItem.EmployeeId;

                _context.Orders.Add(OrderModel);
                await _context.SaveChangesAsync();

                _context.Update(OrderModel);

                orderModel.Number = DateTime.Now.Year.ToString() + orderModel.Id.ToString("000000");

                await _context.SaveChangesAsync();

                await WriteLog(orderModel.Id, LogChangeTypesEnum.CreateOrder, null);
            }
            catch (Exception e)
            {
                RedirectToPage("/Error", new { ex = e });
            }

            return RedirectToPage("/OrderPositions/Index", new { id = OrderModel.Id });
        }
    }
}