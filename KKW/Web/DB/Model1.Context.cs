﻿//------------------------------------------------------------------------------
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
    
        public virtual DbSet<tb_balance_cash_record> tb_balance_cash_record { get; set; }
        public virtual DbSet<tb_bargain_money> tb_bargain_money { get; set; }
        public virtual DbSet<tb_check_store> tb_check_store { get; set; }
        public virtual DbSet<tb_check_store_detail> tb_check_store_detail { get; set; }
        public virtual DbSet<tb_expend> tb_expend { get; set; }
        public virtual DbSet<tb_expend_average> tb_expend_average { get; set; }
        public virtual DbSet<tb_group> tb_group { get; set; }
        public virtual DbSet<tb_group_model> tb_group_model { get; set; }
        public virtual DbSet<tb_in_invoice> tb_in_invoice { get; set; }
        public virtual DbSet<tb_in_invoice_product> tb_in_invoice_product { get; set; }
        public virtual DbSet<tb_order> tb_order { get; set; }
        public virtual DbSet<tb_out_invoice> tb_out_invoice { get; set; }
        public virtual DbSet<tb_out_invoice_product> tb_out_invoice_product { get; set; }
        public virtual DbSet<tb_out_invoice_product_shipping> tb_out_invoice_product_shipping { get; set; }
        public virtual DbSet<tb_pay_method> tb_pay_method { get; set; }
        public virtual DbSet<tb_prod_cost_modify_record> tb_prod_cost_modify_record { get; set; }
        public virtual DbSet<tb_product> tb_product { get; set; }
        public virtual DbSet<tb_product_cate> tb_product_cate { get; set; }
        public virtual DbSet<tb_product_quantity_change_history> tb_product_quantity_change_history { get; set; }
        public virtual DbSet<tb_proxy> tb_proxy { get; set; }
        public virtual DbSet<tb_return_history> tb_return_history { get; set; }
        public virtual DbSet<tb_return_wholesaler> tb_return_wholesaler { get; set; }
        public virtual DbSet<tb_return_wholesaler_detail> tb_return_wholesaler_detail { get; set; }
        public virtual DbSet<tb_sales_total> tb_sales_total { get; set; }
        public virtual DbSet<tb_serial_no> tb_serial_no { get; set; }
        public virtual DbSet<tb_serial_no_and_p_code> tb_serial_no_and_p_code { get; set; }
        public virtual DbSet<tb_serial_no_change_storage> tb_serial_no_change_storage { get; set; }
        public virtual DbSet<tb_serial_no_delete_history> tb_serial_no_delete_history { get; set; }
        public virtual DbSet<tb_serial_no_no_instore> tb_serial_no_no_instore { get; set; }
        public virtual DbSet<tb_sn_change_record> tb_sn_change_record { get; set; }
        public virtual DbSet<tb_temp_ids> tb_temp_ids { get; set; }
        public virtual DbSet<tb_trade> tb_trade { get; set; }
        public virtual DbSet<tb_user> tb_user { get; set; }
        public virtual DbSet<tb_user_group> tb_user_group { get; set; }
        public virtual DbSet<tb_user_model> tb_user_model { get; set; }
        public virtual DbSet<tb_user2> tb_user2 { get; set; }
        public virtual DbSet<tb_warehouse> tb_warehouse { get; set; }
        public virtual DbSet<tb_warehouse_all_info> tb_warehouse_all_info { get; set; }
        public virtual DbSet<tb_yun_stock_async> tb_yun_stock_async { get; set; }
        public virtual DbSet<tb_yun_stock_async_code> tb_yun_stock_async_code { get; set; }
        public virtual DbSet<tb_yun_stock_child> tb_yun_stock_child { get; set; }
        public virtual DbSet<tb_yun_stock_main> tb_yun_stock_main { get; set; }
        public virtual DbSet<tb_yun_stock_price> tb_yun_stock_price { get; set; }
        public virtual DbSet<tbtoken> tbtokens { get; set; }
    }
}
