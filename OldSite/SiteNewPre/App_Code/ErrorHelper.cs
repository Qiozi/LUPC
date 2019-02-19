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
    public static void Save(Exception e, nicklu2Model.nicklu2Entities db)
    {
        var error = nicklu2Model.tb_error_log.Createtb_error_log(0);
        error.comment = e.Message;
        error.summary = e.Source;
        error.regdate = DateTime.Now;
        db.AddTotb_error_log(error);
        db.SaveChanges();
    }
}