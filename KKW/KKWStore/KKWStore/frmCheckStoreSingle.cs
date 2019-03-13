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
    public partial class frmCheckStoreSingle : Form
    {
        db.qstoreEntities1 context = new db.qstoreEntities1();
        List<db.tb_check_store_detail> AllModels = new List<db.tb_check_store_detail>();
        List<db.tb_check_store_detail> StoreModels = new List<db.tb_check_store_detail>();
        List<string> NoStoreSeriaNOS = new List<string>();
        int _existCount = 0;
        int _noExistCount = 0;
        int _no_exist_err_count = 0;
        int CurrentID = -1;

        public frmCheckStoreSingle(int id)
        {
            CurrentID = id;
            InitializeComponent();
            this.Shown += new EventHandler(frmCheckStoreEdit_Shown);
        }

         

        void frmCheckStoreEdit_Shown(object sender, EventArgs e)
        {
            if (CurrentID > 0)
            {
                this.button_exist_no_save.Enabled = false;
                this.button_report.Enabled = false;
                this.button_save_exist.Enabled = false;

               // CheckStoreDetailModel[] all = CheckStoreDetailModel.FindAllByProperty("ParentID", CurrentID);
                var all = context.tb_check_store_detail.Where(p => p.ParentID.HasValue && p.ParentID.Value.Equals(CurrentID));
                foreach (var cs in all)
                {
                    AllModels.Add(new db.tb_check_store_detail()
                    {
                        id = cs.id,
                        flag = cs.flag,
                        p_code = cs.p_code,
                        p_name = cs.p_name,
                        p_cost = cs.p_cost,
                        SerialNo = cs.SerialNo,
                        ParentID = cs.ParentID
                    });
                }
                GenerateReport();
            }

            this.txt_staff.Text = Helper.Config.CurrentUser.user_name; 
        }
        private void button_report_Click(object sender, EventArgs e)
        {
            //
            // 取得有
            DataTable allDt = db.SerialNoAndPCodeModel_p.GetAllStoreProduct();
            AllModels.Clear();
            NoStoreSeriaNOS.Clear();
            foreach (DataRow dr in allDt.Rows)
            {
                AllModels.Add(new db.tb_check_store_detail()
                {
                    flag = 2,   // 不存在
                    p_name = dr["p_name"].ToString(),
                    p_code = dr["p_code"].ToString(),
                    p_cost = decimal.Parse(dr["cost"].ToString()),
                    SerialNo = dr["SerialNo"].ToString()                    
                });
            }

            string[] sns = this.txt_input.Text.Trim().Replace("\r\n", "|").Split(new char[] { '|' });
            foreach (var s in sns)
            {
                bool exist = false;
                string sn = s.Trim();
                if (s.IndexOf("A") > -1)
                    sn = s.Replace("A", "").Trim();
                if (sn.IndexOf(',') > -1)
                    sn = sn.Substring(0, sn.IndexOf(','));

                for (int i = 0; i < AllModels.Count; i++ )
                {
                    if (sn == AllModels[i].SerialNo.Trim()
                        && sn != "")
                    {
                        AllModels[i].flag = 1;  // 存在库存
                        exist = true;
                        break;
                    }
                }

                if (!exist) // 不是我们的SN 
                {
                    NoStoreSeriaNOS.Add(sn);
                }
            }
            GenerateReport();

            this.button_save_exist.Enabled = true;
            this.button_exist_no_save.Enabled = true;
        }

        void GenerateReport()
        {
           

            System.Text.StringBuilder sb_exist = new StringBuilder();
            System.Text.StringBuilder sb_no_exist = new StringBuilder();
            System.Text.StringBuilder sb_no_exist_err = new StringBuilder();
            foreach (var a in AllModels)
            {
                if (a.flag == 1)
                {
                    _existCount += 1;
                    sb_exist.Append(a.SerialNo + "\t" + a.p_code + "\t" + a.p_name + "\t" + a.p_cost.ToString() + "\r\n");
                }
                else if (a.flag == 2)
                {
                    _noExistCount += 1;
                    sb_no_exist.Append(a.SerialNo + "\t" + a.p_code + "\t" + a.p_name + "\t" + a.p_cost.ToString() + "\r\n");
                }
                else
                {
                    _no_exist_err_count += 1;
                    sb_no_exist_err.Append(a.SerialNo + "\r\n");
                }
            }
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(string.Format(" 共有{0} 个商品，{1} 个商品不存在，{2} 个条码不在仓库中 \r\n"
                , _existCount
                , _noExistCount
                , CurrentID > -1 ? _no_exist_err_count : NoStoreSeriaNOS.Count));

            if (_noExistCount > 0)
            {
                sb.Append("\r\n==============================================================\r\n");
                sb.Append(_noExistCount.ToString() + " 个商品不存在\r\n\r\n");
                sb.Append(sb_no_exist.ToString());
            }

            if (CurrentID > -1)
            {
                if (_no_exist_err_count > 0)
                {
                    sb.Append("\r\n==============================================================\r\n");
                    sb.Append(_no_exist_err_count.ToString() + " 个条码不在仓库中\r\n");
                    sb.Append(sb_no_exist_err.ToString() + "\r\n");
                }
            }
            else
            {
                if (NoStoreSeriaNOS.Count > 0)
                {
                    sb.Append("\r\n==============================================================\r\n");
                    sb.Append(NoStoreSeriaNOS.Count.ToString() + " 个条码不在仓库中\r\n");
                    foreach (var s in NoStoreSeriaNOS)
                        sb.Append(s.ToString() + "\r\n");
                }
            }
            this.richTextBox_result.Text = sb.ToString();
        }

        private void button_save_exist_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.txt_staff.Text == "")
                {
                    MessageBox.Show("请输入经手人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var csm = new db.tb_check_store();
                csm.check_regdate = DateTime.Now;
                csm.comment = this.textBox1.Text;
                csm.staff = this.txt_staff.Text;
                csm.valid_quantity = _existCount;
                csm.err_quantity = NoStoreSeriaNOS.Count;
                csm.regdate = DateTime.Now;
                context.AddTotb_check_store(csm);
                context.SaveChanges();

                foreach (var a in AllModels)
                {
                    var csdm = new db.tb_check_store_detail();
                    csdm.p_code = a.p_code;
                    csdm.p_cost = a.p_cost;
                    csdm.p_name = a.p_name;
                    csdm.ParentID = csm.id;
                    csdm.SerialNo = a.SerialNo;
                    csdm.regdate = DateTime.Now;
                    context.AddTotb_check_store_detail(csdm);
                    context.SaveChanges();
                }

                foreach (var nos in NoStoreSeriaNOS)
                {
                    var csdm = new db.tb_check_store_detail();
                    csdm.ParentID = csm.id;
                    csdm.SerialNo = nos;
                    csdm.regdate = DateTime.Now;
                    context.AddTotb_check_store_detail(csdm);
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

        private void button_exist_no_save_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("请确认不保存退出", "警告", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                this.Close();
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

        private void richTextBox_result_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
