//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DownloadEBayOrder
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_pay
    {
        public int pay_serial_no { get; set; }
        public Nullable<int> order_serial_no { get; set; }
        public Nullable<float> pay_amount { get; set; }
        public Nullable<float> pay_balance { get; set; }
        public string pay_method { get; set; }
        public Nullable<System.DateTime> pay_date { get; set; }
        public Nullable<int> pay_sales_person { get; set; }
        public string pay_note { get; set; }
        public Nullable<int> tag { get; set; }
    }
}
