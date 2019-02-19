using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteApp.cmds
{
    public partial class getShopCart : Helper.PageBase
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (ReqCmd)
                {
                    case "getOrderContQty":
                        WriteOrderContQty();
                        break;               
                    case "getOrderList":
                        WriteOrderList();
                        break;

                  
                }


            }
            Response.End();
        }

        /// <summary>
        /// 输出订单列表
        /// 
        /// </summary>
        void WriteOrderList()
        {

        }

        void WriteOrderContQty()
        {
            int qty = SiteDB.HelperOrder.GetTmpOrderListQty(GetOrderCode(), db);
            Response.Write(qty.ToString());
        }



        /// <summary>
        /// 目录ID
        /// </summary>
        int ReqCateId
        {
            get { return Util.GetInt32SafeFromString(Page, "cid", 0); }
        }

        string ReqCmd
        {
            get { return Util.GetStringSafeFromString(Page, "cmd"); }
        }

        int ReqPageIndex
        {
            get { return Util.GetInt32SafeFromString(Page, "page", 0); }
        }
    }
}