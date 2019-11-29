using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmStockList : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        DB.tb_yun_fileinfo_stock_main _dbMain = new DB.tb_yun_fileinfo_stock_main();
        List<DB.tb_yun_fileinfo_stock_child> _dbList = new List<DB.tb_yun_fileinfo_stock_child>();

        public frmStockList()
        {
            InitializeComponent();
            this.Shown += FrmStockList_Shown;
        }

        private void FrmStockList_Shown(object sender, EventArgs e)
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
                            _dbMain = new DB.tb_yun_fileinfo_stock_main
                            {
                                FileMD5 = Md5File(),
                                FileName = Path.GetFileName(this.textBox1.Text),
                                Gid = Guid.NewGuid(),
                                Regdate = new Util().GetCurrDateTime,
                                StaffId = BLL.Config.StaffGid ,
                                StaffName = BLL.Config.StaffName ?? "",
                                AllProdQty = 0,
                                AllProdStock = 0,
                                AllProdTotal = 0M
                            };
                        }

                        foreach (DataRow dr in table.Rows)
                        {
                            var prodCode = (dr["商品编码"] ?? "").ToString();
                            var specCode = (dr["规格编码"] ?? "").ToString();
                            var prodName = (dr["商品名称"] ?? "").ToString();
                            var prodSpec = (dr["商品规格"] ?? "").ToString();
                            var prodSN = (dr["商品条码"] ?? "").ToString();
                            var qtyAll = (dr["实际"] ?? "").ToString();
                            var qtyUse = (dr["可用"] ?? "").ToString();
                            var qtyOn = (dr["在途"] ?? "").ToString();
                            var qtyUsed = (dr["占用"] ?? "").ToString();
                            var total = (dr["总货值"] ?? "").ToString();
                            var qty30day = (dr["30天销量"] ?? "").ToString();
                            var qtyWarn = (dr["警戒量"] ?? "").ToString();

                            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff");
                            var newModel = new DB.tb_yun_fileinfo_stock_child
                            {
                                Gid = Guid.NewGuid(),
                                ParentId = _dbMain.Gid,
                                ProdCode = prodCode,
                                ProdName = prodName,
                                ProdSN = prodSN,
                                ProdSpec = prodSpec,
                                Qty30DaySale = ConvertHelper.CustomConvert<int>(qty30day),
                                QtyAll = ConvertHelper.CustomConvert<int>(qtyAll),
                                QtyOn = ConvertHelper.CustomConvert<int>(qtyOn),
                                QtyStock = ConvertHelper.CustomConvert<int>(qtyUse),
                                QtyUsed = ConvertHelper.CustomConvert<int>(qtyUsed),
                                QtyWarn = ConvertHelper.CustomConvert<int>(qtyWarn),
                                SpecCode = specCode,
                                Regdate = datetime,
                                Total = ConvertHelper.CustomConvert<decimal>(total)
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
                        li.SubItems.Add(item.SpecCode);
                        li.SubItems.Add(item.ProdName);
                        li.SubItems.Add(item.ProdSpec);
                        li.SubItems.Add(item.ProdSN);
                        li.SubItems.Add(item.QtyAll.ToString());
                        li.SubItems.Add(item.QtyStock.ToString());
                        li.SubItems.Add(item.QtyOn.ToString());
                        li.SubItems.Add(item.QtyUsed.ToString());
                        li.SubItems.Add(item.Total.ToString());
                        li.SubItems.Add(item.Qty30DaySale.ToString());
                        li.SubItems.Add(item.QtyWarn.ToString());

                        if (item.QtyStock <= 0)
                        {
                            li.BackColor = Color.LightPink;
                        }
                        this.listView1.Items.Add(li);
                    }

                    _dbMain.AllProdTotal = _dbList.Sum(me => (decimal?)me.Total).GetValueOrDefault();
                    _dbMain.AllProdStock = _dbList.Sum(me => (int?)me.QtyStock).GetValueOrDefault();
                    _dbMain.AllProdQty = _dbList.Count;

                    this.label1.Text = string.Format(@"商品数量：{0}   总库存数量：{1}   总库存货值：{2}",
                        _dbMain.AllProdQty,
                        _dbMain.AllProdStock,
                        _dbMain.AllProdTotal);
                }
                this.Cursor = Cursors.Default;
            }
        }

        public string Md5File(string filename = "")
        {
            if (string.IsNullOrEmpty(filename))
                return string.Empty;

            return MD5Checker.GetMD5ByMD5CryptoService(filename);
        }

        private void buttonImport_Click(object sender, EventArgs e)
        {
            DB.qstoreEntities _context = new DB.qstoreEntities();
            var count = _context.tb_yun_fileinfo_stock_main.Count(me => me.FileName.Equals(_dbMain.FileName));
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
                    if (File.Exists(this.textBox1.Text))
                    {
                        string path = Path.GetDirectoryName(BLL.Config.DBFullname);
                        string newFilename = Path.Combine(path, DateTime.Now.ToString("yyyyMMdd_") + _dbMain.Gid + ".xlsbak");
                        File.Copy(this.textBox1.Text, newFilename);
                        _dbMain.FileMD5 = Md5File(newFilename);
                    }
                    else
                    {
                        throw new Exception("文件不存在。");
                    }
                    //throw new Exception("dd");
                    _context.tb_yun_fileinfo_stock_main.Add(_dbMain);
                    _context.tb_yun_fileinfo_stock_child.AddRange(_dbList);
                    _context.SaveChanges();
                    tran.Commit();
                    _dbMain = new DB.tb_yun_fileinfo_stock_main();
                    _dbList = new List<DB.tb_yun_fileinfo_stock_child>();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
