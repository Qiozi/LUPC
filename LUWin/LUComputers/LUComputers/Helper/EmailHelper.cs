using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace LUComputers.Helper
{
    public class EmailHelper
    {
        public EmailHelper() { }

        public bool SendToEmail(string to_email, string sendBody, string title)
        {
            try
            {
                MailMessage mm = new MailMessage();
                mm.Body = sendBody;

                mm.BodyEncoding = System.Text.Encoding.Default;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.From = new MailAddress("sales@lucomputers.com", "WuTH");//"xiaowu021@126.com", "Qiozi");
                mm.IsBodyHtml = true;
                mm.To.Add(new MailAddress("terryeah@gmail.com"));
                if (to_email != "terryeah@gmail.com")
                    mm.To.Add(new MailAddress(to_email));
                mm.Priority = MailPriority.Normal;
                mm.Sender = new MailAddress("sales@lucomputers.com");// ("xiaowu021@126.com");
                mm.Subject = title;
                mm.SubjectEncoding = System.Text.Encoding.Default;
                SmtpClient client = new SmtpClient("sales@lucomputers.com");// new SmtpClient("xiaowu021@126.com");
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.EnableSsl = false;
                client.Port = 25;
                client.Host = "p3smtpout.secureserver.net";// Config.mailServer;// "smtp.126.com";// 
                client.Credentials = new System.Net.NetworkCredential("sales@lucomputers.com", "5calls2day");
                // new System.Net.NetworkCredential("xiaowu021@126.com", "1234qwer");
                client.Send(mm);
                mm.Dispose();
                return true;

            }
            catch { return false; }
        }
    }
}
