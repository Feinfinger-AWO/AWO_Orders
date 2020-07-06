using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.ArticleTypes
{
    public class CreateModel : BasePageModel
    {
        private readonly AWO_Orders.Data.ArticleTypeContext _context;

        public CreateModel(AWO_Orders.Data.ArticleTypeContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public ArticleTypeModel ArticleTypeModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int Next)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(ArticleTypeModel);

            _context.ArticleTypes.Add(ArticleTypeModel);

            await _context.SaveChangesAsync();

            if (Next == 0)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("./Create");
            }
        }
    }
}
