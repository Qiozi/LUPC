using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using jmail;
using System.Net.Mail;

public partial class Q_Admin_change_currency_converter : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Util.GetStringSafeFromQueryString(Page, "cmd") == "qiozi@msn.com")
            {
                string error = "";
                decimal new_currency;
                decimal.TryParse(Util.GetStringSafeFromQueryString(Page, "currency"), out new_currency);
                SendEmail(new_currency, ref error);
            }
            else
            {
                InitialDatabase();
            }
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        BindCurrencyConverter();
    }

    private void BindCurrencyConverter()
    {
        DataTable dt = Config.ExecuteDataTable("Select id,currency_cad, currency_usd, case when is_auto=1 then 'auto' else 'input' end as is_auto, date_format(regdate, '%b/%d/%Y') regdate from tb_currency_convert order by id desc limit 0,5");
        this.rpt_currency_converter.DataSource = dt;
        this.rpt_currency_converter.DataBind();

        // 
        //if (dt.Rows.Count > 0)
        this.lbl_currency_converter.Text = ConvertPrice.CurrentCurrencyConverter.ToString();// dt.Rows[0]["currency_usd"].ToString();
    }
    protected void rpt_currency_converter_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Set":

                Config.ExecuteNonQuery("Update tb_currency_convert Set is_current=0; Update tb_currency_convert Set is_current=1,regdate=now() WHere id='" + e.CommandArgument.ToString() + "'");
                DataTable dt = Config.ExecuteDataTable("Select currency_usd from tb_currency_convert Where is_current=1 order by regdate desc limit 0,1");
                if (dt.Rows.Count > 0)
                {
                    decimal currency;
                    decimal.TryParse(dt.Rows[0][0].ToString(), out currency);
                    ConvertPrice.CurrentCurrencyConverter = currency;
                    BindCurrencyConverter();
                    string error="";
                    SendEmail(currency, ref error);
                    CH.Alert(error, this.Literal1);
                }
                break;
        }
        //this.btn_submit.Text = e.CommandArgument.ToString();
    }



    public bool SendEmail(decimal new_currency, ref string error)
    {
        // MessageClass mc = new MessageClass();
        // mc.ContentType = "text/html";
        // mc.Body = string.Format("1 CAD = {0} USD <br/> 1 USD = {1} CAD", new_currency.ToString(), (1M / new_currency).ToString("#0.0000"));// ;
        // mc.Logging = true;
        // mc.Silent = true;
        // mc.ReplyTo = "sales@lucomputers.com";
        // mc.MailServerUserName = Config.mailUserName;
        // mc.MailServerPassWord = Config.mailPassword;
        // mc.From = "sales@lucomputers.com";
        // mc.FromName = "LU COMPUTERS";
        // //mc.Subject = "LU COMPUTERS: INVOICE (#" + InvoiceCode + ") THANK YOU FOR YOUR BUSINESS!";
        // mc.Subject = "LU WEB CURRENCY CONVERTER Change to "+ new_currency.ToString();

        //// mc.AddRecipient("sales@lucomputers.com";, to_mail, null);
        // mc.AddRecipientBCC("wu.th@qq.com",null);
        // mc.AddRecipient("sales@lucomputers.com", "sales@lucomputers.com", null);

        // bool b = mc.Send(Config.mailServer, false);
        // if (!b)
        // {
        //     error = mc.ErrorMessage;
        // }
        // return b;

        MailMessage mm = new MailMessage();
        mm.Body = string.Format("1 CAD = {0} USD <br/> 1 USD = {1} CAD", new_currency.ToString(), (1M / new_currency).ToString("#0.0000")); ;

        mm.Bcc.Add(new MailAddress("wu.th@qq.com"));
        mm.BodyEncoding = System.Text.Encoding.Default;
        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        mm.From = new MailAddress("sales@lucomputers.com", "LU Computers");
        mm.IsBodyHtml = true;

        mm.To.Add(new MailAddress("sales@lucomputers.com"));

        mm.Priority = MailPriority.Normal;
        mm.ReplyTo = new MailAddress("sales@lucomputers.com");
        mm.Sender = new MailAddress("sales@lucomputers.com");

        mm.Subject = "LU WEB CURRENCY CONVERTER Change to " + new_currency.ToString();

        mm.SubjectEncoding = System.Text.Encoding.Default;

        SmtpClient client = new SmtpClient(Config.mailUserName);
        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        client.UseDefaultCredentials = false;
        client.EnableSsl = false;
        client.Host = Config.mailServer;
        client.Credentials = new System.Net.NetworkCredential(Config.mailUserName, Config.mailPassword);

        client.Send(mm);
        mm.Dispose();

        return true;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        decimal new_currency;
        decimal.TryParse(this.txt_currency.Text, out new_currency);
        if (new_currency > 0M)
        {
            Config.ExecuteNonQuery(@"Update tb_currency_convert Set is_current=0;

    insert into tb_currency_convert
	(currency_cad, currency_usd, is_current, is_auto,regdate
	)
	values
	(1, '" + new_currency.ToString()+@"', 1, 0, now())");
            
            DataTable dt = Config.ExecuteDataTable("Select currency_usd from tb_currency_convert Where is_current=1 order by regdate desc limit 0,1");
            if (dt.Rows.Count > 0)
            {
                decimal currency;
                decimal.TryParse(dt.Rows[0][0].ToString(), out currency);
                ConvertPrice.CurrentCurrencyConverter = currency;
                BindCurrencyConverter();
                string error = "";
                SendEmail(currency, ref error);
                if (error != "")
                    CH.Alert(error, this.Literal1);
            }
        }
    }
}
