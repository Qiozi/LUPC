//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_product_quantity_change_history
    {
        public int id { get; set; }
        public string p_code { get; set; }
        public Nullable<int> old_quantity { get; set; }
        public Nullable<int> new_quantity { get; set; }
        public System.DateTime regdate { get; set; }
    }
}
