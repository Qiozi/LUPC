using System;
using System.Collections.Generic;
using System.Text;

namespace KKWStore.Helper
{
    public class Config
    {
        public static string IP { get; set; }


        public static bool IsAdmin { get; set; }

        /// <summary>
        /// 参观者，只能看库存
        /// </summary>
        public static bool IsVisitor { get; set; }

        /// <summary>
        /// 参观者Ids
        /// </summary>
        public static int[] VisitorIds = new int[] { 2 };

        public static db.tb_user CurrentUser { get; set; }

        /// <summary>
        /// 云仓（显神）
        /// </summary>
        public static int DefaultStoreId = 2;

        /// <summary>
        /// 系统管理员帐号，主要用于系统操作纪录
        /// </summary>
        public static int SysAdminId { get { return 15; } }

        public static List<db.tb_user_model> UserPermanentList { get; set; }

        //public static string LocalhostServiceIP
        //{
        //    get { return "Server=localhost;Database=qstore;User ID=root;Password=1234qwer;allow zero datetime=true;default command timeout=13600;"; }
        //}

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
