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
    public partial class frmUpCompInfo : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        DB.tb_yun_fileinfo_company_stock_main _dbMain = new DB.tb_yun_fileinfo_company_stock_main();
        List<DB.tb_yun_fileinfo_company_stock_child> _dbList = new List<DB.tb_yun_fileinfo_company_stock_child>();
        List<string> _monthList = new List<string>();
        Guid _preParentGid = Guid.Empty;

        public frmUpCompInfo()
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
            this.Cursor = Cursors.WaitCursor;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = openFileDialog1.FileName;
                var filename = this.textBox1.Text;


                if (!string.IsNullOrEmpty(filename))
                {
                    DB.qstoreEntities _context = new DB.qstoreEntities();
                    var onlyFilename = Path.GetFileName(this.textBox1.Text);
                    var count = _context.tb_yun_fileinfo_company_stock_main.Count(me => me.FileName.Equals(onlyFilename));
                    if (count > 0)
                    {
                        MessageBox.Show("此文件已导入过。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    // 
                    var dt = ExcelHelper.ExcelToDataSet(filename);

                    try
                    {
                        var table = dt.Tables[0];

                        if (table.Rows.Count > 0)
                        {
                            _dbMain = new DB.tb_yun_fileinfo_company_stock_main
                            {
                                FileMD5 = Md5File(),
                                FileName = Path.GetFileName(this.textBox1.Text),
                                Gid = Guid.NewGuid(),
                                Regdate = new Util().GetCurrDateTime,
                                StaffId = BLL.Config.StaffGid,
                                StaffName = BLL.Config.StaffName ?? "",
                                AllProdQty = 0,
                                AllProdSaleCost = 0M,
                                AllProdSaleQty = 0
                            };
                        }

                        foreach (DataRow dr in table.Rows)
                        {
                            var prodCode = (dr["商品编码"] ?? "").ToString();
                            var prodName = (dr["商品名称"] ?? "").ToString();
                            var remark = (dr["备注"] ?? "").ToString();
                            var price = (dr["单价"] ?? "").ToString();
                            var cost = (dr["成本价"] ?? "").ToString();
                            var qty = (dr["数量"] ?? "").ToString();

                            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff");
                            var newModel = new DB.tb_yun_fileinfo_company_stock_child
                            {
                                Gid = Guid.NewGuid(),
                                ParentId = _dbMain.Gid,
                                ProdCode = prodCode,
                                ProdName = prodName,
                                Regdate = datetime,
                                Cost = ConvertHelper.CustomConvert<decimal>(cost),
                                Price = ConvertHelper.CustomConvert<decimal>(price),
                                Qty = ConvertHelper.CustomConvert<int>(qty),
                                Remark = remark,
                                StaffGid = BLL.Config.StaffGid,
                                StaffName = BLL.Config.StaffName,
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
                        li.SubItems.Add(item.Price.ToString());
                        li.SubItems.Add(item.Cost.ToString());
                        li.SubItems.Add(item.Qty.ToString());
                        li.SubItems.Add(item.Remark.ToString());

                        if (item.Cost <= 0M)
                        {
                            li.BackColor = Color.LightPink;
                        }
                        this.listView1.Items.Add(li);
                    }

                    _dbMain.AllProdSaleCost = _dbList.Sum(me => (decimal?)me.Cost * me.Qty).GetValueOrDefault();
                    _dbMain.AllProdSaleQty = _dbList.Sum(me => (int?)me.Qty).GetValueOrDefault();
                    _dbMain.AllProdQty = _dbList.Count;

                    this.label1.Text = string.Format(@"商品数量：{0}   总数量：{1}   总成本：{2}； 警告0成本商品数量：{3}",
                        _dbMain.AllProdQty,
                        _dbMain.AllProdSaleQty,
                        Util.FormatPrice(_dbMain.AllProdSaleCost),
                        _dbList.Count(me => me.Cost <= 0.1M).ToString());
                }

            }
            this.Cursor = Cursors.Default;
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
            var count = _context.tb_yun_fileinfo_company_stock_main.Count(me => me.FileName.Equals(_dbMain.FileName));
            if (string.IsNullOrEmpty(_dbMain.FileName) || count > 0)
            {
                MessageBox.Show("此文件已导入过。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            using (var tran = _context.Database.BeginTransaction())
            {
                try
                {
                    if (File.Exists(this.textBox1.Text))
                    {
                        string newFilename = Path.Combine(Path.GetDirectoryName(BLL.Config.DBFullname), _dbMain.Gid + ".xlsbak");
                        File.Copy(this.textBox1.Text, newFilename, true);
                        _dbMain.FileMD5 = Md5File(newFilename);
                    }
                    else
                    {
                        throw new Exception("文件不存在。");
                    }

                    var preMain = _context.tb_yun_fileinfo_company_stock_main
                        .OrderByDescending(me => me.Regdate)
                        .FirstOrDefault();

                    _context.tb_yun_fileinfo_company_stock_main.Add(_dbMain);
                    _context.tb_yun_fileinfo_company_stock_child.AddRange(_dbList);

                    foreach (var item in _dbList)
                    {
                        Guid gid1 = Guid.Empty;
                        Guid gid2 = item.Gid;
                        bool isIn = false;
                        int qty = 0;
                        if (preMain == null)
                        {
                            isIn = true;
                            qty = item.Qty;
                        }
                        else
                        {
                            // 有旧数据
                            var oldStock = _context
                                .tb_yun_fileinfo_company_stock_child
                                .FirstOrDefault(me => me.ParentId.Equals(preMain.Gid) &&
                                    me.ProdCode.Equals(item.ProdCode));
                            if (oldStock != null)
                            {
                                gid1 = oldStock.Gid;
                                qty = item.Qty - oldStock.Qty;

                                if (qty > 0)
                                {
                                    isIn = true;
                                }
                                else
                                {
                                    isIn = false;
                                    qty = 0 - qty;
                                }
                            }
                            else
                            {
                                // 新产品；
                                isIn = true;
                                qty = item.Qty;
                            }
                        }



                        if (isIn && qty != 0)
                        {
                            var newModel = new DB.tb_yun_fileinfo_company_stock_record
                            {
                                Cost = item.Cost,
                                ProdName = item.ProdName,
                                Gid = Guid.NewGuid(),
                                InOut = "IN",
                                ProdCode = item.ProdCode,
                                Price = item.Price,
                                Qty = qty,
                                Regdate = new Util().GetCurrDateTime,
                                Remark = string.Format("旧{0}与新{1}库存记录比较，需要入库", gid1, gid2),
                                StaffGid = BLL.Config.StaffGid,
                                StaffName = BLL.Config.StaffName,


                            };
                            _context.tb_yun_fileinfo_company_stock_record.Add(newModel);
                        }
                        else if (qty != 0)
                        {
                            var newModel = new DB.tb_yun_fileinfo_company_stock_record
                            {
                                Cost = item.Cost,
                                ProdName = item.ProdName,
                                Gid = Guid.NewGuid(),
                                InOut = "OUT",
                                ProdCode = item.ProdCode,
                                Price = item.Price,
                                Qty = qty,
                                Regdate = new Util().GetCurrDateTime,
                                Remark = string.Format("旧{0}与新{1}库存记录比较， 需要出库", gid1, gid2),
                                StaffGid = BLL.Config.StaffGid,
                                StaffName = BLL.Config.StaffName,

                            };
                            _context.tb_yun_fileinfo_company_stock_record.Add(newModel);
                        }
                    }

                    // 移除不存在的
                    var _oldProdStock = _context
                        .tb_yun_fileinfo_company_stock_child
                        .Where(me => me.ParentId.Equals(preMain.Gid))
                        .ToList();
                    foreach (var oldItem in _oldProdStock)
                    {
                        var prodCode = oldItem.ProdCode;
                        if (!string.IsNullOrEmpty(prodCode))
                        {
                            var query = _dbList.Count(me => me.ProdCode.Equals(prodCode));
                            if (query < 1)
                            {
                                // 出库记录，旧的库存存在，新的库存不存在， 
                                var newModel = new DB.tb_yun_fileinfo_company_stock_record
                                {
                                    Cost = oldItem.Cost,
                                    ProdName = oldItem.ProdName,
                                    Gid = Guid.NewGuid(),
                                    InOut = "OUT",
                                    ProdCode = oldItem.ProdCode,
                                    Price = oldItem.Price,
                                    Qty = oldItem.Qty,
                                    Regdate = new Util().GetCurrDateTime,
                                    Remark = string.Format("旧的{0}库存存在，新的库存不存在， 需要出库", oldItem.Gid),
                                    StaffGid = BLL.Config.StaffGid,
                                    StaffName = BLL.Config.StaffName,
                                };
                                _context.tb_yun_fileinfo_company_stock_record.Add(newModel);
                            }
                        }
                    }
                    _context.SaveChanges();
                    tran.Commit();
                    _dbMain = new DB.tb_yun_fileinfo_company_stock_main();
                    _dbList = new List<DB.tb_yun_fileinfo_company_stock_child>();
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
