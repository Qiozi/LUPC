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
    
    public partial class tb_prod_cost_modify_record
    {
        public int Id { get; set; }
        public int p_id { get; set; }
        public string p_name { get; set; }
        public string p_code { get; set; }
        public decimal old_cost { get; set; }
        public decimal new_cost { get; set; }
        public string staff_name { get; set; }
        public System.DateTime regdate { get; set; }
    }
}
