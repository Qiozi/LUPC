using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace RemoteAutoBackup
{
    public class EmailHelper
    {
        public EmailHelper() { }
        public static void Send(string to_email
        , string sendBody
        , string subject
        )
        {
            MailMessage mm = new MailMessage();
            mm.Body = sendBody;
            // mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
            mm.BodyEncoding = System.Text.Encoding.Default;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.From = new MailAddress("sales@lucomputers.com", "LU Computers");
            mm.IsBodyHtml = true;
            mm.To.Add(new MailAddress(to_email));
            mm.Priority = MailPriority.Normal;
            mm.ReplyTo = new MailAddress("sales@lucomputers.com");
            mm.Sender = new MailAddress("sales@lucomputers.com");
            mm.Subject = subject;
            mm.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient client;
            client = new SmtpClient("sales@lucomputers.com");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = "p3smtpout.secureserver.net";
            client.Credentials = new System.Net.NetworkCredential("sales@lucomputers.com", "5calls2day");
            client.Send(mm);
            mm.Dispose();
        }
    }
}
