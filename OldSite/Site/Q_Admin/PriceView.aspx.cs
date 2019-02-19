using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Q_Admin_PriceView : PageBase
{
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {

            // Bind Category List
            BindCateList();
        }
    }
    

    #region Properties
    private string CategoryIDS
    {
        get
        {
            string ids = "";
            for (int i = 0; i < rpt_cate_list.Items.Count; i++)
            {
                CheckBoxList cbl = (CheckBoxList)rpt_cate_list.Items[i].FindControl("cbl_child_cate");
                for (int j = 0; j < cbl.Items.Count; j++)
                {
                    if (cbl.Items[j].Selected)
                        ids += "," + cbl.Items[j].Value.ToString();
                }
            }
            if (ids.Length > 2)
                return ids.Substring(1);
            else
                throw new ArgumentNullException("Categorys Is Null");
        }
    }
    #endregion

    #region Methods
    private void BindGV(string categoryIDs)
    {
        dt = GetDB(categoryIDs);
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();

        this.Label1.Text = string.Format(" Rows:&nbsp;" + dt.Rows.Count.ToString());
    }

    private void BindCateList()
    {
        DataTable dt =Config.ExecuteDataTable(@"select menu_child_name, menu_child_serial_no 
from tb_product_category 
where menu_pre_serial_no=0 and tag=1 and page_category=1 order by menu_child_order asc ");
        
        this.rpt_cate_list.DataSource = dt;
        this.rpt_cate_list.DataBind();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryIDs"></param>
    /// <returns></returns>
    private DataTable GetDB(string categoryIDs)
    {
        return Config.ExecuteDataTable(string.Format(@"
SET SQL_BIG_SELECTS=1;
select (p.product_serial_no)
, (p.product_name)
, (c.menu_child_name) Category_Name
, (p.product_ebay_name) eBay_Name
, p.part_ebay_price eBay_Price
, max(p.product_current_cost) cost
, max(round((p.product_current_price-p.product_current_discount)/1.022, 2)) special_price
, sum(case when other_inc_id=2 then other_inc_price else '' end) as Supercom
, sum(case when other_inc_id=2 then other_inc_store_sum else '' end) as Supercom_Quantity
, sum(case when other_inc_id=3 then other_inc_price else '' end) as ASI
, sum(case when other_inc_id=4 then other_inc_price else '' end) as Eprom
, sum(case when other_inc_id=15 then other_inc_price else '' end) as CanadaComputer
, sum(case when other_inc_id=16 then other_inc_price else '' end) as DanDh
, sum(case when other_inc_id=20 then other_inc_price else '' end) as Synnex
, sum(case when other_inc_id=100 then other_inc_price else '' end) as ETC
, sum(case when other_inc_id=101 then other_inc_price else '' end) as Ncix
, sum(case when other_inc_id=104 then other_inc_price else '' end) as DirectDial
, sum(case when other_inc_id=105 then other_inc_price else '' end) as TigerDirect
, sum(case when other_inc_id=106 then other_inc_price else '' end) as NewEgg
, case when (w_sku>0) then 'Y' else '' end as Web_Sys
, case when (e_sku) then 'Y' else '' end as eBay_Sys
 from tb_product p 
inner join (select menu_child_serial_no, menu_child_name from tb_product_category ) c on c.menu_child_serial_no=p.menu_child_serial_no
left join (select distinct other_inc_price, luc_sku, other_inc_id, other_inc_sku,other_inc_store_sum from tb_other_inc_part_info  where to_days(now()) - to_days(last_regdate)<=14 ) op on op.luc_sku=p.product_serial_no 
left join (select distinct luc_sku w_sku from tb_ebay_system_parts) sp on sp.w_sku=p.product_serial_no
left join (select distinct luc_sku e_sku from tb_ebay_system_parts ) ep on ep.e_sku=p.product_serial_no 
where p.menu_child_serial_no in ({0}) and p.tag=1 and p.split_line=0 and p.is_non=0

 group by product_serial_no order by p.product_name asc 

", categoryIDs)); 
    }

    #endregion

    #region events
  
   

    protected void btn_go_Click(object sender, EventArgs e)
    {
        BindGV(CategoryIDS);
    }

    protected void btn_download_Click(object sender, EventArgs e)
    {
        //if (dt == null)
        dt = GetDB(CategoryIDS);
        ExcelHelper eh = new ExcelHelper(dt);
       
        eh.FileName = "compare_pirce_" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";
        eh.Export();
    }

    protected void GridView1_DataBound(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            for (int j = 0; j < GridView1.Columns.Count; j++)
            {
                if (GridView1.Rows[i].Cells[j].Text.Trim() == "0")
                    GridView1.Rows[i].Cells[j].Text = "";
            }
        }
    }

    protected void rpt_cate_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
       // 
        if (e.Item.ItemType != ListItemType.Footer
            && e.Item.ItemType != ListItemType.Header
            && e.Item.ItemType != ListItemType.Pager)
        {
            HiddenField _hf = (HiddenField)e.Item.FindControl("hf_p_cid");
            //Response.Write(_hf.Value);
            int p_cid = int.Parse(_hf.Value);
            //int.TryParse(_hf.Value, out p_cid);

            CheckBoxList cbl = (CheckBoxList)e.Item.FindControl("cbl_child_cate");
            cbl.DataSource = Config.ExecuteDataTable(string.Format(@"
                        Select menu_child_serial_no, menu_child_name from tb_product_category 
                            where menu_pre_serial_no='{0}' and tag=1 order by menu_child_order asc"
                , p_cid));
            cbl.DataTextField = "menu_child_name";
            cbl.DataValueField = "menu_child_serial_no";
            cbl.DataBind();
        }
    }
    #endregion


}
