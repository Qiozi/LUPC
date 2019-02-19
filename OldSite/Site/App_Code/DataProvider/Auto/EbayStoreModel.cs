// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-10-01 15:00:26
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;


[Serializable]
public class EbayStoreModel
{
    public static tb_ebay_store GetEbayStoreModel(nicklu2Entities context, int id)
    {
        return context.tb_ebay_store.Single(me => me.id.Equals(id));
    }

    public DataTable FindModelsBySystem(int pagesize, int startrecord, string keyword, ref int count,int store_type)
    {
        // system 
        if (store_type == 2)
        {
            string sql_search = "";
            if (keyword != string.Empty)
            {
                sql_search += string.Format(" and ( e.ebay_code='{0}' or e.id={0})", keyword);

            }
            count = int.Parse(Config.ExecuteScalar(string.Format(@"select count(e.id)
 from tb_ebay_store e
left join 
(select case when other_product_sku>0 then p.other_product_sku else p.product_serial_no end img_sku,  es.ebay_store_id from tb_ebay_store_detail es inner join tb_product p on p.product_serial_no=es.lu_sku
		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
		inner join (select max(computer_case_category) ccc from tb_computer_case) c on c.ccc=pc.menu_child_serial_no or pc.menu_pre_serial_no=c.ccc
		) img
on img.ebay_store_id=e.id where ebay_store_type=2 {0}", sql_search)).ToString());
            return Config.ExecuteDataTable(string.Format(@"select e.*,ifnull(img.img_sku,999999) img_sku
,pr.price lu_price, pr.cost lu_cost
 from tb_ebay_store e
left join 
(select case when other_product_sku>0 then p.other_product_sku else p.product_serial_no end img_sku,  es.ebay_store_id from tb_ebay_store_detail es inner join tb_product p on p.product_serial_no=es.lu_sku
		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
		inner join (select max(computer_case_category) ccc from tb_computer_case) c on c.ccc=pc.menu_child_serial_no or pc.menu_pre_serial_no=c.ccc
		) img
on img.ebay_store_id=e.id 
inner join (
select distinct ebay_store_id, sum(product_current_price) price, sum(product_current_cost) cost from tb_ebay_store_detail esd inner join tb_product p on p.product_serial_no=esd.lu_sku
group by ebay_store_id
) pr on pr.ebay_store_id=e.id

where ebay_store_type=2 {2} limit {0},{1}", startrecord, pagesize, sql_search));
        }
        else
        {
            string sql_search = "";
            if (keyword != string.Empty)
            {
                sql_search += string.Format(" and ( e.ebay_code='{0}' or e.lu_ebay_sku='{0}' or e.id={0})", keyword);

            }

            // part 
            count = int.Parse(Config.ExecuteScalar(string.Format(@"select count(e.id)
 from tb_ebay_store e
where ebay_store_type=1 {0}", sql_search)).ToString());

            return Config.ExecuteDataTable(string.Format(@"select e.*,case when p.other_product_sku > 0 then p.other_product_sku else p.product_serial_no end as img_sku
,p.product_current_price lu_price, p.product_current_cost lu_cost
 from tb_ebay_store e inner join tb_product p on p.product_serial_no = e.lu_ebay_sku
where ebay_store_type=1 {2} limit {0},{1}", startrecord, pagesize, sql_search));

        }
    }
}
