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
    
    public partial class tb_login_log
    {
        public int login_log_serial_no { get; set; }
        public string remote_address { get; set; }
        public Nullable<System.DateTime> login_datetime { get; set; }
        public Nullable<System.DateTime> logout_datetime { get; set; }
        public Nullable<int> login_name { get; set; }
        public string login_log_category { get; set; }
        public string http_user_agent { get; set; }
    }
}
