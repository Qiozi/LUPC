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
    public partial class frmStockCopyToCompany : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        Guid _currProdGid = Guid.Empty;

        public frmStockCopyToCompany(Guid gid)
        {
            _currProdGid = gid;
            InitializeComponent();
            this.Shown += FrmStockCopyToCompany_Shown;
        }

        private void FrmStockCopyToCompany_Shown(object sender, EventArgs e)
        {

            var prod = _context
                 .tb_yun_fileinfo_stock_child
                 .Single(me => me.Gid.Equals(_currProdGid));

            this.label1.Text = "编号：" + prod.ProdCode;
            this.label2.Text = "名称：" + prod.ProdName;


        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("您确认添加？", "询问", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    using (var tran = _context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            var prod = _context
            //              .tb_yun_fileinfo_stock_child
            //              .Single(me => me.Gid.Equals(_currProdGid));

            //            var prodStockMain = _context
            //                .tb_yun_fileinfo_stock_main
            //                .Single(me => me.Gid.Equals(prod.ParentId));

            //            var newModel = new DB.tb_yun_fileinfo_company_stock_record
            //            {
            //                Cost = 0M,
            //                ProdName = prod.ProdName,
            //                Gid = Guid.NewGuid(),
            //                InOut = "IN",
            //                ProdCode = prod.ProdCode,
            //                Price = 0M,
            //                Qty = (int)this.numericUpDown1.Value,
            //                Regdate = new Util().GetCurrDateTime,
            //                Remark = "从 " + prodStockMain.FileName + "(" + Util.FormatDateTime(prodStockMain.Regdate) + ") 库存记录添加",
            //                StaffGid = BLL.Config.StaffGid,
            //                StaffName = BLL.Config.StaffName
            //            };
            //            _context.tb_yun_fileinfo_company_stock_record.Add(newModel);

            //            var existProd = _context
            //                .tb_yun_fileinfo_company_stock
            //                .SingleOrDefault(me => me.ProdCode.Equals(prod.ProdCode));
            //            if (existProd == null)
            //            {
            //                existProd = new DB.tb_yun_fileinfo_company_stock
            //                {
            //                    Gid = Guid.NewGuid(),
            //                    Cost = 0M,
            //                    Price = 0M,
            //                    ProdCode = prod.ProdCode,
            //                    ProdName = prod.ProdName,
            //                    Qty = (int)this.numericUpDown1.Value,
            //                    Regdate = new Util().GetCurrDateTime,
            //                    Remark = "从 " + prodStockMain.FileName + "(" + Util.FormatDateTime(prodStockMain.Regdate) + ") 库存记录添加",
            //                    StaffGid = BLL.Config.StaffGid,
            //                    StaffName = BLL.Config.StaffName
            //                };
            //                _context.tb_yun_fileinfo_company_stock.Add(existProd);
            //            }
            //            else
            //            {
            //                existProd.Qty += (int)this.numericUpDown1.Value;
            //            }
            //            _context.SaveChanges();
            //            tran.Commit();
            //            MessageBox.Show("库存已添加.");

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
