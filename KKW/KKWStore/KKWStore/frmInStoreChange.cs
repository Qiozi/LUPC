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
    public partial class frmInStoreChange : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmInStoreChange()
        {
            
            InitializeComponent();
            this.Shown += new EventHandler(frmInStoreChange_Shown);
        }

        void frmInStoreChange_Shown(object sender, EventArgs e)
        {
            //var list = WarehouseModel.FindAll();
            var list = context.tb_warehouse.ToList();
            comboBoxWarehouse.DataSource = list;
            comboBoxWarehouse.DisplayMember = "WarehouseName";
            comboBoxWarehouse.ValueMember = "ID";


        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认转" + txtQty.Text + "个产品到 【" + ((db.tb_warehouse) comboBoxWarehouse.SelectedItem).WarehouseName + "】", "询问确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < listViewPList.Items.Count; i++)
                {
                    string sn = listViewPList.Items[i].SubItems[0].Text;
                    if (!string.IsNullOrEmpty(sn))
                    {
                        int id;
                        int.TryParse(listViewPList.Items[i].Tag.ToString(), out id);

                        //SerialNoAndPCodeModel snModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(sn);
                        var snModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                        if (snModel != null)
                        {
                            enums.WarehouseType oldWT = (enums.WarehouseType)Enum.Parse(typeof(enums.WarehouseType), snModel.curr_warehouse_id.ToString());
                            enums.WarehouseType newWT = (enums.WarehouseType)Enum.Parse(typeof(enums.WarehouseType), ((db.tb_warehouse)comboBoxWarehouse.SelectedItem).ID.ToString());
                            db.SerialNoAndPCodeModel_p.SaveChangeSNRecord(id
                                , snModel.p_id.Value
                                , snModel.p_code
                                , (int)oldWT
                                , sn
                                , sn
                                , (int)newWT);

                            snModel.curr_warehouse_id = (int)comboBoxWarehouse.SelectedValue;
                            snModel.curr_warehouse_date = DateTime.Now;
                            context.SaveChanges();
                        }
                    }
                }
                MessageBox.Show("提交完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listViewPList.Items.Clear();
            }
        }

        void BindTotal()
        {
            decimal total = 0M;
            int count = 0;
            for (int i = 0; i < listViewPList.Items.Count; i++)
            {
                if (!string.IsNullOrEmpty(listViewPList.Items[i].SubItems[0].Text))
                {
                    decimal _t;
                    decimal.TryParse(listViewPList.Items[i].SubItems[4].Text, out _t);
                    total += _t;
                    count++;
                }
            }
            txtTotal.Text = "￥"+ total.ToString("0.00");
            txtQty.Text = count.ToString();
        }

        private void textBoxSN_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;
            TextBox tb = sender as TextBox;
            if (!string.IsNullOrEmpty(tb.Text) && tb.Text.Trim().Length == 10)
            {
                string sn = tb.Text.Trim();
                var snModel = db.SerialNoAndPCodeModel_p.GetModelBySerialNO(context, sn);
                if (snModel != null)
                {
                    if (snModel.curr_warehouse_id == ((db.tb_warehouse)comboBoxWarehouse.SelectedItem).ID)
                    {
                        MessageBox.Show(sn + " 此条码所在仓库与当前仓库一样", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }


                    for (int i = 0; i < listViewPList.Items.Count; i++)
                    {
                        if (sn == listViewPList.Items[i].SubItems[0].Text)
                        {
                            MessageBox.Show(sn + " 此条码已扫描过", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    if (snModel.out_regdate.Value.Year > 2000)
                    {
                        MessageBox.Show(sn + " 此条码已出库，无效！！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var pm = db.ProductModel_p.GetModelByCode(context, snModel.p_code);
                    ListViewItem li = new ListViewItem(sn);
                    li.Tag = snModel.id;
                    li.SubItems.Add(Enum.Parse(typeof(enums.WarehouseType), snModel.curr_warehouse_id.ToString()).ToString());
                    li.SubItems.Add(snModel.p_code);
                    li.SubItems.Add(pm != null ? pm.p_name : "");
                    li.SubItems.Add(snModel.in_cost.Value.ToString("##0.00"));
                    li.SubItems.Add(snModel.in_regdate.Value.ToString("yyyy-MM-dd"));
                    listViewPList.Items.Add(li);
                    BindTotal();
                    tb.Text = "";
                }
                else
                {
                    MessageBox.Show(sn + " 无效的条码，找不到匹配的数据", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        private void 移除选中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewPList.SelectedItems != null)
            {
                if (listViewPList.SelectedItems.Count == 1)
                {
                    listViewPList.SelectedItems[0].Remove();
                }
            }
        }
    }
}
