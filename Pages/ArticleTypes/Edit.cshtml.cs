using AWO_Orders.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Pages.ArticleTypes
{
    public class EditModel : BasePageModel
    {
        private readonly AWO_Orders.Data.ArticleTypeContext _context;

        public EditModel(AWO_Orders.Data.ArticleTypeContext context)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            SetBaseProbertiesOnPost(ArticleTypeModel);

            _context.Attach(ArticleTypeModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleTypeModelExists(ArticleTypeModel.Id))
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

        private bool ArticleTypeModelExists(int id)
        {
            return _context.ArticleTypes.Any(e => e.Id == id);
        }
    }
}