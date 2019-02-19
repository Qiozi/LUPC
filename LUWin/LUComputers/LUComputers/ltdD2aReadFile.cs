using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using LUComputers.DBProvider;

namespace LUComputers
{
    public partial class ltdD2aReadFile : Form
    {
        string filename = "";
        List<D2aModel> list = new List<D2aModel>();
        public ltdD2aReadFile(string _filename)
        {
            filename = _filename;
            InitializeComponent();
            textBox1.Text = filename;

            if (!string.IsNullOrEmpty(filename))
            {
                button1_Click(null, null);
                button2_Click(null, null);
                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                this.Close();
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
            }
        }

        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text.Trim()))
            {
                List<D2aModel> dt = ToDT(textBox1.Text);
                dataGridView1.DataSource = dt;
            }
        }

        List<D2aModel> ToDT(string filename)
        {
            list.Clear();


            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(sr);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheetAt(0);

            //获取sheet的首行
            IRow headerRow = sheet.GetRow(15);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //{
            //    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue ?? "");
            //    table.Columns.Add(column);
            //}
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = 14; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                // sheet.GetRow(0).GetCell(0).is
                if (row == null) continue;
                if (row.GetCell(0) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(0).ToString();
                    decimal cost1;
                    decimal.TryParse(row.GetCell(6).ToString(), out cost1);
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
                if (row.GetCell(8) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(8).ToString();
                    decimal cost1;
                    decimal.TryParse(row.GetCell(15).ToString(), out cost1);
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
            }
            ISheet sheet2 = workbook.GetSheetAt(1);

            for (int i = 11; i < sheet2.LastRowNum; i++)
            {
                IRow row = sheet2.GetRow(i);
                // sheet.GetRow(0).GetCell(0).is
                if (row == null) continue;
                if (row.GetCell(2) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(2).ToString();
                    decimal cost1;
                    decimal.TryParse(row.GetCell(3).ToString(), out cost1);
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }

            }

            workbook = null;
            sheet = null;
            sheet2 = null;
            sr.Close();
            sr.Dispose();
            return list;
        }

        private void button2_Click(object sender, EventArgs e)
        {


            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_d2a);

            DataTable luSkuDT = Config.ExecuteDateTable(string.Format("select lu_sku,manufacturer_part_number from tb_other_inc_valid_lu_sku where prodType='{0}' ", "NEW"));
            string table_name = new Watch.Eprom().CreateTable(Ltd.wholesaler_d2a);
            {

                for (int i = 0; i < list.Count; i++)
                {

                    int luc_sku = Watch.LU.GetSKUByltdSku(list[i].mfp, ltd_id);

                    if (luc_sku == 0)
                    {
                        luc_sku = Watch.LU.GetSKUByMfp(list[i].mfp, luSkuDT);

                        if (luc_sku != 0)
                        {

                            Config.ExecuteDateTable(string.Format(@"insert into tb_other_inc_match_lu_sku (lu_sku , other_inc_sku, other_inc_type) values 
                                        ('{0}', '{1}', '{2}')", luc_sku, list[i].mfp, ltd_id));

                            //
                            // save Match LU SKU.
                            // 
                            //if (luc_sku > 0)
                            //{
                            //    Helper.SaveNewMatch snm = new LUComputers.Helper.SaveNewMatch(null, null);
                            //    snm.SaveNewMatchSKU(ltd_id, dr["ltd_sku"].ToString().Trim(), luc_sku);
                            //}
                        }

                    }

                    string stock_str = list[i].stock.ToString();
                    if (stock_str != "")
                    {

                        Config.ExecuteNonQuery(string.Format(@"insert into {0} (part_sku, part_cost, store_quantity, mfp, part_name, luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", table_name
                                                             , list[i].mfp
                                                             , list[i].cost
                                                             , list[i].stock
                                                             , list[i].mfp
                                                             , list[i].mfp
                                                             , luc_sku));


                    }
                }
            }



            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id + "'");

            Config.ExecuteNonQuery(string.Format(@"insert into tb_other_inc_part_info 
	(luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, part_sku, mfp, part_cost, store_quantity, 1, now() from {0}", table_name, ltd_id));



            Helper.SaveNewMatch SNM = new LUComputers.Helper.SaveNewMatch();
            LtdHelper LH = new LtdHelper();
            string table_name2 = LH.GetLastStoreTableNameGroup(Ltd.wholesaler_d2a);
            SNM.UpdateToRemote(Ltd.wholesaler_d2a, table_name2);
            if (string.IsNullOrEmpty(filename))
                MessageBox.Show("OK");
        }
    }

    [Serializable]
    public class D2aModel
    {
        public D2aModel() { }

        public string mfp { get; set; }
        public decimal cost { get; set; }
        public int stock { get; set; }

    }
}
