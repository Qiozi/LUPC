//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Web.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_yun_stock_price
    {
        public int Id { get; set; }
        public string yun_code { get; set; }
        public string yun_name { get; set; }
        public decimal yun_price { get; set; }
        public decimal yun_cost { get; set; }
        public System.DateTime regdate { get; set; }
        public int staff_id { get; set; }
        public string staff_name { get; set; }
    }
}
