// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-5 10:42:04
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class SpTmpDetailModel
{

    
    public static tb_sp_tmp_detail[] GetModelsBySysTmpCode(nicklu2Entities context, string sys_tmp_code)
    {
        //return SpTmpDetailModel.FindAllByProperty("sys_tmp_code", sys_tmp_code);
        return context.tb_sp_tmp_detail.Where(me => me.sys_tmp_code.Equals(sys_tmp_code)).ToList().ToArray();
    }

    public static DataTable GetModelsBySysCode(string sys_tmp_code)
    {
        return Config.ExecuteDataTable(@"select part_quantity,p.product_serial_no,p.product_name,p.product_short_name,(p.product_current_price-p.product_current_discount) product_current_price ,sp.sys_tmp_detail, p.is_non,p.product_current_cost from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sys_tmp_code='" + sys_tmp_code + "' order by sp.product_order asc");
    }


    public static decimal GetPriceSUM(int sys_tem_code)
    {
        string sql = "select ifnull(sum(product_current_price), 0) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return 0M;
    }

    public static decimal GetPriceCostSUM(int sys_tem_code)
    {
        string sql = "select sum(product_current_cost) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return -1;
    }
    public static decimal GetPriceSaveCostSUM(int sys_tem_code)
    {
        string sql = "select sum(save_price) from tb_sp_tmp_detail where sys_tmp_code='" + sys_tem_code + "'";
        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
            return decimal.Parse(dt.Rows[0][0].ToString());
        return -1;
    }

    public static int GetNewCode(nicklu2Entities context, int SysID)
    {
 
        int code = Code.NewSysCode(context);
        //SystemCodeStoreModel scsm = new SystemCodeStoreModel();
        //scsm.create_datetime = DateTime.Now;
        //scsm.is_buy = true;
        //scsm.system_code = code;
        //scsm.ip = "127.0.0.1";
        //scsm.system_templete_serial_no = SysID;
        //scsm.Create();
        //return code;

        var model = new tb_system_code_store
        {
            create_datetime = DateTime.Now,
            is_buy = true,
            system_code = code,
            ip = "127.0.0.1",
            system_templete_serial_no = SysID
        };
        context.tb_system_code_store.Add(model);
        context.SaveChanges();
        return code;
    }

    public static void DeleteBySysCode(int sys_temp_code)
    {
        string sql = "delete from tb_sp_tmp_detail where sys_tmp_code='" + sys_temp_code + "'";
        Config.ExecuteNonQuery(sql);
    }
}
