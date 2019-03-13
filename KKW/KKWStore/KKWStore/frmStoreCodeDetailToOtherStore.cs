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
    public partial class frmStoreCodeDetailToOtherStore : Form
    {
        int SourceWarehouseId = 0;
        int TargetWarehouseId = 0;
        int Qty = 0;
        int ProductId = 0;

        public frmStoreCodeDetailToOtherStore(int sourceWarehouseId, int targetWarehouseId,  int productId)
        {
            InitializeComponent();
            SourceWarehouseId = sourceWarehouseId;
            this.TargetWarehouseId = targetWarehouseId;
       
            this.ProductId = productId;
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            var context = new db.qstoreEntities();
            Qty = (int)this.numericUpDown1.Value;
            if (SourceWarehouseId > 0 && this.TargetWarehouseId > 0 && this.Qty > 0)
            {
                var prod = context.tb_product.Single(me => me.id.Equals(ProductId));

                var snDT = db.SerialNoAndPCodeModel_p.GetAllInStoreSNByWare(prod.p_code, SourceWarehouseId);
                if (snDT.Rows.Count < this.Qty)
                {
                    MessageBox.Show("库存不足，请确认输入的转库数量");
                    this.DialogResult = DialogResult.No;
                    this.Close();
                }
                else
                {
                    for (var i = 0; i < this.Qty; i++)
                    {
                        db.SerialNoAndPCodeModel_p.ChangeSNOnWarehouse(int.Parse(snDT.Rows[i]["id"].ToString()),
                            prod.id,
                            prod.p_code,
                            SourceWarehouseId,
                            snDT.Rows[i]["SerialNo"].ToString(),
                            TargetWarehouseId);
                    }
                    MessageBox.Show("转库成功。");
                    this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("参数错误 .");
                this.DialogResult = DialogResult.No;
                this.Close();
            }
        }
    }
}
