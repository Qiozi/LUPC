// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-09-22 10:34:30
//
// // // // // // // // // // // // // // // //
using System;
using System.Data;

/// <summary>
/// Summary description for OrderSystemFinishDateModel
/// </summary>

[Serializable]
public class OrderSystemFinishDateModel 
{

    public string FindDateBySystemCode(int system_code)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select date_format(finish_date,'%Y-%m-%d') from tb_order_system_finish_date 
where system_code='{0}' order by regdate desc limit 0,1", system_code));
        if (dt.Rows.Count == 1)
            return dt.Rows[0][0].ToString();
        else
            return "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FindModelByOrderCode(int order_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select date_format(finish_date,'%Y-%m-%d') finish_date, regdate from tb_order_system_finish_date
 where order_code='{0}' ", order_code));
    }
}

