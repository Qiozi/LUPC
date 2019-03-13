using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Threading;
using System.Diagnostics;

namespace KKWStore
{
    public partial class frmBackup : Form
    {
        public frmBackup()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmBackup_Shown);
        }

        void frmBackup_Shown(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["NoRunBackup"].ToString() == "1")
            {
                Application.Exit();
                Application.ExitThread();
                return;
            }
            Thread t = new Thread(new ThreadStart(backup));
            t.Start();
        }

        void backup()
        {
            this.label1.Invoke(new MethodInvoker(delegate()
            {

                string path = Helper.TaobaoConfig.BackupPath + "\\Backup\\";
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                string filepath = path + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss");
                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                try
                {
                    //
                    // 删除30天前的文件夹
                    DirectoryInfo dirs = new DirectoryInfo(path);
                    DirectoryInfo[] subDirs = dirs.GetDirectories();
                    foreach (var d in subDirs)
                    {
                        
                            TimeSpan T1 = new TimeSpan(d.LastWriteTime.AddDays(60).Ticks);
                            TimeSpan T2 = new TimeSpan(DateTime.Now.Ticks);

                            int day = (DateTime.Now - d.LastWriteTime).Days;

                            if (day > 60)
                        {
                            Directory.Delete(d.FullName, true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

                string bakFilename = filepath + "\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".sql";
                string runPath = "";
                if (System.Configuration.ConfigurationManager.AppSettings["BackupRunPath"].ToString() != "")
                {
                    runPath = System.Configuration.ConfigurationManager.AppSettings["BackupRunPath"].ToString();
                }
                using (Process p = new Process())
                {
                    p.StartInfo.FileName = "cmd.exe";// "C:\\Program Files\\MySQL\\MySQL Server 5.0\\bin\\mysql.exe";
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.RedirectStandardInput = true;
                    p.StartInfo.RedirectStandardOutput = true;
                    p.StartInfo.RedirectStandardError = true;
                    p.StartInfo.CreateNoWindow = true;

                    //string cmd = string.Format(@"mysql -hmysql104.mysite4now.com -unicklu2 -p1234qwer nicklu2 -B -e ""select product_serial_no from tb_product;"" > {1}",
                    //sql, filename);
                    //throw new Exception(sql);
                    //string cmd = string.Format(@"mysql -h{0} -u{1} -p{2} {3} -B -e ""{4};"" > {5}", "localhost", "root", "1234qwer", "qstore", GetSqlByTableName(dr[0].ToString()), bakFilename);
                    string cmd = string.Format(@"mysqldump -h{0} -u{1} -p{2}  {3} > {4}", "localhost", "root", "1234qwer", "QStore", bakFilename);
                    //cmd= "mysql -hmysql104.mysite4now.com -unicklu2 -p1234qwer nicklu2 -B -e \"select * from tb_other_inc_match_lu_sku;\" > C:\\\\lu_watch_soft_tmp\\\\download\\\\db_other_inc_match_lu_sku\\\\20090112013505.csv";
                    //throw new Exception(cmd);
                    p.Start();


                    p.StandardInput.WriteLine("cd " + runPath);
                    p.StandardInput.WriteLine(cmd);

                    p.StandardInput.WriteLine("exit");
                    string output = p.StandardOutput.ReadToEnd();
                    p.Close();
                    UpdateToRemote();
                    Application.Exit();
                    Application.ExitThread();
                }
            }));
        }

        void UpdateToRemote()
        {
            //try
            //{
            //    ProductModel[] pms = ProductModel.FindAll();
            //    string mdbfile = Application.StartupPath + "\\qiozikkw.mdb";
            //    ////
            //    //// 直接更新DB
            //    //db.SqlExec.RemoteExecuteNonQuery("Delete from tb_kkw");
            //    //foreach (var p in pms)
            //    //{
            //    //    string name = p.p_name;
            //    //    if (name.IndexOf("'") > -1)
            //    //        name = name.Replace("'", "");
            //    //    db.SqlExec.RemoteExecuteNonQuery(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity) values ('{0}', '{1}', '{2}')"
            //    //        , p.p_code, p.p_name, p.p_quantity));
            //    //}

            //    //
            //    // 直接更新DB
            //    db.SqlExec.RemoteExecuteNonQuery("Delete from tb_kkw");
            //    System.Text.StringBuilder sb = new StringBuilder();

            //    //foreach (var p in pms)
            //    //{
            //    //    string name = p.p_name;
            //    //    if (name.IndexOf("'") > -1)
            //    //        name = name.Replace("'", "");
            //    //    byte[] by = Encoding.GetEncoding("utf-8").GetBytes(name);// System.Text.ASCIIEncoding.UTF8.
            //    //    string n = Encoding.GetEncoding("utf-8").GetString(by);
            //    //    sb.Append(string.Format(",('{0}', '{1}', '{2}')", p.p_code, n, p.p_quantity));

            //    //}
            //    //if (sb.ToString().Length > 0)
            //    //{
            //    //    db.SqlExec.RemoteExecuteNonQuery(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity) values {0}"
            //    //       , sb.ToString().Substring(1) + ";"));
            //    //}

            //    using (OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile)))
            //    {

            //        OleDbCommand cmd = new OleDbCommand("delete from tb_kkw", conn);
            //        cmd.CommandTimeout = 18000;
            //        conn.Open();
            //        cmd.ExecuteNonQuery();
            //        conn.Close();
            //        conn.Dispose();

            //    }
            //    foreach (var p in pms)
            //    {
            //        if (p.showit == false) continue;
            //        using (OleDbConnection conn = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + mdbfile)))
            //        {

            //            OleDbCommand cmd = new OleDbCommand(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity) values ('{0}', '{1}', {2})"
            //            , p.p_code, p.p_name, p.p_quantity), conn);
            //            cmd.CommandTimeout = 18000;
            //            conn.Open();
            //            cmd.ExecuteNonQuery();
            //            conn.Close();
            //            conn.Dispose();

            //        }
            //    }



            //    //
            //    // ftp
            //    if (System.Configuration.ConfigurationManager.AppSettings["WebUpdate"].ToString() == "1")
            //    {
            //        //string path = Application.StartupPath + "\\txt";
            //        //if (Directory.Exists(path))
            //        //{
            //        //    Directory.Delete(path, true);
            //        //}
            //        //if (!Directory.Exists(path))
            //        //{
            //        //    Directory.CreateDirectory(path);
            //        //    Thread.Sleep(100);
            //        //}

            //        //foreach (var p in pms)
            //        //{
            //        //    string code = p.p_code;
            //        //    if (code.IndexOf("*") > -1)
            //        //    {
            //        //        code = code.Replace("*", "__");
            //        //    }
            //        //    if (p.p_code == null || p.p_code.Trim() == "") continue;
            //        //    StreamWriter sw = new StreamWriter(path + "\\" + p.p_code + ".txt", false, Encoding.UTF8);
            //        //    sw.WriteLine(string.Format("{0}", p.p_name));
            //        //    sw.Close();
            //        //}
            //        //
            //        // 上传
            //        Helper.FTPClient ftp = new Helper.FTPClient(
            //            System.Configuration.ConfigurationManager.AppSettings["ftp_server"].ToString()
            //            , System.Configuration.ConfigurationManager.AppSettings["ftp_uid"].ToString()
            //            , System.Configuration.ConfigurationManager.AppSettings["ftp_pwd"].ToString());
            //        ftp.Upload(mdbfile, "/web/kkw/txt/qiozikkw.mdb");
            //        //DirectoryInfo di = new DirectoryInfo(path);
            //        //FileInfo[] fis = di.GetFiles();
            //        //foreach (var f in fis)
            //        //{
            //        //    ftp.Upload(f.FullName, "/web/kkw/txt/" + f.Name);
            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Helper.Logs.WriteErrorLog(ex);
            //    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    this.Close();
            //}
        }

        public static string GetSqlByTableName(string table_name)
        {
            System.Text.StringBuilder sb = new StringBuilder();
            if (table_name.ToString().Length > 2)
            {
                DataTable dt = db.SqlExec.ExecuteDataTable("show fields from " + table_name);
                sb.Append("");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    sb.Append("," + dt.Rows[i]["field"].ToString());
                }
            }
            if (sb.ToString().Length > 2)
                return "select " + sb.ToString().Substring(1) + " from " + table_name;
            else
                return "";
        }
    }
}

