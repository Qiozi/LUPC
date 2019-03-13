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
    public partial class frmCashStat : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmCashStat()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmStat_Shown);
        }

        void frmStat_Shown(object sender, EventArgs e)
        {
            BindYear();
        }
        void BindYear()
        {
            for (int i = 2016; i <= DateTime.Now.Year; i++)
            {
                comboBox1.Items.Add(i.ToString() + "年");
            }
            comboBox1.Text = DateTime.Now.ToString("yyyy年");
        }

        private void buttonStat_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            //if (!_is_just_now)
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = new DataTable("d");
                dt.Columns.Add("月份");
                dt.Columns.Add("费用支出");
                dt.Columns.Add("进货支出");
                dt.Columns.Add("收入");
                dt.Columns.Add("小计");
                dt.Columns.Add("余额");

                var year = int.Parse(comboBox1.Text.Substring(0, 4));


                #region 全年，按月

                for (int i = 1; i <= 12; i++)
                {
                    DataRow dr = dt.NewRow();
                    DateTime currDate = DateTime.Parse(year.ToString() + i.ToString("-00-") + "01");
                    DateTime endDate = currDate.AddMonths(1).AddMinutes(-1);

                    DateTime currDateForComein = currDate;//.AddMonths(1);
                    DateTime endDateForComein = currDateForComein.AddMonths(1).AddMinutes(-1);

                    decimal feePay = 0;
                    decimal JinhuoPay = 0;
                    decimal comein = 0;
                    decimal stat = 0;
                    decimal balance;
                    var query = context.tb_balance_cash_record.Where(p => p.PayDate >= currDate && p.PayDate <= endDate).ToList();

                    feePay = query.Where(p => p.PayType.Equals((int)enums.PayType.Fee)).Sum(p => (decimal?)p.PayCash).GetValueOrDefault();

                    JinhuoPay = query.Where(p => p.PayType.Equals((int)enums.PayType.PayJinHuo)).Sum(p => (decimal?)p.PayCash).GetValueOrDefault();
                    comein = context.tb_balance_cash_record
                        .Where(p => p.PayDate >= currDateForComein && p.PayDate <= endDateForComein)
                        .Sum(p => (decimal?)p.IncomeCash).GetValueOrDefault();
                    stat = comein - (JinhuoPay + feePay)  ;

                    balance = context.tb_balance_cash_record.Where(p => p.PayDate <= endDate)
                        .Sum(p => (decimal?)p.IncomeCash).GetValueOrDefault() -
                        context.tb_balance_cash_record.Where(p => p.PayDate <= endDate).Sum(p => (decimal?)p.PayCash).GetValueOrDefault();

                    dr["月份"] = currDate.ToString("yyyy年MM月");
                    dr["费用支出"] = feePay.ToString("###,###,##0.00");
                    dr["进货支出"] = JinhuoPay.ToString("###,###,##0.00");
                    dr["收入"] = comein.ToString("###,###,##0.00"); 
                    dr["小计"] = stat.ToString("###,###,##0.00");
                    dr["余额"] = balance.ToString("###,###,##0.00");
                    //dr["结余支出"] = Helper.BalanceHelper.GetBalanceStat(context, currDate, true);
                    dt.Rows.Add(dr);

                }

                dataGridView1.DataSource = dt;
                #endregion

                this.Cursor = Cursors.Default;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            frmResetProfitToBalanceCash frm = new frmResetProfitToBalanceCash();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            var year = int.Parse(comboBox1.Text.Substring(0, 4));

            for (int i = 1; i <= 12; i++)
            {
                DateTime currDate = DateTime.Parse(year.ToString() + i.ToString("-00-") + "01");
                DateTime endDate = currDate.AddMonths(1).AddMinutes(-1);

                var dt = new DataTable("现金流水-费用");
                dt.Columns.Add("操作员");
                dt.Columns.Add("输入日期");
                dt.Columns.Add("支付日期");
                dt.Columns.Add("支付金额");
                dt.Columns.Add("收入金额");
                dt.Columns.Add("支付备注");

                var query = context.tb_balance_cash_record.Where(p => p.PayType.Equals((int)enums.PayType.Fee) &&
             p.PayDate >= currDate &&  p.PayDate <= endDate).OrderByDescending(p => p.CreateTime)
            .ThenByDescending(p => p.Id).ToList();

                foreach (var item in query)
                {
                    var dr = dt.NewRow();
                    dr["操作员"] = item.CreatorName;
                    dr["输入日期"] = item.CreateTime;
                    dr["支付日期"] = item.PayDate;
                    dr["支付金额"] = item.PayCash;
                    dr["收入金额"] = item.IncomeCash;
                    dr["支付备注"] = item.PayNote;
                    dt.Rows.Add(dr);
                }
                Helper.NPOIExcel.ToExcel(dt
                    , "现金流水-费用-" + currDate.ToString("yyyy-MM-dd")
                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\现金流水-费用-" + currDate.ToString("yyyy-MM-dd") + ".xls");

                dt.Rows.Clear();

                query = context.tb_balance_cash_record.Where(p => p.PayType.Equals((int)enums.PayType.PayJinHuo) &&
           p.PayDate >= currDate && p.PayDate <= endDate).OrderByDescending(p => p.CreateTime)
         .ThenByDescending(p => p.Id).ToList();

                foreach (var item in query)
                {
                    var dr = dt.NewRow();
                    dr["操作员"] = item.CreatorName;
                    dr["输入日期"] = item.CreateTime;
                    dr["支付日期"] = item.PayDate;
                    dr["支付金额"] = item.PayCash;
                    dr["收入金额"] = item.IncomeCash;
                    dr["支付备注"] = item.PayNote;
                    dt.Rows.Add(dr);
                }
                Helper.NPOIExcel.ToExcel(dt
                    , "现金流水-进货-" + currDate.ToString("yyyy-MM-dd")
                    , Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\现金流水-进货-" + currDate.ToString("yyyy-MM-dd") + ".xls");
            }

            MessageBox.Show("文件已放桌面");
        }
    }
}
