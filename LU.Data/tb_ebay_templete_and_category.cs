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
    public partial class tb_ebay_templete_and_category
    {
        public int id { get; set; }
        public Nullable<int> templete_id { get; set; }
        public Nullable<int> sys_category_id { get; set; }
        public string part_brand { get; set; }
        public Nullable<int> part_category_id { get; set; }
        public Nullable<bool> is_flash { get; set; }
        public System.DateTime regdate { get; set; }
    }
}
