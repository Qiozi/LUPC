using System;
using System.Collections.Generic;
using System.Text;

namespace KKWStore.Helper
{
    public class PermantentHelper
    {
        public PermantentHelper() { }

        /// <summary>
        /// 是否有权限
        /// </summary>
        /// <param name="pm"></param>
        /// <returns></returns>
        public static bool Ok(enums.PermanentModel pm)
        {
            if (Helper.Config.IsAdmin)
                return true;

            foreach (var m in Helper.Config.UserPermanentList)
            {
                if (m.ModelId == (int)pm)
                    return true;
            }
            return false;

        }
    }
}
