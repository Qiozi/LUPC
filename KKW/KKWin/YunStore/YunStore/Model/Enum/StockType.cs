using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Model.Enum
{
    public enum StockType
    {
        /// <summary>
        /// None
        /// </summary>
        None,
        /// <summary>
        /// 在仓库
        /// </summary>
        On = 1,
        /// <summary>
        /// 出库
        /// </summary>
        Out
    }
}
