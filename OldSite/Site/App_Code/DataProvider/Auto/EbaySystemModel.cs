
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/26/2010 9:36:09 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class EbaySystemModel 
{
    public static tb_ebay_system GetEbaySystemModel(nicklu2Entities context, int systemId)
    {
        return context.tb_ebay_system.FirstOrDefault(me => me.id.Equals(systemId));
    }

    /// <summary>
    /// 取得子产品已禁售的系统
    /// </summary>
    /// <returns></returns>
    public static DataTable FindSystemByWarn()
    {
        return Config.ExecuteDataTable(@"select distinct st.id, st.ebay_system_name system_templete_name from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku
inner join tb_ebay_system st on st.id=sp.system_sku
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
where  st.showit=1 and (pc.tag=0 or  p.tag=0  or (p.product_store_sum<1 and p.ltd_stock < 1 and p.is_non=0 and p.split_line=0) ) and p.menu_child_serial_no <> 260");
        // 260 是超频产品
    }
      

}

