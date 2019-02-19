// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-5 13:35:59
//
// // // // // // // // // // // // // // // //
using System;
using System.Data;

[Serializable]
public class SpTmpModel 
{
    public static DataTable GetModelsByTmpCode(string sys_tmp_code)
    {
        string sql = "select st.*,sp.sys_tmp_cost,sp.sys_tmp_price,sp.sys_tmp_price,sp.sys_tmp_product_name from tb_sp_tmp sp inner join tb_ebay_system st on st.id=sp.system_templete_serial_no where sys_tmp_code ='" + sys_tmp_code + "'";
        return Config.ExecuteDataTable(sql);
    }
    public static DataTable GetModelsByTmpCodeNotConationSystemTemplete(string sys_tmp_code)
    {
        string sql = "select sp.* from tb_sp_tmp sp where sys_tmp_code ='" + sys_tmp_code + "'";
        return Config.ExecuteDataTable(sql);
    }
}
