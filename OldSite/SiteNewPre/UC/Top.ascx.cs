using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_Top : UserControlBase
{
    public int DefaultCateId = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin)
            {
                var cartQty = CartQty;
                var badge = "<span class='badge'>" + cartQty + "</span>";
                ltLoginStr.Text = @"
<a class=""btn "" href=""/ShoppingCart.aspx"">
    <span class="" glyphicon glyphicon-shopping-cart""></span> My Cart" + badge + @"                                                            
</a>
</li>
<li>
<a class=""btn "" href=""/MyAccount.aspx"">
    <span class=""glyphicon glyphicon-user""></span> Hi! " + CustomerName + @" My Account                                                       
</a>
</li>
<li>
<a class=""btn "" href=""/Logout.aspx""><span class=""glyphicon glyphicon-log-out""></span> Logout </a>
";
            }
            else
            {
                ltLoginStr.Text = @"<a class=""btn "" href=""/Login.aspx""><span class=""glyphicon glyphicon-user""></span> Login</a>";
            }

            // 继用上次使用的搜索类型
            DefaultCateId = Util.GetInt32SafeFromQueryString(Page, "cate", 1);
        }
    }
}