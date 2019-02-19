using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class list_part : PageBase
{
    public string ParentName { get; set; }

    public int ParentCid { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var cateId = ReqCateId;

            if (cateId == 414 || cateId == 412 || cateId == 413)
            {
                Response.Redirect("/list_sys.aspx?cid=" + cateId, true);
            }

            if (cateId > 0 || !string.IsNullOrEmpty(ReqCateName))
            {
                if (cateId == 0)
                {
                    var cate = db.tb_product_category.SingleOrDefault(p => p.menu_child_name_logogram.Equals(ReqCateName));
                    if (cate != null)
                    {
                        cateId = cate.menu_child_serial_no;
                    }
                }
                var cateMenuInfo = new LU.BLL.CateMenu(db, cateId);
                if (cateMenuInfo != null)
                {
                    var subCateModel = cateMenuInfo.SubCates;
                    if (subCateModel != null)
                    {
                        ltCateNameParent.Text = cateMenuInfo.ParentTitle;
                        ltCateName.Text = cateMenuInfo.Title;
                        this.Title = cateMenuInfo.Title + " - LU Computers";
                        ParentCid = cateMenuInfo.ParentCateId;
                        #region cate list

                        var cateIndex = 0;
                        foreach (var cate in cateMenuInfo.SubCates)
                        {
                            if (cate.CateType != LU.Model.Enums.CateType.System)
                            {
                                cateIndex++;
                                if (cateIndex % 2 == 1)
                                {
                                    ltCates.Text += "<li role='pressntation'><a href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + cate.Id + "'><i class='iconfont'>" + cate.IconName + "</i> " + cate.Title + "</a>";
                                }
                                else
                                {
                                    ltCates.Text += "<a href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + cate.Id + "'><i class='iconfont'>" + cate.IconName + "</i> " + cate.Title + "</a></li>";
                                }
                            }
                        }
                        if (cateIndex % 2 == 1)
                        {
                            ltCates.Text += "</li>";
                        }
                        #endregion
                    }

                    #region keywords
                    var keyList = db.tb_product_category_keyword.Where(p => p.category_id.HasValue
                                && p.category_id.Value.Equals(cateId)
                                && p.showit.HasValue
                                && p.showit.Value.Equals(true)
                                && p.IsShowOnWebPage.Equals(true)
                                ).ToList();
                    string keystr = "<table id='keyListArea'>";
                    keystr += "<tr><td colspan='2'>" + WriteKeywordCloseButton(GetKeys(), keyList, cateId) + "</td></tr>";
                    List<int> existKeyIds = GetKeyParentIDs(GetKeys());
                    for (var i = 0; i < keyList.Count; i++)
                    {
                        int pid = keyList[i].id;
                        if (existKeyIds.Contains(pid))
                            continue;
                        var subKeyList = db.tb_product_category_keyword_sub.Where(p => p.parent_id.HasValue
                            && p.parent_id.Value.Equals(pid)
                            && p.showit.HasValue
                            && p.showit.Value.Equals(true)
                            && p.IsShowOnWebPage.Equals(true)
                            ).ToList();
                        keystr += "<tr>";
                        keystr += "<td width=\"150px\"><h5><b>" + keyList[i].keyword + "</b></h5></td>";
                        keystr += "<td>";
                        foreach (var subm in subKeyList)
                        {
                            keystr += "<a href='" + ReqAllKey + "&k=" + pid + "|" + subm.keyword + "'>" + subm.keyword + "</a>";
                        }
                        keystr += "</td>";
                        keystr += "</tr>";

                    }
                    keystr += "</table>";
                    ltKeys.Text = keystr;
                    #endregion                    
                }

                InitList(cateId);
            }
            else
            {
                Response.Redirect("/");
            }
        }
        // Response.Write(Util.GetStringSafeFromQueryString(Page, "k"));
    }

    void InitList(int cateId)
    {
        #region list
        var list = LU.BLL.CacheProvider.GetAllProdBaseInfos(db, cateId).Where(p => p.CateId.Equals(cateId)).ToList();
        //if (list == null || list.Count == 0)
        //    list = LU.BLL.ProductProvider.GetAllProducts(db, ReqCateId, cookiesHelper.CurrSiteCountry);
        FilterKey(list);
        this.rptProd.DataSource = list;
        this.rptProd.DataBind();
        #endregion
    }

    void FilterKey(List<LU.Model.Product> list)
    {
        var keys = GetKeys();
        for (int i = 0; i < list.Count; i++)
        {
            foreach (var key in keys)
            {
                if (i >= list.Count || i < 0)
                    break;
                if (!list[i].Keywords.ToLower().Contains(key.Value.ToLower()))
                {
                    list.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    List<int> GetKeyParentIDs(List<KeyValuePair<int, string>> keys)
    {
        List<int> r = new List<int>();
        foreach (var k in keys)
        {
            r.Add(k.Key);
        }
        return r;
    }

    List<KeyValuePair<int, string>> GetKeys()
    {
        List<KeyValuePair<int, string>> result = new List<KeyValuePair<int, string>>();
        if (ReqK.IndexOf("|") > -1)
        {
            string[] keys = ReqK.Split(new char[] { ',' });
            foreach (var k in keys)
            {
                if (k.IndexOf("|") > -1)
                {
                    string[] keyConts = k.Split(new char[] { '|' });
                    int keyID;
                    int.TryParse(keyConts[0].Trim(), out keyID);
                    if (keyID == 0)
                        continue;
                    KeyValuePair<int, string> kv = new KeyValuePair<int, string>(keyID, keyConts[1]);
                    result.Add(kv);
                }
            }
        }
        return result;
    }

    string WriteKeywordCloseButton(List<KeyValuePair<int, string>> ks, List<LU.Data.tb_product_category_keyword> kList, int cateId)
    {
        string result = "<a class='closebutton btn' href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + cateId + "'><span style='color:red;'>Clear</span></a>";
        string resultKey = "";
        for (int i = 0; i < ks.Count; i++)
        {
            string kcateName = "";
            foreach (var kl in kList)
            {
                if (kl.id.Equals(ks[i].Key))
                {
                    kcateName = kl.keyword;

                    break;
                }
            }
            resultKey += "[" + ks[i].Value.ToLower() + "]";
            // Response.Write(ReqAllKey.Replace("&k=" + ks[i].Key + "|" + ks[i].Value, "") + "<br>");
            result += "<a href='" + LU.BLL.Config.Host + "list_part.aspx" + ReqAllKey.Replace("&k=" + ks[i].Key + "|" + ks[i].Value, "") + (ReqAllKey.ToLower().IndexOf("cid") == -1 ? "&cid=" + cateId : "") + "' class='closebutton btn'><span style='color:black;'>" + kcateName + ":</span>  " + ks[i].Value + " <span style='color:red;'><span aria-hidden=\"true\">&times;</span></span></a>";
        }
        ltKeyHidden.Text = resultKey;
        return ks.Count == 0 ? "" : result;
    }

    string ReqAllKey
    {
        get
        {
            if (string.IsNullOrEmpty(HttpContext.Current.Request.Url.Query))
            {
                return "?";
            }
            return HttpUtility.UrlDecode(HttpContext.Current.Request.Url.Query);
        }
    }

    string ReqK
    {
        get
        {
            if (Request.Url.ToString().ToLower().IndexOf(".html?") > -1)
            {
                var paramstr = Request.Url.ToString().Split(new char[] { '?' })[1];
                if (!string.IsNullOrEmpty(paramstr))
                {
                    return paramstr
                        .Replace("&", ",")
                        .Replace("k", "")
                        .Replace("=", "");
                }
                return string.Empty;
            }
            else
                return Util.GetStringSafeFromQueryString(Page, "k");
        }
    }

    public int ReqCateId
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
    }

    public string ReqCateName
    {
        get { return Util.GetStringSafeFromQueryString(Page, "catename"); }
    }
}