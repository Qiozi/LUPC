using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MySql.Data;
using System.Data;
using MySql.Data.MySqlClient;

namespace KKWStore.db
{
    public class SqlExec
    {
        public static string ConnString
        {
            get
            {
                ////return KKWStore.Helper.Config.LocalhostServiceIP;
                //   return "Server=localhost;Database=qstore;User ID=root;Password=1234qwer;allow zero datetime=true;";
                return "Server=121.41.75.68;Database=qstore;User ID=qiozi;Password=qiozi@msn.com1;allow zero datetime=true;";
            }
        }

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

        public static int RemoteExecuteNonQuery(string sql)
        {
            int count = -1;
            //using (MySqlConnection conn = new MySqlConnection("Server=61.172.246.23;Database=a1125104651;User ID=a1125104651;Password=123456;allow zero datetime=true;"))
            //using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nicklu2;User ID=root;Password=1234qwer;allow zero datetime=true;"))
            //{

            //    MySqlCommand cmd = new MySqlCommand(sql, conn);
            //    cmd.CommandTimeout = 18000;
            //    conn.Open();
            //    count = cmd.ExecuteNonQuery();
            //    conn.Close();
            //    conn.Dispose();
            //}
            return count;
        }
        #endregion

        #region 上传到远程
        //public static int RemoteExecuteNonQueryLUComputer(string sql)
        //{
        //    int count = -1;
        //    using (MySqlConnection conn = new MySqlConnection(Helper.Config.ServiceIP))
        //    //using (MySqlConnection conn = new MySqlConnection("Server=localhost;Database=nicklu2;User ID=root;Password=1234qwer;allow zero datetime=true;"))
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
        #endregion
    }
}
