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
    
    public partial class tb_serial_no_change_storage
    {
        public int ID { get; set; }
        public Nullable<int> serialNo_id { get; set; }
        public Nullable<int> p_id { get; set; }
        public string p_code { get; set; }
        public Nullable<int> old_warehouse_ID { get; set; }
        public string old_warehouse_code { get; set; }
        public Nullable<int> new_warehouse_ID { get; set; }
        public string new_warehouse_code { get; set; }
        public string CreateName { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
    }
}
