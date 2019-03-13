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
    public partial class frmReturnEdit : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmReturnEdit()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmReturnEdit_Shown);
        }

        void frmReturnEdit_Shown(object sender, EventArgs e)
        {
            this.textBox1.Focus();
            this.txt_staff.Text = Helper.Config.CurrentUser.user_name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] sns = this.textBox1.Text.Trim().Replace("\r\n", ",").Split(new char[] { ',' });

            List<string> UsedList = new List<string>();

            foreach (var s in sns)
            {
                if (s.Trim() == "") continue;
                string sn = s.Trim().Replace("A", "");

                var snpm = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                if (snpm != null)
                {
                    int year = snpm.out_regdate.Value.Year;
                    if (year > 2000)
                    {
                        snpm.out_regdate = DateTime.MinValue;
                        snpm.is_return = true;
                        context.SaveChanges();

                        var oipm = db.OutInvoiceProductModel_p.GetModelBySerialNO(context, sn);
                        if (oipm != null)
                        {

                            var oi = context.tb_out_invoice.Single(p => p.id.Equals(oipm.out_invoice_id));// OutInvoiceModel.GetOutInvoiceModel(oipm.out_invoice_id);
                            oi.SN_Quantity = oi.SN_Quantity - 1;

                            db.ProductModel_p.ChangeQuantity(context, snpm.p_code);   // 原库存+1
                            var rhm = new db.tb_return_history();// new ReturnHistoryModel();
                            rhm.out_invoice_code = oipm.out_invoice_code;
                            rhm.out_invoice_id = oipm.out_invoice_id.ToString();
                            rhm.SerialNo = sn;
                            rhm.p_id = snpm.p_id;
                            rhm.p_code = snpm.p_code;
                            rhm.staff = this.txt_staff.Text;
                            rhm.return_regdate = DateTime.Now;
                            context.tb_return_history.Add(rhm);
                            context.tb_out_invoice_product.Remove(oipm);
                            context.SaveChanges();
                            // oipm.Delete();  // 在出库中删除
                        }
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("{0} 此号不存在", sn), "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.Close();
        }

        private void btn_select_user_Click(object sender, EventArgs e)
        {
            frmStaffSelected fss = new frmStaffSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            fss.ShowDialog();
            this.txt_staff.Text = Helper.TempInfo.StaffName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text.Replace("\r\n", ",");
            TB.Text = Helper.CharacterHelper.ToDBC(text).Replace(",", "\r\n");
        }
    }
}
