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
    
    public partial class tb_out_invoice
    {
        public int id { get; set; }
        public Nullable<System.DateTime> input_regdate { get; set; }
        public string invoice_code { get; set; }
        public string staff { get; set; }
        public string summary { get; set; }
        public string note { get; set; }
        public string pay_method { get; set; }
        public Nullable<decimal> pay_total { get; set; }
        public Nullable<int> SN_Quantity { get; set; }
        public string NumIid { get; set; }
        public string Tid { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string ReceivedPayment { get; set; }
        public string ReceiverAddress { get; set; }
        public string ReceiverCity { get; set; }
        public string ReceiverDistrict { get; set; }
        public string ReceiverMobile { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverPhone { get; set; }
        public string ReceiverState { get; set; }
        public string ReceiverZip { get; set; }
        public Nullable<bool> is_Taobao { get; set; }
        public string store_name { get; set; }
        public System.DateTime regdate { get; set; }
    }
}