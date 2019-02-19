using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : PageBase
{
    //public int SysSKU1 = 219434;
    // public int SysSKU2 = 218033;

    public class HomeCate
    {
        public bool IsSystem { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public string LogoFont { get; set; }

        public int CateId { get; set; }
    }

    List<LU.Model.Cate> queryCates = new List<LU.Model.Cate>();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            #region Cates
            queryCates = LU.BLL.CacheProvider.GetAllCates(db);
            var hotCates = new List<LU.Model.Cate>();

            var logoDir = new System.IO.DirectoryInfo(Server.MapPath("~/images/cate-logo/"));
            var fs = logoDir.GetFiles();
            // new System.IO.DirectoryInfo()
            foreach (var cate in queryCates)
            {
                foreach (var sub in cate.SubCates)
                {
                    foreach (var f in fs)
                    {
                        if (f.Name == string.Concat(sub.Id, ".jpg"))
                        {
                            hotCates.Add(new LU.Model.Cate
                            {
                                Id = sub.Id,
                                Title = sub.Title,
                                Href = sub.Href
                            });
                        }
                    }
                }
            }

            //this.rptListDetail.DataSource = hotCates;
            //this.rptListDetail.DataBind();
            #endregion

            #region 零件列表

            var cateString = "";
            var cateListStr = "";
            int index = 0;

            var catestring = System.IO.File.ReadAllText(Server.MapPath("/Computer/homeCate.txt"));
            List<HomeCate> cateList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<HomeCate>>(catestring);

            foreach (var c in cateList)
            {
                index++;
                cateString += "<li><a href='#c" + c.Value + "' data-toggle='tooltip' data-placement='top' title='" + c.Text + "'><i class=\"iconfont\">" + c.LogoFont + "</i> " + c.Text + "</a></li>";
                cateListStr += "<div id='c" + c.Value.ToString() + "' class='cateTitle'><i class=\"iconfont\">" + c.LogoFont + "</i><a href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + c.Value + "'> " + c.Text + "</a></div>";
                cateListStr += "<div class='row catelist' cateid='" + c.Value + "'>";
                cateListStr += "</div>";
            }
            //  ltProdList.Text = cateListStr;
            //lblNavCateName.Text = cateString;



            this.rptProdList.DataSource = cateList;
            this.rptProdList.DataBind();

            #region 读取系统标题
            var querySystemTitle = db.tb_pre_index_page_setting.Where(p => (p.id.Equals(1) || p.id.Equals(2) || p.id.Equals(3) || p.id.Equals(6))
&& p.sku.HasValue)
          .OrderBy(p => p.id)
          .Select(s => new { title = s.title, sku = s.sku, cid = s.CateId.Value, id = s.id }).ToList();
            for (var i = querySystemTitle.Count - 1; i >= 0; i--)
            {
                cateList.Insert(0, new HomeCate
                {
                    Text = querySystemTitle[i].title,
                    Value = querySystemTitle[i].sku.ToString(),
                    CateId = querySystemTitle[i].cid,
                    IsSystem = true
                });
            }
            #endregion

            this.rptNavCateNameList.DataSource = cateList;
            this.rptNavCateNameList.DataBind();
            #endregion

            #region 系统
            var systems = LU.BLL.ProductProvider.GetHomeSystem(db, cookiesHelper.CurrSiteCountry);
            for (var i = 0; i < systems.Count; i++)
            {
                foreach (var item in querySystemTitle)
                {
                    if (item.sku == systems[i].Id)
                    {
                        systems[i].OldCateId = item.cid;
                        systems[i].Priority = item.id;
                        systems[i].CateTitle = item.title;
                    }
                }
            }
            rptSysList.DataSource = systems.OrderBy(me => me.Priority);
            rptSysList.DataBind();

            #endregion

            #region Logo area
            this.rptBrandLogo.DataSource = db.tb_producter.Where(p => !string.IsNullOrEmpty(p.logo_url)).ToList();
            this.rptBrandLogo.DataBind();
            #endregion

            SaveKeywordJsonForSearch();

            BindTopMenuList();
        }
    }

    /// <summary>
    /// top menus
    /// </summary>
    void BindTopMenuList()
    {
        rptTopMenu.DataSource = queryCates.Where(me => me.ParentId.Equals(0)).ToList();
        rptTopMenu.DataBind();
    }

    public void SaveKeywordJsonForSearch()
    {
        var dirPath = Server.MapPath("~/Computer/ForSearch");
        if (!System.IO.Directory.Exists(dirPath))
        {
            System.IO.Directory.CreateDirectory(dirPath);
        }
        var filename = Server.MapPath("~/Computer/ForSearch/350.json");
        var isWrite = false;
        if (System.IO.File.Exists(filename))
        {
            System.IO.FileInfo fi = new System.IO.FileInfo(filename);
            if (fi.LastWriteTime.Date < DateTime.Now.Date)
            {
                isWrite = true;
            }
        }
        else
        {
            isWrite = true;
        }
        if (isWrite)
        {

            var query = LU.BLL.CacheProvider.GetAllProdBaseInfos(db, 0);//.Select(p => p.ProduName).ToList();
            var cates = query.Select(p => p.CateId).Distinct().ToList();
            foreach (var c in cates)
            {
                var partlist = query.Where(p => p.CateId.Equals(c)).Select(p => p.ProduName).ToList();
                System.IO.File.WriteAllText(string.Concat(dirPath, "/", c, ".json"), Newtonsoft.Json.JsonConvert.SerializeObject(partlist), System.Text.Encoding.UTF8);
            }
        }

    }

    public string GetCateHref(int cid, bool isSys)
    {
        return string.Concat(LU.BLL.Config.Host, isSys ? "/list_sys.aspx?cid=" + cid : "/list_part.aspx?cid=" + cid);
    }

    //string GetLogo(int sku)
    //{
    //    //return string.Format("{2}pro_img/ebay_gallery/{0}/{1}_ebay_list_t_1.jpg"
    //    //     , sku.ToString().Substring(0, 1)
    //    //     , sku
    //    //     , setting.ImgHost);
    //    return LU.BLL.QiNiuImgHelper.Get(sku, 230, 0, 0);
    //}

    /// <summary>
    /// 系统价格部份HTML 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="price"></param>
    /// <returns></returns>
    string GetPriceString(int sku, decimal price, decimal discount)
    {
        if (discount < 1)
        {
            return string.Format(@"
    <h2><span >${0}</span><small>{2}</small></h2>
   <a href='{1}' class='btn btn-default'>More Info</a>
"
                , price
                , LU.BLL.Util.SysUrl(sku)
                , "");//CurrSiteCountry == CountryType.CAD ? "CAD" : "USD");
        }
        else
        {
            return string.Format(@"
    <h4><del style='color:#555;'>${0}</del></h4>
    <h2><span >${1}</span><small>{3}</small></h2>
   <a href='{2}' class='btn btn-default' >More Info</a>
"
                , price
                , price - discount
                , LU.BLL.Util.SysUrl(sku)
                , "");//, CurrSiteCountry == CountryType.CAD ? "CAD" : "USD");
        }
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //var data = (LU.Model.Cate)e.Item.DataItem;
            //var rpt = e.Item.FindControl("_rpt") as Repeater;
            //var ds = data.SubCates;
            //for (int i = 0; i < ds.Count; i++)
            //{

            //}

            //rpt.DataSource = data.SubCates;
            //rpt.DataBind();
        }
    }

    protected void rptSysList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var data = (LU.Model.SystemProduct)e.Item.DataItem;
            var rpt1 = e.Item.FindControl("_rpt1") as Repeater;
            rpt1.DataSource = data.Parts;
            rpt1.DataBind();
        }
    }

    protected void rptProdList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var rpt = e.Item.FindControl("_rpt") as Repeater;
            var model = e.Item.DataItem as HomeCate;
            rpt.DataSource = LU.BLL.ProductProvider.GetHomeProducts(db, int.Parse(model.Value), cookiesHelper.CurrSiteCountry);
            rpt.DataBind();
        }
    }

    protected void rptTopMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header &&
            e.Item.ItemType != ListItemType.Footer)
        {
            var rpt = e.Item.FindControl("_rpt") as Repeater;
            var rptPart = e.Item.FindControl("_rptPart") as Repeater;

            var data = e.Item.DataItem as LU.Model.Cate;
            if (data.CateType == LU.Model.Enums.CateType.Part)
            {
                rptPart.DataSource = data.SubCates;
                rptPart.DataBind();
            }
            else
            {
                rpt.DataSource = data.SubCates;
                rpt.DataBind();
            }
        }
    }
}