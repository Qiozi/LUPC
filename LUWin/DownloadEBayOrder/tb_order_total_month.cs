//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DownloadEBayOrder
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_order_total_month
    {
        public int id { get; set; }
        public Nullable<decimal> grand_total { get; set; }
        public string M { get; set; }
        public Nullable<int> C { get; set; }
        public Nullable<decimal> pre_grand_total { get; set; }
        public string pre_M { get; set; }
        public Nullable<int> pre_C { get; set; }
        public Nullable<decimal> eBayGrandTotal { get; set; }
        public Nullable<int> eBayC { get; set; }
        public Nullable<decimal> Pre_eBayGrandTotal { get; set; }
        public Nullable<int> Pre_eBayC { get; set; }
    }
}
