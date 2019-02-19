using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// Summary description for setting
/// </summary>
public class setting
{
    public setting()
    {
        //
        // TODO: Add constructor logic here
        //
    }

   

    /// <summary>
    /// 订单在前台显示的状态：完成
    /// 
    /// </summary>
    public static int ORDER_STATUS_FINISHED = 7;

    /// <summary>
    /// 默认订单状态，前台
    /// </summary>
    public static sbyte ORDER_PRE_STATUS = 8;

    /// <summary>
    /// 默认订单状态，后台
    /// </summary>
    public static sbyte ORDER_BACK_STATUS = 12;

    /// <summary>
    /// Service not available.
    /// </summary>
    public static string ServiceNotAvailable = "Service not available.";
    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailUserName
    {
        get { return ConfigurationManager.AppSettings["mailUserName"].ToString(); }
    }
    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailPassword
    {
        get { return ConfigurationManager.AppSettings["mailPassword"].ToString(); }
    }
    /// <summary>
    /// jmail 
    /// </summary>
    public static string mailServer
    {
        get { return ConfigurationManager.AppSettings["mailServer"].ToString(); }
    }

    /// <summary>
    /// 信用卡手续费百分比 0.02
    /// </summary>
    public static decimal CardRate = 0.02M;
    /// <summary>
    /// HST 税率的洲
    /// </summary>
    public static List<int> HstStates = new List<int>() { 4, 5, 6, 8, 2 };

    /// <summary>
    /// 自提
    /// </summary>
    public static List<int> LocalAll = new List<int>() { 22, 23, 24, 21 };

    /// <summary>
    /// 信用卡价格的支付方式ID 
    /// </summary>
    public static List<int> PriceIsCard = new List<int>() { 14, 15, 20, 25, 24 };

    /// <summary>
    /// paypal 支付
    /// </summary>
    public static int PaymentPaypal = 15;

    /// <summary>
    /// paypal 信用卡 
    /// </summary>
    public static int PaymentPaypalCard = 25;

    /// <summary>
    /// email transfer 支付 （只支持加拿大）
    /// </summary>
    public static int PaymentEmailTransfer = 16;
    /// <summary>
    /// bank transfer 支付 （只支持加拿大）
    /// </summary>
    public static int PaymentBankTransfer = 17;

    /// <summary>
    /// Cash Deposit 支付 （只支持加拿大）
    /// </summary>
    public static int PaymentCashDeposit = 18;

    /// <summary>
    /// Money Order 支付 
    /// </summary>
    public static int PaymentMoneyOrder = 19;

    /// <summary>
    /// Personal Check / Company Check  支付
    /// </summary>
    public static int PaymentPersonalCheckCompanyCheck = 20;

    /// <summary>
    /// 自提现鑫
    /// </summary>
    public static int LocalPickupCash = 22;
    /// <summary>
    /// 自提借记卡
    /// </summary>
    public static int LocalPickupDebitCart = 23;

    /// <summary>
    /// 自提信用卡
    /// </summary>
    public static int LocalPickupCreditCart = 24;

    /// <summary>
    /// 信用卡付款；
    /// Credit Card (Visa and Master Card only)
    /// </summary>
    public static int CreditCart = 25;

    /// <summary>
    /// 系统自定义配置能显示的所有商品零件类ID
    /// </summary>
    public static List<int> SysCustomizePartAccessoriesCommentID = new List<int>() {51
    , 50
    , 48
    , 47};

    /// <summary>
    /// 
    /// </summary>
    public static int NoneSelectedID = 16684;

    public static string ImgHost = "https://lucomputers.com/";

}