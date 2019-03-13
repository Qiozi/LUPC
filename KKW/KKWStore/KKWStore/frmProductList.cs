using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Taobao.Top.Api;
//using Taobao.Top.Api.Domain;
using System.Linq;
//using Taobao.Top.Api.Request;
namespace KKWStore
{
    public partial class frmProductList : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        bool _isWarn = false;
        public frmProductList()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmProductList_Shown);
        }

        void frmProductList_Shown(object sender, EventArgs e)
        {
            toolStripComboBox1.Text = "只显示有库存";
            BindBrand();
            // BindList(_isWarn);

            #region warehouse
            List<db.tb_warehouse> wmList = new List<db.tb_warehouse>();
            wmList.Add(new db.tb_warehouse()
            {
                ID = 0,
                WarehouseName = "全部"
            });
            var warehouseList = context.tb_warehouse.Where(me => me.showit.Equals(true)).ToList();// WarehouseModel.FindAll();
            for (int i = 0; i < warehouseList.Count; i++)
            {
                wmList.Add(new db.tb_warehouse()
                {
                    ID = warehouseList[i].ID,
                    WarehouseName = warehouseList[i].WarehouseName
                });
            }
            toolStripComboBoxWarehouse.ComboBox.DataSource = wmList;
            toolStripComboBoxWarehouse.ComboBox.DisplayMember = "WarehouseName";
            toolStripComboBoxWarehouse.ComboBox.ValueMember = "ID";
            toolStripComboBoxWarehouse.SelectedIndexChanged += new EventHandler(toolStripComboBoxWarehouse_SelectedIndexChanged);
            #endregion

            if (Helper.Config.IsVisitor)
            {
                toolStripButton_new.Enabled = false;
                toolStripButton_modify.Enabled = false;
                toolStripButton3.Enabled = false;
                toolStripButton4.Enabled = false;
                toolStripButton_product_check_store.Enabled = false;
            }
        }

        #region Bind
        /// <summary>
        /// 
        /// </summary>
        void BindList(bool IsWarn)
        {
            this.Cursor = Cursors.WaitCursor;

            int warehouseID = toolStripComboBoxWarehouse.ComboBox.SelectedValue == null ? 0 : ((db.tb_warehouse)toolStripComboBoxWarehouse.ComboBox.SelectedItem).ID;
            toolStripStatusLabel1.Text = warehouseID == 0 ? "当前仓库：全部" : "当前仓库：" + ((db.tb_warehouse)toolStripComboBoxWarehouse.ComboBox.SelectedItem).WarehouseName;

            bool havePermantent = Helper.PermantentHelper.Ok(enums.PermanentModel.查看进价);

            string brand = toolStripComboBox_brand.Text.Trim();

            string keyword = toolStripTextBox_keyword.Text.Trim();

            var stockStatus = toolStripComboBox1.Text == "只显示有库存" ? enums.stock.stock : (toolStripComboBox1.Text == "显示全部" ? enums.stock.all : enums.stock.noStock);
            DataTable dt = new DataTable();


            dt = db.ProductModel_p.GetModelsByKeyword(keyword
                , stockStatus
                , IsWarn
                , brand
                , warehouseID);

            this.listView1.Items.Clear();
            int n = 1;
            int quantity = 0;
            decimal total = 0M;
            var ids = new List<int>();

            foreach (DataRow dr in dt.Rows)
            {
              
                decimal _total = 0M;
                int _qty;
                //if (warehouseID == 0) // 2014-09-03  全部改为，显神，公司，已打包
                //    int.TryParse(dr["p_quantity"].ToString(), out _qty);
                //else
                int.TryParse(dr["c"].ToString(), out _qty);
                if (stockStatus == enums.stock.stock)
                {
                    if (_qty < 1) continue;
                }
                if (stockStatus == enums.stock.noStock)
                {
                    if (_qty > 0) continue;
                }
                quantity += _qty;
                // _total += decimal.Parse();// int.Parse(dr["p_quantity"].ToString()) * decimal.Parse(dr["p_cost"].ToString());
                //if (warehouseID == 0)  // 2014-09-03  全部改为，显神，公司，已打包
                //    _total = int.Parse(dr["p_quantity"].ToString()) * decimal.Parse(dr["p_cost"].ToString());
                //else
                decimal.TryParse(dr["costALL"].ToString(), out _total);
                total += _total;

                decimal cost;
                decimal.TryParse(dr["p_cost"].ToString(), out cost);

                if (warehouseID > 0)
                {
                    if (warehouseID == Helper.Config.YunWarehouseId)
                    {
                        if (dr["yunQty"].ToString() == "0") continue;
                    }
                    if (warehouseID == Helper.Config.CompanyWarehouseId)
                    {
                        if (dr["companyQty"].ToString() == "0") continue;
                    }
                    if (warehouseID == Helper.Config.BadGoodsWarehouseId)
                    {
                        if (dr["badQty"].ToString() == "0") continue;
                    }
                    if (warehouseID == Helper.Config.TempWarehouseId)
                    {
                        continue;
                    }
                }

                ListViewItem li = new ListViewItem(n.ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["yun_code"].ToString());
                li.SubItems.Add(dr["p_code"].ToString());
                li.SubItems.Add(dr["WholesalerCode"].ToString());
                li.SubItems.Add(dr["p_name"].ToString());
                li.SubItems.Add(dr["companyQty"].ToString());
                li.SubItems.Add(dr["yunQty"].ToString());
                li.SubItems.Add(dr["badQty"].ToString());
                li.SubItems.Add(_qty.ToString());
                li.SubItems.Add(dr["WarnQty"].ToString());
                li.SubItems.Add(havePermantent ? cost.ToString("###,##0.00") : "--");
                li.SubItems.Add(havePermantent ? _total.ToString("###,##0.00") : "--");
                li.SubItems.Add(dr["WholesalerUrl"].ToString());
                li.SubItems.Add(dr["p_taobao_url"].ToString());
                ids.Add(int.Parse(dr["id"].ToString()));
                this.listView1.Items.Add(li);
                int q1 = _qty;
                int q2;
                // int.TryParse(dr["p_quantity"].ToString(), out q1);
                int.TryParse(dr["WarnQty"].ToString(), out q2);
                if (q1 < q2)
                    this.listView1.Items[n - 1].BackColor = Color.LightPink;
                n += 1;
            }
            // var context = new db.qstoreEntities();
            var query = db.SqlExec.ExecuteDataTable(
                                    warehouseID == 0
                                                ? "Select in_cost from tb_serial_no_and_p_code where out_regdate like '000%' and is_return=0 and p_id in (" + string.Join(",", ids.Count == 0 ? new List<int> { 0 } : ids) + ")"
                                                : "Select in_cost from tb_serial_no_and_p_code where out_regdate like '000%' and is_return=0 and p_id in (" + string.Join(",", ids.Count == 0 ? new List<int> { 0 } : ids) + ") and curr_warehouse_id='" + warehouseID + "'"
                                                    );
            var allCost = 0M;
            foreach (DataRow dr in query.Rows)
            {
                var cost = 0M;
                decimal.TryParse(dr[0].ToString(), out cost);
                allCost += cost;
            }
            this.toolStripStatusLabel_quantity.Text = "库存总数量:  " + query.Rows.Count.ToString();
            this.toolStripStatusLabel_total.Text = "库存总价:  " + (havePermantent ? allCost.ToString("0.00") : "--");

            this.Cursor = Cursors.Default;
        }

        void BindBrand()
        {
            toolStripComboBox_brand.Items.Clear();
            List<string> list = db.ProductModel_p.GetBrand();
            toolStripComboBox_brand.Items.Add("全部");
            for (int i = 0; i < list.Count; i++)
            {
                toolStripComboBox_brand.Items.Add(list[i]);
            }
        }
        #endregion

        /// <summary>
        /// 添加新产品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton_new_Click(object sender, EventArgs e)
        {
            frmProductEdit fpe = new frmProductEdit(-1, "");
            fpe.StartPosition = FormStartPosition.CenterParent;
            if (fpe.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList(_isWarn);
            }
        }


        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BindList(_isWarn);
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                //int pid = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                //ProductModel pm = db.ProductModel_p.GetProductModel(pid);
                //frmProductHistory fph = new frmProductHistory(pm.p_code);
                //fph.StartPosition = FormStartPosition.CenterParent;
                //fph.ShowDialog();
            }
        }

        private void toolStripButton_modify_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int id;
                int.TryParse(this.listView1.SelectedItems[0].Tag.ToString(), out id);
                frmProductEdit fpe = new frmProductEdit(id, "");
                fpe.StartPosition = FormStartPosition.CenterParent;
                if (fpe.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    BindList(_isWarn);
                }
            }
            else
            {
                MessageBox.Show("请选中列表中要修改的产品", "提示", MessageBoxButtons.OK);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //frmInStore fis = new frmInStore();
            //fis.StartPosition = FormStartPosition.CenterParent;
            //if (fis.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            //{
            //    BindList(_isWarn);
            //}
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            frmOutStore fos = new frmOutStore(-1);
            fos.StartPosition = FormStartPosition.CenterParent;
            if (fos.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList(_isWarn);
            }
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("确认要从taobao网加载产品么?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
            //{
            //    return;
            //}
            //TopXmlRestClient client = new TopXmlRestClient(Helper.TaobaoConfig.UrlAPI
            //   , Helper.TaobaoConfig.AppKey
            //   , Helper.TaobaoConfig.AppSecret);

            //ItemsAllGetRequest req = new ItemsAllGetRequest();
            //req.Fields = @"approve_status,iid,num_iid,title,nick,type,cid,pic_url,num,props,valid_thru,list_time,price,has_discount,has_invoice,has_warranty,has_showcase,modified,delist_time,postage_id,seller_cids,outer_id";
            //req.PageNo = 1;
            //req.PageSize = 100;
            //PageList<Item> rsp = client.ItemsAllGet(req);
            //long totalResults = rsp.TotalResults;
            //List<Item> trades = rsp.Content;

            //foreach (Item t in trades)
            //{
            //    var pm = context.tb_product.SingleOrDefault(p => p.p_name.Equals(t.Title));// ProductModel.FindAllByProperty("p_name", t.Title);
            //    if (pm != null)
            //    {
            //        pm.p_name = t.Title;
            //        pm.p_taobao_url = t.DetailUrl;
            //        pm.num_iid = t.NumIid.ToString();
            //        pm.showit = true;
            //    }
            //    else
            //    {
            //        pm = new db.tb_product();// new ProductModel();
            //        pm.p_name = t.Title;
            //        pm.p_taobao_url = t.DetailUrl;
            //        pm.num_iid = t.NumIid.ToString();
            //        pm.showit = true;
            //        context.AddTotb_product(pm);
            //    }
            //    context.SaveChanges();
            //}
            //BindList(_isWarn);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            frmReturnHistory frh = new frmReturnHistory();
            frh.StartPosition = FormStartPosition.CenterParent;
            if (frh.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                BindList(_isWarn);
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmCheckStoreList fcsl = new frmCheckStoreList();
            fcsl.StartPosition = FormStartPosition.CenterParent;
            fcsl.ShowDialog();
        }

        private void toolStripButton_product_check_store_Click(object sender, EventArgs e)
        {

            if (this.listView1.SelectedItems.Count > 0)
            {
                int pid = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                var pm = context.tb_product.Single(p => p.id.Equals(pid));// db.ProductModel_p.GetProductModel(pid);
                frmProductCheckStore fpcs = new frmProductCheckStore(pm);
                fpcs.StartPosition = FormStartPosition.CenterParent;
                if (fpcs.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    BindList(_isWarn);
                }
            }
            else
            {
                MessageBox.Show("请选中列表中要修改的产品", "提示", MessageBoxButtons.OK);
            }
        }

        private void toolStripTextBox_keyword_Click(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox_keyword_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox TB = (ToolStripTextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton_export_Click(object sender, EventArgs e)
        {
            frmProductExport fpe = new frmProductExport();
            fpe.StartPosition = FormStartPosition.CenterParent;
            if (fpe.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                if (Vers.ExportMaxQuantity < 5)
                    Vers.ExportMaxQuantity = 5;

                List<ExportExcel> list = new List<ExportExcel>();
                for (int i = 0; i < this.listView1.Items.Count; i++)
                {
                    int quantity;
                    int.TryParse(listView1.Items[i].SubItems[4].Text, out quantity);
                    if (quantity > Vers.ExportMaxQuantity)
                        continue;
                    list.Add(new ExportExcel()
                    {
                        Code = listView1.Items[i].SubItems[1].Text,
                        ProductName = listView1.Items[i].SubItems[3].Text,
                        Cost = listView1.Items[i].SubItems[6].Text,
                        Quantity = listView1.Items[i].SubItems[4].Text
                    });
                }
                Export(list);
            }
        }

        void Export(List<ExportExcel> list)
        {
            string[] columnsName = new string[] { "编号", "产品名称", "进价", "库存" };
            //string[] fieldsName = new string[] { "sys_user_name", "summary", "regdate" };


            if (list.Count == 0)
            {
                MessageBox.Show("没有需要导出的产品数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DataTable dt = new DataTable();
            for (int i = 0; i < columnsName.Length; i++)
            {
                dt.Columns.Add(columnsName[i]);
            }

            foreach (var m in list)
            {
                DataRow dr = dt.NewRow();
                dr["编号"] = m.Code;
                dr["产品名称"] = m.ProductName;
                dr["进价"] = m.Cost;
                dr["库存"] = m.Quantity;
                dt.Rows.Add(dr);

            }
            Helper.NPOIExcel.ToExcel(dt, "库存" + DateTime.Now.ToString("yyyy-MM-dd"), Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\库存" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls");

            //Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            //if (xlApp == null)
            //{
            //    MessageBox.Show("请确保您的电脑已经安装Excel", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //xlApp.UserControl = true;
            //Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
            ////根据模版产生新的workbook //Workbook workbook = workbooks.Add("D:\\aa.xls");
            //Excel.Workbook workbook = workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            //Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
            //if (worksheet == null)
            //{
            //    MessageBox.Show("请确保您的电脑已经安装Excel", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //try
            //{

            //    Excel.Range range;
            //    long totalCount = list.Count;
            //    //long rowRead = 0;
            //    //float percent = 0;

            //    worksheet.Cells[1, 1] = "导出日期: " + DateTime.Now.ToString();
            //    worksheet.Cells[1, 2] = "";
            //    worksheet.Cells[1, 3] = "";
            //    worksheet.Cells[1, 4] = "";


            //    //写入字段
            //    for (int i = 0; i < columnsName.Length; i++)
            //    {
            //        worksheet.Cells[2, i + 1] = columnsName[i];
            //        range = (Excel.Range)worksheet.Cells[2, i + 1];
            //        range.Interior.ColorIndex = 15;
            //        range.Font.Bold = true;

            //    }
            //    //写入数值

            //    for (int r = 0; r < list.Count; r++)
            //    {

            //        worksheet.Cells[r + 3, 1] = list[r].Code;
            //        worksheet.Cells[r + 3, 2] = list[r].ProductName;
            //        worksheet.Cells[r + 3, 3] = list[r].Cost;
            //        worksheet.Cells[r + 3, 4] = list[r].Quantity;

            //    }


            //    xlApp.Visible = true;

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("到出Excel失败！\r\n" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);

            //}
            //finally
            //{

            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(worksheet);

            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);

            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbooks);

            //    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
            //    //KillProcess("Excel");
            //    GC.Collect();//强行销毁         
            //}
        }

        private void 删除此产品ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (MessageBox.Show("你确认删除选中的产品?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.OK)
            {
                bool IsDel = false;

                if (Helper.Config.IsAdmin)
                {
                    IsDel = true;
                }
                else
                {
                    MessageBox.Show("你没有权限，无法操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                if (IsDel)
                {
                    if (listView1.SelectedItems != null)
                    {
                        int count = 0;
                        for (int i = 0; i < listView1.SelectedItems.Count; i++)
                        {
                            int id;
                            int.TryParse(listView1.SelectedItems[i].Tag.ToString(), out id);
                            db.ProductModel_p.DeleteById(id);
                            count++;
                        }
                        BindList(_isWarn);
                        MessageBox.Show("已删除 " + count.ToString() + " 个产品", "提示", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList(_isWarn);
        }

        private void 淘宝URLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ViewWeb(listView1.SelectedItems[0].SubItems[9].Text);
                }
            }
        }

        private void 批发商URLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems != null)
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ViewWeb(listView1.SelectedItems[0].SubItems[8].Text);
                }
            }
        }
        void ViewWeb(string url)
        {
            if (!string.IsNullOrEmpty(url.Trim()))
                System.Diagnostics.Process.Start("IEXPLORE.EXE", url.Trim());
        }

        private void toolStripButton_warn_Click(object sender, EventArgs e)
        {

            _isWarn = !_isWarn;
            toolStripButton_warn.BackColor = _isWarn ? Color.Blue : System.Windows.Forms.Button.DefaultBackColor;
            BindList(_isWarn);
        }



        private void toolStripComboBox_brand_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList(_isWarn);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            KKWStore.Helper.ExportExcel.Export(listView1, "产品列表");
        }

        private void frmProductList_Load(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 修改整批进价ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int pid = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                frmModifyCost pm = new frmModifyCost(pid);
                pm.StartPosition = FormStartPosition.CenterParent;
                pm.ShowDialog();
            }
        }

        private void toolStripComboBoxWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BindList(_isWarn);
            this.Cursor = Cursors.Default;
        }

        private void 各仓库条码明细ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int pid = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());

            frmStoreCodeDetail f = new frmStoreCodeDetail(pid);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == DialogResult.Retry)
            {
            }
            else
                BindList(false);
        }
    }

    public class ExportExcel
    {
        public string Code { set; get; }
        public string ProductName { set; get; }
        public string Quantity { set; get; }
        public string Cost { get; set; }
        public ExportExcel() { }
    }
}
