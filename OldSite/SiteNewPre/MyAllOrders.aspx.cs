using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAllOrders : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin && IsLocalHostFrom)
            {
                int custSerialno;
                int.TryParse(CustomerSerialNo, out custSerialno);

                var pendingList = (from oh in db.tb_order_helper                                   
                                   join ps in db.tb_pre_status on oh.pre_status_serial_no equals ps.pre_status_serial_no
                                   join cs in db.tb_customer_store on oh.order_code.Value equals cs.order_code.Value
                                   where oh.customer_serial_no.HasValue
                                   && oh.customer_serial_no.Value.Equals(custSerialno)
                                   && oh.is_ok.HasValue
                                   && oh.is_ok.Value.Equals(true)
                                   orderby oh.order_helper_serial_no descending
                                   select new
                                   {
                                       OrderCode = oh.order_code.Value,
                                       OrderDate = oh.order_date,
                                       Download = oh.is_download_invoice .Value ? "<span style=\"cursor: pointer\" onClick=\"\">Download</span>":"",
                                       Amount = oh.grand_total.Value,
                                       PriceUnit = oh.price_unit,
                                       StatusName = ps.pre_status_name
                                   }).ToList();
                rptList.DataSource = pendingList;
                rptList.DataBind();
                if (pendingList.Count == 0)
                    ltNoRecord.Text = "<p style='padding:5em;font-size:24px;' class='note'>No orders.</p>";
                
            }
            else
            {

            }

        }
    }
    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        //if (e.Item.ItemType != ListItemType.Footer
        //    && e.Item.ItemType != ListItemType.Header
        //    && e.Item.ItemType != ListItemType.Pager)
        //{
        //    var orderCodeControl = e.Item.FindControl("hfOrderCode") as HiddenField;
        //    string ordercode = orderCodeControl.Value;

        //    var msgList = (from m in db.tb_chat_msg
        //                   where m.msg_order_code.Equals(ordercode)
        //                   select m).ToList();
        //    string result = "";
        //    for (int i = 0; i < msgList.Count; i++)
        //    {
        //        result += "<strong style='color:green;'>" + msgList[i].msg_author + "</strong>&nbsp;&nbsp;&nbsp;&nbsp;[" + msgList[i].regdate.ToString() + "]";
        //        result += "<pre>" + msgList[i].msg_content_text + "</pre>";
        //        result += "";
        //    }

        //    (e.Item.FindControl("ltMsgList") as Literal).Text = result;
        //}
    }
}