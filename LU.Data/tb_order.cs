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
    
    public partial class tb_order
    {
        public int order_serial_no { get; set; }
        public string order_shipping_address { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }
        public string order_code { get; set; }
        public Nullable<System.DateTime> order_ship_date { get; set; }
        public string order_pay_method { get; set; }
        public Nullable<float> order_amount { get; set; }
        public Nullable<float> order_shipping_and_handling { get; set; }
        public Nullable<float> order_pst_gst { get; set; }
        public Nullable<float> order_discount { get; set; }
        public Nullable<float> order_total { get; set; }
        public string order_staff { get; set; }
        public Nullable<int> order_customer { get; set; }
        public Nullable<sbyte> is_pay_end { get; set; }
        public Nullable<sbyte> tag { get; set; }
        public string order_note { get; set; }
        public string state_name { get; set; }
        public Nullable<int> system_category_serial_no { get; set; }
    }
}
