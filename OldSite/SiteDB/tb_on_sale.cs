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
    
    public partial class tb_on_sale
    {
        public int serial_no { get; set; }
        public Nullable<int> product_serial_no { get; set; }
        public Nullable<System.DateTime> begin_datetime { get; set; }
        public Nullable<System.DateTime> end_datetime { get; set; }
        public Nullable<decimal> save_price_bak { get; set; }
        public Nullable<decimal> save_price { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> sale_price { get; set; }
        public string comment { get; set; }
        public Nullable<System.DateTime> modify_datetime { get; set; }
    }
}
