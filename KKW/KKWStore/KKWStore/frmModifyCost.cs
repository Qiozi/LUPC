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
    public partial class frmModifyCost : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        int _pid = 0;
        public frmModifyCost(int pid)
        {
            _pid = pid;
            InitializeComponent();

            this.Shown += new EventHandler(frmModifyCost_Shown);
        }

        void frmModifyCost_Shown(object sender, EventArgs e)
        {
            if (_pid < 1)
            {
                MessageBox.Show("参数错误", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pm = context.tb_product.Single(p => p.id.Equals(_pid));// ProductModel.GetProductModel(_pid);
            txt_Code.Text = pm.p_code;
            txt_Name.Text = pm.p_name;

            BindList();
        }

        void BindList()
        {
            DataTable dt = db.InInvoiceProductModel_p.GetInvoiceForModifyCost(_pid);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                comboBox1.Items.Add(string.Format("{0}-- 编号：{1} 日期：{2} (￥{3})"
                    , dt.Rows[i]["in_invoice_code"].ToString()
                    , dt.Rows[i]["p_code"].ToString()
                    , dt.Rows[i]["regdate"].ToString()
                    , dt.Rows[i]["cost"].ToString()
                    ));
            }

        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            string in_invoice_code = this.comboBox1.Text.Substring(0, 14).Replace("--", "");
            if (db.InInvoiceProductModel_p.ModifyCostByInvoiceCode(in_invoice_code, numericUpDown1.Value, _pid.ToString(), checkBox1.Checked))
            {
                MessageBox.Show("修改完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("修改失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cbb = sender as ComboBox;
            if (cbb != null && cbb.Items.Count > 0)
            {
                decimal v;
                decimal.TryParse(cbb.Text.Split(new char[] { '￥', ')' })[1].ToString(), out v);
                this.numericUpDown1.Value = v;
                buttonModify.Enabled = true;
            }
            else
                this.numericUpDown1.Value = 0M;
        }
    }
}
