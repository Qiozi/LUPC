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

public partial class Q_Admin_product_helper_price_compare : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.CategoryDropDownLoad1.categoryId = Util.GetInt32SafeFromQueryString(Page, "category_id", -1);
            BindPartFieldsLV();
            btn_go_Click(null, null);
            CH.CloseParentWatting(this.Literal1);
        }
    }
    

    private void BindPartFieldsLV()
    {
        this.cb_fields.DataSource = Config.ExecuteDataTable("select * from tb_part_compare_fields");
        this.cb_fields.DataTextField = "field_comment";
        this.cb_fields.DataValueField = "id";
        this.cb_fields.DataBind();
    }

    
    public string ViewCompare(int category_id)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        bool is_exist_shopbot = false;

        DataTable dt = Config.ExecuteDataTable(string.Format(@"
select product_serial_no sku, 
case when product_name_long_en<>'' then product_name_long_en 
when product_name <> '' then product_name else product_short_name end as name, product_current_cost cost, product_current_price-product_current_discount sell
{1}
from tb_product where menu_child_serial_no='{0}' and tag=1 and split_line=0 and is_non=0"
            , category_id
            , GetTableHeaderString(category_id, ref is_exist_shopbot)));

        sb.Append("<div style=\"background:#f2f2f2;\"><table  cellpadding=\"2\" cellspacing=\"0\" >");
        sb.Append("<tr>");
        sb.Append(string.Format("<th>&nbsp;</th>"));
        sb.Append(string.Format("<th>&nbsp;</th>"));
        for (int i = 0; i < dt.Columns.Count; i++)
        {
            if (dt.Columns[i].ToString() != "name")
                sb.Append(string.Format("<th>{0}</th>", dt.Columns[i].ToString()));
        }
        sb.Append("</tr>");

        string other_inc_sql = GetOtherIncPriceSql(category_id);
        for (int i = 0; i < dt.Rows.Count; i++)
        {

            DataRow dr = dt.Rows[i];
            int lu_sku ;
            int.TryParse(dr["sku"].ToString(), out lu_sku);

            DataTable riveDT = new DataTable();
            if (other_inc_sql != string.Empty)
                riveDT = Config.ExecuteDataTable(string.Format(@"
{1} where ol.lu_sku='{0}'", lu_sku, other_inc_sql));

            decimal sell;
            decimal.TryParse(dr["sell"].ToString(), out sell);

            decimal cost;
            decimal.TryParse(dr["cost"].ToString(), out cost);
            
            sb.Append("<tr  onmouseover='this.className=\"onmouseover\";' onmouseout=\"this.className='onmouseout';\">");
            sb.Append("<td style=' border-bottom: 1px solid #ccc;width: 50px;'>");
            sb.Append(string.Format("<a href=\"/q_admin/other_inc_view_compare.aspx?categoryid={0}&id={1}\" onclick=\"winOpen(this.href, 'shopbotView', 880, 800, 120, 200);return false;\" title=\"Modify Detail\">", category_id, lu_sku));
            sb.Append("shopbot");
            sb.Append("</a>");
            sb.Append("</td>");
            sb.Append("<td style=' border-bottom: 1px solid #ccc;width: 40px;'>");
            sb.Append(string.Format("<a href=\"/q_admin/editPartDetail.aspx?id={0}\" onclick=\"winOpen(this.href, 'modify_detail', 880, 800, 120, 200);return false;\" title=\"Modify Detail\">Edit</a>", lu_sku));
            sb.Append("</td>");
            sb.Append(string.Format("<td width=\"50\"  style=' border-bottom: 1px solid #ccc;'><a href='/product_parts_detail.asp?id={0}&parent_id={1}' target='_blank'>{0}</a></td>", lu_sku, category_id));
            sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>{0}</td>", dr["cost"].ToString()));
            sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;{1}; border-right: 1px solid #cccccc;'>{0}&nbsp;</td>", dr["sell"].ToString(), sell < cost ?"background: red;":""));

            for (int j = 0; j < riveDT.Columns.Count; j++)
            {
                if (riveDT.Rows.Count > 0)
                {
                    decimal riv_price;
                    decimal.TryParse("riveDT.Rows[0][j].ToString()", out riv_price);
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;{1}{2}'>{0}&nbsp;</td>", riveDT.Rows[0][j].ToString() == "" ? "&nbsp;" : riveDT.Rows[0][j].ToString(),
                        riv_price < sell ?" color: red;" :""
                        , j == riveDT.Columns.Count -1?"border-right: 1px solid #cccccc;":""));
                }
                else
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>&nbsp;</td>"));
            }

            if (is_exist_shopbot)
            {
                DataTable shopDT = Config.ExecuteDataTable(string.Format(@"select round(avg(price),2) shopbot_avg_price, round(max(price),2) shopbot_max_price , round(min(price),2) shopbot_min_price
from tb_other_inc_shopbot where lu_sku='{0}'", lu_sku));
                if (shopDT.Rows.Count == 1)
                {
                    decimal shopbot_avg;
                    decimal.TryParse(shopDT.Rows[0]["shopbot_avg_price"].ToString(), out shopbot_avg);
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;{1}'>{0}</td>", shopbot_avg == 0M ? "&nbsp;" : shopbot_avg.ToString()
                   , sell < shopbot_avg ? "background:green;color: white":""));
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>{0}</td>", shopDT.Rows[0]["shopbot_max_price"].ToString() == "" ? "&nbsp;" : shopDT.Rows[0]["shopbot_max_price"].ToString()));
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>{0}</td>", shopDT.Rows[0]["shopbot_min_price"].ToString() == "" ? "&nbsp;" : shopDT.Rows[0]["shopbot_min_price"].ToString()));
                }
                else
                {
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>&nbsp;</td>"));
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>&nbsp;</td>"));
                    sb.Append(string.Format("<td width=\"70\" style='text-align:right;border-bottom: 1px solid #ccc;'>&nbsp;</td>"));
                }
            }


            sb.Append("</tr>");
            sb.Append(string.Format("<tr  onmouseover='this.className=\"onmouseover\";' onmouseout=\"this.className='onmouseout';\"><td colspan='{1}'  style='border-bottom: 1px solid #ccc;color:#ccc;'>{0}</td></tr>", dr["name"].ToString() == "" ? "&nbsp;" : dr["name"].ToString(), 5 + 3 + riveDT.Columns.Count));
                   
        }
        sb.Append("</table></div>");
        return sb.ToString();
    }

    private void BindFieldsValueToPage(int category_id)
    {
        DataTable dt = Config.ExecuteDataTable("Select fields_id from tb_part_compare_fields_record where category_id='" + category_id.ToString() + "'");
        for (int j = 0; j < this.cb_fields.Items.Count; j++)
        {           
                this.cb_fields.Items[j].Selected = false;
        }        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            for (int j = 0; j < this.cb_fields.Items.Count; j++)
            {               
                if (dr["fields_id"].ToString() == this.cb_fields.Items[j].Value.ToString())
                    this.cb_fields.Items[j].Selected = true;
            }
        }
    }

    private string GetTableHeaderString(int category_id, ref bool is_exist_shopbot)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select field_name from tb_part_compare_fields_record pcf 
inner join tb_part_compare_fields pc on pcf.fields_id=pc.id where category_id='{0}'", category_id));
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        LtdHelper lh = new LtdHelper();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            foreach (int j in Enum.GetValues(typeof(Ltd)))
            {
                string text = lh.FilterText(Enum.GetName(typeof(Ltd), j)).ToLower();
                if (dt.Rows[i][0].ToString().ToLower() == text)
                    sb.Append(string.Format(",'' {0}", dt.Rows[i][0].ToString().ToLower()));
            }

        } 
        
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            if (dt.Rows[i][0].ToString() == "shopbot")
            {
                sb.Append(string.Format(",'' shopbot_avg_price"));
                sb.Append(string.Format(",'' shopbot_max_price"));
                sb.Append(string.Format(",'' shopbot_min_price"));
                is_exist_shopbot = true;
            }
        }
        return sb.ToString();

    }

    private string GetOtherIncPriceSql(int category_id)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select field_name from tb_part_compare_fields_record pcf 
inner join tb_part_compare_fields pc on pcf.fields_id=pc.id where category_id='{0}'", category_id));

        LtdHelper lh = new LtdHelper();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            foreach (int j in Enum.GetValues(typeof(Ltd)))
            {
                string text = lh.FilterText(Enum.GetName(typeof(Ltd), j)).ToLower();
                if (dt.Rows[i][0].ToString().ToLower() == text)
                    sb.Append(string.Format(@",max(case when oi.other_inc_id='{0}' then oi.other_inc_price end) as {1}_price 
                ", j, text));
            }
        }
        if(sb.ToString().Length >2)
        return ("select "+ sb.ToString().Substring(1) + @" from tb_other_inc_part_info oi inner join tb_other_inc_match_lu_sku ol 
on ol.other_inc_type=oi.other_inc_id and ol.other_inc_sku=oi.other_inc_sku ");
        return string.Empty;
    }

    protected void btn_go_Click(object sender, EventArgs e)
    {
        if (this.CategoryDropDownLoad1.categoryId > 0)
        {
            GetOtherIncPriceSql(this.CategoryDropDownLoad1.categoryId);
            BindFieldsValueToPage(this.CategoryDropDownLoad1.categoryId);
            this.literal_view_compare.Text = ViewCompare(this.CategoryDropDownLoad1.categoryId);
        }
    }

    protected void cb_fields_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.CategoryDropDownLoad1.categoryId > 0)
        {
            Config.ExecuteNonQuery("delete from tb_part_compare_fields_record where category_id='" + this.CategoryDropDownLoad1.categoryId.ToString() + "'");
            for (int i = 0; i < this.cb_fields.Items.Count; i++)
            {
                if (this.cb_fields.Items[i].Selected)
                {
                    Config.ExecuteNonQuery(string.Format("insert into tb_part_compare_fields_record(fields_id, category_id) values ('{0}', '{1}')",
                        this.cb_fields.Items[i].Value, this.CategoryDropDownLoad1.categoryId));
                }
            }
            btn_go_Click(null, null);
        }
    }
}
