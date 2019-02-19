using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace WebSite404
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new WebClient();
            var result = client.DownloadString("https://www.lucomputers.com");
            if (result.IndexOf("Go to Home") != -1)
            {
                try
                {
                    MailMessage mm = new MailMessage();
                    mm.Body = "Website is error.";

                    mm.BodyEncoding = System.Text.Encoding.Default;
                    mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    mm.From = new MailAddress("sales@lucomputers.com", "WuTH");//"xiaowu021@126.com", "Qiozi");
                    mm.IsBodyHtml = true;
                    mm.To.Add(new MailAddress("terryeah@gmail.com"));

                    mm.To.Add(new MailAddress("wu.th@qq.com"));
                    mm.Priority = MailPriority.Normal;
                    mm.Sender = new MailAddress("sales@lucomputers.com");// ("xiaowu021@126.com");
                    mm.Subject = "lucomputers.com Website is error." + DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                    mm.SubjectEncoding = System.Text.Encoding.Default;
                    SmtpClient smtpClient = new SmtpClient("sales@lucomputers.com");// new SmtpClient("xiaowu021@126.com");
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = false;
                    smtpClient.Port = 25;
                    smtpClient.Host = "p3smtpout.secureserver.net";// Config.mailServer;// "smtp.126.com";// 
                    smtpClient.Credentials = new System.Net.NetworkCredential("sales@lucomputers.com", "5calls2day");
                    // new System.Net.NetworkCredential("xiaowu021@126.com", "1234qwer");
                    smtpClient.Send(mm);
                    mm.Dispose();


                }
                catch (Exception ex) { }
            }
        }
    }
}
