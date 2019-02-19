using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LU.Data;

/// <summary>
/// Summary description for CustomerCode
/// </summary>
public class Code
{
	public Code()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Get Customer Code of Invoice on that send email to customer
    /// </summary>
    /// <param name="customer_serial_no"></param>
    /// <returns></returns>
    public static string FilterCustomerCode(string customer_serial_no)
    {
       
        return customer_serial_no;
    }

    public static int NewCustomerCode(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        //StoreCustomerCodeModel cm = StoreCustomerCodeModel.FindFirst(oo, le);
        //string code = cm.CustomerCode;
        //cm.Delete();
        //return int.Parse(code);

        var query = context.tb_store_customer_code.FirstOrDefault(me => me.ID > 0);
        var code = query.CustomerCode;
        context.tb_store_customer_code.Remove(query);
        context.SaveChanges();
        return int.Parse(code);
    }


    public static int NewSysCode(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        //StoreSysCodeModel sscm = StoreSysCodeModel.FindFirst(oo, le);
        //string code = sscm.SysCode;
        //sscm.Delete();
        //return int.Parse(code);

        var query = context.tb_store_sys_code.FirstOrDefault(me => me.ID > 0);
        var code = query.SysCode;
        context.tb_store_sys_code.Remove(query);
        context.SaveChanges();
        return int.Parse(code);
    }

    public static int NewOrderCode(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        //StoreOrderCodeModel socm = StoreOrderCodeModel.FindFirst(oo, le);
        //int code = socm.OrderCode;
        //socm.Delete();
        //return code;
        var query = context.tb_store_order_code.FirstOrDefault(me => me.ID > 0);
        var code = query.OrderCode.Value;
        context.tb_store_order_code.Remove(query);
        context.SaveChanges();
        return code;
    }

}
