using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmUpSaleInfo : Form
    {
        DB.kkwEntities _context = new DB.kkwEntities();
        DB.tb_yun_fileinfo_sale_main _dbMain = new DB.tb_yun_fileinfo_sale_main();
        List<DB.tb_yun_fileinfo_sale_child> _dbList = new List<DB.tb_yun_fileinfo_sale_child>();

        public frmUpSaleInfo()
        {
            InitializeComponent();
            this.Shown += FrmUpSaleInfo_Shown;
        }

        private void FrmUpSaleInfo_Shown(object sender, EventArgs e)
        {
            this.label1.Text = "";
        }

        private void buttonSelectFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                this.textBox1.Text = openFileDialog1.FileName;

                var filename = this.textBox1.Text;
                if (!string.IsNullOrEmpty(filename))
                {
                    var dt = ExcelHelper.ExcelToDataSet(filename);

                    try
                    {
                        var table = dt.Tables[0];

                        if (table.Rows.Count > 0)
                        {
                            _dbMain = new DB.tb_yun_fileinfo_sale_main
                            {
                                FileMD5 = Md5File(),
                                FileName = Path.GetFileName(this.textBox1.Text),
                                Gid = Guid.NewGuid().ToString(),
                                Regdate = new Util().GetCurrDateTime,
                                StaffId = BLL.Config.StaffGid ?? "",
                                StaffName = BLL.Config.StaffName ?? "",
                                AllProdQty = 0,
                                AllProdSaleCost = 0M,
                                AllProdSaleQty = 0
                            };
                        }

                        foreach (DataRow dr in table.Rows)
                        {
                            var prodCode = (dr["商品编码"] ?? "").ToString();
                            var prodShortName = (dr["商品简称"] ?? "").ToString();
                            var prodName = (dr["商品名称"] ?? "").ToString();
                            var prodSpec = (dr["商品规格"] ?? "").ToString();
                            var price = (dr["商品单价"] ?? "").ToString();
                            var cost = (dr["商品成本价"] ?? "").ToString();
                            var warehouseName = (dr["仓库名称"] ?? "").ToString();
                            var storeName = (dr["店铺"] ?? "").ToString();
                            var qty = (dr["数量"] ?? "").ToString();


                            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff");
                            var newModel = new DB.tb_yun_fileinfo_sale_child
                            {
                                Gid = Guid.NewGuid().ToString(),
                                ParentId = _dbMain.Gid,
                                ProdCode = prodCode,
                                ProdName = prodName,
                                ProdSpec = prodSpec,
                                Regdate = datetime,
                                Cost = ConvertHelper.CustomConvert<decimal>(cost),
                                Price = ConvertHelper.CustomConvert<decimal>(price),
                                ProdShortName = prodShortName,
                                Qty = ConvertHelper.CustomConvert<int>(qty),
                                StoreName = storeName,
                                WarehouseName = warehouseName
                            };
                            _dbList.Add(newModel);

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("错误信息：" + ex.Message);
                    }

                    this.listView1.Items.Clear();
                    foreach (var item in _dbList)
                    {
                        var li = new ListViewItem(item.ProdCode);
                        li.SubItems.Add(item.ProdName);
                        li.SubItems.Add(item.ProdShortName);
                        li.SubItems.Add(item.ProdSpec);
                        li.SubItems.Add(item.Price.ToString());
                        li.SubItems.Add(item.Cost.ToString());
                        li.SubItems.Add(item.WarehouseName.ToString());
                        li.SubItems.Add(item.StoreName.ToString());
                        li.SubItems.Add(item.Qty.ToString());

                        this.listView1.Items.Add(li);
                    }

                    _dbMain.AllProdSaleCost = _dbList.Sum(me => (decimal?)me.Cost).GetValueOrDefault();
                    _dbMain.AllProdSaleQty = _dbList.Sum(me => (int?)me.Qty).GetValueOrDefault();
                    _dbMain.AllProdQty = _dbList.Count;

                    this.label1.Text = string.Format(@"商品数量：{0}   出库总数量：{1}   出库总成本：{2}",
                        _dbMain.AllProdQty,
                        _dbMain.AllProdSaleQty,
                        _dbMain.AllProdSaleCost);
                }
                this.Cursor = Cursors.Default;
            }
        }

        public string Md5File()
        {
            return string.Empty;
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            DB.kkwEntities _context = new DB.kkwEntities();
            var count = _context.tb_yun_fileinfo_sale_main.Count(me => me.FileName.Equals(_dbMain.FileName));
            if (string.IsNullOrEmpty(_dbMain.FileName) || count > 0)
            {
                MessageBox.Show("此文件已导入过。");
                return;
            }


            this.Cursor = Cursors.WaitCursor;
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.tb_yun_fileinfo_sale_main.Add(_dbMain);
                    _context.tb_yun_fileinfo_sale_child.AddRange(_dbList);
                    _context.SaveChanges();
                    tran.Commit();
                    _dbMain = new DB.tb_yun_fileinfo_sale_main();
                    _dbList = new List<DB.tb_yun_fileinfo_sale_child>();
                    this.textBox1.Text = "";
                    this.listView1.Items.Clear();
                    this.label1.Text = "";
                    MessageBox.Show("导入成功。");
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    MessageBox.Show(ex.Message);
                }

            }
            this.Cursor = Cursors.Default;
        }
    }
}
