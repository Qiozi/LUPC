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
    
    public partial class tb_other_inc_bind_price
    {
        public int id { get; set; }
        public Nullable<int> bind_type { get; set; }
        public Nullable<int> category_id { get; set; }
        public string manufactory { get; set; }
        public Nullable<int> priority { get; set; }
        public Nullable<int> other_inc_id { get; set; }
        public Nullable<int> luc_sku { get; set; }
        public Nullable<bool> is_single { get; set; }
        public Nullable<bool> is_relating { get; set; }
    }
}