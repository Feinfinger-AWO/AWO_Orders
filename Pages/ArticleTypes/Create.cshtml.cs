using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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