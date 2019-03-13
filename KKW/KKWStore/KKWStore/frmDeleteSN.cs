using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmDeleteSN : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmDeleteSN()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sn = this.textBox1.Text.Trim();
            if (sn.Length == 0)
            {
                MessageBox.Show("请输入SN.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                try
                {
                    var model = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                    db.tb_product pm = null;
                    if (model != null)
                    {
                        pm = db.ProductModel_p.GetModelByCode(context, model.p_code);
                        if (MessageBox.Show("删除操作不可恢复，您确认删除此SN？\r\n编号：" + pm.p_code + "\r\n名称:" + pm.p_name + "\r\nSN:" + sn + "", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                        {
                            //model.Delete();
                            context.tb_serial_no_and_p_code.Remove(model);

                            db.ProductModel_p.ChangeQuantity(context, model.p_code, -1);

                            var sndhm = new db.tb_serial_no_delete_history();
                            sndhm.p_code = model.p_code;
                            sndhm.SerialNo = sn;
                            sndhm.regdate = DateTime.Now;
                            context.tb_serial_no_delete_history.Add(sndhm);
                            context.SaveChanges();

                            MessageBox.Show("成功删除SN", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.textBox1.Text = "";
                            this.textBox1.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("此SN不存在.请重新输入", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.textBox1.Text = "";
                        this.textBox1.Focus();
                    }
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string text = this.textBox1.Text;
            this.textBox1.Text = Helper.CharacterHelper.ToDBC(text);
        }
    }
}
