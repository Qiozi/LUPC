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
    
    public partial class tb_order_pay_record
    {
        public int id { get; set; }
        public Nullable<int> order_code { get; set; }
        public Nullable<int> pay_record_id { get; set; }
        public Nullable<System.DateTime> pay_regdate { get; set; }
        public Nullable<decimal> pay_cash { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public Nullable<decimal> balance { get; set; }
    }
}
