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
    
    public partial class tb_email_history
    {
        public int serial_no { get; set; }
        public string email_to { get; set; }
        public string email_subject { get; set; }
        public string email_body { get; set; }
        public Nullable<System.DateTime> email_create_datetime { get; set; }
        public Nullable<int> staff_serial_no { get; set; }
        public Nullable<int> system_category_serial_no { get; set; }
    }
}
