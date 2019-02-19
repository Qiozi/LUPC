// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 14:11:38
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class ProductDetailModel 
{
    //public static ProductDetailModel[] GetModelsByProductInSerial(nicklu2Entities context, int product_in_serial_no)
    //{
    //    return ProductDetailModel.FindAllByProperty("product_in_serial_no", product_in_serial_no);
    //}

    public static void DeleteByProductInSerial(nicklu2Entities context, int product_in_serial_no)
    {
        //ProductDetailModel[] models = GetModelsByProductInSerial(product_in_serial_no);
        //for (int i = 0; i < models.Length; i++)
        //{
        //    models[i].Delete();
        //}
        var query = context.tb_product_detail.Where(me => me.product_in_serial_no.Value.Equals(product_in_serial_no)).ToList();
        foreach(var item in query)
        {
            context.tb_product_detail.Remove(item);
        }
        context.SaveChanges();
    }

    public static int GetCountByProduct(nicklu2Entities context, int product_id)
    {
        // return ProductDetailModel.FindAllByProperty("product_serial_no", product_id).Length;

        return context.tb_product_detail.Count(me => me.product_serial_no.Value.Equals(product_id));
    }

    public static tb_product_detail[] GetModelsBySN(nicklu2Entities context, string product_sn)
    {
        //  return ProductDetailModel.FindAllByProperty("product_sn", product_sn);
        return context.tb_product_detail.Where(me => me.product_sn.Equals(product_sn)).ToList().ToArray();
    }

    public static string  GetSNByProductID(int product_serial_no)
    {
        DataTable dt = Config.ExecuteDataTable("select product_sn from tb_product_detail where product_detail_is_sale=0 and product_serial_no=" + product_serial_no + " order by product_detail_serial_no asc limit 1,1 ");
        if (dt.Rows.Count == 1)
            return dt.Rows[0][0].ToString();
        return "未找到此产品SN";
    }
}
