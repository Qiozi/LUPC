using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

public partial class list_sys : PageBase
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



                    string json = System.IO.File.ReadAllText(Server.MapPath("computer/sys_key.txt"));

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
                    if (!existKeyIds.Contains("Video Card"))
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
            }
        }
        // Response.Write(Util.GetStringSafeFromQueryString(Page, "k"));
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
        string result = "<a class='closebutton btn' href='list_sys.aspx?cid=" + ReqCateId + "'><span style='color:red;'>Clear</span></a>";
        string resultKey = "";
        for (int i = 0; i < keys.Count; i++)
        {
            string kcateName = keys[i].Key;

            resultKey += "[" + keys[i].Value + "]";
            // Response.Write(ReqAllKey.Replace("&k=" + ks[i].Key + "|" + ks[i].Value, "") + "<br>");
            result += "<a href='list_sys.aspx" + ReqAllKey.Replace("&k=" + keys[i].Key + "|" + keys[i].Value, "") + "' class='closebutton btn'><span style='color:black;'>" + kcateName + ":</span>  " + keys[i].Value + " <span style='color:red;'><span aria-hidden=\"true\">&times;</span></span></a>";
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
}

public struct SysKey
{
    public List<string> cpu { get; set; }

    public List<string> vc { get; set; }

    public List<string> memory { get; set; }

    public List<string> ssd { get; set; }

    public List<string> hd { get; set; }
}