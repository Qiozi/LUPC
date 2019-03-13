using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace KKWStore
{
    public partial class Form1 : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        bool _isClose = true;
        public Form1()
        {
            Init();

            InitializeComponent();
            if (!_isClose)
            {
                this.FormClosed += new FormClosedEventHandler(Form1_FormClosed);
                this.Shown += new EventHandler(Form1_Shown);
            }
            else
                this.Close();
        }

        void Init()
        {
            if (null == Helper.Config.CurrentUser)
            {
                frmIP fip = new frmIP();
                fip.StartPosition = FormStartPosition.CenterParent;
                _isClose = fip.ShowDialog() != System.Windows.Forms.DialogResult.Yes;
            }


            //for (int i = 81712; i <= 82039; i++)
            //{
            //    db.SqlExec.ExecuteDataTable("update tb_serial_no_and_p_code set serialno=" + (2099919169 + i) + " where p_code='MYK-WGJS'  and id = " + i + " and curr_warehouse_id=1");
            //}

        }

        void Form1_Shown(object sender, EventArgs e)
        {

            DataTable dt = db.SqlExec.ExecuteDataTable("select max(serialNO) serialNO from tb_serial_no_and_p_code");
            if (dt.Rows.Count == 1)
            {

                string serialNo = dt.Rows[0][0].ToString();

                long max;
                long.TryParse(serialNo, out max);

                DataTable sdt = db.SqlExec.ExecuteDataTable("select max(serialNO) serialNO from tb_serial_no");
                if (sdt.Rows.Count == 1)
                {
                    string serialNoUse = sdt.Rows[0][0].ToString();
                    long noUseSerial;
                    long.TryParse(serialNoUse, out noUseSerial);

                    long diff = noUseSerial - max;

                    if (diff < 30000)
                    {
                        for (long i = noUseSerial + 1; i < noUseSerial + 30001; i++)
                        {
                            //SerialNoModel snm = new SerialNoModel();
                            var snm = new db.tb_serial_no();
                            snm.SerialNo = i.ToString();
                            snm.is_print = false;
                            snm.regdate = DateTime.Now;
                            //snm.Create();
                            context.tb_serial_no.Add(snm);
                        }
                        context.SaveChanges();
                    }
                }
            }

            //for (long i = 6201010001; i < 6201099999; i++)
            //{
            //    SerialNoModel snm = new SerialNoModel();
            //    snm.SerialNo = i.ToString();
            //    snm.Create();
            //}
            //MessageBox.Show("OK");

            #region 控制按钮显示
            if (Helper.Config.IsVisitor)
            {
                button_store.Visible = true;
                button_outStore.Visible = false;
                buttonOut2.Visible = false;
                buttonChange.Visible = false;
                buttonChangeSN.Visible = false;
                buttonIn.Visible = false;
                button_inStore.Visible = false;
                button_return.Visible = false;
                buttonReturn.Visible = false;
              
                buttonPay.Visible = false;
                buttonStat.Visible = false;
                button_udpate_saleTotal.Visible = false;
                btn_export_excel_out_store.Visible = false;
                buttonChangeSN.Visible = false;
                buttonProxy.Visible = false;
                buttonNoexistOut.Visible = false;
                buttonReturn.Visible = false;
                buttonUser.Visible = false;
                btnStockTakeClear.Visible = false;
                btnBalanceCashRecord.Visible = false;

                buttonStoreTotal.Visible = false;
                buttonCashTotal.Visible = false;
                buttonMoney.Visible = false;

                button_stocktake.Visible = false;
            }
            else if (!Helper.Config.IsAdmin)
            {
                buttonPay.Visible = false;
                buttonStat.Visible = false;
                button_udpate_saleTotal.Visible = false;
                btn_export_excel_out_store.Visible = false;
                buttonChangeSN.Visible = false;
                buttonProxy.Visible = false;
                buttonNoexistOut.Visible = false;
                buttonReturn.Visible = true;
                buttonUser.Visible = false;
                btnStockTakeClear.Visible = false;

                buttonStoreTotal.Visible = false;
                buttonCashTotal.Visible = false;
                buttonMoney.Visible = false;
                buttonOut2.Visible = false;


            }
            else
            {
                buttonOut2.Visible = false;
                buttonPay.Visible = true;
                buttonStat.Visible = true;
                //buttonModifyPwd.Visible = true;
                button_udpate_saleTotal.Visible = true;
                btn_export_excel_out_store.Visible = true;
                buttonChangeSN.Visible = true;
                buttonProxy.Visible = true;
                buttonNoexistOut.Visible = true;
                buttonUser.Visible = true;
                buttonReturn.Visible = true;
                btnStockTakeClear.Visible = true;
                btnBalanceCashRecord.Visible = true;

                buttonExportQuantity.Visible = true;
                button_cash.Visible = true;
                button_stocktake.Visible = true;
                buttonCashComein.Visible = true;
                buttonUpdateOrder.Visible = true;
                button_inStore.Visible = true;
                button_inStoreYun.Visible = true;
                buttonBargainMoney.Visible = true;


                buttonUpdateOut.Visible = true; // 上传云仓库存
            }
            if (Helper.Config.CurrentUser.id.Equals(16))
            {
                buttonOut2.Visible = true;
            }
            #endregion

            #region total , cash
            int count = 0;
            var storeTotal = db.SerialNoAndPCodeModel_p.GetStoreTotal(context, 0, ref count);
            this.buttonStoreTotal.Text = string.Concat("当前所有仓库有效库存金额：\r\n\r\n￥", storeTotal.ToString("#,###,###,##0.00"));

            var bargainMoney = context.tb_bargain_money.Where(p => p.Showit.Equals(true))
                                        .Sum(p => (decimal?)p.Cash).GetValueOrDefault();
            this.buttonBargainMoney.Text = string.Concat("当前压金总额：\r\n\r\n￥", bargainMoney);

            var incomeCash = context.tb_balance_cash_record
                                .Where(p => p.PayType.Equals(3))
                                .Sum(p => p.IncomeCash);
            var outCash = context.tb_balance_cash_record
                                .Where(p => !p.PayType.Equals(3))
                                .Sum(p => p.PayCash);

            //var proxyCash = context.tb_proxy.Where(p=>)
            this.buttonCashTotal.Text = string.Concat("当前现金流余额：\r\n\r\n￥", (incomeCash - outCash).ToString("#,###,###,##0.00"));
            this.buttonMoney.Text = string.Concat("当前资产：\r\n\r\n￥", (incomeCash - outCash + storeTotal + bargainMoney).ToString("#,###,###,##0.00"));

            #endregion

            this.Text += "   当前用户：" + Helper.Config.CurrentUser.user_name;
        }

        void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmBackup fb = new frmBackup();
            fb.StartPosition = FormStartPosition.CenterParent;
            fb.ShowDialog();
            this.Close();
        }

        private void button_inStore_Click(object sender, EventArgs e)
        {
            frmInStoreSN fsn = new frmInStoreSN();
            fsn.StartPosition = FormStartPosition.CenterParent;
            fsn.ShowDialog();
        }

        private void button_outStore_Click(object sender, EventArgs e)
        {
            frmOutStore fo = new frmOutStore(-1);
            fo.StartPosition = FormStartPosition.CenterParent;
            if (fo.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
            }
        }

        private void button_store_Click(object sender, EventArgs e)
        {
            frmProductList fpl = new frmProductList();
            fpl.StartPosition = FormStartPosition.CenterParent;
            fpl.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btn_order_Click(object sender, EventArgs e)
        {
            frmOrderList fol = new frmOrderList();
            fol.StartPosition = FormStartPosition.CenterParent;
            fol.ShowDialog();
        }

        private void button_stocktake_Click(object sender, EventArgs e)
        {
            var fcsl = new frmCheckStoreList();
            fcsl.StartPosition = FormStartPosition.CenterParent;
            fcsl.ShowDialog();
        }

        private void button_return_Click(object sender, EventArgs e)
        {
            frmReturnHistory frh = new frmReturnHistory();
            frh.StartPosition = FormStartPosition.CenterParent;
            frh.ShowDialog();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmDeleteSN df = new frmDeleteSN();
            df.StartPosition = FormStartPosition.CenterParent;
            df.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmModifyPwd fmp = new frmModifyPwd();
            fmp.StartPosition = FormStartPosition.CenterParent;
            fmp.ShowDialog();
        }

        private void btn_export_excel_out_store_Click(object sender, EventArgs e)
        {
            frmExportOutRecord feor = new frmExportOutRecord();
            feor.StartPosition = FormStartPosition.CenterParent;
            feor.ShowDialog();
        }

        private void button_udpate_saleTotal_Click(object sender, EventArgs e)
        {
            frmSaleTotalRecord frm = new frmSaleTotalRecord();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmPay fp = new frmPay();
            fp.StartPosition = FormStartPosition.CenterParent;
            fp.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmStat fs = new frmStat();
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.ShowDialog();
        }

        private void buttonProxy_Click(object sender, EventArgs e)
        {
            frmProxy fp = new frmProxy();
            fp.StartPosition = FormStartPosition.CenterParent;
            fp.ShowDialog();
        }

        private void buttonChangeSN_Click(object sender, EventArgs e)
        {
            frmChangeSN fcs = new frmChangeSN();
            fcs.StartPosition = FormStartPosition.CenterParent;
            fcs.ShowDialog();
        }

        private void buttonNoexistOut_Click(object sender, EventArgs e)
        {
            frmOutNoScan n = new frmOutNoScan();
            n.StartPosition = FormStartPosition.CenterParent;
            n.ShowDialog();
        }

        private void buttonUploadRemote_Click(object sender, EventArgs e)
        {
            //ProductModel[] pms = ProductModel.FindAll();
            ////
            //// 直接更新DB
            //db.SqlExec.RemoteExecuteNonQueryLUComputer("Delete from tb_kkw");
            //////
            ////// 直接更新DB
            ////db.SqlExec.RemoteExecuteNonQuery("Delete from tb_kkw");

            //int i=1;
            ////foreach (var p in pms)
            ////{
            ////    string name = p.p_name;
            ////    if (name.IndexOf("'") > -1)
            ////        name = name.Replace("'", "");
            ////    db.SqlExec.RemoteExecuteNonQueryLUComputer(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity,p_cost) values ('{0}', '{1}', '{2}', '{3}')"
            ////        , p.p_code, p.p_name, p.p_quantity, p.p_cost));

            ////    buttonUploadRemote.Text = string.Format("{0}/{1}", i.ToString(), pms.Length.ToString());
            ////    buttonUploadRemote.Update();
            ////    i += 1;
            ////}

            //System.Text.StringBuilder sb = new StringBuilder();

            //foreach (var p in pms)
            //{
            //    string name = p.p_name;
            //    if (name.IndexOf("'") > -1)
            //        name = name.Replace("'", "");
            //    byte[] by = Encoding.GetEncoding("utf-8").GetBytes(name);// System.Text.ASCIIEncoding.UTF8.
            //    string n = Encoding.GetEncoding("utf-8").GetString(by);
            //    sb.Append(string.Format(",('{0}', '{1}', '{2}', '{3}')", p.p_code, n, p.p_quantity, p.p_cost));
            //    if (i % 100 == 0)
            //    {
            //        db.SqlExec.RemoteExecuteNonQueryLUComputer(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity,p_cost) values {0}"
            //         , sb.ToString().Substring(1) + ";"));
            //        sb = new StringBuilder();

            //        buttonUploadRemote.Text = string.Format("{0}/{1}", i.ToString(), pms.Length.ToString());
            //        buttonUploadRemote.Update();
            //    }

            //    i += 1;
            //}
            //if (sb.ToString().Length > 0)
            //{
            //    db.SqlExec.RemoteExecuteNonQueryLUComputer(string.Format(@"insert into tb_kkw(p_code, p_name, p_quantity,p_cost) values {0}"
            //       , sb.ToString().Substring(1) + ";"));
            //}
            //buttonUploadRemote.Text = "库存上传";
            //buttonUploadRemote.Update();

            //MessageBox.Show("数据已上传");

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            frmWholesalerReturnList f = new frmWholesalerReturnList();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void buttonUser_Click(object sender, EventArgs e)
        {
            frmStaffManage fss = new frmStaffManage();
            fss.StartPosition = FormStartPosition.CenterParent;
            fss.ShowDialog();
        }

        private void buttonIn2_Click(object sender, EventArgs e)
        {

        }

        private void buttonOut3_Click(object sender, EventArgs e)
        {

        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            frmInList f = new frmInList();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            frmInStoreChange fisc = new frmInStoreChange();
            fisc.StartPosition = FormStartPosition.CenterParent;
            fisc.ShowDialog();
        }

        private void buttonOut2_Click(object sender, EventArgs e)
        {
            frmOut2 fo2 = new frmOut2();
            fo2.StartPosition = FormStartPosition.CenterParent;
            fo2.ShowDialog();
        }

        private void btnStockTakeClear_Click(object sender, EventArgs e)
        {
            frmStockTakeClear f = new frmStockTakeClear();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void btnBalanceCashRecord_Click(object sender, EventArgs e)
        {
            frmBalanceCashRecord frm = new frmBalanceCashRecord();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void button_cash_Click(object sender, EventArgs e)
        {
            frmCashStat frm = new frmCashStat();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
        }

        private void buttonCashComein_Click(object sender, EventArgs e)
        {
            frmCashComein f = new frmCashComein();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void buttonStoreTotal_Click(object sender, EventArgs e)
        {
            frmStoreList f = new frmStoreList();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void button_inStoreYun_Click(object sender, EventArgs e)
        {
            frmInStoreYun f = new frmInStoreYun();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void buttonMoney_Click(object sender, EventArgs e)
        {

        }

        private void buttonBargainMoney_Click(object sender, EventArgs e)
        {

        }

        private void buttonUpdateOrder_Click(object sender, EventArgs e)
        {
            frmOrdersManage f = new frmOrdersManage();
            f.StartPosition = FormStartPosition.CenterParent;
            f.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show(this.openFileDialog1.FileName);
                var lines = System.IO.File.ReadAllLines(this.openFileDialog1.FileName);
                var noExist = new List<string>();
                System.IO.StreamWriter sr = new System.IO.StreamWriter("C:\\Workspaces\\Demo\\s.txt");
                foreach (var sn in lines)
                {
                    var s = sn.Trim();
                    if (!string.IsNullOrEmpty(s))
                    {
                        var query = context.tb_serial_no_and_p_code.SingleOrDefault(p => p.SerialNO.Equals(s));
                        if (query != null)
                        {
                            query.curr_warehouse_id = 1;
                            context.SaveChanges();
                        }
                        else
                        {
                            sr.WriteLine(s);
                        }
                    }
                }

                sr.Close();
                MessageBox.Show("ok");
            }
        }

        private void buttonOutRecord_Click(object sender, EventArgs e)
        {
            frmOutRecord frm = new frmOutRecord();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void buttonExportQuantity_Click(object sender, EventArgs e)
        {
            frmProdQuantity frm = new frmProdQuantity();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
            // this.Cursor = Cursors.WaitCursor;
            //var query = context.tb_product.ToList();
            //foreach (var item in query)
            //{
            //    var pid = item.id;

            //    item.p_quantity = context
            //                    .tb_serial_no_and_p_code
            //                    .Count(p => p.p_id.Value.Equals(pid) &&
            //                                p.out_regdate.HasValue &&
            //                                p.out_regdate.Value.Year < 2000);

            //    context.SaveChanges();
            //}
            //var queryQtys = context.tb_product.Where(p => p.p_quantity.HasValue &&
            //                                            p.p_quantity.Value > 0).ToList();


            //MessageBox.Show("OK");


            // this.Cursor = Cursors.Default;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            frmReadExcel f = new frmReadExcel();
            f.ShowDialog();

        }

        private void buttonYunAsync_Click(object sender, EventArgs e)
        {
            frmYunAsync frm = new frmYunAsync();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void buttontUpdateYunPrice_Click(object sender, EventArgs e)
        {
            frmYunUpdatePrice frm = new frmYunUpdatePrice();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void buttonUpdateOut_Click(object sender, EventArgs e)
        {
            frmUploadYunOut frm = new frmUploadYunOut();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }
    }
}
