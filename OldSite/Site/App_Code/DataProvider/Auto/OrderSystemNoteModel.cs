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
public class OrderSystemNoteModel 
{	
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_code"></param>
    /// <returns></returns>
    public DataTable FindNoteBySystemCode(int system_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select note, regdate from tb_order_system_note where system_code='{0}'", system_code));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FindModelByOrderCode(int order_code)
    {
        return Config.ExecuteDataTable(string.Format(@"select note, regdate from tb_order_system_note where order_code='{0}'", order_code));
   
    }
}
