using System.Configuration;

/// <summary>
/// Summary description for Settings
/// </summary>
public class EbaySettings
{
	public EbaySettings()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    //Get the Keys from App.Config file
    public static string devID = ConfigurationManager.AppSettings["EbayDevID"];
    public static string appID = ConfigurationManager.AppSettings["EbayAppID"];
    public static string certID = ConfigurationManager.AppSettings["EbayCertID"];

    //Get the Server to use (Sandbox or Production)
    public static string serverUrl = ConfigurationManager.AppSettings["EbayServerUrl"];

    //Get the User Token to Use
    public static string userToken = ConfigurationManager.AppSettings["EbayUserToken"];

    public static int siteID = int.Parse(ConfigurationManager.AppSettings["EbaySiteID"].ToString());

    public static string API_COMPATIBILITY_LEVEL = ConfigurationManager.AppSettings["API_COMPATIBILITY_LEVEL"].ToString();

    public static string ebayMasterXmlPath = ConfigurationManager.AppSettings["ebayMasterXmlPath"].ToString();

    public static int ebayAPIUpdateDateDiff = int.Parse(ConfigurationManager.AppSettings["ebayAPIUpdateDateDiff"].ToString());

    public static string ebayFlashComboboxQuantitys = ConfigurationManager.AppSettings["ebayFlashComboboxQuantitys"].ToString();

    public static bool eBay_Page_View_Flash_Is_New = ConfigurationManager.AppSettings["eBay_Page_View_Flash_Is_New"].ToString() == "1";

    /// <summary>
    /// ebay system templete new ID. 
    /// 2011-1-29
    /// </summary>
    public static int ebay_templete_id_new = int.Parse(ConfigurationManager.AppSettings["ebay_templete_id_new"].ToString());
    /// <summary>
    /// ebay system sub templete new ID. 
    /// 2011-1-31
    /// </summary>
    public static int ebay_templete_id_new_sub = int.Parse(ConfigurationManager.AppSettings["ebay_templete_id_new_sub"].ToString());
    /// <summary>
    /// ebay Barebone system templete ID.
    /// 2011-2-14
    /// </summary>
    public static int ebay_templete_id_barebone = int.Parse(ConfigurationManager.AppSettings["ebay_templete_id_barebone"].ToString());
    /// <summary>
    /// ebay system sub templete new ID (Complete System)
    /// </summary>
    public static int ebay_templete_id_complete_system_Sub = int.Parse(ConfigurationManager.AppSettings["ebay_templete_id_complete_system_Sub"].ToString());

    /// <summary>
    /// Save eBay system price history.
    /// 2011-4-26
    /// </summary>
    public static bool eBaySysSavePriceHistory = int.Parse(ConfigurationManager.AppSettings["eBaySysSavePriceHistory"].ToString()) == 1;

    public static int relation_motherboard_video_group_id = int.Parse(ConfigurationManager.AppSettings["relation_motherboard_video_group_id"].ToString());
    public static int relation_motherboard_audio_group_id = int.Parse(ConfigurationManager.AppSettings["relation_motherboard_audio_group_id"].ToString());
    public static int relation_motherboard_network_group_id = int.Parse(ConfigurationManager.AppSettings["relation_motherboard_network_group_id"].ToString());

    public static int eBayModifyPartQuantity = int.Parse(ConfigurationManager.AppSettings["eBayModifyPartQuantity"].ToString());
    public static decimal ebayAccessoriesPrice = decimal.Parse(ConfigurationManager.AppSettings["ebayAccessoriesPrice"].ToString());
    public static decimal ebayBasicShippingFee = decimal.Parse(ConfigurationManager.AppSettings["ebayBasicShippingFee"].ToString());
}
