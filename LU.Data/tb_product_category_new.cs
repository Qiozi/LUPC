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
    public partial class tb_product_category_new
    {
        public int category_id { get; set; }
        public string category_name { get; set; }
        public Nullable<int> category_type { get; set; }
        public Nullable<bool> is_ebay { get; set; }
        public Nullable<int> priority { get; set; }
        public Nullable<int> parent_category_id { get; set; }
        public Nullable<bool> showit { get; set; }
        public Nullable<int> view_count { get; set; }
    }
}
