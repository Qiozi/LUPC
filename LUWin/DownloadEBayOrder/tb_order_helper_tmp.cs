//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DownloadEBayOrder
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_order_helper_tmp
    {
        public int order_helper_serial_no { get; set; }
        public Nullable<int> order_code { get; set; }
        public Nullable<int> customer_serial_no { get; set; }
        public Nullable<decimal> sub_total { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<decimal> total { get; set; }
        public Nullable<System.DateTime> ready_date { get; set; }
        public Nullable<sbyte> rush { get; set; }
        public string pay_method { get; set; }
        public Nullable<System.DateTime> order_date { get; set; }
        public Nullable<sbyte> system_category_serial_no { get; set; }
        public System.DateTime create_datetime { get; set; }
        public string note { get; set; }
        public sbyte out_status { get; set; }
        public Nullable<decimal> tax_charge { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<sbyte> is_pay_end { get; set; }
        public int pre_status_serial_no { get; set; }
        public Nullable<System.DateTime> prick_up_datetime1 { get; set; }
        public Nullable<System.DateTime> prick_up_datetime2 { get; set; }
        public Nullable<int> shipping_company { get; set; }
        public Nullable<sbyte> tag { get; set; }
        public Nullable<decimal> shipping_charge { get; set; }
        public string Msg_from_Seller { get; set; }
        public Nullable<sbyte> call_me { get; set; }
        public string out_note { get; set; }
        public Nullable<int> tax_rate { get; set; }
        public Nullable<bool> is_old { get; set; }
        public Nullable<decimal> gst_rate { get; set; }
        public Nullable<decimal> pst_rate { get; set; }
        public Nullable<decimal> hst_rate { get; set; }
        public Nullable<decimal> sur_charge_rate { get; set; }
        public Nullable<decimal> sur_charge { get; set; }
        public Nullable<decimal> gst { get; set; }
        public Nullable<decimal> pst { get; set; }
        public Nullable<decimal> hst { get; set; }
        public Nullable<decimal> sub_total_rate { get; set; }
        public Nullable<decimal> total_rate { get; set; }
        public Nullable<decimal> grand_total { get; set; }
        public Nullable<bool> is_ok { get; set; }
        public Nullable<decimal> taxable_total { get; set; }
        public Nullable<bool> tax_export { get; set; }
        public Nullable<int> order_pay_status_id { get; set; }
        public Nullable<int> order_invoice { get; set; }
        public Nullable<bool> is_download_invoice { get; set; }
        public Nullable<decimal> input_order_discount { get; set; }
        public Nullable<bool> is_lock_input_order_discount { get; set; }
        public Nullable<bool> is_lock_shipping_charge { get; set; }
        public Nullable<bool> order_source { get; set; }
    }
}
