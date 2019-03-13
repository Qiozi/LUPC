using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace KKWStore
{
    public partial class frmExportOutRecord : Form
    {
        bool isYear = false;

        public frmExportOutRecord()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmExportOutRecord_Shown);
        }

        void frmExportOutRecord_Shown(object sender, EventArgs e)
        {
            for (int i = 2000; i <= DateTime.Now.Year; i++)
            {
                comboBox1.Items.Add(i.ToString());
            }
            comboBox1.Text = DateTime.Now.Year.ToString();
            dateTimePicker1.Value = DateTime.Now.AddMonths(-1);
        }

        private void buttonExportDate_Click(object sender, EventArgs e)
        {

        }

        private void buttonExportYear_Click(object sender, EventArgs e)
        {
            //int year;
            //int.TryParse(comboBox1.Text, out year);

            //DataTable dt = new DataTable();
            //if (isYear)
            //{
            //    dt.Columns.Add("项目");
            //    dt.Columns.Add("1月");
            //    dt.Columns.Add("2月");
            //    dt.Columns.Add("3月");
            //    dt.Columns.Add("4月");
            //    dt.Columns.Add("5月");
            //    dt.Columns.Add("6月");
            //    dt.Columns.Add("7月");
            //    dt.Columns.Add("8月");
            //    dt.Columns.Add("9月");
            //    dt.Columns.Add("10月");
            //    dt.Columns.Add("11月");
            //    dt.Columns.Add("12月");

            //    DataRow dr1 = dt.NewRow();
            //    dr1[0] = "出库成本金额(/10000)";
            //    dr1[1] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-01%' and IsReturnWholesaler=0", year));
            //    dr1[2] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-02%' and IsReturnWholesaler=0", year));
            //    dr1[3] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-03%' and IsReturnWholesaler=0", year));
            //    dr1[4] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-04%' and IsReturnWholesaler=0", year));
            //    dr1[5] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-05%' and IsReturnWholesaler=0", year));
            //    dr1[6] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-06%' and IsReturnWholesaler=0", year));
            //    dr1[7] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-07%' and IsReturnWholesaler=0", year));
            //    dr1[8] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-08%' and IsReturnWholesaler=0", year));
            //    dr1[9] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-09%' and IsReturnWholesaler=0", year));
            //    dr1[10] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-10%' and IsReturnWholesaler=0", year));
            //    dr1[11] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-11%' and IsReturnWholesaler=0", year));
            //    dr1[12] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost)/10000 from tb_serial_no_and_p_code where out_regdate like '{0}-12%' and IsReturnWholesaler=0", year));

            //    dt.Rows.Add(dr1);

            //    DataRow dr2 = dt.NewRow();
            //    dr2[0] = "出库产品总数量(/100)";
            //    dr2[1] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-01%' and IsReturnWholesaler=0", year));
            //    dr2[2] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-02%' and IsReturnWholesaler=0", year));
            //    dr2[3] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-03%' and IsReturnWholesaler=0", year));
            //    dr2[4] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-04%' and IsReturnWholesaler=0", year));
            //    dr2[5] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-05%' and IsReturnWholesaler=0", year));
            //    dr2[6] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-06%' and IsReturnWholesaler=0", year));
            //    dr2[7] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-07%' and IsReturnWholesaler=0", year));
            //    dr2[8] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-08%' and IsReturnWholesaler=0", year));
            //    dr2[9] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-09%' and IsReturnWholesaler=0", year));
            //    dr2[10] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-10%' and IsReturnWholesaler=0", year));
            //    dr2[11] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-11%' and IsReturnWholesaler=0", year));
            //    dr2[12] = db.SqlExec.ExecuteScalar(string.Format("Select count(id)/100 from tb_serial_no_and_p_code where out_regdate like '{0}-12%' and IsReturnWholesaler=0", year));

            //    dt.Rows.Add(dr2);

            //    #region create excel

            //    double _maxValue = 0;
            //    int valueColumnStarIndex = 0;
            //    string saveFileName = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), year + "年出库成本.xls");
            //    if (File.Exists(saveFileName))
            //    {
            //        File.Delete(saveFileName);
            //    }

            //    Helper.ExcelHelper exc = new Helper.ExcelHelper(Application.StartupPath + "\\mm.xls", saveFileName, true);

            //    List<KeyValuePair<string, string>> ColumnNames = new List<KeyValuePair<string, string>>();
            //    List<List<string>> Contents = new List<List<string>>();

            //    exc.SetCells(1, 1, string.Format("{0}", year + "年出库成本"));

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr = dt.Rows[i];

            //        for (int j = 0; j < dt.Columns.Count; j++)
            //        {

            //            if (i == 0)
            //            {
            //                exc.SetCells(i + 2, j + 1, dt.Columns[j].ToString());
            //            }

            //            exc.SetCells(i + 3, j + 1, dr[j].ToString());
            //            //if (valueColumnStarIndex == 0)
            //            //{
            //            //    if (dr[j].ToString().IndexOf("月") > -1)
            //            //        valueColumnStarIndex = j+1;
            //            //}
            //            if (j > 0)
            //            {
            //                double d;
            //                double.TryParse(dr[j].ToString(), out d);


            //                if (d > _maxValue)
            //                    _maxValue = d;
            //            }
            //        }
            //    }
            //    //for (int i = 0; i < _itemValue.Count; i++)
            //    //{
            //    //    double d;
            //    //    double.TryParse(_itemValue[i].Split(new char[] { '|' })[0], out d);


            //    //    if (d > _maxValue)
            //    //        _maxValue = d;

            //    //}



            //    //InfoBusiness.DBUtility.ExcelHelper.CreateExcel(string.Format("{0} {1}", ddlDepartmentId.SelectedItem.Text, ddlYear.SelectedItem.Text), ColumnNames, Contents, "", Server.MapPath("~/csd.xls"));
            //    // GC.Collect();
            //    #endregion
            //    // Response.Write(_maxValue.ToString());
            //    int endRow = dt.Rows.Count + 2;
            //    int endColumn = dt.Columns.Count;

            //    valueColumnStarIndex = 2;

            //    exc.GrawHistogram3(true, false, 2, valueColumnStarIndex, endColumn, valueColumnStarIndex, endRow, valueColumnStarIndex, endColumn, "test", "", "", 400, 800, "Sheet1", 10, endColumn * 60 + 100, (int)_maxValue);
            //    //exc.GrawHistogram(4, 1, 8, 4, 8, 4, "test2", "", "", 400, 400, "Sheet1", 300, 300, (int)_maxValue);
            //    //exc.CreateLine(4, 1, 8, 4, 8, 3, "test", "类别", "类别2", 400, 400, "Sheet1", 300, 300, Server.MapPath("~/"));
            //    exc.MergeCells(1, 1, 1, dt.Columns.Count, string.Format("{0}", year + "年出库成本(单位：万元)"));

            //    exc.SaveFile(saveFileName);

            //    // helper.OutputFilePath = Application.StartupPath + "\\TEST.xls";


            //    //exc.SetCells(1, 1, "10");}
            //}
            //else
            //{
            //    string subTitle = dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 到 " + dateTimePicker2.Value.ToString("yyyy-MM-dd");
            //    dt.Columns.Add("项目");
            //    dt.Columns.Add(subTitle);


            //    DataRow dr1 = dt.NewRow();
            //    dr1[0] = "出库成本金额";
            //    dr1[1] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where date_format(out_regdate ,'%Y%j') >= {0} and  date_format(out_regdate ,'%Y%j') <= {1}  and IsReturnWholesaler=0", dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.DayOfYear.ToString(), dateTimePicker2.Value.Year.ToString() + dateTimePicker2.Value.DayOfYear.ToString()));


            //    dt.Rows.Add(dr1);

            //    DataRow dr2 = dt.NewRow();
            //    dr2[0] = "出库产品总数量(/100)";
            //    dr2[1] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where date_format(out_regdate ,'%Y%j') >= {0} and  date_format(out_regdate ,'%Y%j') <= {1} and IsReturnWholesaler=0", dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.DayOfYear.ToString(), dateTimePicker2.Value.Year.ToString() + dateTimePicker2.Value.DayOfYear.ToString()));


            //    dt.Rows.Add(dr2);

            //    dataGridView1.DataSource = dt;


            //    #region create excel

            //    double _maxValue = 0;
            //    int valueColumnStarIndex = 0;
            //    string saveFileName = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString(), subTitle + "出库成本.xls");
            //    if (File.Exists(saveFileName))
            //        File.Delete(saveFileName);
            //    Helper.ExcelHelper exc = new Helper.ExcelHelper(Application.StartupPath + "\\mm.xls", saveFileName, true);

            //    List<KeyValuePair<string, string>> ColumnNames = new List<KeyValuePair<string, string>>();
            //    List<List<string>> Contents = new List<List<string>>();

            //    exc.SetCells(1, 1, string.Format("{0}", ""));

            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        DataRow dr = dt.Rows[i];

            //        for (int j = 0; j < dt.Columns.Count; j++)
            //        {

            //            if (i == 0)
            //            {
            //                exc.SetCells(i + 2, j + 1, dt.Columns[j].ToString());
            //            }

            //            exc.SetCells(i + 3, j + 1, dr[j].ToString());
 
            //            if (j > 0)
            //            {
            //                double d;
            //                double.TryParse(dr[j].ToString(), out d);


            //                if (d > _maxValue)
            //                    _maxValue = d;
            //            }
            //        }
            //    }

            //    #endregion


            //    //exc.MergeCells(1, 1, 1, dt.Columns.Count, string.Format("{0}", subTitle + "出库成本"));

            //    exc.SaveFile(saveFileName);

            //}

        }

        private void button2_Click(object sender, EventArgs e)
        {
            isYear = true;
            int year;
            int.TryParse(comboBox1.Text, out year);

            DataTable dt = new DataTable();
            dt.Columns.Add("项目");
            dt.Columns.Add("1月");
            dt.Columns.Add("2月");
            dt.Columns.Add("3月");
            dt.Columns.Add("4月");
            dt.Columns.Add("5月");
            dt.Columns.Add("6月");
            dt.Columns.Add("7月");
            dt.Columns.Add("8月");
            dt.Columns.Add("9月");
            dt.Columns.Add("10月");
            dt.Columns.Add("11月");
            dt.Columns.Add("12月");

            DataRow dr1 = dt.NewRow();
            dr1[0] = "出库成本金额";
            dr1[1] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-01%' and IsReturnWholesaler=0", year));
            dr1[2] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-02%' and IsReturnWholesaler=0", year));
            dr1[3] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-03%' and IsReturnWholesaler=0", year));
            dr1[4] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-04%' and IsReturnWholesaler=0", year));
            dr1[5] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-05%' and IsReturnWholesaler=0", year));
            dr1[6] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-06%' and IsReturnWholesaler=0", year));
            dr1[7] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-07%' and IsReturnWholesaler=0", year));
            dr1[8] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-08%' and IsReturnWholesaler=0", year));
            dr1[9] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-09%' and IsReturnWholesaler=0", year));
            dr1[10] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-10%' and IsReturnWholesaler=0", year));
            dr1[11] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-11%' and IsReturnWholesaler=0", year));
            dr1[12] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-12%' and IsReturnWholesaler=0", year));

            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2[0] = "出库产品总数量";
            dr2[1] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-01%' and IsReturnWholesaler=0", year));
            dr2[2] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-02%' and IsReturnWholesaler=0", year));
            dr2[3] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-03%' and IsReturnWholesaler=0", year));
            dr2[4] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-04%' and IsReturnWholesaler=0", year));
            dr2[5] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-05%' and IsReturnWholesaler=0", year));
            dr2[6] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-06%' and IsReturnWholesaler=0", year));
            dr2[7] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-07%' and IsReturnWholesaler=0", year));
            dr2[8] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-08%' and IsReturnWholesaler=0", year));
            dr2[9] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-09%' and IsReturnWholesaler=0", year));
            dr2[10] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-10%' and IsReturnWholesaler=0", year));
            dr2[11] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-11%' and IsReturnWholesaler=0", year));
            dr2[12] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-12%' and IsReturnWholesaler=0", year));

            dt.Rows.Add(dr2);

            dataGridView1.DataSource = dt;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            isYear = false;
            int year;
            int.TryParse(comboBox1.Text, out year);

            DataTable dt = new DataTable();
            dt.Columns.Add("项目");
            dt.Columns.Add(dateTimePicker1.Value.ToString("yyyy-MM-dd") + " 到 " + dateTimePicker2.Value.ToString("yyyy-MM-dd"));
            //dt.Columns.Add("2月");
            //dt.Columns.Add("3月");
            //dt.Columns.Add("4月");
            //dt.Columns.Add("5月");
            //dt.Columns.Add("6月");
            //dt.Columns.Add("7月");
            //dt.Columns.Add("8月");
            //dt.Columns.Add("9月");
            //dt.Columns.Add("10月");
            //dt.Columns.Add("11月");
            //dt.Columns.Add("12月");
            
            DataRow dr1 = dt.NewRow();
            dr1[0] = "出库成本金额";
            dr1[1] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where date_format(out_regdate ,'%Y%j') >= {0} and  date_format(out_regdate ,'%Y%j') <= {1} and IsReturnWholesaler=0", dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.DayOfYear.ToString(), dateTimePicker2.Value.Year.ToString() + dateTimePicker2.Value.DayOfYear.ToString()));
            //dr1[2] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-02%'", year));
            //dr1[3] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-03%'", year));
            //dr1[4] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-04%'", year));
            //dr1[5] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-05%'", year));
            //dr1[6] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-06%'", year));
            //dr1[7] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-07%'", year));
            //dr1[8] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-08%'", year));
            //dr1[9] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-09%'", year));
            //dr1[10] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-10%'", year));
            //dr1[11] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-11%'", year));
            //dr1[12] = db.SqlExec.ExecuteScalar(string.Format("Select Sum(in_cost) from tb_serial_no_and_p_code where out_regdate like '{0}-12%'", year));

            dt.Rows.Add(dr1);

            DataRow dr2 = dt.NewRow();
            dr2[0] = "出库产品总数量";
            dr2[1] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where date_format(out_regdate ,'%Y%j') >= {0} and  date_format(out_regdate ,'%Y%j') <= {1} and IsReturnWholesaler=0", dateTimePicker1.Value.Year.ToString() + dateTimePicker1.Value.DayOfYear.ToString(), dateTimePicker2.Value.Year.ToString() + dateTimePicker2.Value.DayOfYear.ToString()));
            //dr2[2] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-02%'", year));
            //dr2[3] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-03%'", year));
            //dr2[4] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-04%'", year));
            //dr2[5] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-05%'", year));
            //dr2[6] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-06%'", year));
            //dr2[7] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-07%'", year));
            //dr2[8] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-08%'", year));
            //dr2[9] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-09%'", year));
            //dr2[10] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-10%'", year));
            //dr2[11] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-11%'", year));
            //dr2[12] = db.SqlExec.ExecuteScalar(string.Format("Select count(id) from tb_serial_no_and_p_code where out_regdate like '{0}-12%'", year));

            dt.Rows.Add(dr2);

            dataGridView1.DataSource = dt;
        }
    }
}
