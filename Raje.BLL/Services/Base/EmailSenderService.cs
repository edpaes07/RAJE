using Raje.DL.Services.BLL.Base;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Raje.BLL.Services.Base
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly SMTPOptions _smtpOptions;

        public EmailSenderService(IOptions<SMTPOptions> smtpOptions)
        {
            _smtpOptions = smtpOptions.Value;
        }

        public async Task SendAsync(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_smtpOptions.UsernameEmail)
                };

                mail.To.Add(new MailAddress(email, _smtpOptions.From));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using SmtpClient smtp = new SmtpClient(_smtpOptions.PrimaryDomain, _smtpOptions.PrimaryPort)
                {
                    Credentials = new NetworkCredential(_smtpOptions.UsernameEmail, _smtpOptions.UsernamePassword),
                    EnableSsl = Convert.ToBoolean(_smtpOptions.EnableSSL)
                };
                await smtp.SendMailAsync(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}