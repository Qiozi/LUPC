
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Model.Enum
{
    public enum WareHouseType
    {
        /// <summary>
        /// NONE
        /// </summary>
        [Description("NONE")]
        None,
        /// <summary>
        /// 公司
        /// </summary>
        [Description("公司")]
        Comp,
        /// <summary>
        /// 秒仓
        /// </summary>
        [Description("秒仓")]
        Yun
    }
}
