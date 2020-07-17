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

namespace AWO_Orders.Pages.OrderPositions
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        public EditModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        [BindProperty]
        public OrderPositionModel OrderPosition { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OrderPosition = await _context.OrderPositions
                .Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(o => o.Order).FirstOrDefaultAsync(m => m.Id == id);

            if (OrderPosition == null)
            {
                return NotFound();
            }

            OrderId = OrderPosition.OrderId;
            Number = OrderPosition.Number;

            ViewData["ArticleTypeId"] = new SelectList(_context.Set<ArticleTypeModel>(), "Id", "Ident");           
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(decimal Count)
        {
            OrderPosition.Count = Count;
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // <input asp-for="OrderPosition.Count" class="form-control" />
            SetBaseProbertiesOnPost(OrderPosition);
            _context.Attach(OrderPosition).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderPositionExists(OrderPosition.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new { id = OrderPosition.OrderId });
        }

        private bool OrderPositionExists(int id)
        {
            return _context.OrderPositions.Any(e => e.Id == id);
        }

        public int OrderId { get; set; }

        public int Number { get; set; }
    }
}
