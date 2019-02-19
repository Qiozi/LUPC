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
        public string Text { get; set; }

        public string Value { get; set; }

        public string LogoFont { get; set; }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                cateListStr += "<div id='c" + c.Value.ToString() + "' class='cateTitle'><i class=\"iconfont\">" + c.LogoFont + "</i><a href='list_part.aspx?cid=" + c.Value + "'> " + c.Text + "</a></div>";
                cateListStr += "<div class='row catelist' cateid='" + c.Value + "'>";
                cateListStr += "</div>";
            }
            ltProdList.Text = cateListStr;
            lblNavCateName.Text = cateString;
            #endregion

            #region 系统

            #endregion
        }
    }

    public string GetSysString()
    {
        var sysString = string.Empty;
        var sysSkus = db.tb_pre_index_page_setting.Where(p => (p.id.Equals(1) || p.id.Equals(2) || p.id.Equals(3) || p.id.Equals(6)) && p.sku.HasValue)
                       .OrderBy(p => p.id)
                       .Select(s => s.sku.Value).ToList();
        var comments = (from c in db.tb_ebay_system_part_comment
                        select new
                        {
                            c.id,
                            c.comment
                        }).ToList();

        for (int i = 0; i < sysSkus.Count; i++)
        {
            var sysSku1 = sysSkus[i];
            var sysList1 = (from s in db.tb_ebay_system_parts
                            join p in db.tb_product on s.luc_sku.Value equals p.product_serial_no
                            where s.system_sku.HasValue && s.system_sku.Value.Equals(sysSku1)
                            orderby s.id
                            select new
                            {
                                ShortName = p.product_short_name,
                                CommentId = s.comment_id,
                                PartSku = p.product_serial_no,
                                ImgSku = p.other_product_sku.HasValue && p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                Price = p.product_current_price.Value,
                                discount = p.product_current_discount.Value,
                                ToUrl = p.new_href_url
                            }).ToList();



            string sysResult = "<div class='list-group row'>";
            string sysImgLogos = "";
            foreach (var sysPart in sysList1)
            {
                var commentName = comments.Single(c => c.id.Equals(sysPart.CommentId)).comment.ToString();
                if (sysPart.PartSku != 16684 && !string.IsNullOrEmpty(commentName) && !string.IsNullOrEmpty(sysPart.ShortName))
                {
                    sysResult += "<a class='list-group-item col-md-3 fontBold' style='color:#666;'>" + commentName + "</a>";
                    sysResult += "<a class='list-group-item col-md-9' style='color:#333;' href='" + (string.IsNullOrEmpty(sysPart.ToUrl) ? "/detail_part.aspx?sku=" + sysPart.PartSku : string.Concat("/computer/parts_detail/", sysPart.ToUrl)) + "'>" + sysPart.ShortName + "</a>";
                    if (sysPart.ImgSku != 999999 && commentName.ToLower() == "case")
                        sysImgLogos += "<img src='" + GetLogo(sysPart.ImgSku) + "'>";
                }
            }
            sysResult += "</div>";
            sysString += string.Format(@" 
                <blockquote class=""sysTitle"">
                    <p><a href='/detail_sys.aspx?sku={5}'><i class=""iconfont"">&#xe612;</i> {3}</a></p>
                    <footer><a href='http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item={4}' target='_blank'>To eBay.ca: {4}</a><img src='images/ebay_logo.jpg'></footer>
                </blockquote>
                <div class=""row"" >
                    <div class=""col-md-3 syslogoArea"">
                        {0}
                    </div>
                    <div class=""col-md-7 syspartlistArea"">
                        {1}
                    </div>
                    <div class=""col-md-2 sysPriceArea"">
                        {2}
                    </div>
                </div>"
                , sysImgLogos
                , sysResult
                , GetPriceString(sysSku1, ConvertPrice(sysList1.Sum(p => p.Price)), ConvertPrice(sysList1.Sum(p => p.discount)))
                , db.tb_ebay_system.Single(p => p.id.Equals(sysSku1)).ebay_system_name
                , db.tb_ebay_selling.Single(p => p.sys_sku.HasValue && p.sys_sku.Value.Equals(sysSku1)).ItemID
                , sysSku1);

        }
        return sysString;
    }

    public string GetCateString()
    {
        string catestr = string.Empty;

        var list = from c in db.tb_product_category
                   where c.menu_pre_serial_no.HasValue && c.menu_pre_serial_no.Value.Equals(0)
                   && c.tag.HasValue && c.tag.Value.Equals(1)
                   && c.is_view_menu.HasValue && c.is_view_menu.Value.Equals(true)
                   && c.menu_child_serial_no != 378
                   orderby c.menu_child_order ascending
                   select new
                   {
                       ID = c.menu_child_serial_no,
                       Name = c.menu_child_name,
                       SubList = (from s in db.tb_product_category where s.menu_pre_serial_no.HasValue && s.menu_child_serial_no != 378 && s.menu_pre_serial_no.Value.Equals(c.menu_child_serial_no) && s.tag.HasValue && s.tag.Value.Equals(1) orderby s.menu_child_order ascending select new { ID = s.menu_child_serial_no, Name = s.menu_child_name, PageType = s.page_category })
                   };
        foreach (var item in list)
        {
            if (item.Name == "Server Systems")
            {
                continue;
            }
            if (item.ID != 2 && item.ID != 211)
            {
                catestr += string.Format(@"<div class=""col-xs-6 col-md-2 cateArea""><h5>{0}</h5>"
                    , item.Name);
                foreach (var sub in item.SubList)
                {
                    catestr += "<div class=\"\"><small><a href='" + GetCateHref(sub.ID, sub.PageType == 0) + "'>" + sub.Name + "</a></small></div>";
                }
                catestr += "</div>";
            }
            else if (item.ID == 2)
            {
                catestr += "<div class=\"col-xs-6 col-md-4 cateArea\"><h5>" + item.Name + "</h5>";

                var subContResult = "";
                foreach (var sub in item.SubList)
                {

                    subContResult += "<div class=\"col-xs-6 col-md-6\" style='padding-left:0px;'><small class='nav-hover'><a href='" + GetCateHref(sub.ID, sub.PageType == 0) + "'>" + sub.Name + "</a></small></div>";
                }

                catestr += subContResult + "</div>";
            }

        }
        catestr += @" <div class=""col-xs-6 col-md-2"">
                        <div class=""ebaycount"" style=""vertical-align:middle""><h5></h5>
                            <a href=""http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&userid=dpowerseller&ftab=AllFeedback"" target='_blank'>
                            <img style=""border: none;"" src=""/images/ebay-count.png"" width=""90%"" /></a>
                        </div>
                    </div>";
        return catestr;
    }

    string GetCateHref(int cid, bool isSys)
    {
        return isSys ? "list_sys.aspx?cid=" + cid : "list_part.aspx?cid=" + cid;
    }

    string GetLogo(int sku)
    {
        //return string.Format("{2}pro_img/ebay_gallery/{0}/{1}_ebay_list_t_1.jpg"
        //     , sku.ToString().Substring(0, 1)
        //     , sku
        //     , setting.ImgHost);
        return GetImgFullname.Get(sku, 230, 0, 0);
    }

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
   <a href='detail_sys.aspx?sku={1}' class='btn btn-default'>More Info</a>
"
                , price
                , sku
                , "");//CurrSiteCountry == CountryType.CAD ? "CAD" : "USD");
        }
        else
        {
            return string.Format(@"
    <h4><del style='color:#555;'>${0}</del></h4>
    <h2><span >${1}</span><small>{3}</small></h2>
   <a href='detail_sys.aspx?sku={2}' class='btn btn-default' >More Info</a>
"
                , price
                , price - discount
                , sku
                , "");//, CurrSiteCountry == CountryType.CAD ? "CAD" : "USD");
        }
    }
}