// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-10-17 15:00:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_ebay_store_page")]
[Serializable]
public class EbayStorePageModel : ActiveRecordBase<EbayStorePageModel>
{
	public EbayStorePageModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    int _id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static EbayStorePageModel GetEbayStorePageModel(int _id)
    {
        EbayStorePageModel[] models = EbayStorePageModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayStorePageModel();
    }

    int _ebay_templete_id;
    [Property]
    public int ebay_templete_id
    {
        get { return _ebay_templete_id; }
        set { _ebay_templete_id = value; }
    }
    string _ebay_templete_comment;
    [Property]
    public string ebay_templete_comment
    {
        get { return _ebay_templete_comment; }
        set { _ebay_templete_comment = value; }
    }
    string _ebay_templete_content;
    [Property]
    public string ebay_templete_content
    {
        get { return _ebay_templete_content; }
        set { _ebay_templete_content = value; }
    }
    string _ebay_templete_content2;
    [Property]
    public string ebay_templete_content2
    {
        get { return _ebay_templete_content2; }
        set { _ebay_templete_content2 = value; }
    }
    string _ebay_templete_top;
    [Property]
    public string ebay_templete_top
    {
        get { return _ebay_templete_top; }
        set { _ebay_templete_top = value; }
    }
    string _ebay_templete_info;
    [Property]
    public string ebay_templete_info
    {
        get { return _ebay_templete_info; }
        set { _ebay_templete_info = value; }
    }
    //string _ebay_templete_after_filter;
    //[Property]
    //public string ebay_templete_after_filter
    //{
    //    get { return _ebay_templete_after_filter; }
    //    set { _ebay_templete_after_filter = value; }
    //}
    //string _ebay_templete_after_filter2;
    //[Property]
    //public string ebay_templete_after_filter2
    //{
    //    get { return _ebay_templete_after_filter2; }
    //    set { _ebay_templete_after_filter2 = value; }
    //}
    //string _ebay_templete_after_filter3;
    //[Property]
    //public string ebay_templete_after_filter3
    //{
    //    get { return _ebay_templete_after_filter3; }
    //    set { _ebay_templete_after_filter3 = value; }
    //}
    bool _is_system;
    [Property]
    public bool is_system
    {
        get { return _is_system; }
        set { _is_system = value; }
    }
    string _ebay_code;
    [Property]
    public string ebay_code
    {
        get { return _ebay_code; }
        set { _ebay_code = value; }
    }
    decimal _ebay_price;
    [Property]
    public decimal ebay_price
    {
        get { return _ebay_price; }
        set { _ebay_price = value; }
    }
    DateTime _ebay_publish_date;
    [Property]
    public DateTime ebay_publish_date
    {
        get { return _ebay_publish_date; }
        set { _ebay_publish_date = value; }
    }

    int _lu_sku;
    [Property]
    public int lu_sku
    {
        get { return _lu_sku; }
        set { _lu_sku = value; }
    }
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
    DateTime _last_regdate;
    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }
}
