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
    
    public partial class tb_ebay_system_history
    {
        public int id { get; set; }
        public string sys_sku { get; set; }
        public Nullable<int> category_id { get; set; }
        public string ebay_system_name { get; set; }
        public string ebay_subtitle { get; set; }
        public Nullable<decimal> ebay_system_price { get; set; }
        public string ebay_system_current_number { get; set; }
        public Nullable<decimal> selected_ebay_sell { get; set; }
        public Nullable<decimal> no_selected_ebay_sell { get; set; }
        public Nullable<bool> is_include_shipping { get; set; }
        public Nullable<bool> showit { get; set; }
        public Nullable<int> view_count { get; set; }
        public string logo_filenames { get; set; }
        public string keywords { get; set; }
        public string system_title1 { get; set; }
        public string system_title2 { get; set; }
        public string system_title3 { get; set; }
        public string cutom_label { get; set; }
        public string main_comment_ids { get; set; }
        public Nullable<bool> is_issue { get; set; }
        public string large_pic_name { get; set; }
        public Nullable<bool> is_from_ebay { get; set; }
        public Nullable<decimal> adjustment { get; set; }
        public Nullable<int> source_code { get; set; }
        public Nullable<bool> is_online { get; set; }
        public Nullable<bool> is_disable_flash_customize { get; set; }
        public System.DateTime regdate { get; set; }
        public string ebay_itemid { get; set; }
    }
}
