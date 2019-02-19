using System.Net.Mail;

/// <summary>
/// Summary description for EmailHelper
/// </summary>
public class EmailHelper
{
	public EmailHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static bool SendTo(string to_email
        , string sendBody
        , string subject 
        )
    {
       
        if (Config.isLocalhost)
        {
            MailMessage mm = new MailMessage();
            mm.Body = sendBody;
            //mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
            mm.BodyEncoding = System.Text.Encoding.Default;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            mm.From = new MailAddress("xiaowu021@126.com", "Qiozi");
            mm.IsBodyHtml = true;
            mm.To.Add(new MailAddress("wu.th@qq.com"));
            mm.To.Add(new MailAddress(to_email));
            mm.Priority = MailPriority.Normal;
            mm.ReplyTo = new MailAddress("xiaowu021@126.com");//new MailAddress("sales@lucomputers.com");
            mm.Sender = new MailAddress("xiaowu021@126.com");// new MailAddress("sales@lucomputers.com");

            mm.Subject = subject;
            //if (File.Exists(AttachFilename))
            //    mm.Attachments.Add(new System.Net.Mail.Attachment(AttachFilename));

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
        else
        {
            MailMessage mm = new MailMessage();
            mm.Body = sendBody;
            mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
            mm.Bcc.Add(new MailAddress("sales@lucomputers.com"));
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
            client = new SmtpClient(Config.mailUserName);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.EnableSsl = false;
            client.Host = Config.mailServer;
            client.Credentials = new System.Net.NetworkCredential(Config.mailUserName, Config.mailPassword);
            client.Send(mm);
            mm.Dispose();
        }
       

        return true;
    }
}