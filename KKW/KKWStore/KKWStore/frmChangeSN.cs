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
    public partial class frmChangeSN : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        public frmChangeSN()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmChangeSN_Shown);
        }

        void frmChangeSN_Shown(object sender, EventArgs e)
        {
            BindProductCode();
        }

        void BindProductCode()
        {
            //DataTable dt = db.SqlExec.ExecuteDataTable("select distinct p_code from tb_product where p_code <> '' order by p_code asc ");
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    this.comboBox1.Items.Add(dt.Rows[i][0].ToString());
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string sn = textBox1.Text.Trim();
            string oldCode = textBox2.Text.Trim();
            string newCode = comboBox1.Text.Trim();

            if (sn != "" && oldCode != "" && newCode != "")
            {

                var newCodeProd = db.ProductModel_p.GetModelByCode(context, comboBox1.Text.Trim());

                if (newCodeProd == null)
                {
                    MessageBox.Show("找不到新的商品编号");
                    return;
                }
                var scrm = new db.tb_sn_change_record();// new SnChangeRecordModel();
                scrm.SerialNo = sn;
                scrm.OldProductCode = textBox2.Text.Trim();
                scrm.NewProductCode = comboBox1.Text.Trim();
                scrm.Regdate = DateTime.Now;
                context.tb_sn_change_record.Add(scrm);
                context.SaveChanges();

                var snpm = context.tb_serial_no_and_p_code.Where(p => p.SerialNO.Equals(sn)).ToList();// db.SerialNoAndPCodeModel_p.FindAllByProperty("SerialNO", sn);
                if (snpm.Count > 0)
                {
                    snpm[0].p_code = newCode;
                    snpm[0].p_id = newCodeProd.id;
                    snpm[0].in_cost = newCodeProd.p_cost;
                    context.SaveChanges();

                    textBox2.Text = "";
                    textBox1.Text = "";
                    db.ProductModel_p.ChangeQuantity(context, oldCode);
                    db.ProductModel_p.ChangeQuantity(context, newCode);
                    MessageBox.Show("序列号转化成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (oldCode != "" && newCode != "")
            {
                if (MessageBox.Show("您确认需要把两个产品合并为一么？将会删除其中一个编号，并合并到 " + newCode, "询问", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                {
                    var snpm = context.tb_serial_no_and_p_code.Where(p => p.p_code.Equals(oldCode));// db.SerialNoAndPCodeModel_p.FindAllByProperty("p_code", oldCode);
                    List<int> list = new List<int>();

                    foreach (var m in snpm)
                    {
                        list.Add(m.id);
                    }

                    for (int i = 0; i < list.Count; i++)
                    {
                        var newSnpm = context.tb_serial_no_and_p_code.SingleOrDefault(s => s.id.Equals(list[i]));// db.SerialNoAndPCodeModel_p.GetSerialNoAndPCodeModelcontext, (list[i]);
                        if (newSnpm != null)
                        {
                            newSnpm.p_code = newCode;
                            context.SaveChanges();
                        }
                    }

                    var pm = context.tb_product.Where(p => p.p_code.Equals(oldCode)).ToList();// ProductModel.FindAllByProperty("p_code", oldCode);

                    for (int i = 0; i < pm.Count; i++)
                    {
                        //pm[i].Delete();
                        context.tb_product.Remove(pm[i]);
                    }
                    context.SaveChanges();
                    db.ProductModel_p.ChangeQuantity(context, newCode);

                    textBox2.Text = "";
                    textBox1.Text = "";
                    MessageBox.Show("序列号转化成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            string sn = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(sn))
                return;
            var oldModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
            if (oldModel != null)
            {
                textBox2.Text = oldModel.p_code;
            }
            else
            {
                MessageBox.Show("此序列号不在数据库中", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
