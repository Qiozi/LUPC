using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;


public partial class cmds_sentMail : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                EmailHelper.send(TempleteString.Replace("wu.th@qq.com-bodyhtml", ReqHTMLBody), "LU COMPUTER SYSTEM.", ReqEmailTo);
                Response.Write("OK");
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            Response.End();
            //MailMessage mm = new MailMessage();
            //mm.Body = TempleteString.Replace("wu.th@qq.com-bodyhtml", ReqHTMLBody);// "this is a html<span style='color:red;'>Hello</span>";
            ////mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
            //mm.BodyEncoding = System.Text.Encoding.Default;
            //mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
            //mm.From = new MailAddress("xiaowu021@126.com", "Qiozi");
            //mm.IsBodyHtml = true;
            //mm.To.Add(new MailAddress("wu.th@qq.com"));
            //if (ReqEmailTo != "wu.th@qq.com")
            //    mm.To.Add(new MailAddress(ReqEmailTo));
            //mm.Priority = MailPriority.Normal;
            ////mm.ReplyTo = new MailAddress("xiaowu021@126.com");//new MailAddress("sales@lucomputers.com");
            //mm.Sender = new MailAddress("xiaowu021@126.com");// new MailAddress("sales@lucomputers.com");

            //mm.Subject = "test";

            ////if (File.Exists(AttachFilename))
            ////    mm.Attachments.Add(new System.Net.Mail.Attachment(AttachFilename));

            //mm.SubjectEncoding = System.Text.Encoding.Default;

            //SmtpClient client = new SmtpClient("xiaowu021@126.com");
            //client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.UseDefaultCredentials = false;
            //client.EnableSsl = false;
            //client.Port = 25;
            //client.Host = "smtp.126.com";// Models.Config.mailServer;
            ////client.Credentials = new System.Net.NetworkCredential(Models.Config.mailUserName, Models.Config.mailPassword);
            //client.Credentials = new System.Net.NetworkCredential("xiaowu021@126.com", "1234qwer");
            //client.Send(mm);
            //mm.Dispose();
            //return true;
        }
    }

    string ReqEmailTo
    {
        get { return Util.GetStringSafeFromString(Page, "email"); }
    }

    string ReqHTMLBody
    {
        get { return Util.GetStringSafeFromString(Page, "htmlCont").Replace("&lt;", "<").Replace("&gt;", ">"); }
    }

    #region templete



    string TempleteString = @"
  
     wu.th@qq.com-bodyhtml

";
    #endregion
}