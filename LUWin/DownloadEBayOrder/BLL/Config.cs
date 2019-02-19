using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace DownloadEBayOrder
{
    public class Config
    {
        #region eBay Setting
        //Get the Keys from App.Config file
        public static string devID = ConfigurationManager.AppSettings["EbayDevID"];
        public static string appID = ConfigurationManager.AppSettings["EbayAppID"];
        public static string certID = ConfigurationManager.AppSettings["EbayCertID"];

        //Get the Server to use (Sandbox or Production)
        public static string serverUrl = ConfigurationManager.AppSettings["EbayServerUrl"];

        //Get the User Token to Use
        public static string userToken = ConfigurationManager.AppSettings["EbayUserToken"];
        public static int siteID = int.Parse(ConfigurationManager.AppSettings["EbaySiteID"].ToString());

        public static int DownDays = int.Parse(ConfigurationManager.AppSettings["DownDays"].ToString());
        #endregion

        public static string SysDocuments = ConfigurationManager.AppSettings["SysDocuments"];

        public static string[] DownloadTimes = ConfigurationManager.AppSettings["DownloadTimes"].ToString().Split(new char[] { '|' });
        public static string WatchPath = ConfigurationManager.AppSettings["WatchPath"].ToString();

        public static int GetSellerTransactionsNumberOfDays = int.Parse(ConfigurationManager.AppSettings["GetSellerTransactionsNumberOfDays"].ToString());


        public static string generatePartHtmlFileStorePath = ConfigurationManager.AppSettings["generatePartHtmlFileStorePath"];

        public static string generatePartHtmlHost { get { return ConfigurationManager.AppSettings["generatePartHtmlHost"]; } }

        public static string ConnString = ConfigurationManager.ConnectionStrings["LU"].ConnectionString;

        #region Execute DataTable
        public static DataTable ExecuteDataTable(string sql)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 18000;
                conn.Open();

                MySqlDataReader mydr = cmd.ExecuteReader();
                int fc = mydr.FieldCount;
                for (int i = 0; i < fc; i++)
                {
                    string column_name = mydr.GetName(i);
                    dt.Columns.Add(column_name);
                }



                while (mydr.Read())
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < fc; i++)
                    {
                        object o = mydr[i];
                        dr[i] = o;
                    }
                    dt.Rows.Add(dr);
                }
                mydr.Close();
                conn.Close();

                //return dt;
                //MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                //conn.Open();
                //adapter.Fill(dt);
                //conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public static int ExecuteNonQuery(string sql)
        {
            int count = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 18000;
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }
            return count;
        }

        public static object ExecuteScalar(string sql)
        {
            object o = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                o = cmd.ExecuteScalar();
                conn.Close();
                conn.Dispose();
            }
            return o;
        }

        public static int ExecuteScalarInt(string sql)
        {
            object o = -1;
            using (MySqlConnection conn = new MySqlConnection(ConnString))
            {
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                conn.Open();
                o = cmd.ExecuteScalar();
                conn.Close();
                conn.Dispose();
            }
            return int.Parse(o.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">E:\\WuTH\\My Projects\\LUComputer\\src\\LUComputers.WatchPrice\\LUComputers\\bin\\Debug\\download\\supercom_store_20090111043015.csv</param>
        /// <param name="tablename">tb_supercom_store</param>
        /// <param name="fields">(f0, f1, f2,  f11,f8,f9,f10,f3,f4,f5,f6,f7)</param>
        /// <returns>true</returns>
        public static bool ExecuteUpdateLoadByCSVTab(string filename, string tablename, string fields)
        {
            ExecuteNonQuery(string.Format(@"LOAD DATA INFILE '{0}' REPLACE INTO TABLE `{1}` 
FIELDS TERMINATED BY '\t' 
OPTIONALLY ENCLOSED BY '' 
ESCAPED BY '\\' 
LINES TERMINATED BY '\r\n'
{2}", filename, tablename, fields));
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">E:\\WuTH\\My Projects\\LUComputer\\src\\LUComputers.WatchPrice\\LUComputers\\bin\\Debug\\download\\supercom_store_20090111043015.csv</param>
        /// <param name="tablename">tb_supercom_store</param>
        /// <param name="fields">(f0, f1, f2,  f11,f8,f9,f10,f3,f4,f5,f6,f7)</param>
        /// <returns>true</returns>
        public static bool ExecuteUpdateLoadByCSV(string filename, string tablename, string fields)
        {
            ExecuteNonQuery(string.Format(@"LOAD DATA INFILE '{0}' REPLACE INTO TABLE `{1}` 
FIELDS TERMINATED BY ',' 
OPTIONALLY ENCLOSED BY '""' 
ESCAPED BY '\\' 
LINES TERMINATED BY '\r\n'
{2}", filename, tablename, fields));
            return true;
        }

        #endregion
    }
}
