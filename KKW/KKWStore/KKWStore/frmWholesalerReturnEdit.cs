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
    public partial class frmWholesalerReturnEdit : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        string _returnCode = "";
        DataTable _dt = new DataTable("list");

        public frmWholesalerReturnEdit(string returnCode)
        {
            _returnCode = returnCode;

            InitializeComponent();
            this.Shown += new EventHandler(frmWholesalerReturnEdit_Shown);
        }

        void InitWin()
        {
            _dt.Columns.Add("条型码");
            _dt.Columns.Add("编号");
            _dt.Columns.Add("商品名称");
            _dt.Columns.Add("进货日期");
            _dt.Columns.Add("品牌");

            if (string.IsNullOrEmpty(_returnCode))
            {
                this.textBoxReturnCode.Text = DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            else
            {
                this.Text = "确认退货";
                this.textBoxReturnCode.Text = _returnCode;

                //ReturnWholesalerModel[] rwm = ReturnWholesalerModel.FindAllByProperty("ReturnCode", _returnCode);
                var rwm = context.tb_return_wholesaler.Where(w => w.ReturnCode.Equals(_returnCode)).ToList();
                if (rwm != null)
                {
                    textBoxNote.Text = rwm[0].Note;

                    this.buttonSure.Enabled = !rwm[0].IsSure.Value;
                }

                //ReturnWholesalerDetailModel[] rwmds = ReturnWholesalerDetailModel.FindAllByProperty("ReturnCode", _returnCode);
                var rwmds = context.tb_return_wholesaler_detail.Where(w => w.ReturnCode.Equals(_returnCode)).ToList();

                for (int i = 0; i < rwmds.Count; i++)
                {
                    DataRow dr = _dt.NewRow();
                    dr["条型码"] = rwmds[i].SN;
                    dr["编号"] = rwmds[i].p_code;
                    dr["商品名称"] = rwmds[i].p_name;
                    dr["进货日期"] = rwmds[i].InStoreDate.Value.ToString("yyyy-MM-dd");
                    dr["品牌"] = rwmds[i].Brand;
                    _dt.Rows.Add(dr);
                    BindList();
                }
            }
        }

        void BindList()
        {
            listView1.Items.Clear();
            for (int i = 0; i < _dt.Rows.Count; i++)
            {
                DataRow dr = _dt.Rows[i];
                ListViewItem li = new ListViewItem((i + 1).ToString());
                li.SubItems.Add(dr["条型码"].ToString());
                li.SubItems.Add(dr["编号"].ToString());
                li.SubItems.Add(dr["商品名称"].ToString());
                li.SubItems.Add(dr["进货日期"].ToString());
                li.SubItems.Add(dr["品牌"].ToString());
                listView1.Items.Add(li);
            }
        }

        void frmWholesalerReturnEdit_Shown(object sender, EventArgs e)
        {
            InitWin();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_returnCode))
            {
                if (MessageBox.Show("您确认保存？保存后将不能修改条型码列表", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
                {
                    string returnCode = this.textBoxReturnCode.Text;
                    string note = this.textBoxNote.Text.Trim();

                    var rwm = new db.tb_return_wholesaler();// new ReturnWholesalerModel();
                    rwm.ReturnCode = returnCode;
                    rwm.IsSure = false;
                    rwm.Qty = _dt.Rows.Count;
                    rwm.Note = this.textBoxNote.Text.Trim();
                    rwm.Regdate = DateTime.Now;
                    //rwm.Create();
                    context.tb_return_wholesaler.Add(rwm);
                    context.SaveChanges();

                    for (int i = 0; i < _dt.Rows.Count; i++)
                    {
                        DataRow dr = _dt.Rows[i];
                        var rwdm = new db.tb_return_wholesaler_detail();// new ReturnWholesalerDetailModel();
                        rwdm.ReturnCode = returnCode;
                        rwdm.SN = dr["条型码"].ToString();
                        rwdm.p_name = dr["商品名称"].ToString();
                        rwdm.p_code = dr["编号"].ToString();
                        rwdm.Brand = dr["品牌"].ToString();
                        DateTime inStoreDate;
                        DateTime.TryParse(dr["进货日期"].ToString(), out inStoreDate);
                        rwdm.InStoreDate = inStoreDate;
                        rwdm.ReturnRegdate = DateTime.Now;
                        
                        //rwdm.Create();
                        context.tb_return_wholesaler_detail.Add(rwdm);
                        context.SaveChanges();
                    }

                    this.buttonSave.Enabled = false;
                    this.buttonSure.Enabled = true;
                }
            }
            else
            {
                var rwm = context.tb_return_wholesaler.Where(p => p.ReturnCode.Equals(_returnCode)).ToList();// ReturnWholesalerModel.FindAllByProperty("ReturnCode", _returnCode);

                if (rwm.Count > 0)
                {
                    rwm[0].Note = textBoxNote.Text.Trim();
                    context.SaveChanges();
                }
            }
        }

        private void buttonSure_Click(object sender, EventArgs e)
        {
            string returncode = this.textBoxReturnCode.Text;
            if (!string.IsNullOrEmpty(returncode))
            {
                var rwm = context.tb_return_wholesaler.Where(p => p.ReturnCode.Equals(returncode)).ToList();// ReturnWholesalerModel.FindAllByProperty("ReturnCode", _returnCode);
                if (rwm.Count > 0)
                {
                    if (rwm.Count != 1)
                        return;
                    rwm[0].Note = textBoxNote.Text.Trim();
                    rwm[0].IsSure = true;
                    rwm[0].SureDate = DateTime.Now;
                    context.SaveChanges();

                    MessageBox.Show("已确认", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.buttonSure.Enabled = false;
                    List<string> snList = db.ReturnWholesalerDetailModel_p.GetReturnSN(rwm[0].ReturnCode);
                    foreach (var sn in snList)
                    {
                        db.SerialNoAndPCodeModel_p.ProductReturnWholesaler(long.Parse(sn));
                    }
                }
            }
        }

        private void textBox_Input_KeyUp(object sender, KeyEventArgs e)
        {
            string sn = this.textBox_Input.Text.Trim();

            if (e.KeyCode == Keys.Enter)
            {

                foreach (DataRow m in _dt.Rows)
                {
                    if (m["条型码"].ToString() == sn)
                    {
                        MessageBox.Show("条码已列表", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                bool invalid = false;
                var pm = db.ProductModel_p.GetModelBySN(context, sn, ref invalid);
                var serial = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                DataRow dr = _dt.NewRow();
                dr["条型码"] = sn;
                dr["编号"] = pm.p_code;
                dr["商品名称"] = pm.p_name;
                dr["进货日期"] = serial.in_regdate.Value.ToString("yyyy-MM-dd");
                dr["品牌"] = pm.brand;
                _dt.Rows.Add(dr);
                BindList();
                this.textBox_Input.Text = "";
            }
        }
    }
}
