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
    public partial class frmOutNoScan : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmOutNoScan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string sn = textBox1.Text.Trim();

            int pid = db.SerialNoAndPCodeModel_p.GetProductId(context, sn);
            if (pid >0 )
            {
                //ProductModel pm = ProductModel.GetProductModel(pid);
                var pm = context.tb_product.Single(p => p.id.Equals(pid));

                if(MessageBox.Show(string.Format("你是否需要出库\r\n条码：{0} \r\n编号：{1} \r\n名称：{2}", sn, pm.p_code, pm.p_name), "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    if (db.SerialNoAndPCodeModel_p.AutoOut2(context, sn))
                    {
                        MessageBox.Show("出库成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        textBox1.Text = "";
                    }
                    else
                        MessageBox.Show("失败，也许条形码不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("失败，也许条形码不存在", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
