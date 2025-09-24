using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Holstentor.Data;
//using Holstentor.Models;

namespace Holstentor.Services
{
    // This class is used by the application to send Email and SMS
    // when you turn on two-factor authentication in ASP.NET Identity.
    // For more details see this link https://go.microsoft.com/fwlink/?LinkID=532713
    public class AuthMessageSender : IEmailSender, ISmsSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            //ApplicationDbContext db = new ApplicationDbContext();
            //var qservice = db.Tbl_Einstellungen.FirstOrDefault();
            //ApplicationUser user = new ApplicationUser();
            //var useremail = user.Email;
            MailMessage msg = new MailMessage();
            msg.Body = message;
            msg.BodyEncoding = Encoding.UTF8;
            msg.IsBodyHtml = true;
            msg.From = new MailAddress("restaurantamholstentor@gmail.com", "Restaurant am Holstentor", Encoding.UTF8);
            msg.Priority = MailPriority.Normal;
            msg.Sender = msg.From;
            msg.Subject = subject;
            msg.SubjectEncoding = Encoding.UTF8;
            msg.To.Add(new MailAddress(email, email, Encoding.UTF8));

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("restaurantamholstentor@gmail.com", "+H0123456789");

            smtp.Send(msg);

            return Task.FromResult(0);
        }

        public Task SendEmailAsyncUser(string name, string email, string message)
        {
            return Task.FromResult(0);
        }
        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }
}