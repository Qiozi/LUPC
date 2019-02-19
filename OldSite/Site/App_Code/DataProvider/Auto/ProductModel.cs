// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/29/2007 5:26:15 PM
//
// // // // // // // // // // // // // // // //
using System;
using System.Linq;
using System.Data;
using LU.Data;

[Serializable]
public class ProductModel
{


    public static tb_product GetProductModel(nicklu2Entities context, int _product_serial_no)
    {
        return context.tb_product.SingleOrDefault(me => me.product_serial_no.Equals(_product_serial_no));
    }

    public static tb_product[] GetProductModelByMenuChild(nicklu2Entities context, int product_category_id, bool order)
    {
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_child_serial_no", product_category_id);
        //return ProductModel.FindAll(CurrentOrder(order), eq2);
        var query = context.tb_product
                           .OrderBy(me=>me.product_order.Value)
                           .Where(me => me.menu_child_serial_no.Value.Equals(product_category_id))
                           .ToList();
        return query.ToArray();
    }

    //public static NHibernate.Expression.Order[] CurrentOrder(bool order)
    //{
    //    NHibernate.Expression.Order o1 = new NHibernate.Expression.Order("product_order", order);
    //    NHibernate.Expression.Order o = new NHibernate.Expression.Order("product_serial_no", order);
    //    NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o1, o };
    //    return oo;
    //}

    public static tb_product[] GetProductModelByMenuChildAndShowit(nicklu2Entities context, int product_category_id, byte show_it, bool order)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("tag", show_it);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_child_serial_no", product_category_id);

        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //NHibernate.Expression.Order[] oo = CurrentOrder(order);
        //return ProductModel.FindAll(oo, a);
        var query = context.tb_product
                            .Where(me => me.tag.Value.Equals(show_it) &&
                                         me.menu_child_serial_no.Value.Equals(product_category_id))
                            .OrderBy(me => me.product_order.Value)
                            .ToList();
        return query.ToArray();
    }

    public static tb_product[] GetModelsByTagAndLine(nicklu2Entities context, int product_category_id)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("tag", byte.Parse("1") );
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_child_serial_no", product_category_id);
        //NHibernate.Expression.EqExpression eq3 = new NHibernate.Expression.EqExpression("split_line", byte.Parse("0"));
        //NHibernate.Expression.EqExpression eq4 = new NHibernate.Expression.EqExpression("is_non", byte.Parse("0"));
        //NHibernate.Expression.AndExpression a1 = new NHibernate.Expression.AndExpression(eq3, eq4);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        //NHibernate.Expression.AndExpression aa = new NHibernate.Expression.AndExpression(a, eq3);

        //return ProductModel.FindAll(CurrentOrder(true), aa);

        var query = context.tb_product
                            .Where(me => me.tag.Value.Equals(1) &&
                                        me.menu_child_serial_no.Value.Equals(product_category_id) &&
                                        me.split_line.Value.Equals(0) &&
                                        me.is_non.Value.Equals(0))
                            .OrderBy(me => me.product_order.Value)
                            .ToList();
        return query.ToArray();
    }

    public static tb_product[] GetProductmodelByMenuChildAndShowit(nicklu2Entities context, int product_category_id, Showit showit, bool order)
    {
        if (showit == Showit.all)
            return GetProductModelByMenuChild(context, product_category_id, order);
        else
            return GetProductModelByMenuChildAndShowit(context, product_category_id, byte.Parse(showit==Showit.show_true ? "1":"0"), order);
    }

    public static DataTable GetModelsBySearch(string keyword, bool tag)
    {
        string sql = "Select * from tb_product where 1=1 ";
        if ( tag)
            sql += " and tag=1";
        if (keyword != "")
        {
            sql += "  and (product_serial_no like '%" + keyword + "%' or product_name like '%" + keyword + "%' or product_short_name like '%" + keyword + "%')";
        }
        sql += " order by product_order asc";
        return Config.ExecuteDataTable(sql);
    }

    public static DataTable GetModelsBySKU(string SKU, bool tag)
    {
        string sql = "Select * from tb_product where 1=1 ";
        if (tag)
            sql += " and tag=1";
        if (SKU != "")
        {
            sql += "  and (product_serial_no in ("+ SKU +"))";
        }
        sql += " order by product_order asc";
        return Config.ExecuteDataTable(sql);
    }

    /// <summary>
    /// Get product info by Factory part#
    /// </summary>
    /// <param name="factory_part"></param>
    /// <returns></returns>
    public static tb_product[] FindModelsByFactoryPart(nicklu2Entities context, string factory_part)
    {
        if (factory_part == "" && factory_part == null)
            throw new Exception(" Factory Part# don't emtry");
        // return ProductModel.FindAllByProperty("manufacturer_part_number", factory_part);
        return context.tb_product.Where(me => me.manufacturer_part_number.Equals(factory_part)).ToList().ToArray();
    }

    //public static DataTable FindModelsByUpdate(string sql, DataTable regdate)
    //{
    //    return Config.ExecuteDataTable("select product_current_price, tag, menu_child_serial_no," + sql + " from tb_product where regdate='" + regdate + "'");
    //}

    public static int FindPartSumByCategory(int menu_child_serial_no)
    {
        return int.Parse(Config.ExecuteScalar("select count(product_serial_no) from tb_product where tag=1 and is_non=0 and split_line=0 and export = 1 and menu_child_serial_no='" + menu_child_serial_no + "'").ToString());
    }

    public static DataTable FindExportProduct(string category_ids)
    {
        if (category_ids == string.Empty)
        {
            category_ids = " select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and export=1 ";
        }
        
        DataTable dt = Config.ExecuteDataTable(@"select manufacturer_part_number VendorSku, '' VendorUrl, product_name ProductName ,
product_name_long_en ProductShortDescription, '' ProductImageUrl, product_current_price ProductPrice
,producter_serial_no MfrName, manufacturer_part_number MfrSku,'New' ProductCondition,p.menu_child_serial_no, p.product_serial_no
from tb_product p inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no 
where p.menu_child_serial_no in (" + category_ids + @") and p.export=1 and p.tag=1 and p.is_non=0 and split_line=0 
 and p.menu_child_serial_no in (" + new GetAllValidCategory().ToString() + @") 
order by pc.menu_child_order,p.product_order asc 

");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            int pid = int.Parse(dr["product_serial_no"].ToString());
            int cid = int.Parse(dr["menu_child_serial_no"].ToString());
           // dr["ProductPrice"] = ConvertPrice.ChangePriceToNotCard(decimal.Parse(dr["ProductPrice"].ToString()));
            dr["VendorUrl"] = "http://www.lucomputers.com/site/product_parts_detail.asp?pro_class=" + cid.ToString() + "&id=" + pid.ToString() + "&cid=" + cid.ToString();
            dr["ProductImageUrl"] = "http://www.lucomputers.com/pro_img/COMPONENTS/" + pid.ToString() + "_list_1.jpg";
            //dr["ProductPrice"] = ConvertPrice.ChangePriceToNotCard(decimal.Parse(dr["ProductPrice"].ToString()) - ProductModel.FindOnSaleDiscountByPID(pid));// -ProductModel.FindRebateDiscountByPID(pid);
            dr["ProductPrice"] = ConvertPrice.ChangePriceToNotCard(decimal.Parse(dr["ProductPrice"].ToString()) - ProductModel.FindOnSaleDiscountByPID(pid))-ProductModel.FindRebateDiscountByPID(pid);
        }
        return dt;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="product_id"></param>
    /// <param name="type">1 on sale, 2 rebate</param>
    /// <returns></returns>
    public static decimal FindOnSaleOrRebateDiscountByPID(int product_id, int type_id)
    {
        object o = Config.ExecuteScalar(@"select
sp.save_cost
from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
where sp.product_serial_no='" + product_id + @"' and sp.show_it=1 and 
 sp.promotion_or_rebate=" + type_id + " and date_format(now(),'%Y%j') between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j') order by sp.sale_promotion_serial_no desc limit 0,1");
        decimal discount_price = 0;
        if (o != null)
            decimal.TryParse(o.ToString(), out discount_price);
        return discount_price;
    }
    public static decimal FindOnSaleDiscountByPID(int product_id)
    {
        //return FindOnSaleOrRebateDiscountByPID(product_id, 1);
        decimal sp = 0M;
        object save_price =  Config.ExecuteScalar(@"select sum(os.save_price) 
from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
where (date_format(now(),'%Y%j') between date_format(os.begin_datetime,'%Y%j') 
and date_format(os.end_datetime,'%Y%j')) and p.tag=1 and pc.tag=1 and p.product_serial_no='" + product_id.ToString() + "'");
        decimal.TryParse(save_price.ToString(), out sp);
        return sp;
    }
    public static decimal FindRebateDiscountByPID(int product_id)
    {
        return FindOnSaleOrRebateDiscountByPID(product_id, 2);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="ids"></param>
    /// <param name="typeid">1 sku; 2 factory; 3 supliter</param>
    /// <returns></returns>
    public static DataTable FindInfoBySkuFactorySupliter(String ids, int typeid)
    {
        string sql = "select product_serial_no,manufacturer_part_number,supplier_sku,product_current_price,product_name,product_current_cost,tag from tb_product where 1=1";
        switch (typeid)
        {
            case 1:
                sql += " and product_serial_no in (" + ids + ")";
                break;
            case 2:
                sql += " and manufacturer_part_number in (" + ids + ")";
                break;
            case 3:
                sql += " and supplier_sku in (" + ids + ")";
                break;
        }
        //throw new Exception(sql);
        return Config.ExecuteDataTable(sql);
    
    }
    /// <summary>
    /// 取得所有有效产品的ID号 
    /// </summary>
    /// <returns></returns>
    public static DataTable FindAllProductID()
    {
        return Config.ExecuteDataTable("Select product_serial_no,other_product_sku from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.tag=1 and pc.tag=1 and p.split_line=0 and p.is_non=0");
    
    }

    public DataTable FindExaminePrice(int categoryID)
    {
//        return Config.ExecuteDataTable(string.Format(@"select p.product_serial_no,p.product_name , product_current_price, product_current_cost, product_current_discount,p.manufacturer_part_number,s.product_cost from tb_product p inner join tb_product_store_sum s on p.manufacturer_part_number=s.manufacturer_part_number and p.manufacturer_part_number<>''
// and p.tag=1 and p.split_line=0 and p.is_non=0 and p.menu_child_serial_no={0} and  p.product_current_cost<>s.product_cost and s.product_store_category=2", categoryID));
        return Config.ExecuteDataTable(string.Format(@"select p.product_serial_no,p.product_name , product_current_price, product_current_cost, product_current_discount,p.manufacturer_part_number , 0 product_cost from tb_product p  
          where p.tag=1 and p.split_line=0 and p.is_non=0 and p.menu_child_serial_no={0} ", categoryID));
    }
    public int FindExaminePriceSum(int categoryID)
    {
        return int.Parse(Config.ExecuteScalar(string.Format(@"select count(p.product_serial_no) c from tb_product p 
 where p.tag=1 and p.split_line=0 and p.is_non=0 and p.menu_child_serial_no={0} and  p.product_current_cost<>p.product_current_cost_2 ", categoryID)).ToString());
    }
  
    public DataTable ExportTOExcel(int menu_child_serial_no, int tag, string regdate_day)
    {
        return Config.ExecuteDataTable(@"select 	product_serial_no sku	, product_order priority
	, product_name middle_name
	, product_short_name short_name
	, tag showit
	, producter_serial_no manufacturer
	, producter_url manufacturer_url
	, manufacturer_part_number 
	, supplier_sku
    
	
 	,hot, 
	new, 
	split_line, 
	product_name_long_en long_name, 
	product_img_sum img_sum, 
	keywords, 
	other_product_sku, 
	export,
    product_current_cost cost,
    round(product_current_price/1.022, 2) special_cost_price
    ,product_store_sum store_sum
    ,model
	from tb_product where menu_child_serial_no='" + menu_child_serial_no.ToString() + "' " + (tag == 1 ? " and tag=1 " : "") + " " + (regdate_day == string.Empty ? "" : " and date_format(regdate ,'%Y%j') >= " + regdate_day.ToString() + "  order by priority desc"));
    }

    public bool FindPartIsExistByManufacture(string menufacture)
    {
        if (Config.ExecuteScalar("select count(product_serial_no) c from tb_product where manufacturer_part_number='" + menufacture + "'").ToString() == "0")
            return false;
        else
            return true;
    }

    public DataTable FindPartNameSKUManufacture(int categoryid, bool is_split_line,int _start_recound,int _pagesize,ref int count)
    {
        count = int.Parse(Config.ExecuteScalar(string.Format(@"Select count(product_serial_no) 
from tb_product 
where tag=1 and " + (is_split_line == false ? " split_line=0  and " : "") + " is_non=0 and menu_child_serial_no='" + categoryid + @"' 
        order by product_order asc ")).ToString());
        
        return Config.ExecuteDataTable(string.Format(@"Select product_serial_no, product_name,product_short_name, manufacturer_part_number, product_store_sum, product_current_price, product_current_cost
, case when product_store_sum >2 then 2 
when ltd_stock >2 then 2 
when product_store_sum + ltd_stock >2 then 2 
when product_store_sum  <=2 and product_store_sum >0 then 3
when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3
when ltd_stock <=2 and ltd_stock >0 then 3
when product_store_sum+ltd_stock =0 then 4 
when product_store_sum+ltd_stock <0 then 5 end as ltd_stock 
, case when other_product_sku > 0 then other_product_sku
    else product_serial_no end as img_sku, date_format(regdate, '%b %d %Y %r') regdate,date_format(last_regdate, '%b %d %Y %r') last_regdate
   , (product_current_price-product_current_discount) product_current_sold
        ,product_current_special_cash_price
,menu_child_serial_no
,split_line
from tb_product where tag=1 and " + (is_split_line == false ? " split_line=0  and " : "") + " is_non=0 and menu_child_serial_no='" + categoryid + "' order by product_order asc limit {0},{1}",_start_recound, _pagesize));
    }
    public DataTable FindPartNameSKUManufacture(int categoryid, ref int count)
    {
        return FindPartNameSKUManufacture(categoryid, false, 0, 1000, ref count);
    }
    public DataTable FindPartNameSKUManufacture(string lu_skus, ref int count)
    {
        return FindPartNameSKUManufacture(lu_skus, false,0, 1000, ref count);
    }
    public DataTable FindPartNameSKUManufacture(string lu_skus, bool is_split_line, int _start_recound,int _pagesize, ref int count)
    {
        count = int.Parse(Config.ExecuteScalar(@"Select count(product_serial_no)
from tb_product where tag=1 and " + (is_split_line == false ? " split_line=0  and " : "") + " is_non=0 and product_serial_no in(" + lu_skus + ") order by product_order asc ").ToString());
        return Config.ExecuteDataTable(string.Format(@"Select product_serial_no, product_name,product_short_name, manufacturer_part_number, product_store_sum, product_current_price, product_current_cost
, case when product_store_sum >2 then 2 
when ltd_stock >2 then 2 
when product_store_sum + ltd_stock >2 then 2 
when product_store_sum  <=2 and product_store_sum >0 then 3
when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3
when ltd_stock <=2 and ltd_stock >0 then 3
when product_store_sum+ltd_stock =0 then 4 
when product_store_sum+ltd_stock <0 then 5 end as ltd_stock 
, case when other_product_sku > 0 then other_product_sku
    else product_serial_no end as img_sku, regdate, last_regdate
, (product_current_price-product_current_discount) product_current_sold
,product_current_special_cash_price
,menu_child_serial_no
,split_line
from tb_product where tag=1 and " + (is_split_line == false ? " split_line=0  and " : "") + " is_non=0 and product_serial_no in(" + lu_skus + ") order by product_order asc limit {0},{1}", _start_recound, _pagesize));
    }

    public DataTable FindModelToStoreAndCostAndMenufacture()
    {
        return Config.ExecuteDataTable(@"select 1 Ltd_code,product_serial_no Ltd_sku,
product_current_cost Ltd_cost,
 product_store_sum Ltd_stock,
manufacturer_part_number Ltd_manufacture_code,
product_name Ltd_part_name
from tb_product where tag=1 and split_line=0 and is_non=0 and menu_child_serial_no in (

select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_pre_serial_no=0 and menu_parent_serial_no=1 and page_category=1
)
union all 
select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=1 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_pre_serial_no=0 and menu_parent_serial_no=1 and page_category=1
))

) order by menu_child_serial_no, product_order asc ");
    }

    /// <summary>
    /// 所有有效产品的数量
    /// </summary>
    /// <returns></returns>
    public int FindModelSum()
    {
        return int.Parse(Config.ExecuteScalar(@"select count(product_serial_no)
from tb_product where tag=1 and split_line=0 and is_non=0 and menu_child_serial_no in (

select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_pre_serial_no=0 and menu_parent_serial_no=1 and page_category=1
)
union all 
select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=0 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_is_exist_sub=1 and menu_pre_serial_no in (
select menu_child_serial_no from tb_product_category where tag=1 and menu_pre_serial_no=0 and menu_parent_serial_no=1 and page_category=1
))

) ").ToString());
    }
    /// <summary>
    /// 用于与上家产品比较中
    /// </summary>
    /// <param name="categoryID"></param>
    /// <returns></returns>
    public DataTable FindModelUseComparePrice(int categoryID)
    {
        return Config.ExecuteDataTable(@"select product_serial_no,product_current_cost, product_name, manufacturer_part_number, (product_current_price) sold, case when product_current_discount=0 then '' else product_current_discount end as product_current_discount
,(select rival_price from tb_rival_store where rival_ltd_id=1 and rival_manufacture_code=p.manufacturer_part_number limit 0,1) ncix_price
, p.menu_child_serial_no
    from tb_product p
where menu_child_serial_no='" + categoryID.ToString() + "' and tag=1 and split_line=0 and is_non=0");
    }

    public static int FindSkuByManufacture(string menufacturer_part_number)
    {
        DataTable dt = Config.ExecuteDataTable(@"
select product_serial_no from tb_product where manufacturer_part_number='" + menufacturer_part_number + "' limit 0,1");
        if (dt.Rows.Count == 1)
        {
            int sku;
            int.TryParse(dt.Rows[0][0].ToString(), out sku);
            return sku;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// Get Stock by SKU.
    /// </summary>
    /// <param name="lu_sku"></param>
    /// <param name="ltd_stock"></param>
    /// <returns></returns>
    public string FindStockByLuSku(int lu_sku, int ltd_stock)
    {
        if (lu_sku != -1)
        {
            object o = Config.ExecuteScalar(@"select case when product_store_sum >2 then 2 
when ltd_stock >2 then 2 
when product_store_sum + ltd_stock >2 then 2 
when product_store_sum  <=2 and product_store_sum >0 then 3
when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3
when ltd_stock <=2 and ltd_stock >0 then 3
when product_store_sum+ltd_stock =0 then 4 
when product_store_sum+ltd_stock <0 then 5 end as ltd_stock 
			from tb_product where product_serial_no='" + lu_sku + "' ");
            int.TryParse(o.ToString(), out ltd_stock);
        }
       return GetShowStockString(ltd_stock);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="stock_quantity"></param>
    /// <returns></returns>
    public string GetShowStockString(int stock_quantity)
    {
        string stock_string;
        if (stock_quantity == 1)
            stock_string = "<span style='color:green; font-size:8.5pt'>In Stock</span>";
        else if (stock_quantity == 2)
            stock_string = "<span style='color:green; font-size:8.5pt' >Stock Available</span>";
        else if (stock_quantity == 3)
            stock_string = "<span style='color:green; font-size:8.5pt' >Stock Low(Call)</span>";
        else if (stock_quantity == 4)
            stock_string = "<span style='color:#cccccc; font-size:8.5pt'>Out of Stock</span>";
        else
            stock_string = "<span style='color:#cccccc; font-size:8.5pt'>Back Order</span>";

        return stock_string;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="lu_sku"></param>
    /// <param name="ltd_stock"></param>
    /// <returns></returns>
    public string FindStockByLuSkuForOrder(int lu_sku, int ltd_stock)
    {
        if (lu_sku != -1 && lu_sku > 0)
        {
            try
            {
                object o = Config.ExecuteScalar(@"select case when product_store_sum >2 then 2 
when ltd_stock >2 then 2 
when product_store_sum + ltd_stock >2 then 2 
when product_store_sum  <=2 and product_store_sum >0 then 3
when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3
when ltd_stock <=2 and ltd_stock >0 then 3
when product_store_sum+ltd_stock =0 then 4 
when product_store_sum+ltd_stock <0 then 5 end as ltd_stock 
			from tb_product where product_serial_no='" + lu_sku + "' ");
                int.TryParse(o.ToString(), out ltd_stock);
            }
            catch { ltd_stock = 0; }
        }
        else
            return "";

        string stock_string;
        if (ltd_stock == 1)
            stock_string = "";//"<span style='color:green; font-size:8.5pt'>In Stock</span>";
        else if (ltd_stock == 2)
            stock_string = "<span style='color:green; font-size:8.5pt' ></span>";
        else if (ltd_stock == 3)
            stock_string = "<span style='color:green; font-size:8.5pt' ></span>";
        else if (ltd_stock == 4)
            stock_string = "<span style='color:red; font-size:8.5pt'>**</span>";
        else
            stock_string = "<span style='color:red; font-size:8.5pt'>**</span>";

        return stock_string;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryID"></param>
    /// <param name="showit"></param>
    /// <returns></returns>
    public DataTable FindSimpleDownload(int categoryID, bool showit)
    {
        return Config.ExecuteDataTable(string.Format(@"select product_serial_no sku, tag showit ,
 product_name middle_name, product_short_name short_name,
split_line, product_order priority, 
manufacturer_part_number
,product_current_cost cost, product_current_price price from tb_product p

where menu_child_serial_no='{0}' and {1} order by product_order asc", categoryID, (showit ? "  tag=1 " : "")));
    }

    public static bool IsValid(int lu_sku)
    {
        int count = Config.ExecuteScalarInt32(string.Format(@"select count(product_serial_no) from tb_product where tag=1 and menu_child_serial_no in ({0})", new GetAllValidCategory().ToString()));
        if (count > 0)
            return true;
        else
            return false;
    }
}
