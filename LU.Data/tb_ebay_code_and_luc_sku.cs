//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LU.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_ebay_code_and_luc_sku
    {
        public int id { get; set; }
        public Nullable<int> SKU { get; set; }
        public Nullable<bool> is_sys { get; set; }
        public string ebay_code { get; set; }
        public Nullable<decimal> BuyItNowPrice { get; set; }
        public Nullable<bool> is_online { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
    }
}
