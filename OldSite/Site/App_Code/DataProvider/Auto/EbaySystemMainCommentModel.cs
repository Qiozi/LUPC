
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/7/2010 11:01:14 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System.Linq;
using System;

[Serializable]
public class EbaySystemMainCommentModel 
{
    public static tb_ebay_system_main_comment GetEbaySystemMainCommentModel(nicklu2Entities context, int id)
    {

        return context.tb_ebay_system_main_comment.Single(me => me.id.Equals(id));
    }
}

