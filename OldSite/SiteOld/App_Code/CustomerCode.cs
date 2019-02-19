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
        //string id = customer_serial_no.Trim();
        //int pre_two = Config.invoice_customer_head;
        //if (id.Length == 4)
        //{
        //    return "7" + pre_two.ToString() + id;
        //}
        //if (id.Length == 5)
        //{
        //    return "7" + (pre_two + int.Parse(id.Substring(0, 1))).ToString() + id;
        //}
        //if (id.Length < 4)
        //{
        //    return "7" + pre_two.ToString() + int.Parse(id).ToString("0000");
        //}
        //else
        //    throw new Exception("Customer ID Error!");
        return customer_serial_no;
    }

    public static int NewCustomerCode()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        StoreCustomerCodeModel cm = StoreCustomerCodeModel.FindFirst(oo, le);
        string code = cm.CustomerCode;
        cm.Delete();
        return int.Parse(code);
    }


    public static int NewSysCode()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        StoreSysCodeModel sscm = StoreSysCodeModel.FindFirst(oo, le);
        string code = sscm.SysCode;
        sscm.Delete();
        return int.Parse(code);
    }

    public static int NewOrderCode()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("ID", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        NHibernate.Expression.GtExpression le = new NHibernate.Expression.GtExpression("ID", 0);
        StoreOrderCodeModel socm = StoreOrderCodeModel.FindFirst(oo, le);
        int code = socm.OrderCode;
        socm.Delete();
        return code;
    }

}
