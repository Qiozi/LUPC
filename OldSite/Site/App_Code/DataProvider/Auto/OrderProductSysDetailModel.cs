
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	23/05/2009 11:07:46 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class OrderProductSysDetailModel
{
    public static tb_order_product_sys_detail GetOrderProductSysDetailModel(nicklu2Entities context, int sys_tmp_detail)
    {
        return context.tb_order_product_sys_detail.FirstOrDefault(me => me.sys_tmp_detail.Equals(sys_tmp_detail));
    }

    public static DataTable GetModelsBySysCode(string sys_tmp_code)
    {
        Config.ExecuteNonQuery(@"update tb_order_product_sys_detail set product_order = (select max(menu_child_order) from tb_product_category pc inner join tb_product p on p.menu_child_serial_no=pc.menu_child_serial_no where p.product_serial_no = tb_order_product_sys_detail.product_serial_no)
 where sys_tmp_code='" + sys_tmp_code + "'");
        return Config.ExecuteDataTable(@"select sp.part_quantity,sp.product_serial_no,sp.product_name,sp.cate_name,(sp.product_current_price-sp.save_price) product_current_price ,sp.product_current_cost
    from tb_order_product_sys_detail sp 
    where sys_tmp_code='" + sys_tmp_code + "' order by product_order asc ");

    }

}

