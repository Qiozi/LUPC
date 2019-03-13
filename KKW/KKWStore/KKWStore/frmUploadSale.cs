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
    public partial class frmUploadSale : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmUploadSale()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmUploadSale_Shown);
        }

        void frmUploadSale_Shown(object sender, EventArgs e)
        {
            BindStoreName();
            this.dateTimePicker1.Value = DateTime.Now;
        }

        void BindStoreName()
        {
            string[] names = System.Configuration.ConfigurationManager.AppSettings["AppName"].ToString().Split(new char[] { '|' });
            foreach (var name in names)
            {
                comboBox1.Items.Add(name);
            }
        }

        void ShowLastRecord()
        {
            var query = context.tb_sales_total.OrderByDescending(p => p.ID).Take(1).ToList();
            if (query.Count == 1)
            {
                this.label1.Text = string.Concat(query[0].StoreName, ": ", query[0].SaleDate.ToString(), " -- ", query[0].SaleTotal.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string storeName = comboBox1.Text.ToString();
            buttonStoreName.Text = storeName;
            if (string.IsNullOrEmpty(storeName))
            {
                MessageBox.Show("请选择是哪个店的数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }
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
                                //SalesTotalModel.DeleteAll("SaleDate='" + date + "' and StoreName='"+ storeName +"'");
                                //SalesTotalModel stm = new SalesTotalModel();
                                DateTime de;
                                DateTime.TryParse(date, out de);
                                int year = de.Year;
                                int month = de.Month;
                                int day = de.Day;

                                var query = context.tb_sales_total.Where(p => p.SaleDate.HasValue &&
                                    p.SaleDate.Value.Year.Equals(year) && p.SaleDate.Value.Month.Equals(month) &&
                                    p.SaleDate.Value.Day.Equals(day) && p.StoreName.Equals(storeName)).ToList();
                                foreach (var item in query)
                                {
                                    context.tb_sales_total.Remove(item);
                                }
                                var stm = new db.tb_sales_total();
                                stm.SaleDate = dtDate;
                                stm.SaleTotal = grandTotal;
                                stm.SaleQuantity = quantity;
                                stm.Regdate = DateTime.Now;
                                stm.StoreName = storeName;
                                context.tb_sales_total.Add(stm);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            if (numericUpDownCDian.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = numericUpDownCDian.Value,
                    StoreName = "C 店"
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }
            if (numericUpDownJinDong.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = numericUpDownJinDong.Value,
                    StoreName = "京东商城"
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }
            if (numericUpDownTianMao.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = numericUpDownTianMao.Value,
                    StoreName = "天猫商城"
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }

            if (numericUpDownReturn.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = 0 - numericUpDownReturn.Value,
                    StoreName = "退款总额"
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }

            if (numericUpDownOther1.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = numericUpDownOther1.Value,
                    StoreName = textBoxOther1.Text.Trim()
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }

            if (numericUpDownOther2.Value > 0)
            {
                var model = new db.tb_sales_total
                {
                    Regdate = DateTime.Now,
                    SaleDate = dateTimePicker1.Value,
                    SaleQuantity = 1,
                    SaleTotal = numericUpDownOther2.Value,
                    StoreName = textBoxOther2.Text.Trim()
                };
                context.tb_sales_total.Add(model);
                context.SaveChanges();
            }

            this.Cursor = Cursors.Default;
            MessageBox.Show("OK");
            ShowLastRecord();
        }
    }
}
