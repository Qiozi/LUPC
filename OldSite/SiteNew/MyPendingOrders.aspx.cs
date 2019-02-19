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
            IsSignIn();
            if (IsLogin && IsLocalHostFrom)
            {
                int custSerialno = CurrCustomer.customer_serial_no.Value;

                
                var pendingList = (from c in db.tb_order_helper
                                   where c.customer_serial_no.HasValue && c.customer_serial_no.Value.Equals(custSerialno) &&
                                   c.is_ok.HasValue && c.is_ok.Value.Equals(true) &&
                                   c.pre_status_serial_no != setting.ORDER_STATUS_FINISHED
                                   orderby c.order_helper_serial_no descending
                                   select new LU.Model.M.OrderListItem
                                   {
                                       OrderCode = c.order_code.Value,
                                       OrderDate = DateTime.Now,
                                       ShippingFirstName = string.Empty,
                                       ShippingLastName = string.Empty,
                                       Amount = c.grand_total.Value,
                                       PriceUnit = c.price_unit,
                                       PreStatusId = c.pre_status_serial_no,
                                       UpsTrackingNumber = string.Empty,
                                       ShippingDate = null
                                   }).ToList();
                for (int i = 0; i < pendingList.Count; i++)
                {
                    var item = pendingList[i];
                    var customer = db.tb_customer_store.Single(p => p.order_code.HasValue && p.order_code.Value.Equals(item.OrderCode));
                    var upsShippingInfo = db.tb_order_ups_tracking_number.SingleOrDefault(p => p.order_code.HasValue && p.order_code.Value.Equals(item.OrderCode));
                    item.ShippingFirstName = customer.customer_shipping_first_name;
                    item.ShippingLastName = customer.customer_shipping_last_name;
                    item.PreStatusName = db.tb_pre_status.Single(p => p.pre_status_serial_no.Equals(item.PreStatusId)).pre_status_name;
                    item.UpsTrackingNumber = upsShippingInfo != null ? upsShippingInfo.ups_tracking_number : string.Empty;
                    item.ShippingDate = upsShippingInfo != null ? upsShippingInfo.regdate : DateTime.MinValue;
                }
                rptList.DataSource = pendingList;
                rptList.DataBind();
                if (pendingList.Count == 0)
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