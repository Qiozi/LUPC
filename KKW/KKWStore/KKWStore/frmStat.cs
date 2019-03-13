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
    public partial class frmStat : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        //bool _is_just_now = false;
        string _allText = "全部";

        public frmStat()
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
            for (int i = 2000; i <= DateTime.Now.Year + 3; i++)
            {
                comboBox1.Items.Add(i.ToString() + "年");
            }
            comboBox1.Text = DateTime.Now.ToString("yyyy年");

            comboBox2.Items.Add(_allText);
            for (int i = 1; i <= 12; i++)
            {
                comboBox2.Items.Add(i.ToString("00") + "月");
            }
            comboBox2.Text = _allText;// DateTime.Now.ToString("MM月");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = null;
            //if (!_is_just_now)
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable dt = new DataTable("d");
                dt.Columns.Add("日期");
                dt.Columns.Add("成本支出");
                dt.Columns.Add("商品出库金额");
                dt.Columns.Add("代销他人成本");
                dt.Columns.Add("销售额");
                dt.Columns.Add("赠品金额");
                dt.Columns.Add("利润");
                // dt.Columns.Add("结余支出");

                if (comboBox2.Text == "全部")
                {
                    decimal profitALL = 0;
                    decimal saleTotalALL = 0;
                    decimal partCostALL = 0;
                    decimal proxyCostALL = 0;
                    decimal payCostALL = 0;
                    decimal freeCostALL = 0;

                    #region 全年，按月

                    for (int i = 1; i <= 12; i++)
                    {
                        DataRow dr = dt.NewRow();
                        DateTime currDate = DateTime.Parse(this.comboBox1.Text.Substring(0, 4) + i.ToString("-00-") + "01");
                        //                        string beginDate = currDate.ToString("yyyyMMdd");
                        //                        string endDate = currDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

                        //                        db.SqlExec.ExecuteNonQuery("Delete from tb_expend_average");

                        //                        string sql = string.Format(@"select ifnull(sum(total), 0) from (select 
                        //         case when date_format(outBeginDate, '%Y%m%d') >= '{0}' and date_format(outEndDate, '%Y%m%d') <= '{1}' then outTotal
                        //              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
                        //              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{0}' then (date_format(outEndDate, '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
                        //              when date_format(outBeginDate, '%Y%m%d') >=  '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format(outBeginDate, '%Y%j')+1) * average
                        //              
                        //              else  100000000 end total      
                        //    from tb_expend where (date_format(outEndDate, '%Y%m%d') >= {0} and date_format(outEndDate, '%Y%m%d') <= '{1}') 
                        //	or (date_format(outBeginDate, '%Y%m%d') <='{1}' and date_format(outBeginDate, '%Y%m%d') >= '{0}')
                        //    or (date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}')
                        //) t", beginDate
                        //                                                                                                                , endDate);
                        //                        DataTable dtSum = db.SqlExec.ExecuteDataTable(sql);
                        //                        string t = dtSum.Rows[0][0].ToString();
                        //                        dr[i - 1] = t;

                        //                        DataTable dtSale = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_sales_total where date_format(saleDate,'%Y%m')='{0}'",
                        //                           currDate.ToString("yyyyMM")));

                        //                        DataTable dtPartCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(in_cost), 0) from tb_serial_no_and_p_code where date_format(out_regdate, '%Y%m') = '{0}' and IsReturnWholesaler=0 ",
                        //                            currDate.ToString("yyyyMM")));
                        //                         赠品
                        //                        DataTable dtFeeCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(s.in_cost), 0) from tb_serial_no_and_p_code s inner join tb_out_invoice_product o on s.SerialNO=o.SerialNO and o.IsFree=1 where date_format(s.out_regdate, '%Y%m') = '{0}' and s.IsReturnWholesaler=0 ",
                        //                           currDate.ToString("yyyyMM")));

                        //                        DataTable dtProxy = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_proxy where date_format(saleDate, '%Y%m')='{0}'",
                        //                           currDate.ToString("yyyyMM")));


                        //                        decimal profit = 0;
                        //                        decimal saleTotal;
                        //                        decimal.TryParse(dtSale.Rows[0][0].ToString(), out saleTotal);
                        //                        decimal partCost;
                        //                        decimal.TryParse(dtPartCost.Rows[0][0].ToString(), out partCost);
                        //                        decimal proxyCost;
                        //                        decimal.TryParse(dtProxy.Rows[0][0].ToString(), out proxyCost);
                        //                        decimal payCost;
                        //                        decimal.TryParse(t, out payCost);
                        //                        decimal freeCost;
                        //                        decimal.TryParse(dtFeeCost.Rows[0][0].ToString(), out freeCost);
                        //                        profit = saleTotal - partCost - proxyCost - payCost;

                        decimal profit = 0;
                        decimal saleTotal;
                        decimal partCost;
                        decimal proxyCost;
                        decimal payCost;
                        decimal freeCost;
                        profit = Helper.ProfitStat.StatMonthProfit(currDate, profit, out saleTotal, out partCost, out proxyCost, out payCost, out freeCost);
                        //profit = saleTotal - partCost - proxyCost - payCost;

                        profitALL += profit;
                        saleTotalALL += saleTotal;
                        partCostALL += partCost;
                        proxyCostALL += proxyCost;
                        payCostALL += payCost;
                        freeCostALL += freeCost;

                        // Helper.BalanceHelper.SavePayBalance(context, currDate, saleTotal);
                        if (currDate > DateTime.Parse("2016-07-31"))
                        {
                            Helper.BalanceHelper.SaveStatBalance(context, saleTotal, 0M, currDate, "系统自动计算(" + currDate.ToString("yyyy-MM") + " 营业额)", "系统");
                            Helper.BalanceHelper.SaveBalance(context, 0, freeCost, 0M, currDate, "系统自动计算(" + currDate.ToString("yyyy-MM") + " 赠品金额)", "系统", Helper.Config.SysAdminId, enums.PayType.Fee);
                        }
                        dr["日期"] = currDate.ToString("yyyy年MM月");
                        dr["成本支出"] = payCost.ToString("###,###,##0.00");
                        dr["商品出库金额"] = partCost.ToString("###,###,##0.00");
                        dr["代销他人成本"] = proxyCost.ToString("###,###,##0.00");
                        dr["销售额"] = saleTotal.ToString("###,###,##0.00");
                        dr["赠品金额"] = freeCost.ToString("###,###,##0.00");
                        dr["利润"] = Helper.Config.IsAdmin ? profit.ToString("###,###,##0.00") : "--";
                        //dr["结余支出"] = Helper.BalanceHelper.GetBalanceStat(context, currDate, true);
                        dt.Rows.Add(dr);

                    }

                    DataRow drALL = dt.NewRow();
                    drALL["日期"] = "合计";
                    drALL["成本支出"] = payCostALL.ToString("###,###,##0.00");
                    drALL["商品出库金额"] = partCostALL.ToString("###,###,##0.00");
                    drALL["代销他人成本"] = proxyCostALL.ToString("###,###,##0.00");
                    drALL["销售额"] = saleTotalALL.ToString("###,###,##0.00");
                    drALL["赠品金额"] = freeCostALL.ToString("###,###,##0.00");
                    drALL["利润"] = Helper.Config.IsAdmin ? profitALL.ToString("###,###,##0.00") : "--";
                    // drALL["结余支出"] = "--";
                    dt.Rows.Add(drALL);

                    dataGridView1.DataSource = dt;
                    #endregion

                }
                else
                {
                    #region  按天计算
                    int month;
                    int.TryParse(comboBox2.Text.Substring(0, 2), out month);
                    int year;
                    int.TryParse(comboBox1.Text.Substring(0, 4), out year);
                    DateTime cd;
                    DateTime.TryParse(string.Format("{0}-{1}-{2}", year, month, "01"), out cd);

                    decimal profitALL = 0;
                    decimal saleTotalALL = 0;
                    decimal partCostALL = 0;
                    decimal proxyCostALL = 0;
                    decimal payCostALL = 0;
                    decimal freeCostALL = 0;


                    for (int i = 1; i <= cd.AddMonths(+1).AddDays(-1).Day; i++)
                    {
                        DateTime currDate = DateTime.Parse(year.ToString("0000") + month.ToString("-00-") + i.ToString("00"));
                        string beginDate = currDate.ToString("yyyyMMdd");
                        string endDate = currDate.ToString("yyyyMMdd");

                        db.SqlExec.ExecuteNonQuery("Delete from tb_expend_average");
                        string sql = string.Format(@"select ifnull(sum(total), 0) from (select 
         case when date_format(outBeginDate, '%Y%m%d') >= '{0}' and date_format(outEndDate, '%Y%m%d') <= '{1}' then outTotal
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{0}' then (date_format(outEndDate, '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') >=  '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format(outBeginDate, '%Y%j')+1) * average
              
              else  100000000 end total      
    from tb_expend where (date_format(outEndDate, '%Y%m%d') >= {0} and date_format(outEndDate, '%Y%m%d') <= '{1}') 
	or (date_format(outBeginDate, '%Y%m%d') <='{1}' and date_format(outBeginDate, '%Y%m%d') >= '{0}')
    or (date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}')
) t", beginDate, endDate);


                        DataTable dtSum = db.SqlExec.ExecuteDataTable(sql);
                        string t = dtSum.Rows[0][0].ToString();

                        DataTable dtSale = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_sales_total where saleDate='{0}'",
                            currDate.ToString("yyyy-MM-dd")));

                        DataTable dtPartCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(in_cost), 0) from tb_serial_no_and_p_code where date_format(out_regdate, '%Y%m%d') = '{0}' and IsReturnWholesaler=0 ",
                            currDate.ToString("yyyyMMdd")));

                        // 赠品
                        DataTable dtFreeCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(s.in_cost), 0) from tb_serial_no_and_p_code s inner join tb_out_invoice_product o on s.SerialNO=o.SerialNO and o.IsFree=1 where date_format(s.out_regdate,  '%Y%m%d') = '{0}' and s.IsReturnWholesaler=0 ",
                           currDate.ToString("yyyyMMdd")));

                        DataTable dtProxy = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_proxy where saleDate='{0}'",
                           currDate.ToString("yyyy-MM-dd")));

                        decimal profit = 0;
                        decimal saleTotal;
                        decimal.TryParse(dtSale.Rows[0][0].ToString(), out saleTotal);
                        decimal partCost;
                        decimal.TryParse(dtPartCost.Rows[0][0].ToString(), out partCost);
                        decimal proxyCost;
                        decimal.TryParse(dtProxy.Rows[0][0].ToString(), out proxyCost);
                        decimal payCost;
                        decimal.TryParse(t, out payCost);
                        decimal freeCost;
                        decimal.TryParse(dtFreeCost.Rows[0][0].ToString(), out freeCost);

                        profit = saleTotal - partCost - proxyCost - payCost;

                        profitALL += profit;
                        saleTotalALL += saleTotal;
                        partCostALL += partCost;
                        proxyCostALL += proxyCost;
                        payCostALL += payCost;
                        freeCostALL += freeCost;


                        DataRow dr = dt.NewRow();
                        dr[0] = string.Format("{0}-{1}-{2}", year, month, i.ToString("00"));
                        dr[1] = payCost.ToString("###,###,##0.00");
                        dr[2] = partCost.ToString("###,###,##0.00");
                        dr[3] = proxyCost.ToString("###,###,##0.00");
                        dr[4] = saleTotal.ToString("###,###,##0.00");
                        dr[5] = freeCost.ToString("###,###,##0.00");
                        dr[6] = Helper.Config.IsAdmin ? profit.ToString("###,###,##0.00") : "--";

                        // dr[7] = Helper.BalanceHelper.GetBalanceStat(context, currDate, false);
                        dt.Rows.Add(dr);

                    }
                    #endregion

                    DataRow drALL = dt.NewRow();
                    drALL["日期"] = "合计";
                    drALL["成本支出"] = payCostALL.ToString("###,###,##0.00");
                    drALL["商品出库金额"] = partCostALL.ToString("###,###,##0.00");
                    drALL["代销他人成本"] = proxyCostALL.ToString("###,###,##0.00");
                    drALL["销售额"] = saleTotalALL.ToString("###,###,##0.00");
                    drALL["赠品金额"] = freeCostALL.ToString("###,###,##0.00");
                    drALL["利润"] = Helper.Config.IsAdmin ? profitALL.ToString("###,###,##0.00") : "--";
                    // drALL["结余支出"] = "--";
                    dt.Rows.Add(drALL);

                    dataGridView1.DataSource = dt;
                }
                this.Cursor = Cursors.Default;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.dataGridView1.DataSource = null;

            DataTable dt = new DataTable("d");
            //if (!_is_just_now)
            {
                int year = int.Parse(comboBox1.Text.Replace("年", ""));
                string month = comboBox2.Text;
                int monthInt = month == _allText ? 0 : int.Parse(month.Replace("月", ""));

                dt.Columns.Add("类别");
                for (int i = 1; i <= 12; i++)
                {
                    if (monthInt != 0 && monthInt != i) continue;
                    dt.Columns.Add(i.ToString("00") + "月");
                }

                DataTable cateDT = db.SqlExec.ExecuteDataTable("select distinct catename from tb_expend order by catename desc ");
                for (int j = 0; j < cateDT.Rows.Count; j++)
                {
                    DataRow dr = dt.NewRow();

                    string cateName = cateDT.Rows[j][0].ToString();
                    // decimal sum = 0;
                    dr[0] = cateName;

                    for (int i = 1; i <= 12; i++)
                    {
                        if (monthInt != 0 && monthInt != i) continue;
                        //decimal singleSum = decimal.Parse(db.SqlExec.ExecuteScalar(string.Format(@"Select ifnull(sum(outTotal), 0) from tb_expend where catename='{0}'
                        //                                and date_format(outBeginDate, '%Y%m%d')>='{1}'
                        //                                and date_format(outEndDate, '%Y%m%d')<= '{2}'"
                        //    , cateName
                        //    , string.Format("{0}{1}01", year.ToString(), i.ToString("00"))
                        //    , string.Format("{0}{1}31", year.ToString(), i.ToString("00")))).ToString());

                        decimal singleSum = decimal.Parse(db.SqlExec.ExecuteScalar(string.Format(@"
 select ifnull(sum(s),0) from (
Select ifnull(sum(outTotal), 0) as s, max(id)  from tb_expend where  catename='{0}' and  (date_format(outBeginDate, '%Y%m%d')>='{1}' and date_format(outEndDate, '%Y%m%d')<= '{2}' )
		union 
Select average * (to_days('{2}')-to_days('{1}')) , max(id)  from tb_expend where  catename='{0}' and (date_format(outBeginDate, '%Y%m%d')<'{1}' and date_format(outEndDate, '%Y%m%d')>'{2}')
		union 
Select average * (to_days(outEndDate)-to_days('{1}')) , max(id)  from tb_expend where  catename='{0}' and  (date_format(outBeginDate, '%Y%m%d')<'{1}' and date_format(outEndDate, '%Y%m%d')>='{1}' and date_format(outEndDate, '%Y%m%d')<='{2}')
		union 
Select average * (to_days('{2}')-to_days(outBeginDate)), max(id)  from tb_expend where  catename='{0}' and  (date_format(outBeginDate, '%Y%m%d')>='{1}' and date_format(outBeginDate, '%Y%m%d')<='{2}' and date_format(outEndDate, '%Y%m%d')>'{2}')
   ) t"
                          , cateName
                          , string.Format("{0}{1}01", year.ToString(), i.ToString("00"))
                          , DateTime.Parse(string.Format("{0}-{1}-01", year.ToString(), i.ToString("00"))).AddMonths(1).AddSeconds(-1).ToString("yyyyMMdd"))).ToString());

                        dr[monthInt > 0 ? 1 : i] = singleSum.ToString("###,###,##0.00");
                    }

                    dt.Rows.Add(dr);
                }

                dt.Columns.Add("合计");
                DataRow drSum = dt.NewRow();
                drSum[0] = "合计";
                dt.Rows.Add(drSum);
                for (int i = 0; i < dt.Rows.Count - 1; i++)
                {
                    decimal columnSum = 0M;
                    for (int j = 1; j < dt.Columns.Count - 1; j++)
                    {
                        decimal columnValue;
                        decimal.TryParse(dt.Rows[i][j].ToString(), out columnValue);
                        columnSum += columnValue;
                    }
                    dt.Rows[i][dt.Columns.Count - 1] = columnSum.ToString("###,###,##0.00");
                }
                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    decimal rowSum = 0M;
                    for (int j = 0; j < dt.Rows.Count - 1; j++)
                    {
                        decimal rowValue;
                        decimal.TryParse(dt.Rows[j][i].ToString(), out rowValue);
                        rowSum += rowValue;
                    }
                    dt.Rows[dt.Rows.Count - 1][i] = rowSum.ToString("###,###,##0.00");
                }
            }
            this.Cursor = Cursors.Default;
            dataGridView1.DataSource = dt;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            KKWStore.Helper.ExportExcel.Export(this.dataGridView1, "统计");
        }

        private void frmStat_Load(object sender, EventArgs e)
        {

        }
    }
}
