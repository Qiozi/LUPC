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
    
    public partial class tb_ask_question
    {
        public int aq_serial_no { get; set; }
        public string aq_email { get; set; }
        public string aq_title { get; set; }
        public string aq_body { get; set; }
        public string aq_product_title { get; set; }
        public Nullable<int> menu_child_serial_no { get; set; }
        public string product_serial_no { get; set; }
        public string aq_reply_body { get; set; }
        public Nullable<sbyte> aq_send { get; set; }
        public Nullable<System.DateTime> create_datetime { get; set; }
        public string ip { get; set; }
        public Nullable<sbyte> product_category { get; set; }
        public Nullable<System.DateTime> send_regdate { get; set; }
    }
}