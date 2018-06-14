using System.Net.Mail;
using System.Text;

namespace NIHApp.Domain.Entities
{
    public class EmailMessage : MailMessage
    {
        public static EmailMessage CreateInstance(MailAddress fromEmail, string toEmail, string subject, string body, bool isHtml)
        {
            var msg = new EmailMessage { From = fromEmail };
            msg.To.Add(toEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = isHtml;

            msg.BodyEncoding = Encoding.UTF8;
            msg.Headers.Add("Content-Type", isHtml ? "text/html" : "text/plain");
            msg.Body = body;
            return msg;
        }

        public static EmailMessage CreateInstance(MailAddress fromEmail, string toEmail, string ccEmail, string subject, string body, bool isHtml)
        {
            var msg = new EmailMessage { From = fromEmail };
            msg.To.Add(toEmail);
            if (ccEmail != string.Empty)
                msg.CC.Add(ccEmail);
            msg.Subject = subject;
            msg.IsBodyHtml = isHtml;

            msg.BodyEncoding = Encoding.UTF8;
            msg.Headers.Add("Content-Type", isHtml ? "text/html" : "text/plain");
            msg.Body = body;
            return msg;
        }

        public static EmailMessage CreateInstance(MailAddress fromEmail, string[] toEmails, string subject, string body, bool isHtml)
        {
            var msg = new EmailMessage { From = fromEmail };
            foreach (string toEmail in toEmails)
            {
                if (toEmail.Trim() != string.Empty)
                    msg.To.Add(toEmail);
            }
            msg.Subject = subject;
            msg.IsBodyHtml = isHtml;

            msg.BodyEncoding = Encoding.UTF8;
            msg.Headers.Add("Content-Type", isHtml ? "text/html" : "text/plain");
            msg.Body = body;
            return msg;
        }

        public void Attach(string attPath)
        {
            Attachments.Add(new Attachment(attPath));
        }

    }
}
