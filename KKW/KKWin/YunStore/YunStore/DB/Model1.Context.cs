﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class qstoreEntities : DbContext
    {
        public qstoreEntities()
            : base("name=qstoreEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<tb_user> tb_user { get; set; }
        public virtual DbSet<tb_profit> tb_profit { get; set; }
        public virtual DbSet<tb_yun_fileinfo_company_stock_main> tb_yun_fileinfo_company_stock_main { get; set; }
        public virtual DbSet<tb_yun_fileinfo_company_stock_record> tb_yun_fileinfo_company_stock_record { get; set; }
        public virtual DbSet<tb_yun_fileinfo_sale_child> tb_yun_fileinfo_sale_child { get; set; }
        public virtual DbSet<tb_yun_fileinfo_sale_main> tb_yun_fileinfo_sale_main { get; set; }
        public virtual DbSet<tb_yun_fileinfo_stock_child> tb_yun_fileinfo_stock_child { get; set; }
        public virtual DbSet<tb_yun_fileinfo_stock_main> tb_yun_fileinfo_stock_main { get; set; }
        public virtual DbSet<tb_yun_fileinfo_company_stock_child> tb_yun_fileinfo_company_stock_child { get; set; }
    }
}
