using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class frmResetProfitToBalanceCash : Form
    {
        public frmResetProfitToBalanceCash()
        {
            InitializeComponent();
            this.dateTimePicker1.Value = DateTime.Now;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            var context = new db.qstoreEntities();
            var date = DateTime.Parse(this.dateTimePicker1.Value.ToString("yyyy-MM-01"));

            var balance = context.tb_balance_cash_record
                                 .Where(p => p.CreatorId.Equals(Helper.Config.SysAdminId) &&
                                        p.PayNote.StartsWith("系统自动计算") &&
                                        p.PayDate.Equals(date)).ToList();
            foreach (var item in balance)
            {
                context.tb_balance_cash_record.Remove(item);
            }
            context.SaveChanges();

            DateTime currDate = DateTime.Parse(date.ToString("yyyy-MM-01"));
            Helper.BalanceHelper.StatBalance(context, date, true);

            MessageBox.Show(string.Concat("现金流里 ", date.ToString("yyyy-MM"), " 利润已重置。 "));
            this.Close();
        }
    }
}
