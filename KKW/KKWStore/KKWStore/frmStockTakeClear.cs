using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{

    public partial class frmStockTakeClear : Form
    {
        public enum SnType
        {
            None,
            Normal,
            NotInStore,
            NotOutStore,
            NotExist
        }

        public class SnItem
        {

            public string SN { get; set; }

            public SnType SnType { get; set; }
        }

        public class ProdItem
        {
            public int Pid { get; set; }

            public List<string> Sns { get; set; }
        }

        db.qstoreEntities context = new db.qstoreEntities();

        public frmStockTakeClear()
        {
            InitializeComponent();
        }

        private void btnSureSave_Click(object sender, EventArgs e)
        {

        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(txtFolderPath.Text.Trim()))
            {
                var dir = new DirectoryInfo(txtFolderPath.Text.Trim());
                FileInfo[] fis = dir.GetFiles();
                var snAry = new List<string>();
                foreach (var file in fis)
                {
                    if (file.Extension.Equals(".xls"))
                    {
                        var list = ReadFile(file.FullName);
                        foreach (var s in list)
                        {
                            snAry.Add(s);
                        }
                    }
                }
                // Analyize(snAry);
                var result = new List<SnItem>();
                var allDBSn = (from p in context.tb_serial_no_and_p_code
                               where (!p.out_regdate.HasValue ||
                                (p.out_regdate.HasValue && p.out_regdate.Value.Year < 2000)) &&
                                p.curr_warehouse_id.HasValue && p.curr_warehouse_id.Value.Equals(1)
                               select new
                               {
                                   p.SerialNO
                               }).ToList();

                var allNotExist = new List<string>();
                foreach (var sn in allDBSn)
                {
                    if (!snAry.Contains(sn.SerialNO))
                    {
                        allNotExist.Add(sn.SerialNO);
                    }
                }

                foreach (var sn in snAry)
                {
                    var query = allDBSn.SingleOrDefault(p => p.SerialNO.Equals(sn));
                    result.Add(new SnItem
                    {
                        SN = sn,
                        SnType = query == null ? SnType.NotExist : SnType.Normal
                    });
                }

                this.label_allSNCount.Text = string.Concat("所有SN ", allDBSn.Count.ToString());
                this.label_no_use.Text = string.Concat("末使用 ", allDBSn.Where(s => s.SerialNO.Equals("1000000000")).Count());

                this.txtResultNormal.Text = string.Join("\r\n", result.Where(p => p.SnType.Equals(SnType.Normal)).Select(p => p.SN).ToList());
                this.label_normal.Text = string.Concat("正常:", result.Where(p => p.SnType.Equals(SnType.Normal)).ToList().Count.ToString());

                this.txtResultNoExist.Text = string.Join("\r\n", result.Where(p => p.SnType.Equals(SnType.NotExist)).Select(p => p.SN).ToList());
                this.label_no_exist.Text = string.Concat("不存在:", result.Where(p => p.SnType.Equals(SnType.NotExist)).ToList().Count.ToString());

                this.txtResultNoInStore.Text = string.Join("\r\n", allNotExist);
                this.label_no_in_store.Text = string.Concat("不在盘点中的：", allNotExist.Count.ToString());
            }
            else
            {
                MessageBox.Show("请选文件夹");
            }
        }

        void Analyize(List<string> sns)
        {
            var result = new List<ProdItem>();
            foreach (var sn in sns)
            {
                if (string.IsNullOrEmpty(sn))
                {
                    continue;
                }
                var snT = sn.Trim();
                var snModel = context.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(snT));
                if (snModel == null)
                {
                    db.SqlExec.ExecuteNonQuery(@"
insert into tb_serial_no_no_instore 
	(SerialNo, Regdate)
	values
	('" + snT + "', now())");
                }
                else
                {
                    var existModel = result.SingleOrDefault(p => p.Pid.Equals(snModel.p_id.Value));
                    if (existModel == null)
                    {
                        result.Add(new ProdItem()
                        {
                            Pid = snModel.p_id.Value,
                            Sns = new List<string>() { snT }
                        });
                    }
                    else
                    {
                        existModel.Sns.Add(snModel.SerialNO);
                    }
                }

            }
            foreach (var item in result)
            {
                ExecAnalyize(item);
            }
        }

        void ExecAnalyize(ProdItem prodItem)
        {
            var f = new frmCheckStoreEdit(0, prodItem.Sns);
            f.ShowDialog();
        }

        bool ValidNum(string txt)
        {
            var m = "0123456789";
            for (int i = 0; i < txt.Length; i++)
            {
                if (m.IndexOf(txt[i]) == -1)
                {
                    return false;
                }
            }
            return true;
        }

        List<string> ReadFile(string filename)
        {
            var result = new List<string>();
            var fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            var workbook = new HSSFWorkbook(fs);
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                var sheet = workbook.GetSheetAt(i);
                for (int j = 0; j <= sheet.LastRowNum; j++)  //LastRowNum 是当前表的总行数
                {
                    IRow row = sheet.GetRow(j);  //读取当前行数据
                    if (row != null)
                    {
                        for (int k = 0; k <= row.LastCellNum; k++)  //LastCellNum 是当前行的总列数
                        {
                            ICell cell = row.GetCell(k);  //当前表格
                            if (cell != null)
                            {
                                if (cell.ToString().Trim().Length == 10)
                                {
                                    if (ValidNum(cell.ToString().Trim()))
                                        result.Add(cell.ToString());   //获取表格中的数据并转换为字符串类型
                                }

                            }
                        }
                    }
                }
            }
            return result;
        }

        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.txtFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
