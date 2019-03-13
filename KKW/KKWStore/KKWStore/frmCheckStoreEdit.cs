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
    public partial class frmCheckStoreEdit : Form
    {
        public enum SnType
        {
            None,
            Normal,
            NotInStore,
            NotInCheck
        }

        public class SnItem
        {
            public string SN { get; set; }

            public SnType SnType { get; set; }
        }

        public class DiffItem
        {
            public int Pid { get; set; }

            public List<string> Sns { get; set; }
        }

        db.qstoreEntities context = new db.qstoreEntities();
        List<string> _autoSns = new List<string>();
        int _currentCheckRecordID = -1;
        List<db.tb_warehouse> _warehouseList = null;

        List<SnItem> _allSnFromStore = new List<SnItem>();
        List<string> _allSnFromCheck = new List<string>();

        public frmCheckStoreEdit(int id, List<string> sns)
        {
            _currentCheckRecordID = id;
            _autoSns = sns;
            InitializeComponent();
            this.Shown += new EventHandler(frmCheckStoreEdit_Shown);
        }

        void frmCheckStoreEdit_Shown(object sender, EventArgs e)
        {
            InitWarehouse();
            this.txt_staff.Text = Helper.Config.CurrentUser.user_name;//.StaffName;

            if (_currentCheckRecordID > 0)
            {
                this.button_cancel.Enabled = false;
                this.button_report.Enabled = false;
                this.button_save.Enabled = false;
                this.txt_input.Enabled = false;
                this.buttonAutoRun.Enabled = Helper.Config.IsAdmin;

                var all = context.tb_check_store_detail.Where(p =>
                    p.ParentID.Equals(_currentCheckRecordID)).ToList();

                this._allSnFromStore = (from c in all
                                        where c.SnType != (int)SnType.None
                                        select new SnItem
                                        {
                                            SN = c.SerialNo,
                                            SnType = ((SnType)Enum.Parse(typeof(SnType), c.SnType.ToString()))
                                        }).ToList();
                this._allSnFromCheck = (from c in all
                                        where c.SnType == (int)SnType.None
                                        select new
                                        {
                                            c.SerialNo
                                        }).ToList().Select(p => p.SerialNo).ToList();
                AnalyzeTheData();
            }
            if (_autoSns.Count > 0)
            {
                AnalyzeTheData();
                button_save_exist_Click(null, null);
                buttonAutoRun_Click(null, null);
            }
        }

        void AnalyzeTheData()
        {
            ClearTextBox();
            string oneSN = "";

            if (_currentCheckRecordID < 1)
            {
                if (_autoSns.Count > 0)
                {
                    _allSnFromCheck = _autoSns;
                }
                else
                {
                    var snsStr = this.txt_input.Text.Trim().Replace("\r\n", "|").Split(new char[] { '|' }).ToList();
                    this._allSnFromCheck.Clear();
                    foreach (var sn in snsStr)
                    {
                        if (!string.IsNullOrEmpty(sn.Trim()))
                        {
                            _allSnFromCheck.Add(sn.Trim());
                        }
                    }
                }
            }

            if (_allSnFromCheck.Count == 0)
            {
                MessageBox.Show("请输入条码；", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.txt_input.Focus();
                return;
            }
            if (!db.SerialNoAndPCodeModel_p.IsSingleProduct(context, _allSnFromCheck.ToArray()))
            {
                MessageBox.Show("输入的条形码集合不是同一个产品!", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                var snDiffs = GenerateDiff(context, _allSnFromCheck);
                ShowDiff(context, snDiffs);
                return;
            }

            oneSN = _allSnFromCheck[0];

            if (checkedListBox1.CheckedItems.Count != 1)
            {
                MessageBox.Show("只能选择一个仓库");
                return;
            }
            var whareHouseName = checkedListBox1.CheckedItems[0].ToString();
            var p_id = db.SerialNoAndPCodeModel_p.GetProductId(context, oneSN);
            txtProdName.Text = context.tb_product.Single(p => p.id.Equals(p_id)).p_name;
            //
            // 取得有
            var allDt = db.SerialNoAndPCodeModel_p.GetValidSNByProdId(p_id, _warehouseList.Single(p => p.WarehouseName.Equals(whareHouseName)).ID);


            _allSnFromStore = new List<SnItem>();
            foreach (DataRow dr in allDt.Rows)
            {
                //AllModels.Add(new db.tb_check_store_detail()
                //{
                //    flag = 2,   // 不存在
                //    p_name = "",
                //    p_code = dr["p_code"].ToString(),
                //    p_cost = decimal.Parse(dr["cost"].ToString()),
                //    SerialNo = dr["SerialNo"].ToString()
                //});
                var sn = dr["SerialNo"].ToString();
                var snType = SnType.None;
                if (_allSnFromCheck.Distinct().SingleOrDefault(p => p.Equals(sn)) != null)
                {
                    snType = SnType.Normal;
                }
                else
                {
                    snType = SnType.NotInCheck;
                }

                _allSnFromStore.Add(new SnItem
                {
                    SN = sn,
                    SnType = snType
                });
            }

            var allStoreCount = 0M;
            var allCheckCount = 0M;
            var nomalCount = 0M;
            var notInCheckCount = 0M;
            var notInStoreCount = 0M;

            this.richTextBox_all_store_1.Text = string.Join("\r\n", _allSnFromStore.Select(p => p.SN));
            allStoreCount = AllStoreCount();
            this.label_all_store_1.Text = string.Concat("仓库所有数量：", allStoreCount);

            this.richTextBox_check_all_2.Text = string.Join("\r\n", _allSnFromCheck);
            allCheckCount = AllCheckCount();
            this.label_CheckAll_2.Text = string.Concat("盘点所有数量：", allCheckCount);

            this.richTextBox_check_nomal_3.Text = string.Join("\r\n", _allSnFromStore.Where(p => p.SnType.Equals(SnType.Normal)).Select(p => p.SN));
            nomalCount = NormalCount();
            this.label_Normal_3.Text = string.Concat("正常：", nomalCount);

            this.richTextBox_no_in_check_5.Text = string.Join("\r\n", _allSnFromStore.Where(p => p.SnType.Equals(SnType.NotInCheck)).Select(p => p.SN));
            notInCheckCount = NotInCheckCount();
            this.label_no_in_check_5.Text = string.Concat("漏检查数量", notInCheckCount);

            var asn = _allSnFromStore.Select(p => p.SN).ToList();
            this.richTextBox_no_in_store_4.Text = string.Join("\r\n", _allSnFromCheck.Where(p => !asn.Contains(p)).ToList());
            notInStoreCount = NotInStoreCount();
            this.label_no_in_store_4.Text = string.Concat("不在仓库中", notInStoreCount);

            if (allCheckCount == allStoreCount &&
                allStoreCount == nomalCount &&
                notInStoreCount == 0 &&
                notInCheckCount == 0)
            {
                this.label_status.Text = "正常";
                this.label_status.ForeColor = Color.Green;

            }
            else
            {
                this.label_status.Text = "异常";
                this.label_status.ForeColor = Color.Red;
            }

        }

        void InitWarehouse()
        {
            _warehouseList = context.tb_warehouse.ToList();
            foreach (var item in _warehouseList)
            {
                this.checkedListBox1.Items.Add(item.WarehouseName, item.ID.Equals(1) ? true : false);
            }
        }

        private void button_report_Click(object sender, EventArgs e)
        {
            AnalyzeTheData();

            this.button_save.Enabled = true;
            this.button_cancel.Enabled = true;
        }

        int AllStoreCount()
        {
            return _allSnFromStore.Count;
        }

        int AllCheckCount()
        {
            return _allSnFromCheck.Count;
        }

        int NormalCount()
        {
            return _allSnFromStore.Where(p => p.SnType.Equals(SnType.Normal)).Count();
        }

        int NotInCheckCount()
        {
            return _allSnFromStore.Where(p => p.SnType.Equals(SnType.NotInCheck)).Count();
        }

        int NotInStoreCount()
        {
            var asn = _allSnFromStore.Select(p => p.SN).ToList();
            return _allSnFromCheck.Where(p => !asn.Contains(p)).Count();
        }

        void ClearTextBox()
        {
            this.richTextBox_all_store_1.Text = "";
            this.richTextBox_check_all_2.Text = "";
            this.richTextBox_check_nomal_3.Text = "";
            this.richTextBox_no_in_check_5.Text = "";
            this.richTextBox_no_in_store_4.Text = "";

            this.label_all_store_1.Text = "";
            this.label_CheckAll_2.Text = "";
            this.label_no_in_check_5.Text = "";
            this.label_no_in_store_4.Text = "";
            this.label_Normal_3.Text = "";
            this.label_status.Text = "";
        }

        //void GenerateReport()
        //{

        //    System.Text.StringBuilder sb_exist = new StringBuilder();
        //    System.Text.StringBuilder sb_no_exist = new StringBuilder();
        //    System.Text.StringBuilder sb_no_exist_err = new StringBuilder();
        //    foreach (var a in AllModels)
        //    {
        //        if (a.flag == 1)
        //        {
        //            _existCount += 1;
        //            sb_exist.Append(a.SerialNo + "\t" + a.p_code + "\t" + a.p_name + "\t" + a.p_cost.ToString() + "\r\n");
        //        }
        //        else if (a.flag == 2)
        //        {
        //            _noExistCount += 1;
        //            sb_no_exist.Append(a.SerialNo + "\t" + a.p_code + "\t" + a.p_name + "\t" + a.p_cost.ToString() + "\r\n");
        //        }
        //        else
        //        {
        //            _no_exist_err_count += 1;
        //            sb_no_exist_err.Append(a.SerialNo + "\r\n");
        //        }
        //    }
        //    System.Text.StringBuilder sb = new StringBuilder();
        //    sb.Append(string.Format(" 共有{0} 个商品，{1} 个商品不存在，{2} 个条码不在仓库中 \r\n"
        //        , _existCount
        //        , _noExistCount
        //        , _currentID > -1 ? _no_exist_err_count : NoStoreSeriaNOS.Count));

        //    if (_noExistCount > 0)
        //    {
        //        sb.Append("\r\n==============================================================\r\n");
        //        sb.Append(_noExistCount.ToString() + " 个商品不存在\r\n\r\n");
        //        sb.Append(sb_no_exist.ToString());
        //    }

        //    if (_currentID > -1)
        //    {
        //        if (_no_exist_err_count > 0)
        //        {
        //            sb.Append("\r\n==============================================================\r\n");
        //            sb.Append(_no_exist_err_count.ToString() + " 个条码不在仓库中\r\n");
        //            sb.Append(sb_no_exist_err.ToString() + "\r\n");
        //        }
        //    }
        //    else
        //    {
        //        if (NoStoreSeriaNOS.Count > 0)
        //        {
        //            sb.Append("\r\n==============================================================\r\n");
        //            sb.Append(NoStoreSeriaNOS.Count.ToString() + " 个条码不在仓库中\r\n");
        //            foreach (var s in NoStoreSeriaNOS)
        //                sb.Append(s.ToString() + "\r\n");
        //        }
        //    }
        //    this.richTextBox_all_store_1.Text = sb.ToString();
        //}

        private void button_save_exist_Click(object sender, EventArgs e)
        {
            try
            {
                if (_autoSns.Count == 0)
                {
                    if (MessageBox.Show("确认保存？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                    {
                        return;
                    }
                }
                if (this.txt_staff.Text == "")
                {
                    MessageBox.Show("请输入经手人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //frmConfirmPwd f = new frmConfirmPwd();
                //if (!Helper.Config.IsAdmin)
                //{
                //    MessageBox.Show("你没有权限，无法操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}

                //CheckStoreModel csm = new CheckStoreModel();
                if (this._allSnFromStore.Count < 1 && this._allSnFromCheck.Count < 1)
                {
                    return;
                }
                var csm = new db.tb_check_store
                {
                    check_regdate = DateTime.Now,
                    AutoRunStatus = AllStoreCount() == NormalCount() ? (int)enums.CheckAutoRunStatus.Finish : (int)enums.CheckAutoRunStatus.Wait,
                    comment = this.txtProdName.Text,
                    regdate = DateTime.Now,
                    err_quantity = NotInCheckCount() + NotInStoreCount(),
                    staff = this.txt_staff.Text,
                    CheckStatus = AllStoreCount() == NormalCount() ? (int)enums.CheckResultStatus.Normal : (int)enums.CheckResultStatus.Faild,
                    valid_quantity = NormalCount()
                };

                context.tb_check_store.Add(csm);
                context.SaveChanges();
                _currentCheckRecordID = csm.id;
                var snOne = _allSnFromStore.Count > 0 ? _allSnFromStore[0].SN : _allSnFromCheck[0];

                var p_id = db.SerialNoAndPCodeModel_p.GetProductId(context, snOne);
                var prod = context.tb_product.Single(p => p.id.Equals(p_id));

                foreach (var a in _allSnFromStore)
                {
                    var sn = a.SN;
                    AddToDBCache(sn, a.SnType, prod, csm.id);

                    // 数据库有条码，但盘点时没输入
                    //if (a.flag == 2)
                    //{
                    //    db.SerialNoAndPCodeModel_p.AutoOut(context, a.SerialNo, txt_staff.Text);
                    //}
                }
                foreach (var sn in _allSnFromCheck)
                {
                    AddToDBCache(sn, SnType.None, prod, csm.id);
                }
                context.SaveChanges();

                //foreach (var nos in NoStoreSeriaNOS)
                //{
                //    //CheckStoreDetailModel csdm = new CheckStoreDetailModel();
                //    var csdm = new db.tb_check_store_detail();
                //    csdm.flag = 3;
                //    csdm.ParentID = csm.id;
                //    csdm.SerialNo = nos;
                //    //csdm.Create();
                //    csdm.regdate = DateTime.Now;
                //    context.AddTotb_check_store_detail(csdm);
                //    context.SaveChanges();
                //}



                this.DialogResult = System.Windows.Forms.DialogResult.Yes;
                if (_autoSns.Count == 0)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
                MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void AddToDBCache(string sn, SnType snType, db.tb_product prod, int parentId)
        {

            var snModel = context.tb_serial_no_and_p_code.Single(p => p.SerialNO.Equals(sn));
            var csdm = new db.tb_check_store_detail
            {
                p_code = prod.p_code,
                p_cost = snModel.in_cost.Value,
                p_name = prod.p_name,
                ParentID = parentId,
                regdate = DateTime.Now,
                p_id = prod.id,
                SerialNo = sn,
                SnType = (int)snType

            };

            context.tb_check_store_detail.Add(csdm);
        }

        private void button_exist_no_save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认不保存退出", "警告", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                this.Close();
            }
        }

        private void btn_select_user_Click(object sender, EventArgs e)
        {
            frmStaffSelected fss = new frmStaffSelected();
            fss.StartPosition = FormStartPosition.CenterParent;
            fss.ShowDialog();
            this.txt_staff.Text = Helper.TempInfo.StaffName;
        }

        private void txt_input_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text.Replace("\r\n", "[]");
            TB.Text = Helper.CharacterHelper.ToDBC(text).Replace("[]", "\r\n");
        }


        List<DiffItem> GenerateDiff(db.qstoreEntities context, List<string> sns)
        {
            var result = new List<DiffItem>();
            foreach (var sn in sns)
            {
                if (string.IsNullOrEmpty(sn))
                {
                    continue;
                }
                var snModel = context.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(sn));
                var existModel = result.SingleOrDefault(p => p.Pid.Equals(snModel.p_id.Value));
                if (existModel == null)
                {
                    result.Add(new DiffItem()
                    {
                        Pid = snModel.p_id.Value,
                        Sns = new List<string>() { sn }
                    });
                }
                else
                {
                    existModel.Sns.Add(snModel.SerialNO);
                }
            }
            return result;
        }

        void ShowDiff(db.qstoreEntities context, List<DiffItem> diffModels)
        {
            var prodIds = diffModels.Select(p => p.Pid).Distinct().ToList();
            var count = prodIds.Count;
            for (int i = 0; i < count; i++)
            {
                if (i >= 5)
                {
                    return;
                }
                var pid = prodIds[i];
                var snsString = string.Join("\r\n", diffModels.Single(p => p.Pid.Equals(pid)).Sns);
                var prod = context.tb_product.Single(p => p.id.Equals(pid));
                if (i == 0)
                {
                    this.label_all_store_1.Text = prod.p_code;
                    this.richTextBox_all_store_1.Text = snsString;
                }
                else if (i == 1)
                {
                    this.label_CheckAll_2.Text = prod.p_code;
                    this.richTextBox_check_all_2.Text = snsString;
                }
                else if (i == 2)
                {
                    this.label_Normal_3.Text = prod.p_code;
                    this.richTextBox_check_nomal_3.Text = snsString;
                }
                else if (i == 3)
                {
                    this.label_no_in_store_4.Text = prod.p_code;
                    this.richTextBox_no_in_store_4.Text = snsString;
                }
                else if (i == 4)
                {
                    this.label_no_in_check_5.Text = prod.p_code;
                    this.richTextBox_no_in_check_5.Text = snsString;
                }
            }
        }

        private void buttonAutoRun_Click(object sender, EventArgs e)
        {
            if (!Helper.Config.IsAdmin)
            {
                MessageBox.Show("您不是管理员，无法进行此操作");
                return;
            }

            if (MessageBox.Show("确定执行条码进出操作？？？", "提示", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.Yes)
            {
                this.buttonAutoRun.Enabled = false;

                // 出库
                var outSN = this.richTextBox_no_in_check_5.Text.Trim();
                AutoRunOut(outSN.Split(new char[] { '\n' }).ToList());
                // 入库
                var inSns = this.richTextBox_no_in_store_4.Text.Trim();
                AutoRunIn(inSns.Split(new char[] { '\n' }).ToList());

                var checkModel = context.tb_check_store.Single(p => p.id.Equals(_currentCheckRecordID));
                checkModel.AutoRunStatus = (int)enums.CheckAutoRunStatus.Finish;
                context.SaveChanges();
                MessageBox.Show("操作完成");
                this.Close();
            }
        }

        // 自动出库
        void AutoRunOut(List<string> sns)
        {
            foreach (var sn in sns)
            {
                if (string.IsNullOrEmpty(sn))
                {
                    continue;
                }
                var snModel = context.tb_serial_no_and_p_code.Single(p => p.SerialNO.Equals(sn));
                snModel.out_regdate = DateTime.Now;
                snModel.Comment = string.Concat(Helper.Config.CurrentUser.user_name, " 盘点自动出库");
            }
            context.SaveChanges();
        }

        // 自动入库
        void AutoRunIn(List<string> sns)
        {
            foreach (var sn in sns)
            {
                if (!string.IsNullOrEmpty(sn.Trim()))
                {
                    var snModel = context.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(sn));
                    if (snModel == null)
                    {
                        var otherSn = _allSnFromStore[0].SN;
                        var otherSnModel = context.tb_serial_no_and_p_code.Single(p => p.SerialNO.Equals(otherSn));
                        var pId = otherSnModel.p_id;
                        var prod = context.tb_product.Single(p => p.id.Equals(pId.Value));
                        var invoiceCode = db.InInvoiceModel_p.NewInvoiceCode();
                        var inInvoiceModel = new db.tb_in_invoice
                        {
                            curr_warehouse_id = 1,
                            input_regdate = DateTime.Now,
                            regdate = DateTime.Now,
                            note = "盘点自动入库",
                            staff = this.txt_staff.Text,
                            invoice_code = invoiceCode,
                            pay_method = "盘点",
                            pay_total = 0,
                            summary = "盘点",
                            Supplier = "盘点"
                        };
                        context.tb_in_invoice.Add(inInvoiceModel);
                        context.SaveChanges();

                        var inInvoiceProdModel = new db.tb_in_invoice_product
                        {
                            cost = prod.p_cost,
                            in_invoice_code = invoiceCode,
                            in_invoice_id = inInvoiceModel.id,
                            p_code = prod.p_code,
                            p_id = prod.id,
                            quantity = 1,
                            regdate = DateTime.Now
                        };
                        context.tb_in_invoice_product.Add(inInvoiceProdModel);
                        context.SaveChanges();
                    }
                    else
                    {
                        if (snModel.out_regdate.Value.ToString("yyyy-MM-dd") != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            if (snModel.out_regdate.HasValue && snModel.out_regdate.Value.Year > 2000)
                            {
                                snModel.out_regdate = DateTime.Parse("0001-01-01");
                                snModel.Comment = "盘点自动入库";
                            }
                        }
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
