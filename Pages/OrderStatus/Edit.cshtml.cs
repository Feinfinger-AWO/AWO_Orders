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

namespace AWO_Orders.Pages.OrderStatus
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrderStatusContext _context;

        public EditModel(AWO_Orders.Data.OrderStatusContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderStatusModel OrderStatusModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var values = Enum.GetValues(typeof(OrderBaseStatusEnum)).Cast<OrderBaseStatusEnum>();

            ViewData["BaseStatus"] = new SelectList(values.Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            OrderStatusModel = await _context.OrderStatus
                .Include(o => o.Employee).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderStatusModel == null)
            {
                return NotFound();
            }
           ViewData["ChangedBy"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
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

            SetBaseProbertiesOnPost(OrderStatusModel);

            _context.Attach(OrderStatusModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderStatusModelExists(OrderStatusModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool OrderStatusModelExists(int id)
        {
            return _context.OrderStatus.Any(e => e.Id == id);
        }
    }
}
