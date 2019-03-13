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
    public partial class frmOutStore : Form
    {
        bool IsTest = true;

        db.qstoreEntities context = new db.qstoreEntities();
        const int IndexCode = 0;
        const int IndexName = 1;
        const int IndexCost = 2;
        List<InputProduct> StoreList = new List<InputProduct>();
        List<InputProduct> freeList = new List<InputProduct>();
        int CurrentInvoiceID = -1;
        db.tb_out_invoice iim = new db.tb_out_invoice();

        DateTime _begin;
        bool _isChange = false;
        int _pressIndex = -1;

        public frmOutStore(int id)
        {
            CurrentInvoiceID = id;

            InitializeComponent();

            this.Shown += new EventHandler(frmOutStore_Shown);
        }

        void frmOutStore_Shown(object sender, EventArgs e)
        {
            this.txt_staff.Text = Helper.Config.CurrentUser.user_name;//.StaffName;
            frmInStore.StoreUserModel = new db.tb_user();// new UserModel();
            frmInStore.PayMethodText = "";
            frmInStore.SupplierText = "";
            StoreList = new List<InputProduct>();

            this.txt_invoice_code.Text = DateTime.Now.ToString("yyMMddhhmmss");
            dataGridView1.Rows.Add(40);
            BindNullList();

            if (CurrentInvoiceID > 0)
            {
                BindControls();
            }

            if (!Helper.PermantentHelper.Ok(enums.PermanentModel.产品出库))
            {
                MessageBox.Show("你没有权限操作此功能", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        void BindControls()
        {
            if (CurrentInvoiceID > 0)
            {
                var outim = context.tb_out_invoice.Single(p => p.id.Equals(CurrentInvoiceID));// OutInvoiceModel.GetOutInvoiceModel(CurrentInvoiceID);
                this.txt_address.Text = outim.ReceiverAddress;
                this.txt_detail.Text = "";// 快递
                this.txt_invoice_code.Text = outim.invoice_code;
                this.txt_note.Text = outim.note;
                this.txt_paymethod.Text = outim.pay_method;
                this.txt_staff.Text = outim.staff;
                this.txt_summary.Text = outim.summary;
                this.txt_Supplier.Text = outim.ReceiverName;
                this.txt_total.Text = outim.pay_total.ToString();
                this.txt_mobile.Text = outim.ReceiverMobile;
                dateTimePicker1.Value = outim.input_regdate.Value;

                string shipingSNs = "";
                var outimShippings = db.OutInvoiceProductShippingModel_p.GetModelsByInvoiceCode(context, CurrentInvoiceID);
                foreach (var shipping in outimShippings)
                {
                    shipingSNs += shipping.ShippingCode + "\r\n";
                }
                this.txt_detail.Text = shipingSNs;

                DataTable dt = db.OutInvoiceProductModel_p.GetModelsByOutID(CurrentInvoiceID);

                foreach (DataRow dr in dt.Rows)
                {
                    StoreList.Add(new InputProduct()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        Code = dr["SerialNO"].ToString(),
                        Name = dr["p_name"].ToString(),
                        Cost = decimal.Parse(dr["p_cost"].ToString())
                    });
                }

                BindList();
            }
        }

        /// <summary>
        /// 绑定空行
        /// </summary>
        void BindNullList()
        {
            for (int i = 1; i <= 40; i++)
            {
                dataGridView1[IndexCode, i].Value = "";
                dataGridView1[IndexName, i].Value = "";
                dataGridView1[IndexCost, i].Value = "";
            }
        }
        /// <summary>
        /// 
        /// </summary>
        void BindList()
        {
            BindNullList();

            for (int i = 0; i < StoreList.Count; i++)
            {
                dataGridView1[IndexCode, i].Value = StoreList[i].Code;
                dataGridView1[IndexName, i].Value = StoreList[i].Name;
                dataGridView1[IndexCost, i].Value = StoreList[i].Cost;
            }
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
                if (this.dataGridView1[IndexCode, i].Value != null)
                {
                    if (this.dataGridView1[IndexCode, i].Value.ToString().Length < 1) continue;
                    quantity += 1;
                }
                if (null != this.dataGridView1[IndexCost, i].Value)
                {
                    decimal _total = 0M;
                    decimal.TryParse(this.dataGridView1[IndexCost, i].Value.ToString(), out _total);
                    total += _total;
                }
            }
            this.label_total_quantity.Text = string.Format("{0}", quantity.ToString());
            this.label_total.Text = string.Format("{0}", total.ToString());
        }

        private void btn_select_user_Click(object sender, EventArgs e)
        {
            frmStaffSelected fss = new frmStaffSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_staff.Text = Helper.TempInfo.StaffName;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmPayMethodSelected fss = new frmPayMethodSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            if (fss.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                this.txt_paymethod.Text = frmInStore.PayMethodText;
            }
        }

        // 保存订单
        private void btn_exist_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认保存出货单", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    string supplier = this.txt_Supplier.Text.Trim();
                    if (supplier.Length < 2)
                    {
                        MessageBox.Show("请输入购买单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.txt_Supplier.Focus();
                        return;
                    }
                    if (CurrentInvoiceID > 0)
                    {
                        iim = context.tb_out_invoice.Single(p => p.id.Equals(CurrentInvoiceID));// OutInvoiceModel.GetOutInvoiceModel(CurrentInvoiceID);
                    }
                    else
                    {
                        iim = new db.tb_out_invoice();// new OutInvoiceModel();
                    }
                    iim.input_regdate = this.dateTimePicker1.Value;
                    iim.invoice_code = this.txt_invoice_code.Text.Trim();
                    iim.note = this.txt_note.Text.Trim();
                    iim.summary = this.txt_summary.Text.Trim();
                    iim.ReceiverName = this.txt_Supplier.Text;
                    iim.staff = this.txt_staff.Text;
                    iim.pay_method = this.txt_paymethod.Text;
                    decimal pay_total;
                    decimal.TryParse(this.txt_total.Text, out pay_total);
                    iim.pay_total = pay_total;
                    iim.ReceiverAddress = this.txt_address.Text;
                    iim.ReceiverMobile = this.txt_mobile.Text;
                    iim.Price = pay_total.ToString();
                    iim.is_Taobao = false;

                    if (CurrentInvoiceID < 1)
                    {
                        iim.regdate = DateTime.Now;
                        //iim.Create();
                        context.tb_out_invoice.Add(iim);
                        context.SaveChanges();
                    }

                    // 
                    // 保存出售的产品SN
                    if (CurrentInvoiceID > 0)   // 如果是修改，删除已存在的。
                    {
                        //OutInvoiceProductModel.DeleteAll("out_invoice_code='" + iim.invoice_code + "'");
                        var query = context.tb_out_invoice_product.Where(p => p.out_invoice_code.Equals(iim.invoice_code)).ToList();
                        foreach (var item in query)
                        {
                            context.tb_out_invoice_product.Remove(item);
                        }
                    }

                    int count = 0;
                    for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
                    {
                        if (StoreList.Count < 1)
                        {
                            continue;
                        }
                        var iipm = new db.tb_out_invoice_product();// new OutInvoiceProductModel();
                        if (dataGridView1[IndexCode, i] == null)
                        {
                            continue;
                        }
                        iipm.SerialNO = dataGridView1[IndexCode, i].Value.ToString();
                        iipm.out_invoice_code = iim.invoice_code;
                        iipm.out_invoice_id = iim.id;
                        iipm.IsFree = false;
                        iipm.regdate = DateTime.Now;
                        iipm.is_return = false;
                        foreach (var ip in StoreList)
                        {
                            if (ip.Code == iipm.SerialNO)
                            {
                                iipm.p_id = ip.id;
                                break;
                            }
                        }

                        if (iipm.SerialNO.Trim().Length < 1) continue;
                        db.SerialNoAndPCodeModel_p.OutStore(context, iipm.SerialNO, DateTime.Now);
                        context.tb_out_invoice_product.Add(iipm);

                        count += 1;
                    }

                    for (int i = 0; i < this.freeList.Count; i++)
                    {

                        var iipm = new db.tb_out_invoice_product();
                        iipm.IsFree = true;
                        iipm.SerialNO = this.freeList[i].Code;
                        iipm.out_invoice_code = iim.invoice_code;
                        iipm.out_invoice_id = iim.id;
                        iipm.p_id = freeList[i].p_id;
                        iipm.regdate = DateTime.Now;
                        iipm.is_return = false;
                        if (iipm.SerialNO.Trim().Length < 1) continue;

                        context.tb_out_invoice_product.Add(iipm);
                        db.SerialNoAndPCodeModel_p.OutStore(context, iipm.SerialNO, DateTime.Now);
                        count += 1;
                    }
                    iim.SN_Quantity = count;

                    context.SaveChanges();
                    //
                    // 保存运单号
                    string[] sns = this.txt_detail.Text.Trim().Replace("\r\n", ",").Split(new char[] { ',' });
                    if (CurrentInvoiceID > 0)   // 如果是修改，删除已存在的。
                    {
                        //OutInvoiceProductShippingModel.DeleteAll("out_invoice_code='" + iim.invoice_code + "'");
                        var query = context.tb_out_invoice_product_shipping.Where(p => p.out_invoice_code.Equals(iim.invoice_code)).ToList();
                        foreach (var item in query)
                        {
                            context.tb_out_invoice_product_shipping.Remove(item);
                        }
                        context.SaveChanges();
                    }
                    foreach (var s in sns)
                    {
                        if (s.Trim() == "") continue;

                        var oism = new db.tb_out_invoice_product_shipping();// new OutInvoiceProductShippingModel();
                        oism.ShippingCode = s.Trim();
                        oism.out_invoice_code = iim.invoice_code;
                        oism.out_invoice_id = iim.id;
                        oism.regdate = DateTime.Now;
                        context.tb_out_invoice_product_shipping.Add(oism);
                        context.SaveChanges();
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                    this.Close();
                }
                catch (Exception ex)
                {
                    Helper.Logs.WriteErrorLog(ex);
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView gv = (DataGridView)this.dataGridView1;
                // 编号
                if (e.ColumnIndex == IndexCode)
                {
                    if (_isChange)
                        return;

                    if (gv[e.ColumnIndex, e.RowIndex].Value == null) return;

                    string SN = gv[e.ColumnIndex, e.RowIndex].Value.ToString().Replace("A", "");
                    SN = Helper.CharacterHelper.ToDBC(SN);
                    gv[e.ColumnIndex, e.RowIndex].Value = SN;
                    if (SN.Trim().Length == 0)
                        return;

                    bool isSolded = false;
                    var pm = db.ProductModel_p.GetModelBySN(context, SN, ref isSolded);
                    if (pm == null)
                    {
                        MessageBox.Show("此条型码不在数据库中", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (isSolded)
                    {
                        MessageBox.Show("此商品已出售，如果需要再次出售，请先进行退货处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool exist = false;
                    foreach (var p in StoreList)
                    {
                        if (p.Code == SN)
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        if (StoreList == null)
                            StoreList = new List<InputProduct>();
                        StoreList.Add(new InputProduct()
                        {
                            id = pm.id,
                            Code = SN,
                            Name = pm.p_name,
                            Cost = pm.p_price.Value
                        });
                    }
                    BindList();
                }
                // 单价
                if (e.ColumnIndex == IndexCost)
                {

                }
            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txt_detail_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text.Replace("\r\n", ",");
            TB.Text = Helper.CharacterHelper.ToDBC(text).Replace(",", "\r\n");
        }

        private void txt_total_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }



        private void txt_detail_KeyUp(object sender, KeyEventArgs e)
        {
            if (!IsTest)
            {
                if ((e.KeyValue >= 48 && e.KeyValue <= 57) || (e.KeyValue >= 96 && e.KeyValue < 105) || e.KeyValue == 17 || e.KeyCode == Keys.V)
                {
                    MessageBox.Show("为了保证数据正确，请用扫描枪.", "提示", MessageBoxButtons.OK);
                    txt_detail.Text = "";
                }
            }
        }

        private void txt_detail_MouseUp(object sender, MouseEventArgs e)
        {
            if (!IsTest)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    MessageBox.Show("为了保证数据正确，请用扫描枪.", "提示", MessageBoxButtons.OK);
                    txt_detail.Text = "";
                }
            }
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            System.Windows.Forms.DataGridViewTextBoxEditingControl dgv = (System.Windows.Forms.DataGridViewTextBoxEditingControl)sender;
            bool isClear = false;
            _isChange = false;
            _pressIndex = dgv.Text.ToString().Length;

            if (_pressIndex == 1)
            {

                _begin = DateTime.Now;
            }

            if (_pressIndex >= 7)
            {
                System.TimeSpan sometime = DateTime.Now - _begin;

                if (sometime.Seconds > 1)
                {
                    isClear = true;
                    _isChange = true;
                }
            }
            if (!IsTest)
            {
                if (isClear || e.KeyCode == Keys.V)
                {
                    _isChange = true;
                    MessageBox.Show("为了保证数据正确，请用扫描枪.", "提示", MessageBoxButtons.OK);

                    try
                    {
                        dataGridView1.Rows.Clear();
                        dataGridView1.Rows.Add(40);

                    }
                    catch { }
                }
            }
        }


        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            _isChange = false;
            DataGridView dgv = (DataGridView)sender;
            if (dgv.CurrentCell.ColumnIndex != 0)
                return;
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;
                // 事件处理器删除
                tb.KeyDown -= new KeyEventHandler(dataGridView1_KeyUp);
                tb.MouseDown -= new MouseEventHandler(tb_MouseClick);
                // 添加处理事件
                tb.KeyDown += new KeyEventHandler(dataGridView1_KeyUp);
                tb.MouseDown += new MouseEventHandler(tb_MouseClick);
            }
        }

        void tb_MouseClick(object sender, MouseEventArgs e)
        {
            if (!IsTest)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    MessageBox.Show("为了保证数据正确，请用扫描枪.", "提示", MessageBoxButtons.OK);
                    dataGridView1.CurrentCell.Value = "";
                }
            }
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            // _begin = DateTime.Now;
        }

        //添加赠品
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            var txt = sender as TextBox;
            var sn = txt.Text.Trim();
            if (sn.Length == 0)
            {
                // MessageBox.Show("请输入条码");
            }
            else if (sn.Length == 10)
            {
                bool isSolded = false;
                var pm = db.ProductModel_p.GetModelBySN(context, sn, ref isSolded);
                if (pm == null)
                {
                    MessageBox.Show("此条型码不在数据库中", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (isSolded)
                {
                    MessageBox.Show("此商品已出售，如果需要再次出售，请先进行退货处理", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                bool exist = false;
                foreach (var p in freeList)
                {
                    if (p.Code == sn)
                    {
                        exist = true;
                        break;
                    }
                }
                if (!exist)
                {
                    if (freeList == null)
                        freeList = new List<InputProduct>();
                    freeList.Add(new InputProduct()
                    {
                        id = pm.id,
                        Code = sn,
                        Name = pm.p_name,
                        Cost = pm.p_cost.Value,
                        p_id = pm.id
                    });
                    txt.Text = "";
                }
                BindFreeList();
            }
        }

        //显示赠品
        void BindFreeList()
        {
            var total = 0M;
            this.listViewFree.Items.Clear();
            for (int i = 0; i < freeList.Count; i++)
            {
                ListViewItem li = new ListViewItem(freeList[i].Name);
                li.Tag = freeList[i].id;
                li.SubItems.Add(freeList[i].Cost.ToString());
                total += freeList[i].Cost;
                this.listViewFree.Items.Add(li);
            }
            // ListTotal();
            this.labelFreeQty.Text = freeList.Count.ToString();
            this.labelFreeTotal.Text = total.ToString();
        }

        // 删除赠品
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var listView = listViewFree as ListView;

            for (int i = 0; i < listView.SelectedItems.Count; i++)
            {
                int id;
                int.TryParse(listView.SelectedItems[i].Tag.ToString(), out id);
                for (int j = 0; j < freeList.Count; j++)
                {
                    if (id == freeList[j].id)
                    {
                        freeList.RemoveAt(j);
                        j--;
                    }
                }
            }
        }

    }
}
