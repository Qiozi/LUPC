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
    
    public partial class tb_part_price_change_setting
    {
        public int id { get; set; }
        public Nullable<int> category_id { get; set; }
        public Nullable<decimal> cost_min { get; set; }
        public Nullable<decimal> cost_max { get; set; }
        public Nullable<int> rate { get; set; }
        public Nullable<bool> is_percent { get; set; }
        public System.DateTime regdate { get; set; }
    }
}