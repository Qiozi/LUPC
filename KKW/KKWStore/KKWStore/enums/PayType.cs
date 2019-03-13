using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KKWStore.enums
{
    public enum PayType
    {
        None=0,
        /// <summary>
        /// 费用
        /// </summary>
        Fee = 1,
        /// <summary>
        /// 进货 
        /// </summary>
        PayJinHuo = 2,
        /// <summary>
        /// 收入
        /// </summary>
        Comein = 3
    }
}
