﻿using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using LU.Data;
using LU.Model.eBay;

/// <summary>
/// Summary description for EbayPageText
/// </summary>
public class EbayPageText : PageBase
{
    public class DiffPartItem
    {
        public decimal SystemPriceSource { get; set; }

        public decimal SystemPriceOther { get; set; }

        public int CommentId { get; set; }

        public int PartSku { get; set; }

        public string ItemId { get; set; }

        public int SystemSku { get; set; }

        public int SourceSystemSku { get; set; }

        /// <summary>
        /// 差价
        /// </summary>
        public decimal DiffPrice { get; set; }

        public string ShortTitle { get; set; }

        public List<string> Parts2 { get; set; }

        /// <summary>
        /// source system
        /// </summary>
        public List<string> Parts1 { get; set; }
    }

    const string LU_WEB_EBAY_SYSTEM_MAIN_COMMENT = "[lu_web_ebay_system_main_comment]";
    DataTable KeywordDT = null;
    const string PAGE_PATH_SYSTEM = "~/ebay_page_store/system/";
    const string FILE_EXT = ".html";
    const string IMG_AR1 = @"<img src=""https://www.lucomputers.com/pro_img/COMPONENTS/ar.jpg"" width=""11"" border=""0"" height=""11"">";
    int WarrantySKU = 14420;

    const string Assembling = "All components are fully assembled and the system is tested with Windows OS before shipping.";
    const string Customize = @"We provide you with a system configurater below; you may change the computer to your specifics. 
After you reconfigure the system you can list your own system to eBay and buy through eBay. 
During posting the new system it may take a short while please be patient.
<b>When receive your order we will review this customized computer to ensure all components are compatible, if any changes needed we will contact you to discuss and get your consent. </b>
";

    public EbayPageText()
    {
        KeywordDT = Config.ExecuteDataTable("Select keyword from tb_ebay_system_part_name_keyword");

        //
        // TODO: Add constructor logic here
        //
    }

    public void SaveSystem(string ebay_page, int system_sku)
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}{1}{2}", PAGE_PATH_SYSTEM, system_sku, FILE_EXT)));
        sw.Write(ebay_page);
        sw.Close();
    }

    public string ReadSystem(int system_sku)
    {

        if (File.Exists(System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}{1}{2}", PAGE_PATH_SYSTEM, system_sku, FILE_EXT))))
        {
            StreamReader sr = new StreamReader(System.Web.HttpContext.Current.Server.MapPath(string.Format("{0}{1}{2}", PAGE_PATH_SYSTEM, system_sku, FILE_EXT)));
            string s = sr.ReadToEnd();
            sr.Close();
            return s;
        }
        else
            return "";
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="pc"></param>
    /// <returns></returns>
    public string GetEbayPageHtml(nicklu2Entities context, int sku
        , eBayProdType pc
        , bool is_eBay_create
        , FlashType ft)
    {
        EbayHelper EH = new EbayHelper();
        int categoryID = EH.GetCategoryID(sku);
        int templeteID = 0;
        if (ft == FlashType.barebone)
        {
            templeteID = EbaySettings.ebay_templete_id_barebone;
        }
        else if (ft == FlashType.old && !is_eBay_create)
        {
            templeteID = EH.GetTempleteID(categoryID);
        }
        else if (ft == FlashType.Child && !is_eBay_create)
        {
            templeteID = EbaySettings.ebay_templete_id_new;
        }
        else if (is_eBay_create)
        {
            if (ft == FlashType.old)
            {
                templeteID = EbaySettings.ebay_templete_id_complete_system_Sub;// EH.GetTempleteID(categoryID, sku, is_eBay_create);
            }
            else
                templeteID = EbaySettings.ebay_templete_id_new_sub;
        }
        //throw new Exception(templeteID.ToString());
        return GetEbayPageHtml(context, sku, pc, categoryID, templeteID, is_eBay_create, ft);
    }

    /// <summary>
    /// If the system is ebay create, there would be no flash.
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="pc"></param>
    /// <param name="categoryID"></param>
    /// <param name="templeteID"></param>
    /// <returns></returns>
    public string GetEbayPageHtml(nicklu2Entities context, int sku
        , eBayProdType pc
        , int categoryID
        , int templeteID
        , bool is_ebay_create
        , FlashType ft)
    {
        if (pc == eBayProdType.system)
        {

            var esm = EbaySystemModel.GetEbaySystemModel(context, sku);
            if (esm == null) throw new Exception("Don't find eBay system.");

            esm.templete_id = templeteID;
            //  esm.Update();
            context.SaveChanges();

            DataTable dt = Config.ExecuteDataTable("select templete_content, templete_content2, templete_info,templete_top from tb_ebay_templete where  id='" + templeteID.ToString() + "'");
            if (dt.Rows.Count > 0)
            {
                decimal ebay_sell = 10000M;
                decimal ebay_selected_sell = 0M;
                decimal no_selected_ebay_sell = 0M;
                decimal all_ebay_sell = 0M;
                ebay_sell = GetEbayPrice.GetSysPrice(context, sku
                    , ref ebay_selected_sell
                    , ref no_selected_ebay_sell
                    , ref all_ebay_sell);


                //throw new Exception(templeteID.ToString());

                // templateID = 41
                if (templeteID == EbaySettings.ebay_templete_id_new)
                {
                    return GenerateEbayPageNew41(context, dt.Rows[0]["templete_content"].ToString() + dt.Rows[0]["templete_content2"].ToString()
                    , dt.Rows[0]["templete_info"].ToString()
                    , dt.Rows[0]["templete_top"].ToString()
                    , sku
                    , categoryID
                    , !esm.is_disable_flash_customize.Value
                    , is_ebay_create
                    , esm.is_shrink.Value
                    , ebay_sell);
                }
                else if (templeteID == EbaySettings.ebay_templete_id_new_sub)
                {
                    // template Id = 42
                    return GenerateEbayPageNewSub(context, dt.Rows[0]["templete_content"].ToString() + dt.Rows[0]["templete_content2"].ToString()
                   , dt.Rows[0]["templete_info"].ToString()
                   , dt.Rows[0]["templete_top"].ToString()
                   , sku
                   , categoryID
                   , !esm.is_disable_flash_customize.Value
                   , is_ebay_create
                   , esm.is_shrink.Value
                   , all_ebay_sell
                   , ebay_selected_sell);
                }
                else if (templeteID == 46 || templeteID == EbaySettings.ebay_templete_id_barebone)
                {
                    //new template for ebay 
                    return GenerateEbayPageNew(context, dt.Rows[0]["templete_content"].ToString() + dt.Rows[0]["templete_content2"].ToString()
                    , dt.Rows[0]["templete_info"].ToString()
                    , dt.Rows[0]["templete_top"].ToString()
                    , sku
                    , categoryID
                    , !esm.is_disable_flash_customize.Value
                    , is_ebay_create
                    , esm.is_shrink.Value);
                }
                else
                {
                    return GenerateEbayPage(context, dt.Rows[0]["templete_content"].ToString() + dt.Rows[0]["templete_content2"].ToString()
                    , dt.Rows[0]["templete_info"].ToString()
                    , dt.Rows[0]["templete_top"].ToString()
                    , sku
                    , categoryID
                    , !esm.is_disable_flash_customize.Value
                    , is_ebay_create
                    , esm.is_shrink.Value);
                }
            }
            else
            {
                throw new Exception("Don't find Templete");
            }



        }
        else if (pc == eBayProdType.part)
        {
            return "";
        }
        else
        {
            throw new Exception("Ebay type is error.");
        }
    }


    private string GenerateEbayPageNew(nicklu2Entities context, string templete
                                        , string templete_top
                                        , string templete_info
                                        , int system_sku
                                        , int categoryID
                                        , bool is_exist_flash
                                        , bool is_ebay_create
                                        , bool is_shrink)
    {
        try
        {
            var imageAR1 = @"<img class=""media-object"" src=""https://www.lucomputers.com/images/ebay-templates/ar2.png"" alt=""..."">";
            var imageAR3 = @"<img class=""media-object"" src=""https://www.lucomputers.com/images/ebay-templates/ar3.png"" alt=""..."">";
            bool is_exist_memory = false;
            bool is_exist_HD = false;
            bool is_exist_OD = false;
            bool is_exist_OS = false;
            bool is_exist_LCD = false;

            var caseLogoImage = "";
            //bool is_exist_KB = false;

            var ebayDT = Config.ExecuteDataTable("select itemid,luc_sku from tb_ebay_selling where luc_sku >0");

            // GenerateFlashViewFile gfvf = new GenerateFlashViewFile(system_sku, flashViewFile.system);

            string part_logo_img_name = "";
            var ESM = EbaySystemModel.GetEbaySystemModel(context, system_sku);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            string top_str = "";
            string part_list_upgrades = "";
            string lu_web_top_row_2 = "[lu_web_top_row2]";
            System.Text.StringBuilder sb_sku_list = new System.Text.StringBuilder();
            ArrayList al = new ArrayList();

            #region exec sql 

            DataTable system_part_list = Config.ExecuteDataTable(string.Format(@"select * from 
(Select p.product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
,cs.priority
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku and ep.ebayshowit=1
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' and cs.section=1 
union all 
select video_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_video=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.video_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_video=1 and ep.luc_sku=16684)=1
union all 
select audio_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_audio=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.audio_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_audio=1 and ep.luc_sku=16684)=1
union all 
select network_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_network=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.network_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_network=1 and ep.luc_sku=16684)=1
	) t order by priority asc ", system_sku));

            #endregion

            eBayDataTableDelete16684.Delete(system_part_list);

            #region parts info

            DataRow DR_Warranty = system_part_list.NewRow();
            DR_Warranty["product_serial_no"] = WarrantySKU;
            DR_Warranty["part_group_name"] = "Warranty";
            DR_Warranty["product_name"] = "3 years parts and labor limited warranty. Life time toll free telephone technical support.";
            DR_Warranty["img_sku"] = 859;
            DR_Warranty["product_current_price"] = 0;

            system_part_list.Rows.Add(DR_Warranty);

            DataRow DR_Assembling = system_part_list.NewRow();
            DR_Assembling["product_serial_no"] = 999999;
            DR_Assembling["part_group_name"] = "Assembling";
            DR_Assembling["product_name"] = Assembling;// "All components are fully assembled and the system is tested with Windows OS before shipping.";// "All components are fully assembled and system tested with Windows OS.";
            DR_Assembling["img_sku"] = 999999;
            DR_Assembling["product_current_price"] = 0;

            system_part_list.Rows.Add(DR_Assembling);

            if (!is_ebay_create)
            {
                DataRow dr3 = system_part_list.NewRow();
                dr3["product_serial_no"] = -1;
                dr3["part_group_name"] = "Customize";
                dr3["product_name"] = Customize;// string.Format(@"We provide you with a system configurater below; you may change the computer to your specifics. After you reconfigure the system you can list your own system to eBay and buy through eBay. During posting the new system it may take a short while please be patient. ");

                dr3["img_sku"] = 19325; // 空白图片 
                dr3["product_current_price"] = 0;
                system_part_list.Rows.Add(dr3);
            }
            DataRow dr2 = system_part_list.NewRow();
            dr2["product_serial_no"] = -100;
            dr2["part_group_name"] = "Shipping Out";
            dr2["product_name"] = string.Format(@"We ship out your computer within 3 days and we will email the tracking number to you.");

            dr2["img_sku"] = 19325; // 空白图片 
            dr2["product_current_price"] = 0;
            system_part_list.Rows.Add(dr2);

            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                string comment = dr["part_group_name"].ToString().ToLower();

                if (!is_exist_memory)
                {
                    if (comment.IndexOf("memory") != -1)
                    {
                        is_exist_memory = true;
                    }
                }

                if (!is_exist_HD)
                {
                    if (comment.IndexOf("hard") != -1)
                        is_exist_HD = true;
                }

                if (!is_exist_OD)
                {
                    if (comment.IndexOf("optical") != -1)
                        is_exist_OD = true;
                }

                if (!is_exist_OS)
                {
                    if (comment.ToLower().IndexOf("windows os") != -1)
                        is_exist_OS = true;
                }
                if (!is_exist_LCD)
                {
                    if (comment.ToLower().IndexOf("lcd") != -1)
                        is_exist_LCD = true;
                }
            }

            if (!is_exist_OS)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -101;
                dr["part_group_name"] = "Windows OS";
                dr["product_name"] = "Not included";
                dr["img_sku"] = 19325; // 空白图片 
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }
            if (!is_exist_LCD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -102;
                dr["part_group_name"] = "LCD Monitor";
                dr["product_name"] = "Not included (LCD shown on the picture for decoration only, see details)";
                dr["img_sku"] = 19325; // 空白图片 
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }


            if (!is_exist_OD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -103;
                dr["part_group_name"] = "Optical Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = 19325; // 空白图片 
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }
            if (!is_exist_HD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -104;
                dr["part_group_name"] = "Hard Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = 19325; // 空白图片 
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }

            if (!is_exist_memory)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -105;
                dr["part_group_name"] = "Memory";
                dr["product_name"] = "Not included (Please check motherboard memory capablity bleow)";
                dr["img_sku"] = 19325; // 空白图片 
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }

            #endregion

            // part lsit 1
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                int lu_sku;
                int.TryParse(dr["product_serial_no"].ToString(), out lu_sku);
                string desc = "";

                string comment = dr["part_group_name"].ToString().ToLower();

                string part_path_file = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, lu_sku);// Server.MapPath(string.Format("~/Part_Comment/{0}_comment.html", lu_sku)).ToLower();
               
                //test
                if (Config.isLocalhost)
                    part_path_file = string.Format(@"E:\Projects\LUcomputer\Src\part_comment\{0}_comment.html", lu_sku);

                if (System.IO.File.Exists(part_path_file))
                {
                    string single_desc = ReplaceHref.ReplaceHrefText(FileHelper.ReadFile(part_path_file));
                    desc = single_desc;//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer");

                }

                int desc_length = desc.Trim().Length;
                string s = "";
                // if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) != -1)
                decimal product_current_price;
                decimal.TryParse(dr["product_current_price"].ToString(), out product_current_price);
                if (desc_length > 5)
                {
                    string descT = FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_desc.txt"));

                    s = string.Format(@"
                                        <div class=""panel panel-info"">
                                            <div class=""panel-heading""><strong>{0}</strong><a class=""pull-right"" name=""{2}"">" + imageAR3 + @"</a></div>
                                            <div class=""panel-body"">
                                               <div class=""media"">
                                                  <div class=""media-left phoneNoDisplay"">
                                                      <img class=""media-object"" src=""https://www.lucomputers.com//pro_img/ebay_gallery/{3}/{4}_ebay_list_t_1.jpg"" alt=""..."">
                                                  </div>
                                                  <div class=""media-body"">
                                                    {1}
                                                  </div>
                                                </div>
                                            </div>
                                        </div>"
                                    , ReplacePartName(dr["product_name"].ToString())
                                    , desc
                                    , dr["product_serial_no"].ToString()
                                    , dr["img_sku"].ToString().Substring(0, 1)
                                    , dr["img_sku"].ToString());
                }


                var ebayItemId = string.Empty;
                foreach (DataRow edr in ebayDT.Rows)
                {
                    if (edr["luc_sku"].ToString() == dr["product_serial_no"].ToString())
                    {
                        ebayItemId = edr["itemid"].ToString();
                    }
                }

                top_str += string.Format(@"
                    <tr>
                        <td nowrap>
                            <strong>{1}</strong>
                        </td>
                        <td>{2}</td>
                        <td width=""40"">{0}</td>
                        <td width=""30"">
                            <a href=""#{3}"">{4}</a>
                        </td>
                    </td>"
                            , string.IsNullOrEmpty(ebayItemId) ? "&nbsp;" : @"<a target=""_blank"" href=""https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + ebayItemId + @""">" + ReplaceHref.eBayFont(false) + "</a>"// dr["img_sku"].ToString()
                            , dr["part_group_name"].ToString()
                            , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                            , dr["product_serial_no"].ToString()
                            , desc_length > 5 ? imageAR1 : ""
                            );


                // this.btn_save_page.Text += dr["img_sku"].ToString();
                if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) == -1)
                    str += s;


                //if (System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_t.jpg", dr["img_sku"].ToString()))) && dr["img_sku"].ToString() != "999999")
                {

                    al.Add(string.Format("{0}|{1}", dr["img_sku"].ToString(), dr["part_group_name"].ToString()));
                    //  sb_sku_list.Append("|" + dr["img_sku"].ToString() + "," + dr["part_group_name"].ToString());
                }

                sb.Append(string.Format("[{0}]", lu_sku));

                if (comment.Trim().ToLower() == "case")
                {
                    caseLogoImage = string.Concat("<img class='image-max-logo' src='https://www.lucomputers.com/pro_img/components/", dr["img_sku"].ToString(), "_g_1.jpg' alt='...'>");
                }

            }

            //
            //  lucomputers SKU Number: {0}
            // 
            //top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_top.txt"))
            //    , system_sku, top_str);
            templete = templete
                            .Replace("[lu_web_part_luc_sku]", system_sku.ToString())
                            //.Replace("[lu-web-part-list]", top_str)
                            .Replace("[lu_web_info_row]", str)
                            .Replace("[lu-web-case-logo]", caseLogoImage)
                            .Replace("[lu_web_top_row]", string.Concat("<table class='table table-striped'>", top_str, "</table>"));



            string comm_ids = GetMainCommentIds(system_sku);
            var commDT = Config.ExecuteDataTable("select comment from tb_ebay_system_main_comment where id='" + comm_ids + "'");
            if (commDT.Rows.Count == 1)
            {
                templete = templete.Replace("[lu_web_cpu_comment]", commDT.Rows[0][0].ToString());
            }


            var sameSysString = string.Empty;
            var diffSystemLsit = GetSystemWithSameCPU(context, system_sku);

            var currSystemListString = new eBayPageHelper(context).GetWayWeAlsoSuggest(diffSystemLsit, system_sku);//) "<table class='table'>";
            //foreach (var item in diffSystemLsit)
            //{
            //    if (diffSystemLsit.Count <= 1)
            //    {
            //        currSystemListString = string.Empty;
            //        continue;
            //    }
            //    if (system_sku == item.LUCSku)
            //    {
            //        currSystemListString += string.Format(@"<tr><td ><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:red;text-decoration:none;'><img src='https://www.lucomputers.com/images/computer-512.png' width='40'/>&nbsp;{0}</a></td><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:red;text-decoration:none;'>{1}</a></td></tr>",
            //            item.Title,
            //            item.eBayPrice.ToString("$##,##0.00"),
            //            item.eBayItemId);
            //    }
            //    else
            //    {
            //        currSystemListString += string.Format(@"<tr><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:#333;text-decoration:none;'><img src='https://www.lucomputers.com/images/computer-512.png' width='40'/>&nbsp;{0}</a></td><td><a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={2}' style='color:#333;text-decoration:none;'>{1}</a></td></tr>",
            //            item.Title,
            //            item.eBayPrice.ToString("$##,##0.00"),
            //            item.eBayItemId);
            //    }
            //}

            //if (!string.IsNullOrEmpty(currSystemListString) && currSystemListString.Length > 100)
            //{
            //    currSystemListString += "</table>";
            //}
            if (!string.IsNullOrEmpty(currSystemListString))
            {
                templete = templete.Replace("[lu-web-same-system-box]", currSystemListString);
            }
            else
            {
                templete = templete.Replace("[lu-web-same-system-box]", @"");
            }

            int partCount = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + system_sku.ToString() + "' and ebayshowit=1");
            if (EbaySettings.ebayFlashComboboxQuantitys.IndexOf("[" + partCount.ToString() + "]") != -1)
                templete = ReplaceFlashBindSystem(templete
                    , system_sku
                    , partCount
                    , is_ebay_create ? false : is_exist_flash
                    , is_shrink);


            if (templete.IndexOf("[Copyrights]") > -1)
            {
                templete = templete.Replace("[Copyrights]", "Copyrights &copy; 2004 ~ " + DateTime.Now.Year.ToString() + ". Lu Computers. All rights reserved.");
            }

            return ReplaceSystemSumm(templete, categoryID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 获取相同CPU的系统名称与价格
    /// </summary>
    /// <returns></returns>
    List<eBayProdInfo> GetSystemWithSameCPU(nicklu2Entities context, int sysSku)
    {
        //        var query = context.Database.SqlQuery<eBayProdInfo>(string.Format(@"select 
        //ebay_system_name as Title,
        //ebay_system_price as eBayPrice,
        //ebay.ItemID as eBayItemId,
        //es.id as LUCSku
        //from tb_ebay_system es inner
        //join tb_ebay_selling ebay on ebay.sys_sku = es.id
        //where es.id in (select system_sku from tb_ebay_system_parts where ebayShowIt = 1 and luc_sku = (select max(luc_sku) from tb_ebay_system_parts where system_sku = '{0}' and comment_id = 1))
        //order by ebay_system_name asc", sysSku)).ToList();
        //        return query;
        var filePath = Config.AppPath + "..\\SysDocuments\\eBaySuggest\\" + sysSku + ".json";
        if (File.Exists(filePath))
        {
            var parts = Newtonsoft
                            .Json
                            .JsonConvert
                            .DeserializeObject<List<eBayProdInfo>>(File.ReadAllText(filePath));// GetMyWeAlsoSuggestNotebook(pm);
            return parts;
        }
        else
        {
            return new List<eBayProdInfo>();
        }

    }

    /// <summary>
    /// 取得相似的系统
    /// </summary>
    /// <param name="sku"></param>
    /// <returns></returns>
    List<DiffPartItem> GetSameSystem(int sku)
    {
        var diffItemList = new List<DiffPartItem>();
        var commentIds = new List<int>();
        commentIds.Add(2);
        commentIds.Add(4);
        commentIds.Add(9);
        commentIds.Add(32);
        commentIds.Add(45);
        commentIds.Add(46);

        var sysEbayCodes = Config.ExecuteDataTable("select itemid, sys_sku,BuyItNowPrice from tb_ebay_selling where sys_sku >0");

        //var partEbayCodes = Config.ExecuteDataTable("select itemid, sys_sku,BuyItNowPrice from tb_ebay_selling where luc_sku >0");

        var system = Config.ExecuteDataTable("Select * from tb_ebay_system where id='" + sku + "'");// DBContext.tb_ebay_system.Single(me => me.id.Equals(sku));
        var parendId = int.Parse(system.Rows[0]["parentID"].ToString());
        if (system.Rows[0]["parentID"].ToString() == "0")
        {
            parendId = sku;
            Config.ExecuteNonQuery("Update tb_ebay_system set parentID='" + sku + "' where id='" + sku + "'");
        }
        var systemList = Config.ExecuteDataTable("Select * from tb_ebay_system where parentID ='" + parendId + "'");

        var systemIds = new List<int>();
        var systemIdsStr = string.Empty;
        foreach (DataRow dr in systemList.Rows)
        {
            var id = 0;
            int.TryParse(dr["id"].ToString(), out id);
            systemIds.Add(id);
            if (string.IsNullOrEmpty(systemIdsStr))
            {
                systemIdsStr = id.ToString();
            }
            else
            {
                systemIdsStr += "," + id.ToString();
            }
        }
        if (string.IsNullOrEmpty(systemIdsStr))
        {
            return diffItemList;
        }
        // var systemParts = Config.ExecuteDataTable("Select * from tb_ebay_system_parts where system_sku in (" + systemIdsStr + ")");
        var currSysParts = Config.ExecuteDataTable(@"select * from (select ep.*,  case when p.short_name_for_sys = '' or p.short_name_for_sys is null then p.product_short_name else p.short_name_for_sys end as title, ec.priority from tb_ebay_system_parts  ep inner join tb_product p on ep.luc_sku=p.product_serial_no inner join tb_ebay_system_part_comment ec on ec.id=ep.comment_id ) t where system_sku='" + sku + "' order by priority asc");// systemParts.Where(me => me.system_sku.Value.Equals(sku)).ToList();
        foreach (var item in systemIds)
        {

            if (item != sku)
            {
                var otherSysParts = Config.ExecuteDataTable("select * from (select ep.*,  case when p.short_name_for_sys = '' or p.short_name_for_sys is null then p.product_short_name else p.short_name_for_sys end as title, ec.priority from tb_ebay_system_parts  ep inner join tb_product p on ep.luc_sku=p.product_serial_no inner join tb_ebay_system_part_comment ec on ec.id=ep.comment_id ) t where system_sku='" + item + "' order by priority asc"); //systemParts.Where(me => me.system_sku.Value.Equals(item)).ToList();

                var diffCount = 0;
                DataRow diffPart1 = null, diffPart2 = null;
                foreach (DataRow p1 in currSysParts.Rows)
                {
                    var diff = false;
                    foreach (DataRow p2 in otherSysParts.Rows)
                    {
                        if (p1["luc_sku"].ToString() == p2["luc_sku"].ToString())
                        {
                            diff = true;
                            break;
                        }
                    }
                    if (!diff)
                    {
                        diffPart1 = p1;
                        diffCount++;
                        foreach (DataRow p2 in otherSysParts.Rows)
                        {
                            if (p2["comment_id"].ToString() == p1["comment_id"].ToString())
                            {
                                diffPart2 = p2;
                            }
                        }
                    }
                }
                if (diffCount == 1)
                {
                    if (commentIds.Contains(int.Parse(diffPart1["comment_id"].ToString())))
                    {
                        var otherSystem = Config.ExecuteDataTable("Select * from tb_ebay_system where id='" + diffPart2["system_sku"].ToString() + "'");// DBContext.tb_ebay_system.Single(me => me.id.Equals(diffPart2.system_sku.Value));
                        var ebayItemId = string.Empty;

                        decimal price1 = 0M, price2 = 0M;
                        foreach (DataRow dr in sysEbayCodes.Rows)
                        {
                            if (dr["sys_sku"].ToString() == diffPart2["system_sku"].ToString())
                            {
                                ebayItemId = dr["itemid"].ToString();
                                price2 = decimal.Parse(dr["BuyItNowPrice"].ToString());
                            }
                            if (dr["sys_sku"].ToString() == diffPart1["system_sku"].ToString())
                            {
                                price1 = decimal.Parse(dr["BuyItNowPrice"].ToString());
                            }
                        }

                        var productDT = Config.ExecuteDataTable("Select ifnull(short_name_for_sys,product_short_name) product_short_name from tb_product where product_serial_no = '" + diffPart2["luc_sku"].ToString() + "'");
                        var parts1 = new List<string>();
                        foreach (DataRow dr in currSysParts.Rows)
                        {
                            if (dr["title"].ToString() != "None selected" && !string.IsNullOrEmpty(dr["title"].ToString()))
                            {
                                parts1.Add(dr["title"].ToString());
                            }
                        }
                        var parts2 = new List<string>();
                        foreach (DataRow dr in otherSysParts.Rows)
                        {
                            if (dr["title"].ToString() != "None selected" && !string.IsNullOrEmpty(dr["title"].ToString()))
                            {
                                parts2.Add(dr["title"].ToString());
                            }
                        }
                        diffItemList.Add(new DiffPartItem
                        {
                            CommentId = int.Parse(diffPart2["comment_id"].ToString()),
                            ItemId = ebayItemId,
                            PartSku = int.Parse(diffPart2["luc_sku"].ToString()),
                            SourceSystemSku = sku,
                            SystemSku = int.Parse(diffPart2["system_sku"].ToString()),
                            DiffPrice = price2 - price1,
                            ShortTitle = productDT.Rows[0]["product_short_name"].ToString(),
                            Parts2 = parts2,
                            SystemPriceSource = price1,
                            SystemPriceOther = price2,
                            Parts1 = parts1
                        });
                    }
                }
            }
        }
        return diffItemList;
    }




    /// <summary>
    /// 
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="templete_top"></param>
    /// <param name="templete_info"></param>
    /// <param name="system_sku"></param>
    /// <param name="categoryID"></param>
    /// <param name="is_exist_flash"></param>
    /// <returns></returns>
    private string GenerateEbayPage(nicklu2Entities context, string templete
        , string templete_top
        , string templete_info
        , int system_sku
        , int categoryID
        , bool is_exist_flash
        , bool is_ebay_create
        , bool is_shrink)
    {
        try
        {
            bool is_exist_memory = false;
            bool is_exist_HD = false;
            bool is_exist_OD = false;
            bool is_exist_OS = false;
            bool is_exist_LCD = false;
            //bool is_exist_KB = false;

            GenerateFlashViewFile gfvf = new GenerateFlashViewFile(system_sku, flashViewFile.system);

            string part_logo_img_name = "";
            var ESM = EbaySystemModel.GetEbaySystemModel(context, system_sku);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            string top_str = "";
            string part_list_upgrades = "";
            string lu_web_top_row_2 = "[lu_web_top_row2]";
            System.Text.StringBuilder sb_sku_list = new System.Text.StringBuilder();
            ArrayList al = new ArrayList();


            DataTable system_part_list = Config.ExecuteDataTable(string.Format(@"select * from 
(Select p.product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
,cs.priority
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku and ep.ebayshowit=1
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' and cs.section=1 
union all 
select video_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_video=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.video_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_video=1 and ep.luc_sku=16684)=1
union all 
select audio_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_audio=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.audio_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_audio=1 and ep.luc_sku=16684)=1
union all 
select network_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_network=1) priority
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.network_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_network=1 and ep.luc_sku=16684)=1
	) t order by priority asc ", system_sku));

            eBayDataTableDelete16684.Delete(system_part_list);


            DataRow DR_Warranty = system_part_list.NewRow();
            DR_Warranty["product_serial_no"] = WarrantySKU;
            DR_Warranty["part_group_name"] = "Warranty";
            DR_Warranty["product_name"] = "3 years parts and labor limited warranty. Life time toll free telephone technical support.";
            DR_Warranty["img_sku"] = 859;
            DR_Warranty["product_current_price"] = 0;

            system_part_list.Rows.Add(DR_Warranty);

            DataRow DR_Assembling = system_part_list.NewRow();
            DR_Assembling["product_serial_no"] = 999999;
            DR_Assembling["part_group_name"] = "Assembling";
            DR_Assembling["product_name"] = Assembling;// "All components are fully assembled and the system is tested with Windows OS before shipping.";// "All components are fully assembled and system tested with Windows OS.";
            DR_Assembling["img_sku"] = 999999;
            DR_Assembling["product_current_price"] = 0;

            system_part_list.Rows.Add(DR_Assembling);


            if (!is_ebay_create)
            {
                DataRow dr3 = system_part_list.NewRow();
                dr3["product_serial_no"] = -1;
                dr3["part_group_name"] = "Customize";
                dr3["product_name"] = Customize;// string.Format(@"We provide you with a system configurater below; you may change the computer to your specifics. After you reconfigure the system you can list your own system to eBay and buy through eBay. During posting the new system it may take a short while please be patient. ");

                dr3["img_sku"] = -1;
                dr3["product_current_price"] = 0;
                system_part_list.Rows.Add(dr3);
            }
            DataRow dr2 = system_part_list.NewRow();
            dr2["product_serial_no"] = -100;
            dr2["part_group_name"] = "Shipping Out";
            dr2["product_name"] = string.Format(@"We ship out your computer within 3 days and we will email the tracking number to you.");

            dr2["img_sku"] = -1;
            dr2["product_current_price"] = 0;
            system_part_list.Rows.Add(dr2);

            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                string comment = dr["part_group_name"].ToString().ToLower();

                if (!is_exist_memory)
                {
                    if (comment.IndexOf("memory") != -1)
                    {
                        is_exist_memory = true;
                    }
                }

                if (!is_exist_HD)
                {
                    if (comment.IndexOf("hard") != -1)
                        is_exist_HD = true;
                }

                if (!is_exist_OD)
                {
                    if (comment.IndexOf("optical") != -1)
                        is_exist_OD = true;
                }

                if (!is_exist_OS)
                {
                    if (comment.ToLower().IndexOf("windows os") != -1)
                        is_exist_OS = true;
                }
                if (!is_exist_LCD)
                {
                    if (comment.ToLower().IndexOf("lcd") != -1)
                        is_exist_LCD = true;
                }
            }

            if (!is_exist_OS)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -101;
                dr["part_group_name"] = "Windows OS";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }
            if (!is_exist_LCD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -102;
                dr["part_group_name"] = "LCD Monitor";
                dr["product_name"] = "Not included (LCD shown on the picture for decoration only, see details)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }


            if (!is_exist_OD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -103;
                dr["part_group_name"] = "Optical Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }
            if (!is_exist_HD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -104;
                dr["part_group_name"] = "Hard Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }

            if (!is_exist_memory)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -105;
                dr["part_group_name"] = "Memory";
                dr["product_name"] = "Not included (Please check motherboard memory capablity bleow)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, system_part_list.Rows.Count - 4);
            }

            // part lsit 1
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                int lu_sku;
                int.TryParse(dr["product_serial_no"].ToString(), out lu_sku);
                string desc = "";

                string comment = dr["part_group_name"].ToString().ToLower();

                string part_path_file = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, lu_sku);// Server.MapPath(string.Format("~/Part_Comment/{0}_comment.html", lu_sku)).ToLower();
        
                //test
                if (Config.isLocalhost)
                    part_path_file = string.Format(@"E:\Projects\LUcomputer\Src\part_comment\{0}_comment.html", lu_sku);

                if (System.IO.File.Exists(part_path_file))
                {
                    string single_desc = ReplaceHref.ReplaceHrefText(FileHelper.ReadFile(part_path_file));
                    desc = single_desc;//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer");

                }

                int desc_length = desc.Trim().Length;
                string s = "";
                // if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) != -1)
                decimal product_current_price;
                decimal.TryParse(dr["product_current_price"].ToString(), out product_current_price);
                if (desc_length > 5)
                {
                    string descT = FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_desc.txt"));

                    s = string.Format(descT
                        , string.Format("{0}<a name=\"{1}\"></a>", ReplacePartName(dr["product_name"].ToString()), dr["product_serial_no"].ToString())
                        , Config.http_domain + gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString()))
                        , desc
                        , gfvf.GetPartMinImgForEbayG(int.Parse(dr["img_sku"].ToString())));

                }

                //
                //
                part_logo_img_name += GetPartLogoIMG(gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString())));

                if (i % 2 == (system_part_list.Rows.Count % 2))
                {
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString(), desc_length > 5 ? IMG_AR1 : "");
                }
                else
                {
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , desc_length > 5 ? IMG_AR1 : "");
                }
                //top_str += string.Format(templete_top, dr["part_group_name"].ToString(), dr["product_name"].ToString());


                // this.btn_save_page.Text += dr["img_sku"].ToString();
                if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) == -1)
                    str += s;


                //if (System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_t.jpg", dr["img_sku"].ToString()))) && dr["img_sku"].ToString() != "999999")
                {

                    al.Add(string.Format("{0}|{1}", dr["img_sku"].ToString(), dr["part_group_name"].ToString()));
                    //  sb_sku_list.Append("|" + dr["img_sku"].ToString() + "," + dr["part_group_name"].ToString());
                }

                sb.Append(string.Format("[{0}]", lu_sku));

            }

            //
            //  lucomputers SKU Number: {0}
            // 
            top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_top.txt"))
                , system_sku, top_str);

            //
            // generate flash view file.
            // 
            gfvf.al_sku = al;
            if (gfvf.Export())
            { }

            if (!is_exist_OS)
            {
                //System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath("/soft_img/params/no_win_OS.txt"));
                //templete = templete.Replace("[lu_web_no_window_OS_info]", sr.ReadToEnd());
                //sr.Close();
                //sr.Dispose();

                templete = templete.Replace("[lu_web_no_window_OS_info]", "");
            }
            else
                templete = templete.Replace("[lu_web_no_window_OS_info]", "");

            templete = ReplaceSystemTitle(templete.Replace("[lu_web_info_row]", str).Replace("[lu_web_top_row]", top_str).Replace("[lu_web_content_part_logo]", part_logo_img_name), system_sku);

            if (templete.IndexOf(lu_web_top_row_2) != -1
                && part_list_upgrades.Length > 100)
            {
                templete = templete.Replace(lu_web_top_row_2, string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list2_main.txt")), part_list_upgrades));

            }
            else if (!ESM.is_disable_flash_customize.Value)
            {
                templete = templete.Replace(lu_web_top_row_2, "");
            }
            else
                templete = templete.Replace(lu_web_top_row_2, "");
            templete = ReplaceSystemBIGImg(templete
                , system_sku
                , !is_ebay_create
                , ESM.parent_ebay_itemid);
            templete = ReplaceSystemMainComment(templete, system_sku);

            int partCount = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + system_sku.ToString() + "' and ebayshowit=1");
            if (EbaySettings.ebayFlashComboboxQuantitys.IndexOf("[" + partCount.ToString() + "]") != -1)
                templete = ReplaceFlashBindSystem(templete
                    , system_sku
                    , partCount
                    , is_ebay_create ? false : is_exist_flash
                    , is_shrink);
            if (templete.IndexOf("[web_page_right]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();

                sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string += sr.ReadToEnd();
                sr.Close();

                templete = templete.Replace("[web_page_right]", page_right_string);
            }

            if (templete.IndexOf("[web_page_right_1]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_1]", page_right_string);
            }
            if (templete.IndexOf("[web_page_right_2]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_2]", page_right_string);
            }

            if (templete.IndexOf("[web_page_right_ad]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_ad.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_ad]", page_right_string);
            }
            if (templete.IndexOf("[Copyrights]") > -1)
            {
                templete = templete.Replace("[Copyrights]", "Copyrights &copy; 2004 ~ " + DateTime.Now.Year.ToString() + ". Lu Computers. All rights reserved.");
            }

            return ReplaceSystemSumm(templete, categoryID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 2011-1-29
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="templete_top"></param>
    /// <param name="templete_info"></param>
    /// <param name="system_sku"></param>
    /// <param name="categoryID"></param>
    /// <param name="is_exist_flash"></param>
    /// <returns></returns>
    private string GenerateEbayPageNew41(nicklu2Entities context, string templete
        , string templete_top
        , string templete_info
        , int system_sku
        , int categoryID
        , bool is_exist_flash
        , bool is_ebay_create
        , bool is_shrink
        , decimal sys_sell)
    {
        try
        {
            bool is_exist_memory = false;
            bool is_exist_HD = false;
            bool is_exist_OD = false;
            bool is_exist_OS = false;
            bool is_exist_LCD = false;
            // int WarrantySKU = 14420;

            GenerateFlashViewFile gfvf = new GenerateFlashViewFile(system_sku, flashViewFile.system);

            string part_logo_img_name = "";
            var ESM = EbaySystemModel.GetEbaySystemModel(context, system_sku);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            string top_str = "";
            //string part_list_upgrades = "";
            string lu_web_top_row_2 = "[lu_web_top_row2]";
            System.Text.StringBuilder sb_sku_list = new System.Text.StringBuilder();
            ArrayList al = new ArrayList();
            string curr_sys_part_list = "";


            DataTable system_part_list = Config.ExecuteDataTable(string.Format(@"select * from 
(Select concat(p.product_serial_no,'') product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
,cs.priority, ep.is_belong_price
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku and ep.ebayshowit=1
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' and cs.section=1 
union all 
select video_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_video=1) priority, ep.is_belong_price
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.video_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_video=1 and ep.luc_sku=16684)=1
union all 
select audio_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_audio=1) priority, ep.is_belong_price
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.audio_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and  ep.ebayshowit=1 where ep.system_sku='{0}' and is_audio=1 and ep.luc_sku=16684)=1
union all 
select network_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
	, p.product_current_price, (select max(priority) from tb_ebay_system_part_comment where is_network=1) priority, ep.is_belong_price
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.network_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and  ep.ebayshowit=1 where ep.system_sku='{0}' and is_network=1 and ep.luc_sku=16684)=1
	) t order by  priority asc ", system_sku));

            eBayDataTableDelete16684.Delete(system_part_list);


            DataRow DR_Warranty = system_part_list.NewRow();
            DR_Warranty["product_serial_no"] = WarrantySKU;
            DR_Warranty["part_group_name"] = "Warranty";
            DR_Warranty["product_name"] = "3 years parts and labor limited warranty. Life time toll free telephone technical support.";
            DR_Warranty["img_sku"] = 859;
            DR_Warranty["product_current_price"] = 0;
            DR_Warranty["is_belong_price"] = 1;
            //
            // insert to DataTable
            int is_belong_price_count = 0;
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                if (system_part_list.Rows[i]["is_belong_price"].ToString() == "0")
                {
                    system_part_list.Rows.InsertAt(DR_Warranty, i);
                    break;
                }
                else
                    is_belong_price_count += 1;
            }
            if (is_belong_price_count == system_part_list.Rows.Count)
                system_part_list.Rows.Add(DR_Warranty);

            DataRow DR_Assembling = system_part_list.NewRow();
            DR_Assembling["product_serial_no"] = 999999;
            DR_Assembling["part_group_name"] = "Assembling";
            DR_Assembling["product_name"] = Assembling;// "All components are fully assembled and the system is tested with Windows OS before shipping.";
            DR_Assembling["img_sku"] = 999999;
            DR_Assembling["product_current_price"] = 0;
            DR_Assembling["is_belong_price"] = 1;
            //
            // insert to DataTable
            is_belong_price_count = 0;
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                if (system_part_list.Rows[i]["is_belong_price"].ToString() == "0")
                {
                    system_part_list.Rows.InsertAt(DR_Assembling, i);
                    break;
                }
                else
                    is_belong_price_count += 1;
            }
            if (is_belong_price_count == system_part_list.Rows.Count)
                system_part_list.Rows.Add(DR_Assembling);

            // part lsit 1
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                int lu_sku;
                int.TryParse(dr["product_serial_no"].ToString(), out lu_sku);
                string desc = "";

                string comment = dr["part_group_name"].ToString().ToLower();

                if (!is_exist_memory)
                {
                    if (comment.IndexOf("memory") != -1)
                    {
                        is_exist_memory = true;
                    }
                }

                if (!is_exist_HD)
                {
                    if (comment.IndexOf("hard") != -1)
                        is_exist_HD = true;
                }

                if (!is_exist_OD)
                {
                    if (comment.IndexOf("optical") != -1)
                        is_exist_OD = true;
                }

                if (!is_exist_OS)
                {
                    if (comment.ToLower().IndexOf("windows os") != -1)
                        is_exist_OS = true;
                }
                if (!is_exist_LCD)
                {
                    if (comment.ToLower().IndexOf("lcd") != -1)
                        is_exist_LCD = true;
                }


                string part_path_file = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, lu_sku);// Server.MapPath(string.Format("~/Part_Comment/{0}_comment.html", lu_sku)).ToLower();
                //test
                if (Config.isLocalhost)
                    part_path_file = string.Format(@"E:\Projects\LUcomputer\Src\part_comment\{0}_comment.html", lu_sku);

                if (System.IO.File.Exists(part_path_file)
                    && dr["is_belong_price"].ToString() == "1")
                {
                    string single_desc = ReplaceHref.ReplaceHrefText(FileHelper.ReadFile(part_path_file));
                    desc = single_desc;//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer");

                }

                int desc_length = desc.Trim().Length;
                string s = "";
                // if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) != -1)
                decimal product_current_price;
                decimal.TryParse(dr["product_current_price"].ToString(), out product_current_price);
                if (desc_length > 5)
                {
                    string descT = FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_desc.txt"));

                    s = string.Format(descT
                        , string.Format("{0}<a name=\"{1}\"></a>", ReplacePartName(dr["product_name"].ToString()), dr["product_serial_no"].ToString())
                        , Config.http_domain + gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString()))
                        , desc
    , gfvf.GetPartMinImgForEbayG(int.Parse(dr["img_sku"].ToString())));

                }

                //
                //
                part_logo_img_name += GetPartLogoIMG(gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString())));

                string part_name = "";
                if (dr["is_belong_price"].ToString() == "1")
                {
                    part_name = ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()));
                }
                else
                {
                    part_name = "Not included";
                }


                if (i % 2 == (system_part_list.Rows.Count % 2))
                {

                    if ("Assembling" == dr["part_group_name"].ToString())
                    {
                        top_str += string.Format(@"<tr><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td></tr>");
                        curr_sys_part_list += string.Format(@"<tr><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td></tr>");

                    }
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString(), desc_length > 5 ? IMG_AR1 : "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , part_name
                        , dr["product_serial_no"].ToString(), desc_length > 5 ? IMG_AR1 : "");

                }
                else
                {
                    if ("Assembling" == dr["part_group_name"].ToString())
                    {
                        top_str += string.Format(@"<tr><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td></tr>");
                        curr_sys_part_list += string.Format(@"<tr><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td><td bgcolor=""#ffffff"">&nbsp;</td></tr>");
                    }
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , desc_length > 5 ? IMG_AR1 : "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , part_name
                        , dr["product_serial_no"].ToString()
                        , desc_length > 5 ? IMG_AR1 : "");

                }
                //top_str += string.Format(templete_top, dr["part_group_name"].ToString(), dr["product_name"].ToString());


                // this.btn_save_page.Text += dr["img_sku"].ToString();
                if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) == -1)
                    str += s;


                //if (System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_t.jpg", dr["img_sku"].ToString()))) && dr["img_sku"].ToString() != "999999")
                {

                    al.Add(string.Format("{0}|{1}", dr["img_sku"].ToString(), dr["part_group_name"].ToString()));
                    //  sb_sku_list.Append("|" + dr["img_sku"].ToString() + "," + dr["part_group_name"].ToString());
                }

                sb.Append(string.Format("[{0}]", lu_sku));
            }



            // if part isn't exist Memory, Hard Drive, Optical Drive, LCD, OSand add Part Comment.

            if (!is_exist_OS)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = "win";
                dr["part_group_name"] = "Windows OS";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, 3);
            }
            if (!is_exist_LCD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = "promo";
                dr["part_group_name"] = "LCD Monitor";
                dr["product_name"] = "Not included (LCD shown on the picture for decoration only, see details)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, 3);
            }


            if (!is_exist_OD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Optical Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, 3);
            }
            if (!is_exist_HD)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Hard Drives";
                dr["product_name"] = "Not included";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, 3);
            }

            if (!is_exist_memory)
            {
                DataRow dr = system_part_list.NewRow();
                dr["product_serial_no"] = -1;
                dr["part_group_name"] = "Memory";
                dr["product_name"] = "Not included (Please check motherboard memory capablity bleow)";
                dr["img_sku"] = -1;
                dr["product_current_price"] = 0;
                system_part_list.Rows.InsertAt(dr, 3);
            }



            //
            // current system part list.
            templete = templete.Replace("[lu_web_ebay_system_part_list_1]"
                , string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list10_main.txt")), system_sku, curr_sys_part_list));

            //
            //  lucomputers SKU Number: {0}
            // 
            //top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_top.txt"))
            //    , system_sku, top_str);
            top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list11_main.txt"))
               , system_sku
               , top_str
               , ESM.ebay_system_price);

            //
            // generate flash view file.
            // 
            gfvf.al_sku = al;
            if (gfvf.Export())
            { }

            if (!is_exist_OS)
            {
                //System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath("/soft_img/params/no_win_OS.txt"));
                //templete = templete.Replace("[lu_web_no_window_OS_info]", sr.ReadToEnd());
                //sr.Close();
                //sr.Dispose();

                templete = templete.Replace("[lu_web_no_window_OS_info]", "");
            }
            else
                templete = templete.Replace("[lu_web_no_window_OS_info]", "");

            templete = ReplaceSystemTitle(templete.Replace("[lu_web_info_row]", str).Replace("[lu_web_top_row]", top_str).Replace("[lu_web_content_part_logo]", part_logo_img_name), system_sku);

            //if (templete.IndexOf(lu_web_top_row_2) != -1 && part_list_upgrades.Length > 100)
            //{
            //    templete = templete.Replace(lu_web_top_row_2, "");

            //}
            //else
            templete = templete.Replace(lu_web_top_row_2, "");

            templete = ReplaceSystemBIGImg(templete
                , system_sku
                , !is_ebay_create
                , ESM.parent_ebay_itemid);
            templete = ReplaceSystemMainComment(templete, system_sku);



            if (templete.IndexOf("[system_all_total]") > -1)
            {
                templete = templete.Replace("[system_all_total]", ESM.ebay_system_price.ToString());
            }

            int partCount = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + system_sku.ToString() + "' and ebayshowit=1");
            if (EbaySettings.ebayFlashComboboxQuantitys.IndexOf("[" + partCount.ToString() + "]") != -1)
                templete = ReplaceFlashBindSystem(templete
                    , system_sku
                    , partCount
                    , is_ebay_create ? false : is_exist_flash
                    , is_shrink);

            if (templete.IndexOf("[web_page_right]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();

                sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string += sr.ReadToEnd();
                sr.Close();

                templete = templete.Replace("[web_page_right]", page_right_string);
            }

            if (templete.IndexOf("[web_page_right_1]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_1]", page_right_string);
            }
            if (templete.IndexOf("[web_page_right_2]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_2]", page_right_string);
            }

            if (templete.IndexOf("[web_page_right_ad]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_ad.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_ad]", page_right_string);
            }

            if (templete.IndexOf("[Copyrights]") > -1)
            {
                templete = templete.Replace("[Copyrights]", "Copyrights &copy; 2004 ~ " + DateTime.Now.Year.ToString() + ". Lu Computers. All rights reserved.");
            }
            return ReplaceSystemSumm(templete, categoryID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// 把数据表1，里已存在数据表2的零件移除
    /// 条件：comment_id, sku, part_group_id
    /// </summary>
    /// <param name="dt1"></param>
    /// <param name="dt2"></param>
    void RemoveSameCommentAndSKU(DataTable dt1, DataTable dt2)
    {
        for (int i = 0; i < dt1.Rows.Count; i++)
        {
            DataRow pdr = dt1.Rows[i];

            foreach (DataRow dr in dt2.Rows)
            {
                if (pdr["comment_id"].ToString() == dr["comment_id"].ToString()
                    && pdr["product_serial_no"].ToString() == dr["product_serial_no"].ToString())
                {
                    dt1.Rows.RemoveAt(i);
                    i--;
                    break;
                }
            }
        }
    }
    /// <summary>
    /// 2011-1-29
    /// 
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="templete_top"></param>
    /// <param name="templete_info"></param>
    /// <param name="system_sku"></param>
    /// <param name="categoryID"></param>
    /// <param name="is_exist_flash"></param>
    /// <returns></returns>
    private string GenerateEbayPageNewSub(nicklu2Entities context, string templete
        , string templete_top
        , string templete_info
        , int system_sku
        , int categoryID
        , bool is_exist_flash
        , bool is_ebay_create
        , bool is_shrink
        , decimal all_ebay_sell
        , decimal ebay_belong_sell)
    {
        try
        {
            bool is_exist_memory = false;
            bool is_exist_HD = false;
            bool is_exist_OD = false;
            bool is_exist_OS = false;
            bool is_exist_LCD = false;

            GenerateFlashViewFile gfvf = new GenerateFlashViewFile(system_sku, flashViewFile.system);

            string part_logo_img_name = "";
            var ESM = EbaySystemModel.GetEbaySystemModel(context, system_sku);
            if (ESM == null)
                return "No Match System.";
            var ParentESM = EbaySystemModel.GetEbaySystemModel(context, ESM.parentID.Value);
            if (ParentESM == null)
                return "No Match Parent System.";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string str = "";
            string top_str = "";
            string part_list_upgrades = "";
            string lu_web_top_row_2 = "[lu_web_top_row2]";
            System.Text.StringBuilder sb_sku_list = new System.Text.StringBuilder();
            ArrayList al = new ArrayList();
            string curr_sys_part_list = "";

            DataTable system_part_list = Config.ExecuteDataTable(string.Format(@"
select * from (
Select p.product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
, ep.is_belong_price, cs.priority, ep.is_label_of_flash
,ep.comment_id
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku and ep.ebayshowit=1
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}' 
and ep.is_label_of_flash=0

union all 
select video_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_video=1) priority, ep.is_label_of_flash
,ep.comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.video_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_video=1 and ep.luc_sku=16684)=1
union all 
select audio_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_audio=1) priority, ep.is_label_of_flash
,ep.comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.audio_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_audio=1 and ep.luc_sku=16684)=1
union all 
select network_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_network=1) priority, ep.is_label_of_flash
, ep.comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.network_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_network=1 and ep.luc_sku=16684)=1
) t where is_label_of_flash=0
order by priority asc", system_sku));

            DataTable system_part_list2 = Config.ExecuteDataTable(string.Format(@"
select * from (
Select p.product_serial_no
,case when length(p.product_ebay_name)>5 then p.product_ebay_name
      else p.product_name end as product_name
,cs.comment part_group_name
,case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
,p.product_current_price
, ep.is_belong_price , cs.priority
,ep.comment_id
from tb_product p
    inner join tb_ebay_system_parts ep          on p.product_serial_no=ep.luc_sku and ep.ebayshowit=1
    inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id
where ep.system_sku='{0}'
and ep.is_belong_price=1

union all 
select video_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_video=1) priority
,(select id from tb_ebay_system_part_comment where is_video=1 limit 0,1) comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.video_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_video=1 and ep.luc_sku<>16684)=0
union all 
select audio_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
	    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_audio=1) priority
,(select id from tb_ebay_system_part_comment where is_audio=1 limit 0,1) comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.audio_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_audio=1 and ep.luc_sku<>16684)=0
union all 
select network_sku, p.product_ebay_name, 
	(select comment from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment
	, case when other_product_sku >0 then other_product_sku
    else p.product_serial_no end as img_sku
	, p.product_current_price, ep.is_belong_price, (select max(priority) from tb_ebay_system_part_comment where is_network=1) priority
,(select id from tb_ebay_system_part_comment where is_network=1 limit 0,1) comment_id
	from tb_part_relation_motherboard_video_audio_port pr 
	inner join tb_ebay_system_parts ep on ep.luc_sku = pr.mb_sku and ep.ebayshowit=1
	inner join tb_ebay_system_part_comment ec on ec.id= ep.comment_id  and ec.is_mb=1 and ep.system_sku='{0}'
	inner join tb_product p on p.product_serial_no = pr.network_sku
	where (select count(cs.id) from tb_ebay_system_parts ep   
		inner join tb_ebay_system_part_comment cs   on ep.comment_id=cs.id and ep.ebayshowit=1 where ep.system_sku='{0}' and is_network=1 and ep.luc_sku<>16684)=0
) t where is_belong_price=1

order by priority asc", ESM.parentID));

            eBayDataTableDelete16684.Delete(system_part_list);
            eBayDataTableDelete16684.Delete(system_part_list2);
            RemoveSameCommentAndSKU(system_part_list, system_part_list2);


            DataRow DR_Warranty = system_part_list2.NewRow();
            DR_Warranty["product_serial_no"] = WarrantySKU;
            DR_Warranty["part_group_name"] = "Warranty";
            DR_Warranty["product_name"] = "3 years parts and labor limited warranty. Life time toll free telephone technical support.";
            DR_Warranty["img_sku"] = 859;
            DR_Warranty["product_current_price"] = 0;
            DR_Warranty["is_belong_price"] = 1;

            system_part_list2.Rows.Add(DR_Warranty);

            DataRow DR_Assembling = system_part_list2.NewRow();
            DR_Assembling["product_serial_no"] = 999999;
            DR_Assembling["part_group_name"] = "Assembling";
            DR_Assembling["product_name"] = Assembling;// "All components are fully assembled and the system is tested with Windows OS before shipping.";
            DR_Assembling["img_sku"] = 999999;
            DR_Assembling["product_current_price"] = 0;
            DR_Assembling["is_belong_price"] = 1;

            system_part_list2.Rows.Add(DR_Assembling);



            // part lsit 1
            #region part list 1
            for (int i = 0; i < system_part_list.Rows.Count; i++)
            {
                DataRow dr = system_part_list.Rows[i];
                int lu_sku;
                int.TryParse(dr["product_serial_no"].ToString(), out lu_sku);
                string desc = "";

                string comment = dr["part_group_name"].ToString().ToLower();

                if (!is_exist_memory)
                {
                    if (comment.IndexOf("memory") != -1)
                    {
                        is_exist_memory = true;
                    }
                }

                if (!is_exist_HD)
                {
                    if (comment.IndexOf("hard") != -1)
                        is_exist_HD = true;
                }

                if (!is_exist_OD)
                {
                    if (comment.IndexOf("optical") != -1)
                        is_exist_OD = true;
                }

                if (!is_exist_OS)
                {
                    if (comment.ToLower().IndexOf("windows os") != -1)
                        is_exist_OS = true;
                }
                if (!is_exist_LCD)
                {
                    if (comment.ToLower().IndexOf("lcd") != -1)
                        is_exist_LCD = true;
                }
                
                string part_path_file = string.Format("{0}{1}_comment.html", Config.Part_Comment_Path, lu_sku); //Server.MapPath(string.Format("~/Part_Comment/{0}_comment.html", lu_sku)).ToLower();
    
                //test
                if (Config.isLocalhost)
                    part_path_file = string.Format(@"E:\Projects\LUcomputer\Src\part_comment\{0}_comment.html", lu_sku);

                if (System.IO.File.Exists(part_path_file)
                    && dr["is_belong_price"].ToString() == "1")
                {
                    string single_desc = ReplaceHref.ReplaceHrefText(FileHelper.ReadFile(part_path_file));
                    desc = single_desc;//.Replace("<a ", "<luComputer ").Replace("</a>", "</luComputer>").Replace("<A ", "<luComputer ").Replace("</A>", "</luComputer>").Replace(" onclick", " lucomputer").Replace("window.open", "lucomputer");

                }

                int desc_length = desc.Trim().Length;
                string s = "";
                // if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) != -1)
                decimal product_current_price;
                decimal.TryParse(dr["product_current_price"].ToString(), out product_current_price);
                if (desc_length > 5)
                {
                    string descT = FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_desc.txt"));

                    s = string.Format(descT
                        , string.Format("{0}<a name=\"{1}\"></a>", ReplacePartName(dr["product_name"].ToString()), dr["product_serial_no"].ToString())
                        , Config.http_domain + gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString()))
                        , desc
    , gfvf.GetPartMinImgForEbayG(int.Parse(dr["img_sku"].ToString())));

                }

                //
                //
                part_logo_img_name += GetPartLogoIMG(gfvf.GetPartMinImgForEbayT(int.Parse(dr["img_sku"].ToString())));

                string part_name = "";
                if (dr["is_belong_price"].ToString() == "1")
                {
                    part_name = ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()));
                }
                else
                {
                    part_name = "Not included";
                }
                if (i % 2 == (system_part_list.Rows.Count % 2))
                {
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString(), desc_length > 5 ? IMG_AR1 : "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , part_name
                        , dr["product_serial_no"].ToString(), desc_length > 5 ? IMG_AR1 : "");
                }
                else
                {
                    top_str += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , desc_length > 5 ? IMG_AR1 : "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_desc_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , part_name
                        , dr["product_serial_no"].ToString()
                        , desc_length > 5 ? IMG_AR1 : "");
                }
                //top_str += string.Format(templete_top, dr["part_group_name"].ToString(), dr["product_name"].ToString());


                // this.btn_save_page.Text += dr["img_sku"].ToString();
                if (sb.ToString().IndexOf(string.Format("[{0}]", lu_sku)) == -1)
                    str += s;


                //if (System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_t.jpg", dr["img_sku"].ToString()))) && dr["img_sku"].ToString() != "999999")
                {

                    al.Add(string.Format("{0}|{1}", dr["img_sku"].ToString(), dr["part_group_name"].ToString()));
                    //  sb_sku_list.Append("|" + dr["img_sku"].ToString() + "," + dr["part_group_name"].ToString());
                }

                sb.Append(string.Format("[{0}]", lu_sku));
            }
            #endregion

            // if part isn't exist Memory, Hard Drive, Optical Drive, LCD, OSand add Part Comment.

            #region part list 2

            // part list 2

            for (int i = 0; i < system_part_list2.Rows.Count; i++)
            {
                DataRow dr = system_part_list2.Rows[i];
                if (i % 2 == (system_part_list2.Rows.Count % 2))
                {
                    part_list_upgrades += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_tr1.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , dr["product_serial_no"].ToString() != "-1" ? IMG_AR1 : "");
                }
                else
                {
                    part_list_upgrades += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , "");

                    curr_sys_part_list += string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_tr2.txt"))
                        , dr["part_group_name"].ToString()
                        , ReplacePartNameKeyword(ReplacePartName(dr["product_name"].ToString()))
                        , dr["product_serial_no"].ToString()
                        , dr["product_serial_no"].ToString() != "-1" ? IMG_AR1 : "");
                }
            }
            #endregion
            //
            // current system part list.
            //templete = templete.Replace("[lu_web_ebay_system_part_list_1]"
            //    , string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list10_main_sub.txt")), system_sku, curr_sys_part_list));

            //
            //  lucomputers SKU Number: {0}
            // 
            //top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list_top.txt"))
            //    , system_sku, top_str);
            top_str = string.Format(FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_list11_main_sub.txt"))
               , system_sku
               , top_str
               , ESM.selected_ebay_sell
               , GetEbayHref(ESM.parent_ebay_itemid)
               , ESM.parentID
               , ParentESM.selected_ebay_sell
               , part_list_upgrades
               , ESM.selected_ebay_sell + ParentESM.selected_ebay_sell);

            //
            // generate flash view file.
            // 
            gfvf.al_sku = al;
            if (gfvf.Export())
            { }

            if (!is_exist_OS)
            {
                //System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath("/soft_img/params/no_win_OS.txt"));
                //templete = templete.Replace("[lu_web_no_window_OS_info]", sr.ReadToEnd());
                //sr.Close();
                //sr.Dispose();

                templete = templete.Replace("[lu_web_no_window_OS_info]", "");
            }
            else
                templete = templete.Replace("[lu_web_no_window_OS_info]", "");

            templete = ReplaceSystemTitle(templete.Replace("[lu_web_info_row]", str).Replace("[lu_web_top_row]", top_str).Replace("[lu_web_content_part_logo]", part_logo_img_name), system_sku);

            if (templete.IndexOf(lu_web_top_row_2) != -1 && part_list_upgrades.Length > 100)
            {
                templete = templete.Replace(lu_web_top_row_2, "");
            }
            else
                templete = templete.Replace(lu_web_top_row_2, "");

            templete = ReplaceSystemBIGImg(templete
                , system_sku
                , !is_ebay_create
                , ESM.parent_ebay_itemid);
            templete = ReplaceSystemMainComment(templete, system_sku);

            int partCount = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + system_sku.ToString() + "' and ebayshowit=1");
            if (EbaySettings.ebayFlashComboboxQuantitys.IndexOf("[" + partCount.ToString() + "]") != -1)
                templete = ReplaceFlashBindSystem(templete
                    , system_sku
                    , partCount
                    , is_ebay_create ? false : is_exist_flash
                    , is_shrink);

            //
            // ebay itemid
            if (templete.IndexOf("[ebay_itemid]") > -1)
            {
                templete = templete.Replace("[ebay_itemid]", GetEbayHref(ESM.parent_ebay_itemid));
            }


            if (templete.IndexOf("[system_all_total]") > -1)
            {
                templete = templete.Replace("[system_all_total]", ESM.ebay_system_price.ToString());
            }

            if (templete.IndexOf("[web_page_right]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();

                sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string += sr.ReadToEnd();
                sr.Close();

                templete = templete.Replace("[web_page_right]", page_right_string);
            }

            if (templete.IndexOf("[web_page_right_1]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_1.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_1]", page_right_string);
            }
            if (templete.IndexOf("[web_page_right_2]") > -1)
            {
                string page_right_string = "";
                System.IO.StreamReader sr = new StreamReader(Server.MapPath("~/soft_img/params/ebay_tpl_page_right_sub_2.txt"));
                page_right_string = sr.ReadToEnd();
                sr.Close();
                templete = templete.Replace("[web_page_right_2]", page_right_string);
            }

            return ReplaceSystemSumm(templete, categoryID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// 如果是None selected产品，列表显示Not included
    /// </summary>
    /// <param name="part_name"></param>
    /// <returns></returns>
    public string ReplacePartName(string part_name)
    {
        if (part_name.ToLower().IndexOf("none selected") != -1)
            return "Not included";
        return part_name;
    }

    /// <summary>
    /// Obtain flash system configuration function.
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <param name="partCount"></param>
    /// <param name="is_exist_flash"></param>
    /// <param name="is_shrink"></param>
    /// <returns></returns>
    private string ReplaceFlashBindSystem(string templete
        , int system_sku
        , int partCount
        , bool is_exist_flash
        , bool is_shrink)
    {
        EbayItem ei = new EbayItem();
        string keyword = "[lu_web_ebay_sys_flash_view]";
        if (templete.IndexOf(keyword) != -1)
        {
            if (is_exist_flash)
            {
                if (is_shrink)
                {
                    templete = templete.Replace(keyword
                       , ei.GetFlashString(system_sku.ToString()
                           , FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_flash_bind_sys_new.txt"))
                           , partCount
                           , is_shrink));
                }
                else
                {
                    templete = templete.Replace(keyword
                        , ei.GetFlashString(system_sku.ToString()
                            , FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_sys_flash_bind_sys.txt"))
                            , partCount
                            , is_shrink));
                }
            }
            else
            {
                templete = templete.Replace(keyword
                   , "");
            }
        }

        return templete;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    private string ReplaceSystemBIGImg(string templete, int system_sku
        , bool BigImg
        , string ebayParentItemid)
    {
        string large_picture_name = "";
        string string_big_img = "[lu_web_ebay_system_big_img]";
        if (templete.IndexOf(string_big_img) != -1)
        {
            if (BigImg)
            {

                DataTable dt = Config.ExecuteDataTable("Select large_pic_name from tb_ebay_system where id='" + system_sku.ToString() + "'");
                if (dt.Rows.Count == 1)
                    large_picture_name = dt.Rows[0][0].ToString() ?? "";
                if (large_picture_name != "")
                    return templete.Replace(string_big_img, "<img border='0' src='https://www.lucomputers.com/ebay/" + large_picture_name + ".jpg' style='border: 0px solid #666666' width='758' height='563'/>");
                else
                    return templete.Replace(string_big_img, "<img border='0' src='https://www.lucomputers.com/ebay/" + system_sku.ToString() + ".jpg' style='border: 0px solid #666666' width='758' height='563'/>");
            }
            else
            {
                return templete.Replace(string_big_img, string.Format(@"<div style='padding:10px 10px 10px 37px; color:#A53411;'>
<font color=""#006699"" face=""Verdana"" size=""2"">
<b>This computer is a user configured system from eBay item <a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={0}' target='_blank'><font color=""#f25413"" face=""Verdana""><b>{0}</b></font></a>. If you like to have your own specification please go to Item <a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={0}' target='_blank'><font color=""#f25413"" face=""Verdana""><b>{0}</b></font></a> and use the configurator on the page.</b>
</font></div>", ebayParentItemid));
            }
        }

        return templete;
    }

    /// <summary>
    /// 替换标题
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    private string ReplaceSystemTitle(string templete, int system_sku)
    {
        string system_title1 = "[lu_web_system_titel1]";
        string system_title2 = "[lu_web_system_titel2]";
        string system_title3 = "[lu_web_system_titel3]";

        DataTable dt = Config.ExecuteDataTable("Select System_title1, system_title2, system_title3 from tb_ebay_system where id='" + system_sku.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            if (templete.IndexOf(system_title1) != -1)
            {
                templete = templete.Replace(system_title1, dt.Rows[0]["system_title1"].ToString());
            }
            if (templete.IndexOf(system_title2) != -1)
                templete = templete.Replace(system_title2, dt.Rows[0]["system_title2"].ToString());

            if (templete.IndexOf(system_title3) != -1)
                templete = templete.Replace(system_title3, dt.Rows[0]["system_title3"].ToString());
        }
        return templete;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="img_Sku_name"></param>
    /// <returns></returns>
    private string GetPartLogoIMG(string img_Sku_name)
    {
        return string.Format(@"<img alt="""" src=""{0}"" width=""80"">", img_Sku_name);
    }

    private string ReplacePartNameKeyword(string part_name)
    {
        DataTable dt = KeywordDT;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string keyword = dt.Rows[i]["keyword"].ToString();
            part_name = part_name.Replace(keyword, string.Format("<span style='font-weight:bold; color:#333333;'>{0}</span>", keyword));
        }
        return part_name;
    }

    /// <summary>
    /// 系统 主描述，改为与CPU关联（2014-08-25）
    /// </summary>
    /// <param name="templete"></param>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    private string ReplaceSystemMainComment(string templete, int system_sku)
    {
        string sub_temp = FileHelper.ReadFile(Server.MapPath("~/soft_img/params/ebay_tpl_main.txt"));

        if (templete.IndexOf(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT) != -1)
        {
            string comm_ids = GetMainCommentIds(system_sku);

            string comm = "";
            if (comm_ids.Length > 0)
            {
                if (comm_ids.IndexOf('|') != -1)
                {
                    string[] ids = comm_ids.Split(new char[] { '|' });
                    foreach (string id in ids)
                    {
                        DataTable dt = Config.ExecuteDataTable("select comment from tb_ebay_system_main_comment where id='" + id.ToString() + "' order by sub_id asc");
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            comm += dt.Rows[i][0].ToString() + "<br/><br/>";
                        }
                    }
                }
                else
                {
                    int id;
                    int.TryParse(comm_ids, out id);
                    DataTable dt = Config.ExecuteDataTable("select comment from tb_ebay_system_main_comment where id='" + id.ToString() + "'");
                    if (dt.Rows.Count == 1)
                    {
                        comm = dt.Rows[0][0].ToString() + "<br/><br/>";
                    }
                }

                sub_temp = sub_temp.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, comm);
                templete = templete.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, sub_temp);
            }
            else
            {
                templete = templete.Replace(LU_WEB_EBAY_SYSTEM_MAIN_COMMENT, "");
            }
        }

        return templete;
    }

    private string ReplaceSystemSumm(string templete, int categoryID)
    {
        string system_summ_1 = "[lu_web_summ_1]";
        string system_summ_2 = "[lu_web_summ_2]";

        DataTable dt = Config.ExecuteDataTable("Select templete_summ_1, templete_summ_2 from tb_ebay_templete where id='" + categoryID.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][0].ToString().Length > 10)
                templete = templete.Replace(system_summ_1, dt.Rows[0][0].ToString());

            if (dt.Rows[0][1].ToString().Length > 10)
                templete = templete.Replace(system_summ_2, dt.Rows[0][0].ToString());

        }
        return templete;
    }

    /// <summary>
    /// get item on eBay site URL.
    /// </summary>
    /// <param name="itemid"></param>
    /// <returns></returns>
    public string GetEbayHref(string itemid)
    {
        return string.Format("<a href='https://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&item={0}' target='_blank'>{0}</a>", itemid);
    }

    #region methods
    /// <summary>
    ///  CPU 关联Main优先（2014-08-25）
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    private string GetMainCommentIds(int system_sku)
    {
        string ids = string.Empty;

        DataTable cdt = Config.ExecuteDataTable(string.Format(@"select pcac.commentid from tb_part_cpu_and_comment pcac inner join  tb_ebay_system_parts esp on esp.luc_sku=pcac.luc_sku
 where esp.system_sku='" + system_sku + "'"));
        if (cdt.Rows.Count > 0)
        {
            foreach (DataRow dr in cdt.Rows)
            {
                if (ids == string.Empty)
                    ids = dr["commentid"].ToString();
                else if (ids.Length > 0)
                    ids += "|" + dr["commentid"].ToString();
            }
        }
        else
        {
            DataTable dt = Config.ExecuteDataTable("Select main_comment_ids from tb_ebay_system where id='" + system_sku.ToString() + "'");
            if (dt.Rows.Count == 1)
                ids = dt.Rows[0][0].ToString();
        }
        return ids;
    }
    #endregion
}

public enum eBayProdType
{
    system,
    part
}