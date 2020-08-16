using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AWO_Orders.Data;
using AWO_Orders.Models;
using System.Text;
using AWO_Orders.Interface;

namespace AWO_Orders.Pages.SendMail
{
    public class SelectMailPDFModel : PageModel
    {
        private readonly AWO_Orders.Data.EmployeeContext _context;
        private readonly IMailer _mailer;
        private readonly ExternalOrdersContext _ordersContext;

        public SelectMailPDFModel(AWO_Orders.Data.EmployeeContext context, IMailer mailer, ExternalOrdersContext ordersContext)
        {
            _context = context;
            _mailer = mailer;
            _ordersContext = ordersContext;
        }

        public IList<EmployeeModel> EmployeeModel { get;set; }

        public async Task OnGetAsync(int? id)
        {
            if(id.HasValue)
                PDFId = id.Value;

            EmployeeModel = await _context.Employees
                .Include(e => e.Location).ToListAsync();
        }

        public IActionResult OnPost(int? Pdfid, IList<EmployeeModel> items)
        {
            if (Pdfid.HasValue)
            {
                var ok = SendPDFMail(Pdfid.Value, items.Where(i => i.Selected).ToList());
                return RedirectToPage("/Info", new { subject = "Dokument wurde versand!", nextPage = "/ExternalOrders/Index" });
            }
            return null;
        }

        private bool SendPDFMail(int externId, IList<EmployeeModel> items)
        {
            var mail = "";

            foreach (var employee in items)
            {
                if (!string.IsNullOrWhiteSpace(employee.EMail))
                {
                    mail += ";" + employee.EMail;
                }
            }

            if (!String.IsNullOrWhiteSpace(mail))
            {

                var order = _ordersContext.ExternalOrders.Single(o => o.Id == externId);
                //todo: Text auslagern
                _mailer.SendPdfAsync(mail, "Dokument für Bestellung #"+ order.Id.ToString(), "",order.Document, "ExterneBestellung_" + order.Id + ".pdf");

                if (_mailer.LastError != null)
                {
                    return false;
                }
            }
            return true;
        }
        public int PDFId { get; set; }
    }
}
