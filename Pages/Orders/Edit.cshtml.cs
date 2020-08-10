using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.Orders
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrdersContext _context;

        public EditModel(AWO_Orders.Data.OrdersContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderModel OrderModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderModel = await _context.Orders
                .Include(o => o.Employee)
                .Include(o => o.Status).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderModel == null)
            {
                return NotFound();
            }

           ViewData["StatusId"] = new SelectList(_context.Set<OrderStatusModel>().OrderBy(s => s.SortNumber), "Id", "Ident");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(OrderModel);

            _context.Attach(OrderModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                await WriteLog(OrderModel.Id, LogChangeTypesEnum.EditOrder, null);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderModelExists(OrderModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index",new { filterStatusId = OrderModel.StatusId });
        }

        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
