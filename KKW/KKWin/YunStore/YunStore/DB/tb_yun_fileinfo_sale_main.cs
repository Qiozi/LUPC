//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YunStore.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class tb_yun_fileinfo_sale_main
    {
        public System.Guid Gid { get; set; }
        public string Regdate { get; set; }
        public string StaffName { get; set; }
        public System.Guid StaffId { get; set; }
        public string FileName { get; set; }
        public string FileMD5 { get; set; }
        public int AllProdQty { get; set; }
        public int AllProdSaleQty { get; set; }
        public decimal AllProdSaleCost { get; set; }
        public string SaleMonth { get; set; }
    }
}
