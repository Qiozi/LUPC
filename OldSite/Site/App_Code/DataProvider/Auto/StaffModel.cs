// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-11-30 13:46:05
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;
/// <summary>
/// StaffModel 的摘要说明
/// </summary>
/// 
[Serializable]
public class StaffModel 
{
  
    public static object FindModelsByLoginName(nicklu2Entities context, string login_name)
    {
        //StaffModel[] sms = StaffModel.FindAllByProperty("staff_login_name", login_name);
        //if (sms.Length == 1)
        //{
        //    return sms[0];
        //}
        //return null;
        return context.tb_staff.FirstOrDefault(me => me.staff_login_name.Equals(login_name));
    }
}
