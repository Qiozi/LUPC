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
    
    public partial class tb_ebay_part_price_history
    {
        public int id { get; set; }
        public Nullable<int> luc_sku { get; set; }
        public string mfp { get; set; }
        public string part_ebay_name { get; set; }
        public string custom_label { get; set; }
        public string ebay_itemid { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> web_price { get; set; }
        public Nullable<decimal> shipping { get; set; }
        public Nullable<decimal> profit { get; set; }
        public Nullable<decimal> ebay_fee { get; set; }
        public Nullable<decimal> ebay_price { get; set; }
        public System.DateTime regdate { get; set; }
    }
}
