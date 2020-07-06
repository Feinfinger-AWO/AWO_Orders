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


        public async Task OnGetAsync(string searchString)
        {
            FilterText = searchString;

            ArticleTypeModel = await _context.ArticleTypes.ToListAsync();

            if (!String.IsNullOrWhiteSpace(FilterText))
            {
                ArticleTypeModel = ArticleTypeModel.Where(a => a.Ident.ToLower().Contains(FilterText.ToLower())).ToList();
            }

            ArticleTypeModel = ArticleTypeModel.Select(a => { a.Employee = _context.Employees.SingleOrDefault(b => b.Id == a.ChangedBy); return a; }).ToList();
            ArticleTypeModel = ArticleTypeModel.OrderBy(a => a.Ident).ToList();
        }


        public IList<ArticleTypeModel> ArticleTypeModel { get; set; }

        /// <summary>
        /// Gets or sets the text to filter by ident
        /// </summary>
        public string FilterText { get; set; }
    }
}
