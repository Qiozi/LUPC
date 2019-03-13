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
    public enum BalanceRecordType
    {
        All = 0,
        Pay = 1,
        Income = 2,
        Borrow = 3,
        /// <summary>
        /// 管理员操作
        /// </summary>
        Sys
    }

    public partial class frmBalanceCashRecord : Form
    {
        bool _isInputDate = true;
        db.qstoreEntities context = new db.qstoreEntities();
        List<db.tb_balance_cash_record> query;
        public frmBalanceCashRecord()
        {
            InitializeComponent();
            this.Shown += frmBalanceCashRecord_Shown;
        }

        void frmBalanceCashRecord_Shown(object sender, EventArgs e)
        {
            this.labelSearch.Text = "";
            InitData();
        }

        void InitData()
        {
            //if (balanceRecordType == BalanceRecordType.Borrow)
            //{
            //    query = context.tb_balance_cash_record.Where(p => p.IsExclude.Equals(false) &&
            //       p.PayCash > 0M).OrderByDescending(p => p.PayDate)
            //   .ThenByDescending(p => p.Id).ToList();
            //}
            //else if (balanceRecordType == BalanceRecordType.Income)
            //{
            //    query = context.tb_balance_cash_record.Where(p => !p.IncomeCash.Equals(0M)).OrderByDescending(p => p.PayDate)
            //  .ThenByDescending(p => p.Id).ToList();
            //}
            // else if (balanceRecordType == BalanceRecordType.Pay)
            {
                var currDate = this.dateTimePicker1.Value.Date;
                var endDate = this.dateTimePicker2.Value.Date.AddDays(1).AddMilliseconds(-1);

                var key = txtNote.Text.Trim();

                if (_isInputDate)
                {
                    query = context.tb_balance_cash_record.Where(p => p.IsExclude.Equals(true) && 
                            p.PayType.Equals((int)enums.PayType.PayJinHuo) &&
                            p.CreateTime >= currDate &&
                            p.CreateTime <= endDate &&
                            p.PayNote.Contains(key)).ToList();
                   
                }
                else
                {
                    query = context.tb_balance_cash_record.Where(p => p.IsExclude.Equals(true) &&
                                p.PayType.Equals((int)enums.PayType.PayJinHuo) &&
                                p.PayDate >= currDate&&
                                p.PayDate <= endDate &&
                                p.PayNote.Contains(key)).ToList();
                }
               
                if (comboBoxOrder.SelectedItem != null && comboBoxOrder.SelectedItem.ToString() == "价格")
                {
                    query = query.OrderByDescending(p => p.PayCash)
                         .ThenByDescending(p => p.Id).ToList();
                }
                else if(comboBoxOrder.SelectedItem != null && comboBoxOrder.SelectedItem.ToString() == "备注")
                {
                    query = query.OrderByDescending(p => p.PayNote)
                        .ThenByDescending(p => p.Id).ToList();
                }
                else
                {
                    query = query.OrderByDescending(p => p.CreateTime)
                        .ThenByDescending(p => p.Id).ToList();
                }

                this.labelSearch.Text = string.Concat(" 查询日期范围： ", currDate.ToString("yyyy-MM-dd"), " 到 ", endDate.ToString("yyyy-MM-dd"));
            }
            //else if (balanceRecordType == BalanceRecordType.Sys)
            //{
            //    query = context.tb_balance_cash_record.Where(p => p.CreatorId.Equals(Helper.Config.SysAdminId)).OrderByDescending(p => p.PayDate)
            //   .ThenByDescending(p => p.Id).ToList();
            //}
            //else
            //{
            //    query = context.tb_balance_cash_record.OrderByDescending(p => p.PayDate)
            //                   .ThenByDescending(p => p.Id).ToList();
            //}
            label_total.Text = query.Count.ToString();
            this.listView1.Items.Clear();
            for (int i = 0; i < query.Count; i++)
            {
                var item = new ListViewItem(query[i].CreatorName);
                item.Tag = query[i].Id;

                item.SubItems.Add(query[i].CreateTime.ToString("yyyy-MM-dd"));
                item.SubItems.Add(query[i].PayDate.ToString("yyyy-MM-dd"));
                //item.SubItems.Add(query[i].PreBalance.ToString("##,###,##0.00"));
                item.SubItems.Add(query[i].PayCash.ToString("##,###,##0.00"));
                item.SubItems.Add(query[i].IncomeCash.ToString("#,###,##0.00"));
                // item.SubItems.Add(query[i].AfterBalance.ToString("#,###,##0.00"));
                item.SubItems.Add(query[i].PayNote);
                this.listView1.Items.Add(item);
            }

            var balance = Helper.BalanceHelper.GetBalance(context);
            this.btnBalance.Text = query.Sum(p => (decimal?)p.PayCash).GetValueOrDefault().ToString("#,###,##0.00");
            //var borrom = context.tb_balance_cash_record.Where(p => p.IsExclude.Equals(false) &&
            //    !p.CreatorId.Equals(Helper.Config.SysAdminId)).Sum(p => (decimal?)p.PayCash).GetValueOrDefault();
            // this.btn_export.Text = borrom.ToString("#,###,##0.00");
            // this.btnNowText.Text = (balance + borrom).ToString("#,###,##0.00");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmCashStat fs = new frmCashStat();
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.numericUpDown1.Value == 0M)
            {
                MessageBox.Show("请输入金额");
                this.numericUpDown1.Focus();
                return;
            }
            if (string.IsNullOrEmpty(this.txtNote.Text.Trim()))
            {
                MessageBox.Show("请输入备注");
                this.txtNote.Focus();
                return;
            }

            if (DateTime.Now.Day >= 8)
            {
                // 计算上个月利润
                //if (!Helper.BalanceHelper.CurrMonthHaveAutoStat(context, DateTime.Now))
                //{
                //    DateTime currDate = DateTime.Parse(DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01"));

                //    Helper.BalanceHelper.StatBalance(context, currDate.AddMonths(1));
                //}
            }

            Helper.BalanceHelper.SavePayBalance(context
                , this.numericUpDown1.Value
                , this.dateTimePicker1.Value
                , this.txtNote.Text
                , Helper.Config.CurrentUser.id
                , Helper.Config.CurrentUser.user_name
                , enums.PayType.PayJinHuo
                );

            this.txtNote.Text = "";
            this.numericUpDown1.Value = 0M;
            MessageBox.Show("OK");
            InitData();
        }

        //private void btnBorrow_Click(object sender, EventArgs e)
        //{
        //    InitData(BalanceRecordType.Sys);
        //}

        //private void btnAll_Click(object sender, EventArgs e)
        //{
        //    InitData(BalanceRecordType.All);
        //}

        //private void btnPay_Click(object sender, EventArgs e)
        //{
        //    InitData(BalanceRecordType.Pay);
        //}

        //private void btnIncome_Click(object sender, EventArgs e)
        //{
        //    InitData(BalanceRecordType.Income);
        //}

        //private void radioButton1_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.radioButton2.Checked = !this.radioButton1.Checked;
        //}

        //private void radioButton2_CheckedChanged(object sender, EventArgs e)
        //{
        //    this.radioButton1.Checked = !this.radioButton2.Checked;
        //}

        private void btnResetProfit_Click(object sender, EventArgs e)
        {
            frmResetProfitToBalanceCash frm = new frmResetProfitToBalanceCash();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btn_export_Click(object sender, EventArgs e)
        {
            var dt = new DataTable("现金流水");
            dt.Columns.Add("操作员");
            dt.Columns.Add("输入日期");
            dt.Columns.Add("支付日期");
            dt.Columns.Add("支付前结余");
            dt.Columns.Add("支付金额");
            dt.Columns.Add("收入金额");
            dt.Columns.Add("支付备注");
            foreach (var item in query)
            {
                var dr = dt.NewRow();
                dr["操作员"] = item.CreatorName;
                dr["输入日期"] = item.CreateTime;
                dr["支付日期"] = item.PayDate;
                dr["支付前结余"] = item.PreBalance;
                dr["支付金额"] = item.PayCash;
                dr["收入金额"] = item.IncomeCash;
                dr["支付备注"] = item.PayNote;
                dt.Rows.Add(dr);
            }
            Helper.NPOIExcel.ToExcel(dt
                , "现金流水" + DateTime.Now.ToString("yyyy-MM-dd")
                , Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\现金流水" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");
            MessageBox.Show("文件已放桌面");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            _isInputDate = true;
            InitData();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            _isInputDate = false;
            InitData();
        }
    }
}
