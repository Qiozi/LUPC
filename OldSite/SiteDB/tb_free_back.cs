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
    
    public partial class tb_free_back
    {
        public int free_back_serial_no { get; set; }
        public string free_back_title { get; set; }
        public string free_back_body { get; set; }
        public string free_back_author { get; set; }
        public Nullable<System.DateTime> free_back_create_date { get; set; }
        public Nullable<int> staff_serial_no { get; set; }
        public Nullable<int> free_back_parent_serial_no { get; set; }
        public Nullable<sbyte> tag { get; set; }
        public Nullable<int> system_category_serial_no { get; set; }
        public string feedback_first_name { get; set; }
        public string feedback_last_name { get; set; }
        public string feedback_phone { get; set; }
        public string feedback_ext_code { get; set; }
    }
}
