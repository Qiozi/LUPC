using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class EmailHelper
    {
        public static string EmailHost = "p3smtpout.secureserver.net";
        public static string EmailUsername = "sales@lucomputers.com";
        public static string EmailPwd = "5calls2day";

        public static void send(string body
            , string subject
            , string ReqEmailTo)
        {
            MailMessage mm = new MailMessage();
            mm.Body = body;// "this is a html<span style='color:red;'>Hello</span>";
                           //mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
            mm.BodyEncoding = System.Text.Encoding.Default;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.From = new MailAddress("sales@lucomputers.com", "LU Computers");
            mm.IsBodyHtml = true;
            //mm.To.Add(new MailAddress("wu.th@qq.com"));
            mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
            if (ReqEmailTo != "wu.th@qq.com")
                mm.To.Add(new MailAddress(ReqEmailTo));
            mm.Priority = MailPriority.Normal;
            //mm.ReplyTo = new MailAddress(EmailUsername);
            mm.Sender = new MailAddress(EmailUsername);// new MailAddress("sales@lucomputers.com");

            mm.Subject = subject;

            //if (File.Exists(AttachFilename))
            //    mm.Attachments.Add(new System.Net.Mail.Attachment(AttachFilename));

            mm.SubjectEncoding = System.Text.Encoding.Default;

            SmtpClient client = new SmtpClient(EmailUsername);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Port = 25;
            client.Host = EmailHost;// Models.Config.mailServer;
                                    //client.Credentials = new System.Net.NetworkCredential(Models.Config.mailUserName, Models.Config.mailPassword);
            client.Credentials = new System.Net.NetworkCredential(EmailUsername, EmailPwd);
            client.Send(mm);
            mm.Dispose();
            //return true;
        }
    }
}
