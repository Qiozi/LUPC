using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
    public class ALC
    {
        public ALC() { }

        public void Run(string s,string NoteTypename, ref string table_name)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_ALC);

            string allTableName = DBProvider.TableName.ALCAll;// "store_all_alc";
            Config.ExecuteNonQuery("Delete from " + allTableName);
            table_name = CreateTable(Ltd.wholesaler_ALC);


            Regex r = new Regex("style=\"[A-Za-z0-9;:\\-\\#]+\"");

            Regex r1 = new Regex("align=\"[A-Za-z]+\"");
            Regex r2 = new Regex("onmouseout=\"[A-Za-z0-9;:\\-\\s\\.\\#=']+\"");
            Regex r3 = new Regex("onmouseover=\"[A-Za-z0-9;:\\-\\s\\.\\#=']+\"");
            s = r.Replace(s.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), "");//"style=\"(.*)\"", "");
            s = r1.Replace(s, "");
            s = r2.Replace(s, "");
            s = r3.Replace(s, "");
            s = s.Replace("<td >", "<td>");
            s = s.Replace("<td  >", "<td>");
            s = s.Replace("\t", "");

            Regex re = new Regex(@"<td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\+\,\-\#\s\.&""\$]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //Regex re = new Regex(@"<td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);//<td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>\$([0-9]+)</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);


            //this.richTextBox_result.Text = s;

            MatchCollection mc = re.Matches(s);

           
             

            foreach (Match m in mc)
            {
                try
                {
                    string source = m.Groups[0].Value.ToString().Trim().Replace("</td><td>","|");
                    string[] sources = source.Split(new char[] { '|' });
		            int luc_sku;
                    string part_sku = sources[0].ToString().Replace("<td>", "").Trim();     
                    int part_cost ;
                    int.TryParse(sources[3].ToString().Replace("$", ""), out part_cost);

                    int store_quantity;
                    int.TryParse((sources[4].ToString().Trim() == "Call" ? "-1" : sources[4].ToString()), out store_quantity);

                    string mfp = part_sku;// m.Groups[3].Value.ToString();

                    Watch.LU LU = new LU();
                    luc_sku = LU.GetSKUByMfp(mfp, "Refurbished");

                    string part_name = string.Format("{0}, {1}, {2}, {3}, {4}"
                        , sources[5].ToString().Trim()
                        , sources[6].ToString().Trim()
                        , sources[7].ToString().Trim()
                        , sources[8].ToString().Trim()
                        , sources[9].ToString().Trim());
               
                    part_name = "Acer " + sources[2].ToString().Trim() + " "+ NoteTypename +" ";

                    Config.ExecuteNonQuery(string.Format(@"insert into {0} (sku, price, quantity, mfp,  luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}')", table_name
                                                         , part_sku
                                                         , part_cost
                                                         , store_quantity
                                                         , mfp
                                                         , luc_sku));

                    Config.ExecuteNonQuery(string.Format(@"insert into {0} (alc_sku, description, mfgpn, price,stock,note2,note3,note4,note5,note1,luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}')", allTableName
                                                         , part_sku
                                                         , part_name
                                                         , mfp
                                                         , part_cost
                                                         , store_quantity
                                                         , sources[5].ToString().Trim()
                                                         , sources[6].ToString().Trim()
                                                         , sources[7].ToString().Trim()
                                                         , sources[8].ToString().Trim()
                                                         , sources[9].ToString().Trim().Replace("</td>","")
                                                         , luc_sku));
                }
                catch (Exception ex) { throw ex; }

            }

            Config.ExecuteNonQuery(string.Format(@"Delete from tb_other_inc_match_lu_sku where other_inc_type='{0}';
Insert into tb_other_inc_match_lu_sku (lu_sku, other_inc_sku, other_inc_type) select luc_sku, sku, {0} from {1} where luc_sku>0"
                ,ltd_id, table_name));
      
        }

        public string CreateTable(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_ALC);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_ALC.ToString())) + date_short_name;

            DBProvider.CreateTable.PriceTable(talbe_name);
            return talbe_name;

//            LtdHelper LH = new LtdHelper();
//            DataTable dt = Config.ExecuteDateTable("show tables like '%" + (LH.FilterText(ltd.ToString())) + "%'");
//            for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
//            {
//                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
//            }

            
//            int ltd_id = LH.LtdHelperValue(ltd);
//            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
//            string talbe_name = "store_" + (LH.FilterText(ltd.ToString())) + "_part_" + date_short_name;

//            //
//            // record table Name and Datatime.
//            //
//            if (Config.ExecuteScalarInt("select count(*) from tb_other_inc_run_date where date_format(run_date,'%Y%m%d')='" + date_short_name + "' and other_inc_id='" + ltd_id.ToString() + "'") == 0)
//            {
//                Config.ExecuteNonQuery(string.Format(@"
//insert into tb_other_inc_run_date 
//	( other_inc_id, run_date,db_table_name)
//	values
//	( '{0}', now(),'{1}')
//", ltd_id, talbe_name));
//            }
//            //
//            // create table
//            //
//            Config.ExecuteNonQuery(@"
// drop table  IF EXISTS `" + talbe_name + @"`;
//
// CREATE TABLE `" + talbe_name + @"` (              
//                    `serial_no` int(6) NOT NULL auto_increment,  
//		            `luc_sku`   int(6) default Null,
//                    `part_sku` varchar(30) default NULL,        
//                    `part_cost` decimal(8,2) default NULL,            
//                    `store_quantity` int(6) default 0,    
//                    `mfp` varchar(30) default NULL,  
//		            `part_name` varchar(300) default Null,
//                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
//                    PRIMARY KEY  (`serial_no`)         
//                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1           
//
//");
//            return talbe_name;
        }

        /// <summary>
        /// 二手产品纪录表
        /// </summary>
        /// <returns></returns>
        public string CreateTableOpenBox()
        {
            Ltd ltd = Ltd.wholesaler_ALC;

            LtdHelper LH = new LtdHelper();
            DataTable dt = Config.ExecuteDateTable("show tables like '%" + (LH.FilterText(ltd.ToString())) + "%'");
            for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }


            int ltd_id = LH.LtdHelperValue(ltd);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_" + (LH.FilterText(ltd.ToString())) + "_openbox_part_" + date_short_name;

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
                      `ID` int(6) NOT NULL AUTO_INCREMENT,   
                      `LUC_sku` int(6) DEFAULT NULL,         
                      `ALC_SKU` varchar(50) DEFAULT NULL,    
                      `MFP` varchar(50) DEFAULT NULL,        
                      `Brand` varchar(50) DEFAULT NULL,      
                      `Model` varchar(50) DEFAULT NULL,      
                      `pricture` varchar(300) DEFAULT NULL,  
                      `cost` decimal(8,2) DEFAULT NULL,      
                      `quantity` int(6) DEFAULT NULL,        
                      `screen` decimal(5,2) DEFAULT NULL,    
                      `title` varchar(500) DEFAULT NULL,     
                      `ebay_description` text,               
                      PRIMARY KEY (`ID`)                  
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1           

");
            return talbe_name;
        }
    }
}
