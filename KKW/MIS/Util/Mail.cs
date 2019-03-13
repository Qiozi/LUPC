using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;
using System.IO;

namespace Util
{
    public class Mail
    {
        public static bool SendError(string AttachFilename)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.Body = "Hello Qiozi.";
                //mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
                mm.BodyEncoding = System.Text.Encoding.Default;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.From = new MailAddress("xiaowu021@126.com", "LU Computers App");
                mm.IsBodyHtml = true;
                mm.To.Add(new MailAddress("wu.th@qq.com"));
                mm.Priority = MailPriority.Normal;
                // mm.ReplyTo = new MailAddress();//new MailAddress("sales@lucomputers.com");
                // mm.ReplyToList = new MailAddressCollection().Add(new MailAddress("xiaowu021@126.com"));
                mm.Sender = new MailAddress("xiaowu021@126.com");// new MailAddress("sales@lucomputers.com");

                mm.Subject = "LU Offline App error info";

                if (File.Exists(AttachFilename))
                    mm.Attachments.Add(new System.Net.Mail.Attachment(AttachFilename));

                mm.SubjectEncoding = System.Text.Encoding.Default;

                SmtpClient client = new SmtpClient("xiaowu021@126.com");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Port = 25;
                client.Host = "smtp.126.com";// Models.Config.mailServer;
                //client.Credentials = new System.Net.NetworkCredential(Models.Config.mailUserName, Models.Config.mailPassword);
                client.Credentials = new System.Net.NetworkCredential("xiaowu021@126.com", "1234qwer");
                client.Send(mm);
                mm.Dispose();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
