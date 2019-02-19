using System;
using System.Data;

/// <summary>
/// Summary description for eBaySystemWorker
/// </summary>
public class eBaySystemWorker
{
	public eBaySystemWorker()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Verification system, if there are repeated, returned to its ItemID
    /// else returned to Null.
    /// 
    /// </summary>
    /// <param name="partGroupID_sku_commentIDS"></param>
    /// <param name="new_price"></param>
    /// <returns></returns>
    public static string ValidateSystem(string[] partGroupID_sku_commentIDS, decimal new_price)
    {
        try
        {
            DataTable dt = Config.ExecuteDataTable("Select id from tb_ebay_system where ebay_system_price='" + new_price.ToString() + "' or selected_ebay_sell='" + new_price.ToString() + "' order by id desc");
            if (dt.Rows.Count > 0)
            {
                //
                // 保存需要的配置，用于比较
                #region generate datatable
                DataTable gDt = new DataTable();
                gDt.Columns.Add("Sku");
                gDt.Columns.Add("CommentID");
                for (int i = 1; i < partGroupID_sku_commentIDS.Length; i++)
                {
                    var sukComentString = partGroupID_sku_commentIDS[i];
                    if (sukComentString.IndexOf("|") == -1)
                    {
                        continue;
                    }

                    int luc_sku = int.Parse(splitString(partGroupID_sku_commentIDS[i], 1));
                    if (luc_sku > 0)
                    {
                        DataRow dr = gDt.NewRow();
                        dr["Sku"] = luc_sku;
                        dr["CommentID"] = splitString(partGroupID_sku_commentIDS[i], 3);
                        gDt.Rows.Add(dr);
                    }
                }
                #endregion
                //
                // 循环价格相同的系统
                bool allExist = true;
                foreach (DataRow dr in dt.Rows)
                {
                    int systemCode = 0;
                    int.TryParse(dr["id"].ToString(), out systemCode);
                    DataTable subDT = Config.ExecuteDataTable("select luc_sku,comment_id from tb_ebay_system_parts where system_sku='" + systemCode.ToString() + "' order by luc_sku asc");

                    if (subDT.Rows.Count != gDt.Rows.Count 
                        && subDT.Rows .Count >0)
                        return null;
                    bool exist = false;


                    foreach (DataRow subdr in subDT.Rows)
                    {
                        int subSKU;
                        int.TryParse(subdr["luc_sku"].ToString(), out subSKU);
                        exist = false;
                        foreach (DataRow gdr in gDt.Rows)
                        {
                            if (gdr["Sku"].ToString() == subSKU.ToString()
                                && gdr["CommentID"].ToString() == subdr["comment_id"].ToString())
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            allExist = false;
                            break;                            
                        }
                        else
                        {
                            continue;
                        }
                    }
                    if (!allExist)
                        return null;
                    //
                    // 有相同的系统, 取得itemID
                    DataTable oldCodeDT = Config.ExecuteDataTable("select ebay_code from tb_ebay_code_and_luc_sku where sku='" + systemCode.ToString() + "' and is_online=1 order by regdate desc limit 0,1");
                    //DataTable oldCodeDT = Config.ExecuteDataTable("Select itemid from tb_ebay_selling where sys_sku='" + systemCode.ToString() + "' order by id desc limit 0,1");
                    if (oldCodeDT.Rows.Count == 1)
                        return oldCodeDT.Rows[0][0].ToString();
                    return null;
                }
                return null;
            }
            else
                return null;
        }
        catch (Exception ex)
        {
            ErrorLogModel elmas = new ErrorLogModel();
            elmas.comment = ex.Message;
            elmas.regdate = DateTime.Now;
            elmas.comment = "ebay Generate from Ebay.ca;";
            elmas.summary = "\r\nebay Generate from Ebay.ca" + "\r\n"+ ex.Message + "\r\n----------------------------\r\n" + ex.StackTrace + "\r\n----------------------------\r\n" + ex.Source;
            elmas.Create();


            elmas.Create();
        }
        return null;
    }

    /// <summary>
    /// Create eBay System.
    /// If the system exists, it need not be established, and returned to Old ItemId.
    /// </summary>
    /// <param name="old_system_sku"></param>
    /// <param name="partGroupID_sku_commentIDS"></param>
    /// <param name="new_price"></param>
    /// <returns></returns>
    public static int CreateSys(string old_system_sku
        , string[] partGroupID_sku_commentIDS
        , decimal new_price
        , bool IsParentChildSystem
        , int flashVersion
        , string parent_ebay_itemid
        ,ref string itemid)
    {
        string oldItemID = ValidateSystem(partGroupID_sku_commentIDS, new_price);
        if (oldItemID != null)
        {
            itemid = oldItemID;
            return 0;
        }

        int new_system_sku = Config.ExecuteScalarInt32(string.Format(@"insert into tb_ebay_system 
	( category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, 
	showit, 
	view_count, 
	logo_filenames, 
	keywords, 
	system_title1, 
	system_title2, 
	system_title3, 
	main_comment_ids, 
	is_issue, 
	large_pic_name, 
	is_from_ebay
    ,cutom_label
    ,adjustment
    ,selected_ebay_sell
    ,no_selected_ebay_sell
    ,is_include_shipping
    ,is_disable_flash_customize
    ,ParentID
    ,flashVersion
    ,is_shrink
    ,source_code
    ,parent_ebay_itemid
    ,is_barebone
    ,regdate
	)
select category_id, ebay_system_name, {3}, ebay_system_current_number, 
	1, 
	view_count, 
	logo_filenames, 
	keywords, 
	system_title1, 
	system_title2, 
	system_title3, 
	main_comment_ids, 
	is_issue, 
	large_pic_name, 
	1
    ,cutom_label
    ,adjustment
    ,selected_ebay_sell
    ,no_selected_ebay_sell
    ,is_include_shipping
    ,1
    ,'{0}'
    ,'{1}'
    ,is_shrink
    ,'{0}'
    ,'{2}'
    ,is_barebone, now()
from tb_ebay_system where id='{0}'  ;

select last_insert_id();", old_system_sku
                         , flashVersion
                         , parent_ebay_itemid
                         , new_price));


        for (int i = 1; i < partGroupID_sku_commentIDS.Length; i++)
        {
            var sukComentString = partGroupID_sku_commentIDS[i];
            if (sukComentString.IndexOf("|") == -1)
            {
                continue;
            }
            int luc_sku = int.Parse(splitString(sukComentString, 1));
            

            if (luc_sku > 0)
            {
                int newVideoSKU = 0;
                bool validateVideo = IsNoVideoCardOnMotherboard(luc_sku
                    , int.Parse(splitString(partGroupID_sku_commentIDS[i], 3))
                    , partGroupID_sku_commentIDS
                    , ref newVideoSKU);

                if (validateVideo)
                    luc_sku = Config.noVideoCardSku;
                else if (newVideoSKU > 0)
                    luc_sku = newVideoSKU;

                int detailID = int.Parse(splitString(partGroupID_sku_commentIDS[i], 0));   
                DataTable dt = Config.ExecuteDataTable("Select * from tb_ebay_system_parts where id='" + detailID.ToString() + "'");
                
                if (dt.Rows.Count > 0)
                {
                    int comment_id = int.Parse(splitString(partGroupID_sku_commentIDS[i], 3));
                    int part_group_id = int.Parse(splitString(partGroupID_sku_commentIDS[i], 4));
                    bool isReplace = false;
                    int isR = 0;
                    int.TryParse(splitString(partGroupID_sku_commentIDS[i], 5), out isR);
                    isReplace = isR == 1;

                    var comment_name = "";
                    DataTable cdt = Config.ExecuteDataTable("select comment from tb_ebay_system_part_comment where id='" + splitString(partGroupID_sku_commentIDS[i], 3) + "'");
                    if (cdt.Rows.Count == 0)
                        comment_name = cdt.Rows[0][0].ToString();
                    DataRow dr = dt.Rows[0];

                    if(decimal.Parse(dr["price"].ToString() )<1M
                        && "4,27,28,29,30,13,".IndexOf(comment_id.ToString()+",") >-1
                        && isReplace)
                    {
                        luc_sku = 16684;
                    }


                    Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_system_parts 
                ( luc_sku, comment_id, price, cost, system_sku, part_quantity, max_quantity,part_group_id,comment_name, is_online, is_label_of_flash, is_belong_price, regdate) 
values 
( '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}','{7}', '{8}', '{9}','{10}', '{11}', now())"
                        , luc_sku
                        , comment_id
                        , dr["price"].ToString()
                        , dr["cost"].ToString()
                        , new_system_sku
                        , 1
                        , 1
                        , part_group_id
                        , comment_name
                        , 1
                        , dr["is_label_of_flash"].ToString() == "False" ? "0" : (dr["is_label_of_flash"].ToString() == "Ture" ? "1" : dr["is_label_of_flash"].ToString() )
                        , IsParentChildSystem ? (dr["is_belong_price"].ToString() == "1" ? "0" : "1") : dr["is_belong_price"].ToString()
                        ));
                    //decimal _price;
                    //decimal.TryParse(dr["product_current_price"].ToString(), out _price);
                    //sys_price += _price;
                    //  sys_cost += pm.product_current_cost;}

                    if (isReplace)
                    {
                        Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_system_parts_replace 
	(system_sku, luc_sku, comment_id, part_group_id)
	values
	('{0}', '{1}', '{2}', '{3}')", new_system_sku, luc_sku, comment_id, part_group_id));
                    }
                }
            }
        }


        Config.ExecuteNonQuery(string.Format(@"Update tb_ebay_system Set is_online=1 {0} {2} where id='{1}'"
            , IsParentChildSystem ? " ,selected_ebay_sell='" + new_price + "'" : ""
            , new_system_sku
            , IsParentChildSystem ? " ,is_shrink=1" : " , is_disable_flash_customize=0,is_shrink=0"

            ));

        // 保存系统所在目录(web)
        Config.ExecuteNonQuery(@"insert into tb_ebay_system_and_category 
	( eBaySysCategoryID, SystemSku)
select eBaySysCategoryID, '" + new_system_sku + "' from tb_ebay_system_and_category where SystemSku='" + old_system_sku + "'");

        return new_system_sku;
    }
    /// <summary>
    /// 不用于SLI, Cross
    /// </summary>
    /// <returns></returns>
    public static bool IsNoVideoCardOnMotherboard(int luc_sku,int comment_id, string[] partGroupID_sku_commentIDS, ref int newLucSku)
    {

        // Is Video Card.
        if (Config.ExecuteScalarInt32("select count(id) c from tb_ebay_system_part_comment where is_video=1 and id='" + comment_id.ToString() + "'") != 1)
            return false;

        int videoGroupID = EbaySettings.relation_motherboard_video_group_id;

        DataTable dt;
        string sql = @"Select p.product_serial_no
                from tb_product p inner join tb_part_group_detail pd on p.product_serial_no=pd.product_serial_no
                    where p.split_line=0 and p.tag=1 and pd.showit=1 and pd.part_group_id='{0}' order by p.product_name asc";

        dt = Config.ExecuteDataTable(string.Format(sql, videoGroupID));

        int motherboardCommentId = Config.ExecuteScalarInt32(" select id from tb_ebay_system_part_comment where is_mb=1 ");
        foreach (DataRow dr in dt.Rows)
        {
            if (luc_sku == int.Parse(dr["product_serial_no"].ToString()))
            {
                for (int j = 1; j < partGroupID_sku_commentIDS.Length; j++)
                {
                    if (j >= partGroupID_sku_commentIDS.Length) continue;

                    int commentid = int.Parse(splitString(partGroupID_sku_commentIDS[j], 3));
                    int curr_sku = int.Parse(splitString(partGroupID_sku_commentIDS[j], 1));

                    if (commentid == motherboardCommentId)
                    {

                        DataTable vdt = Config.ExecuteDataTable("Select video_sku from tb_part_relation_motherboard_video_audio_port where mb_sku='" + curr_sku.ToString() + "'");
                        if (vdt.Rows.Count > 0)
                        {
                            if (int.Parse(vdt.Rows[0][0].ToString()) == 16684)
                            {
                                return true;
                            }
                            else
                            {
                                newLucSku = int.Parse(vdt.Rows[0][0].ToString());
                                return false;
                            }
                        }
                    }
                }
            }
        }
        return false;

    }

    /// <summary>
    /// To save the current eBay system configuration to historical records.
    /// </summary>
    /// <param name="itemid"></param>
    /// <param name="systemSKU"></param>
    public static void SaveHistory(string itemid, int systemSKU,bool is_from_ebay)
    {
        if (!EbaySettings.eBaySysSavePriceHistory)
            return;
        int new_system_sku = Config.ExecuteScalarInt32(string.Format(@"insert into tb_ebay_system_history 
	( sys_sku,category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, 
	showit, 
	view_count, 
	logo_filenames, 
	keywords, 
	system_title1, 
	system_title2, 
	system_title3, 
	main_comment_ids, 
	is_issue, 
	large_pic_name, 
	is_from_ebay
    ,cutom_label
    ,adjustment
    ,selected_ebay_sell
    ,no_selected_ebay_sell
    ,is_include_shipping
    ,is_disable_flash_customize
    ,ebay_itemid
	)
select id,category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, 
	showit, 
	view_count, 
	logo_filenames, 
	keywords, 
	system_title1, 
	system_title2, 
	system_title3, 
	main_comment_ids, 
	is_issue, 
	large_pic_name, 
	{2}
    ,cutom_label
    ,adjustment
    ,selected_ebay_sell
    ,no_selected_ebay_sell
    ,is_include_shipping
    ,is_disable_flash_customize
    ,'{1}'
from tb_ebay_system where id='{0}'  ;

select last_insert_id();", systemSKU, itemid
                         , is_from_ebay));

        // components.
        //
        Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_system_parts_history 
                ( parent_id,luc_sku, comment_id, price, cost, system_sku, part_quantity, max_quantity,part_group_id,comment_name, ebay_itemid) 

select '{2}', sp.luc_sku, sp.comment_id, p.product_current_price-p.product_current_discount price, p.product_current_cost, sp.system_sku, sp.part_quantity, sp.max_quantity
,sp.part_group_id,sp.comment_name, '{1}'
 from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku where sp.system_sku='{0}'"
                      , systemSKU
                      , itemid
                      , new_system_sku
                      ));
    }

    /// <summary>
    /// Update flash top of the total price.
    /// </summary>
    /// <param name="sysSKU"></param>
    /// <param name="sell"></param>
    public static void UpdateFlashPrice(EbaySystemModel ESM
        , decimal sell
        , bool IsChildSystem
        , decimal cost
        , decimal ebayFee
        , decimal shippingFee
        , decimal profit)
    {
       // EbaySystemModel ESM = EbaySystemModel.GetEbaySystemModel(sysSKU);
        if (IsChildSystem)
            ESM.selected_ebay_sell = sell;
        else
            ESM.ebay_system_price = sell;
        if (ESM.is_barebone)
            ESM.selected_ebay_sell = sell;
        ESM.cost = cost;
        ESM.ebay_fee = ebayFee;
        ESM.shipping_fee = shippingFee;
        ESM.profit = profit;
        ESM.Update();
    }
    /// <summary>
    /// As long as there is a disabled, returning true parts.
    /// </summary>
    /// <param name="systemSKU"></param>
    /// <returns></returns>
    public static bool WarnPartInvalid(int systemSKU)
    {
        return Config.ExecuteScalarInt32(string.Format(@"
    select count(p.tag) from tb_ebay_system_parts sp left join tb_product p
        on p.product_serial_no=sp.luc_sku and sp.ebayShowit=1
         left join 
tb_part_group_detail pd  on p.product_serial_no=pd.product_serial_no and pd.part_group_id=sp.part_group_id
left join tb_part_group pg on pg.part_group_id=pd.part_group_id
        where system_sku='{0}' and (p.tag=0 or p.tag is null or pd.showit=0 or pd.showit is null or pg.showit=0 or p.for_sys=0) and p.split_line=0", systemSKU)) > 0;
    }

    /// <summary>
    /// get ebay system Itemid.
    /// </summary>
    /// <param name="sys_sku"></param>
    /// <returns></returns>
    public static string GetEbayItemID(int sys_sku)
    {
        DataTable dt = Config.ExecuteDataTable("select ebay_code from tb_ebay_code_and_luc_sku where SKU='" + sys_sku.ToString() + "' and is_sys=1 order by id desc limit 0,1");
        if (dt.Rows.Count == 1)
        {
            return dt.Rows[0][0].ToString();
        }
        else
        {
            EmailHelper.SendTo("wu.th@qq.com"
                , string.Format(" Have {0} parent system({1}).", dt.Rows.Count, sys_sku)
                , "ebay create sub system error. system SKU: " + sys_sku.ToString()
                );
            return null;
        }
    }

    #region methods
    /// <summary>
    /// 
    /// </summary>
    /// <param name="str">detailID +"|"+ sku +"|"+ price + "|" commentID +"|"+ partGroupID</param>
    /// <returns></returns>
    private static string splitString(string str, int index)
    {
        return str.Split(new char[] { '|' })[index];
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="esm"></param>
    /// <returns></returns>
    public static FlashType GetFlashType(EbaySystemModel esm)
    {
        if (esm.is_barebone)
            return FlashType.barebone;
        else if (esm.is_shrink)
            return FlashType.Child;
        else
            return FlashType.old;
    }
}

public enum FlashType
{
    old
    ,Child
    , barebone
}
