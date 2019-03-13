using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace KKWStore
{
    public partial class frmOut2 : Form
    {
        decimal _total = 0M;
        db.qstoreEntities context = new db.qstoreEntities();

        public frmOut2()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmOut2_Shown);
        }

        void frmOut2_Shown(object sender, EventArgs e)
        {

            var list = context.tb_warehouse.Where(me=>me.showit).ToList();
            comboBoxWarehouse.DataSource = list;
            comboBoxWarehouse.DisplayMember = "WarehouseName";
            comboBoxWarehouse.ValueMember = "ID";

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                var wm = (db.tb_warehouse)comboBoxWarehouse.SelectedItem;
                BindProdList(wm.ID, textBoxKeyword.Text.Trim());
            }
        }

        private void comboBoxWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            var wm = (db.tb_warehouse)comboBoxWarehouse.SelectedItem;

            BindProdList(wm.ID, "");
        }

        void BindProdList(int warehouseID, string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                this.listViewPList.Items.Clear();
                return;
            }
            enums.WarehouseType wt = (enums.WarehouseType)Enum.Parse(typeof(enums.WarehouseType), ((db.tb_warehouse)comboBoxWarehouse.SelectedItem).ID.ToString());
            DataTable _searchDT = db.ProductModel_p.GetModelsByKeyword(keyword, wt);
            listViewPList.Items.Clear();
            foreach (DataRow dr in _searchDT.Rows)
            {
                decimal total;
                decimal.TryParse(dr["total"].ToString(), out total);

                decimal totalQty;
                decimal.TryParse(dr["c"].ToString(), out totalQty);

                decimal agePrice = totalQty == 0M ? 0M : total / totalQty;

                ListViewItem li = new ListViewItem(dr["p_code"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["p_name"].ToString());

                if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                {
                    li.SubItems.Add(total.ToString("#0.00"));
                    li.SubItems.Add(string.Concat(totalQty.ToString(),"/", dr["outCount"].ToString()));
                    li.SubItems.Add(agePrice.ToString("#0.00"));
                }
                else
                {
                    li.SubItems.Add("--");
                    li.SubItems.Add(totalQty.ToString());
                    li.SubItems.Add("--");
                }

                listViewPList.Items.Add(li);

            }
            BindSN(string.Empty);
        }

        void BindSN(string p_code)
        {
            var sns = new List<string>();
            BindSN(p_code, 0, out sns);
        }

        void BindSN(string p_code, int qty, out List<string> sns)
        {
            sns = new List<string>();

            /*  产品编号不能为空 */
            if (string.IsNullOrEmpty(p_code))
            {
                this.listViewSN.Items.Clear();
                this.labelSnQty.Text = "";

                return;
            }

            var wm = (db.tb_warehouse)comboBoxWarehouse.SelectedItem;

            DataTable singelDT = db.SerialNoAndPCodeModel_p.GetValidQuantityByProdCode(p_code, wm.ID);

            /* 上传导入， 不操作UI */
            if (qty > 0)
            {
                if (singelDT.Rows.Count < qty)
                {
                    MessageBox.Show("库存不足，请确认“" + p_code + "”库存数量");
                    listViewOrder.Items.Clear();
                    return;
                }
                else
                {
                    for (int i = 0; i < qty; i++)
                    {
                        sns.Add(singelDT.Rows[i]["SerialNO"].ToString());
                    }
                }
                return;
            }
            listViewSN.Items.Clear();
            for (int i = 0; i < singelDT.Rows.Count; i++)
            {
                DataRow dr = singelDT.Rows[i];
                ListViewItem li = new ListViewItem(dr["SerialNO"].ToString());
                li.Tag = dr["id"].ToString();

                if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                    li.SubItems.Add(dr["in_cost"].ToString());
                else
                    li.SubItems.Add("--");

                li.SubItems.Add(dr["in_regdate"].ToString());
                listViewSN.Items.Add(li);
            }
            ShowCurrProdSnQty();
        }

        void ShowCurrProdSnQty()
        {
            this.labelSnQty.Text = string.Concat("当前仓库选中产品库存数量：", this.listViewSN.Items.Count.ToString());
        }

        private void listViewPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;

            listViewSN.Items.Clear();

            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    BindSN(lv.SelectedItems[0].SubItems[0].Text.ToString());
                }
            }
        }

        private void listViewSN_DoubleClick(object sender, EventArgs e)
        {
            //ListView lv = sender as ListView;
            //if (lv.SelectedItems != null)
            //{
            //    if (lv.SelectedItems.Count == 1)
            //    {
            //        InOrder(lv.SelectedItems[0].SubItems[0].Text.ToString());
            //    }
            //}
        }


        /// <summary>
        /// 进入订单列表
        /// </summary>
        /// <param name="sn"></param>
        void InOrder(int snQty, List<string> sns)
        {

            if (snQty < 1 && sns.Count ==0) return;

            if (listViewSN.Items.Count < snQty)
            {
                MessageBox.Show("请确认销售数量，数量大于库存。");
                return;
            }
            if (sns.Count == 0)
            {
                #region ui 操作，从数量上添加
                for (var i = 0; i < snQty; i++)
                {
                    var sn = this.listViewSN.Items[0].SubItems[0].Text.ToString();

                    var snModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                    if (snModel != null)
                    {
                        if (snModel.out_regdate.HasValue && snModel.out_regdate.Value.Year > 2000)
                        {
                            MessageBox.Show("条码已经出售，请检查！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        enums.WarehouseType wt = (enums.WarehouseType)Enum.Parse(typeof(enums.WarehouseType), snModel.curr_warehouse_id.ToString());
                        var pm = db.ProductModel_p.GetModelByCode(context, snModel.p_code);
                        ListViewItem li = new ListViewItem(sn);
                        li.Tag = snModel.id;
                        li.SubItems.Add(wt.ToString());
                        if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                            li.SubItems.Add(snModel.in_cost.ToString());
                        else
                            li.SubItems.Add("--");
                        li.SubItems.Add(snModel.in_regdate.HasValue ? snModel.in_regdate.Value.ToString("yyyy-MM-dd") : "-");
                        li.SubItems.Add(snModel.p_code);
                        li.SubItems.Add(pm != null ? pm.p_name : "");
                        listViewOrder.Items.Add(li);
                        BindTotal();

                        this.listViewSN.Items[0].Remove();
                        ShowCurrProdSnQty();
                    }
                    else
                    {
                        MessageBox.Show("条码不存在！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                } 
                #endregion
            }
            else
            {
                #region 上传导入
                for (var i = 0; i < sns.Count; i++)
                {
                    var sn = sns[i];

                    var snModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                    if (snModel != null)
                    {
                        if (snModel.out_regdate.HasValue && snModel.out_regdate.Value.Year > 2000)
                        {
                            MessageBox.Show("条码已经出售，请检查！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.listViewOrder.Items.Clear();
                            return;
                        }
                        enums.WarehouseType wt = (enums.WarehouseType)Enum.Parse(typeof(enums.WarehouseType), snModel.curr_warehouse_id.ToString());
                        var pm = db.ProductModel_p.GetModelByCode(context, snModel.p_code);
                        ListViewItem li = new ListViewItem(sn);
                        li.Tag = snModel.id;
                        li.SubItems.Add(wt.ToString());
                        if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                            li.SubItems.Add(snModel.in_cost.ToString());
                        else
                            li.SubItems.Add("--");
                        li.SubItems.Add(snModel.in_regdate.HasValue ? snModel.in_regdate.Value.ToString("yyyy-MM-dd") : "-");
                        li.SubItems.Add(snModel.p_code);
                        li.SubItems.Add(pm != null ? pm.p_name : "");
                        listViewOrder.Items.Add(li);
                    }
                    else
                    {
                        MessageBox.Show("条码不存在！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                BindTotal();
                #endregion
            }
        }
        /// <summary>
        /// 计算总价
        /// </summary>
        void BindTotal()
        {
            _total = 0M;
            decimal count = 0M;
            for (int i = 0; i < listViewOrder.Items.Count; i++)
            {
                if (!string.IsNullOrEmpty(listViewOrder.Items[i].SubItems[0].Text))
                {
                    if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                    {
                        decimal total = 0M;
                        decimal.TryParse(listViewOrder.Items[i].SubItems[2].Text, out total);
                        _total += total;
                    }
                    count++;
                }
            }
            labelQty.Text = count.ToString();
            if (Helper.Config.CurrentUser.is_admin.HasValue && Helper.Config.CurrentUser.is_admin.Value)
                labelTotal.Text = "￥" + _total.ToString("0.00");
            else
                labelTotal.Text = "--";
        }

        private void 移除产品ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView lv = listViewOrder as ListView;
            if (lv.SelectedItems != null)
            {
                if (lv.SelectedItems.Count == 1)
                {
                    lv.SelectedItems[0].Remove();
                    BindTotal();
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认保存出库", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    var iim = new db.tb_out_invoice();
                    iim.input_regdate = DateTime.Now;
                    iim.invoice_code = DateTime.Now.ToString("yyMMddhhmmss");
                    iim.pay_total = _total;
                    iim.Price = _total.ToString();
                    iim.staff = Helper.Config.CurrentUser.user_name;
                    iim.regdate = DateTime.Now;
                    context.tb_out_invoice.Add(iim);
                    context.SaveChanges();

                    int count = 0;
                    for (int i = 0; i < this.listViewOrder.Items.Count; i++)
                    {
                        string sn = this.listViewOrder.Items[i].SubItems[0].Text;
                        if (!string.IsNullOrEmpty(sn))
                        {
                            var iipm = new db.tb_out_invoice_product();
                            var pm = db.ProductModel_p.GetModelByCode(context, this.listViewOrder.Items[i].SubItems[4].Text);
                            iipm.SerialNO = sn;
                            iipm.out_invoice_code = iim.invoice_code;
                            iipm.out_invoice_id = iim.id;
                            iipm.p_id = pm == null ? 0 : pm.id;
                            iipm.regdate = DateTime.Now;
                      
                            db.SerialNoAndPCodeModel_p.OutStore(context, iipm.SerialNO, DateTime.Now);
                            context.tb_out_invoice_product.Add(iipm);
                            count += 1;
                        }
                    }

                    iim.SN_Quantity = count;
                    context.SaveChanges();
                    //
                    //  清空数据                  
                    listViewOrder.Items.Clear();
                    listViewSN.Items.Clear();
                    _total = 0;
                    labelTotal.Text = "0";
                    labelQty.Text = "0";

                    var wm = (db.tb_warehouse)comboBoxWarehouse.SelectedItem;
                    BindProdList(wm.ID, this.textBoxKeyword.Text.Trim());
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void textBoxSN_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (e.KeyValue == 13)
            {
                string sn = tb.Text.Trim();
                var qty = 0;
                int.TryParse(sn, out qty);
                if (qty > 0)
                {
                    InOrder(qty, new List<string>());
                    tb.Text = "";
                }
                else
                {
                    MessageBox.Show("请输入正确的数量", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.txtFilename.Text = openFileDialog1.FileName;

            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {
            if (File.Exists(this.txtFilename.Text.Trim()))
            {
                this.listViewOrder.Items.Clear();
                var dt = Helper.NPOIExcel.ToDataTable(this.txtFilename.Text);
                foreach (DataRow dr in dt.Rows)
                {
                    var code = dr["产品编号"].ToString();
                    var qtyStr = dr["产品数量"].ToString();
                    var qty = 0;
                    int.TryParse(qtyStr, out qty);
                    if (qty > 0 && !string.IsNullOrEmpty(code))
                    {
                        var sns = new List<string>();
                        BindSN(code, qty, out sns);
                        InOrder(0, sns);
                    }
                }
            }
            else
            {
                MessageBox.Show("请先上传文件。");
            }
        }
    }
}
