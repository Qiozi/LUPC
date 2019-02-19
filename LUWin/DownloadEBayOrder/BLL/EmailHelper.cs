using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Data;

namespace DownloadEBayOrder
{
    public class EmailHelper
    {
        public EmailHelper() { }
        public static void Send(string to_email
        , string sendBody
        , string subject)
        {
            try
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
            catch (Exception ex)
            {
                Form1.log.WriteErrorLog(ex);
            }
        }

        public static void SendCheckDupl()
        {
            DataTable dt = Config.ExecuteDataTable(@"select count(c) c from (select distinct buyitnowprice, title, count(luc_sku) c from tb_ebay_selling where luc_sku>0 group by buyitnowprice, title ) t where c >1

union  all

select count(c) c from (select distinct buyitnowprice, title, count(luc_sku) c from tb_ebay_selling where sys_sku>0 group by buyitnowprice, title ) t where c >1

union  all

select count(c) c from (select distinct buyitnowprice, title, count(luc_sku) c from tb_ebay_selling group by buyitnowprice, title ) t where c >1 
");
            string body = "Check eBay Dupl Part(" + dt.Rows[0][0].ToString() + "), Sys(" + dt.Rows[1][0].ToString() + "), All(" + dt.Rows[2][0].ToString() + ")";
            MailMessage mm = new MailMessage();
            mm.Body = body;
            // mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
            mm.BodyEncoding = System.Text.Encoding.Default;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.From = new MailAddress("sales@lucomputers.com", "LU Computers");
            mm.IsBodyHtml = true;
            mm.To.Add(new MailAddress("terryeah@gmail.com"));
            mm.Priority = MailPriority.Normal;
            mm.ReplyTo = new MailAddress("sales@lucomputers.com");
            mm.Sender = new MailAddress("sales@lucomputers.com");
            mm.Subject = body;
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
