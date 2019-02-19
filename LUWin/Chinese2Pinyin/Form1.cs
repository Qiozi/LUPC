using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EasyFuncLib;

namespace Chinese2Pinyin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Text = "汉字转换成全拼";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataTable dt = HSSFExcel.ToDataTable(textBox1.Text);
            dt.Columns.Add("输入码");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                string name = dr[0].ToString();
                if (name.IndexOf('(') != -1)
                {
                    name = name.Split(new char[] { '(' })[0];
                }
                if (!string.IsNullOrEmpty(name))
                    dr[dt.Columns.Count - 1] = Filter(Chinese2PinYin.ConvertWithSplitChar(name, '|'));
                else
                    dr[dt.Columns.Count - 1] = "";
            }

            HSSFExcel.ToExcel(dt, "Sheet1", textBox1.Text.Replace(".xls", "OK.xls"));

            MessageBox.Show("OK");
        }

        string Filter(string name)
        {
            string s = name.Substring(0, 1);
            if (name.IndexOf("|") > -1)
            {
                s = "";
                string[] strs = name.Split(new char[] { '|' });
                for (int i = 0; i < strs.Length; i++)
                {
                    s += strs[i].Substring(0, 1);
                }
            }
            return s;
        }
    }
}
