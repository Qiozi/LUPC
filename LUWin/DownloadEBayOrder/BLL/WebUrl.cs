using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class WebUrl
    {
        /// <summary>
        /// 主机地址
        /// </summary>
        static string _hostUrl = "http://ftp.lucomputers.com";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sku"></param>
        /// <returns></returns>
        public static string GetPartDetailUrl(int sku)
        {
            return string.Concat("http://ca.lucomputers.com", "/detail_part.aspx?sku=" + sku + "&specifType=1");
        }

        /// <summary>
        /// 结束ebay 
        /// </summary>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static string GeteBayPartEndUrl(string itemid)
        {
            return string.Concat(_hostUrl, "/q_admin/ebayMaster/online/EndItem.aspx?cmd=qiozi@msn.com_wu.th@qq.com&itemid=", itemid);
        }

        /// <summary>
        /// 系统 读取价格
        /// </summary>
        /// <param name="isShrink"></param>
        /// <param name="adjustment"></param>
        /// <param name="sysSku"></param>
        /// <returns></returns>
        public static string GeteBaySystemPriceUrl(bool isShrink, decimal adjustment, int sysSku)
        {
            return string.Format("{4}/q_admin/ebaymaster/ebay_system_cmd.aspx?cmd=GetSysEbayPriceBySysCost&cmd2=qiozi@msn.com_wu.th@qq.com&is_shrink={0}&Adjustment={1}&systemsku={2}&{3}"
                , isShrink ? "0" : "1"
                , adjustment
                , sysSku
                , DateTime.Now.ToString("ssfff")
                , _hostUrl);
        }

        /// <summary>
        /// 系统修改价格路径
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static string GeteBaySystemModifyPriceUrl(decimal cost, decimal profit, decimal ebayfee, decimal shippingfee, decimal price, string itemid)
        {
            return string.Format("{7}/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost={0}&Profit={1}&eBayFee={2}&ShippingFee={3}&Price={4}&IsDesc=0&onlyprice=1&itemid={5}&issystem=1&{6}"
                , cost
                , profit
                , ebayfee
                , shippingfee
                , price
                , itemid
                , DateTime.Now.ToString("ssfff")
                , _hostUrl);
        }


        /// <summary>
        /// 网站请求价格地址
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="screen"></param>
        /// <param name="adjustment"></param>
        /// <param name="sku"></param>
        /// <returns></returns>
        public static string GeteBayPriceUrl(decimal cost, decimal screen, decimal adjustment, int sku)
        {
            return string.Format("{4}/q_admin/ebayMaster/ebay_notebook_get_ebayPrice.aspx?Cost={0}&Screen={1}&Adjustment={2}&LUC_Sku={3}"
                , cost
                , screen
                , adjustment
                , sku
                , _hostUrl);
        }

        /// <summary>
        /// 修改零件eBay价格地址
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <returns></returns>
        public static string GeteBayModifyPriceUrl(decimal cost, decimal profit, decimal ebayfee, decimal shippingfee, decimal price, string itemid)
        {
            return string.Format("{6}/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost={0}&Profit={1}&eBayFee={2}&ShippingFee={3}&Price={4}&IsDesc=0&onlyprice=1&itemid={5}&issystem=0"
                , cost
                , profit
                , ebayfee
                , shippingfee
                , price
                , itemid
                , _hostUrl);
        }

        /// <summary>
        /// 修改系统描述
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="profit"></param>
        /// <param name="ebayfee"></param>
        /// <param name="shippingfee"></param>
        /// <param name="price"></param>
        /// <param name="itemid"></param>
        /// <param name="isSys"></param>
        /// <returns></returns>
        public static string GeteBayModifyPriceUrlWithDesc(decimal cost, decimal profit, decimal ebayfee, decimal shippingfee, decimal price, string itemid, bool isSys)
        {
            return string.Format("{6}/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost={0}&Profit={1}&eBayFee={2}&ShippingFee={3}&Price={4}&IsDesc=1&onlyprice=0&itemid={5}&issystem={7}"
                , cost
                , profit
                , ebayfee
                , shippingfee
                , price
                , itemid
                , _hostUrl
                , isSys ? 1 : 0);
        }

        /// <summary>
        /// 把没有库存的系统零件，排除
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="qty"></param>
        /// <returns></returns>
        public static string RemovePartNoStock(int sku, int qty)
        {
            return string.Format("{0}/q_admin/ebaymaster/ebay_cmd.aspx?cmd2=qiozi@msn.com_wu.th@qq.com&cmd=ChangeForSys&sku={1}&qty={2}"
                , _hostUrl
                , sku
                , qty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string ClearPartForSys()
        {
            return string.Format("{0}/q_admin/ebaymaster/ebay_cmd.aspx?cmd2=qiozi@msn.com_wu.th@qq.com&cmd=ClearPartForSys"
               , _hostUrl);
        }

        /// <summary>
        ///  生成系统配置的零件文件
        /// </summary>
        /// <param name="sysSku"></param>
        /// <returns></returns>
        public static string GetGenerateSystemPartFile(int sysSku)
        {
            return string.Format("{0}/q_admin/ebaymaster/Online/get_system_configuration.aspx?cmd=GenerateXmlFile&Version=3&system_sku={1}&{2}"
                , _hostUrl
                , sysSku
                , DateTime.Now.ToString("ssfff"));
        }
    }
}
