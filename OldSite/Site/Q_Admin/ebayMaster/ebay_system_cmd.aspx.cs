using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using LU.Data;

public partial class Q_Admin_ebayMaster_ebay_system_cmd : PageBaseNoInit
{
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqCmd2 != "qiozi@msn.com_wu.th@qq.com")
            {
                InitialDatabase();
            }
            Response.Clear();
            switch (ReqCmd)
            {
                case "GetSysEbayPriceBySysCost":
                    GetSysEbayPriceBySysCost();
                    break;

                case "GetEbaySysPrices":
                    GetEbaySystemPrices(ReqSystemSKU);
                    break;

                case "GetEBaySysWarn":
                    GetEbaySystemWarn(ReqSystemSKU);
                    break;

                case "ModifyPriceToWeb":
                    ModifyPriceToWeb();
                    break;

                case "GetEBayStoreCategoryString":
                    GetEBayStoreCategoryString();
                    break;

                case "SysReplacePart":
                    SysReplacePart();
                    break;

                case "SysFindPart":
                    SysFindPart();
                    break;

                case "FindETCSys":
                    findETCSystem(ReqKeyword);
                    break;

                case "getEbaySysDetail":
                    WriteETCSystemDetail(ReqSKU);
                    break;

                case "matchLUAndETCNotebook":
                    SaveMatchLUAndEtcNotebook(ReqSKU, ReqEtcItemId);
                    break;

                case "GenerateSysWarnInfo":
                    GenerateSysWarnInfo();
                    break;

                case "generateSysWarnInfoByDesc":
                    GenerateSysWarnInfoByDesc(ReqSKU);
                    break;

                //case "duplicateSys":
                //    DuplicateSystem();
                //    break;
            }

            Response.End();
        }
    }

    ///// <summary>
    ///// 复制系统
    ///// </summary>
    ///// <param name="oldSystem"></param>
    ///// <param name="memoryArray"></param>
    ///// <param name="vcArray"></param>
    ///// <param name="ssdArray"></param>
    ///// <param name="hddArray"></param>
    //void DuplicateSystem(int oldSystem, string memoryArray, string vcArray, string ssdArray, string hddArray)
    //{
    //    if (oldSystem < 1)
    //    {
    //        return;
    //    }
    //}

    /// <summary>
    /// 判断系统里的描述是否有http
    /// </summary>
    void GenerateSysWarnInfoByDesc(int sku)
    {
        if (sku > 0)
        {
            string desc = string.Empty;

            // system
            if (sku.ToString().Length > 5)
            {
                var esm = EbaySystemModel.GetEbaySystemModel(DBContext, sku);
                desc = new EbayPageText()
                               .GetEbayPageHtml(DBContext, sku
                                                , eBayProdType.system
                                                , false
                                                , eBaySystemWorker.GetFlashType(esm)).ToLower();
            }
            // part
            else
            {
                desc = new eBayPageHelper(DBContext, this).GetPageString(sku);
            }

            if (ReplaceHref.HaveWarnKeyword(desc))
            {
                Response.Write("Warn");
            }
            else
                Response.Write("OK");
        }
    }

    void GenerateSysWarnInfo()
    {
        Config.ExecuteNonQuery("delete from tb_ebay_system_warned");
        var dt = Config.ExecuteDataTable("select sys_sku from tb_ebay_selling where sys_sku >0");
        foreach (DataRow dr in dt.Rows)
        {
            var sysSku = int.Parse(dr[0].ToString());
            var result = eBaySystemWorker.WarnPartInvalid(sysSku);
            if (result)
                Config.ExecuteNonQuery("insert into tb_ebay_system_warned(SystemSku) values ('" + sysSku + "')");
        }
        Response.Write("OK");
    }

    void SaveMatchLUAndEtcNotebook(int luc_sku, string etc_itemid)
    {
        DataTable dt = Config.ExecuteDataTable("Select * from tb_ebay_etc_items where LUC_eBay_Sys_Sku='" + luc_sku + "'");
        if (dt.Rows.Count > 0)
        {
            Config.ExecuteNonQuery("Update tb_ebay_etc_items set LUC_eBay_Sys_Sku='0' where id='" + dt.Rows[0]["id"].ToString() + "'");

        }
        Config.ExecuteNonQuery("update tb_ebay_etc_items set LUC_eBay_Sys_Sku='" + luc_sku + "' where ItemID='" + etc_itemid + "'");
    }

    void WriteETCSystemDetail(int lu_sku)
    {
        //EbayEtcItemsModel[] model = EbayEtcItemsModel.FindAllByProperty("LUC_eBay_Sys_Sku", lu_sku);
        //if (model.Length > 0)
        //{
        //    string itemid = model[0].ItemID;
        //    string fullname = Server.MapPath(Config.etcSysListPath + itemid + "_part.txt");
        //    if (System.IO.File.Exists(fullname))
        //        Response.Write(FileHelper.ReadFile(fullname));
        //    else
        //        Response.Write(" no match system info." + System.IO.File.Exists(fullname).ToString());

        //}
    }

    void findETCSystem(string keyword)
    {
        //NHibernate.Expression.LikeExpression le = new NHibernate.Expression.LikeExpression("ItemTitle", keyword, NHibernate.Expression.MatchMode.Anywhere);
        //EbayEtcItemsModel[] list = EbayEtcItemsModel.FindAll(le);

        var list = DBContext.tb_ebay_etc_items.Where(me => me.ItemTitle.Contains(keyword)).ToList();
        Response.Write(JsonFx.Json.JsonWriter.Serialize(list));
    }

    public void SysFindPart()
    {
        int count = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + ReqSystemSKU + "' and luc_sku='" + ReqOldPartSku + "'");
        if (count > 0)
            Response.Write("1");
        else
            Response.Write("0");
    }

    public void SysReplacePart()
    {
        int count = Config.ExecuteScalarInt32("select count(id) from tb_ebay_system_parts where system_sku='" + ReqSystemSKU + "' and luc_sku='" + ReqOldPartSku + "'");
        if (count > 0)
        {
            bool b = false;

            DataTable dt = Config.ExecuteDataTable("Select id from tb_ebay_system_parts where system_sku='" + ReqSystemSKU + "' and luc_sku='" + ReqOldPartSku + "'");
            foreach (DataRow dr in dt.Rows)
            {
                int id = int.Parse(dr["id"].ToString());

                var pm = ProductModel.GetProductModel(DBContext, ReqNewPartSku);
                if (pm != null)
                {
                    Config.ExecuteNonQuery("update tb_ebay_system_parts set luc_sku='" + ReqNewPartSku + "', price='" + pm.product_current_price + "' ,cost='" + pm.product_current_cost + "' where id='" + id.ToString() + "'");
                    b = true;
                }
                else
                {
                    b = false;// Response.Write("4");
                }
            }
            if (b)
                Response.Write("1");
            else
                Response.Write("4");
        }
        else
            Response.Write("0");
    }

    void GetEBayStoreCategoryString()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        List<eBayCategory> scList = new GetEbayCategoryIDs().GetStoreCategory(Server.MapPath("~/soft_img/eBayXml/GetStoreResponse.xml"));
        sb.Append(string.Format(@"<option value='{0}'>{1}</option>", 0, "Please selected"));
        foreach (var m in scList)
        {
            if (ReqStoreCategoryID == m.Id)
                sb.Append(string.Format(@"<option value='{0}' selected='true'>{1}</option>", m.Id, m.Name));
            else
                sb.Append(string.Format(@"<option value='{0}'>{1}</option>", m.Id, m.Name));
        }
        Response.Write(sb.ToString());
    }

    void ModifyPriceToWeb()
    {
        if (ReqSKU > 0 && ReqSell > 0M)
        {
            decimal cost = 0M;
            cost = decimal.Parse((ReqSell / 1.022M).ToString("###.#"));
            Config.ExecuteNonQuery(string.Format(@"
delete from tb_on_sale where product_serial_no='{0}';
update tb_product set product_current_discount = 0, product_current_price = '{1}'
, product_current_cost='{2}', is_modify=1, last_regdate=now() 
where product_serial_no = '{0}';
"
                , ReqSKU
                , ReqSell
                , cost
                ));
            Response.Write("<script> this.close();</script>");
            Response.End();
        }
        else
            Response.Write("params is error.");
    }

    void GetEbaySystemPrices(int system_sku)
    {

        if (ReqIs_shrink)
        {
            #region include shipping
            decimal all_web_price = 0M;
            decimal all_cost = 0M;
            decimal all_shipping_fee = 0M;

            decimal selected_profits = 0M;
            decimal selected_ebay_fee = 0M;
            decimal selected_web_price = 0M;
            decimal selected_cost = 0M;
            GetEbayPrice.GetEbaySysCost(system_sku
                , ref selected_cost
                , ref selected_web_price
                , ref all_cost
                , ref all_web_price);

            //Response.Write(all_cost.ToString());

            var ESM = new tb_ebay_system();// EbaySystemModel();
            decimal adjustment = GetEbayPrice.GetEbaySysAdjustment(DBContext, system_sku, ref ESM);
            decimal all_profits = 0M;
            decimal all_ebay_fee = 0M;
            decimal all_ebay_price = GetEbayPrice.GetEbaySysPrice(all_cost
                , adjustment
                , ref all_profits
                , ref all_ebay_fee
                , ref all_shipping_fee
                , true);


            decimal selected_ebay_price_real = 0M;
            decimal no_selected_ebay_price_real = 0M;
            decimal all_ebay_price_real = 0M;
            decimal flash_price = GetEbayPrice.GetSysPrice(DBContext, system_sku
                , ref selected_ebay_price_real
                , ref no_selected_ebay_price_real
                , ref all_ebay_price_real);



            decimal selected_shipping_fee = 0M;
            decimal selected_ebay_price = GetEbayPrice.GetEbaySysPrice(selected_cost
                , 0M
                , ref selected_profits
                , ref selected_ebay_fee
                , ref selected_shipping_fee
                , true);


            decimal no_selected_profits = 0M;
            decimal no_selected_ebay_fee = 0M;
            decimal no_selected_cost = all_cost - selected_cost;
            decimal no_selected_shipping_fee = 0M;
            decimal no_selected_ebay_price = GetEbayPrice.GetEbaySysPrice(no_selected_cost
                , 0M
                , ref no_selected_profits
                , ref no_selected_ebay_fee
                , ref no_selected_shipping_fee
                , true);



            Response.Write(string.Format(@"[{{
            
            'all_cost':'{0}'
            ,'all_web_price':'{1}'
            ,'adjustment':'{2}'
            ,'all_ebay_price':'{3}'
            ,'all_profits':'{4}'
            ,'all_ebay_fee':'{5}'
            ,'all_shipping_fee':'{6}'
            ,'all_ebay_price_real':'{7}'

            ,'selected_cost':'{8}'
            ,'selected_ebay_price':'{9}'
            ,'selected_profits':'{10}'
            ,'selected_ebay_fee':'{11}'
            ,'selected_shipping_fee':'{12}'
            ,'selected_ebay_price_real':'{13}'

            ,'no_selected_cost':'{14}'
            ,'no_selected_ebay_price':'{15}'
            ,'no_selected_profits':'{16}'
            ,'no_selected_ebay_fee':'{17}'
            ,'no_selected_shipping_fee':'{18}'
            ,'no_selected_ebay_price_real':'{19}'

            ,'flash_price':'{20}'
}}]"
                , ConvertPrice.RoundPrice(all_cost)
                , ConvertPrice.RoundPrice(all_web_price)
                , ConvertPrice.RoundPrice(adjustment)
                , ConvertPrice.RoundPrice2(all_ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice
                , ConvertPrice.RoundPrice(all_profits)
                , ConvertPrice.RoundPrice(all_ebay_fee)
                , ConvertPrice.RoundPrice(all_shipping_fee)
                , ConvertPrice.RoundPrice(all_ebay_price_real)

                , ConvertPrice.RoundPrice(selected_cost)
                , ConvertPrice.RoundPrice2(selected_ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice
                , ConvertPrice.RoundPrice(selected_profits)
                , ConvertPrice.RoundPrice(selected_ebay_fee)
                , ConvertPrice.RoundPrice(selected_shipping_fee)
                , ConvertPrice.RoundPrice(selected_ebay_price_real)

                , ConvertPrice.RoundPrice(no_selected_cost)
                , ConvertPrice.RoundPrice2(no_selected_ebay_price) - 0.01M
                , ConvertPrice.RoundPrice(no_selected_profits)
                , ConvertPrice.RoundPrice(no_selected_ebay_fee)
                , ConvertPrice.RoundPrice(no_selected_shipping_fee)
                , ConvertPrice.RoundPrice(no_selected_ebay_price_real)

                , ConvertPrice.RoundPrice(flash_price)
                ));
            Response.End();
            #endregion
        }
        else
        {
            #region no include shipping
            decimal all_web_price = 0M;
            decimal all_cost = 0M;
            //Response.Write(all_cost.ToString());

            decimal selected_cost = 0M;
            decimal selected_web_price = 0M;
            GetEbayPrice.GetEbaySysCost(system_sku
            , ref selected_cost
            , ref selected_web_price
            , ref all_cost
            , ref all_web_price);

            var ESM = new tb_ebay_system();// EbaySystemModel();
            decimal adjustment = GetEbayPrice.GetEbaySysAdjustment(DBContext, system_sku, ref ESM);
            decimal all_profits = 0M;
            decimal all_ebay_fee = 0M;
            decimal shipping_Fee = 0M;
            decimal all_ebay_price = GetEbayPrice.GetEbaySysPrice(all_cost
                , adjustment
                , ref all_profits
                , ref all_ebay_fee
                , ref shipping_Fee
                , true);




            decimal all_shipping_fee = shipping_Fee;
            decimal selected_ebay_price_real = 0M;
            decimal no_selected_ebay_price_real = 0M;
            decimal all_ebay_price_real = 0M;
            decimal flash_price = GetEbayPrice.GetSysPrice(DBContext, system_sku
                , ref selected_ebay_price_real
                , ref no_selected_ebay_price_real
                , ref all_ebay_price_real);



            Response.Write(string.Format(@"[{{
            
            'all_cost':'{0}'
            ,'all_web_price':'{1}'
            ,'adjustment':'{2}'
            ,'all_ebay_price':'{3}'
            ,'all_profits':'{4}'
            ,'all_ebay_fee':'{5}'
            ,'all_shipping_fee':'{6}'
            ,'all_ebay_price_real':'{7}'

            ,'selected_cost':'{8}'
            ,'selected_ebay_price':'{9}'
            ,'selected_profits':'{10}'
            ,'selected_ebay_fee':'{11}'
            ,'selected_shipping_fee':'{12}'
            ,'selected_ebay_price_real':'{13}'

            ,'no_selected_cost':'{14}'
            ,'no_selected_ebay_price':'{15}'
            ,'no_selected_profits':'{16}'
            ,'no_selected_ebay_fee':'{17}'
            ,'no_selected_shipping_fee':'{18}'
            ,'no_selected_ebay_price_real':'{19}'
            
            ,'flash_price':'{20}'
}}]"
               , ConvertPrice.RoundPrice(all_cost)
               , ConvertPrice.RoundPrice(all_web_price)
               , ConvertPrice.RoundPrice(adjustment)
               , (ConvertPrice.RoundPrice2(all_ebay_price) - 0.01M) + EbaySettings.ebayAccessoriesPrice
               , ConvertPrice.RoundPrice(all_profits)
               , ConvertPrice.RoundPrice(all_ebay_fee)
               , ConvertPrice.RoundPrice(all_shipping_fee)
               , ConvertPrice.RoundPrice(all_ebay_price_real)

               , "NA"
               , "NA"
               , "NA"
               , "NA"
               , "NA"
               , "NA"

               , "NA"
               , "NA"
               , "NA"
               , "NA"
               , "NA"
               , "NA"

               , ConvertPrice.RoundPrice(flash_price)
               ));
            Response.End();
            #endregion
        }

    }

    void GetEbaySystemWarn(int system_sku)
    {
        bool Warn = eBaySystemWorker.WarnPartInvalid(ReqSystemSKU);

        Response.Write(string.Format(@"[{{            
            'warn':'{0}'
}}]"
            , Warn));
    }
    /// <summary>
    /// 
    /// </summary>
    private void GetSysEbayPriceBySysCost()
    {
        decimal profits = 0M;
        decimal ebay_fee = 0M;
        decimal shipping_fee = 0M;
        decimal ebay_price = 0M;

        decimal sys_cost = decimal.Parse(Config.ExecuteScalar(@"select sum(product_current_cost*ses.part_quantity) cost from tb_product p 
    inner join  tb_ebay_system_parts ses 
    on ses.luc_sku=p.product_serial_no 
    where p.tag=1 and p.split_line=0 and system_sku='" + ReqSystemSKU.ToString() + "'").ToString());

        if (ReqIs_shrink)
        {
            ebay_price = GetEbayPrice.GetEbaySysPrice(sys_cost, ReqAdjustment
                , ref profits
                , ref ebay_fee
                , ref shipping_fee
                , true);
        }
        else
        {
            ebay_price = GetEbayPrice.GetEbaySysPrice(sys_cost, ReqAdjustment
                , ref profits
                , ref ebay_fee
                , ref shipping_fee
                , true);
        }
        bool Warn = eBaySystemWorker.WarnPartInvalid(ReqSystemSKU);

        Response.Write(string.Format(@"[{{            
            'ebayPrice':'{0}'
            ,'shipping_fee':'{1}'
            ,'profit':'{2}'
            ,'ebay_fee':'{3}'
            ,'warn':'{4}'
            ,'cost':'{5}'
}}]"
            , ConvertPrice.RoundPrice2(ebay_price) - 0.01M + EbaySettings.ebayAccessoriesPrice
            , ConvertPrice.RoundPrice(shipping_fee)
            , ConvertPrice.RoundPrice(profits)
            , ConvertPrice.RoundPrice(ebay_fee)
            , Warn
            , sys_cost));
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    public string ReqCmd2
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd2"); }
    }
    public int ReqSystemSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "systemsku", -1); }
    }

    public int ReqOldPartSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "oldPartSku", -1); }
    }

    public int ReqNewPartSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "newPartSku", -1); }
    }

    int SystemSKU { get; set; }

    public decimal ReqCost
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Cost", -1); }
    }

    public decimal ReqSell
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "sell", -1); }
    }

    public int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "luc_sku", -1); }
    }

    /// <summary>
    /// Etc Item Id
    /// </summary>
    public string ReqEtcItemId
    {
        get { return Util.GetStringSafeFromQueryString(Page, "etcitemid"); }
    }

    public decimal ReqAdjustment
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Adjustment", -1); }
    }

    public bool ReqPriceTypeIncludeShipping
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "includeShipping", -1) == 1; }
    }

    public bool ReqIs_shrink
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "is_shrink", -1) == 1; }
    }

    string ReqStoreCategoryID
    {
        get { return Util.GetStringSafeFromQueryString(Page, "selectedID"); }
    }

    string ReqKeyword
    {
        get { return Util.GetStringSafeFromQueryString(Page, "keyword"); }
    }

    //List<int> ConvertToIntList(string reqStr)
    //{
    //    if (string.IsNullOrEmpty(reqStr))
    //        return new List<int>();

    //    var cates = reqStr.Split(new char[] { ',' });
    //    var result = new List<int>();
    //    foreach (var item in cates)
    //    {
    //        var cate = 0;
    //        int.TryParse(item, out cate);
    //        if (cate > 0)
    //            result.Add(cate);
    //    }
    //    return result;
    //}
    //List<int> ReqCates
    //{
    //    get
    //    {
    //        return ConvertToIntList(Util.GetStringSafeFromQueryString(Page, "caseArray"));
    //    }
    //}

    //List<int> ReqMemorys
    //{
    //    get
    //    {
    //        return ConvertToIntList(Util.GetStringSafeFromQueryString(Page, "memeoryArray"));
    //    }
    //}

    //List<int> ReqVideoCards
    //{
    //    get
    //    {
    //        return ConvertToIntList(Util.GetStringSafeFromQueryString(Page, "vcArray"));
    //    }
    //}

    //List<int> ReqSSD
    //{
    //    get
    //    {
    //        return ConvertToIntList(Util.GetStringSafeFromQueryString(Page, "ssdArray"));
    //    }
    //}

    //List<int> ReqHDD
    //{
    //    get
    //    {
    //        return ConvertToIntList(Util.GetStringSafeFromQueryString(Page, "hddArray"));
    //    }
    //}
}
