using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace SiteApp.cmds
{
    public partial class getProdList : Helper.PageBase
    {


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                switch (ReqCmd)
                {
                    case "getProdList":
                        WriteProdListJson(ReqCateId, ReqPageIndex);
                        break;
                    case "getSysList":
                        WriteSysListJson(ReqCateId, ReqPageIndex);
                        break;
                }


            }
            Response.End();
        }
        /// <summary>
        /// 系统列表
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="PageIndex"></param>
        void WriteSysListJson(int cid, int PageIndex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            List<SiteModels.View.SysList> list = SiteDB.ProdHelper.GetSysProd(cid, true, 5, PageIndex, db);
            //string json = JsonConvert.SerializeObject(list);
            //Response.Write(json);
            foreach (var d in list)
            {
                string subList = "<ul>";
                foreach (var sl in d.DetailList)
                {
                    subList += string.Format(@"<li>{0}</li>", sl.PartName);

                }
                subList += "</ul>";
                string priceStr = d.discount > 0 ? "<s>$" + d.price + "</s>&nbsp;&nbsp;<mark>Save: " + d.discount + "</mark>&nbsp;&nbsp;<label>$" + d.sell + "</label>" : "<label>$" + d.sell + "</label>";
                sb.Append(string.Format(@"<ul class=""media-list list-group-item"">
    <li class=""media"">

        <div class=""media-body"">
            <h5 class=""media-heading"">{1}</h5>
        </div>
            
                <div class=""media"">
                    <a class=""pull-left"" onclick='return false;'>
                        <img class=""media-object"" src=""{0}"" alt=""..."">
                    </a>
                    <div class=""media-body"">
                        {4}
                       
                    </div>
                </div>
                
                <a class=""pull-right"" style=""cursor:pointer;"" onclick=""toShopCart('{3}');return false;""><span class=""glyphicon glyphicon-shopping-cart""></span> Buy</a>

                <div class=""pull-right""> {2}&nbsp;&nbsp;&nbsp;</div>
    </li>
</ul>"
                    , d.ImgUrl
                    , d.Name
                    , priceStr
                    , d.Sku
                    , subList
                    ));
            }
            Response.Write(sb.ToString());
        }

        /// <summary>
        /// 零件，笔记本列表
        /// 
        /// </summary>
        /// <param name="cid"></param>
        /// <param name="pageIndex"></param>
        void WriteProdListJson(int cid, int pageIndex)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            List<SiteModels.View.ProdList> list = SiteDB.ProdHelper.GetProd(cid, 1, 20, pageIndex, db);
            //string json = JsonConvert.SerializeObject(list);
            //Response.Write(json);
            foreach (var d in list)
            {
                string priceStr = d.discount > 0 ? "<s>$" + d.price + "</s>&nbsp;&nbsp;<mark>Save: " + d.discount + "</mark>&nbsp;&nbsp;<label>$" + d.sell + "</label>" : "<label>$" + d.sell + "</label>";
                sb.Append(string.Format(@"<ul class=""media-list list-group-item"">
<li class=""media"">
     <a class=""pull-left"" href=""#"">
        <img class=""media-object"" src=""{0}"" alt=""..."">
     </a>
     <div class=""media-body"">
        <h5 class=""media-heading"">{1}</h5>
        {2}
     </div>
     <a class=""pull-right"" style=""cursor:pointer;"" onclick=""toShopCart('{3}');return false;""><span class=""glyphicon glyphicon-shopping-cart""></span> Buy</a>
   </li>
</ul>"
                    , d.ImgUrl
                    , d.Name
                    , priceStr
                    , d.Sku
                    ));
            }
            Response.Write(sb.ToString());

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