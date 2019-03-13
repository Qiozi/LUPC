using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmPayMethodSelected : Form
    {
        public frmPayMethodSelected()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmPayMethodSelected_Shown);
        }

        void frmPayMethodSelected_Shown(object sender, EventArgs e)
        {
            BindList();
        }
        void BindList()
        {
            List<string> pays = db.InInvoiceModel_p.GetPayMethodList();
            listView1.Items.Clear();
            foreach (var s in pays)
            {
                ListViewItem li = new ListViewItem(s);
                listView1.Items.Add(li);
            }
        }
        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Helper.TempInfo.PayMethodText = listView1.SelectedItems[0].Text;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
                MessageBox.Show("请选择付款帐户", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
