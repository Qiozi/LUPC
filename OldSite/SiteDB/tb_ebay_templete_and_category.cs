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
    
    public partial class tb_ebay_templete_and_category
    {
        public int id { get; set; }
        public Nullable<int> templete_id { get; set; }
        public Nullable<int> sys_category_id { get; set; }
        public string part_brand { get; set; }
        public Nullable<int> part_category_id { get; set; }
        public Nullable<bool> is_flash { get; set; }
        public System.DateTime regdate { get; set; }
    }
}
