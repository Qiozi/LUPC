//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LUComputers
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_other_inc_valid_lu_sku
    {
        public int id { get; set; }
        public Nullable<int> lu_sku { get; set; }
        public string manufacturer_part_number { get; set; }
        public Nullable<bool> is_valid { get; set; }
        public Nullable<bool> is_ncix_remain { get; set; }
        public Nullable<decimal> price { get; set; }
        public Nullable<decimal> cost { get; set; }
        public Nullable<decimal> discount { get; set; }
        public Nullable<int> ltd_stock { get; set; }
        public Nullable<int> menu_child_serial_no { get; set; }
        public string brand { get; set; }
        public Nullable<int> other_inc_id { get; set; }
        public Nullable<bool> is_changed_price { get; set; }
        public Nullable<decimal> adjustment { get; set; }
        public Nullable<decimal> curr_change_cost { get; set; }
        public Nullable<decimal> curr_change_price { get; set; }
        public string curr_change_ltd { get; set; }
        public Nullable<int> curr_change_quantity { get; set; }
        public string curr_change_regdate { get; set; }
        public Nullable<bool> isOk { get; set; }
        public Nullable<System.DateTime> regdate { get; set; }
        public string prodType { get; set; }
    }
}
