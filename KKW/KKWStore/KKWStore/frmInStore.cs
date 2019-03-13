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
    public partial class frmInStore : Form
    {
        const int IndexCode = 0;
        const int IndexName = 1;
        const int IndexQuantity = 2;
        const int IndexCost = 3;
        const int IndexTotal = 4;
        const int IndexComment = 5;
        string CurrentDetailParentCode = "";
        const int _curr_warehouse_id = 1;
        db.qstoreEntities context = new db.qstoreEntities();

        List<InputProduct> StoreList = new List<InputProduct>();

        public static db.tb_user StoreUserModel = new db.tb_user();
        public static string PayMethodText = "";
        public static string SupplierText = "";

        public frmInStore()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmInStore_Shown);
        }

        void frmInStore_Shown(object sender, EventArgs e)
        {
            this.txt_staff.Text = Helper.Config.CurrentUser.user_name;//.StaffName;
            StoreList = new List<InputProduct>();
            StoreUserModel = new db.tb_user();
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
                dataGridView1[IndexCost, i].Value = StoreList[i].Cost;
                dataGridView1[IndexTotal, i].Value = StoreList[i].Total;
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
            string code = dataGridView1[IndexCode, e.RowIndex].Value.ToString();
            if (code != "")
            {
                CurrentDetailParentCode = code;
                this.button_current_parent_code.Text = code;
                this.txt_detail.Text = "";
                foreach (var p in StoreList)
                {
                    if (p.Code == code && code != "")
                    {
                        if (p.SubCodes == null) continue;
                        foreach (var s in p.SubCodes)
                        {
                            this.txt_detail.Text += s.DetailCode + "\r\n";
                        }
                    }
                }
                this.txt_detail.Text = this.txt_detail.Text.Trim() + "\r\n";
                this.txt_detail.Focus();
                this.txt_detail.Text += "\r\n";
            }
            else
            {
                CurrentDetailParentCode = "";
                this.button_current_parent_code.Text = "";
                this.txt_detail.Text = "";
            }
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
            this.label_total_quantity.Text = string.Format("{0}", quantity.ToString());
            this.label_total.Text = string.Format("{0}", total.ToString());
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
            {
                List<string> existList = new List<string>();
                int validQuantity = 0;
                string[] sns = this.txt_detail.Text.Trim().Replace("\r\n", ",").Split(new char[] { ',' });
                ///this.txt_detail.Text = "";
                this.txt_detail.Text = "";
                List<string> UsedList = new List<string>();

                foreach (var s in sns)
                {
                    if (s.Trim() == "") continue;
                    string sn = s.Trim().Replace("A", "");
                    bool used = false;
                    if (db.SerialNoModel_p.ExistBySerialNO(context, sn, ref used) == true)
                    {
                        if (existList.Contains(s.Trim())) continue;
                        this.txt_detail.Text += sn + "\r\n";
                        validQuantity += 1;
                        existList.Add(sn);
                    }
                    if (used)
                        UsedList.Add(sn);
                }
                if (UsedList.Count > 0)
                {
                    MessageBox.Show(string.Format("{0} 个 SN 已被其他产品使用过", UsedList.Count), "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                for (int i = 0; i < StoreList.Count; i++)
                {
                    if (CurrentDetailParentCode == StoreList[i].Code)
                    {
                        StoreList[i].Quantity = validQuantity;
                        StoreList[i].Total = validQuantity * StoreList[i].Cost;
                        StoreList[i].SubCodes = new List<ProductDetailCode>();
                        foreach (var s in existList)
                        {
                            StoreList[i].SubCodes.Add(new ProductDetailCode()
                            {
                                ParentCode = StoreList[i].Code,
                                DetailCode = s
                            });
                        }
                        this.label_detail_quantity.Text = StoreList[i].SubCodes.Count.ToString();
                    }
                }
                BindList();
            }
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
            string code = txt_code_input.Text.Trim();
            bool exist = false;
            foreach (var p in StoreList)
            {
                if (code == p.Code)
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
            CurrentDetailParentCode = code;             // 保存当前产品的编号
            this.button_current_parent_code.Text = code;

            //ProductModel pm = db.ProductModel_p.GetModelByCode(code);
            var pm = db.ProductModel_p.GetModelByCode(context, code);
            if (pm != null)
            {
                InputProduct p = new InputProduct();
                p.id = 0;
                p.p_id = pm.id;
                p.Name = pm.p_name;
                p.Code = pm.p_code;
                p.Cost = pm.p_cost.Value;
                p.Quantity = 0;
                p.Total = 0;
                p.SubCodes = new List<ProductDetailCode>();

                StoreList.Add(p);
                BindList();
                this.txt_detail.Focus();

            }
            else
            {
                MessageBox.Show("未查到此产品信息，请输入.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmProductEdit fpe = new frmProductEdit(-1, code);
                fpe.StartPosition = FormStartPosition.CenterParent;
                if (fpe.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    ProductInputStoreList();

                }
                else
                    this.txt_code_input.Text = "";
            }
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
                        MessageBox.Show("单据编码已被使用。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        this.txt_invoice_code.Focus();
                        return;
                    }

                    string supplier = this.txt_Supplier.Text.Trim();
                    if (supplier.Length < 2)
                    {
                        MessageBox.Show("请输入供货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txt_Supplier.Focus();
                        return;
                    }

                    //var iim = new InInvoiceModel();
                    var iim = new db.tb_in_invoice();
                    iim.input_regdate = this.dateTimePicker1.Value;
                    iim.invoice_code = this.txt_invoice_code.Text.Trim();
                    iim.note = this.txt_note.Text.Trim();
                    iim.summary = this.txt_summary.Text.Trim();
                    iim.Supplier = this.txt_Supplier.Text;
                    iim.staff = this.txt_staff.Text;
                    iim.pay_method = this.txt_paymethod.Text;
                    iim.pay_total = decimal.Parse(this.label_total.Text);
                    iim.curr_warehouse_id = _curr_warehouse_id;
                    iim.regdate = DateTime.Now;
                    //iim.Create();
                    context.tb_in_invoice.Add(iim);
                    context.SaveChanges();

                    foreach (var p in StoreList)
                    {

                        //InInvoiceProductModel iipm = new InInvoiceProductModel();
                        var iipm = new db.tb_in_invoice_product();
                        iipm.cost = p.Cost;
                        iipm.p_code = p.Code;
                        iipm.quantity = p.Quantity;
                        iipm.in_invoice_id = iim.id;
                        iipm.in_invoice_code = iim.invoice_code;
                        iipm.p_id = p.p_id;
                        iipm.regdate = DateTime.Now;

                        context.tb_in_invoice_product.Add(iipm);
                        context.SaveChanges();
                        db.ProductModel_p.ChangeQuantity(context, iipm.p_code, p.Cost);  // 变化库存数量

                        foreach (var d in p.SubCodes)
                        {
                            //SerialNoAndPCodeModel snpm = new SerialNoAndPCodeModel();
                            var snpm = new db.tb_serial_no_and_p_code
                            {
                                IsFree = false,
                                curr_warehouse_date = DateTime.Now,
                                curr_warehouse_id = _curr_warehouse_id,
                                Comment = string.Empty,
                                in_cost = p.Cost,
                                in_invoice_code = iim.invoice_code,
                                in_regdate = DateTime.Now,
                                is_order_code = false,
                                is_return = false,
                                IsReturnWholesaler = false,
                                IsReturnWholesaler_regdate = DateTime.MinValue,
                                out_regdate = DateTime.MinValue,
                                p_code = p.Code,
                                SerialNO = d.DetailCode,
                                p_id = p.p_id,
                                regdate = DateTime.Now,
                                return_regdate = DateTime.MinValue

                            };

                            context.tb_serial_no_and_p_code.Add(snpm);
                        }
                        context.SaveChanges();
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void txt_code_input_Validating(object sender, CancelEventArgs e)
        {
            this.txt_code_input.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmSupplierSelected fss = new frmSupplierSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_Supplier.Text = SupplierText;
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
            //TextBox TB = (TextBox)sender;
            //string text = TB.Text;
            //TB.Text = Helper.CharacterHelper.ToDBC(text);
        }


    }

    [Serializable]
    public class InputProduct
    {
        public int p_id { set; get; }
        public int id { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
        public int Quantity { set; get; }
        public decimal Cost { set; get; }
        public decimal Total { get; set; }
        public string Comment { get; set; }

        public List<ProductDetailCode> SubCodes { get; set; }
    }

    [Serializable]
    public class ProductDetailCode
    {
        public string ParentCode { set; get; }
        public string DetailCode { set; get; }
    }
}
