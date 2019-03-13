
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	6/3/2013 9:36:07 AM
//
// // // // // // // // // // // // // // // //

using System;
using System.Data;
using System.Linq;

namespace KKWStore.db
{
    [Serializable]
    public class UserModel
    {

        public UserModel()
        {

        }


        public static bool ChangePwd(qstoreEntities context, string oldPwd, string newPwd, tb_user um)
        {
            if (KKWStore.Helper.MD5.Encode(oldPwd) == um.user_pwd)
            {
                var tbUser = context.tb_user.Single(me => me.id.Equals(um.id));
                um.user_pwd = KKWStore.Helper.MD5.Encode(newPwd);
                tbUser.user_pwd = KKWStore.Helper.MD5.Encode(newPwd);
                //um.phone = KKWStore.Helper.MD5.Encode(newPwd);
                context.SaveChanges();
                return true;
            }
            else
                return false; ;
        }
    }
}

