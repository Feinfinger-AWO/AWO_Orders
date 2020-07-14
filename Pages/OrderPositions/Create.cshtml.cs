using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.OrderPositions
{
    public class CreateModel : PageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        public CreateModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["ArticleTypeId"] = new SelectList(_context.Set<ArticleTypeModel>(), "Id", "Ident");
        ViewData["ChangedBy"] = new SelectList(_context.Set<EmployeeModel>(), "Id", "EMail");
        ViewData["OrderId"] = new SelectList(_context.Set<OrderModel>(), "Id", "Number");
            return Page();
        }

        [BindProperty]
        public OrderPosition OrderPosition { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.OrderPositions.Add(OrderPosition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
