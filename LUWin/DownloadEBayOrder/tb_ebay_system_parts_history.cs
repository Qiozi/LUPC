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
    
    public partial class tb_ebay_system_parts_history
    {
        public int id { get; set; }
        public string parent_id { get; set; }
        public Nullable<int> system_sku { get; set; }
        public Nullable<int> luc_sku { get; set; }
        public Nullable<int> comment_id { get; set; }
        public string comment_name { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<int> part_quantity { get; set; }
        public Nullable<int> max_quantity { get; set; }
        public string compatibility_parts { get; set; }
        public Nullable<int> part_group_id { get; set; }
        public Nullable<bool> is_online { get; set; }
        public Nullable<bool> is_label_of_flash { get; set; }
        public System.DateTime regdate { get; set; }
        public string ebay_itemid { get; set; }
    }
}
