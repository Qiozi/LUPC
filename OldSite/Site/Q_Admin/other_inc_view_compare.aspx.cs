using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_view_compare : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {//
            if (LUSKU > 0)
            {
                //
                // other inc 
                this.lv_other_inc.DataSource = Config.ExecuteDataTable(string.Format(@"
select distinct other_inc_price, other_inc_name,other_inc_store_sum, last_regdate regdate from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
on oi.other_inc_sku=ol.other_inc_sku and oi.other_inc_id=ol.other_inc_type
inner join tb_other_inc o on o.id=oi.other_inc_id where ol.lu_sku='{0}' and oi.tag=1 order by other_inc_price asc ", LUSKU));
                this.lv_other_inc.DataBind();

                this.lv_shopbot.DataSource = Config.ExecuteDataTable(string.Format(@"select  distinct lu_sku, other_inc_name, price, regdate 
	from 
	tb_other_inc_shopbot where
 lu_sku='{0}' order by price asc ", LUSKU));
                this.lv_shopbot.DataBind();


                BindPartPrice();

                this.lb_lu_sku.Text = LUSKU.ToString();

                int img_sku = LUSKU;
                DataTable dt = Config.ExecuteDataTable("select other_product_sku from tb_product where product_serial_no='" + LUSKU.ToString() + "'");
                if (dt.Rows.Count == 1)
                {
                    int other_sku;
                    int.TryParse(dt.Rows[0][0].ToString(), out other_sku);
                    if (other_sku != 0)
                        img_sku = other_sku;
                }

                this.literal_lu_img.Text = string.Format("<img src='http://www.lucomputers.com/pro_img/COMPONENTS/{0}_list_1.jpg' title='{0}' width='200' >", img_sku == 0 ? 999999 : img_sku);


                this.literal_pre_next_btn.Text = GetButtonString(CategoryID, LUSKU, Date);
            }
        }
    }

    private string GetButtonString(int categoryid, int luSKU, string date)
    {
        int pre_sku = 0;
        int next_sku = 0;
        bool is_exist = false;
        int current_serial = 0;
        DataTable dt = new DataTable();
        if (categoryid != -1)
        {

            dt = Config.ExecuteDataTable(string.Format(@"select product_serial_no from tb_product where menu_child_serial_no='{0}' and tag=1 and split_line=0 and is_non=0 order by product_order,product_serial_no asc", categoryid));
        }
        else if (Position != -1)
        {
            dt = Config.ExecuteDataTable(string.Format(@"select distinct lu_sku from tb_other_inc_shopbot where other_inc_name='lucomputers.com' and position='{0}' order by lu_sku asc  ", Position));
        }
        else
        {
            dt = Config.ExecuteDataTable(string.Format(@"select distinct product_serial_no from tb_other_inc_bind_price_tmp_history where  mark='{0}' order by category_id, id asc", date));

        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            int _sku;
            int.TryParse(dt.Rows[i][0].ToString(), out _sku);

            if (!is_exist)
            {

            }
            else
            {
                next_sku = _sku;
                break;
            }
            if (luSKU == _sku)
            {
                current_serial = i + 1;
                is_exist = true;
            }
            else
            {
                pre_sku = _sku;
            }

        }

        if (pre_sku != 0 && next_sku != 0)
        {
            return string.Format(@"
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={0}&categoryid={1}&date={5}&position={6}"");' value='pre part'/>
{3} &nbsp; of &nbsp; {4}
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={2}&categoryid={1}&date={5}&position={6}"");' value='next part'/>", pre_sku, categoryid
                                                                                                                                        , next_sku
                                                                                                                                        , current_serial
                                                                                                                                        , dt.Rows.Count
                                                                                                                                         , date
                                                                                                                                 , Position);
        }
        else if (next_sku != 0 && pre_sku == 0)
        {
            return string.Format(@"
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={0}&categoryid={1}&date={5}&position={6}"");' value='pre part'  disabled=""disabled""/>
{3} &nbsp; of &nbsp; {4}
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={2}&categoryid={1}&date={5}&position={6}"");' value='next part'/>", pre_sku, categoryid
                                                                                                                                        , next_sku
                                                                                                                                        , current_serial
                                                                                                                                        , dt.Rows.Count
                                                                                                                                        , date
                                                                                                                                 , Position);
        }
        else if (next_sku == 0 && pre_sku != 0)
        {
            return string.Format(@"
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={0}&categoryid={1}&date={5}&position={6}"");' value='pre part'/>
{3} &nbsp; of &nbsp; {4}
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={2}&categoryid={1}&date={5}&position={6}"");' value='next part'  disabled=""disabled""/>", pre_sku, categoryid
                                                                                                                                 , next_sku
                                                                                                                                 , current_serial
                                                                                                                                 , dt.Rows.Count
                                                                                                                                 , date
                                                                                                                                 , Position);
        }
        else
        {
            return string.Format(@"
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={0}&categoryid={1}&date={5}&position={6}"");' value='pre part' disabled=""disabled""/>
{3} &nbsp; of &nbsp; {4}
<input type='button' onclick='window.location.replace(""/q_admin/other_inc_view_compare.aspx?id={2}&categoryid={1}&date={5}&position={6}"");' value='next part'  disabled=""disabled""/>", pre_sku, categoryid
                                                                                                                                 , next_sku
                                                                                                                                 , current_serial
                                                                                                                                 , dt.Rows.Count
                                                                                                                                 , date
                                                                                                                                 , Position);
        }


        //        return "";
    }

    public void BindPartPrice()
    {


        DataTable dt = Config.ExecuteDataTable(string.Format("select manufacturer_part_number,menu_child_serial_no,product_current_real_cost, product_name,product_current_discount, product_current_price, product_current_price - product_current_discount sold , product_current_cost, date_format(real_cost_regdate, \"%b-%d-%Y\") real_cost_regdate   from tb_product where product_serial_no='{0}'", LUSKU));
        if (dt.Rows.Count > 0)
        {
            this.lbl_mfp.Text = dt.Rows[0]["manufacturer_part_number"].ToString();
            this.lb_product_name.Text = dt.Rows[0]["product_name"].ToString();
            this.lbl_part_cost.Text = dt.Rows[0]["product_current_cost"].ToString();
            this.lbl_part_sold.Text = dt.Rows[0]["sold"].ToString();
            decimal price;
            decimal.TryParse(dt.Rows[0]["sold"].ToString(), out price);
            this.txt_product_current_price.Text = (price / Config.is_card_rate).ToString("###.00");
            this.lbl_part_price.Text = dt.Rows[0]["product_current_price"].ToString();
            this.lbl_product_current_discount.Text = dt.Rows[0]["product_current_discount"].ToString();
            this.lbl_part_real_cost.Text = dt.Rows[0]["product_current_real_cost"].ToString();
            this.lbl_part_real_cost_regdate.Text = dt.Rows[0]["real_cost_regdate"].ToString();
        }
    }

    #region properties

    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryid", -1); }
    }
    public string Date
    {
        get { return Util.GetStringSafeFromQueryString(Page, "date"); }
    }
    public string cmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    public int LUSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }
    public int Position
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "position", -1); }
    }
    #endregion

    protected void btn_save_Click(object sender, EventArgs e)
    {
        decimal special_cash;
        decimal.TryParse(this.txt_product_current_price.Text.Trim(), out special_cash);
        if (special_cash != 0M)
        {
            var pm = ProductModel.GetProductModel(DBContext, LUSKU);
            pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(pm.product_current_discount.Value + special_cash);
            pm.product_current_special_cash_price = special_cash;
            DBContext.SaveChanges();
            BindPartPrice();
            CH.Alert(KeyFields.SaveIsOK, this.lv_other_inc);
        }
        else
        {
            this.txt_product_current_price.Focus();
            CH.Alert("Please input special cash", this.lv_other_inc);
        }
    }
}
