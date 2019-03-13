using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace KKWStore.Helper
{
    public class ExportExcel
    {
        public ExportExcel() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lv"></param>
        /// <param name="filename"></param>
        public static void Export(ListView lv, string filename)
        {
            // string[] columnsName = new string[] { "编号", "产品名称", "进价", "库存" };
            //string[] fieldsName = new string[] { "sys_user_name", "summary", "regdate" };


            if (lv.Items.Count == 0)
            {
                MessageBox.Show("没有需要导出的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = new DataTable();
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                dt.Columns.Add(lv.Columns[i].Text);
            }

            foreach (ListViewItem li in lv.Items)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    dr[i] = li.SubItems[i].Text;
                }
                dt.Rows.Add(dr);

            }
            Helper.NPOIExcel.ToExcel(dt, filename + DateTime.Now.ToString("yyyy-MM-dd"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\" + filename + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");

            MessageBox.Show("数据已生成在桌面", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        public static void Export(DataGridView dgv, string filename)
        {
            // string[] columnsName = new string[] { "编号", "产品名称", "进价", "库存" };
            //string[] fieldsName = new string[] { "sys_user_name", "summary", "regdate" };


            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("没有需要导出的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DataTable dt = new DataTable();
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                dt.Columns.Add(dgv.Columns[i].HeaderText);
            }

            foreach (DataGridViewRow li in dgv.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    dr[i] = li.Cells[i].Value;
                }
                dt.Rows.Add(dr);

            }
            Helper.NPOIExcel.ToExcel(dt, filename + DateTime.Now.ToString("yyyy-MM-dd"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\" + filename + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");

            MessageBox.Show("数据已生成在桌面", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
