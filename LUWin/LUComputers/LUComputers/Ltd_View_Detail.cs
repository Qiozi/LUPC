using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LUComputers
{
    public partial class Ltd_View_Detail : Form
    {
        public Ltd_View_Detail()
        {
            InitializeComponent();

        }

        private void Ltd_View_Detail_Load(object sender, EventArgs e)
        {
            InitialWindow();
        }

        public void InitialWindow()
        {
            //
            // load Ltd info 
            //
            this.toolStripComboBox_Ltd.Items.Clear();
            LtdHelper LH = new LtdHelper();
            DataTable dt = LH.LtdHelperToDataTable();
            this.toolStripComboBox_Ltd.Items.Add("Select Ltd");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                this.toolStripComboBox_Ltd.Items.Add(dt.Rows[i]["text"].ToString());
            }

            //
            // Load Cmd Type
            //
            this.toolStripComboBox_view_type.Items.Clear();
            string[] name = Enum.GetNames(typeof(ViewType));
            for (int i = 0; i < name.Length; i++)
            {
                this.toolStripComboBox_view_type.Items.Add(name[i]);
            }
        }

        private void toolStripComboBox_Ltd_SelectedIndexChanged(object sender, EventArgs e)
        {
            LtdHelper LH = new LtdHelper();
            string seledtedLtdText = this.toolStripComboBox_Ltd.Text;
            int ltd_id = LH.GetLtdIdByText(seledtedLtdText);

            if (ltd_id > 0)
            {
                DataTable resultDT = Config.ExecuteDateTable("select id , compare_table_name from tb_other_inc_log where ltd_id='"+ ltd_id.ToString()+"' order by id desc limit 0,10");
                this.toolStripComboBox_date_list.Items.Clear();
                for (int i = 0; i < resultDT.Rows.Count; i++)
                {
                    this.toolStripComboBox_date_list.Items.Add(resultDT.Rows[i]["compare_table_name"].ToString());
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string ltd_name = this.toolStripComboBox_Ltd.Text;
            string db_table = this.toolStripComboBox_date_list.Text;
            string view_type = this.toolStripComboBox_view_type.Text;
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.GetLtdIdByText(ltd_name);

            if (ltd_id > 0)
            {
                //
                // view type
                //
                if (view_type.Length < 2)
                {
                    MessageBox.Show("Please select view type");
                    return;
                }
                ViewType VT = (ViewType) Enum.Parse(typeof(ViewType), view_type);

                //
                // table name s
                //
                string table1 = "";
                string table2 = "";
                SplitTableName(db_table, ref table1, ref table2);


                string sql = GetQuerySQL(VT, ltd_id, table1, table2);
                DataTable dt = Config.ExecuteDateTable(sql);
                this.dataGridView1.DataSource = dt;
                this.toolStripTextBox_recordcount.Text = "record: " + dt.Rows.Count.ToString();
            }
            else
                MessageBox.Show("Please select LTD");

        }

        private void SplitTableName(string table_names, ref string table1, ref string table2)
        {
            if (table_names.IndexOf('|') == -1)
                throw new Exception("Table names is error.");

            table1 = table_names.Split(new char[] { '|' })[0];
            table2 = table_names.Split(new char[] { '|' })[1];
        }

        private string GetQuerySQL(ViewType vt, int ltd_id, string yestoday_table, string today_table)
        {
            LtdHelper LH = new LtdHelper();
            Ltd ltd = LH.LtdModelByValue(ltd_id);
            string sql = string.Empty;
            string index_fields_name="";
            string cost_fields_name = "";
            string quantity_fields_name = "";
            Watch.ComparePrice CP = new LUComputers.Watch.ComparePrice();
            CP.CompareDBFields(ltd, ref index_fields_name, ref cost_fields_name, ref quantity_fields_name);
            switch (vt)
            {
                case ViewType.零件总数:
                    sql = " Select * from " + today_table;
                    break;
                case ViewType.新产品:
                    sql = string.Format(@"select * from {0} where {2} not in (select {2} from {1})", today_table, yestoday_table, index_fields_name);
                    break;
                case ViewType.被删除产品:
                    sql = string.Format(@"select * from {1} where {2} not in (select {2} from {0})", today_table, yestoday_table, index_fields_name);
                    break;
                case ViewType.涨价产品:
                    sql = string.Format(@"select * from {1} t1 inner join {0} t2 on t1.{2}=t2.{2} where t2.{3}>t1.{3}", today_table, yestoday_table, index_fields_name, cost_fields_name);
                    break;
                case ViewType.降价产品:
                    sql = string.Format(@"select * from {1} t1 inner join {0} t2 on t1.{2}=t2.{2} where t2.{3}<t1.{3}", today_table, yestoday_table, index_fields_name, cost_fields_name);
                    break;

                case ViewType.已匹配的产品:
                    //sql = string.Format(@"select * from {0} where luc_sku >0 ", today_table);
                    sql = string.Format(@"select  
lu.lu_sku luc_sku, lu.manufacturer_part_number MFP, lu.price lu_price, lu.discount, lu.cost lu_cost, lu.ltd_stock lu_stock
, t2.{4} 
, t2.{3} new_cost, t1.{3} old_cost, t2.{3}- t1.{3},t2.{2}, (t2.{3}* 1.022)-lu.price price_diff, t2.{3}-lu.cost cost_diff  from  {1} t1 inner join {0} t2 on t1.{2}=t2.{2}  left join tb_other_inc_valid_lu_sku lu on lu.lu_sku=t2.luc_sku 
where t2.luc_sku >0", today_table, yestoday_table, index_fields_name, cost_fields_name, quantity_fields_name);
                    break;

                case ViewType.已匹配的新产品:
                    //sql = string.Format(@"select * from {0} where luc_sku >0 and {2} not in (select {2} from {1})", today_table, yestoday_table, index_fields_name);
                    sql = string.Format(@"select  
lu.lu_sku luc_sku, lu.manufacturer_part_number MFP, lu.price lu_price, lu.discount, lu.cost lu_cost, lu.ltd_stock lu_stock
, t.{4} 
, t.{3} new_cost, t.{3} old_cost,t2.{3}- t.{3} ltd_cost_diff, t.{2}, t.{3}-lu.price price_diff, t.{3}-lu.cost cost_diff  from {0} t left join {1} t2 on t.{2}=t2.{2}  left join tb_other_inc_valid_lu_sku lu on lu.lu_sku=t.luc_sku where t.luc_sku >0 and t2.{2} is null ", today_table, yestoday_table, index_fields_name, cost_fields_name, quantity_fields_name);
                    break;
                case ViewType.已匹配的被删除产品:
                    sql = string.Format(@"select * from {1} where luc_sku >0 and {2} not in (select {2} from {0})", today_table, yestoday_table, index_fields_name);
                    break;
                case ViewType.已匹配的涨价产品:
                    sql = string.Format(@"select  
lu.lu_sku luc_sku, lu.manufacturer_part_number MFP, lu.price lu_price, lu.discount, lu.cost lu_cost, lu.ltd_stock lu_stock
, t2.{4} 
, t2.{3} new_cost, t1.{3} old_cost, t2.{3}- t1.{3} ltd_cost_diff,t2.{2}, t2.{3}-lu.price price_diff, t2.{3}-lu.cost cost_diff  from  {1} t1 inner join {0} t2 on t1.{2}=t2.{2}  left join tb_other_inc_valid_lu_sku lu on lu.lu_sku=t2.luc_sku 
where t2.{3}>t1.{3} and t2.luc_sku >0", today_table, yestoday_table, index_fields_name, cost_fields_name, quantity_fields_name);
                    break;
                case ViewType.已匹配的降价产品:
                    sql = string.Format(@"select 
lu.lu_sku luc_sku, lu.manufacturer_part_number MFP, lu.price lu_price, lu.discount, lu.cost lu_cost, lu.ltd_stock lu_stock
, t2.{4} 
, t2.{3} new_cost, t1.{3} old_cost, t2.{3}- t1.{3} ltd_cost_diff,t2.{2}, t2.{3}-lu.price price_diff, t2.{3}-lu.cost cost_diff  from {1} t1 inner join {0} t2 on t1.{2}=t2.{2} left join tb_other_inc_valid_lu_sku lu on lu.lu_sku=t2.luc_sku 
where t2.{3}<t1.{3} and t2.luc_sku >0", today_table, yestoday_table, index_fields_name, cost_fields_name, quantity_fields_name);
                    break;
            }
            return sql;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "luc_sku")
            {
                try
                {
                    decimal discount;
                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["discount"].Value.ToString(), out discount);

                    if (discount > 0M)
                    {
                        MessageBox.Show("Discount");
                        return;
                    }
                    int luc_sku;
                    int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out luc_sku);

                    string ltd_name = this.toolStripComboBox_Ltd.Text;
                    LtdHelper LH = new LtdHelper();
                    int ltd_id = LH.GetLtdIdByText(ltd_name);
                    Ltd ltd = LH.LtdModelByValue(ltd_id);

                    decimal new_cost;
                    decimal.TryParse(dataGridView1.Rows[e.RowIndex].Cells["new_cost"].Value.ToString(), out new_cost);

                    EditPriceToRemote EPTR = new EditPriceToRemote(luc_sku, ltd, new_cost);
                    EPTR.ShowDialog();
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            LtdHelper LH = new LtdHelper();
            string ltd_name = this.toolStripComboBox_Ltd.Text;
            int ltd_id = LH.GetLtdIdByText(ltd_name);
            Ltd ltd = LH.LtdModelByValue(ltd_id);

            DialogResult dr = MessageBox.Show("Are you sure","conform", MessageBoxButtons.OKCancel);
            if (dr == DialogResult.OK)
            {
                this.toolStripProgressBar1.Maximum = dataGridView1.Rows.Count;
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {

                    int lu_sku;
                    int.TryParse((dataGridView1.Rows[i].Cells["luc_sku"].Value ?? "").ToString(), out lu_sku);
                    if (lu_sku > 0)
                    {
                        int menu_child_serial_no = 0;

                        DataTable dt = Config.ExecuteDateTable("Select * from tb_other_inc_valid_lu_sku where lu_sku='" + lu_sku.ToString() + "'");
                        if (dt.Rows.Count > 0)
                        {
                            int.TryParse(dt.Rows[0]["menu_child_serial_no"].ToString(), out menu_child_serial_no);
                        }
                        // 22 CPU Category ID.
                        if (menu_child_serial_no != 22 && menu_child_serial_no != 0 && menu_child_serial_no != 350)
                        {
                            decimal new_cost;
                            decimal.TryParse(dataGridView1.Rows[i].Cells["new_cost"].Value.ToString(), out new_cost);

                            decimal ltd_cost_diff;
                            decimal.TryParse(dataGridView1.Rows[i].Cells["ltd_cost_diff"].Value.ToString(), out ltd_cost_diff);

                            decimal discount;
                            decimal.TryParse(dataGridView1.Rows[i].Cells["discount"].Value.ToString(), out discount);
                            if (discount == 0M && new_cost >1M)
                            {

                                if (Config.ExecuteScalarInt("Select count(*) from tb_dont_update where luc_sku='" + lu_sku.ToString() + "'") == 0)
                                {
                                    //
                                    // Supercom 
                                    if (ltd == Ltd.wholesaler_supercom)
                                    {
                                        decimal special = EditPriceToRemote.GetNewSpecial(new_cost, lu_sku, 0);
                                        decimal new_price = EditPriceToRemote.GetNewPrice(special);

                                        EditPriceToRemote.ExecRemote(new_price, new_cost, special, lu_sku);

                                        this.toolStripStatusLabel1.Text = lu_sku.ToString();
                                        this.toolStripStatusLabel2.Text = new_price.ToString() + "|" + new_cost.ToString();
                                    }
                                    //
                                    // ASI
                                    if (ltd == Ltd.wholesaler_asi)
                                    {

                                        string table_name = LH.GetLastStoreTableName(Ltd.wholesaler_supercom);
                                        if (Config.ExecuteScalarInt("select count(*) from " + table_name + " where luc_sku='" + lu_sku.ToString() + "'") == 0)
                                        {
                                           // table_name = LH.GetLastStoreTableName(Ltd.wholesaler_EPROM);
                                           // if (Config.ExecuteScalarInt("select count(*) from " + table_name + " where luc_sku='" + lu_sku.ToString() + "'") == 0)
                                            {

                                                decimal special = EditPriceToRemote.GetNewSpecial(new_cost, lu_sku, 0);
                                                decimal new_price = EditPriceToRemote.GetNewPrice(special);

                                                EditPriceToRemote.ExecRemote(new_price, new_cost, special, lu_sku);

                                                this.toolStripStatusLabel1.Text = lu_sku.ToString();
                                                this.toolStripStatusLabel2.Text = new_price.ToString() + "|" + new_cost.ToString();
                                            }
                                        }
                                    }
                                  
                                    //
                                    // Smart
                                    if (ltd == Ltd.wholesaler_Smartvision_Direct)
                                    {
                                       
                                                decimal special = EditPriceToRemote.GetNewSpecial(new_cost, lu_sku, 0);
                                                decimal new_price = EditPriceToRemote.GetNewPrice(special);

                                                EditPriceToRemote.ExecRemote(new_price, new_cost, special, lu_sku);

                                                this.toolStripStatusLabel1.Text = lu_sku.ToString();
                                                this.toolStripStatusLabel2.Text = new_price.ToString() + "|" + new_cost.ToString();
                                    
                                    }
                                }
                            }
                        }

                    }

                    this.toolStripProgressBar1.Value = i + 1;
                    this.statusStrip1.Update();

                }
            }
        }



    }

    public enum ViewType
    {
        零件总数,
        新产品,
        被删除产品,
        涨价产品,
        降价产品,
        已匹配的产品,
        已匹配的新产品,
        已匹配的被删除产品,
        已匹配的涨价产品,
        已匹配的降价产品,

    }
}
