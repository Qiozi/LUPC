using KKWStore.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmYunUpdatePrice : Form
    {
        qstoreEntities context = new db.qstoreEntities();

        public frmYunUpdatePrice()
        {
            InitializeComponent();
            this.Shown += FrmYunUpdatePrice_Shown;
        }

        private void FrmYunUpdatePrice_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            this.listView1.Items.Clear();
            var query = context.tb_yun_stock_price.OrderBy(me => me.yun_name).ToList();
            foreach (var item in query)
            {
                var li = new ListViewItem(item.yun_code);
                li.SubItems.Add(item.yun_name);
                li.SubItems.Add(item.yun_price.ToString());
                li.SubItems.Add(item.yun_cost.ToString());
                listView1.Items.Add(li);
            }
            this.groupBox1.Text = string.Format("商品数量： " + query.Count.ToString() + ", 最后上传时间：" + (query.Count > 0 ? query[0].regdate.ToString("yyyy-MM-dd HH:mm:ss") : ""));
        }

        private void textBoxFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName);
                this.textBoxFile.Text = openFileDialog1.FileName;
                ReadFile();
            }
        }

        void ReadFile()
        {
            this.Cursor = Cursors.WaitCursor;
            var dt = new Helper.ExcelHelper(this.textBoxFile.Text).ExcelToDataTable();

            if (dt.Rows.Count > 3)
            {
                var query = context.tb_yun_stock_price.ToList();
                foreach (var item in query)
                {
                    query.Remove(item);
                }
            }
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                var yunName = dr["商品名称*"].ToString();
                var yunCode = dr["商品编码*"].ToString();
                decimal yunPrice = 0;
                decimal.TryParse(dr["商品价格"].ToString(), out yunPrice);
                decimal cost = 0M;
                decimal.TryParse(dr["成本价"].ToString(), out cost);

                var newModel = new db.tb_yun_stock_price
                {
                    regdate = DateTime.Now,
                    staff_id = Helper.Config.CurrentUser.id,
                    staff_name = Helper.Config.CurrentUser.user_name,
                    yun_code = yunCode,
                    yun_cost = cost,
                    yun_name = yunName,
                    yun_price = yunPrice
                };
                context.tb_yun_stock_price.Add(newModel);
            }
            context.SaveChanges();
            BindList();
            this.Cursor = Cursors.Default;
        }
    }
}
