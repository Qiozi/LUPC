using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Payment
/// </summary>
public class Payment
{
	public Payment()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// 取得支付方式的文本
    /// 
    /// </summary>
    /// <param name="payId"></param>
    /// <param name="db"></param>
    /// <returns></returns>
    public static string GetPaymentText(int payId, nicklu2Entities db)
    {
        var pay = db.tb_pay_method_new.FirstOrDefault(p => p.pay_method_serial_no.Equals(payId));
        return (pay == null ? "" : pay.pay_method_name);
    }
}