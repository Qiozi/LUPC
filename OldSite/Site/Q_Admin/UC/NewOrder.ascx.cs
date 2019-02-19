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

public partial class Q_Admin_UC_NewOrder : OrderCtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void lb_Search_Click(object sender, EventArgs e)
    {
        try
        {
            string order_code = this.txt_order_code.Text.Trim();
            OrderPageBase.OrderCode = order_code;
            //if (order_code != "" && OrderHelperModel.GetNewOrderCode(int.Parse(order_code)) )
            //{
            //    OrderPageBase.OrderCode = order_code;
            //}
            //else
            //{
            //    AnthemHelper.Alert("未找到此订单号");
            //    return;
            //}

            //OrderPageBase.OrderCode = "";
            if (onCliekSearchOrderCode != null)
            {
                onCliekSearchOrderCode();
            }
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void lb_new_order_Click(object sender, EventArgs e)
    {
        
        if (OrderPageBase.CurrentCustomerID == -1)
        {
            AnthemHelper.Alert("请选择一个客户");
            return;
        }
        var context = new LU.Data.nicklu2Entities();

        OrderPageBase.OrderCode = OrderHelperModel.GetNewOrderCode(context).ToString() ;
        if (onCliekNewOrderCode != null)
        {
            onCliekNewOrderCode();
        }
    }

    public event OnClickSearchOrderCode onCliekSearchOrderCode;
    public event OnClickSearchOrderCode onCliekNewOrderCode;
}
public delegate void OnClickSearchOrderCode();