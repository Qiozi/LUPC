using KKWStore.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmChangeWholesale : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        bool _textChanging = false;

        public frmChangeWholesale()
        {
            InitializeComponent();

            this.Shown += FrmChangeWholesale_Shown;
        }

        private void FrmChangeWholesale_Shown(object sender, EventArgs e)
        {
            BindStoreList();
        }

        void BindStoreList()
        {
            var warehouseList = context.tb_warehouse.Where(me => me.showit.Equals(true)).ToList();// WarehouseModel.FindAll();
            tb_warehouse[] warehouseList2 = new tb_warehouse[warehouseList.Count];
            warehouseList.CopyTo(warehouseList2);


            this.comboBoxSource.DataSource = warehouseList;
            comboBoxSource.DisplayMember = "WarehouseName";
            comboBoxSource.ValueMember = "ID";

            this.comboBoxTarget.DataSource = warehouseList2;
            comboBoxTarget.DisplayMember = "WarehouseName";
            comboBoxTarget.ValueMember = "ID";
        }

        void Clear()
        {
            this.textBoxCode.Text = "";
            this.textBoxProdName.Text = "";
            this.textBoxSourceQty.Text = "0";
            this.textBoxTargetQty.Text = "0";
            this.textBoxYunCode.Text = "";
            this.numericUpDown1.Value = 0M;

        }

        void BindChange()
        {
            var code = this.textBoxCode.Text.Trim();
            if (string.IsNullOrEmpty(code))
                return;

            var p = db.ProductModel_p.GetModelByCode(context, code);
            if (p == null)
            {
                this.textBoxProdName.Text = "";
                this.textBoxYunCode.Text = "";
                this.textBoxSourceQty.Text = "0";
                this.textBoxTargetQty.Text = "0";
                MessageBox.Show("没有找到此商品：" + textBoxCode.Text);
            }
            else
            {
                this.textBoxYunCode.Text = p.yun_code;
                this.textBoxProdName.Text = p.p_name;

                this.textBoxSourceQty.Text = db.SerialNoAndPCodeModel_p.GetAllInStoreSNByWare(code, ((tb_warehouse)comboBoxSource.SelectedItem).ID).Rows.Count.ToString();
                this.textBoxTargetQty.Text = db.SerialNoAndPCodeModel_p.GetAllInStoreSNByWare(code, ((tb_warehouse)comboBoxSource.SelectedItem).ID).Rows.Count.ToString();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int sourceQty = 0;
            int.TryParse(this.textBoxSourceQty.Text, out sourceQty);
            if (numericUpDown1.Value > sourceQty)
            {
                MessageBox.Show("库存不足");
                return;
            }
            var code = this.textBoxCode.Text.Trim();
            var p = db.ProductModel_p.GetModelByCode(context, code);
            var sourceStoreId = ((tb_warehouse)comboBoxSource.SelectedItem).ID;
            var targetStoreId = ((tb_warehouse)comboBoxTarget.SelectedItem).ID;
            var sourceDB = db.SerialNoAndPCodeModel_p.GetAllInStoreSNByWare(code, sourceStoreId);

            for (var i = 0; i < (int)numericUpDown1.Value; i++)
            {
                DataRow dr = sourceDB.Rows[i];
                db.SerialNoAndPCodeModel_p.ChangeSNOnWarehouse(int.Parse(dr["Id"].ToString()), p.id, p.p_code, sourceStoreId, dr["SerialNo"].ToString(), targetStoreId);
            }
            MessageBox.Show("转库完成");
            Clear();
        }

        private void comboBoxSource_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void comboBoxTarget_MouseUp(object sender, MouseEventArgs e)
        {
            BindChange();

        }

        private void comboBoxSource_MouseUp(object sender, MouseEventArgs e)
        {
            BindChange();
        }

        private void textBoxCode_TextChanged(object sender, EventArgs e)
        {
            BindChange();
        }

        private void textBoxYunCode_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
