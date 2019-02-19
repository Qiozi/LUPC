using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Q_Admin_ebayMaster_Online_ValideBaySysLogo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ValidEBaySysLogo();
        }
    }
    

    void ValidEBaySysLogo()
    {
        Response.Clear();

        string cont = EbayItemGenerate.GetItem(DBContext, ReqItemId, "<OutputSelector>Item.PictureDetails</OutputSelector>", ReqSKU, true);
        string url = FilterStr(cont);

        if (!string.IsNullOrEmpty(url))
        {
            url = url.ToLower().Replace("http://www.lucomputers.com/pro_img/components/", "");
            string filename = Server.MapPath("/pro_img/components/" + url);
            if (File.Exists(filename))
            {
                DataTable dt = Config.ExecuteDataTable("select sp.luc_sku from tb_ebay_system_parts sp inner join tb_ebay_system_part_comment spc on spc.id=sp.comment_id where system_sku='" + ReqSKU + "' and spc.e_field_name='case'");

                if (dt.Rows.Count == 1)
                {
                    if ((dt.Rows[0][0].ToString() + "_g_1.jpg").ToLower() == url.ToLower())
                    {
                        Response.Write("1");  // OK
                        Response.End();
                    }
                    
                }
                Response.Write("-2"); // 系统不存在，或图片库存与显示不一样。
                Response.End();
            }
            else
            {
                Response.Write(url);
                Response.Write("<br>");
                Response.Write(filename);
                Response.Write("<br>");
                Response.Write("-1"); // 图片不存在
                Response.End();
            }
        }
        else
        {
            Response.Write("0"); // ebay 没有正确值传回
            Response.End();
        }
        Response.Write("-3");
        Response.End();
    }   

    string FilterStr(string str)
    {
        if (!string.IsNullOrEmpty(str))
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);

                return doc["GetItemResponse"]["Item"]["PictureDetails"]["ExternalPictureURL"].InnerText;
            }
catch{}

        }
        return str;
    }


    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    string ReqItemId
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }
}