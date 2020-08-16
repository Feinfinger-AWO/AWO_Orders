using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Interface
{
    public interface IMailer
    {
        Task SendPdfAsync(string email, string subject, string body, Multipart mBody = null);
        Task SendPdfAsync(string email, string subject, string body, byte[] pdf, string pdfName);

        public Exception  LastError { get; set; }
    }
}
