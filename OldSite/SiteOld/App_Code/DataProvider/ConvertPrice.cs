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
/// Summary description for ConvertPrice
/// </summary>
public class ConvertPrice
{
	public ConvertPrice()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// convert price format.
    /// </summary>
    /// <param name="current_price"></param>
    /// <param name="card_rate"></param>
    /// <returns></returns>
    public static decimal ChangePrice(decimal current_price, decimal card_rate)
    {
        //decimal price = 0M;
        // current_price = current_price;// *card_rate;
        //if (current_price < 100M)
        //{
        //    price = decimal.Parse(current_price.ToString("###")) - 0.01M;
        //}
        //else
        //{
        //    price = decimal.Parse(current_price.ToString("###")) - 0.01M;
        //}
        return current_price;
    }

    public static decimal CurrentCurrencyConverter = 1M;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="cc"></param>
    /// <param name="ca_price"></param>
    /// <returns></returns>
    public static decimal Price(CountryCategory cc,decimal ca_price)
    {
        if (cc != CountryCategory.CA)
        {
            return ca_price * CurrentCurrencyConverter;
        }
        return ca_price;
    }

    public static decimal ChangePriceToNotCard(decimal current_price, decimal rate)
    {
        if (current_price == 0M)
            return 0M;
        return  decimal.Parse((current_price / rate).ToString("###.00"));
    }

    public static decimal ChangePriceToNotCard(decimal current_price)
    {
        return ChangePriceToNotCard(current_price, Config.is_card_rate);
    }

    public static decimal SpecialCashPriceDiscount(decimal current_price)
    {
        return current_price - ChangePriceToNotCard(current_price, Config.is_card_rate);
    }
    /// <summary>
    /// update tb_product set product_current_special_cash_price= round(product_current_price/1.022, 2) + (100-right(round(product_current_price/1.022, 2),2))/100 - 0.01
    /// </summary>
    /// <param name="SpecialCashPrice"></param>
    /// <returns></returns>
    public static decimal SpecialCashPriceConvertToCardPrice(decimal SpecialCashPrice)
    {
        //return  decimal.Parse((ChangePriceToNotCard(SpecialCashPrice) + (100M - decimal.Parse((SpecialCashPrice / Config.is_card_rate).ToString(".00"))) / 100M - 0.01M).ToString());
        if (SpecialCashPrice == 0M)
            return 0M;
        return decimal.Parse((SpecialCashPrice * Config.is_card_rate).ToString("###")) - 0.01M;
    }
    /// <summary>
    /// ex: 5.055 -> 5.06
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal RoundPrice(decimal price)
    {
        return decimal.Parse(price.ToString("###.00"));
    }
    /// <summary>
    /// ex: 5.23 -> 5
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal RoundPrice2(decimal price)
    {
        return decimal.Parse(price.ToString("###"));
    }

    public static string RoundPrice(string price)
    {
        decimal _p;
        decimal.TryParse(price, out _p);
        return decimal.Parse(_p.ToString("###.00")).ToString("$###,###.00");
    }
    /// <summary>
    /// 'ebay：1-50  6%，  51-1000 3.75%， 1000+ 1%
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public decimal ChangePriceToEbay(decimal regular_price)
    {
        //decimal price;
        decimal result;
        result = 0M;

        if (regular_price <= 50M && regular_price > 0M)
            result = regular_price + regular_price * 0.06M;
        else if (regular_price > 50M && regular_price <= 1000M)
            result = regular_price + 50 * 0.06M + (regular_price - 50) * 0.0375M;
        else if (regular_price > 1000M)
            result = regular_price + 50 * 0.06M + (regular_price - 50) * 0.0375M + (regular_price - 50 - 1000) * 0.01M;

        return result;
    }
}
