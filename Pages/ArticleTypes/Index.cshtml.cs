using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;

namespace AWO_Orders.Pages.ArticleTypes
{
    public class IndexModel : PageModel
    {
        private readonly AWO_Orders.Data.ArticleTypeContext _context;

        public IndexModel(AWO_Orders.Data.ArticleTypeContext context)
        {
            _context = context;
        }

        public IList<ArticleTypeModel> ArticleTypeModel { get;set; }

        public async Task OnGetAsync()
        {
            ArticleTypeModel = await _context.ArticleTypes.ToListAsync();
        }
    }
}
