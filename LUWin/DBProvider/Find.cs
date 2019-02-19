using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using LUComputers.DBProvider;

namespace LUComputers.DBProvider
{
    public class Find
    {
        /// <summary>
        /// 最后一个表
        /// </summary>
        /// <param name="tableNameNoDate"></param>
        /// <returns></returns>
        public static string LastTableName(string tableNameNoDate)
        {                        
            return Config.ExecuteScalar(string.Format(@"
select table_name from information_schema.tables where table_schema='ltd_info' and table_name like '{0}%' order by table_name desc limit 1", tableNameNoDate)).ToString();
        }

        /// <summary>
        /// 最后两个表
        /// 
        /// </summary>
        /// <param name="ltdName">公司名称。。</param>
        /// <param name="oldTable"></param>
        /// <param name="lastTable"></param>
        /// <returns></returns>
        public static void LastTwoTableName(string ltdName, ref string oldTable, ref string lastTable)
        {
            string tableNameNoDate = TableName.GetPriceTableNamePart(ltdName.ToLower());
            DataTable dt = Config.ExecuteDateTable(string.Format(@"
select table_name from information_schema.tables where table_schema='ltd_info' and table_name like '{0}%' order by table_name desc limit 0,2", tableNameNoDate));
            if(dt.Rows.Count == 2){
                lastTable = dt.Rows[0][0].ToString();
                oldTable = dt.Rows[1][0].ToString();
            }
        }

        
    }
}
