using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Model.Enum
{
    public enum StockFileType
    {
        None,
        /// <summary>
        /// 库存
        /// </summary>
        Stock = 1,
        /// <summary>
        /// 销售
        /// </summary>
        Sale = 2
    }
}
