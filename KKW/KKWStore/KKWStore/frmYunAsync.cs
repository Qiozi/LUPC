using KKWStore.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmYunAsync : Form
    {
        db.qstoreEntities context = new qstoreEntities();

        public class Item
        {
            public string Code { get; set; }

            public int Qty { get; set; }
        }


        public class MatchItem
        {
            public string YunCode { get; set; }


            public int YunQty { get; set; }

            public string YunProdName { get; set; }

            public Item LocalItem { get; set; }

        }

        public List<MatchItem> MatchItems { get; set; }

        public frmYunAsync()
        {
            InitializeComponent();
            this.Shown += FrmYunAsync_Shown;
        }

        private void FrmYunAsync_Shown(object sender, EventArgs e)
        {
            BindHistory();
        }

        void BindHistory()
        {

            listViewHistory1.Items.Clear();
            var query1 = context.tb_yun_stock_main.OrderByDescending(me => me.Id).Take(50).ToList();
            foreach (var item in query1)
            {
                var li = new ListViewItem(item.FileName);
                li.Tag = item.Id;
                li.SubItems.Add(item.regdate.ToString("yyyy-MM-dd HH:mm"));
                li.SubItems.Add(item.StaffName);
                listViewHistory1.Items.Add(li);
            }

            listViewHistory3.Items.Clear();
            var query3 = context.tb_yun_stock_async.OrderByDescending(me => me.Id).Take(500).ToList();
            foreach (var item in query3)
            {
                var li = new ListViewItem(item.Regdate.ToString("yyyy-MM-dd HH:mm"));
                li.Tag = item.Id;
                li.SubItems.Add(item.yun_code);
                li.SubItems.Add(item.yun_qty.ToString());
                li.SubItems.Add(item.p_code);
                li.SubItems.Add(item.p_qty.ToString());
                li.SubItems.Add((item.yun_qty - item.p_qty).ToString());
                listViewHistory3.Items.Add(li);
            }
        }

        void MatchFile()
        {
            this.Cursor = Cursors.WaitCursor;
            //using (var context = new db.qstoreEntities())
            {
                this.listView1.Items.Clear();
                var dt = new Helper.ExcelHelper(this.textBoxFile.Text).ExcelToDataTable();
                MatchItems = new List<MatchItem>();

                // 保存文件数据
                var fileInfo = new db.tb_yun_stock_main
                {
                    FileName = Path.GetFileName(this.textBoxFile.Text),
                    regdate = DateTime.Now,
                    StaffId = Helper.Config.CurrentUser.id,
                    StaffName = Helper.Config.CurrentUser.user_name
                };
                context.tb_yun_stock_main.Add(fileInfo);
                context.SaveChanges();

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    var yunCode = dr["商品编码"].ToString();
                    int yunQty = 0;
                    int.TryParse(dr["可用量"].ToString(), out yunQty);
                    var item = FindCode(context, yunCode.Trim());
                    MatchItems.Add(new KKWStore.frmYunAsync.MatchItem
                    {
                        LocalItem = item,
                        YunCode = yunCode,
                        YunQty = yunQty,
                        YunProdName = (string.IsNullOrEmpty(dr["商品规格"].ToString()) ? "" : "[" + dr["商品规格"].ToString() + "]") + dr["商品名称"].ToString()
                    });
                    var fileContextInfo = new db.tb_yun_stock_child
                    {
                        ParentId = fileInfo.Id,
                        ProdCode = yunCode,
                        ProdName = dr["商品名称"].ToString(),
                        ProdQtyAvailability = yunQty,
                        ProdQtyTotal = int.Parse(dr["总量"].ToString()),
                        ProdQtyUsedOrder = int.Parse(dr["订单占用量"].ToString()),
                        ProdQtyWarn = int.Parse(dr["警戒量"].ToString()),
                        ProdSN = dr["商品条码"].ToString(),
                        ProdStandard = dr["商品规格"].ToString(),
                        Regdate = DateTime.Now
                    };
                    context.tb_yun_stock_child.Add(fileContextInfo);
                    context.SaveChanges();
                }
            }

            foreach (var item in MatchItems)
            {
                var li = new ListViewItem(item.YunCode);
                li.SubItems.Add(item.YunQty.ToString());
                li.SubItems.Add(item.LocalItem == null ? "--" : item.LocalItem.Code);
                li.SubItems.Add(item.LocalItem == null ? "--" : item.LocalItem.Qty.ToString());
                li.SubItems.Add(item.LocalItem == null ? "" : (item.YunQty - item.LocalItem.Qty).ToString());
                li.SubItems.Add(item.LocalItem == null ? "Error" : "OK");
                listView1.Items.Add(li);
            }

            this.Cursor = Cursors.Default;

            this.groupBox1.Text = string.Format("匹配结果: 共有 {4} 个商品， 总库存 {5}， {0}个商品已匹配， {1}个商品未匹配， {2} 个需要入库，{3} 个需要出库。"
                , MatchItems.Count(me => me.LocalItem != null)
                , MatchItems.Count(me => me.LocalItem == null)
                , MatchItems.Count(me => me.LocalItem != null && me.YunQty - me.LocalItem.Qty > 0)
                , MatchItems.Count(me => me.LocalItem != null && (me.YunQty - me.LocalItem.Qty) < 0)
                , MatchItems.Count()
                , MatchItems.Sum(me => me.YunQty));
        }

        int GetQty(qstoreEntities db, string code)
        {
            return db.tb_serial_no_and_p_code.Count(me => me.p_code.Equals(code) &&
                                                          me.curr_warehouse_id.HasValue &&
                                                          me.curr_warehouse_id.Value.Equals(Helper.Config.YunWarehouseId) &&
                                                          (!me.out_regdate.HasValue || me.out_regdate.Value.Year < 2000));
        }

        Item FindCode(db.qstoreEntities context, string yunCode)
        {
            if (string.IsNullOrEmpty(yunCode))
            {
                return null;
            }

            try
            {
                var query = context.tb_product.SingleOrDefault(me => me.yun_code.Equals(yunCode));
                if (query != null)
                {
                    return new KKWStore.frmYunAsync.Item
                    {
                        Code = query.p_code,
                        Qty = GetQty(context, query.p_code)
                    };
                }
            }
            catch (Exception ex)
            {
                MessageBox.
                    Show("重复编号： " + yunCode);
            }

            return null;
        }

        private void textBoxFile_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //MessageBox.Show(openFileDialog1.FileName);
                this.textBoxFile.Text = openFileDialog1.FileName;

            }
        }



        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            //foreach (var item in MatchItems)
            //{
            //    if (item.LocalItem != null)
            //    {
            //        var diffQty = item.YunQty - item.LocalItem.Qty;
            //        if (diffQty > 0)
            //        {
            //            InStore(context, diffQty, item, DateTime.Now);
            //        }
            //        if (diffQty < 0)
            //        {
            //            OutStore(context, diffQty, item, DateTime.Now);
            //        }
            //    }
            //}

            var model = new db.tbtoken
            {
                CreateTime = DateTime.Now,
                Gid = Guid.NewGuid(),
                Showit = true,
                UserId = Helper.Config.CurrentUser.id,
                UserName = Helper.Config.CurrentUser.user_name
            };

            context.tbtokens.Add(model);
            context.SaveChanges();
            var url = "http://kkw.518n.cc/home/uploadfiletemp?gid=" + model.Gid.ToString();
           
            Process.Start("iexplore.exe", url);
        }


        /// <summary>
        /// 入库
        /// </summary>
        /// <param name="context"></param>
        /// <param name="qty"></param>
        /// <param name="item"></param>
        public void InStore(db.qstoreEntities context, int qty, MatchItem item, DateTime inDate)
        {
            var snArray = new List<string>();
            var prod = context.tb_product.Single(me => me.p_code.Equals(item.LocalItem.Code));
            string invoiceCode = db.InInvoiceModel_p.NewInvoiceCode();

            string supplier = "未知，系统自动处理";
            var total = 0M;
            var yunCost = 0M;
            if (!string.IsNullOrEmpty(prod.yun_code))
            {
                var yunInfo = context.tb_yun_stock_price.FirstOrDefault(me => me.yun_code.Equals(prod.yun_code));
                if (yunInfo != null)
                {
                    yunCost = yunInfo.yun_cost;
                }
            }

            var cost = yunCost > 0 ? yunCost : prod.p_cost.Value;
            prod.p_cost = cost; // 变化为新的cost
            total = qty * cost;

            var iim = new db.tb_in_invoice
            {
                curr_warehouse_id = Helper.Config.YunWarehouseId,
                input_regdate = inDate,
                invoice_code = invoiceCode,
                pay_method = "现金",
                note = "云仓库存同步，入库",
                pay_total = total,
                staff = Helper.Config.CurrentUser.user_name,
                regdate = inDate,
                summary = string.Empty,
                Supplier = supplier

            };

            context.tb_in_invoice.Add(iim);

            context.SaveChanges();



            var iipm = new db.tb_in_invoice_product
            {
                cost = cost,
                in_invoice_code = iim.invoice_code,
                in_invoice_id = iim.id,
                p_code = prod.p_code,
                p_id = prod.id,
                quantity = qty,
                regdate = inDate
            };

            context.tb_in_invoice_product.Add(iipm);
            context.SaveChanges();

            for (int i = 0; i < qty; i++)
            {
                var sn = db.SerialNoModel_p.GetSerialNo(context);
                snArray.Add(sn);
                var snpm = new db.tb_serial_no_and_p_code
                {
                    IsFree = false,
                    curr_warehouse_date = inDate,
                    curr_warehouse_id = Helper.Config.YunWarehouseId,
                    Comment = string.Empty,
                    in_cost = cost,
                    in_invoice_code = iim.invoice_code,
                    in_regdate = inDate,
                    is_order_code = false,
                    is_return = false,
                    IsReturnWholesaler = false,
                    IsReturnWholesaler_regdate = DateTime.MinValue,
                    out_regdate = DateTime.MinValue,
                    p_code = prod.p_code,
                    SerialNO = sn,
                    p_id = prod.id,
                    regdate = inDate,
                    return_regdate = DateTime.MinValue
                };

                context.tb_serial_no_and_p_code.Add(snpm);

            }
            context.SaveChanges();
            SaveHistory(context, snArray, prod, item, "in");
            db.ProductModel_p.ChangeQuantity(context, iipm.p_code, cost);  // 变化库存数量
        }

        /// <summary>
        /// 保存 sn 操作记录
        /// </summary>
        /// <param name="context"></param>
        /// <param name="snArray"></param>
        /// <param name="pm"></param>
        /// <param name="item"></param>
        /// <param name="type"></param>
        void SaveHistory(db.qstoreEntities context, List<string> snArray, db.tb_product pm, MatchItem item, string type = "out")
        {
            var asyncRecord = new db.tb_yun_stock_async
            {
                p_code = pm.p_code,
                p_id = pm.id,
                p_qty = item.LocalItem.Qty,
                Regdate = DateTime.Now,
                StaffId = Helper.Config.CurrentUser.id,
                StaffName = Helper.Config.CurrentUser.user_name,
                yun_code = item.YunCode,
                yun_qty = item.YunQty
            };
            context.tb_yun_stock_async.Add(asyncRecord);
            context.SaveChanges();

            foreach (var sn in snArray)
            {
                if (!string.IsNullOrEmpty(sn.Trim()))
                {
                    // sn 记录
                    var outRecord = new db.tb_yun_stock_async_code
                    {
                        cmd = type,
                        p_code = item.LocalItem.Code,
                        p_id = pm.id,
                        regdate = DateTime.Now,
                        serial_no = sn,
                        staff_id = Helper.Config.CurrentUser.id,
                        staff_name = Helper.Config.CurrentUser.user_name,
                        yun_stock_async_id = asyncRecord.Id
                    };
                    context.tb_yun_stock_async_code.Add(outRecord);
                }
            }
            context.SaveChanges();
        }

        /// <summary>
        /// 出库
        /// </summary>
        /// <param name="context"></param>
        /// <param name="qty"></param>
        /// <param name="item"></param>
        public void OutStore(db.qstoreEntities context, int qty, MatchItem item, DateTime outDate)
        {
            qty = 0 - qty;
            var snArray = new List<string>();
            DataTable singelDT = db.SerialNoAndPCodeModel_p.GetValidQuantityByProdCode(item.LocalItem.Code, Helper.Config.YunWarehouseId);
            if (singelDT.Rows.Count < qty)
            {
                MessageBox.Show(item.LocalItem.Code + " 库存不足");
                return;
            }
            for (var i = singelDT.Rows.Count - 1; i >= 0; i--)
            {
                if (i >= qty)
                {
                    singelDT.Rows.RemoveAt(i);
                }
            }

            var _total = 0M;
            foreach (DataRow dr in singelDT.Rows)
            {
                decimal cost = 0M;
                decimal.TryParse(dr["in_cost"].ToString(), out cost);
                _total += cost;
            }
            var iim = new db.tb_out_invoice();
            iim.input_regdate = DateTime.Now;
            iim.invoice_code = DateTime.Now.ToString("yyMMddhhmmss");
            iim.pay_total = _total;
            iim.Price = _total.ToString();
            iim.staff = Helper.Config.CurrentUser.user_name;
            iim.regdate = DateTime.Now;
            context.tb_out_invoice.Add(iim);
            context.SaveChanges();

            int count = 0;

            var pm = db.ProductModel_p.GetModelByCode(context, item.LocalItem.Code);

            foreach (DataRow dr in singelDT.Rows)
            {
                string sn = dr["SerialNo"].ToString();
                if (!string.IsNullOrEmpty(sn))
                {
                    snArray.Add(sn);
                    var iipm = new db.tb_out_invoice_product();

                    iipm.SerialNO = sn;
                    iipm.out_invoice_code = iim.invoice_code;
                    iipm.out_invoice_id = iim.id;
                    iipm.p_id = pm == null ? 0 : pm.id;
                    iipm.regdate = DateTime.Now;

                    db.SerialNoAndPCodeModel_p.OutStore(context, iipm.SerialNO, outDate);
                    context.tb_out_invoice_product.Add(iipm);
                    count += 1;
                }
            }
            SaveHistory(context, snArray, pm, item, "out");
            iim.SN_Quantity = count;
            context.SaveChanges();

        }

        private void 查看明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHistory1.SelectedItems != null)
            {
                if (listViewHistory1.SelectedItems.Count == 1)
                {
                    var item = listViewHistory1.SelectedItems[0];
                    var id = int.Parse(item.Tag.ToString());
                    var frm = new frmYunFileRecord(item.SubItems[0].Text, id);
                    frm.Show();
                }
            }
        }

        private void 查看明细ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listViewHistory3.SelectedItems != null)
            {
                if (listViewHistory3.SelectedItems.Count == 1)
                {
                    var id = int.Parse(listViewHistory3.SelectedItems[0].Tag.ToString());
                    var frm = new frmYunOpRecord(id);
                    frm.Show();
                }
            }
        }

        private void buttonGenerateNew_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var count = 0;
            var inrecord = new db.tb_in_invoice
            {
                regdate = DateTime.Now,
                note = "云仓库存同步，自动添加。操作者：" + Helper.Config.CurrentUser.user_name,
                staff = Helper.Config.CurrentUser.user_name,
                input_regdate = DateTime.Now,
                curr_warehouse_id = Helper.Config.YunWarehouseId
            };
            context.tb_in_invoice.Add(inrecord);
            context.SaveChanges();
            foreach (var item in MatchItems.Where(me => me.LocalItem == null))
            {
                count++;
                var pmNew = new db.tb_product();
                pmNew.p_name = item.YunProdName.Trim();
                pmNew.p_code = item.YunCode.Trim();
                pmNew.p_taobao_url = string.Empty;
                pmNew.p_cost = 0M;
                pmNew.p_price = 0m;
                pmNew.p_quantity = 0;
                pmNew.WholesalerCode = string.Empty;
                pmNew.WholesalerUrl = string.Empty;
                pmNew.WarnQty = 5;
                pmNew.brand = string.Empty;
                pmNew.yun_code = item.YunCode.Trim();
                pmNew.showit = true;
                pmNew.regdate = DateTime.Now;
                context.tb_product.Add(pmNew);
                context.SaveChanges();
                var inProd = new db.tb_in_invoice_product
                {
                    cost = 0M,
                    in_invoice_code = inrecord.invoice_code,
                    in_invoice_id = inrecord.id,
                    p_code = pmNew.p_code,
                    p_id = pmNew.id,
                    quantity = 0,
                    regdate = DateTime.Now
                };
                context.tb_in_invoice_product.Add(inProd);
                context.SaveChanges();
            }
            MessageBox.Show("已创建 " + count.ToString() + "个新产品。");
            MessageBox.Show("开始重新匹配");
            MatchFile();
            MessageBox.Show("匹配完成");
            this.Cursor = Cursors.Default;
        }

        private void buttonReadFile_Click(object sender, EventArgs e)
        {
            MatchFile();
        }
    }
}
