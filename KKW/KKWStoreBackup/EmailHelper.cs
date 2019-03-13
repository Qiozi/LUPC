using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace KKWStoreBackup
{
    public class EmailHelper
    {
        public EmailHelper() { }
        public static void Send(string to_email
        , string filename
        , string subject
        )
        {
            MailMessage mm = new MailMessage();
            mm.Body = "备份数据，见附件";
            // mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
            mm.BodyEncoding = System.Text.Encoding.Default;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.From = new MailAddress("ali20070909@aliyun.com", "数据备份");
            mm.IsBodyHtml = true;
            mm.To.Add(new MailAddress(to_email));
            mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
            mm.Priority = MailPriority.Normal;
            mm.ReplyTo = new MailAddress("ali20070909@aliyun.com");
            mm.Sender = new MailAddress("ali20070909@aliyun.com");
            mm.Subject = subject;
            mm.SubjectEncoding = System.Text.Encoding.Default;
            mm.Attachments.Add(new System.Net.Mail.Attachment(filename));

            SmtpClient client;
            client = new SmtpClient("ali20070909@aliyun.com");
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = true;
            client.EnableSsl = false;
            client.Host = "smtp.aliyun.com";
            client.Credentials = new System.Net.NetworkCredential("ali20070909@aliyun.com", "1234qwer");
            client.Send(mm);           
            mm.Dispose();
        }
    }
}
