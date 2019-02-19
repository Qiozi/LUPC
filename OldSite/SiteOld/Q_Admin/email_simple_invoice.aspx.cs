using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using jmail;
using System.Net.Mail;

public partial class Q_Admin_email_simple_invoice : OrderPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsSend)
            {
                string email = Email;
                string error = "";
                if (this.SendToCustomer(email, new OrderMailContent().SendContent(this.OrderCodeRequest.ToString(), this.txtNote.Text)
                    , false
                    , false
                    , ref error))
                {
                    //Response.Write("<script>parent.document.getElementById(\"send_msg\").innerHTML = \"A copy of your order was send to:&nbsp;&nbsp;"+ email +";\"</script>");
                    if (OrderCodeRequest.ToString().Length == 5)
                    {
                        Response.Clear();
                        Response.Write("<html><head></head><body><script>this.close();</script></body></html>");
                        Response.End();
                    }
                }
                else
                {
                    Response.Clear();
                    Response.Write("<script>alert(\""+error +"\");</script>");
                }
            }
            else
            {
                this.TextBox1.Text = Email;
                InvoiceCode = OrderCodeRequest.ToString();
                this.lb_content.Text = new OrderMailContent().SendContent(OrderCodeRequest.ToString());
                this.TextBox_email_subject.Text = "LU COMPUTERS ORDER(#" + InvoiceCode + ") CONFIRMATION THANK YOU FOR YOUR BUSINESS!";
            }
        }
    }

    /// <summary>
    /// Get Invoice Code on that send email to cutomer
    /// </summary>
    /// <param name="order_id"></param>
    /// <returns></returns>
    public string invoice_code(string order_id)
    {
        string id = order_id.Trim();
        int pre_two = Config.invoice_head;
        if (id.Length == 4)
        {
            return pre_two.ToString() + id;
        }
        if (id.Length == 5)
        {
            return (pre_two + int.Parse(order_id.Substring(0,1))).ToString() + id;
        }
        else
            throw new Exception("Order ID Error!");
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        string email = this.TextBox1.Text;
        string error = "";
        if (this.SendToCustomer(email, new OrderMailContent().SendContent(this.OrderCodeRequest.ToString(), this.txtNote.Text)
            , RadioButtonList1.SelectedValue == "0" ? false : true
            , RadioButtonList1.SelectedValue == "1" ? true : false
            , ref error))
        {
            CH.Alert("is OK", this.Literal1);

        }
        else
        {
            CH.Alert("error!" + error, this.Literal1);
        }
    }

    public bool AccDownPDF(int order_code, bool is_invoice)
    {
        OrderHelperModel[] ohm = OrderHelperModel.GetModelsByOrderCode(order_code);
        if (ohm.Length > 0)
        {
            if (is_invoice)
            {
                if (ohm[0].order_invoice.ToString().Length != Config.OrderInvoiceLength
                    && ohm[0].order_invoice.ToString().Length != 5)
                {
                    CH.Alert("此订单没有发票号码(Invoice No.)", this.Literal1);
                    return false;
                }
                else
                {
                    //if (Config.order_complete.IndexOf("[" + ohm[0].pay_method.ToString() + "]") == -1)
                    //{
                    //    CH.Alert("订单状态不是完成已可下载", this.lv_sys_list);
                    //}
                    //else

                    ohm[0].is_download_invoice = true;
                    ohm[0].Update();
                    return true;

                }
            }
            else
            {
                ohm[0].is_download_invoice = false;
                ohm[0].Update();
                return true;
            }
        }
        return false;
    }

    public bool SendToCustomer( string to_email,string sendBody, bool accessories_pdf, bool accessories_invoice, ref string error)
    {
        MailMessage mm = new MailMessage();
        mm.Body = sendBody;
        ////if (OrderCodeRequest.ToString().Length != 5)
        if (string.IsNullOrEmpty(to_email))
            mm.To.Add(new MailAddress("terryeah@gmail.com"));
        else
        {
            mm.Bcc.Add(new MailAddress("terryeah@gmail.com"));
            mm.To.Add(new MailAddress(to_email));
        }
        mm.Bcc.Add(new MailAddress("sales@lucomputers.com"));
        mm.BodyEncoding = System.Text.Encoding.Default;
        mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
        mm.From = new MailAddress("sales@lucomputers.com","LU Computers");
        mm.IsBodyHtml = true;
        // if (OrderCodeRequest.ToString().Length != 5)
            
        // else
        //   mm.To.Add(new MailAddress("terryeah@gmail.com"));
        mm.Priority = MailPriority.Normal;
        mm.ReplyTo = new MailAddress("sales@lucomputers.com");
        mm.Sender = new MailAddress("sales@lucomputers.com");
        if (TextBox_email_subject.Text.Trim().Length < 2)
            mm.Subject = "LU COMPUTERS " + (OrderCodeRequest.ToString().Length == 5 ? "EBAY" : "") + " ORDER(#" + OrderCodeRequest + ") CONFIRMATION THANK YOU FOR YOUR BUSINESS!" + (string.IsNullOrEmpty(to_email) ? " empty!! " : "");
        else
            mm.Subject = this.TextBox_email_subject.Text.Trim() + (string.IsNullOrEmpty(to_email) ? " empty!! " : "");
        if (accessories_pdf)
        {
            AccDownPDF(int.Parse(InvoiceCode), accessories_invoice);
            PDFHelper PH = new PDFHelper();
            string pdf_path_file = "";
            PH.CreatePDF(int.Parse(InvoiceCode), ref pdf_path_file, ref error, this.Page);
            //CH.Alert(pdf_path_file, this.Literal1);
            mm.Attachments.Add(new System.Net.Mail.Attachment(pdf_path_file));
        }
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

    #region porperties
    public string Email
    {
        get { return Util.GetStringSafeFromQueryString(Page, "email"); }
    }

    public string InvoiceCode
    {
        get
        {
            object o = ViewState["InvoiceCode"];
            if (o != null)
                return o.ToString();
            return "";
        }
        set { ViewState["InvoiceCode"] = value; }
    }
    public bool IsSend
    {
        get
        {
            string s = Util.GetStringSafeFromQueryString(Page, "issend");
            if (s == "true")
                return true;
            else
                return false;
        }
    }

    #endregion

    protected void btn_accessories_Click(object sender, EventArgs e)
    {
        string email = this.TextBox1.Text;
        string error = "";
        //string invoiceCode = "";
        if (this.SendToCustomer(email, new OrderMailContent().SendContent(this.OrderCodeRequest.ToString(), this.txtNote.Text), true, true, ref error))
        {

            CH.Alert("is OK", this.Literal1);

        }
        else
        {
            CH.Alert("error!" + error, this.Literal1);
        }
    }

    protected void btn_attach_order_Click(object sender, EventArgs e)
    {
        string email = this.TextBox1.Text;
        string error = "";
        if (this.SendToCustomer(email, new OrderMailContent().SendContent(this.OrderCodeRequest.ToString(), this.txtNote.Text), true, false, ref error))
        {
            CH.Alert("is OK", this.Literal1);
        }
        else
        {
            CH.Alert("error!" + error, this.Literal1);
        }
    }

    protected void btn_view_invoice_Click(object sender, EventArgs e)
    {
        AccDownPDF(int.Parse(InvoiceCode), true);
        PDFHelper PH = new PDFHelper();
        string pdf_path_file = "";
        string error = "";
        PH.CreatePDF(int.Parse(InvoiceCode), ref pdf_path_file, ref error, this.Page);
        Response.Redirect("/order_pdf/" + InvoiceCode + ".pdf");
    }

    protected void btn_view_order_form_Click(object sender, EventArgs e)
    {
        AccDownPDF(int.Parse(InvoiceCode), false);
        PDFHelper PH = new PDFHelper();
        string pdf_path_file = "";
        string error = "";
        PH.CreatePDF(int.Parse(InvoiceCode), ref pdf_path_file, ref error, this.Page);
        Response.Redirect("/order_pdf/" + InvoiceCode + ".pdf");
    }
}
