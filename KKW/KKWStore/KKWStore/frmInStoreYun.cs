using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmInStoreYun : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        enums.WarehouseType _currWarehouse = enums.WarehouseType.云仓;
        int _currProdId = 0;
        string _currProdCode = "";

        public frmInStoreYun()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmInStoreSN_Shown);
        }

        void frmInStoreSN_Shown(object sender, EventArgs e)
        {
            labelWarehouse.Text = "入" + _currWarehouse.ToString();
            BindProdList("");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyword"></param>
        void BindProdList(string keyword)
        {
            DataTable _searchDT = db.ProductModel_p.GetModelsTransferSNByKeyword(keyword);
            listViewPList.Items.Clear();
            listViewSN.Items.Clear();
            _currProdCode = "";
            _currProdId = 0;
            labelCurrProductName.Text = "";

            foreach (DataRow dr in _searchDT.Rows)
            {
                decimal total;
                decimal.TryParse(dr["total"].ToString(), out total);

                decimal totalQty;
                decimal.TryParse(dr["c"].ToString(), out totalQty);

                decimal agePrice = totalQty == 0M ? 0M : total / totalQty;

                //DataTable singelDT = db.SerialNoAndPCodeModel_p.GetValidListByProdCodeNoSN(int.Parse(dr["id"].ToString()));
                //foreach (DataRow sdr in singelDT.Rows)
                //{
                //    decimal _t;
                //    decimal.TryParse(sdr["cost"].ToString(), out _t);
                //    total += _t;
                //}
                //totalQty = singelDT.Rows.Count;
                //agePrice = totalQty == 0M ? 0M : total / totalQty;

                ListViewItem li = new ListViewItem(dr["p_code"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["p_name"].ToString());
                li.SubItems.Add(total.ToString("#0.00"));
                li.SubItems.Add(totalQty.ToString());
                li.SubItems.Add(agePrice.ToString("#0.00"));

                listViewPList.Items.Add(li);
            }

            if (_searchDT.Rows.Count == 0)
            {
                ListViewItem li = new ListViewItem("查无数据");
                li.Tag = -1;
                li.SubItems.Add("");
                listViewPList.Items.Add(li);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox TB = (TextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
            this.listViewPList.Items.Clear();
            this.listViewSN.Items.Clear();

            if (TB.Text.Length > 1)
            {
                BindProdList(TB.Text.Trim());
            }
        }

        private void listViewPList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lv = sender as ListView;

            if (lv.SelectedItems == null)
            {
                _currProdId = 0;
                _currProdCode = "";
                labelCurrProductName.Text = "";
                listViewSN.Items.Clear();
            }
            else if (lv.SelectedItems.Count == 1)
            {
                labelCurrProductName.Text = lv.SelectedItems[0].SubItems[1].Text;
                _currProdCode = lv.SelectedItems[0].SubItems[0].Text;
                int id;
                int.TryParse(lv.SelectedItems[0].Tag.ToString(), out id);
                _currProdId = id;
                BindSN(id);
            }
        }

        void BindSN(int pid)
        {
            DataTable singelDT = db.SerialNoAndPCodeModel_p.GetValidListByProdCodeNoSN(pid, enums.WarehouseType.中转_未分配);
            listViewSN.Items.Clear();
            for (int i = 0; i < singelDT.Rows.Count; i++)
            {
                DataRow dr = singelDT.Rows[i];
                ListViewItem li = new ListViewItem(dr["SerialNO"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["cost"].ToString());
                li.SubItems.Add("");
                li.SubItems.Add("");
                listViewSN.Items.Add(li);
            }
        }

        private void textBoxSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {

            }
        }

        private void buttonSubmitQty_Click(object sender, EventArgs e)
        {
            if (listViewSN.Items.Count < (int)numericUpDown1.Value)
            {
                MessageBox.Show("输入的数量比当前库存更多？？");
                return;
            }
            bool match = false;
            var qty = (int)numericUpDown1.Value;
            for (int i = 0; i < qty; i++)
            {
                if (listViewSN.Items[i].SubItems[0].Text.Trim() == "1000000000")
                {
                    listViewSN.Items[i].SubItems[2].Text = "OK";
                    listViewSN.Items[i].SubItems[3].Text = db.SerialNoModel_p.GetSerialNo(context);
                }
                else
                {
                    listViewSN.Items[i].SubItems[2].Text = "OK";
                    listViewSN.Items[i].SubItems[3].Text = listViewSN.Items[i].SubItems[0].Text.Trim();
                }
                match = true;
            }
            if (!match)
            {
                MessageBox.Show("没有相应的产品可匹配，请检查 中转 库存");
                return;
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (_currProdId < 1)
            {
                MessageBox.Show("无产品数据", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            var removeQty = (int)numericUpDownRemoveQty.Value;
            var currRemoveQty = 0;
            for (int i = 0; i < listViewSN.Items.Count; i++)
            {
                string code = listViewSN.Items[i].SubItems[3].Text;
                int id;
                int.TryParse(listViewSN.Items[i].Tag.ToString(), out id);

                if (listViewSN.Items[i].SubItems[2].Text != "OK")
                    continue;

                if (code.Length == 10 && code != Helper.Config.TmpSNCode)
                {
                    db.SerialNoAndPCodeModel_p.ChangeSN(id, code
                        , (int)_currWarehouse
                        , _currProdId
                        , _currProdCode
                        , (int)enums.WarehouseType.中转_未分配);
                    if (currRemoveQty < removeQty)
                    {
                        currRemoveQty++;
                        // 手动出库
                        db.SerialNoAndPCodeModel_p.AutoOut2(context, code, "盘点手工出库， 吴（补）");
                    }
                }
            }
            numericUpDownRemoveQty.Value = 0M;
            MessageBox.Show("操作完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindProdList(textBox1.Text.Trim());
        }
    }
}
