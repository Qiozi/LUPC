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
    
    public partial class tb_order_product
    {
        public int serial_no { get; set; }
        public string order_code { get; set; }
        public Nullable<int> product_serial_no { get; set; }
        public Nullable<int> order_product_sum { get; set; }
        public Nullable<decimal> order_product_price { get; set; }
        public sbyte tag { get; set; }
        public Nullable<decimal> order_product_cost { get; set; }
        public string sku { get; set; }
        public Nullable<int> menu_child_serial_no { get; set; }
        public Nullable<int> menu_pre_serial_no { get; set; }
        public Nullable<int> product_type { get; set; }
        public string product_name { get; set; }
        public Nullable<decimal> old_price { get; set; }
        public Nullable<decimal> order_product_sold { get; set; }
        public Nullable<decimal> save_price { get; set; }
        public string product_type_name { get; set; }
        public Nullable<bool> is_old { get; set; }
        public Nullable<decimal> product_current_price_rate { get; set; }
        public string ebayItemID { get; set; }
        public string prodType { get; set; }
    }
}
