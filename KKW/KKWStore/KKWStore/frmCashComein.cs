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
    public partial class frmCashComein : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmCashComein()
        {
            InitializeComponent();
            this.Shown += FrmCashComein_Shown;
        }

        private void FrmCashComein_Shown(object sender, EventArgs e)
        {
            BindList();
        }

        void BindList()
        {
            this.listView1.Items.Clear();
            var query = context.tb_balance_cash_record
                            .Where(p => p.PayType.Equals((int)enums.PayType.Comein))
                            .OrderByDescending(p => p.PayDate)
                            .ToList();

            foreach (var item in query)
            {
                ListViewItem li = new ListViewItem(item.CreateTime.ToString("yyyy-MM-dd"));
                li.Tag = item.Id.ToString();
                li.SubItems.Add(item.PayDate.ToString("yyyy-MM-dd"));
                li.SubItems.Add("￥" + item.IncomeCash.ToString("0.00"));
                li.SubItems.Add(item.PayNote);
                listView1.Items.Add(li);
            }
            this.labelTotal.Text = query.Select(p => p.IncomeCash).Sum().ToString("￥#,###,###,##0.00");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNote.Text))
            {
                MessageBox.Show("备注不能为空");
                return;
            }
            if (this.numericUpDown1.Value < 1M)
            {
                MessageBox.Show("请输入金额");
                return;
            }

            if (DateTime.Now.ToString("dd") == "01")
            {
                MessageBox.Show("请误在本月1号输入进帐，1号为系统自动计算日期");
                return;
            }

            var balance = new db.tb_balance_cash_record
            {
                PayDate = DateTime.Now,// payCash > 0 ? currDate : DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")),
                CreatorId = Helper.Config.CurrentUser.id,
                CreateTime = DateTime.Now,
                IncomeCash = this.numericUpDown1.Value,
                AfterBalance = 0M,
                CreatorName = Helper.Config.CurrentUser.user_name,
                PayCash = 0M,
                PayNote = this.txtNote.Text.Trim(),
                PreBalance = 0M,
                IsExclude = true,
                PayType = (int)enums.PayType.Comein
            };
            context.tb_balance_cash_record.Add(balance);
            context.SaveChanges();
            MessageBox.Show("OK");
            this.txtNote.Text = "";
            this.numericUpDown1.Value = 0M;
            this.BindList();
        }
    }
}
