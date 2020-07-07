using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Orders
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;

        public CreateModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["ChangedBy"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
            ViewData["StatusId"] = new SelectList(_context.Set<OrderStatusModel>(), "Id", "Ident");
            OrderModel.StatusId = 1;
            return Page();
        }

        [BindProperty]
        public OrderModel OrderModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(OrderModel);
            OrderModel.EmplId = LoginItem.EmployeeId;

            _context.Order.Add(OrderModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
