// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/29/2007 5:26:15 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProductDescModel
{
    public static void SavePartComment(nicklu2Entities context, int sku, string comment, string comment_short)
    {
        //ProductDescModel.DeleteAll(" part_sku = '" + sku.ToString() + "' ");
        //ProductDescModel pdm = new ProductDescModel();
        //pdm.part_comment = comment;
        //pdm.part_sku = sku;
        //if (comment_short != null)
        //    pdm.part_short_comment = comment_short;
        //pdm.Create();

        var query = context.tb_part_comment.Where(me => me.part_sku.Value.Equals(sku)).ToList();
        foreach (var item in query)
        {
            context.tb_part_comment.Remove(item);
        }

        var model = new tb_part_comment
        {
            part_comment = comment,
            part_sku = sku,
            part_short_comment = comment_short
        };
        context.tb_part_comment.Add(model);
        context.SaveChanges();
    }
}
