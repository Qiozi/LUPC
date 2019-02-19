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
using LU.Data;
using System.Collections.Generic;
using LU.Model.eBay;

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
    LU.Data.nicklu2Entities _context;

    public eBayPageHelper(nicklu2Entities context, System.Web.UI.Page page)
    {
        _context = context;
        _page = page;
        //
        // TODO: Add constructor logic here
        //
    }
    public eBayPageHelper(nicklu2Entities context)
    {
        _context = context;
    }

    public string GetPageString(int sku)
    {
        bool is_exist_flash = false;

        var PM = ProductModel.GetProductModel(_context, sku);

        int templete_id = 0;
        try
        {
            templete_id = GetTempleteID(PM.producter_serial_no, is_exist_flash, PM.menu_child_serial_no.Value);
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
//            DataTable part_desc_dt = Config.ExecuteDataTable(string.Format(@"
//select part_comment from tb_part_comment where part_sku='{0}'", sku));

//            if (part_desc_dt.Rows.Count == 0)
//            {
//                string commFullFile = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, sku);
//                if (System.IO.File.Exists(commFullFile))
//                {
//                    //var pdm = new ProductDescModel();
//                    ProductDescModel.SavePartComment(_context, sku, FileHelper.ReadFile(commFullFile), null);
//                    part_desc_dt = Config.ExecuteDataTable(string.Format(@"
//select part_comment from tb_part_comment where part_sku='{0}'", sku));
//                }

//            }
            // [brand-logo]
            // [mfp]
            // [img-sku]

            var pm = ProductModel.GetProductModel(_context, sku);
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
                    if (i > 6) continue;
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

            string commFullFile = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, sku);
            string single_desc = ReplaceHref.ReplaceHrefText(FileHelper.ReadFile(commFullFile));
            if (!string.IsNullOrEmpty(single_desc))
            {
                templete = templete.Replace("[lu_web_info_row]", single_desc);//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer").Replace("http://www.msi.com/", "").Replace("https://www.msi.com/", ""));//.Replace("[lu_web_flash_view]", "<iframe src=\"" + Config.http_domain + "view_in_flash.asp?lu_sku=" + lu_sku + "\" style=\"width: 750px; height: 400px; border:0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>");
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

            if (templete.IndexOf("[lu-web-may-we-also-suggest-box]") > -1)
            {
                var suggest = GetMayWeAlsoSuggest(pm);
                templete = templete.Replace("[lu-web-may-we-also-suggest-box]", suggest);
            }
        }
        else
        {
            throw new Exception("No Match Default templete");
        }
        return templete;
    }

    /// <summary>
    /// 推荐建议
    /// </summary>
    /// <returns></returns>
    string GetMayWeAlsoSuggest(tb_product pm)
    {
        var filePath = Config.AppPath + "..\\SysDocuments\\eBaySuggest\\" + pm.product_serial_no + ".json";
        if (File.Exists(filePath))
        {
            var parts = Newtonsoft
                            .Json
                            .JsonConvert
                            .DeserializeObject<List<eBayProdInfo>>(File.ReadAllText(filePath));// GetMyWeAlsoSuggestNotebook(pm);
            return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        }
        else
        {
            return string.Empty;
        }

        //switch (pm.menu_child_serial_no)
        //{
        //    case 350: // 笔记本电脑               
        //        var parts = GetMyWeAlsoSuggestNotebook(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        //    case 31: // 主板
        //        parts = GetMyWeAlsoSuggestMotherboard(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        //    case 41: //显卡
        //        parts = GetMyWeAlsoSuggestVideoCard(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        //    case 22: // CPU
        //        parts = GetMyWeAlsoSuggestCPU(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        //    case 36:// 电源
        //        parts = GetMyWeAlsoSuggestPowerSupply(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);

        //    case 25:// hard drive
        //        parts = GetMyWeAlsoSuggestHDD(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);

        //    default:
        //        parts = GetMyWeAlsoSuggestEbayCateId(pm);
        //        return GetWayWeAlsoSuggest(parts, pm.product_serial_no);
        //}
        //return string.Empty;
    }
    string GetLogo(bool isSys, int sku)
    {
        if (isSys)
        {
            return "https://www.lucomputers.com/images/computer-512.png";
        }
        else
        {
            return "https://www.lucomputers.com/pro_img/COMPONENTS/" + sku + "_t.jpg";
        }
    }


    public string GetWayWeAlsoSuggest(List<eBayProdInfo> list, int currSku)
    {
        var resString = "<table class='table'>";
        foreach (var item in list)
        {
            if (list.Count <= 1)
            {
                resString = string.Empty;
                continue;
            }
            if (currSku == item.LUCSku)
            {
                resString += string.Format(@"<tr><td ><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:red;text-decoration:none;'><img src='{3}' width='40'/>&nbsp;{0}</a></td><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:red;text-decoration:none;'>{1}</a></td></tr>",
                    item.Title,
                    item.eBayPrice.ToString("$##,##0.00"),
                    item.eBayItemId,
                    GetLogo(currSku.ToString().Length == 6, item.ImgSku));
            }
            else
            {
                resString += string.Format(@"<tr><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:#333;text-decoration:none;'><img src='{3}' width='40'/>&nbsp;{0}</a></td><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:#333;text-decoration:none;'>{1}</a></td></tr>",
                    item.Title,
                    item.eBayPrice.ToString("$##,##0.00"),
                    item.eBayItemId,
                    GetLogo(currSku.ToString().Length == 6, item.ImgSku));
            }
        }

        if (!string.IsNullOrEmpty(resString) && resString.Length > 100)
        {
            resString += "</table>";
        }
        else
        {
            resString = string.Empty;
        }
        if (!string.IsNullOrEmpty(resString))
        {
            return @"
                    <div class=""main-model borderRadius10 shadaow10"">
                        <div class=""lu-spec-title2"" style=""color:#ff6a00"">
                            <strong>May We Also Suggest</strong>
                        </div>
                            <div style='padding:1rem;display:flex;flex-wrap:wrap; overflow:hidden;'>
                                " + resString + @"
                            </div>
                    </div>";
        }
        else
        {
            return string.Empty;
        }
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
            templete_id = EbaySettings.ebay_part_default_id;// 13;
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
            var pm = ProductModel.GetProductModel(_context, luc_sku);
            int img_sku = pm.other_product_sku.Value > 0 ? pm.other_product_sku.Value : luc_sku;
            string s = "<img src='" + GetImgFullname.Get(img_sku, 600, 600, 0) + "' border='0' id='lu_part_big_photo_1' style='float:center;max-width:650px;'/>";
            if (pm.product_img_sum > 1)
            {
                s += "<table align='center' cellspecial='5' style='margin-top:20px;margin-left:40px; margin-right:40px;'><tr>";
                for (int i = 0; i < pm.product_img_sum; i++)
                {
                    //                    if (i < 8)
                    //                    {
                    //                        s += @"<td style=""border:1px solid #ccc; cursor: pointer""
                    //onMouseOver=""this.style.border='1px solid #ff9900';document.getElementById('lu_part_big_photo_1').src='" + (GetImgFullname.Get(img_sku, 600, 600, i + 1)) + @"';""
                    // onMouseOut=""this.style.border='1px solid #cccccc';""><img border=""0"" src=""" + (GetImgFullname.Get(img_sku, 600, 600, i + 1)) + @""" width=""100"" height=""100""></td>";
                    //                    }
                    // 如果太多图片，有些模板会被撑开。
                    if (i < 6)
                    {
                        s += @"<td style=""border:1px solid #ccc; cursor: pointer""><img border=""0"" src=""" + (GetImgFullname.Get(img_sku, 600, 600, i + 1)) + @""" width=""100"" height=""100""></td>";
                    }
                }
                s += "</tr></table>";
            }

            return s;
        }
    }
}
