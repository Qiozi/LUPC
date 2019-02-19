using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_edit_detail_customer_msg : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        InitPage();
    }

    void InitPage()
    {
        BindMsgDG();
        BindMail();
        //if (IsSendMail)
        //{
        //    this.panel_mail_area.Visible = true;
        //    BindMail();
        //    //literalMsg.Text = BindSendMailContent();

        //}
        //else
        //    this.panel_mail_area.Visible = false;
        this.checkbox_send_mail.Checked = IsSendMail;

    }

    string BindSendMailContent()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        ChatMsgModel[] cmms = ChatMsgModel.FindModelsByOrderCode(ReqOrderCode.ToString());

        if (cmms != null)
        {
            sb.Append("<table width=\"80%\">");
            for (int i = 0; i < cmms.Length; i++)
            {
                sb.Append("<tr>");
                sb.Append(string.Format("     <td>{0}</td><td width='200'>{1}</td>", cmms[i].msg_author, cmms[i].regdate));
                sb.Append(string.Format("     <td>"));
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            sb.Append(new OrderMailContent().SendContent(ReqOrderCode.ToString()));

        }
        return sb.ToString();
    }
    /// <summary>
    /// 客户名称
    /// </summary>
    string CustomName
    {
        get
        {
            object obj = ViewState["CustomName"];
            if (obj != null)
                return obj.ToString();
            return "";
        }
        set
        {
            ViewState["CustomName"] = value;
        }
    }
    /// <summary>
    /// 绑定客户信息
    /// 
    /// </summary>
    void BindMsgDG()
    {
        this.dl_msg_list.DataSource = ChatMsgModel.FindModelsByOrderCode(ReqOrderCode.ToString());
        this.dl_msg_list.DataBind();
        
    }

    /// <summary>
    /// 取得最后一次输入的内容
    /// </summary>
    /// <returns></returns>
    string LastMsg()
    {
        string content = "";
        ChatMsgModel[] list = ChatMsgModel.FindModelsByOrderCode(ReqOrderCode.ToString());
        if (list != null)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].msg_author.ToLower() != "me")
                    content = list[i].msg_content_text;
            }
        }
        return content;
    }

    /// <summary>
    /// 客户的email
    /// </summary>
    void BindMail()
    {
        CustomerStoreModel CS = CustomerStoreModel.FindByOrderCode(ReqOrderCode.ToString());
        CustomName = CS.customer_shipping_first_name + " " + CS.customer_shipping_last_name;
        string mail = CS.customer_email2 == "" ? CS.customer_email1 : CS.customer_email2;
        if (mail == "")
            mail = CS.customer_login_name;
        this.txt_mail.Text = mail;
    }
    /// <summary>
    /// 
    /// </summary>
    public int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "order_code", -1); }
    }

    public bool IsSendMail
    {
        get { return Util.GetInt32SafeFromQueryString(this, "isMail", -1) == 1; }
    }

    protected void btn_submit_to_customer_Click(object sender, EventArgs e)
    {
        try
        {

            if (this.txt_msg_from_seller.Text.Trim().Length > 0)
            {
                ChatMsgModel cmm = new ChatMsgModel();
                cmm.msg_author = "Seller";
                cmm.msg_content_text = this.txt_msg_from_seller.Text.Trim().Replace("\r\n", "<br>");
                cmm.msg_order_code = ReqOrderCode.ToString();
                cmm.msg_type = 1;
                cmm.regdate = DateTime.Now;
                cmm.staff_id = LoginUser.LoginIDInt;
                cmm.Create();
                //CH.Alert(KeyFields.SaveIsOK, this.Label1);

            }
            if (checkbox_send_mail.Checked)
                btn_send_mail_Click(null, null);
            Response.Write("<script> alert('ok');window.location.href='/q_admin/orders_edit_detail_customer_msg.aspx?order_code=" + ReqOrderCode.ToString() +"';</script>");
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Label1);
        }
    }
    protected void checkbox_send_mail_CheckedChanged(object sender, EventArgs e)
    {
        
        //if (checkbox_send_mail.Checked)
        //    Response.Redirect("/q_admin/orders_edit_detail_customer_msg.aspx?isMail=1&order_code=" + ReqOrderCode.ToString());
        //else
        //    Response.Redirect("/q_admin/orders_edit_detail_customer_msg.aspx?order_code=" + ReqOrderCode.ToString());
    }

    protected void btn_send_mail_Click(object sender, EventArgs e)
    {
        string msg = LastMsg();
        if (string.IsNullOrEmpty(msg))
        {
            CH.Alert("No data", this.Label1);
            return;
        }
        string bodyHtml = "";// "<div style=\"font-size:10pt;font-family: 'Courier New'\">Order# " + ReqOrderCode.ToString() + "</div>";
        bodyHtml += "<div style=\"font-size:10pt;font-family: 'Courier New';line-height: 15px;\">Dear " + CustomName.Trim() + ",</div>";
        bodyHtml += "<div style=\"font-size:10pt;font-family: 'Courier New';line-height: 15px;\">" + msg + "<br/></div>";
        bodyHtml += "<div style=\"font-size:10pt;font-family: 'Courier New';line-height: 15px;\"><br/>Thank you,<br/>-LU Computers</div>";
        string body = string.Format("<html><head><title></title></head><body>{0}</body></html>", bodyHtml); ;

        if (EmailHelper.SendTo(this.txt_mail.Text.Trim()
             , body
             , string.Format("LU Computers Message (Order# {0})", ReqOrderCode.ToString())))
        {
           
        }
        else
            CH.Alert("Failed", this.Label1);
    }

    protected void btn_close_Click(object sender, EventArgs e)
    {
        Response.Write("<script>parent.location.href= '/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString() + "&#msg'; </script>");
       
    }
}