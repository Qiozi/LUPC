// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:14
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

public class RightModel
{

    // Methods
    public static void DeleteNull(nicklu2Entities context, string page_name, string part_product_category)
    {
        try
        {
            int part_id = int.Parse(part_product_category);
            //if (part_id != -1)
            //{
            Config.ExecuteNonQuery("delete from tb_right where  part_product_category='" + part_product_category + "' and right_page='" + page_name + "' ");
            // }
        }
        catch
        {
            TrackModel.InsertInfo(context, "delete from tb_right where  part_product_category='" + part_product_category + "' and right_page='" + page_name + "' ", 1000);
        }
    }

    public static tb_right FindModelByRightPage(nicklu2Entities context, string page_name, string part_product_category)
    {
        //tb_right[] rm = ActiveRecordBase<RightModel>.FindAllByProperty("right_page", page_name);
        //for (int i = 0; i < rm.Length; i++)
        //{
        //    if (rm[i].part_product_category == part_product_category)
        //    {
        //        return rm[i];
        //    }
        //}
        //return null;

        var query = context.tb_right.FirstOrDefault(me => me.right_page.Equals(page_name) && me.part_product_category.Equals(part_product_category));
        return query;
    }

    public static tb_right GetAccountModel(nicklu2Entities context, int _right_id)
    {
        var query = context.tb_right.FirstOrDefault(me => me.right_id.Equals(_right_id));
        if (query == null)
            return new tb_right();
        else
            return query;

        //RightModel[] models = ActiveRecordBase<RightModel>.FindAllByProperty("right_id", _right_id);
        //if (models.Length == 1)
        //{
        //    return models[0];
        //}
        //return new RightModel();
    }

}
