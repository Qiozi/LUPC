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
    public partial class frmModifySNOfProd : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        int _inInvoiceID = 0;
        string _inInvoiceCode = "";
        string _currModifySKU = "";
        int _currInPordID = 0;
        public frmModifySNOfProd(int inInvoiceID)
        {
            _inInvoiceID = inInvoiceID;

            InitializeComponent();

            labelWarehouse.Text = enums.WarehouseType.显神仓库.ToString();

            this.Shown += new EventHandler(frmModifySNOfProd_Shown);
        }

        void frmModifySNOfProd_Shown(object sender, EventArgs e)
        {
            if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
            {
                MessageBox.Show("此界面，需要管理员权限");
                this.Close();
                return;
            }
            if (_inInvoiceID > 0)
            {
                BindProdList();
            }
        }

        void BindProdList()
        {
            var inInvoice = context.tb_in_invoice.Single(p => p.id.Equals(_inInvoiceID));// db.InInvoiceModel_p.GetInInvoiceModel(context, _inInvoiceID);

            if (inInvoice != null)
            {
                _inInvoiceCode = inInvoice.invoice_code;

                var ipList = context.tb_in_invoice_product.Where(p => p.in_invoice_code.Equals(inInvoice.invoice_code)).ToList();// InInvoiceProductModel.FindAllByProperty("in_invoice_code", inInvoice.invoice_code);// db.InInvoiceProductModel_p.g
                if (ipList != null)
                {
                    listViewPList.Items.Clear();
                    for (int i = 0; i < ipList.Count; i++)
                    {
                        var pm = context.tb_product.Single(p => p.id.Equals(ipList[i].p_id));// ProductModel.GetProductModel(ipList[i].p_id);

                        var li = new ListViewItem(ipList[i].p_code);
                        li.Tag = ipList[i].id;
                        li.SubItems.Add(pm != null ? pm.p_name : "");
                        li.SubItems.Add((ipList[i].cost.Value * ipList[i].quantity.Value).ToString("0.0"));
                        li.SubItems.Add(ipList[i].quantity.ToString());
                        li.SubItems.Add(ipList[i].cost.Value.ToString("0.0"));
                        listViewPList.Items.Add(li);
                    }
                }
            }
        }

        private void listViewPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            listViewSN.Items.Clear();
            ListView lv = listViewPList as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    if (!string.IsNullOrEmpty(_inInvoiceCode) && !string.IsNullOrEmpty(lv.SelectedItems[0].SubItems[0].Text.ToString()))
                    {
                        int.TryParse(lv.SelectedItems[0].Tag.ToString(), out  _currInPordID);

                        _currModifySKU = lv.SelectedItems[0].SubItems[0].Text;
                        labelCurrProductName.Text = lv.SelectedItems[0].SubItems[1].Text;

                        BindSN(lv.SelectedItems[0].SubItems[0].Text.ToString(), _inInvoiceCode);
                    }
                }
            }
        }

        void BindSN(string p_code, string inInvoiceCode)
        {

            DataTable singelDT = db.SerialNoAndPCodeModel_p.GetAllSNByProdInInvoice(p_code, inInvoiceCode);
            listViewSN.Items.Clear();
            for (int i = 0; i < singelDT.Rows.Count; i++)
            {
                DataRow dr = singelDT.Rows[i];
                ListViewItem li = new ListViewItem(dr["SerialNO"].ToString());
                li.Tag = dr["id"].ToString();

                li.SubItems.Add(dr["in_cost"].ToString());


                li.SubItems.Add(dr["regdate"].ToString());
                listViewSN.Items.Add(li);
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
                {
                    var pm = db.ProductModel_p.GetModelByCode(context, textBox1.Text.Trim());
                    if (pm != null)
                    {
                        labelNewProd.Text = pm.p_name;
                    }
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请输入新产品SKU", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
                return;

            string newSKU = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(newSKU))
            {
                MessageBox.Show("请输入新产品SKU", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(_currModifySKU))
            {
                MessageBox.Show("请选择需要修改的产品", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int mcount = 0;
            var pm = db.ProductModel_p.GetModelByCode(context, newSKU);
            if (pm == null)
            {
                MessageBox.Show("查无此产品: " + newSKU, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < listViewSN.Items.Count; i++)
            {
                int id = 0;
                int.TryParse(listViewSN.Items[i].Tag.ToString(), out id);

                if (pm != null)
                {
                    var snModel = context.tb_serial_no_and_p_code.Single(p => p.id.Equals(id));// SerialNoAndPCodeModel.GetSerialNoAndPCodeModel(id);
                    snModel.p_code = newSKU;
                    snModel.p_id = pm.id;
                    context.SaveChanges();
                    mcount++;
                }
            }

            var inpModel = context.tb_in_invoice_product.Single(p => p.id.Equals(_currInPordID));// InInvoiceProductModel.GetInInvoiceProductModel(_currInPordID);
            inpModel.p_code = newSKU;
            inpModel.p_id = pm.id;
            //inpModel.Update();
            context.SaveChanges();

            MessageBox.Show(mcount.ToString() + " 个条码修改完成.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindProdList();
            listViewSN.Items.Clear();
        }
    }
}
