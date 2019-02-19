using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Codes
/// </summary>
public class Codes
{
	public Codes()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 新的客户编号
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static int NewCustomerCode(nicklu2Model.nicklu2Entities db)
    {
        int code = 0;
        var c = db.tb_store_customer_code.FirstOrDefault(p => true);
        if (c != null)
            code = int.Parse(c.CustomerCode);
        db.DeleteObject(c);
        db.SaveChanges();
        return code;
    }

    /// <summary>
    /// 新的系统号
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static int NewSysCode(nicklu2Model.nicklu2Entities db)
    {
        int code = 0;
        var c = db.tb_store_sys_code.FirstOrDefault(p => true);
        if (c != null)
            code = int.Parse(c.SysCode);
        db.DeleteObject(c);
        db.SaveChanges();
        return code;
    }

    /// <summary>
    /// 新的订单号
    /// </summary>
    /// <param name="db"></param>
    /// <returns></returns>
    public static int NewOrderCode(nicklu2Model.nicklu2Entities db)
    {

        int code = 0;// socm.OrderCode;
        var c = db.tb_store_order_code.FirstOrDefault(p => true);
        if (c != null)
            code = c.OrderCode.Value;
        db.DeleteObject(c);
        db.SaveChanges();

        if (db.tb_customer_store.FirstOrDefault(p => p.order_code.HasValue
            && p.order_code.Value.Equals(code)) == null)
            return code;
        else
           return NewOrderCode(db);
    }
}