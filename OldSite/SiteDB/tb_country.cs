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
    
    public partial class tb_country
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Nullable<bool> is_eBay_Exclude_Ship_to { get; set; }
        public Nullable<bool> bak_ship_to { get; set; }
    }
}