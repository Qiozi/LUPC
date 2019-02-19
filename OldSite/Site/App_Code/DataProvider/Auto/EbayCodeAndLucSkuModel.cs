
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/3/2010 12:08:18 PM
//
// // // // // // // // // // // // // // // //
using System;
using System.Data;

[Serializable]
public class EbayCodeAndLucSkuModel
{


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

