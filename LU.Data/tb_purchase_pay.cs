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
    
    public partial class tb_purchase_pay
    {
        public int amount { get; set; }
        public int purchase_pay_serial_no { get; set; }
        public Nullable<int> pay_method_serial_no { get; set; }
        public string check_code { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<float> balance { get; set; }
        public Nullable<int> tag { get; set; }
        public Nullable<System.DateTime> create_datetime { get; set; }
        public Nullable<int> purchase_serial_no { get; set; }
    }
}
