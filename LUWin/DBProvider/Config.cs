using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace LUComputers.DBProvider
{
    public class Config
    {
        //public static string CreateFilePath
        //{
        //    get { return ConfigurationManager.AppSettings["create_file_http_url"].ToString(); }
        //}

        public static string http_url
        {
            get { return ConfigurationManager.AppSettings["http_url"].ToString(); }
        }

        public static string ETCCategoryURL
        {
            get { return ConfigurationManager.AppSettings["etc_category_url"].ToString(); }
        }

        //public static string change_home_url
        //{
        //    get { return ConfigurationManager.AppSettings["change_home_url"].ToString(); }
        //}
        public static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["LU"].ConnectionString.ToString(); }
        }

        public static string RemoteConnString
        {
            get { return ConfigurationManager.AppSettings["RemoteConnString"].ToString(); }
        }

        public static string valid_other_inc_id
        {
            get { return ConfigurationManager.AppSettings["valid_other_inc_id"].ToString(); }
        }

        public static string soft_download_path
        {
            get { return ConfigurationManager.AppSettings["soft_download_path"].ToString(); }
        }

        public static string auto_timer
        {
            get { return ConfigurationManager.AppSettings["auto_timer"].ToString(); }
        }

        public static int restart_day
        {
            get { return int.Parse(ConfigurationManager.AppSettings["restart_day"].ToString()); }
        }

        public static int run_hour
        {
            get { return int.Parse(ConfigurationManager.AppSettings["run_hour"].ToString()); }
        }

        public static string zipFileStorePath
        {
            get { return ConfigurationManager.AppSettings["zipFileStorePath"].ToString(); }
        }

        public static string cost_file_path
        {
            get { return ConfigurationManager.AppSettings["cost_file_path"].ToString(); }
        }

        public static string WebApiHost
        {
            get { return ConfigurationManager.AppSettings["WebApiHost"].ToString(); }
        }

        #region Execute DataTable
        public static DataTable ExecuteDateTable(string sql)
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

        #region Execute Bug DataTable
        //public static DataTable BugExecuteDateTable(string sql)
        //{
        //    DataTable dt = new DataTable();
        //    using (MySqlConnection conn = new MySqlConnection(BugConnString))
        //    {
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);

        //        conn.Open();
        //        adapter.Fill(dt);
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return dt;
        //}

        //public static int BugExecuteNonQuery(string sql)
        //{
        //    int count = -1;
        //    using (MySqlConnection conn = new MySqlConnection(BugConnString))
        //    {

        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        cmd.CommandTimeout = 18000;
        //        conn.Open();
        //        count = cmd.ExecuteNonQuery();
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return count;
        //}

        //public static object BugExecuteScalar(string sql)
        //{
        //    object o = -1;
        //    using (MySqlConnection conn = new MySqlConnection(BugConnString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        conn.Open();
        //        o = cmd.ExecuteScalar();
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return o;
        //}

        //public static int BugExecuteScalarInt(string sql)
        //{
        //    object o = -1;
        //    using (MySqlConnection conn = new MySqlConnection(BugConnString))
        //    {
        //        MySqlCommand cmd = new MySqlCommand(sql, conn);
        //        conn.Open();
        //        o = cmd.ExecuteScalar();
        //        conn.Close();
        //        conn.Dispose();
        //    }
        //    return int.Parse(o.ToString());
        //}


        #endregion

        #region remote sql
        public static int RemoteExecuteNonQuery(string sql)
        {
            int count = -1;

            using (MySqlConnection conn = new MySqlConnection(RemoteConnString))
            {

                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 180000;
                conn.Open();
                count = cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
            }

            return count;
        }

        /// <summary>
        /// 远程 正式库
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataTable RemoteExecuteDateTable(string sql)
        {
            DataTable dt = new DataTable();

            using (MySqlConnection conn = new MySqlConnection(RemoteConnString))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);

                conn.Open();
                adapter.Fill(dt);
                conn.Close();
                conn.Dispose();
            }

            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">E:\\WuTH\\My Projects\\LUComputer\\src\\LUComputers.WatchPrice\\LUComputers\\bin\\Debug\\download\\supercom_store_20090111043015.csv</param>
        /// <param name="tablename">tb_supercom_store</param>
        /// <param name="fields">(f0, f1, f2,  f11,f8,f9,f10,f3,f4,f5,f6,f7)</param>
        /// <returns>true</returns>
        public static bool UpdateLoadToRemoteByCSV(string filename, string tablename, string fields)
        {
            RemoteExecuteNonQuery(string.Format(@"LOAD DATA Local INFILE '{0}' REPLACE INTO TABLE `{1}` 
FIELDS TERMINATED BY ',' 
OPTIONALLY ENCLOSED BY '""' 
ESCAPED BY '\\' 
LINES TERMINATED BY '\r\n'
{2}", filename, tablename));
            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename">E:\\WuTH\\My Projects\\LUComputer\\src\\LUComputers.WatchPrice\\LUComputers\\bin\\Debug\\download\\supercom_store_20090111043015.csv</param>
        /// <param name="tablename">tb_supercom_store</param>
        /// <param name="fields">(f0, f1, f2,  f11,f8,f9,f10,f3,f4,f5,f6,f7)</param>
        /// <returns>true</returns>
        public static bool UpdateLoadToRemoteByCSVTab(string filename, string tablename, string fields)
        {
            RemoteExecuteNonQuery(string.Format(@"LOAD DATA Local INFILE '{0}' REPLACE INTO TABLE `{1}` 
FIELDS TERMINATED BY '\t' 
OPTIONALLY ENCLOSED BY '' 
ESCAPED BY '\\' 
LINES TERMINATED BY '\r\n'
{2}", filename, tablename, fields));
            return true;
        }

        #endregion

        #region Excel
        public static string ExcelConnstring(string filename)
        {
            return "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filename + ";Extended Properties=Excel 8.0;";
        }
        #endregion
    }
}
