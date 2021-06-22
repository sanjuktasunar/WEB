using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Web.Services.Services
{
    public interface IEmailService
    {
        bool SendEmail(string to, string subject, string message);
    }

    public class EmailService:IEmailService
    {
        public bool SendEmail(string toEmail,string subject,string message)
        {
            try
            {
                string senderEmail = "bishwokarmatrading73@gmail.com";
                string password = "company2073";
                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient client = new SmtpClient();
                client.Host = "relay-hosting.secureserver.net";
                client.EnableSsl = true;
                client.Timeout = 100000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(senderEmail, password);
                MailMessage mailMessage = new MailMessage(senderEmail,toEmail,subject,message);
                mailMessage.IsBodyHtml = true;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mailMessage);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
