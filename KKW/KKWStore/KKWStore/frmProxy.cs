using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class frmProxy : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmProxy()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmUploadSale_Shown);
        }

        void frmUploadSale_Shown(object sender, EventArgs e)
        {
        }  

        private void button1_Click(object sender, EventArgs e)
        {

            string fileFullname = textBox1.Text.Trim();
            if (!string.IsNullOrEmpty(fileFullname))
            {
                if (fileFullname.Substring(fileFullname.Length - 3).ToString() == "csv")
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(fileFullname);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("日期");
                    dt.Columns.Add("数量");
                    dt.Columns.Add("金额");

                    string[] rows = sr.ReadToEnd().Split(new char[] { '\n' });
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (i == 0) continue;
                        string s = rows[i];

                        if (s.IndexOf(",") > -1)
                        {
                            string[] sArray = s.Split(new char[] { ',' });
                            string date = sArray[0].Trim();
                            string qty = sArray[1].Trim();
                            string total = sArray[2].Trim();
                            int quantity;
                            int.TryParse(qty, out quantity);
                            decimal grandTotal;
                            decimal.TryParse(total, out grandTotal);
                            DateTime dtDate;
                            DateTime.TryParse(date, out dtDate);

                            if (!string.IsNullOrEmpty(date) && !string.IsNullOrEmpty(qty) && !string.IsNullOrEmpty(total))
                            {
                                //ProxyModel.DeleteAll("SaleDate='" + date + "'");
                                DateTime de;
                                DateTime.TryParse(date, out de);
                                int year = de.Year;
                                int month = de.Month;
                                int day = de.Day;
                                var query = context.tb_proxy.Where(p=>p.SaleDate.HasValue && p.SaleDate.Value.Year .Equals(year) &&
                                    p.SaleDate.Value.Month.Equals(month) && p.SaleDate.Value.Day.Equals(day)).ToList();
                                foreach (var item in query)
                                {
                                    context.tb_proxy.Remove(item);
                                }
                                
                                var stm = new db.tb_proxy();// new ProxyModel();
                                stm.SaleDate = dtDate;
                                stm.SaleTotal = grandTotal;
                                stm.SaleQuantity = quantity;
                                stm.Regdate = DateTime.Now;
                                context.tb_proxy.Add(stm);
                                context.SaveChanges();

                                DataRow dr = dt.NewRow();
                                dr["日期"] = date;
                                dr["数量"] = qty;
                                dr["金额"] = total;
                                dt.Rows.Add(dr);
                            }
                        }
                        dataGridView1.DataSource = dt;

                    }
                }
            }
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }
    }
}