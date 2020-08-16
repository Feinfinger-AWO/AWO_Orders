using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using Microsoft.VisualBasic;
using System.IO;
using AWO_Orders.Components;
using System.Text;

namespace AWO_Orders.Pages.ExternalOrders
{
    public class DetailsModel : PageModel
    {
        private readonly AWO_Orders.Data.ExternalOrdersContext _context;
        private readonly AWO_Orders.Data.OrdersContext _contextOrders;
        private readonly AWO_Orders.Data.OrderPositionContext _contextPositions;



        public DetailsModel(AWO_Orders.Data.ExternalOrdersContext context,
            OrdersContext contextOrders,
            OrderPositionContext contextPositions)
        {
            _context = context;
            _contextOrders = contextOrders;
            _contextPositions = contextPositions;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ExternalOrderModel = await _context.ExternalOrders
                .Include(e => e.Employee)
                .Include(e => e.Manager).FirstOrDefaultAsync(m => m.Id == id);

            DocumentExists = ExternalOrderModel.Document != null;

            var positions = from p in _contextPositions.OrderPositions where p.ExternId == id select p;

            OrderPosition = positions.Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(p => p.Order).ToList();

            if (ExternalOrderModel == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPostPDFShow(int? id)
        {
            if (!id.HasValue) return null;

            ExternalOrderModel = _context.ExternalOrders.FirstOrDefault(m => m.Id == id);

            var stream = new MemoryStream(ExternalOrderModel.Document);
            return File(stream, "application/pdf", "ExterneBestellung_" + ExternalOrderModel.Id + ".pdf");
        }

        public IActionResult OnPostPDF(int? id, string[] notes, int? idMail)
        {
            if (idMail.HasValue)
            {
                if(_context.ExternalOrders.First(e=>e.Id == idMail).Document == null) 
                {
                    return RedirectToPage("/Info", new { subject = "Es ist kein Dokument vorhanden!", nextPage = "/ExternalOrders/Details", paramId =  idMail  });
                }
                return RedirectToPage("/SendMail/SelectMailPDF", new { id = idMail.Value });
            }

            Print = true;

            ExternalOrderModel = _context.ExternalOrders
                .Include(e => e.Employee)
                .Include(e => e.Manager).FirstOrDefault(m => m.Id == id);

            var positions = from p in _contextPositions.OrderPositions where p.ExternId == id select p;

            OrderPosition = positions.Include(o => o.ArticleType)
                .Include(o => o.Employee)
                .Include(p => p.Order).ToList();

            var builder = new StringBuilder();
            builder.Append(notes);
            ExternalOrderModel.Notes = notes[0];

            RazorPageAsPdf pdf = new RazorPageAsPdf(this)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
            };

            var build = pdf.BuildFile(this.PageContext);
            ExternalOrderModel.Document = build.Result;

            _context.SaveChanges();
           
            var stream = new MemoryStream(ExternalOrderModel.Document);
            return File(stream, "application/pdf", "ExterneBestellung_"+ ExternalOrderModel.Id + ".pdf");
        }

        public ExternalOrderModel ExternalOrderModel { get; set; }

        public IList<OrderPositionModel> OrderPosition { get; set; }

        public bool Print { get; set; }

        public bool DocumentExists { get; set; }
    }
}
