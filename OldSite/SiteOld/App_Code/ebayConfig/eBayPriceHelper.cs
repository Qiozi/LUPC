
/// <summary>
/// Summary description for eBayPriceHelper
/// </summary>
public class eBayPriceHelper
{
    public eBayPriceHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// each additional item Price
    /// </summary>
    /// <param name="shipping_fee"></param>
    /// <returns></returns>
    public decimal eachAddItemShipping(decimal shipping_fee, decimal minShipping)
    {
        if (shipping_fee >= minShipping)
            shipping_fee -= minShipping;

        if (shipping_fee == 0M) return 0M;
        return decimal.Parse((shipping_fee / 1.33M).ToString("###"));
    }

    public decimal eachAddItemShipping(decimal shipping_fee)
    {
        return eachAddItemShipping(shipping_fee, 0);
    }

    public decimal eBayNetbookPartPrice(decimal cost
        , decimal screen
        , decimal adjustment
        , ref decimal shipping_fee
        , ref decimal profit
        , ref decimal ebay_fee
        , ref decimal bank_fee)
    {
        shipping_fee = eBayShipping.BasicShippingFee(screen);
        return decimal.Parse(PR_part(cost, shipping_fee, adjustment, ref profit, ref ebay_fee, ref bank_fee).ToString("000")) - 0.01M;
    }

    /// <summary>
    /// 计算发布到eBay 上的价格
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="shipping_fee"></param>
    /// <param name="adjustment"></param>
    /// <returns></returns>
    public decimal PR_part(decimal cost, decimal shipping_fee, decimal adjustment
        , ref decimal profit
        , ref decimal ebay_fee
        , ref decimal bank_fee)
    {
        decimal a = cost;
        decimal b = shipping_fee;
        decimal e = adjustment;
        decimal c = 0M;

        if (a > 0M && a < 500M)
        {
            profit = 23M;
            bank_fee = (a + profit) * 0.022M;

            c = (a + profit) * 1.022M;
            
        }
        else if (a > 500M && a < 1500M)
        {
            profit = a * 0.05M;
            bank_fee = (a + profit) * 0.022M;

            c = (a + profit) * 1.022M;
        }
        else
        {
            profit = a * 0.045M;
            bank_fee = (a + profit) * 0.022M;

            c = (a + profit) * 1.022M;
        }

        c = c + e;

        profit += e;

        if (c > 0M && c < 1000M)
        {
            ebay_fee = (4M + (c - 50M) * 0.05M) * 1.022M;
            return c + ebay_fee + shipping_fee;
        }
        else
        {
            ebay_fee = (4M + 950M * 0.05M + (c - 1000M) * 0.02M) * 1.022M;
            return c + ebay_fee + shipping_fee;
        }
    }

   

    /// <summary>
    /// get eBay part Title.
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public string GetEbayTitle(decimal price, int SystemSKU)
    {
        if (price > 0M && price <= 700M)
            return "Office Desktop PC";
        else if (price > 700.01M && price <= 1000M)
            return "Highend Gaming";
        else if (price > 1000M && price <= 1500M)
            return "Extreme Gaming";
        else
            return "Ultimate Gaming";

    }

    #region old eBay price


    /// <summary>
    /// new ebay sys price
    /// 2012-07-18
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="shipping_fee"></param>
    /// <param name="adjustment"></param>
    /// <param name="profits"></param>
    /// <param name="ebay_fee"></param>
    /// <returns></returns>
    public static decimal SysPRNew(decimal cost
        , decimal shipping_fee
        , decimal adjustment
        , ref decimal profits
        , ref decimal ebay_fee)
    {
        profits = cost * 0.10M;
        
        decimal paypal = PaypalFee(cost);
        decimal fvf = FVF(cost, shipping_fee, adjustment);
        ebay_fee = paypal + fvf;

        return ebay_fee + cost + adjustment + shipping_fee + profits;
    }

    /// <summary>
    /// ebay fee
    /// 抽点数
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="shippingFee"></param>
    /// <param name="adjustment"></param>
    /// <returns></returns>
    public static decimal FVF(decimal cost, decimal shippingFee, decimal adjustment)
    {
        decimal f = (cost + shippingFee + adjustment) * 1.17M;
        if (f > 0M && f <= 50M)
        {
            return f * 0.07M;
        }
        else if (f > 50.01M && f < 1000M)
        {
            return 3.5M + (f - 50M) * 0.05M;
        }
        else
            return 51M + (f - 1000M) * 0.02M;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="price"></param>
    /// <returns></returns>
    public static decimal PaypalFee(decimal price){
        return price * 1.10M * 0.022M;
    }

    #endregion

}
