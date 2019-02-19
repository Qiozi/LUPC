using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyPendingOrders : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin && IsLocalHostFrom)
            {
               
                 //set rs = conn.execute("select oh.order_source, oh.msg_from_seller,oh.total, order_code, ps.pre_status_name ,order_date,
                 //customer_first_name,customer_last_name ,oh.grand_total,oh.price_unit
                // from tb_order_helper oh inner join tb_customer c on c.customer_serial_no=oh.customer_serial_no 
                // inner join tb_pre_status ps on ps.pre_status_serial_no=oh.pre_status_serial_no 
                // where c.customer_serial_no=" & SQLquote(LAYOUT_CCID) & " 
               //  and ps.pre_status_serial_no<>"& LAYOUT_ORDER_STATUS_FINISHED &" and oh.is_ok=1 order by order_helper_serial_no desc")
                int custSerialno;
                int.TryParse(CustomerSerialNo, out custSerialno);
               
                var pendingList = (from oh in db.tb_order_helper
                                   join ups in db.tb_order_ups_tracking_number.DefaultIfEmpty() on oh.order_code.Value equals ups.order_code.Value 
                                   into ohups
                                   from m in ohups.DefaultIfEmpty()

                                   join ps in db.tb_pre_status on oh.pre_status_serial_no equals ps.pre_status_serial_no
                                   join cs in db.tb_customer_store on oh.order_code.Value equals cs.order_code.Value
                                   
                                   where oh.customer_serial_no.HasValue
                                   && oh.customer_serial_no.Value.Equals(custSerialno)
                                   && oh.is_ok.HasValue
                                   && oh.is_ok.Value.Equals(true)
                                   && oh.pre_status_serial_no !=setting.ORDER_STATUS_FINISHED
                                   orderby oh.order_helper_serial_no descending
                                   select new
                                   {
                                       OrderCode = oh.order_code.Value,
                                       OrderDate = oh.order_date,
                                       Name = cs.customer_shipping_first_name + " " + cs.customer_shipping_last_name,
                                       Amount = oh.grand_total.Value, 
                                       PriceUnit = oh.price_unit,
                                       StatusName = ps.pre_status_name,
                                       UpsTrackingNumber  = m.ups_tracking_number,
                                       ShippingDate = m.regdate
                                   }).ToList();
                rptList.DataSource = pendingList;
                rptList.DataBind();
                if(pendingList.Count ==0)
                    ltNoRecord.Text = "<p style='padding:5em;font-size:24px;' class='note'>No pending orders.</p>";

            }
            else
            {

            }
           
        }
    }
    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer
            && e.Item.ItemType != ListItemType.Header
            && e.Item.ItemType != ListItemType.Pager)
        {
            var orderCodeControl = e.Item.FindControl("hfOrderCode") as HiddenField;
            string ordercode = orderCodeControl.Value;

            var msgList = (from m in db.tb_chat_msg
                           where m.msg_order_code.Equals(ordercode)
                           select m).ToList();
            string result = "";
            for (int i = 0; i < msgList.Count; i++)
            {
                result += "<strong style='color:green;'>" + msgList[i].msg_author + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;[" + msgList[i].regdate.ToString() + "]";
                result += "<pre>" + msgList[i].msg_content_text + "</pre>";
                result += "";
            }

            (e.Item.FindControl("ltMsgList") as Literal).Text = result;
        }
    }
}