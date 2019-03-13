using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web
{
    public class Config
    {


        /// <summary>
        /// 没条码产品的条码
        /// 
        /// </summary>
        public static string TmpSNCode = "1000000000";


        /// <summary>
        /// 云仓仓库Id： 9
        /// </summary>
        public static int YunWarehouseId = 9;

        /// <summary>
        /// 公司仓库Id：1
        /// </summary>
        public static int CompanyWarehouseId = 1;

        /// <summary>
        /// 瑕疵仓库Id：5
        /// </summary>
        public static int BadGoodsWarehouseId = 5;

        /// <summary>
        /// 中转仓库Id：8
        /// </summary>
        public static int TempWarehouseId = 8;
    }
}