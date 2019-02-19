using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_NetCmd_ChangePartPrice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (true)
            {
                if (Request.QueryString["QioziCommand"].ToString() == "qiozi@msn.com")
                {

                    DataTable categoryDT= Config.ExecuteDataTable("select distinct category_id from tb_other_inc_bind_price");
                    Config.ExecuteNonQuery(@"
delete from tb_other_inc_bind_price_tmp;

SET SQL_BIG_SELECTS=1;");
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    sb.Append("");
                    for (int i = 0; i < categoryDT.Rows.Count; i++)
                    {
                        sb.Append(","+categoryDT.Rows[i]["category_id"].ToString());
                    }
                    string category_ids = "";
                    if (sb.ToString().Length > 2)
                        category_ids = sb.ToString().Substring(1);
                    if (category_ids != "")
                    {

//                        Config.ExecuteNonQuery(string.Format(@"
//SET SQL_BIG_SELECTS=1;
//insert into tb_other_inc_bind_price_tmp (product_serial_no, category_id, other_inc_id, other_inc_sku, other_inc_price,other_inc_store_sum, product_current_cost, product_current_price,product_current_price_tmp,bind_type)
//select distinct product_serial_no, category_id, oi.other_inc_id, ol.other_inc_sku, oip.other_inc_price, oip.other_inc_store_sum, 
//p.product_current_cost,p.product_current_price, 0 ,bind_type
//from tb_product p inner join tb_other_inc_bind_price oi on oi.category_id=p.menu_child_serial_no and  oi.category_id in ({0}) and
// (oi.is_relating=1 and bind_type=2 and oi.manufactory=p.producter_serial_no and p.producter_serial_no <> '' and p.producter_serial_no is not null)
//		inner join tb_other_inc_match_lu_sku ol on ol.lu_sku=p.product_serial_no and ol.other_inc_type=oi.other_inc_id 
//		inner join tb_other_inc_part_info oip on oip.other_inc_id=oi.other_inc_id and oip.other_inc_sku=ol.other_inc_sku 
//where p.tag=1 and split_line=0 and is_non=0 and p.product_current_cost > 0 and oip.other_inc_price>0 and p.product_current_discount=0 ;
//", category_ids));

//                        Config.ExecuteNonQuery(string.Format(@"
//SET SQL_BIG_SELECTS=1;
//insert into tb_other_inc_bind_price_tmp (product_serial_no, category_id, other_inc_id, other_inc_sku, other_inc_price,other_inc_store_sum, product_current_cost, product_current_price, product_current_price_tmp,bind_type)
//select distinct product_serial_no, category_id, oi.other_inc_id, ol.other_inc_sku, oip.other_inc_price, oip.other_inc_store_sum, p.product_current_cost,p.product_current_price, 0,bind_type
//from tb_product p inner join tb_other_inc_bind_price oi on oi.category_id=p.menu_child_serial_no and oi.category_id in 
//({0}) and oi.is_relating=1 and bind_type=1
//inner join tb_other_inc_match_lu_sku ol on ol.lu_sku=p.product_serial_no and ol.other_inc_type=oi.other_inc_id
//inner join tb_other_inc_part_info oip on oip.other_inc_id=oi.other_inc_id and oip.other_inc_sku=ol.other_inc_sku
//where p.tag=1 and split_line=0 and is_non=0 and p.product_current_cost >0 and p.product_current_discount=0
//
// and product_serial_no not in (select product_serial_no from tb_other_inc_bind_price_tmp)
// and product_serial_no not in (select distinct product_serial_no
//            from tb_product p inner join tb_other_inc_bind_price oi on oi.category_id=p.menu_child_serial_no and  oi.category_id in 
//({0}) and
//             (oi.is_relating=0 and bind_type=2 and oi.manufactory=p.producter_serial_no and p.producter_serial_no <> '' and p.producter_serial_no is not null)
//		            inner join tb_other_inc_match_lu_sku ol on ol.lu_sku=p.product_serial_no and ol.other_inc_type=oi.other_inc_id 
//		            inner join tb_other_inc_part_info oip on oip.other_inc_id=oi.other_inc_id and oip.other_inc_sku=ol.other_inc_sku 
//            where p.tag=1 and split_line=0 and is_non=0 and p.product_current_cost > 0 ) ;
//", category_ids));
//                        Config.ExecuteNonQuery(@"
//
///* product */
//CREATE TEMPORARY TABLE tmp_product (
//
//product_serial_no int(6) NOT NULL,
//menu_child_serial_no int(6) NOT NULL,
//product_current_cost decimal(7,2) not null,
//product_current_price decimal(7,2) not null,
//producter_serial_no varchar(30) not null  
//);
//
//insert into tmp_product(product_serial_no,menu_child_serial_no,product_current_cost,product_current_price, producter_serial_no)
//select product_serial_no, menu_child_serial_no, product_current_cost,product_current_price, producter_serial_no
//	from tb_other_inc_bind_price op inner join tb_product p on p.menu_child_serial_no=op.category_id
//	where tag=1 and split_line=0 and is_non=0 and product_current_cost >0 and product_current_discount=0;
//
//
///* match lu sku */
//CREATE TEMPORARY TABLE tmp_match_lu_sku (
//lu_sku int(6) NOT NULL,
//other_inc_type int(6) NOT NULL,
//other_inc_sku varchar(30) not null   
//);
//insert into tmp_match_lu_sku(lu_sku, other_inc_type, other_inc_sku)
//select o.lu_sku, other_inc_type, other_inc_sku 
//from (select distinct other_inc_id from  tb_other_inc_bind_price) p inner join tb_other_inc_match_lu_sku o on o.other_inc_type=p.other_inc_id;
//
//
///* part info */
//CREATE TEMPORARY TABLE tmp_part_info (
//other_inc_id int(6) NOT NULL,
//other_inc_store_sum int(6) NOT NULL,
//other_inc_sku varchar(30) not null,
//other_inc_price decimal(7,2) not null
//);
//
//insert into tmp_part_info(other_inc_id, other_inc_sku,other_inc_store_sum, other_inc_price)
//select oi.other_inc_id,other_inc_sku,other_inc_store_sum,other_inc_price 
//from (select distinct other_inc_id from  tb_other_inc_bind_price) p 
//inner join  tb_other_inc_part_info oi  on oi.other_inc_id=p.other_inc_id where tag=1 ;
//
///* vendor match */
//SET SQL_BIG_SELECTS=1;
//insert into tb_other_inc_bind_price_tmp (product_serial_no, category_id, other_inc_id, other_inc_sku, other_inc_price,other_inc_store_sum, product_current_cost, product_current_price,product_current_price_tmp,bind_type)
//select distinct product_serial_no, category_id, oi.other_inc_id, ol.other_inc_sku, oip.other_inc_price, oip.other_inc_store_sum, 
//p.product_current_cost,p.product_current_price, 0 ,bind_type
//
//from tmp_product p 
//	inner join 
//	tb_other_inc_bind_price oi on oi.category_id=p.menu_child_serial_no and
// (oi.is_relating=1 and bind_type=2 and oi.manufactory=p.producter_serial_no and p.producter_serial_no <> '' and p.producter_serial_no <> 'NULL' and p.producter_serial_no is not null)
//		inner join tmp_match_lu_sku ol on ol.lu_sku=p.product_serial_no and ol.other_inc_type=oi.other_inc_id 
//		inner join tmp_part_info oip on oip.other_inc_id=oi.other_inc_id and oip.other_inc_sku=ol.other_inc_sku ;
//
///* category match */
//SET SQL_BIG_SELECTS=1;
//insert into tb_other_inc_bind_price_tmp (product_serial_no, category_id, other_inc_id, other_inc_sku, other_inc_price,other_inc_store_sum, product_current_cost, product_current_price, product_current_price_tmp,bind_type)
//select p.product_serial_no, oi.category_id, oi.other_inc_id, ol.other_inc_sku, oip.other_inc_price, oip.other_inc_store_sum, p.product_current_cost,p.product_current_price, 0,oi.bind_type
//	from 
//		tmp_product p
//		inner join 
//		(select category_id,other_inc_id,bind_type from tb_other_inc_bind_price where is_relating=1 and bind_type=1) oi on oi.category_id=p.menu_child_serial_no 
//
//	inner join tmp_match_lu_sku ol on ol.lu_sku=p.product_serial_no and ol.other_inc_type=oi.other_inc_id
//	inner join tmp_part_info oip on oip.other_inc_id=oi.other_inc_id and oip.other_inc_sku=ol.other_inc_sku
//	left join tb_other_inc_bind_price_tmp tp on tp.product_serial_no=p.product_serial_no where tp.product_serial_no is null;
//
///* delete temp table */
//drop table tmp_part_info;
//drop table tmp_match_lu_sku;
//drop table tmp_product;");

//                        if (categoryDT.Rows.Count > 0)
//                        {
//                            Config.ExecuteNonQuery(@"update tb_other_inc_bind_price_tmp ot set 
//product_current_price_tmp = round(product_current_price  * ( other_inc_price/product_current_cost), 0)-0.01
//where  other_inc_price<>product_current_cost;
//
//delete from tb_other_inc_bind_price_tmp where product_current_price_tmp<1;
//
//update tb_product p, tb_other_inc_bind_price_tmp oi 
//set p.product_current_price=oi.product_current_price_tmp
//, p.product_current_cost = oi.other_inc_price 
//, p.product_current_special_cash_price = ( oi.product_current_price_tmp) / 1.022
//where p.product_serial_no=oi.product_serial_no and oi.product_current_price_tmp>0;
//
//insert into tb_other_inc_bind_price_tmp_history 
//	( product_serial_no, category_id, other_inc_sku, other_inc_price, other_inc_store_sum, 
//	other_inc_id, 
//	product_current_price, 
//	product_current_price_tmp, 
//	product_current_cost
//	,mark, bind_type
//	)
//select product_serial_no, category_id, other_inc_sku, other_inc_price, other_inc_store_sum, 
//	other_inc_id, 
//	product_current_price, 
//	product_current_price_tmp, 
//	product_current_cost
//	,date_format(now(), '%Y-%m-%d'), bind_type
//	from tb_other_inc_bind_price_tmp;");
//                        }
                    }

                    UpdateNewInfo();
                    UpdateRealPrice10dayInfo();
                    //ChangeStoreInLU();
                   // ChangeShopbotPosition();
                    Response.Write(string.Format("OK..{0}", DateTime.Now.ToString()));
                    Response.End();
                }
            }
        }
    }

    public void UpdateNewInfo()
    {
        Config.ExecuteNonQuery("update tb_product set `new`=0,is_modify=1 where date_format(now(), \"%Y%j\")- date_format(regdate, \"%Y%j\")>15");
    }
    public void UpdateRealPrice10dayInfo()
    {
        DataTable dt = Config.ExecuteDataTable("select product_serial_no from tb_product where date_format(now(), '%Y%j') -date_format(real_cost_regdate, '%Y%j')>10");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(","+dt.Rows[i][0].ToString());

        }

        if(sb.ToString() .Length >2)
            Config.ExecuteNonQuery(string.Format("Update tb_product set product_current_real_cost=0,is_modify=1, real_cost_regdate='0000-00-00 00:00:00' where product_serial_no in ({0});", sb.ToString().Substring(1)));

    }

    private void ChangeShopbotPosition()
    {
        DataTable dt = Config.ExecuteDataTable("select * from (select distinct lu_sku,other_inc_name,count(lu_sku)c from tb_other_inc_shopbot where other_inc_name='lucomputers.com' group by lu_sku ) tmp where c>1");
        Config.ExecuteNonQuery("Update tb_product set shopbot_info=''");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int luc_sku;
            int.TryParse(dr["lu_sku"].ToString(), out luc_sku);
            DataTable subdt = Config.ExecuteDataTable("select distinct lu_sku, other_inc_name, price from tb_other_inc_shopbot where lu_sku ='" + luc_sku + "' order by price asc");
            for (int j = 0; j < subdt.Rows.Count; j++)
            {
                if (subdt.Rows[j]["other_inc_name"].ToString() == "lucomputers.com")
                {
                    Config.ExecuteNonQuery(@"
Update tb_product set shopbot_info='" + (j + 1).ToString() + "," + subdt.Rows.Count.ToString() + "',is_modify=1 where product_serial_no='" + luc_sku + @"';");
                    Config.ExecuteNonQuery(@"
Update tb_other_inc_shopbot set position='" + (j + 1).ToString() + "' where lu_sku ='" + luc_sku + "' and other_inc_name='lucomputers.com';");
                    break;
                }
            }
        }
    }


    public void ChangeStoreInLU()
    {
        try
        {


            string error = "";

            Config.ExecuteNonQuery(@"delete from tb_other_inc_match_lu_sku where other_inc_type not in (select id from tb_other_inc where id <> 50);
delete from tb_other_inc_part_info where other_inc_id not in (select id from tb_other_inc where id <> 50);");

            DataTable dt = Config.ExecuteDataTable(@"select product_serial_no from tb_product p where (p.tag=1 or p.issue=0) and split_line=0 and is_non=0 and menu_child_serial_no in (" + new GetAllValidCategory().ToString() + ")");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    int lu_sku;
                    int.TryParse(dt.Rows[i][0].ToString(), out lu_sku);
                    decimal cost = 0M;
                    int max_stock = 0;
                    int min_stock = 0;
                    int sum_stock = 0;

                    decimal _cost;

                    DataTable _storeDT = Config.ExecuteDataTable(@"select other_inc_price product_cost, other_inc_store_sum product_store_sum from tb_other_inc_match_lu_sku ol inner join tb_other_inc_part_info oi
on oi.other_inc_sku=ol.other_inc_sku and  tag=1 and lu_sku='" + lu_sku.ToString() + "'");
                    for (int j = 0; j < _storeDT.Rows.Count; j++)
                    {
                        DataRow dr = _storeDT.Rows[j];
                        int _stock;
                        int.TryParse(dr["product_store_sum"].ToString(), out _stock);
                        if (_stock > 0)
                            sum_stock += _stock;
                        if (_stock > max_stock)
                            max_stock = _stock;
                        if (_stock < min_stock)
                            min_stock = _stock;


                        decimal.TryParse(dr["product_cost"].ToString(), out _cost);
                        if (max_stock > 0)
                        {
                            if (_stock > 0)
                            {
                                if (_cost < cost)
                                {
                                    cost = _cost;
                                }
                            }
                        }
                        else
                        {
                            if (_stock > 0)
                                cost = _cost;
                            else
                            {
                                if (_cost < cost)
                                    cost = _cost;
                            }
                        }
                    }

                    if (sum_stock == 0)
                    {
                        sum_stock = min_stock;
                    }

                    var context = new LU.Data.nicklu2Entities();
                    var pm = ProductModel.GetProductModel(context, lu_sku);
                    pm.product_current_cost_2 = cost;
                    pm.ltd_stock = sum_stock;
                    pm.last_regdate = DateTime.Now;
                    context.SaveChanges();
                    //if (lu_sku == 3783)
                    //{
                    //    CH.Alert(string.Format("{0}|{1}|{2}|{3}|{4}", lu_sku, sum_stock, max_stock, min_stock, cost), this.btn_run);
                    //    return;
                    //}
                }
                catch (Exception ex)
                {
                    error += ex.Message + "<br>";

                }
            }
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
