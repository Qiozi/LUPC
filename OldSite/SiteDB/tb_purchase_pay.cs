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
    
    public partial class tb_purchase_pay
    {
        public int amount { get; set; }
        public int purchase_pay_serial_no { get; set; }
        public Nullable<int> pay_method_serial_no { get; set; }
        public string check_code { get; set; }
        public Nullable<System.DateTime> date { get; set; }
        public Nullable<float> balance { get; set; }
        public Nullable<int> tag { get; set; }
        public Nullable<System.DateTime> create_datetime { get; set; }
        public Nullable<int> purchase_serial_no { get; set; }
    }
}
