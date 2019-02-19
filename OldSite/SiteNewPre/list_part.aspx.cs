using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class list_part : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCateId > 0)
            {
                var cateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(ReqCateId));
                if (cateModel != null)
                {
                    var preId = cateModel.menu_pre_serial_no.Value;
                    var subCateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(preId));
                    if (subCateModel != null)
                    {
                        ltCateNameParent.Text = subCateModel.menu_child_name + "";// +cateModel.menu_child_name;
                        ltCateName.Text = cateModel.menu_child_name;

                        #region cate list
                        var parentCateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(0)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < parentCateList.Count; i++)
                        {
                            //ltCatesParent.Text += "<li>" + parentCateList[i].menu_child_name + "</li>";
                        }

                        var cateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(preId)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < cateList.Count; i++)
                        {
                            if (cateList[i].menu_child_serial_no == 378)
                            {
                                continue;
                            }
                            if (cateList[i].page_category.Value == 0)
                            {
                                ltCates.Text += "<li role='pressntation'><a href='list_sys.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                            }
                            else
                            {
                                ltCates.Text += "<li role='pressntation'><a href='list_part.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                            }
                        }
                        #endregion
                    }

                    var keyList = db.tb_product_category_keyword.Where(p => p.category_id.HasValue
                        && p.category_id.Value.Equals(ReqCateId)
                        && p.showit.HasValue
                        && p.showit.Value.Equals(true)
                        && p.IsShowOnWebPage.Equals(true)
                        ).ToList();
                    string keystr = "<table id='keyListArea'>";
                    keystr += "<tr><td colspan='2'>" + WriteKeywordCloseButton(GetKeys(), keyList) + "</td></tr>";
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
                        keystr += "<td><h5><b>" + keyList[i].keyword + "</b></h5></td>";
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
                }
            }
        }
        // Response.Write(Util.GetStringSafeFromQueryString(Page, "k"));
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

    string WriteKeywordCloseButton(List<KeyValuePair<int, string>> ks, List<nicklu2Model.tb_product_category_keyword> kList)
    {
        string result = "<a class='closebutton btn' href='list_part.aspx?cid=" + ReqCateId + "'><span style='color:red;'>Clear</span></a>";
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
            result += "<a href='list_part.aspx" + ReqAllKey.Replace("&k=" + ks[i].Key + "|" + ks[i].Value, "") + "' class='closebutton btn'><span style='color:black;'>" + kcateName + ":</span>  " + ks[i].Value + " <span style='color:red;'><span aria-hidden=\"true\">&times;</span></span></a>";
        }
        ltKeyHidden.Text = resultKey;
        return ks.Count == 0 ? "" : result;
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
}