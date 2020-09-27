using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.OrderStatus
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.OrderStatusContext _context;

        public CreateModel(AWO_Orders.Data.OrderStatusContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var values = Enum.GetValues(typeof(OrderBaseStatusEnum)).Cast<OrderBaseStatusEnum>();

            ViewData["BaseStatus"] = new SelectList(values.Select(v => new SelectListItem
            {
                Text = v.ToString(),
                Value = ((int)v).ToString()
            }).ToList(), "Value", "Text");

            return Page();
        }

        [BindProperty]
        public OrderStatusModel OrderStatusModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(OrderStatusModel);

            _context.OrderStatus.Add(OrderStatusModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}