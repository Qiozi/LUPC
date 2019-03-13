using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmSupplierSelected : Form
    {
        public frmSupplierSelected()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmSupplierSelected_Shown);
        }

        void frmSupplierSelected_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            List<Supplier> supps = db.InInvoiceModel_p.GetSupplierList();
            listView1.Items.Clear();
            foreach (var u in supps)
            {
                ListViewItem li = new ListViewItem(u.Name);
                li.SubItems.Add(u.Cost);
                listView1.Items.Add(li);
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                
                Helper.TempInfo.Supplier = listView1.SelectedItems[0].Text;
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
            else
                MessageBox.Show("请选择供货商", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
    [Serializable]
    public class Supplier
    {
        public string Name { get; set; }
        public string Cost { set; get; }
    }
}
