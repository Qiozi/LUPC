//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LU.Data
{
    using System;
    using System.Collections.Generic;
    
    [Serializable]
    public partial class tb_currency_convert
    {
        public int id { get; set; }
        public Nullable<decimal> currency_cad { get; set; }
        public Nullable<decimal> currency_usd { get; set; }
        public Nullable<bool> is_current { get; set; }
        public Nullable<bool> is_auto { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public Nullable<bool> is_modify { get; set; }
    }
}
