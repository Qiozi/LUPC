using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;

/// <summary>
/// Summary description for eBayPageHelper
/// </summary>
public class eBayPageHelper
{
    public const int TEMPLETE_ID_FLASH = 6;
    public const int TEMPLETE_ID_NO_FLASH = 10;
    public const int TEMPLETE_ID_MSI_FLASH = 11;
    public const int TEMPLETE_ID_ACER_NO_FLASH = 12;
    public const int TEMPLETE_ID_MSI_NO_FLASH = 13;
    public const int TEMPLETE_ID_ACER_FLASH = 14;
    public const int TEMPLETE_ID_LENOVO_NO_FLASH = 15;

    public const int TEMPLETE_ID_LG_NO_FLASH = 16;
    public const int TEMPLETE_ID_FUJIT_NO_FLASH = 17;

    public const int TEMPLETE_ID_NEW_PART = 9;

    System.Web.UI.Page _page;

    public eBayPageHelper(System.Web.UI.Page page)
    {
        _page = page;
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetPageString(int sku)
    {
        bool is_exist_flash = false;

        ProductModel PM = ProductModel.GetProductModel(sku);

        int templete_id = 0;
        try
        {
            templete_id = GetTempleteID(PM.producter_serial_no, is_exist_flash, PM.menu_child_serial_no);
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
            return ex.Message;
        }
        DataTable tmp_dt = Config.ExecuteDataTable("select templete_content from tb_ebay_templete where  id='" + templete_id.ToString() + "'");

        string templete = "";

        if (tmp_dt.Rows.Count == 1)
        {
            templete = tmp_dt.Rows[0]["templete_content"].ToString();
            //TempleteID = int.Parse(tmp_dt.Rows[0]["id"].ToString());
            DataTable part_desc_dt = Config.ExecuteDataTable(string.Format(@"
select part_comment from tb_part_comment where part_sku='{0}'", sku));

            if (part_desc_dt.Rows.Count == 0)
            {
                string commFullFile = string.Format("{0}{1}_comment.html", _page.Server.MapPath(Config.Part_Comment_Path), sku);
                if (System.IO.File.Exists(commFullFile))
                {
                    ProductDescModel pdm = new ProductDescModel();
                    pdm.SavePartComment(sku, FileHelper.ReadFile(commFullFile), null);
                    part_desc_dt = Config.ExecuteDataTable(string.Format(@"
select part_comment from tb_part_comment where part_sku='{0}'", sku));
                }

            }
            // [brand-logo]
            // [mfp]
            // [img-sku]

            ProductModel pm = ProductModel.GetProductModel(sku);
            //templete = templete.Replace("[lu_web_part_big_img]","<img src='http://www.lucomputers.com/pro_img/COMPONENTS/"+ SKU.ToString()+"_g_1.jpg' />");

            if (templete.IndexOf("[brand-logo]") != -1)
            {
                var brandLogo = string.Empty;
                var brand = pm.producter_serial_no;
                if (!string.IsNullOrEmpty(brand))
                {
                    var brandDT = Config.ExecuteDataTable("select logo_url from tb_producter where producter_name='" + brand.Trim() + "'");
                    if (brandDT.Rows.Count == 1)
                    {
                        if (!string.IsNullOrEmpty(brandDT.Rows[0]["logo_url"].ToString()))
                        {
                            brandLogo = brandDT.Rows[0][0].ToString();
                        }
                    }
                }
                templete = templete.Replace("[brand-logo]", string.Concat("<img src='" + brandLogo + "' class='lu-spec-brand-logo'>"));
            }
            if (templete.IndexOf("[mfp]") != -1)
            {
                templete = templete.Replace("[mfp]", string.IsNullOrEmpty(pm.manufacturer_part_number) ? "N/A" : pm.manufacturer_part_number);
            }
            if (templete.IndexOf("[cate-list]") != -1)
            {
                var cateString = string.Empty;
                var cateDT = Config.ExecuteDataTable("SELECT title, logo1, tourl FROM tb_ebay_template_cates WHERE showit=1 AND tourl IS NOT NULL ORDER BY priority ASC, id ASC");
                if (cateDT.Rows.Count > 0)
                {
                    foreach (DataRow dr in cateDT.Rows)
                    {
                        cateString += string.Format(@"<a class=""lu-spec-item-a"" href=""{2}""><img class=""lu-spec-item-img"" src=""https://www.lucomputers.com/images/ebay-templates/{0}"" /><span class=""lu-spec-cate-a-txt"">{1}</span></a>"
                                                        , dr["logo1"].ToString()
                                                        , dr["title"].ToString()
                                                        , dr["tourl"].ToString());
                    }
                }
                templete = templete.Replace("[cate-list]", cateString);
            }
            if (templete.IndexOf("[img-sku]") != -1)
            {
                var imgString = string.Empty;
                var imgSku = pm.other_product_sku > 0 ? pm.other_product_sku.ToString() : pm.product_serial_no.ToString();
                for (int i = 0; i < pm.product_img_sum; i++)
                {
                    if (i > 2) continue;
                    if (pm.product_img_sum == 1)
                    {
                        imgString += string.Format(@"<div class=""spec-logo-item txtCenter"">
                        <img class=""spec-logo-item-img-sm-one"" src=""https://www.lucomputers.com/pro_img/components/{0}_g_{1}.jpg"" />
                    </div>", imgSku, i + 1);
                    }
                    else
                    {
                        imgString += string.Format(@"<div class=""spec-logo-item"">
                        <img class=""spec-logo-item-img-sm"" src=""https://www.lucomputers.com/pro_img/components/{0}_g_{1}.jpg"" />
                    </div>", imgSku, i + 1);
                    }

                }

                templete = templete.Replace("[img-sku]", imgString);
            }

            if (templete.IndexOf("[lu_web_part_big_img]") != -1)
            {
                templete = templete.Replace("[lu_web_part_big_img]", GetPartFlashOrPhoto(sku));
            }
            if (pm.producter_serial_no.Trim().ToLower() == "lenovo")
            {
                if (pm.product_ebay_name.ToLower().IndexOf("<br>") > -1)
                {
                    templete = templete.Replace("[lu_web_title]", pm.product_ebay_name_2);
                }
                else
                {
                    templete = templete.Replace("[lu_web_title]", pm.product_ebay_name_2 + "<br>");
                }
            }
            else
            {
                templete = templete.Replace("[lu_web_title]", pm.product_ebay_name_2);
            }
            if (templete.IndexOf("[lu_web_part_mfp_code]") != -1)
            {
                templete = templete.Replace("[lu_web_part_mfp_code]", pm.manufacturer_part_number);
            }

            if (templete.IndexOf("[lu_web_part_long_name]") != -1)
            {
                templete = templete.Replace("[lu_web_part_long_name]", pm.product_name_long_en);
            }

            if (templete.IndexOf("[lu_web_part_luc_sku]") != -1)
            {
                templete = templete.Replace("[lu_web_part_luc_sku]", sku.ToString());
            }

            if (templete.IndexOf("[lu_web_part_summary]") != -1)
            {
                string summary = GetPartSummary(sku);
                templete = templete.Replace("[lu_web_part_summary]", summary);
            }

            if (part_desc_dt.Rows.Count == 1)
            {
                templete = templete.Replace("[lu_web_info_row]", ReplaceHref.ReplaceHrefText(part_desc_dt.Rows[0][0].ToString()));//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer").Replace("http://www.msi.com/", "").Replace("https://www.msi.com/", ""));//.Replace("[lu_web_flash_view]", "<iframe src=\"" + Config.http_domain + "view_in_flash.asp?lu_sku=" + lu_sku + "\" style=\"width: 750px; height: 400px; border:0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>");
            }
            else
            {
                throw new Exception("Part is not info..");
            }

            if (templete.IndexOf("[web_page_right]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(_page.MapPath("~/soft_img/params/ebay_tpl_page_right_notebook.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right]", page_right_string);
            }

            if (templete.IndexOf("[Copyrights]") > -1)
            {
                templete = templete.Replace("[Copyrights]", "Copyrights &copy; 2004 ~ " + DateTime.Now.Year.ToString() + ". Lu Computers. All rights reserved.");
            }
        }
        else
        {
            throw new Exception("No Match Default templete");
        }
        return templete;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="part_sku"></param>
    /// <returns></returns>
    public string GetPartSummary(int part_sku)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select s.summary from tb_ebay_part_comment ep inner join tb_ebay_templete_sub_summary s 
on s.id=ep.tpl_summary_id where part_sku='" + part_sku.ToString() + "'"));
        if (dt.Rows.Count > 0)
            return dt.Rows[0][0].ToString();
        return "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="mfp_name"></param>
    /// <param name="is_exist_flash"></param>
    /// <param name="categoryID"></param>
    /// <returns></returns>
    public int GetTempleteID(string mfp_name, bool is_exist_flash, int categoryID)
    {
        int templete_id = 0;
        DataTable dt = Config.ExecuteDataTable(string.Format(@"
Select templete_id from tb_ebay_templete_and_category where part_category_id='{0}' 
and part_brand='{1}' and is_flash='{2}'", categoryID, mfp_name, is_exist_flash ? 1 : 0));

        if (dt.Rows.Count > 0)
        {
            int.TryParse(dt.Rows[0][0].ToString(), out templete_id);
        }

        if (templete_id == 0)
        {
            // throw new Exception("Dont's find Templete ID");
            templete_id = 13;
        }
        return templete_id;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <returns></returns>
    public string GetPartFlashOrPhoto(int luc_sku)
    {
        DirectoryInfo dir = new DirectoryInfo(_page.Server.MapPath("~/flash_view/"));

        if (File.Exists(_page.Server.MapPath(string.Format("~/flash_view/{0}", luc_sku.ToString() + ".swf"))))
        {
            return @"<object classid=""clsid:D27CDB6E-AE6D-11cf-96B8-444553540000"" codebase=""http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,29,0"" width=""450"" height=""450"">
                        <param name=""movie"" value=""http://www.lucomputers.com/flash_view/" + luc_sku.ToString() + @".swf"">
                        <param name=""quality"" value=""High"">
                        <embed src=""http://www.lucomputers.com/flash_view/" + luc_sku.ToString() + @".swf"" quality=""High"" pluginspage=""http://www.macromedia.com/go/getflashplayer"" type=""application/x-shockwave-flash"" width=""450"" height=""450""></embed>
                      </object>";
        }
        else
        {
            var pm = ProductModel.GetProductModel(luc_sku);
            int img_sku = pm.other_product_sku > 0 ? pm.other_product_sku : luc_sku;
            string s = "<img src='" + GetImgFullname.Get(img_sku, 600, 600, 0) + "' border='0' id='lu_part_big_photo_1' style='float:center;max-width:650px;'/>";
            if (pm.product_img_sum > 1)
            {
                s += "<table align='center' cellspecial='5' style='margin-top:20px;margin-left:40px; margin-right:40px;'><tr>";
                for (int i = 0; i < pm.product_img_sum; i++)
                {
                    if (i < 7)
                    {
                        s += @"<td style=""border:1px solid #ccc; cursor: pointer""
onMouseOver=""this.style.border='1px solid #ff9900';document.getElementById('lu_part_big_photo_1').src='" + (GetImgFullname.Get(img_sku, 600, 600, 0)) + @"';""
 onMouseOut=""this.style.border='1px solid #cccccc';""><img border=""0"" src=""" + (GetImgFullname.Get(img_sku, 65, 65, i)) + @""" width=""65"" height=""65""></td>";
                    }
                }
                s += "</tr></table>";
            }

            return s;
        }
    }
}
