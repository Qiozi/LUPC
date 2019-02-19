
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/26/2010 9:36:09 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_system")]
[Serializable]
public class EbaySystemModel : ActiveRecordBase<EbaySystemModel>
{

    public EbaySystemModel()
    {

    }

    public static EbaySystemModel GetEbaySystemModel(int _id)
    {
        EbaySystemModel[] models = EbaySystemModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbaySystemModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _category_id;

    [Property]
    public int category_id
    {
        get { return _category_id; }
        set { _category_id = value; }
    }

    string _ebay_system_name;

    [Property]
    public string ebay_system_name
    {
        get { return _ebay_system_name; }
        set { _ebay_system_name = value; }
    }

    string _ebay_subtitle;

    [Property]
    public string ebay_subtitle
    {
        get { return _ebay_subtitle; }
        set { _ebay_subtitle = value; }
    }

    decimal _ebay_system_price;

    [Property]
    public decimal ebay_system_price
    {
        get { return _ebay_system_price; }
        set { _ebay_system_price = value; }
    }

    string _ebay_system_current_number;

    [Property]
    public string ebay_system_current_number
    {
        get { return _ebay_system_current_number; }
        set { _ebay_system_current_number = value; }
    }

    decimal _selected_ebay_sell;
    [Property]
    public decimal selected_ebay_sell
    {
        get { return _selected_ebay_sell; }
        set { _selected_ebay_sell = value; }
    }

    decimal _no_selected_ebay_sell;
    [Property]
    public decimal no_selected_ebay_sell
    {
        get { return _no_selected_ebay_sell; }
        set { _no_selected_ebay_sell = value; }
    }

    bool _showit;

    [Property]
    public bool showit
    {
        get { return _showit; }
        set { _showit = value; }
    }

    int _view_count;

    [Property]
    public int view_count
    {
        get { return _view_count; }
        set { _view_count = value; }
    }

    string _logo_filenames;

    [Property]
    public string logo_filenames
    {
        get { return _logo_filenames; }
        set { _logo_filenames = value; }
    }

    string _keywords;

    [Property]
    public string keywords
    {
        get { return _keywords; }
        set { _keywords = value; }
    }

    string _system_title1;

    [Property]
    public string system_title1
    {
        get { return _system_title1; }
        set { _system_title1 = value; }
    }

    string _system_title2;

    [Property]
    public string system_title2
    {
        get { return _system_title2; }
        set { _system_title2 = value; }
    }

    string _system_title3;

    [Property]
    public string system_title3
    {
        get { return _system_title3; }
        set { _system_title3 = value; }
    }

    string _cutom_label;

    [Property]
    public string cutom_label
    {
        get { return _cutom_label; }
        set { _cutom_label = value; }
    }

    string _main_comment_ids;

    [Property]
    public string main_comment_ids
    {
        get { return _main_comment_ids; }
        set { _main_comment_ids = value; }
    }

    bool _is_issue;

    [Property]
    public bool is_issue
    {
        get { return _is_issue; }
        set { _is_issue = value; }
    }

    string _large_pic_name;

    [Property]
    public string large_pic_name
    {
        get { return _large_pic_name; }
        set { _large_pic_name = value; }
    }

    bool _is_from_ebay;

    [Property]
    public bool is_from_ebay
    {
        get { return _is_from_ebay; }
        set { _is_from_ebay = value; }
    }

    decimal _adjustment;

    [Property]
    public decimal adjustment
    {
        get { return _adjustment; }
        set { _adjustment = value; }
    }

    int _source_code;
    [Property]
    public int source_code
    {
        get { return _source_code; }
        set { _source_code = value; }
    }

    bool _is_shrink;
    [Property]
    public bool is_shrink
    {
        get { return _is_shrink; }
        set { _is_shrink = value; }
    }

    bool _is_disable_flash_customize;
    [Property]
    public bool is_disable_flash_customize
    {
        get { return _is_disable_flash_customize; }
        set { _is_disable_flash_customize = value; }
    }

    int _parentID;
    [Property]
    public int parentID
    {
        get { return _parentID; }
        set { _parentID = value; }
    }

    string _parent_ebay_itemid;
    [Property]
    public string parent_ebay_itemid
    {
        get { return _parent_ebay_itemid; }
        set { _parent_ebay_itemid = value; }
    }

    bool _is_barebone;
    [Property]
    public bool is_barebone
    {
        get { return _is_barebone; }
        set { _is_barebone = value; }
    }

    int _templete_id;

    [Property]
    public int templete_id
    {
        get { return _templete_id; }
        set { _templete_id = value; }
    }

    decimal _cost;
    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    decimal _ebay_fee;
    [Property]
    public decimal ebay_fee
    {
        get { return _ebay_fee; }
        set { _ebay_fee = value; }
    }

    decimal _shipping_fee;
    [Property]
    public decimal shipping_fee
    {
        get { return _shipping_fee; }
        set { _shipping_fee = value; }
    }

    decimal _profit;
    [Property]
    public decimal profit
    {
        get { return _profit; }
        set { _profit = value; }
    }



    //public static NHibernate.Expression.Order[] CurrentOrder(bool order)
    //{
    //    NHibernate.Expression.Order o1 = new NHibernate.Expression.Order("system_templete_order", order);
    //    NHibernate.Expression.Order o = new NHibernate.Expression.Order("system_templete_serial_no", order);
    //    NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o1, o };
    //    return oo;
    //}

    //public static SystemTempleteModel[] GetSystemTempleteModelByMenuChildAndShowit(int sys_product_category_id, byte show_it, bool order)
    //{
    //    NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("tag", show_it);
    //    NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("system_templete_category_serial_no", sys_product_category_id);

    //    NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);

    //    return SystemTempleteModel.FindAll(CurrentOrder(order), a);
    //}

    //public static SystemTempleteModel[] GetSystemTempleteModelByMenuChild(int sys_product_category_id, bool order)
    //{
    //    NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("system_templete_category_serial_no", sys_product_category_id);

    //    return SystemTempleteModel.FindAll(CurrentOrder(order), eq2);
    //}

    //public static SystemTempleteModel[] GetSystemTempleteModelByMenuChildAndShowit(int sys_product_category_id, Showit show_it, bool order)
    //{
    //    if (show_it == Showit.all)
    //        return GetSystemTempleteModelByMenuChild(sys_product_category_id, order);
    //    else
    //        return GetSystemTempleteModelByMenuChildAndShowit(sys_product_category_id, byte.Parse(show_it == Showit.show_true ? "1" : "0"), order);

    //}

    //public static DataTable GetModelsBySearch(string keyword)
    //{
    //    string sql = "Select * from tb_system_templete where tag=1 ";
    //    if (keyword != "")
    //        sql += " and ( system_templete_serial_no like '%" + keyword + "%' or system_templete_name like '%" + keyword + "%')";
    //    sql += " order by system_templete_order asc";
    //    return Config.ExecuteDataTable(sql);
    //}

    //public static DataTable GetModelsBySearch(string keyword, int sys_product_category_id)
    //{
    //    string sql = "Select * from tb_system_templete where tag=1 ";
    //    if (keyword != "")
    //        sql += " and ( system_templete_serial_no like '%" + keyword + "%' or system_templete_name like '%" + keyword + "%')";
    //    if (sys_product_category_id != -1)
    //        sql += " and system_templete_category_serial_no=" + sys_product_category_id;
    //    sql += " order by system_templete_order asc";
    //    return Config.ExecuteDataTable(sql);
    //}

    /// <summary>
    /// 取得子产品已禁售的系统
    /// </summary>
    /// <returns></returns>
    public static DataTable FindSystemByWarn()
    {
        return Config.ExecuteDataTable(@"select distinct st.id, st.ebay_system_name system_templete_name from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku
inner join tb_ebay_system st on st.id=sp.system_sku
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no 
where  st.showit=1 and (pc.tag=0 or  p.tag=0  or (p.product_store_sum<1 and p.ltd_stock < 1 and p.is_non=0 and p.split_line=0) ) and p.menu_child_serial_no <> 260");
        // 260 是超频产品
    }

//    /// <summary>
//    /// find system price filter by card rate
//    /// </summary>
//    /// <param name="system_templete_serial_no"></param>
//    /// <returns></returns>
//    public static decimal FindSystemPrice(int system_templete_serial_no, decimal card_rate)
//    {
//        decimal price = 0M;
//        EbaySystemPartsModel[] spms = EbaySystemPartsModel.FindAllByProperty("system_sku", system_templete_serial_no);
//        for (int i = 0; i < spms.Length; i++)
//        {
//            ProductModel pm = ProductModel.GetProductModel(spms[i].luc_sku);
//            if (pm.product_current_price != 0M)
//                price += ConvertPrice.ChangePrice(pm.product_current_price - ProductModel.FindOnSaleDiscountByPID(pm.product_serial_no), card_rate);
//        }
//        return price;
//    }



//    public static void FileSystemPriceAndSave(int system_templete_serial_no, ref decimal price, ref decimal save)
//    {
//        DataTable dt = Config.ExecuteDataTable(@" select sum(p.product_current_price) price, sum(p.product_current_discount) save from tb_product p inner join 
// tb_ebay_system_parts sp on p.product_serial_no = sp.luc_sku inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no
//where pc.tag=1 and p.tag=1 and sp.system_sku='" + system_templete_serial_no.ToString() + "'");
//        if (dt.Rows.Count == 1)
//        {
//            decimal.TryParse(dt.Rows[0]["price"].ToString(), out price);
//            decimal.TryParse(dt.Rows[0]["save"].ToString(), out save);
//        }
//    }

}

