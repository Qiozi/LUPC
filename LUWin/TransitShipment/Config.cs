using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace TransitShipment
{
    public class Config
    {
        public static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["LU"].ConnectionString.ToString(); }
        }
           
        public static string cost_file_path
        {
            get { return ConfigurationManager.AppSettings["tmpSavePath"].ToString(); }
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
        #endregion
    }
}
