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

public partial class Q_Admin_product_helper_system_warn : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.product_manage);
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindSystemRPT(false);
    }

    private void BindSystemRPT( bool autoUpdate)
    {
        DataTable dt = EbaySystemModel.FindSystemByWarn();
        dt.Columns.Add("sub_item");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            dt.Rows[i]["sub_item"] = GetSubItem(int.Parse(dt.Rows[i]["system_templete_serial_no"].ToString()), dt);
        }
        this.rpt_system.DataSource = dt;
        this.rpt_system.DataBind();
    }

    private string GetSubItem(int system_templete_serial_no, DataTable dt)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        EbaySystemPartsModel[] spm = EbaySystemPartsModel.FindAllByProperty("system_sku", system_templete_serial_no);

        sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" style=\"width:90%;\" align='left'> ");
        for (int i = 0; i < spm.Length; i++)
        {

            string bgcolor = "  color: #FFD1D1; ";
            //ProductModel p = ProductModel.GetProductModel(spm[i].product_serial_no);
            DataTable pdt = Config.ExecuteDataTable(string.Format(@"
Select tag
, product_name
, menu_child_serial_no
, product_store_sum
, ltd_stock
, is_non
, split_line
from tb_product 
where product_serial_no='{0}'
", spm[i].luc_sku));

            if (pdt.Rows.Count == 1)
            {
                DataRow dr = pdt.Rows[0];
                int tag;
                int.TryParse(dr["tag"].ToString(), out tag);
                if (tag != 0)
                    bgcolor = "";
                string name = "";
                if (dr["product_name"].ToString() != null)
                    name = dr["product_name"].ToString();
                if (name.ToLower().IndexOf("overclocked") == -1)
                {
                    int menu_child_serial_no;
                    int.TryParse(dr["menu_child_serial_no"].ToString(), out menu_child_serial_no);
                    ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(menu_child_serial_no);
                    if (pc.tag == 0)
                        bgcolor = " color: green; ";
                    else
                    {
                        ProductCategoryModel ppc = ProductCategoryModel.GetProductCategoryModel(pc.menu_pre_serial_no);
                        if (ppc.tag == 0)
                            bgcolor = " color: green; ";
                    }
                }
                if (bgcolor == "")
                {
                    int product_store_sum;
                    int.TryParse(dr["product_store_sum"].ToString(), out product_store_sum);

                    int ltd_stock;
                    int.TryParse(dr["ltd_stock"].ToString(), out ltd_stock);

                    int is_non;
                    int.TryParse(dr["is_non"].ToString(), out is_non);

                    int split_line;
                    int.TryParse(dr["split_line"].ToString(), out split_line);

                    if (product_store_sum < 1 && ltd_stock < 1 && is_non ==0 && split_line == 0)
                    {
                        bgcolor = " color: red; ";
                    }
                }

                if (bgcolor.Length > 3)
                {
                    sb.Append("<tr>");
                    sb.Append("<td style='width: 80px; '>&nbsp;</td>");
                    sb.Append("<td style='" + bgcolor + "'>");
                    sb.Append(string.Format("<a href=\"product_helper_system_warn_change_part.aspx?part_id={0}&group_id={1}&system_product_serial_no={2}\" onclick=\"winOpen(this.href, 'modifyPartOfSystem', 600, 400, 200, 200);return false;\">Modify</a>&nbsp;&nbsp;&nbsp;&nbsp;", spm[i].luc_sku, spm[i].part_group_id, spm[i].id));
                    sb.Append(spm[i].luc_sku);
                    sb.Append("</td>");
                    sb.Append("<td style='" + bgcolor + "; text-align:left;' width='80%'>");

                    sb.Append(name);
                    sb.Append("</td>");
                    sb.Append("<td style='" + bgcolor + "'>");
                    if (tag == 0)
                    {
                        sb.Append("close");
                    }

                    sb.Append("</td>");
                    sb.Append("</tr>");
                }
            }
        }
        sb.Append("</table>");
        return sb.ToString();
    }

}
