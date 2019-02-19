using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_export_valid_lu_sku_and_manufacture : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Util.GetStringSafeFromQueryString(Page, "cmd").ToLower() != "qiozi@msn.com")
        {
            Response.End();
            return;
        }


        Response.ClearContent();//
        Response.ContentType = "text/html";

        Config config = new Config();
        if (CmdType == "exportMatchSku")
        {
            //DataTable dt = Config.ExecuteDataTable("select distinct lu_sku, other_inc_sku, other_inc_type, prodType from tb_other_inc_match_lu_sku");
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    sb.Append("," + string.Format("('{0}','{1}','{2}','{3}')"
            //        , dt.Rows[i]["lu_sku"].ToString()
            //        , dt.Rows[i]["other_inc_sku"].ToString()
            //        , dt.Rows[i]["other_inc_type"].ToString()
            //        , dt.Rows[i]["prodType"].ToString()));
            //}
            //if (sb.ToString().Length > 10)
            //{               
            //    Response.Write(sb.ToString().Substring(1) + ";");
            //}
        }
        else
        {
            DataTable dt = Config.ExecuteDataTable(string.Format(@"
delete from tb_other_inc_valid_lu_sku;
insert into tb_other_inc_valid_lu_sku 
	( lu_sku, manufacturer_part_number, is_valid, is_ncix_remain, price, cost, discount, ltd_stock, menu_child_serial_no, brand,adjustment, prodType)
select product_serial_no, manufacturer_part_number, 1, 0, product_current_price
, product_current_cost, product_current_discount, ltd_stock, menu_child_serial_no, producter_serial_no,adjustment, prodType from tb_product 
where tag=1 and is_non=0 and split_line=0 and manufacturer_part_number<>''
 and manufacturer_part_number<>'' and manufacturer_part_number <> 'NULL' 
 and menu_child_serial_no in ({0}) and menu_child_serial_no not in ({1});
select lu_sku product_serial_no, manufacturer_part_number, price, cost, discount, ltd_stock, menu_child_serial_no, brand,adjustment, prodType from tb_other_inc_valid_lu_sku;
", new GetAllValidCategory().ToString(), config.do_not_watch_category_ids));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("," + string.Format(" ({0},'{1}','{2}','{3}','{4}','{5}', '{6}', '{7}', '{8}', '{9}')"
                     , dt.Rows[i]["product_serial_no"].ToString()
                     , dt.Rows[i]["manufacturer_part_number"].ToString()
                     , dt.Rows[i]["price"].ToString()
                     , dt.Rows[i]["cost"].ToString()
                     , dt.Rows[i]["discount"].ToString()
                     , dt.Rows[i]["ltd_stock"].ToString()
                     , dt.Rows[i]["menu_child_serial_no"].ToString()
                     , dt.Rows[i]["brand"].ToString()
                     , dt.Rows[i]["adjustment"].ToString()
                     , dt.Rows[i]["prodType"].ToString()
                     ));
            }
            if (sb.ToString().Length > 0)
            {
                Response.Write(sb.ToString().Substring(1) + ";");
            }
        }
        Response.End();
    }

    public string CmdType
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmdtype"); }
    }
}
