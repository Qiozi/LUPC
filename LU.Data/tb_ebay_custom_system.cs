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
    
    [Serializable]
    public partial class tb_ebay_custom_system
    {
        public int ebay_system_custom_id { get; set; }
        public string ebay_client_first_name { get; set; }
        public string ebay_client_last_name { get; set; }
        public string ebay_client_email { get; set; }
        public string ebay_client_ip { get; set; }
        public string ebay_client_note { get; set; }
        public string ebay_client_phone { get; set; }
        public string system_code { get; set; }
        public string old_system_templete_serial_no { get; set; }
        public string new_system_templete_serial_no { get; set; }
        public string create_system_staff { get; set; }
        public Nullable<bool> is_send_email { get; set; }
        public Nullable<System.DateTime> send_email_date { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public Nullable<System.DateTime> last_regdate { get; set; }
    }
}
