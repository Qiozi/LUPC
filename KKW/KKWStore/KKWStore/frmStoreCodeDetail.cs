using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmStoreCodeDetail : Form
    {
        int _P_id = 0;

        public frmStoreCodeDetail(int p_id)
        {
            _P_id = p_id;
            InitializeComponent();
            this.Shown += new EventHandler(frmStoreCodeDetail_Shown);
        }

        void frmStoreCodeDetail_Shown(object sender, EventArgs e)
        {
            db.qstoreEntities context = new db.qstoreEntities();
           
            List<int> statWare = new List<int>() { 1, 2, 4 };// 公司，显神，已打包
            var cates = context.tb_warehouse.Where(me => me.showit.Equals(true)).ToList();
            var prod = context.tb_product.Single(p => p.id.Equals(_P_id));
            this.Text += ": " + prod.p_name;
            int storeQty = 0;
            int split = 180;
            for (var i = 0; i < cates.Count; i++)
            {
                Label l = new Label();
                l.Location = new Point(12 + (i * split), 36);

                l.Width = split;
                this.Controls.Add(l);

                ListBox lb = new ListBox();
                lb.Location = new Point(12 + (i * split), 66);
                lb.Height = 400;
                lb.Width = split - 10;
                if(cates[i].ID == Helper.Config.CompanyWarehouseId)
                {
                    lb.ContextMenuStrip = this.contextMenuStrip1;
                }
                if(cates[i].ID == Helper.Config.BadGoodsWarehouseId)
                {
                    lb.ContextMenuStrip = this.contextMenuStrip2;
                }
                if(cates[i].ID == Helper.Config.YunWarehouseId)
                {
                    lb.ContextMenuStrip = this.contextMenuStrip3;
                }
                this.Controls.Add(lb);

                var list = db.SerialNoAndPCodeModel_p.GetAllInStoreSNByWare(prod.p_code, cates[i].ID);
                lb.DataSource = list;
                lb.DisplayMember = "code_and_in_date";
                lb.ValueMember = "id";

                l.Text = cates[i].WarehouseName + "【" + list.Rows.Count.ToString() + "】";

                if (statWare.Contains(cates[i].ID))
                {
                    l.ForeColor = Color.Red;
                }
                storeQty += list.Rows.Count;
            }
            label1.Text = "所有有效库存: " + "【" + storeQty.ToString() + "】";
            label1.ForeColor = Color.Red;
        }

        private void FlushControls(Control con)
        {
            for(var i = 0; i<this.Controls.Count; i++)
            {
                Control c = this.Controls[i];
                String type = c.GetType().ToString();
                switch (type)
                {
                    case "System.Windows.Forms.ListBox":
                        this.Controls.Remove(c);
                        i--;
                        break;
                    case "System.Windows.Forms.Label":
                        if(c.Name != "label1")
                        {
                            this.Controls.Remove(c);
                            i--;
                        }
                        break;
                }
            }
        }

        private void 公司转云仓仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.CompanyWarehouseId;
            var targetWarehouseId = Helper.Config.YunWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
            }
        }

        private void 公司转瑕疵仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.CompanyWarehouseId;
            var targetWarehouseId = Helper.Config.BadGoodsWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
            }
        }

        private void 瑕疵转公司仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.BadGoodsWarehouseId;
            var targetWarehouseId = Helper.Config.CompanyWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
            }
        }

        private void 瑕疵转云仓仓库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.BadGoodsWarehouseId;
            var targetWarehouseId = Helper.Config.YunWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
            }
        }

        private void 云仓转公司仓库ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.YunWarehouseId;
            var targetWarehouseId = Helper.Config.CompanyWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
             
            }
        }

        private void 云仓转瑕疵仓库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var sourceWarehouseId = Helper.Config.YunWarehouseId;
            var targetWarehouseId = Helper.Config.BadGoodsWarehouseId;

            frmStoreCodeDetailToOtherStore frm = new frmStoreCodeDetailToOtherStore(sourceWarehouseId, targetWarehouseId, _P_id);
            frm.StartPosition = FormStartPosition.CenterParent;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                FlushControls(this);
                frmStoreCodeDetail_Shown(null, null);
            }
        }
    }
}
