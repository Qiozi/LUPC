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
    public partial class frmReadExcel : Form
    {
        public frmReadExcel()
        {
            InitializeComponent();
        }

        private void textBox1_DoubleClick(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.qstoreEntities db = new KKWStore.db.qstoreEntities();
            var dt = Util.HSSFExcel.ToDataTable(this.textBox1.Text);
            var count = 0;
            foreach(DataRow dr in dt.Rows)
            {
                var code = dr["Column1"].ToString();
                var yunCode = dr["Column2"].ToString();
                if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(yunCode))
                {
                    var prod = db.tb_product.SingleOrDefault(me => me.p_code.Equals(code));
                    if (prod != null)
                    {
                        prod.yun_code = yunCode.Trim();
                        db.SaveChanges();
                        count++;
                    }
                }
            }
            MessageBox.
                Show(count.ToString());
        }
    }
}
