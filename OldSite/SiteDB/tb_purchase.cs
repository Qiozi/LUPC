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
    
    public partial class tb_purchase
    {
        public int purchase_serial_no { get; set; }
        public string purchase_invoice { get; set; }
        public string purchase_net_amount { get; set; }
        public string purchase_gst { get; set; }
        public string purchase_pst { get; set; }
        public string purchase_paid_amount { get; set; }
        public string purchase_check_no { get; set; }
        public string purchase_bank { get; set; }
        public Nullable<System.DateTime> purchase_date { get; set; }
        public string purchase_note { get; set; }
        public Nullable<int> vendor_serial_no { get; set; }
        public string staff_serial_no { get; set; }
        public Nullable<int> system_category_serial_no { get; set; }
        public string purchase_product_list { get; set; }
    }
}
