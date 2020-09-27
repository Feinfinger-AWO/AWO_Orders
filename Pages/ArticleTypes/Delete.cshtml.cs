using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.ArticleTypes
{
    public class DeleteModel : PageModel
    {
        private readonly AWO_Orders.Data.ArticleTypeContext _context;

        public DeleteModel(AWO_Orders.Data.ArticleTypeContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ArticleTypeModel ArticleTypeModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticleTypeModel = await _context.ArticleTypes.FirstOrDefaultAsync(m => m.Id == id);

            if (ArticleTypeModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ArticleTypeModel = await _context.ArticleTypes.FindAsync(id);

            if (ArticleTypeModel != null)
            {
                _context.ArticleTypes.Remove(ArticleTypeModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}