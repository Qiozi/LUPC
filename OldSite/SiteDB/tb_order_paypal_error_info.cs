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
    
    public partial class tb_order_paypal_error_info
    {
        public int id { get; set; }
        public string errKey { get; set; }
        public string errItem { get; set; }
        public Nullable<int> order_code { get; set; }
        public string code { get; set; }
    }
}