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
    
    public partial class tb_ebay_system_part_comment
    {
        public int id { get; set; }
        public string comment { get; set; }
        public string category_ids { get; set; }
        public Nullable<bool> is_case { get; set; }
        public Nullable<bool> is_cpu { get; set; }
        public Nullable<bool> is_lcd { get; set; }
        public Nullable<bool> is_mb { get; set; }
        public Nullable<bool> is_video { get; set; }
        public Nullable<bool> is_audio { get; set; }
        public Nullable<bool> is_network { get; set; }
        public Nullable<bool> is_cpu_fan { get; set; }
        public Nullable<int> max_quantity { get; set; }
        public Nullable<int> priority { get; set; }
        public Nullable<bool> showit { get; set; }
        public Nullable<decimal> append_charge { get; set; }
        public Nullable<int> section { get; set; }
        public string e_field_name { get; set; }
        public Nullable<bool> eBayShowit { get; set; }
        public Nullable<int> defaultPartGroupId { get; set; }
    }
}
