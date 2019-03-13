using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace KKWStoreBackup
{
    public partial class Form1 : Form
    {
        bool isLoad = System.Configuration.ConfigurationManager.AppSettings["isLocal"].ToString() == "1";
        string _storePath = "";
        public Form1()
        {
            InitializeComponent();
            _storePath = isLoad ? Application.StartupPath : "C:\\Users\\Administrator\\Desktop\\backupapp\\";
            this.Shown += new EventHandler(Form1_Shown);
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["autoRun"].ToString() == "1")
            {
                button1_Click(null, null);
                Application.Exit();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();


            string dumpFilename = isLoad ? @"C:\Program Files\MySQL\MySQL Server 5.0\bin\mysqldump.exe" :
                @"C:\Program Files\MySQL\MySQL Server 5.6\bin\mysqldump.exe";

            if (!File.Exists(dumpFilename))
            {
                MessageBox.Show("mysqldump.exe 文件不存在");
                return;
            }
            if (File.Exists(Application.StartupPath + "\\kkw.sql"))
                File.Delete(Application.StartupPath + "\\kkw.sql");

            Info.FileName = dumpFilename;
            if (isLoad)
            {
                Info.Arguments = "--host=localhost --user=root --password=1234qwer qstore -r \"" + Application.StartupPath + "\\kkw.sql\"";
            }
            else
            {
                Info.Arguments = "--host=localhost --user=qiozi --password=qiozi@msn.com1 qstore -r \"" + _storePath + "\\kkw.sql\"";
            }
            System.Diagnostics.Process proc;
            try
            {
                proc = System.Diagnostics.Process.Start(Info);
                proc.WaitForExit();

                Util.T7zip.T7zipTools.ZipExeFullname = Application.StartupPath + "\\7za.exe";

                if (File.Exists(Application.StartupPath + "\\kkw.7z"))
                {
                    File.Delete(Application.StartupPath + "\\kkw.7z");
                }

                Util.T7zip.T7zipTools.ZipFile(Application.StartupPath + "\\kkw.sql", Application.StartupPath + "\\kkw.7z");

                if (File.Exists(_storePath + (DateTime.Now.ToString("kkw-yyyy-MM-dd")) + ".7z"))
                {
                    File.Delete(_storePath + (DateTime.Now.ToString("kkw-yyyy-MM-dd")) + ".7z");
                }

                File.Move(Application.StartupPath + "\\kkw.7z", _storePath + (DateTime.Now.ToString("kkw-yyyy-MM-dd")) + ".7z");

                if (!isLoad)
                    DeleteFile(20);

                File.Delete(Application.StartupPath + "\\kkw.sql");

                EmailHelper.Send("371844531@QQ.COM", _storePath + (DateTime.Now.ToString("kkw-yyyy-MM-dd")) + ".7z", "酷可维数据备份" + DateTime.Now.ToString());
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog(ex);
                return;
            }
        }
        void DeleteFile(int dayCount)
        {
            DirectoryInfo dir = new DirectoryInfo(_storePath);
            FileInfo[] files = dir.GetFiles();
            foreach (var f in files)
            {
                if (DateTime.Now.DayOfYear - f.CreationTime.DayOfYear > dayCount)
                {
                    if (f.Name.Contains("kkw-"+DateTime.Now.Year.ToString()))
                    {
                        File.Delete(f.FullName);
                    }
                }
            }
        }
    }
}
