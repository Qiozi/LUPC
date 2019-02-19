using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_NetCmd_GetEbaySysInfo : System.Web.UI.Page
{
    List<TempPartGroupInfo> _partGroupInfo = new List<TempPartGroupInfo>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCmd == "qiozi@msn.com")
            {
                BindGV();
            }
        }
    }

    
    DataTable GetDT()
    {
        DataTable dt = new DataTable("ebaySys");

        return dt;
    }

    void BindGV()
    {
        DataTable dt = GetDT();
        switch (ReqType)
        {
            case "AllSysList":
                CreateEBaySys(dt, false);
                break;
            case "AllSysDetail":
                CreateEBaySys(dt, true);
       
                break;
            case "AllGroupDetail":
                CreatePartGroup(dt);
                break;
            default:

                break;
        }
        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
      
    }

    /// <summary>
    /// 存comment , partGroupid
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="partGroupID"></param>
    void SetPartGroupInfo(string comment, int partGroupID, int priority)
    {
        bool exist = false;
        bool existComment = false;
        foreach (var cp in _partGroupInfo)
        {
            if (comment.Trim() == cp.GroupComment)
            {
                existComment = true;

                foreach (var id in cp.PartGroupIds)
                {
                    if (partGroupID == id)
                    {
                        exist = true;
                        break;

                    }
                }
            }
        }
        if (!exist)
        {
            if (existComment)
            {
                for (int i = 0; i < _partGroupInfo.Count; i++)
                {
                    if (comment.Trim() == _partGroupInfo[i].GroupComment)
                    {
                        _partGroupInfo[i].PartGroupIds.Add(partGroupID);
                    }
                }
            }
            else
            {
                _partGroupInfo.Add(new TempPartGroupInfo()
                {
                    GroupComment = comment,
                    PartGroupIds = new List<int>() { partGroupID },
                    Priority = priority
                });
            }
        }
    }

    void CreateEBaySys(DataTable rdt, bool haveSub)
    {
        string sheetname = haveSub ? "eBaySystemDetail" : "ebaySystem";
        if (!haveSub)
        {
            rdt.Columns.Add("SysSKU");
            rdt.Columns.Add("ItemId");
            rdt.Columns.Add("Cost");
            rdt.Columns.Add("eBay_Fee");
            rdt.Columns.Add("Base_Shipping_Fee");
            rdt.Columns.Add("Profit");
            rdt.Columns.Add("BuyItNowPrice");
            rdt.Columns.Add("Cutom_label");
            rdt.Columns.Add("Title");
            rdt.Columns.Add("SellQuantity");
            rdt.Columns.Add("IsBarebone");
            rdt.Columns.Add("Logo_Pricture");
        }
        else
        {
            rdt.Columns.Add("SysSKU");
            rdt.Columns.Add("ItemId");
            rdt.Columns.Add("CommentID");
            rdt.Columns.Add("Comment");
            rdt.Columns.Add("GroupID");
            rdt.Columns.Add("GroupName");
            rdt.Columns.Add("PartSKU");
            rdt.Columns.Add("PartName");
            rdt.Columns.Add("PartCost");
        }
        DataTable dt = Config.ExecuteDataTable(@"
select es.id, e.BuyItNowPrice,es.category_id
 ,e.SKU cutom_label, es.ebay_system_price
 ,e.ItemID, es.adjustment, e.title
 ,es.is_barebone, es.is_shrink 
 ,es.large_pic_name 
 ,es.logo_filenames 
 ,es.sub_part_quantity 
 ,oeq.quantity sellqty
,es.ebay_fee
,es.shipping_fee
,es.profit
,es.cost
 from tb_ebay_system es 
 inner join tb_ebay_selling e on e.sys_sku=es.id 
 left join tb_order_ebay_quantity oeq on oeq.itemid=e.ItemID

 inner join (select category_id, category_name from tb_product_category_new where parent_category_id='10000' and showit=1 ) cate on cate.category_id=es.category_id
 where es.is_from_ebay=0 and es.showit=1 order by e.sku asc
");            
       
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            if (!haveSub)
            {
                DataRow rdr = rdt.NewRow();

                rdr["SysSKU"] = dr["ID"].ToString();
                rdr["ItemId"] = dr["ItemID"].ToString();
                rdr["Cost"] = dr["cost"].ToString() ?? "0";
                rdr["eBay_Fee"] = dr["ebay_fee"].ToString() ?? "0";
                rdr["Base_Shipping_Fee"] = dr["shipping_fee"].ToString() ?? "0";
                rdr["Profit"] = dr["profit"].ToString() ?? "0";
                rdr["BuyItNowPrice"] = dr["BuyItNowPrice"].ToString() ?? "0";
                rdr["Cutom_label"] = dr["cutom_label"].ToString();
                rdr["Title"] = dr["title"].ToString();
                rdr["SellQuantity"] = dr["sellqty"].ToString();
                rdr["IsBarebone"] = dr["is_barebone"].ToString();
                rdr["Logo_Pricture"] = "http://www.lucomputers.com/ebay/" + dr["logo_filenames"].ToString() + ".jpg";
                rdt.Rows.Add(rdr);
            }
            // 明细
            DataTable sysDetailDT = Config.ExecuteDataTable(string.Format(@"select c.id, c.comment,category_ids, ps.luc_sku,
p.product_ebay_name, ps.part_quantity, ps.max_quantity
,p.product_current_price-p.product_current_discount sell, p.product_current_cost
,ifnull(ps.part_group_id, -1) part_group_id 
,ps.is_label_of_flash
,ps.is_belong_price
,c.is_mb
,c.is_cpu
,c.priority
, pg.part_group_comment
from tb_ebay_system_part_comment c inner join tb_ebay_system_parts ps on ps.comment_id=c.id
 left join tb_product p on p.product_serial_no=ps.luc_sku 
 inner join tb_part_group pg on pg.part_group_id=ps.part_group_id
where c.showit=1 and ps.system_sku = '{0}' order by c.priority asc", dr["ID"].ToString()));

            
            if (!haveSub)
            {
                continue;
            }


            for (int j = 0; j < sysDetailDT.Rows.Count; j++)
            {

                DataRow sdr = sysDetailDT.Rows[j];
                DataRow rdr = rdt.NewRow();
    
                SetPartGroupInfo(sdr["comment"].ToString(), int.Parse(sdr["part_group_id"].ToString()), int.Parse(sdr["priority"].ToString()));

                rdr["SysSKU"] = dr["ID"].ToString();
                rdr["ItemId"] = dr["ItemId"].ToString();
                rdr["CommentID"] = sdr["id"].ToString();

                rdr["Comment"] = sdr["comment"].ToString();
                rdr["GroupID"] =sdr["part_group_id"].ToString();
                rdr["GroupName"] =sdr["part_group_comment"].ToString();

                rdr["PartSKU"] = sdr["luc_sku"].ToString();

                rdr["PartName"] = sdr["product_ebay_name"].ToString();

                rdr["PartCost"] = sdr["product_current_cost"].ToString();
                rdt.Rows.Add(rdr);

                if (sysDetailDT.Rows.Count - 1 == j)
                {
                    DataRow rdr2 = rdt.NewRow();
                    rdt.Rows.Add(rdr2);
                    DataRow rdr3 = rdt.NewRow();
                    rdt.Rows.Add(rdr3);
                }
            }
        }
       
    }

    void CreateEBaySys()
    {
       
        DataTable dt = Config.ExecuteDataTable(@"
select es.id, e.BuyItNowPrice,es.category_id
 ,e.SKU cutom_label, es.ebay_system_price
 ,e.ItemID, es.adjustment, e.title
 ,es.is_barebone, es.is_shrink 
 ,es.large_pic_name 
 ,es.logo_filenames 
 ,es.sub_part_quantity 
 ,oeq.quantity sellqty
,es.ebay_fee
,es.shipping_fee
,es.profit
,es.cost
 from tb_ebay_system es 
 inner join tb_ebay_selling e on e.sys_sku=es.id 
 left join tb_order_ebay_quantity oeq on oeq.itemid=e.ItemID

 inner join (select category_id, category_name from tb_product_category_new where parent_category_id='10000' and showit=1 ) cate on cate.category_id=es.category_id
 where es.is_from_ebay=0 and es.showit=1 order by e.sku asc
");

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
           
            // 明细
            DataTable sysDetailDT = Config.ExecuteDataTable(string.Format(@"select c.id, c.comment,category_ids, ps.luc_sku,
p.product_ebay_name, ps.part_quantity, ps.max_quantity
,p.product_current_price-p.product_current_discount sell, p.product_current_cost
,ifnull(ps.part_group_id, -1) part_group_id 
,ps.is_label_of_flash
,ps.is_belong_price
,c.is_mb
,c.is_cpu
,c.priority
, pg.part_group_comment
from tb_ebay_system_part_comment c inner join tb_ebay_system_parts ps on ps.comment_id=c.id
 left join tb_product p on p.product_serial_no=ps.luc_sku 
 inner join tb_part_group pg on pg.part_group_id=ps.part_group_id
where c.showit=1 and ps.system_sku = '{0}' order by c.priority asc", dr["ID"].ToString()));


            for (int j = 0; j < sysDetailDT.Rows.Count; j++)
            {
                DataRow sdr = sysDetailDT.Rows[j];             
                SetPartGroupInfo(sdr["comment"].ToString(), int.Parse(sdr["part_group_id"].ToString()), int.Parse(sdr["priority"].ToString()));             
            }
        }

    }

    void CreatePartGroup(DataTable rdt)
    {
        CreateEBaySys();
        _partGroupInfo.Sort((x, y) => x.Priority - y.Priority);
        rdt.Columns.Add("Comment");
        rdt.Columns.Add("GroupID");
        rdt.Columns.Add("GroupName");
        rdt.Columns.Add("PartSKU");
        rdt.Columns.Add("PartCost");
        rdt.Columns.Add("PartStock");
        rdt.Columns.Add("PartName");
        Response.Write(_partGroupInfo.Count.ToString());
        foreach (var g in _partGroupInfo)
        {

            foreach (var id in g.PartGroupIds)
            {

                PartGroupModel pgm = PartGroupModel.GetPartGroupModel(id);
                
                // 明细
                DataTable detailDT = Config.ExecuteDataTable(string.Format(@"Select p.product_current_cost, p.product_serial_no, 
p.ltd_stock, 
 case when length(p.product_ebay_name)>4 then p.product_ebay_name 
   when length(p.product_name_long_en )>4 then p.product_name_long_en 
  when length(p.product_name)>4 then p.product_name 
	   else p.product_short_name end as product_name, p.product_current_price price, pd.part_group_id 
from tb_part_group_detail pd inner join tb_product p on p.product_serial_no=pd.product_serial_no 
where part_group_id='{0}' and 
p.split_line=0 and p.tag=1  
order by p.product_ebay_name asc, p.product_current_cost asc", id));

                for (int j = 0; j < detailDT.Rows.Count; j++)
                {
                    DataRow sdr = detailDT.Rows[j];

                    DataRow rdr = rdt.NewRow();
                    rdr["GroupID"] = id;
                    rdr["Comment"] = g.GroupComment;
                    rdr["GroupName"] = pgm.part_group_comment;
                    rdr["PartSKU"] = sdr["product_serial_no"].ToString();
                    rdr["PartCost"] = sdr["product_current_cost"].ToString();
                    rdr["PartStock"] = sdr["ltd_stock"].ToString();
                    rdr["PartName"] = sdr["product_name"].ToString();
                    
                    rdt.Rows.Add(rdr);

                    if (j == detailDT.Rows.Count - 1)
                    {
                        DataRow rdr2 = rdt.NewRow();
                        rdt.Rows.Add(rdr2);
                        DataRow rdr3 = rdt.NewRow();
                        rdt.Rows.Add(rdr3);
                    }
                }

            }
        }



    }



    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    string ReqType
    {
        get { return Util.GetStringSafeFromQueryString(Page, "type"); }
    }
}