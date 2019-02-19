using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ErrorHelper
/// </summary>
public class ErrorHelper
{
    public ErrorHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 保存程序出错纪录
    /// </summary>
    /// <param name="e"></param>
    /// <param name="db"></param>
    public static void Save(Exception e, nicklu2Entities db)
    {
        var error = new LU.Data.tb_error_log
        {
            comment = e.Message,
            summary = e.Source,
            regdate = DateTime.Now
        };
        db.tb_error_log.Add(error);
        db.SaveChanges();
    }
}