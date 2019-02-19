using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteApp.Helper
{
    public class PageBase : System.Web.UI.Page
    {
        public SiteDB.nicklu2Entities db = new SiteDB.nicklu2Entities();

        public PageBase() { }

        public int GetOrderCode()
        {
            int code = ReqOrderCode;
            if (ReqOrderCode > 0)
            {
                return ReqOrderCode;
            }
            if (Request.Cookies["orderCode"] != null)
            {
                int.TryParse(Request.Cookies["orderCode"].Value, out code);
            }
            if (code == 0)
            {
                code = SiteDB.Codes.NewOrderCode(db);
                Response.Cookies["orderCode"].Value = code.ToString();
            }
            return code;
        }

        public int ReqOrderCode
        {
            get { return Util.GetInt32SafeFromQueryString(Page, "orderCode", 0); }
        }

        /// <summary>
        /// 是否已登入
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            if (Request.Cookies["username"] == null)
            {
                return false;
            }

            string username = Request.Cookies["username"].Value ?? "";
            if (username.Length > 0)
                return true;
            else
                return false;
        }
    }
}