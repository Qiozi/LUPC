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
    
    public partial class tb_cart_temp_price
    {
        public int tmp_price_serial_no { get; set; }
        public Nullable<decimal> sub_total { get; set; }
        public Nullable<decimal> shipping_and_handling { get; set; }
        public Nullable<decimal> sales_tax { get; set; }
        public Nullable<decimal> grand_total { get; set; }
        public string order_code { get; set; }
        public Nullable<System.DateTime> create_datetime { get; set; }
        public Nullable<decimal> gst { get; set; }
        public Nullable<decimal> pst { get; set; }
        public Nullable<decimal> hst { get; set; }
        public Nullable<decimal> sur_charge_rate { get; set; }
        public Nullable<decimal> sur_charge { get; set; }
        public Nullable<decimal> gst_rate { get; set; }
        public Nullable<decimal> pst_rate { get; set; }
        public Nullable<decimal> hst_rate { get; set; }
        public Nullable<decimal> sub_total_rate { get; set; }
        public Nullable<decimal> shipping_and_handling_rate { get; set; }
        public Nullable<decimal> sales_tax_rate { get; set; }
        public Nullable<decimal> grand_total_rate { get; set; }
        public Nullable<decimal> gst_charge_rate { get; set; }
        public Nullable<decimal> pst_charge_rate { get; set; }
        public Nullable<decimal> hst_charge_rate { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> taxable_total { get; set; }
        public string price_unit { get; set; }
    }
}
