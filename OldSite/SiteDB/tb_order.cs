//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SiteDB
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
