using System;
using System.Windows.Forms;
using System.IO;

namespace RemoteAutoBackup
{
    public partial class Form1 : Form
    {
        string _storePath = "C:\\Workspaces\\DBBackup\\";

        public Form1()
        {
            InitializeComponent();
            this.Shown += new EventHandler(Form1_Shown);
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            BackupDB();
            this.Close();
        }

        private void BackupDB()
        {            
           var Info = new System.Diagnostics.ProcessStartInfo();

            string dumpFilename = @"C:\Program Files (x86)\MySQL\MySQL Server 5.0\bin\mysqldump.exe";

            if (!File.Exists(dumpFilename))
            {
                MessageBox.Show("mysqldump.exe 文件不存在");
                return;
            }
            if (File.Exists(_storePath + "nicklu2.sql"))
            {
                File.Delete(_storePath + "nicklu2.sql");
            }
            Info.FileName = dumpFilename;

            Info.Arguments = "--host=localhost --user=root --password=1234qwer nicklu2 -r \"" + _storePath + "nicklu2.sql\"";
            System.Diagnostics.Process proc;
            try
            {
                proc = System.Diagnostics.Process.Start(Info);
                proc.WaitForExit();

                File.Move(_storePath + "nicklu2.sql", _storePath + (DateTime.Now.ToString("nicklu2-yyyy-MM-dd")) + ".sql");

                DeleteFile(15);

                File.Delete(_storePath + "nicklu2.sql");

                EmailHelper.Send("wu.th@qq.com", "LUComputer website data backup OK", "LUComputer website data backup OK");
            }
            catch (System.ComponentModel.Win32Exception ex)
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
                    File.Delete(f.FullName);
                }
            }
        }
    }
}
