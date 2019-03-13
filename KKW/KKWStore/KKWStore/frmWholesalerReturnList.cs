using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmWholesalerReturnList : Form
    {

        enums.ReturnFindType  _returnFindType = enums.ReturnFindType.In;

        public frmWholesalerReturnList()
        {
            InitializeComponent();

            this.Shown += new EventHandler(frmWholesalerReturnList_Shown);
        }

        void frmWholesalerReturnList_Shown(object sender, EventArgs e)
        {
            BindWholesaler();
            BindProductCode();
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }

        void BindWholesaler()
        {
            List<string> dt = db.ProductModel_p.GetBrand();

            comboBox1.Items.Clear();
            for (int i = 0; i < dt.Count; i++)
                comboBox1.Items.Add(dt[i]);
        }

        void BindProductCode()
        {
            List<string> dt = db.ProductModel_p.GetProdCodes();

            this.comboBoxProdCode.Items.Clear();
            for (int i = 0; i < dt.Count; i++)
                comboBoxProdCode.Items.Add(dt[i]);
        }

        void BindList()
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();
            DataTable dt = new DataTable();

            if (_returnFindType == enums.ReturnFindType.In) // 进货
            {
                dt = db.InInvoiceProductModel_p.GetInStoreHistory(dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text, comboBoxProdCode.Text);

               
            }
            else if(_returnFindType == enums.ReturnFindType.Return) // 退货
            {
                dt = db.ReturnWholesalerDetailModel_p.GetReturnHistoryList(dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text, comboBoxProdCode.Text);

            }
            else if (_returnFindType == enums.ReturnFindType.Out)
            {
                dt = db.SerialNoAndPCodeModel_p.GetOutList(dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text, comboBoxProdCode.Text);

            }

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                listView1.Columns.Add(dt.Columns[i].ColumnName, 150);
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem li = new ListViewItem(dt.Rows[i][0].ToString());
                for (int j = 1; j < dt.Columns.Count; j++)
                {
                    li.SubItems.Add(dt.Rows[i][j].ToString());

                }
                listView1.Items.Add(li);
            }

            toolStripStatusLabelNote.Text = (_returnFindType == enums.ReturnFindType.In ? "进货" : (_returnFindType == enums.ReturnFindType.Out ? "出库" : "退货")) + " : " + dt.Rows.Count.ToString() + " 条";
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            BindList();
        }

        private void radioButtonIn_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonOut_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void buttonAddReturn_Click(object sender, EventArgs e)
        {
            frmWholesalerReturnEdit fwre = new frmWholesalerReturnEdit("");
            fwre.StartPosition = FormStartPosition.CenterParent;
            fwre.ShowDialog();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                if (listView1.SelectedItems.Count == 1)
                {
                    string code = listView1.SelectedItems[0].SubItems[0].Text;

                    if (_returnFindType == enums.ReturnFindType.In)
                    {
                        frmWholesalerReturnDetailIn f = new frmWholesalerReturnDetailIn(enums.ReturnFindType.In, dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text, code);
                        f.StartPosition = FormStartPosition.CenterParent;
                        f.ShowDialog();
                    }
                    else if ( _returnFindType == enums.ReturnFindType.Return)
                    {
                        frmWholesalerReturnEdit fwre = new frmWholesalerReturnEdit(code);
                        fwre.StartPosition = FormStartPosition.CenterParent;
                        fwre.ShowDialog();
                    }
                    else if (_returnFindType == enums.ReturnFindType.Out)
                    {
                        frmWholesalerReturnDetailIn f = new frmWholesalerReturnDetailIn(enums.ReturnFindType.Out, dateTimePicker1.Value, dateTimePicker2.Value, comboBox1.Text, code);
                        f.StartPosition = FormStartPosition.CenterParent;
                        f.ShowDialog();
                    }
                }
            }
        }

        private void radioButtonIn_Click(object sender, EventArgs e)
        {
            radioButtonReturn.Checked = !radioButtonIn.Checked;
            radioButton_out.Checked = !radioButtonIn.Checked;
           
            _returnFindType = enums.ReturnFindType.In;
            BindWholesaler();
        }

        private void radioButtonOut_Click(object sender, EventArgs e)
        {
            radioButtonIn.Checked = !radioButtonReturn.Checked;
            radioButton_out.Checked = !radioButtonReturn.Checked;
           
            _returnFindType = enums.ReturnFindType.Return;
            BindWholesaler();
        }

        private void radioButton_out_Click(object sender, EventArgs e)
        {
            radioButtonReturn.Checked = !radioButton_out.Checked;
            radioButtonIn.Checked = !radioButton_out.Checked;

            _returnFindType = enums.ReturnFindType.Out;
            BindWholesaler();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            KKWStore.Helper.ExportExcel.Export(listView1, (_returnFindType == enums.ReturnFindType.In ? "进货" : (_returnFindType == enums.ReturnFindType.Out ? "出库" : "退货")));
           
        }
    }
}
