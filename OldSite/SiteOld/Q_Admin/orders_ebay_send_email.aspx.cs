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

public partial class Q_Admin_orders_ebay_send_email : PageBase
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

        int order_code = Util.GetInt32SafeFromQueryString(Page, "order_code", -1);
        DataTable dt = Config.ExecuteDataTable("select order_source from tb_order_helper where order_code='" + order_code.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][0].ToString() == "3")
            {
                int stracking = Config.ExecuteScalarInt32("select count(order_helper_serial_no) from tb_order_helper where order_code='" + order_code.ToString() + "'");
                if (stracking > 0)
                {
                    DataTable eDT = Config.ExecuteDataTable("select * from tb_order_ebay where order_code='" + order_code.ToString() + "'");
                    DataTable shipDT = Config.ExecuteDataTable("select date_format(regdate,'%m/%d/%Y') regdate from tb_order_ups_tracking_number where order_code='" + order_code.ToString() + "'");
                    if (eDT.Rows.Count > 0 && shipDT.Rows.Count > 0)
                    {
                        DataRow dr = eDT.Rows[0];
                        DataTable loginDT = Config.ExecuteDataTable("select customer_login_name, customer_password from tb_customer where customer_login_name='" + dr["user_id"].ToString() + "'");

                        if (loginDT.Rows.Count > 0)
                        {
                            if (SendEmail(order_code.ToString(),
                               dr["buyer_email"].ToString(),
                                shipDT.Rows[0][0].ToString(),
                                dr["item_title"].ToString(),
                                dr["item_number"].ToString(),
                                dr["user_id"].ToString(),
                                "dpowerseller",
                                dr["total_price_unit"].ToString() + " $" + dr["total_price"].ToString()
                                , loginDT.Rows[0]["customer_login_name"].ToString()
                                , loginDT.Rows[0]["customer_password"].ToString()))
                            {
                                Config.ExecuteNonQuery("Update tb_order_helper set is_send_email=1,Is_Modify=1 where order_code='" + order_code.ToString() + "'");
                                Response.Write("<script>alert('it is ok');window.close();</script>");
                            }
                            else
                            {
                                Response.Write("<script>alert('发送失败;');</script>");
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('数据不完整');</script>");
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('数据不完整');</script>");
                    }
                }
                else
                {

                    Response.Write("<script>alert('UPS tracking number 不存在;');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('不是ebay 订单;');</script>");
            }

        }
        else
        {
            Response.Write("<script>alert('订单不存在;');</script>");
        }

    }

    public bool SendEmail(string order_code, string buyer_email_address, string ship_date, string item_title,
        string item_number,string buye_user_id,  string seller_user_id, string total, string your_account_name, string your_password)
    {
        string sbody = string.Format(@"
<html>
<head>
<title>lu computer</title>
<style>
    .body {{ font-size: 8pt; font-family: ""Calibri"", Courier, monospace; }}
</style>
</head>
<body>
I have shipped the following item to you on {0}. You should be receiving it shortly.  
<br/><br/>
Item title: {1}<br/>
Item number: {2}<br/>
Buyer User ID: {3}<br/>
Buyer Email Address：{4}<br/>
Seller User ID: {5}<br/>
Total: {6}<br/>
<br/>
Please logon to <a href=""http://www.lucomputers.com"" target=""_blank"">www.lucomputers.com</a> to find your tracking number.  <br/>
Your account name: {7}<br/>
Your password: {8}<br/>
<br/>
Please do not reply to this email.  If you have a message for us you can leave it on your lucomputers account.<br/>
<br/>
Once your item arrives in satisfactory condition, please leave feedback for me. I will do the same for you.<br/>
<br/>
Thank you again for your business!
</body>
</html>", ship_date, item_title, item_number, buye_user_id, buyer_email_address, seller_user_id, total, your_account_name, your_password);

        MessageClass mc = new MessageClass();
        if (true)
        {
            mc.ContentType = "text/html";
            mc.Body = sbody;// ;
            mc.Logging = true;
            mc.Silent = true;
            mc.ReplyTo = "sales@lucomputers.com";
            mc.MailServerUserName = Config.mailUserName;
            mc.MailServerPassWord = Config.mailPassword;
            mc.From = "sales@lucomputers.com";
            mc.FromName = "LU COMPUTERS";
            mc.Subject = "LU COMPUTERS ORDER(#" + order_code + ") HAVE SHIPPED CONFIRMATION, THANK YOU FOR YOUR BUSINESS!";
            mc.AddRecipient(buyer_email_address, buyer_email_address, null);
            mc.AddRecipient("terryeah@gmail.com", "terryeah@gmail.com", null);
        }
        //else
        //{
        //    mc.ContentType = "text/html";
        //    mc.Body = sbody;
        //    mc.Logging = true;
        //    mc.Silent = true;
        //    mc.MailServerUserName = "xiaowu021@126.com";
        //    mc.MailServerPassWord = "1234qwer";
        //    mc.From = "xiaowu021@126.com";
        //    mc.FromName = "LU COMPUTER";
        //    mc.Subject = "LU COMPUTERS ORDER(#" + order_code + ") HAVE SHIPPED CONFIRMATION, THANK YOU FOR YOUR BUSINESS!";
        //    mc.AddRecipient(buyer_email_address, buyer_email_address, null);
        //}
        bool b = mc.Send("smtp.126.com", false);
        mc.Clear();
        return b;

    }
}
