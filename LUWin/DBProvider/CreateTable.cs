using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace LUComputers.DBProvider
{
    public class CreateTable
    {
        /// <summary>
        /// 创建普通存储价格的表
        /// </summary>
        /// <param name="tableName"></param>
        public static void PriceTable(string tableName)
        {
            if (tableName.IndexOf("-") > -1)                
                tableName = tableName.Replace("-", "_");

            string shortName = tableName.Replace(DateTime.Now.ToString("yyyyMMdd"), "");
            DataTable dt = Config.ExecuteDateTable("show tables like '%"+ shortName +"%'");
            for (int i = 0; i < dt.Rows.Count - 60; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }
            //
            // create table
            //
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + tableName + @"`;

 CREATE TABLE `" + tableName + @"` (              
                    `id` int(6) NOT NULL auto_increment,
                    `luc_sku`   int(6) Not null default 0,
		            `sku`   varchar(40) NOT NULL,
                    `price`      decimal(10,2) NOT NULL,
                    `mfp`      varchar(50) default NULL,
                    `upc` varchar(50) default NULL,      
                    `quantity` int(6) not null default 0, 
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
                    PRIMARY KEY  (`id`)         
                    ) ENGINE=MyISAM DEFAULT CHARSET=utf8 ");

            Config.ExecuteNonQuery(@"ALTER TABLE `ltd_info`.`" + tableName + @"` 
ADD INDEX `mfp` (`mfp` ASC);
");

        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="luc_sku"></param>
        /// <param name="sku"></param>
        /// <param name="price"></param>
        /// <param name="mfp"></param>
        /// <param name="upc"></param>
        /// <param name="quantity"></param>
        public static void IntoTable(string tableName, int luc_sku, string sku, decimal price, string mfp, string upc, int quantity)
        {
            Config.ExecuteNonQuery(string.Format(@"insert into {0}(luc_sku, sku, price, mfp, upc, quantity, regdate)
                values ('{1}','{2}',{3},'{4}','{5}',{6},now())"
                , tableName
                , luc_sku
                , sku
                , price
                , mfp
                , upc
                , quantity));
        }

        #region synnex
        public static void Table_Synnex(string tableName)
        {
           
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + tableName + @"`;

 CREATE TABLE `" + tableName + @"` (              
                    `serial_no` int(6) NOT NULL auto_increment,  
		            `luc_sku`   int(6) default 0,
                    `cost`      decimal(10,2) not Null default 0,
                    `msrp`      varchar(20) default NULL,
                    `part_sku` varchar(30) default NULL,        
                    `part_cost` varchar(15) default NULL,    
                    `status_code` varchar(5) default NULL,  
                    `cat_code` varchar(15) default NULL,  
                    `store_quantity` varchar(10) default 0,    
                    `mfp` varchar(40) default NULL,  
                    `mfp_name` varchar(40) default NULL,
                    `upc` varchar(50) default NULL,  
                    `ship_weight` decimal(10,2) default 0,
		            `part_name` varchar(100) default Null,
                    `long_name` varchar(200) default Null,
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
                    PRIMARY KEY  (`serial_no`)         
                    ) ENGINE=MyISAM DEFAULT CHARSET=latin1           

");
        }

        #endregion


        #region dandh
        public static void Table_Dandh(string tableName)
        {
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + tableName + @"`;

 CREATE TABLE `" + tableName + @"` (           
                      `id` int(6) NOT NULL AUTO_INCREMENT,      
                      `stock_status` varchar(5) default NULL,        
                      `quantity_on_hand` int(3) default 0,        
                      `rebate_flag` varchar(5) default NULL,         
                      `rebate_end_date` varchar(12) default NULL,    
                      `item_number` varchar(30) not NULL,        
                      `mfr_item_number` varchar(30) not NULL,    
                      `upc_code` varchar(50) default NULL,           
                      `subcategory_code` int(6) not NULL,        
                      `vendor_name` varchar(50) default NULL,        
                      `cost` decimal(8,2) not NULL,              
                      `rebate_amount` decimal(8,2) default NULL,     
                      `held_for_future_use` int(6) default NULL,     
                      `freight` decimal(5,2) default NULL,           
                      `ship_via` varchar(5) default NULL,            
                      `weight` decimal(8,2) not NULL,            
                      `short_description` varchar(150) default NULL,  
                      `long_description` varchar(350) default NULL,
                      `luc_sku` int(6) default 0,
                      `regdate` timestamp not NULL default CURRENT_TIMESTAMP,
                       PRIMARY KEY (`id`)          
                    ) ENGINE=MyISAM DEFAULT CHARSET=latin1           

");
        }
        #endregion

        #region asi
        public static void Table_ASI(string tableName)
        {
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + tableName + @"`;

 CREATE TABLE `" + tableName + @"` (              
                    `id` int(11) NOT NULL auto_increment,     
                    `asi_sku` int(10) NOT NULL,            
                    `itmeid` varchar(100) default NULL,        
                    `description` varchar(500) default NULL,  
                    `vendor` varchar(30) NOT NULL,        
                    `cat` varchar(20) Not NULL,           
                    `quantity` int(6) NOT NULL,           
                    `price` decimal(10,2) NOT NULL,        
                    `weight` varchar(20) NOT NULL,        
                    `size` varchar(20) default NULL,          
                    `unit` varchar(20) NOT NULL,          
                    `sub_category` varchar(50) default NULL,  
                    `status` varchar(20) NOT NULL, 
                    `luc_sku` int(6) default 0,
                    `upc` varchar(50) default null,
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,
                    PRIMARY KEY  (`id`)                 
                    ) ENGINE=MyISAM DEFAULT CHARSET=latin1           

");
        }
        #endregion              
    }

    
}
