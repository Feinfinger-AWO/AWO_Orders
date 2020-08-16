using AWO_Orders.Interface;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AWO_Orders.Models
{
    public class Mailer : IMailer
    {
        private readonly SmtpSettings _smtpSettings;
        private readonly IWebHostEnvironment _env;
        private readonly string defaultSubject;
        private Exception lastError;

        public Mailer(IOptions<SmtpSettings> smtpSettings, IWebHostEnvironment env)
        {
            _smtpSettings = smtpSettings.Value;
            _env = env;
            defaultSubject = smtpSettings.Value.Subject;
        }


        public async Task SendPdfAsync(string email, string subject, string body,byte[]pdf,string pdfName)
        {
            var multipart = new Multipart("mixed");
            multipart.Add(new TextPart("html") { Text = body });
            var stream = new MemoryStream(pdf);

            var attachment = new MimePart("application", "pdf")
            {
                Content = new MimeContent(stream, ContentEncoding.Default),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = pdfName
            };

            multipart.Add(attachment);
            await SendPdfAsync(email, subject, null, multipart);
        }

        public async Task SendPdfAsync(string email, string subject, string body,Multipart multiBody = null)
        {
            LastError = null;
            try
            {
                var message = new MimeMessage();  
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));

                foreach (var mail in email.Trim(';').Split(new char[] { ';' }))
                {
                    message.To.Add(new MailboxAddress(mail));
                }
                
                message.Subject = subject ?? defaultSubject;

                if (multiBody == null)
                {
                    message.Body = new TextPart("html")
                    {
                        Text = body
                    };
                }
                else
                {
                    message.Body = multiBody;
                }

                using (var client = new SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    if (_env.IsDevelopment())
                    {
                        await client.ConnectAsync(_smtpSettings.Server, _smtpSettings.Port, true);
                    }
                    else
                    {
                        await client.ConnectAsync(_smtpSettings.Server);
                    }

                    await client.AuthenticateAsync(_smtpSettings.Username, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }catch(Exception e)
            {
                LastError = e;
            }
        }

        public Exception LastError { get => lastError; set => lastError = value; }
    }
}
