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
    
    public partial class tb_other_inc
    {
        public int id { get; set; }
        public string other_inc_name { get; set; }
        public Nullable<int> other_inc_type { get; set; }
        public Nullable<bool> tag { get; set; }
        public Nullable<int> inc_record { get; set; }
        public Nullable<int> inc_record_valid { get; set; }
        public Nullable<int> inc_record_match { get; set; }
        public Nullable<int> bigger_than_lu { get; set; }
        public Nullable<int> less_than_lu { get; set; }
        public Nullable<int> equal_than_lu { get; set; }
        public Nullable<System.DateTime> last_run_date { get; set; }
    }
}
