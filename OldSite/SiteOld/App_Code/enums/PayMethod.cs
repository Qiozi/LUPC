using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// PayMethod 的摘要说明
/// </summary>
public class PayMethodHelper
{
	public PayMethodHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static DataTable pay_method_ToDataTable()
    {
        return PropertiesAttributeHelper.EnumToDatatable(typeof(pay_method));
    }

    public static int pay_method_value(pay_method ct)
    {
        foreach (int i in Enum.GetValues(typeof(pay_method)))
        {
            if (Enum.GetName(typeof(pay_method), i) == ct.ToString())
            {
                return i;
            }
        }
        return -1;
    }
}

public enum pay_method
{
    /// <summary>
    /// MC
    /// </summary>
    [PropertiesAttribute("MC", "MC")]
    MC = 1,
    /// <summary>
    /// VS
    /// </summary>
    [PropertiesAttribute("VS", "VS")]
    VS = 2,
    /// <summary>
    /// AM
    /// </summary>
    [PropertiesAttribute("AM", "AM")]
    AM = 3,
    /// <summary>
    /// EMAIL
    /// </summary>
    [PropertiesAttribute("EMAIL", "EMAIL")]
    EMAIL = 4,
    /// <summary>
    /// BANK_TRANSFER
    /// </summary>
    [PropertiesAttribute("BANK_TRANSFER", "BANK_TRANSFER")]
    BANK_TRANSFER = 5,
    /// <summary>
    /// WIRE
    /// </summary>
    [PropertiesAttribute("WIRE", "WIRE")]
    WIRE = 6,
    /// <summary>
    /// CHECK
    /// </summary>
    [PropertiesAttribute("CHECK", "CHECK")]
    CHECK = 7,
    /// <summary>
    /// DIRECT_DEPOSIT
    /// </summary>
    [PropertiesAttribute("DIRECT_DEPOSIT", "DIRECT_DEPOSIT")]
    DIRECT_DEPOSIT = 8,
    /// <summary>
    /// DEBIT_CARD
    /// </summary>
    [PropertiesAttribute("DEBIT_CARD", "DEBIT_CARD")]
    DEBIT_CARD = 9,
    /// <summary>
    /// CASH
    /// </summary>
    [PropertiesAttribute("CASH", "CASH")]
    CASH = 10,
    /// <summary>
    /// MONEY_ORDER
    /// </summary>
    [PropertiesAttribute("MONEY_ORDER", "MONEY_ORDER")]
    MONEY_ORDER = 11,
    /// <summary>
    /// CERTIFIED_CHECK
    /// </summary>
    [PropertiesAttribute("CERTIFIED_CHECK", "CERTIFIED_CHECK")]
    CERTIFIED_CHECK = 12,
    /// <summary>
    /// DISCOVER
    /// </summary>
    [PropertiesAttribute("DISCOVER", "DISCOVER")]
    DISCOVER = 13

}
