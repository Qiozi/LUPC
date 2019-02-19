using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.Enums
{
    public enum Cmd
    {
        None = 0,
        DownOrder = 1,
        DownSingleOrder = 2,
        DowneBayPrice = 3,
        GeneratePartHtmlFileForApp = 4,
        NewSiteHomeData = 5,
        NewGeneratePartHtmlFile = 6,
        NewSystemKeyword = 7,
        NewHomeCateList = 8,
        NewSyslistAndPartlist = 9,
        NewPartKeyword = 10,
        /// <summary>
        ///  发送当天的营业额到邮箱
        /// </summary>
        SendTodaySellTotal = 11,
        /// <summary>
        /// 
        /// </summary>
        ReStoreOnsale = 12,
        ModifyPartEbayPrice = 13,
        ModifySystemEbayPrice = 14,
        ModifyEbayPricePartAndSys = 15,
        ReadPriceFile = 16,
        GenerateSysPartsFile = 17,
        RemovePartQuantityIsZone = 18,
        SysToCategory = 19,
        ForEbayDownOnSaleDetail = 20,
        ChangeWord = 21,
        WriteAllProductForSearch = 22,
        /// <summary>
        /// 执行缓存 的，URL ,,例如，更新到ebay.
        /// </summary>
        DoneCacheUrl = 23,
        /// <summary>
        /// 修改所有产品描述
        /// </summary>
        ModifyAllDesc = 25,
        /// <summary>
        /// 修改所有零件描述
        /// </summary>
        ModifyAllPartDesc = 26,
        /// <summary>
        /// 修改所有系统描述
        /// </summary>
        ModifyAllSystemDesc = 27
    }
}
