using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.ArticleTypes
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.ArticleTypeContext _context;

        public DetailsModel(AWO_Orders.Data.ArticleTypeContext context)
        {
            _context = context;
        }

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
    }
}