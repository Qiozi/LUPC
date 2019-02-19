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
using System.Xml.Linq;

public partial class mamage_top : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindTopDG(false);
        }
    }

    #region Methods

    private void BindTopDG(bool autoUpdate)
    {
        this.DataGrid1.DataSource = DBContext.tb_top.ToList();// TopModel.FindAll();
        this.DataGrid1.DataBind();
        this.DataGrid1.UpdateAfterCallBack = autoUpdate;
    }




    #endregion

    #region Events
    protected void Button1_Click(object sender, EventArgs e)
    {
        string text = @" <div id=""left_top_10_area""  style=""margin-top:3px;"">
<table width=""166"" height=""28"" border=""0"" cellpadding=""0"" cellspacing=""0"" id=""__01"" align=""center"">
  <tr>
    <td> <img src=""/soft_img/app/title2_01.gif"" width=""19"" height=""28"" alt=""""></td>
    <td width=""123"" align=""center"" background=""/soft_img/app/title2_02.gif""><span class=""text_orange_13"">TOP 10 </span>
        <% if session(""user"") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> """" then 
			 Response.Write ""&nbsp;<a href=""""/manage_top.aspx?"""" onclick=""""js_callpage_cus(this.href, 'right_manage', 1000,800);return false;"""">M</a>""									
		end if
        %>
    </td>
    <td> <img src=""/soft_img/app/title2_03.gif"" width=""23"" height=""28"" alt=""""></td>
  </tr>
</table>
<table width=""165""  border=""0"" cellspacing=""0"" cellpadding=""0"">
  <tr>
    <td height=""1""></td>
  </tr>
</table>
<table width=""166""  border=""0"" cellpadding=""1"" cellspacing=""2"" bgcolor=""#FFFFFF"" style=""border:#8FC2E2 1px solid; "" align=""center"">
  <tr>
    <td style=""border:#E3E3E3 1px solid; ""><table width=""100%""  border=""0"" align=""center"" cellpadding=""2"" cellspacing=""0"">";
        

        for (int i = 0; i < this.DataGrid1.Items.Count; i++)
        {
            int id = AnthemHelper.GetAnthemDataGridCellText(this.DataGrid1.Items[i], 0);
            int sku = AnthemHelper.GetAnthemDataGridCellTextBoxTextInt(this.DataGrid1.Items[i], 1, "_txt_top_sku");
            string comment = AnthemHelper.GetAnthemDataGridCellTextBoxText(this.DataGrid1.Items[i], 2, "_txt_top_comment");
            var tm = DBContext.tb_top.Single(me => me.top_id.Equals(id));// TopModel.GetTopModel(id);
            tm.top_sku = sku;
            tm.top_comment = comment;

            DBContext.SaveChanges();

            string menu_child_serial_no = string.Empty;
            if (tm.top_comment.Trim() == "")
            {
               var p = ProductModel.GetProductModel(DBContext, sku);
                tm.top_comment = p.product_short_name;
                menu_child_serial_no = p.menu_child_serial_no.ToString();
            }
            else
            {
               var p = ProductModel.GetProductModel(DBContext, sku);
                menu_child_serial_no = p.menu_child_serial_no.ToString();
            }
            int cid;
            int.TryParse(menu_child_serial_no, out cid);

            var pc = ProductCategoryModel.GetProductCategoryModel(DBContext, cid);
            
            text += string.Format(@"<tr>
        <td width=""10"" valign=""top"" style=""padding-top:5px; border-bottom:solid 1px #f2f2f2;"" ><img src=""/soft_img/app/{0}.gif"" width=""20"" height=""11""></td>
        <td valign=""top"" style=""border-bottom:solid 1px #f2f2f2;""><a href=""/site/product_parts_detail.asp?class={4}&id={2}&cid={1}"" class=""hui-orange-s"">
		
			{3}
		
        </a></td>
      </tr>", i + 1, menu_child_serial_no, sku, tm.top_comment, pc.menu_pre_serial_no);
        }
        text += @"</table>
   
    </td>
  </tr>
</table>
</div>
<table  bgcolor=""#ffffff"" style=""border: 1px solid rgb(143, 194, 226);margin-top:3px;"" cellpadding=""1"" cellspacing=""2"" width=""166"" height=""374"" align=""center"">
  <tbody><tr>
    <td style=""border: 1px solid rgb(227, 227, 227); padding-top: 5px; line-height: 12px; font-size: 8.5pt; font-family: Tahoma;"" align=""center"" bgcolor=""#ffffff"" valign=""top"" height=""62""><a href=""/site/p_sale.asp""> <img src=""/soft_img/app/left_41.jpg"" border=""0"" hspace=""5""></a></td>
  </tr>
  <tr>
    <td style=""border: 1px solid rgb(227, 227, 227); padding-top: 5px; line-height: 12px; font-size: 8.5pt; font-family: Tahoma;"" align=""center"" bgcolor=""#ffffff"" valign=""top"" height=""62""><a href=""/site/p_rebate.asp""> <img src=""/soft_img/app/left_42.jpg"" border=""0"" width=""144"" height=""55"" hspace=""5""></a></td>

  </tr>
  <tr>
    <td style=""border: 1px solid rgb(227, 227, 227); padding-top: 5px; line-height: 12px; font-size: 8.5pt; font-family: Tahoma;"" align=""center"" bgcolor=""#ffffff"" valign=""top"" height=""78""><a href=""/site/p_new.asp""> <img src=""/soft_img/app/left_43.jpg"" border=""0"" width=""144"" height=""72"" hspace=""5""></a></td>
  </tr>
  <tr>
    <td style=""border: 1px solid rgb(227, 227, 227); padding-top: 5px; line-height: 12px; font-size: 8.5pt; font-family: Tahoma;"" align=""center"" bgcolor=""#ffffff"" valign=""top"" height=""67""><a href=""/site/shipping.asp""> <img src=""/soft_img/app/left_44.jpg"" border=""0"" width=""144"" height=""55"" hspace=""5""></a></td>
  </tr>
  <tr>

    <td style=""border: 1px solid rgb(227, 227, 227); padding-top: 5px; line-height: 12px; font-size: 8.5pt; font-family: Tahoma;"" align=""center"" bgcolor=""#ffffff"" valign=""top"" width=""155"" height=""75""><a href=""/site/payment.asp""><img src=""/soft_img/app/left_5.jpg"" border=""0"" width=""130"" height=""61"" hspace=""5""></a></td>
  </tr>
</tbody></table>
";
        FileHelper.GenerateFile(Server.MapPath("~/site/top10_tpl.asp"), text);
        this.BindTopDG(true);
        
    }

    #endregion
}
