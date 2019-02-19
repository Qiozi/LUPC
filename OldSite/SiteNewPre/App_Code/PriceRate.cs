using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PriceRate
/// </summary>
public class PriceRate
{
	public PriceRate()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //public static decimal GetPrice(decimal CADPrice, decimal rate)
    //{
    //    return Multiply(CADPrice , rate);
    //}

    public static string Format(decimal price)
    {
        return price.ToString("##,###,##0.00");
    }

    /// <summary>
    /// 两个decimal 相乖 确定到两位小数
    /// </summary>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <returns></returns>
    public static decimal Multiply(decimal p1, decimal p2)
    {
        return decimal.Parse((p1 * p2).ToString("0.00"));
    }


    /// <summary>
    /// 价格转换， 如果是美国，转为美金价
    /// </summary>
    /// <param name="CADPrice"></param>
    /// <param name="CT"></param>
    /// <param name="Rate"></param>
    /// <returns></returns>
    public static decimal ConvertPrice(decimal CADPrice, CountryType CT, decimal Rate)
    {
        if (CT == CountryType.CAD)
            return CADPrice;
        else
            return Multiply(CADPrice, Rate); // USD
    }
}