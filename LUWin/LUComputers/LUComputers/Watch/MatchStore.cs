using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class MatchStore
    {
        public MatchStore() { }



        public bool DeleteMatchLUSku(int ltd_id)
        {
            Config.ExecuteNonQuery(string.Format("Delete from tb_other_inc_match_lu_sku where other_inc_type='{0}'", ltd_id));
            return true;
        }

        public string CreateTable()
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.MatchStore);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_match_lu_sku_" + date_short_name;
            DataTable dt = Config.ExecuteDateTable("show tables like '%store_match_lu_sku_%'");
            for (int i = 0; i < dt.Rows.Count - 30; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }
            //
            // record table Name and Datatime.
            //
            if (Config.ExecuteScalarInt("select count(*) from tb_other_inc_run_date where date_format(run_date,'%Y%m%d')='" + date_short_name + "' and other_inc_id='" + ltd_id.ToString() + "'") == 0)
            {
                Config.ExecuteNonQuery(string.Format(@"
insert into tb_other_inc_run_date 
	( other_inc_id, run_date,db_table_name)
	values
	( '{0}', now(),'{1}')
", ltd_id, talbe_name));
            }
            //
            // create table
            //
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + talbe_name + @"`;

 CREATE TABLE `" + talbe_name + @"` (    
                             `id` int(6) NOT NULL auto_increment,       
                             `lu_sku` int(6) default NULL,              
                             `other_inc_sku` varchar(30) default NULL,  
                             `other_inc_type` int(2) default NULL,      
                             `regdate` timestamp NULL default CURRENT_TIMESTAMP,   
                            `prodType` varchar(20) default 'NEW',
                             PRIMARY KEY  (`id`)     
                  ) ENGINE=MyISAM DEFAULT CHARSET=latin1  ; 
");
            return talbe_name;
        }
    }
}
