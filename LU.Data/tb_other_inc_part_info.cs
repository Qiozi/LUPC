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
    
    public partial class tb_other_inc_part_info
    {
        public int id { get; set; }
        public Nullable<int> luc_sku { get; set; }
        public Nullable<int> other_inc_id { get; set; }
        public string other_inc_sku { get; set; }
        public string manufacture_part_number { get; set; }
        public Nullable<decimal> other_inc_price { get; set; }
        public Nullable<int> other_inc_store_sum { get; set; }
        public Nullable<bool> tag { get; set; }
        public string prodType { get; set; }
        public string ETA { get; set; }
        public System.DateTime regdate { get; set; }
        public Nullable<System.DateTime> last_regdate { get; set; }
    }
}
