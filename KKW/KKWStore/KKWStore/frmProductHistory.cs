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
    public partial class frmProductHistory : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        string _P_Code = null;

        public frmProductHistory(string p_code)
        {
            _P_Code = p_code;
            InitializeComponent();
            this.Shown += new EventHandler(frmProductHistory_Shown);
        }

        void frmProductHistory_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            DataTable dt = db.OutInvoiceProductModel_p.GetProductOutHistory(_P_Code);
            this.listView1.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem li = new ListViewItem(dr["staff"].ToString());
                li.Tag = dr["invoice_code"].ToString();
                li.SubItems.Add(dr["input_regdate"].ToString());
                li.SubItems.Add(dr["receiverName"].ToString());
                li.SubItems.Add(dr["invoice_code"].ToString());
                li.SubItems.Add(dr["note"].ToString());
                li.SubItems.Add(dr["summary"].ToString());
                li.SubItems.Add(dr["pay_method"].ToString());
                li.SubItems.Add(dr["Tid"].ToString());
                listView1.Items.Add(li);
            }
            this.toolStripStatusLabel1.Text = string.Format("合计： {0} 条纪录", dt.Rows.Count.ToString());
            if (_P_Code == null)
            {
                var pm = db.ProductModel_p.GetModelByCode(context, _P_Code);
                this.toolStripLabel1.Text = pm.p_name;
            }
            else
                this.toolStripLabel1.Text = "";
        }
    }
}
