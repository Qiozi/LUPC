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
    
    public partial class tb_menu
    {
        public int menu_child_serial_no { get; set; }
        public string menu_child_name { get; set; }
        public string menu_child_href { get; set; }
        public Nullable<sbyte> menu_is_exist_sub { get; set; }
        public Nullable<int> menu_parent_serial_no { get; set; }
        public Nullable<int> menu_pre_serial_no { get; set; }
        public Nullable<sbyte> tag { get; set; }
        public Nullable<int> menu_child_order { get; set; }
        public Nullable<int> old_db_id { get; set; }
        public string target { get; set; }
    }
}