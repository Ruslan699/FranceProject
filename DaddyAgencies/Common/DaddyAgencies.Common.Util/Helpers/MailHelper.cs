using System.Net;
using System.Net.Mail;

namespace DaddyAgencies.Common.Util.Helpers
{
    public static class MailHelper
    {
        public static void SendEmail(string to, string subject, string body)
        {
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("les.agency1@gmail.com", "12345@lesagency"),
                EnableSsl = true
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("les.agency1@gmail.com");
            mailMessage.To.Add(to);
            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = body;
            client.Send(mailMessage);
        }
    }
}
