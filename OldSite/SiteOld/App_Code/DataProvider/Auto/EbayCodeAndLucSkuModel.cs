
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/3/2010 12:08:18 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_code_and_luc_sku")]
[Serializable]
public class EbayCodeAndLucSkuModel : ActiveRecordBase<EbayCodeAndLucSkuModel>
{

    public EbayCodeAndLucSkuModel()
    {

    }

    public static EbayCodeAndLucSkuModel GetEbayCodeAndLucSkuModel(int _id)
    {
        EbayCodeAndLucSkuModel[] models = EbayCodeAndLucSkuModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayCodeAndLucSkuModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _SKU;

    [Property]
    public int SKU
    {
        get { return _SKU; }
        set { _SKU = value; }
    }

    bool _is_sys;

    [Property]
    public bool is_sys
    {
        get { return _is_sys; }
        set { _is_sys = value; }
    }

    string _ebay_code;

    [Property]
    public string ebay_code
    {
        get { return _ebay_code; }
        set { _ebay_code = value; }
    }

    bool _is_online;
    [Property]
    public bool is_online
    {
        get { return _is_online; }
        set { _is_online = value; }
    }

    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }

    /// <summary>
    /// 获取工厂编号MFP#
    /// </summary>
    /// <param name="ebayItemId"></param>
    /// <returns></returns>
    public static string GetMFP(string ebayItemId, ref string typeStr)
    {
        typeStr = "Laptop/Notebook";
        DataTable dt = Config.ExecuteDataTable(@"select manufacturer_part_number, product_name from tb_product p inner join tb_ebay_code_and_luc_sku ec on ec.sku = p.product_serial_no
where ec.ebay_code='" + ebayItemId + "'");
        if (dt.Rows.Count > 0)
        {
            if (dt.Rows[0][1].ToString().ToLower().IndexOf("netbook")>-1)
                typeStr = "Netbook";
            else if (dt.Rows[0][1].ToString().ToLower().IndexOf("Tablet")>-1)
                typeStr = "Tablet";
            return dt.Rows[0][0].ToString();
        }
        return null;
    }
}

