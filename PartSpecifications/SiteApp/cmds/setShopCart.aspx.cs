using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteApp.cmds
{
    public partial class setShopCart : Helper.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (ReqCmd)
                {
                    case "toCart":
                        try
                        {
                            int orderCode = SiteDB.HelperOrder.SaveTmpOrder(ReqSku
                                , ReqIsSystem
                                , GetOrderCode()
                                , System.Web.HttpContext.Current.Request.UserHostAddress
                                , db);
                            Response.Write(orderCode);
                        }
                        catch { Response.Write("0"); }
                        break;
                }
            }
            Response.End();
        }

       


        /// <summary>
        /// 目录ID
        /// </summary>
        int ReqCateId
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
        }

        string ReqCmd
        {
            get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
        }

        int ReqPageIndex
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "page", 0); }
        }
        /// <summary>
        /// SKU
        /// </summary>
        int ReqSku
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
        }
        /// <summary>
        /// 是否是系统
        /// </summary>
        bool ReqIsSystem
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "isSys", 0) == 1; }
        }

   
    }
}