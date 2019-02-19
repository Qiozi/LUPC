using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

/// <summary>
/// 产品价格下载
/// </summary>
public partial class Q_Admin_PriceviewNew : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        if (Util.GetStringSafeFromQueryString(Page, "cmd") != "BLUComputers.com")
            base.InitialDatabase();
        //
        //
        DataTable dt = Config.ExecuteDataTable(@"
SET SQL_BIG_SELECTS=1;
select (p.product_serial_no)
, manufacturer_part_number MFP
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
, sum(case when other_inc_id=13 then other_inc_price else '' end) as MMAX
, sum(case when other_inc_id=17 then other_inc_price else '' end) as D2A
, '' as adjuest_price
 from tb_product p 
left join (select distinct other_inc_price, luc_sku, other_inc_id, other_inc_sku,other_inc_store_sum from tb_other_inc_part_info  where to_days(now()) - to_days(last_regdate)<=10 ) op on op.luc_sku=p.product_serial_no 
left join (select distinct luc_sku w_sku from tb_ebay_system_parts ) sp on sp.w_sku=p.product_serial_no
left join (select distinct luc_sku e_sku from tb_ebay_system_parts ) ep on ep.e_sku=p.product_serial_no 
inner join (
	Select pc1.menu_child_serial_no, pc1.menu_child_name, pc1.menu_child_order o1, pc2.menu_child_order o2 from tb_product_category pc1 inner join tb_product_category pc2
	on pc1.menu_pre_serial_no = pc2.menu_child_serial_no
	where pc2.page_category = 1 and pc2.menu_pre_serial_no=0 and pc1.tag=1 and pc2.menu_child_serial_no <> 211 and pc2.tag=1
) c on p.menu_child_serial_no = c.menu_child_serial_no 
where p.tag=1 and p.split_line=0 and p.is_non=0
 group by product_serial_no order by c.o2 asc, c.o1 asc,p.product_name asc");

        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();

    }

}