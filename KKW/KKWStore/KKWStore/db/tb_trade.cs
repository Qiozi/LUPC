//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KKWStore.db
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_trade
    {
        public int id { get; set; }
        public string seller_nick { get; set; }
        public string byuer_nick { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public string iid { get; set; }
        public string price { get; set; }
        public string pic_path { get; set; }
        public string num { get; set; }
        public string tid { get; set; }
        public string buyer_message { get; set; }
        public string shipping_type { get; set; }
        public string alipay_no { get; set; }
        public string payment { get; set; }
        public string discount_fee { get; set; }
        public string adjust_fee { get; set; }
        public string receiver_name { get; set; }
        public string receiver_state { get; set; }
        public string receiver_city { get; set; }
        public string receiver_district { get; set; }
        public string receiver_address { get; set; }
        public string receiver_zip { get; set; }
        public string receiver_mobile { get; set; }
        public string receiver_phone { get; set; }
        public string num_iid { get; set; }
        public System.DateTime regdate { get; set; }
    }
}