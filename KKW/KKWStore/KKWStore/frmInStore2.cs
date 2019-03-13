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
    /// <summary>
    /// 采购输入 
    /// </summary>
    public partial class frmInStore2 : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        const int IndexCode = 0;
        const int IndexName = 1;
        const int IndexQuantity = 2;
        const int IndexCost = 3;
        const int IndexTotal = 4;
        const int IndexComment = 5;
        int CurrentDetailParentID = 0;
        const int _curr_warehouse_id = (int) enums.WarehouseType.中转_未分配; // 显神仓库

        List<InputProduct> StoreList = new List<InputProduct>();

        public static db.tb_user StoreUserModel = new db.tb_user();
        public static string PayMethodText = "";
        public static string SupplierText = "";
        int _id = 0;

        DataTable _searchDT = new DataTable();
        bool _isView = false;

        public frmInStore2(int id, bool isView)
        {
            _id = id;
            _isView = isView;

            InitializeComponent();
            this.Shown += new EventHandler(frmInStore2_Shown);
        }

        void frmInStore2_Shown(object sender, EventArgs e)
        {
            // this.txt_note.Text = Helper.Config.CurrentUser.user_name;
            this.txt_staff.Text = Helper.Config.CurrentUser.user_name;
            StoreList = new List<InputProduct>();
            StoreUserModel = new db.tb_user();// new UserModel();
            PayMethodText = "";
            SupplierText = "";

            BindNullList();
            this.txt_invoice_code.Text = db.InInvoiceModel_p.NewInvoiceCode();
            var wmodel = context.tb_warehouse.Single(p => p.ID.Equals(_curr_warehouse_id));// WarehouseModel.GetWarehouseModel(_curr_warehouse_id);

            this.labelWarehouse.Text = wmodel != null ? "当前仓库：" + wmodel.WarehouseName : "";
            if (wmodel == null)
            {
                MessageBox.Show("没有当前仓库信息！");
                this.Close();
            }

            this.Text += Helper.Config.CurrentUser.user_name;
            //
            // 修改
            //
            if (_id > 0)
            {
                btn_exist.Text = "修改";
                var inInvoice = context.tb_in_invoice.Single(p => p.id.Equals(_id));// db.InInvoiceModel_p.GetInInvoiceModel(_id);

                if (inInvoice != null)
                {

                    this.dateTimePicker1.Value = inInvoice.input_regdate.Value;
                    this.txt_invoice_code.Text = inInvoice.invoice_code;
                    this.txt_note.Text = inInvoice.note;
                    this.txt_summary.Text = inInvoice.summary;
                    this.txt_Supplier.Text = inInvoice.Supplier;
                    this.txt_staff.Text = inInvoice.staff;
                    this.txt_paymethod.Text = inInvoice.pay_method;
                    this.label_total.Text = inInvoice.pay_total.Value.ToString("0.00");

                    var ipList = context.tb_in_invoice_product.Where(p => p.in_invoice_code.Equals(inInvoice.invoice_code)).ToList();// InInvoiceProductModel.FindAllByProperty("in_invoice_code", inInvoice.invoice_code);// db.InInvoiceProductModel_p.g

                    for (int i = 0; i < ipList.Count; i++)
                    {
                        var pm = context.tb_product.Single(p => p.id.Equals(ipList[i].p_id));// ProductModel.GetProductModel(ipList[i].p_id);
                        var ip = new InputProduct();
                        ip.id = ipList[i].id;
                        ip.p_id = ipList[i].p_id.Value;
                        ip.Name = pm != null ? pm.p_name : "";// ipList[i].p_name;
                        ip.Code = ipList[i].p_code;
                        ip.Cost = ipList[i].cost.Value;
                        ip.Quantity = ipList[i].quantity.Value;
                        ip.Total = ipList[i].cost.Value * ipList[i].quantity.Value;
                        ip.SubCodes = new List<ProductDetailCode>();

                        StoreList.Add(ip);
                    }
                    BindList();
                }

                if (_isView)
                    btn_exist.Enabled = false;
            }
        }

        #region Bind
        /// <summary>
        /// 
        /// </summary>
        void BindList()
        {
            SetNullList();

            for (int i = 0; i < StoreList.Count; i++)
            {

                dataGridView1[IndexCode, i].Value = StoreList[i].Code;
                dataGridView1[IndexName, i].Value = StoreList[i].Name;
                dataGridView1[IndexQuantity, i].Value = StoreList[i].Quantity;
                dataGridView1[IndexCost, i].Value = StoreList[i].Cost.ToString("##0.00");
                dataGridView1[IndexTotal, i].Value = StoreList[i].Total.ToString("##0.00");
                dataGridView1[IndexComment, i].Value = StoreList[i].Comment;
            }
            ListTotal();
        }

        void SetNullList()
        {
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    dataGridView1[j, i].Value = "";
            }
        }
        /// <summary>
        /// 绑定空行
        /// </summary>
        void BindNullList()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("商品编号");
            dt.Columns.Add("商品全名");
            dt.Columns.Add("数量");
            dt.Columns.Add("单价");
            dt.Columns.Add("金额");
            dt.Columns.Add("备注");

            for (int i = 1; i <= 20; i++)
            {
                DataRow dr = dt.NewRow();
                dr["商品编号"] = "";
                dr["商品全名"] = "";
                dr["数量"] = "";
                dr["单价"] = "";
                dr["金额"] = "";
                dr["备注"] = "";
                dt.Rows.Add(dr);
            }
            this.dataGridView1.DataSource = dt;
        }
        #endregion

        private void btn_select_user_Click(object sender, EventArgs e)
        {
            frmStaffSelected fss = new frmStaffSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_staff.Text = Helper.TempInfo.StaffName;
            }
        }
        /// <summary>
        /// 点击行的头部，可选择产品
        /// 把原来输入的code. 再次显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //string code = dataGridView1[IndexCode, e.RowIndex].Value.ToString();
            //if (code != "")
            //{
            //    CurrentDetailParentCode = code;
            //    this.button_current_parent_code.Text = code;
            //    this.txt_detail.Text = "";
            //    foreach (var p in StoreList)
            //    {
            //        if (p.Code == code && code != "")
            //        {
            //            if (p.SubCodes == null) continue;
            //            foreach (var s in p.SubCodes)
            //            {
            //                this.txt_detail.Text += s.DetailCode + "\r\n";
            //            }
            //        }
            //    }
            //    this.txt_detail.Text = this.txt_detail.Text.Trim() + "\r\n";
            //    this.txt_detail.Focus();
            //    this.txt_detail.Text += "\r\n"; 
            //}
            //else
            //{
            //    CurrentDetailParentCode = "";
            //    this.button_current_parent_code.Text = "";
            //    this.txt_detail.Text = "";
            //}
        }

        private void dataGridView1_CellValidated(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView gv = (DataGridView)this.dataGridView1;
            // 编号
            if (e.ColumnIndex == IndexCode)
            {
                if (dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString() == "")
                {
                    if (StoreList.Count > 0)
                    {
                        StoreList.RemoveAt(e.RowIndex);
                    }
                }
                BindList();
            }
            // 数量
            if (e.ColumnIndex == IndexQuantity)
            {
                ChangeCharge(e.RowIndex);
            }
            // 单价
            if (e.ColumnIndex == IndexCost)
            {
                ChangeCharge(e.RowIndex);
            }
            // 金额
            if (e.ColumnIndex == IndexTotal)
            {
                ChangeCharge(e.RowIndex);
            }
        }
        /// <summary>
        /// 变化数量，价格
        /// </summary>
        /// <param name="rowIndex"></param>
        void ChangeCharge(int rowIndex)
        {
            int quantity = 0;
            int.TryParse(this.dataGridView1[IndexQuantity, rowIndex].Value.ToString(), out quantity);

            decimal cost;
            decimal.TryParse(this.dataGridView1[IndexCost, rowIndex].Value.ToString(), out cost);

            //this.dataGridView1[IndexTotal, rowIndex].Value = (quantity * cost).ToString();

            string code = dataGridView1[IndexCode, rowIndex].Value.ToString();
            for (int i = 0; i < StoreList.Count; i++)
            {
                if (code == StoreList[i].Code)
                {

                    StoreList[i].Quantity = quantity;
                    StoreList[i].Cost = cost;
                    StoreList[i].Total = quantity * cost;
                }
            }
            BindList();
            ListTotal();
        }
        /// <summary>
        /// 统计列表总数量的
        /// </summary>
        void ListTotal()
        {
            int quantity = 0;
            decimal total = 0M;

            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                if (this.dataGridView1[2, i].Value != null)
                {
                    int _quantity;

                    int.TryParse(this.dataGridView1[2, i].Value.ToString(), out _quantity);
                    quantity += _quantity;
                }
                if (null != this.dataGridView1[4, i].Value)
                {
                    decimal _total = 0M;
                    decimal.TryParse(this.dataGridView1[4, i].Value.ToString(), out _total);
                    total += _total;
                }
            }
            this.label_total_quantity.Text = string.Format("{0}", quantity.ToString("##0.00"));
            this.label_total.Text = string.Format("{0}", total.ToString("##0.00"));
        }

        private void dataGridView1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {


        }
        /// <summary>
        /// 计算当前产品的数量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_detail_Leave(object sender, EventArgs e)
        {
            //if (e.KeyChar == 13)
            //{
            //    List<string> existList = new List<string>();
            //    int validQuantity = 0;
            //    string[] sns = this.txt_detail.Text.Trim().Replace("\r\n", ",").Split(new char[] { ',' });
            //    ///this.txt_detail.Text = "";
            //    this.txt_detail.Text = "";
            //    List<string> UsedList = new List<string>();

            //    foreach (var s in sns)
            //    {
            //        if (s.Trim() == "") continue;
            //        string sn = s.Trim().Replace("A", "");
            //        bool used = false;
            //        if (db.SerialNoModel_p.ExistBySerialNO(sn, ref used) == true)
            //        {                       
            //            if (existList.Contains(s.Trim())) continue;
            //            this.txt_detail.Text += sn + "\r\n";
            //            validQuantity += 1;
            //            existList.Add(sn);
            //        }
            //        if (used)
            //            UsedList.Add(sn);
            //    }
            //    if (UsedList.Count > 0)
            //    {
            //        MessageBox.Show(string.Format("{0} 个 SN 已被其他产品使用过", UsedList.Count), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //    for (int i = 0; i < StoreList.Count; i++)
            //    {
            //        if (CurrentDetailParentCode == StoreList[i].Code)
            //        {
            //            StoreList[i].Quantity = validQuantity;
            //            StoreList[i].Total = validQuantity * StoreList[i].Cost;
            //            StoreList[i].SubCodes = new List<ProductDetailCode>();
            //            foreach (var s in existList)
            //            {
            //                StoreList[i].SubCodes.Add(new ProductDetailCode()
            //                {
            //                    ParentCode = StoreList[i].Code,
            //                    DetailCode = s
            //                });
            //            }
            //            this.label_detail_quantity.Text = StoreList[i].SubCodes.Count.ToString();
            //        }
            //    }
            //    BindList();
            //}
        }
        /// <summary>
        /// 输入产品SN
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_code_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                ProductInputStoreList();
            }
        }
        /// <summary>
        /// 进入存储列表
        /// </summary>
        void ProductInputStoreList()
        {
            if (listView1.SelectedItems == null) return;
            if (listView1.SelectedItems.Count < 1) return;
            int id = int.Parse(listView1.SelectedItems[0].Tag.ToString());

            bool exist = false;
            foreach (var p in StoreList)
            {
                if (id == p.id)
                {
                    exist = true;
                    break;
                }
            }
            if (exist)
            {
                MessageBox.Show("此产品已在列表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CurrentDetailParentID = id;             // 保存当前产品的编号
            //this.button_current_parent_code.Text = code;

            var pm = context.tb_product.SingleOrDefault(p => p.id.Equals(id));// db.ProductModel_p.GetProductModel(id);
            if (pm != null)
            {
                InputProduct p = new InputProduct();
                p.id = 0;
                p.p_id = pm.id;
                p.Name = pm.p_name;
                p.Code = pm.p_code;
                p.Cost = pm.p_cost.HasValue ? pm.p_cost.Value : 0M;
                p.Quantity = 0;
                p.Total = 0;
                p.SubCodes = new List<ProductDetailCode>();

                StoreList.Add(p);
                BindList();
                // this.txt_detail.Focus();

            }
            //else
            //{
            //    MessageBox.Show("未查到此产品信息，请输入.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    frmProductEdit fpe = new frmProductEdit(-1, code);
            //    fpe.StartPosition = FormStartPosition.CenterParent;
            //    if (fpe.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        ProductInputStoreList();

            //    }
            //    else
            //        this.txt_code_input.Text = "";
            //}
        }

        private void btn_exist_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认保存进货单", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string invoiceCode = this.txt_invoice_code.Text.Trim();
                    if (invoiceCode.Length < 2)
                    {
                        MessageBox.Show("请输入单据编码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txt_invoice_code.Focus();
                        return;
                    }

                    if (db.InInvoiceModel_p.ExistInvoiceCode(invoiceCode))
                    {
                        if (_id < 1)
                        {
                            MessageBox.Show("单据编码已被使用。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            this.txt_invoice_code.Focus();
                            return;
                        }
                    }

                    string supplier = this.txt_Supplier.Text.Trim();
                    if (supplier.Length < 2)
                    {

                        MessageBox.Show("请输入供货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txt_Supplier.Focus();
                        return;

                    }

                    var iim = new db.tb_in_invoice();// new InInvoiceModel();
                    if (_id > 0)
                    {
                        iim = context.tb_in_invoice.Single(p => p.id.Equals(_id));// InInvoiceModel.GetInInvoiceModel(_id);
                    }
                    if (_id < 1)
                    {
                        iim = new db.tb_in_invoice
                        {
                            curr_warehouse_id = _curr_warehouse_id,
                            input_regdate = this.dateTimePicker1.Value,
                            invoice_code = this.txt_invoice_code.Text.Trim(),
                            pay_method = this.txt_paymethod.Text,
                            note = this.txt_note.Text.Trim(),
                            pay_total = decimal.Parse(this.label_total.Text),
                            staff = this.txt_staff.Text,
                            regdate = DateTime.Now,
                            summary = this.txt_summary.Text.Trim(),
                            Supplier = this.txt_Supplier.Text

                        };

                        context.tb_in_invoice.Add(iim);
                    }
                    else
                    {
                        iim.pay_total = decimal.Parse(this.label_total.Text);
                    }
                    context.SaveChanges();

                    foreach (var p in StoreList)
                    {
                        if (_id < 1)
                        {
                            var iipm = new db.tb_in_invoice_product
                            {
                                cost = p.Cost,
                                in_invoice_code = iim.invoice_code,
                                in_invoice_id = iim.id,
                                p_code = p.Code,
                                p_id = p.p_id,
                                quantity = p.Quantity,
                                regdate = DateTime.Now

                            };

                            context.tb_in_invoice_product.Add(iipm);
                            context.SaveChanges();

                            for (int i = 0; i < p.Quantity; i++)
                            {
                                var snpm = new db.tb_serial_no_and_p_code
                                {
                                    IsFree = false,
                                    curr_warehouse_date = DateTime.Now,
                                    curr_warehouse_id = _curr_warehouse_id,
                                    Comment = string.Empty,
                                    in_cost = p.Cost,
                                    in_invoice_code = iim.invoice_code,
                                    in_regdate = dateTimePicker1.Value,
                                    is_order_code = false,
                                    is_return = false,
                                    IsReturnWholesaler = false,
                                    IsReturnWholesaler_regdate = DateTime.MinValue,
                                    out_regdate = DateTime.MinValue,
                                    p_code = p.Code,
                                    SerialNO = "1000000000",
                                    p_id = p.p_id,
                                    regdate = DateTime.Now,
                                    return_regdate = DateTime.MinValue

                                };

                                context.tb_serial_no_and_p_code.Add(snpm);
                                context.SaveChanges();
                            }

                            db.ProductModel_p.ChangeQuantity(context, iipm.p_code, p.Cost);  // 变化库存数量
                        }
                        else
                        {
                            if (p.id < 1)
                                continue;
                            var iipm = context.tb_in_invoice_product.Single(t => t.id.Equals(p.id));//) InInvoiceProductModel.GetInInvoiceProductModel(p.id);
                            iipm.cost = p.Cost;
                            iipm.p_code = p.Code;
                            iipm.quantity = p.Quantity;
                            iipm.in_invoice_id = iim.id;
                            iipm.in_invoice_code = iim.invoice_code;
                            iipm.p_id = p.p_id;
                            
                            //iipm.Update();
                            context.SaveChanges();
                            db.SqlExec.ExecuteNonQuery("Update tb_serial_no_and_p_code set in_cost='" + p.Cost + "' where in_invoice_code='" + iim.invoice_code + "' and p_id='" + p.p_id + "' and p_code='" + p.Code + "'");
                        }
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();

                    //Helper.BalanceHelper.SavePayBalance(context, decimal.Parse(this.label_total.Text),
                    //     DateTime.Now, string.Concat(dateTimePicker1.Value, " 商品入库成本")
                    //     , Helper.Config.CurrentUser.id
                    //     , Helper.Config.CurrentUser.user_name, true);
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }
        }

        //private void txt_code_input_Validating(object sender, CancelEventArgs e)
        //{
        //    this.txt_code_input.Text = "";
        //}

        private void button11_Click(object sender, EventArgs e)
        {
            frmSupplierSelected fss = new frmSupplierSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_Supplier.Text = Helper.TempInfo.Supplier;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmPayMethodSelected fss = new frmPayMethodSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_paymethod.Text = PayMethodText;
            }
        }


        private void txt_detail_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text.Replace("\r\n", ",");
            TB.Text = Helper.CharacterHelper.ToDBC(text).Replace(",", "\r\n");
        }

        private void txt_code_input_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
            listView1.Items.Clear();
            if (TB.Text.Length > 1)
            {

                _searchDT = db.ProductModel_p.GetModelsByKeyword(TB.Text, enums.stock.all, false, null, 0);

                foreach (DataRow dr in _searchDT.Rows)
                {
                    ListViewItem li = new ListViewItem(dr["p_code"].ToString());
                    li.Tag = dr["id"].ToString();
                    li.SubItems.Add(dr["p_name"].ToString());
                    listView1.Items.Add(li);
                }

                if (_searchDT.Rows.Count == 0)
                {
                    ListViewItem li = new ListViewItem("查无数据");
                    li.Tag = -1;
                    li.SubItems.Add("");
                    listView1.Items.Add(li);
                }
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ProductInputStoreList();
        }

        private void 删除选中行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows != null)
            {
                for(int i=0; i< dataGridView1.SelectedRows.Count; i++)
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[i]);
                    i--;
                }
            }
        }
    }
}
