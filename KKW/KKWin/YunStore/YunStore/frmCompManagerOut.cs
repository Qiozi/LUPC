using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmCompManagerOut : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        Guid _currProdGid = Guid.Empty;

        public frmCompManagerOut(Guid gid)
        {
            _currProdGid = gid;
            InitializeComponent();

            this.Shown += FrmCompManagerOut_Shown;
        }

        private void FrmCompManagerOut_Shown(object sender, EventArgs e)
        {
            //var prod = _context
            //    .tb_yun_fileinfo_company_stock
            //    .Single(me => me.Gid.Equals(_currProdGid));

            //this.label1.Text = "编号：" + prod.ProdCode;
            //this.label2.Text = "名称：" + prod.ProdName;
        }

        private void buttonOut_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("您确认出库？？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    using (var tran = _context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            var existProd = _context
            //                 .tb_yun_fileinfo_company_stock
            //                 .Single(me => me.Gid.Equals(_currProdGid));


            //            if (existProd.Qty < (int)this.numericUpDown1.Value)
            //            {
            //                MessageBox.Show("库存不足。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //            var newModel = new DB.tb_yun_fileinfo_company_stock_record
            //            {
            //                Cost = existProd.Cost,
            //                ProdName = existProd.ProdName,
            //                Gid = Guid.NewGuid(),
            //                InOut = "OUT",
            //                ProdCode = existProd.ProdCode,
            //                Price = existProd.Price,
            //                Qty = (int)this.numericUpDown1.Value,
            //                Regdate = new Util().GetCurrDateTime,
            //                Remark = this.textBoxRemark.Text.Trim(),
            //                StaffGid = BLL.Config.StaffGid,
            //                StaffName = BLL.Config.StaffName,
                             
            //            };
            //            _context.tb_yun_fileinfo_company_stock_record.Add(newModel);

            //            existProd.Qty -= (int)this.numericUpDown1.Value;


            //            _context.SaveChanges();
            //            tran.Commit();
            //            MessageBox.Show("操作成功.");

            //            this.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //            tran.Rollback();
            //            MessageBox.
            //                Show(ex.Message);
            //        }
            //    }
            //}
        }
    }
}
