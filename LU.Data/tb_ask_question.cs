//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
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
