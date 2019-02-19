﻿using System;
using System.Data;

public partial class Q_Admin_ebayMaster_Online_AddItemFromFlash : System.Web.UI.Page
{
    const int ParentChildSystem = 3;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           //Response.Write("Params is null");
           //Response.End();
           //System.IO.StreamWriter sw = new System.IO.StreamWriter(Server.MapPath("~/txt.txt"));
           //sw.Write(Request.ServerVariables["HTTP_REFERER"]);
           //sw.Close();
            
            string content = string.Empty;
            int Version = 0;
            string parentItemid = "";
            FlashType flash_type = FlashType.old;

            try
            {
                content = Request["item"].ToString().ToLower();
                Version = Util.GetInt32SafeFromQueryString(Page, "version", 1);
                parentItemid = Util.GetStringSafeFromQueryString(Page, "parentItemid");
                flash_type = Util.GetInt32SafeFromQueryString(Page, "flash_type", 1) == 1 
                    ? FlashType.old : (Util.GetInt32SafeFromQueryString(Page, "flash_type", 1) ==2
                    ? FlashType.Child :FlashType.barebone );
            }
            catch { }

            //content = @"200816Qiozi%40msn%2Ecom%2417137%7C13671%7C71%2E99%7C1%7C210%2417138%7C14139%7C0%2E00%7C12%7C206%2417139%7C13972%7C53%2E99%7C11%7C225%2417140%7C7060%7C66%2E99%7C2%7C226%2417141%7C11967%7C94%2E99%7C32%7C227%2417142%7C1049%7C0%2E00%7C33%7C227%2417143%7C1049%7C0%2E00%7C34%7C227%2417144%7C6053%7C28%2E99%7C42%7C228%2417145%7C14246%7C49%2E99%7C29%7C176%2417146%7C1049%7C0%2E00%7C30%7C176%2417147%7C15780%7C0%2E00%7C13%7C230%2417148%7C6171%7C146%2E99%7C14%7C233%2417149%7C1049%7C0%2E00%7C25%7C240%2417150%7C14671%7C21%2E99%7C44%7C241%2417151%7C3525%7C33%2E99%7C9%7C231%2417152%7C13989%7C32%2E99%7C10%7C186%2417153%7C1049%7C0%2E00%7C6%7C242%2417154%7C1049%7C0%2E00%7C8%7C232%2417155%7C1049%7C0%2E00%7C7%7C237%2417156%7C1049%7C0%2E00%7C41%7C97";// Request["item"].ToString().ToLower();
            
            if (content != string.Empty)
            {
                //Response.Write(content);

                try
                {
                    if (Util.GetStringSafeFromString(Page, "cmd") == "summary")
                    {
                        //
                        // get SKU.
                        //
                        int sku = Util.GetInt32SafeFromString(Page, "sku", -1);

                        DataTable dt = Config.ExecuteDataTable(@"Select case when length(part_ebay_describe)>5 then part_ebay_describe
                            when length(product_ebay_name)>5 then product_ebay_name
                            when length(product_name_long_en)>5 then product_name_long_en
                            when length(product_name)>5 then product_name
                            else product_short_name end as part_name
                        from tb_product where product_serial_no='" + sku.ToString() + "'");
                        if (dt.Rows.Count > 0)
                            Response.Write(dt.Rows[0][0].ToString());
                        else
                            Response.Write("No Match Data.");
                        Response.End();
                    }
                    else
                    {
                        
                        Config.ExecuteNonQuery(@" insert into tb_ebay_sys_create_str 
	                        ( content, regdate, total)
	                        values
	                        ( '" + Request.Url.AbsoluteUri + "', now(), '"+ ReqTotal +"')");
                        Config.ExecuteNonQuery(@" insert into tb_ebay_sys_create_str 
	                        ( content, regdate, total)
	                        values
	                        ( '" + content + "', now(), '" + ReqTotal + "')");
                        //detailID +"|"+ sku +"|"+ price + "|" +commentID +"|"+ partGroupID
                        string[] gps = content.Split(new char[] { '$' });
                        if (gps.Length > 0)
                        {
                            bool IsChild = flash_type != FlashType.old;
                            Version = IsChild ? Version : 0;
                            SystemSKU = gps[0].Substring(0, 6);
                            if (gps[0].Substring(6).ToLower() == "qiozi@msn.com")
                            {
                                string oldItemId = "";
                                EbayItem ei = new EbayItem();
                                int newSystemSKU = 0;
                                string parent_itemid =  eBaySystemWorker.GetEbayItemID(int.Parse(SystemSKU)); // modify
                                if (parent_itemid == null)
                                    throw new Exception("ebay system create sub system error. Parent system ItemID quantity error.");
                                newSystemSKU = eBaySystemWorker.CreateSys(SystemSKU
                                    , gps
                                    , ReqTotal
                                    , IsChild
                                    , Version
                                    , parent_itemid
                                    , ref oldItemId);

                                if (newSystemSKU == 0)
                                {
                                    if (oldItemId != "")
                                    {
                                        Response.Write(oldItemId);
                                        Response.End();
                                    }
                                    throw new Exception("eBay Sys newSystemSKU == 0");
                                }
                                //EbaySystemModel esm = EbaySystemModel.GetEbaySystemModel(newSystemSKU);
                                EbayItemGenerate eig = new EbayItemGenerate();
                                string cpuShortName = "";
                                ei.Title = GetNeweBaySysTitle(newSystemSKU
                                    , IsChild
                                    , SystemSKU
                                    , parent_itemid
                                    , ref cpuShortName);
                                Config.ExecuteNonQuery("Update tb_ebay_system set ebay_system_name='" + ei.Title.Replace("'", "\\'") + "',ebay_system_price='" + ReqTotal + "', system_title1='" + ei.Title.Replace("'", "\\'") + "' where id='" + newSystemSKU + "'");
                                ei.Buy_it_now_price = ReqTotal;
                                // 如果不是子系统，不走ebay创建的模板。 2014-07-29
                                ei.Description = ebayPageTpl(newSystemSKU, IsChild, IsChild, flash_type);
                                ei.Category = "179";
                                ei.Duration = IsChild ? "" : "GTC";
                                string Category1 = "";
                                if (!Config.isLocalhost)
                                {
                                    DataTable catedt = Config.ExecuteDataTable("Select eBayMyCateId_1 from tb_ebay_category_and_product where sku='" + SystemSKU + "'");
                                    if (catedt.Rows.Count == 1 && !IsChild)
                                    {
                                        ei.Store_category = catedt.Rows[0][0].ToString();
                                    }
                                    else
                                        ei.Store_category = "1635247017";
                                }
                                if (IsChild)
                                    ei.cutom_label = string.Format("New:{0}|Old:{1}", newSystemSKU, SystemSKU);
                                else
                                    ei.cutom_label = cpuShortName + " auto " + newSystemSKU;
                                ei.Pictures_url1 = GetImgUrl(newSystemSKU, IsChild);

                                #region save new sys Price and Old Sys SKU.
                                // esm.ebay_system_price = Total;
                                //esm.source_code = int.Parse(SystemSKU);
                                //esm.Update();
                                #endregion

                                ei.item_specifics = GetItemSpecifics.GetSystemSpecifics(newSystemSKU);
                                ei.Store_category = eBaySysCategory.GetSysOnEbayCategory(ei.Title);
                                string code = eig.AddItem(ei, "", newSystemSKU, 0, true, IsChild);
                                EbayCodeAndLucSkuModel ecalsm = new EbayCodeAndLucSkuModel();
                                ecalsm.ebay_code = code;
                                ecalsm.is_sys = true;
                                ecalsm.SKU = newSystemSKU;
                                ecalsm.is_online = true;
                                ecalsm.regdate = DateTime.Now;
                                ecalsm.Create();
                                Response.Write(code);

                                // 保存目录与产品
                                Config.ExecuteNonQuery(@"insert into tb_ebay_category_and_product 
	(Sku, ProdType, eBayCateID_1, eBayCateText_1, eBayMyCateID_1, 
	eBayMyCateText_1, 
	regdate
	)
	values
	('" + newSystemSKU + "', 'S', '179', '', '" + Category1 + @"','',now());
insert into tb_ebay_selling(ItemID,BuyItNowPrice,sys_sku,TimeLeft,QuantityAvailable) values ('" + code + "','" + ReqTotal + "', '" + newSystemSKU + @"','P21DT12H44M42S','6');
");


                                //   

                            }
                            //Response.Write(gps[0].Substring(6));
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogModel elm = new ErrorLogModel();
                    elm.comment = "eBay Sys Binder Error.";
                    elm.summary = ex.Message + "\r\n----------------------------\r\n" + ex.StackTrace+ "\r\n----------------------------\r\n" +ex.Source;
                    elm.regdate = DateTime.Now;
                    elm.Create();
                    throw ex;
                    // Response.Write(ex.Message);
                }
            }
            else
                Response.Write("Params is null");
            Response.End();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public string GetNeweBaySysTitle(int system_sku
        , bool IsChildSystem
        , string oldSystemSku
        , string parent_itemid
        , ref string cpuShortName)
    {
        if (IsChildSystem)
        {
//            DataTable dt = Config.ExecuteDataTable(@"select ebay_system_short_name from tb_ebay_system_parts sp inner join tb_ebay_system_part_comment ec 
//	on sp.comment_id = ec.id and is_cpu=1 and sp.system_sku='" + system_sku.ToString() + @"'
//	inner join tb_product p on p.product_Serial_no=sp.luc_sku ");
//            if (dt.Rows.Count > 0)
//            {
//                string title = "SKU "+ system_sku +" Upgrade Parts for Item#";// dt.Rows[0][0].ToString().Trim() + " Computer Upgrade (from #";

//                if (title.Length <= 74)
//                {
//                    return title + " " + oldSystemSku.ToString();
//                }
//                else
//                {
//                    if (title.Length > 80)
//                    {
//                        return title.Substring(0, 80);
//                    }
//                    else
//                        return title;
//                }
//            }
//            else
            return "SKU# " + system_sku + " Upgrade Parts from #" + parent_itemid;
        }
        else
        {
//            DataTable dt = Config.ExecuteDataTable(@"select ebay_system_short_name from tb_ebay_system_parts sp inner join tb_ebay_system_part_comment ec 
//	on sp.comment_id = ec.id and is_cpu=1 and sp.system_sku='" + system_sku.ToString() + @"'
//	inner join tb_product p on p.product_Serial_no=sp.luc_sku ");
//            if (dt.Rows.Count > 0)
//            {
//                string title = dt.Rows[0][0].ToString().Trim() + " Desktop Computer SKU# " + system_sku + " (Customized by " + parent_itemid + ")";
//                if (title.Length > 80)
//                {
//                    return title.Substring(0, 80);
//                }
//                else
//                    return title;
//            }
//            else
//            {
//                dt = Config.ExecuteDataTable(@"
//select category_name from tb_product_category_new pc inner join tb_ebay_system e on e.category_id=pc.category_id
//where id='" + system_sku.ToString() + "'");
//                if (dt.Rows.Count > 0)
//                {
//                    return dt.Rows[0][0].ToString().Trim() + " Desktop Computer SKU# " + system_sku + " (Customized by " + parent_itemid + ")";
//                }
//                else
//                    return "LU Computer System SKU: "+ system_sku;
//            }

            return eBaySysTitle.GetSystemAutoTitle(system_sku, ref cpuShortName);
        }

    }

    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <param name="is_exist_flash"></param>
    /// <returns></returns>
    private string ebayPageTpl(int system_sku
        , bool is_ebay_create
        , bool IsParentChildSystem
        , FlashType ft)
    {
        EbayPageText ept = new EbayPageText();
        EbaySystemModel ESM = EbaySystemModel.GetEbaySystemModel(system_sku);
        return ept.GetEbayPageHtml(system_sku
            , eBayProdType.system
            , is_ebay_create
            , ft);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public string GetImgUrl(int system_sku, bool IsChild)
    {
        DataTable dt;

        if (!IsChild)
        {
            dt = Config.ExecuteDataTable(string.Format(@"
select 
case when p.other_product_sku>0 then p.other_product_sku 
    else luc_sku end as img_sku from tb_ebay_system_parts es inner join tb_product p 
on p.product_serial_no=es.luc_sku
where system_sku = '{0}' and comment_id in (select id from tb_ebay_system_part_comment where is_case=1)", system_sku));
        }
        else
        {
            dt = Config.ExecuteDataTable(string.Format(@"
select 
case when p.other_product_sku>0 then p.other_product_sku 
    else luc_sku end as img_sku from tb_ebay_system_parts es inner join tb_product p 
on p.product_serial_no=es.luc_sku
where system_sku = '{0}' and p.product_serial_no <> 16684 and instr(p.product_ebay_name,'onboard')=0  order by es.is_belong_price desc, p.product_serial_no desc limit 1", system_sku));            

        }
        if (dt.Rows.Count > 0)
        {
            if (!System.IO.File.Exists(Server.MapPath(string.Format("~/pro_img/COMPONENTS/{0}_g_1.jpg", dt.Rows[0][0].ToString()))))
            {
                EmailHelper.SendTo("wu.th@qq.com", "no pricture eBay sys " + system_sku.ToString(), "logo no exist that on eBay system " + system_sku.ToString());
            }
            else
                return string.Format("https://www.lucomputers.com/pro_img/COMPONENTS/{0}_g_1.jpg", dt.Rows[0][0].ToString());
        }
        else
        {
            EmailHelper.SendTo("wu.th@qq.com", "no pricture on " + system_sku.ToString(), "no pricture on eBay system " + system_sku.ToString());
        }
        return "";
    }

    #region properties
    string _systemSKU;
    public string SystemSKU
    {
        get { return _systemSKU; }
        set { _systemSKU = value; }
    }

    public decimal ReqTotal
    {
        get { return decimal.Parse(Util.GetDoubleSafeFromQueryString(Page, "total", 10000).ToString("0.00")); }
    }
    #endregion
}
