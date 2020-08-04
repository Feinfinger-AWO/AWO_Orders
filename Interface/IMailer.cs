using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Interface
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);

        public Exception  LastError { get; set; }
    }
}
