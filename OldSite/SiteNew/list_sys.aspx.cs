using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.IO;

public partial class list_sys : PageBase
{
    public int ParentCid { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCateId > 0)
            {
                var cateMenuInfo = new LU.BLL.CateMenu(db, ReqCateId);
                {
                    var subCateModel = cateMenuInfo.SubCates;
                    if (subCateModel != null)
                    {
                        ltCateNameParent.Text = cateMenuInfo.ParentTitle;
                        ltCateName.Text = cateMenuInfo.Title;
                        this.Title = cateMenuInfo.Title + " - LU Computers";
                        ParentCid = cateMenuInfo.ParentCateId;

                        #region cate list                        
                        for (int i = 0; i < subCateModel.Count; i++)
                        {
                            if (subCateModel[i].CateType == LU.Model.Enums.CateType.System)
                            {
                                ltCates.Text += "<li role='pressntation'><a href='" + LU.BLL.Config.Host + "list_sys.aspx?cid=" + subCateModel[i].Id + "'>" + subCateModel[i].Title + "</a></li>";
                            }
                        }
                        #endregion
                    }
                    InitKeyArea();
                }
            }
        }
        BindSysList();
    }

    void BindSysList()
    {
        var query = LU.BLL.CacheProvider.GetSysMiniBaseInfos(db, this.Context).Where(p => p.CateId.Equals(ReqCateId)).ToList();
        var keys = GetKeys();
        foreach (var k in keys)
        {
            for (int i = 0; i < query.Count; i++)
            {
                var sys = query[i];
                sys.Price = ConvertPrice(sys.Price);
                sys.Discount = ConvertPrice(sys.Discount);
                sys.Sold = ConvertPrice(sys.Sold);
                var haveComment = false;
                for (var j = 0; j < sys.SysMiniSubParts.Count; j++)
                {
                    var sysSub = sys.SysMiniSubParts[j];
                    var comment1 = sysSub.Comment.ToLower().Replace(" ", "");
                    var comment = k.Key.ToLower().Replace(" ", "");

                    if (comment.Contains(comment1) && !string.IsNullOrEmpty(comment1))
                    {
                        haveComment = true;
                        if (!string.IsNullOrEmpty(sysSub.ShortNameForSys))
                        {
                            if (sysSub.ShortNameForSys.ToLower() != k.Value.ToLower())
                            {
                                query.RemoveAt(i);
                                i--;
                                break;
                            }
                        }
                    }
                }
                if (!haveComment)
                {
                    query.RemoveAt(i);
                    i--;
                }
            }
        }
        this.rptList.DataSource = query;
        this.rptList.DataBind();
        if (query.Count == 0)
        {
            ltRptNote.Text = "<div style='text-align:center;color:blue;'>No find data.</div>";
        }
        else
        {
            ltRptNote.Text = "";
        }
    }

    void InitKeyArea()
    {
        string json = System.IO.File.ReadAllText(Server.MapPath("computer/sys_key" + ReqCateId + ".txt"));

        JavaScriptSerializer js = new JavaScriptSerializer();

        SysKey KeyModel = js.Deserialize<SysKey>(json);

        //  Response.Write(KeyModel.cpu[0]);

        string keystr = "<table id='keyListArea' class='table table-condensed'>";
        keystr += "<tr><td colspan='2'>" + WriteKeywordCloseButton(GetKeys()) + "</td></tr>";

        List<string> existKeyIds = GetKeyParentIDs(GetKeys());

        if (!existKeyIds.Contains("CPU"))
        {
            // CPU
            keystr += "<tr>";
            keystr += "<td><h5><b>CPU</b></h5></td>";
            keystr += "<td>";
            foreach (var subm in KeyModel.cpu)
            {
                keystr += "<a href='" + ReqAllKey + "&k=CPU|" + subm + "'>" + subm + "</a>";
            }
            keystr += "</td>";
            keystr += "</tr>";
        }
        if (!existKeyIds.Contains("Video Card") && ReqCateId != 412)
        {
            // Video Card
            keystr += "<tr>";
            keystr += "<td nowrap><h5><b>Video Card</b></h5></td>";
            keystr += "<td>";
            foreach (var subm in KeyModel.vc)
            {
                keystr += "<a href='" + ReqAllKey + "&k=Video Card|" + subm + "'>" + subm + "</a>";
            }
            keystr += "</td>";
            keystr += "</tr>";
        }
        if (!existKeyIds.Contains("Memory"))
        {
            // Memory
            keystr += "<tr>";
            keystr += "<td><h5><b>Memory</b></h5></td>";
            keystr += "<td>";
            foreach (var subm in KeyModel.memory)
            {
                keystr += "<a href='" + ReqAllKey + "&k=Memory|" + subm + "'>" + subm + "</a>";
            }
            keystr += "</td>";
            keystr += "</tr>";
        }

        keystr += "</table>";
        ltKeys.Text = keystr;
    }

    List<string> GetKeyParentIDs(List<KeyValuePair<string, string>> keys)
    {
        List<string> r = new List<string>();
        foreach (var k in keys)
        {
            r.Add(k.Key);
        }
        return r;
    }

    List<KeyValuePair<string, string>> GetKeys()
    {
        List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
        if (ReqK.IndexOf("|") > -1)
        {
            string[] keys = ReqK.Split(new char[] { ',' });
            foreach (var k in keys)
            {
                if (k.IndexOf("|") > -1)
                {
                    string[] keyConts = k.Split(new char[] { '|' });
                    if (keyConts[0].Trim() == "")
                        continue;

                    if (keyConts[0].Trim() == "CPU")
                    {
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>("CPU", keyConts[1]);
                        result.Add(kv);
                    }

                    if (keyConts[0].Trim() == "Video Card")
                    {
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>("Video Card", keyConts[1]);
                        result.Add(kv);
                    }
                    if (keyConts[0].Trim() == "Memory")
                    {
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>("Memory", keyConts[1]);
                        result.Add(kv);
                    }
                    if (keyConts[0].Trim() == "SSD")
                    {
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>("SSD", keyConts[1]);
                        result.Add(kv);
                    }
                    if (keyConts[0].Trim() == "Hard Drive")
                    {
                        KeyValuePair<string, string> kv = new KeyValuePair<string, string>("Hard Drive", keyConts[1]);
                        result.Add(kv);
                    }
                }
            }
        }
        return result;
    }

    string WriteKeywordCloseButton(List<KeyValuePair<string, string>> keys)
    {
        string result = "<a class='closebutton btn' href='/list_sys.aspx?cid=" + ReqCateId + "'><span style='color:red;'>Clear</span></a>";
        string resultKey = "";
        for (int i = 0; i < keys.Count; i++)
        {
            string kcateName = keys[i].Key;

            resultKey += "[" + keys[i].Value + "]";
            // Response.Write(ReqAllKey.Replace("&k=" + ks[i].Key + "|" + ks[i].Value, "") + "<br>");
            result += "<a href='/list_sys.aspx" + ReqAllKey.Replace("&k=" + keys[i].Key + "|" + keys[i].Value, "") + "' class='closebutton btn'><span style='color:black;'>" + kcateName + ":</span>  " + keys[i].Value + " <span style='color:red;'><span aria-hidden=\"true\">&times;</span></span></a>";
        }
        ltKeyHidden.Text = resultKey;
        return keys.Count == 0 ? "" : result;
    }

    string ReqAllKey
    {
        get { return HttpUtility.UrlDecode(HttpContext.Current.Request.Url.Query); }
    }

    string ReqK
    {
        get { return Util.GetStringSafeFromQueryString(Page, "k"); }
    }

    public int ReqCateId
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            var logo = e.Item.FindControl("_ltLogo") as Literal;
            var rpt = e.Item.FindControl("_rptSubParts") as Repeater;
            var data = e.Item.DataItem as LU.Model.M.SysMiniModel;
            logo.Text = LU.BLL.QiNiuImgHelper.Get(data.SysMiniSubParts.Single(p => p.Comment.ToLower().Equals("case")).PartImgSku);
            rpt.DataSource = data.SysMiniSubParts;
            rpt.DataBind();

            var priceLit = e.Item.FindControl("_ltPrice") as Literal;
            if (data.Discount < 1)
            {
                priceLit.Text = string.Concat("Price: <span class=\"priceBig\">$", data.Price, "</span>");
            }
            else
            {
                priceLit.Text = string.Concat("Price: <span class=\"priceBig\"><del>$", data.Price, "</del></span>", "<span class=\"priceBig\">  $", data.Sold, "</span>");
            }
            if (!string.IsNullOrEmpty(data.eBayId))
            {
                priceLit.Text += "<a target='_blank' href='" + (string.Concat(LU.BLL.Config.eBayUrl, data.eBayId)) + "' title='eBay id: " + data.eBayId + "'><span style='display:inlineblock;padding-left:3rem;'>" + LU.BLL.Util.eBayFont() + ": </span><span class='price'>$" + data.eBayPrice + "</span></a>";
            }
        }
    }
}

public struct SysKey
{
    public List<string> cpu { get; set; }

    public List<string> vc { get; set; }

    public List<string> memory { get; set; }

    public List<string> ssd { get; set; }

    public List<string> hd { get; set; }
}