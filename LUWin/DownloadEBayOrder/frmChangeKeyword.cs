using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DownloadEBayOrder
{
    public partial class frmChangeKeyword : Form
    {
        nicklu2Entities context = new nicklu2Entities();
        public frmChangeKeyword()
        {
            InitializeComponent();
            this.Shown += frmChangeKeyword_Shown;
        }

        void frmChangeKeyword_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.textBoxOld.Text) && !string.IsNullOrEmpty(this.textBoxNew.Text))
            {
                var model = new tb_keyword_change
                {
                    NewWord = this.textBoxNew.Text,
                    OldWord = this.textBoxOld.Text,
                    Regdate = DateTime.Now
                };
                //context.AddTotb_keyword_change(model);
                context.tb_keyword_change.Add(model);
                context.SaveChanges();
                //MessageBox.Show("OK");
                BindList();
            }
            else
            {
                MessageBox.Show("Null");
            }
        }

        void BindList()
        {
            this.listView1.Items.Clear();
            var query = context.tb_keyword_change.OrderByDescending(p => p.Id);
            foreach (var item in query)
            {
                var lv = new ListViewItem(item.Id.ToString());
              
                lv.SubItems.Add(item.OldWord);
                lv.SubItems.Add(item.NewWord);
                this.listView1.Items.Add(lv);
            }
        }
    }
}
