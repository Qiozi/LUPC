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
    
    public partial class tb_expend
    {
        public int ID { get; set; }
        public string CateName { get; set; }
        public string Title { get; set; }
        public Nullable<decimal> OutTotal { get; set; }
        public Nullable<System.DateTime> OutBeginDate { get; set; }
        public Nullable<System.DateTime> OutEndDate { get; set; }
        public Nullable<decimal> Average { get; set; }
        public string CreateName { get; set; }
        public Nullable<System.DateTime> Regdate { get; set; }
    }
}
