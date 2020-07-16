using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWO_Orders.Pages.OrderPositions
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrderPositionContext _context;

        /// <summary>
        /// Gibt die nächste Positionsnummer zurück
        /// </summary>
        /// <returns></returns>
        private int GetPositionNumber()
        {
            var positions = from p in _context.OrderPositions where p.OrderId == OrderId select p;

            if (!positions.Any()) return 1;

            var number = positions.Max(a => a.Number);
            return number + 1;
        }

        public CreateModel(AWO_Orders.Data.OrderPositionContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int id)
        {
            OrderId = id;
            Number = GetPositionNumber();
            ViewData["ArticleTypeId"] = new SelectList(_context.Set<ArticleTypeModel>(), "Id", "Ident");
            ViewData["OrderId"] = OrderId;
            return Page();
        }

        [BindProperty]
        public OrderPositionModel OrderPosition { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(OrderPosition);

            _context.OrderPositions.Add(OrderPosition);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new { id = OrderPosition.OrderId });
        }

        public int OrderId { get; set; }

        public int Number { get; set; }
    }
}
