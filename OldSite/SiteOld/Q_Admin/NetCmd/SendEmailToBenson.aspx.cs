using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jmail;  

public partial class Q_Admin_NetCmd_SendEmailToBenson : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["QioziCommand"].ToString() == "qiozi@msn.com")
        {
            //this.lbl_run_time.Text = GetEmailBody();
            string str = GetEmailBody();
            this.lbl_run_time.Text = str;
            str = this.lbl_run_time.Text;
            //lubenson@hotmail.com
            if (SendToCustomer("lubenson@hotmail.com", str))
                Response.Write("OK------" + DateTime.Now.ToString());
            else
                Response.Write("Error------" + DateTime.Now.ToString());
        }
    }

    public string GetEmailBody()
    {
        int current_part_quantity = Config.ExecuteScalarInt32("select count(product_serial_no) from tb_product where tag=1 and split_line=0 and is_non=0 and menu_child_serial_no in ("+ new GetAllValidCategory().ToString() +") and issue=1");
        int yestoday_part_quantity= Config.ExecuteScalarInt32("select ifnull(count(product_serial_no),0) from tb_product where tag=1 and split_line=0 and is_non=0 and issue=1 and to_days(now())-to_days(regdate) = 1 and menu_child_serial_no in ("+ new GetAllValidCategory().ToString() +")");
        int today_part_change_price_quantity = Config.ExecuteScalarInt32("select ifnull(count(id), 0) from tb_other_inc_bind_price_tmp");
        int today_part_change_price_quantity_up = Config.ExecuteScalarInt32("select ifnull(count(id), 0) from tb_other_inc_bind_price_tmp where other_inc_price > product_current_cost");
        int today_part_change_price_quantity_down= Config.ExecuteScalarInt32("select ifnull(count(id), 0) from tb_other_inc_bind_price_tmp where other_inc_price < product_current_cost");
        int yestoday_upload_import_price = 0; // Config.ExecuteScalarInt32("select ifnull(count(*), 0) from tb_product_import_price where to_days(part_real_regdate)+1=to_days(now())");
        string yestoday_upload_import_price_cost = "$0.00"; // decimal.Parse(Config.ExecuteScalar("select ifnull(sum(part_real_cost), 0) from tb_product_import_price where to_days(part_real_regdate)+1=to_days(now())").ToString()).ToString("$###,###.00");
        string yestoday_upload_import_price_sell = "$0.00"; //decimal.Parse(Config.ExecuteScalar("select ifnull(sum(part_sell), 0) from tb_product_import_price where to_days(part_real_regdate)+1=to_days(now())").ToString()).ToString("$###,###.00");
        string yestoday_upload_import_price_profit = "$0.00"; //decimal.Parse(Config.ExecuteScalar("select ifnull(sum(part_sell)-sum(part_real_cost), 0) from tb_product_import_price where to_days(part_real_regdate)+1=to_days(now())").ToString()).ToString("$###,###.00");
       
        DataTable import_price_dt = Config.ExecuteDataTable(@"select ifnull(count(id), 0) c , ifnull(sum(part_real_cost), 0) cost_sum,ifnull(sum(part_sell), 0) sell_sum,  ifnull(sum(part_sell)-sum(part_real_cost), 0) profit  from tb_product_import_price where to_days(part_real_regdate)+1=to_days(now())");
       
        if (import_price_dt.Rows.Count == 1)
        {
            yestoday_upload_import_price_cost =decimal.Parse(import_price_dt.Rows[0]["cost_sum"].ToString()).ToString("$###,###.00");
            yestoday_upload_import_price_sell = decimal.Parse(import_price_dt.Rows[0]["sell_sum"].ToString()).ToString("$###,###.00");
            yestoday_upload_import_price_profit = decimal.Parse(import_price_dt.Rows[0]["profit"].ToString()).ToString("$###,###.00");       
        }

       
        int issue_part_quantity=0 ;
        int not_spec_part = 0;
        DataTable issue_dt = Config.ExecuteDataTable(@"select product_serial_no from tb_product  where issue=0");
        for (int i = 0; i < issue_dt.Rows.Count; i++)
        {
             string filename = Server.MapPath(string.Format("/part_comment/{0}_comment.html", issue_dt.Rows[i]["product_serial_no"].ToString()));
             if (File.Exists(filename))
             {
                 StreamReader sr = new StreamReader(filename);
                    string c = sr.ReadToEnd().Trim();
                    if (c.Length < 10)
                    {
                        not_spec_part += 1;
                    }
                    else
                    {
                        issue_part_quantity += 1;
                    }
             }
             else
                 not_spec_part += 1;
        }

        DataTable order_dt = Config.ExecuteDataTable(@"select grand_total, c from tb_order_total_any_day_ago 
where date_format(date_sub(current_date, interval 1 day), '%W %b %d %Y')");
        string yestoday_order_quantity = "0";
        string yestoday_order_total = "$0.00";
        if(order_dt .Rows.Count ==1)
        {
            yestoday_order_quantity = order_dt.Rows[0][0].ToString();
            yestoday_order_total = decimal.Parse(order_dt.Rows[0][1].ToString()).ToString("$###,###.00");
        }


        DataTable inc_dt = Config.ExecuteDataTable("select other_inc_name from tb_other_inc where to_days(last_run_date) = to_days(now())");
        string today_watch_price_incs = "";
        if (inc_dt.Rows.Count > 0)
        {
            for (int i = 0; i < inc_dt.Rows.Count; i++)
            {
                today_watch_price_incs += "," + inc_dt.Rows[i][0].ToString();
            }
            if (today_watch_price_incs.Length > 2)
                today_watch_price_incs = today_watch_price_incs.Substring(1);
        }
        else
        {
            today_watch_price_incs = "None";
        }

         
        string str = string.Format(@"
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head runat=""server"">
    <meta http-equiv=""Content-Type"" content=""text/html; charset=gb2312"">
    <title></title>
    <style>
        body {{ font-size: 12pt;}}
    </style>
</head>
<body>
    <form id=""form1"" runat=""server"">
    <div>
    <h3>Watch Price Software run is complete.({0})</h3>
    <hr size=""1"" />
    <h2>Web Status</h2>
    <h3>Product </h3>
    <div>Current Part Quantity:&nbsp;&nbsp;&nbsp; &nbsp;<b>{1}</b>&nbsp;&nbsp;&nbsp; &nbsp;Yestoday Add Quantity:&nbsp;&nbsp;&nbsp; &nbsp;<b>{2}</b></div><br />
    <div>Today the parts price change quantity:&nbsp;&nbsp;&nbsp; &nbsp;<b>{3}</b>&nbsp;&nbsp;&nbsp; &nbsp;Price Up:&nbsp;&nbsp; <b>{4}</b>&nbsp;&nbsp;&nbsp; &nbsp; Price Down: &nbsp;&nbsp;<b>{5}</b></div>
    <br />
    <div>
            Import price quantity in the yestoday:&nbsp;&nbsp;&nbsp; &nbsp;<b>{6}</b>&nbsp;&nbsp;&nbsp; &nbsp;import part cost total: &nbsp;&nbsp;<b>{7}</b>&nbsp;&nbsp;&nbsp; &nbsp;import part sell sub total:&nbsp;&nbsp;<b>{8}</b>&nbsp;&nbsp;&nbsp; &nbsp;Profit: &nbsp;&nbsp;<b>{9}</b>
            
    </div>
     <br />
    <div>Wait for issue quantity:&nbsp;&nbsp;&nbsp; &nbsp;<b>{10}</b>&nbsp;&nbsp;&nbsp; &nbsp;Don't input Specifications quantity: &nbsp;&nbsp;&nbsp; &nbsp;<b>{11}</b></div>
    <br />
    <h3>Order</h3>
    <div>
            Yestoday order quantity:&nbsp;&nbsp;&nbsp; &nbsp;<b>{12}</b>&nbsp;&nbsp;&nbsp; &nbsp;Total: &nbsp;&nbsp;<b>{13}</b>
            
    </div>
     <h3>Watch Price</h3>
     <div>
            Today complete: {14}
     </div>
    </div>
    </form>
</body>
</html>
", DateTime.Now.ToString()
        , current_part_quantity,yestoday_part_quantity,today_part_change_price_quantity,today_part_change_price_quantity_up, today_part_change_price_quantity_down
        , yestoday_upload_import_price, yestoday_upload_import_price_cost, yestoday_upload_import_price_sell, yestoday_upload_import_price_profit
        , issue_part_quantity, not_spec_part,yestoday_order_quantity, yestoday_order_total, today_watch_price_incs);
        return str;
    }

    public bool SendToCustomer(string to_email, string sendBody)
    {
        MessageClass mc = new MessageClass();
        mc.ContentType = "text/html";
        mc.Body = sendBody;
        mc.Logging = true;
        mc.Silent = true;
        mc.MailServerUserName = Config.mailUserName;
        mc.MailServerPassWord = Config.mailPassword;
        mc.From = "sales@lucomputers.com";
        mc.FromName = "LU Web";
        //mc.Subject = "LU COMPUTERS: INVOICE (#" + InvoiceCode + ") THANK YOU FOR YOUR BUSINESS!";
        mc.Subject = "web status";
        mc.AddRecipient(to_email, to_email, null);
        mc.AddRecipient("qiozi@msn.com", "qiozi@msn.com", null);
        return mc.Send(Config.mailServer, false);
    }
}
