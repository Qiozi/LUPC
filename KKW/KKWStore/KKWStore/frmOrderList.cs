using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using Taobao.Top.Api;
//using Taobao.Top.Api.Domain;
//using Taobao.Top.Api.Request;
using System.Linq;

namespace KKWStore
{
    public partial class frmOrderList : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        public frmOrderList()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmOrderList_Shown);

        }

        void frmOrderList_Shown(object sender, EventArgs e)
        {
            this.toolStripComboBox1.Text = "100";
            //webBrowser1.Url = new Uri("http://open.taobao.com/authorize/?appkey=12156166");
            Helper.TaobaoConfig.SessionKey = "2826967bfacea1c5671d515ea0d364ab1a6d2";
            BindList();
        }

        void BindList()
        {
            string staff = "";// this.toolStripComboBox_Staff.Text;
            string keyword = this.toolStripTextBox1.Text.Trim();
            //if (keyword.Length == 14)
            //    keyword = keyword.Replace("A", "");
            int pageSize = 100;
            int.TryParse(this.toolStripComboBox1.Text, out pageSize);
            if (pageSize == 0) pageSize = 100;
            DataTable dt = db.OutInvoiceModel_p.GetListBySearch(keyword, pageSize, staff);
            this.listView1.Items.Clear();

            foreach (DataRow dr in dt.Rows)
            {
                ListViewItem li = new ListViewItem(dr["store_name"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(DateTime.Parse(dr["input_regdate"].ToString()).ToShortDateString());
                li.SubItems.Add(dr["invoice_code"].ToString());
                li.SubItems.Add(dr["ReceiverName"].ToString());
                li.SubItems.Add(dr["ReceiverAddress"].ToString());
                li.SubItems.Add(dr["ReceiverMobile"].ToString());
                li.SubItems.Add(dr["SN_Quantity"].ToString());
                li.SubItems.Add(db.OutInvoiceProductShippingModel_p.GetShippingSN(context,int.Parse(dr["id"].ToString())));    // 快递单
                li.SubItems.Add(dr["pay_total"].ToString());
                this.listView1.Items.Add(li);
            }

            dt = db.OutInvoiceModel_p.GetStoreTotal();
            this.toolStripStatusLabel1.Text = "";
            this.toolStripStatusLabel2.Text = "";
            this.toolStripStatusLabel3.Text = "";
            this.toolStripStatusLabel4.Text = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i]["staff"].ToString().Trim() == "" ? "其他" : dt.Rows[i]["staff"].ToString();

                if (i == 0)
                {
                    this.toolStripStatusLabel1.Text = string.Format("{0}: {1}个"
                        , name
                        , dt.Rows[i]["c"].ToString());
                }
                if (i == 1)
                {
                    this.toolStripStatusLabel2.Text = string.Format("{0}: {1}个"
                        , name
                        , dt.Rows[i]["c"].ToString());
                }
                if (i == 2)
                {
                    this.toolStripStatusLabel3.Text = string.Format("{0}: {1}个"
                        , name
                        , dt.Rows[i]["c"].ToString());
                }
                if (i == 3)
                {
                    this.toolStripStatusLabel4.Text = string.Format("{0}: {1}个"
                        , name
                        , dt.Rows[i]["c"].ToString());
                }
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            frmSessionKey fsk = new frmSessionKey();
            fsk.StartPosition = FormStartPosition.CenterParent;
            if (fsk.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {

            }
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            BindList();
        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                BindList();
            }
        }

        private void toolStripTextBox1_Enter(object sender, EventArgs e)
        {
            this.toolStripTextBox1.Text = "";
        }
        /// <summary>
        /// 添加商品给选中的订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count > 0)
            {
                int invoicID = int.Parse(this.listView1.SelectedItems[0].Tag.ToString());
                frmOutStore fos = new frmOutStore(invoicID);
                fos.StartPosition = FormStartPosition.CenterParent;
                if (fos.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
                {
                    BindList();
                }
            }
            else
                MessageBox.Show("请选择需要编辑的订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// 创建出库订单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            frmOutStore fos = new frmOutStore(-1);
            fos.StartPosition = FormStartPosition.CenterParent;
            if (fos.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                BindList();
            }
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("确认要从taobao网加载订单么?", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.Cancel)
            //{
            //    return;
            //}

            //this.Cursor = Cursors.WaitCursor;

            //try
            //{
            //    int OrderPageCount = int.Parse(System.Configuration.ConfigurationManager.AppSettings["OrderPageCount"].ToString());

            //    List<Helper.TaobaoKey> keys = Helper.TaobaoConfig.GetTaobaoKeys();

            //    foreach (var s in keys)
            //    {
            //        if (s.AppKey.Trim() == "") continue;

            //        TopXmlRestClient client = new TopXmlRestClient(Helper.TaobaoConfig.UrlAPI
            //            , s.AppKey
            //            , s.AppSecret);

            //        TradesSoldGetRequest req = new TradesSoldGetRequest();

            //        for (int i = 1; i <= OrderPageCount; i++)
            //        {
            //            req.Fields = "tid,seller_nick,buyer_nick,ReceivedPayment,ReceiverAddress,ReceiverCity,ReceiverDistrict,ReceiverMobile,ReceiverName,ReceiverPhone,ReceiverState,ReceiverZip";
            //            req.PageSize = 40;
            //            req.PageNo = i;
            //            PageList<Trade> rsp = client.TradesSoldGet(req, Helper.TaobaoConfig.SessionKey);
            //            long totalResults = rsp.TotalResults;
            //            List<Trade> trades = rsp.Content;

            //            foreach (Trade t in trades)
            //            {
            //                Trade trade = new Trade();
            //                bool add = false;
            //                //OutInvoiceModel oim = db.OutInvoiceModel_p.GetModelByTid(t.Tid.ToString());
            //                var oim = db.OutInvoiceModel_p.GetModelByTid(context, t.Tid.ToString());
            //                if (oim == null)
            //                {
            //                    oim = new db.tb_out_invoice();// new OutInvoiceModel();
            //                    add = true;
            //                    TradeGetRequest req2 = new TradeGetRequest();
            //                    req2.Fields = "tid,seller_nick,buyer_nick,ReceivedPayment,ReceiverAddress,ReceiverCity,ReceiverDistrict,ReceiverMobile,ReceiverName,ReceiverPhone,ReceiverState,ReceiverZip";
            //                    req2.Tid = t.Tid;

            //                    trade = client.TradeGet(req2);
            //                }

            //                oim.store_name = s.name;
            //                oim.invoice_code = t.Tid.ToString();
            //                oim.input_regdate = DateTime.Now;
            //                oim.ReceiverName = t.BuyerNick;
            //                oim.Tid = t.Tid.ToString();
            //                oim.staff = t.SellerNick;
            //                oim.is_Taobao = true;
                                               

            //                //if (add)
            //                //    oim.Create();
            //                //else
            //                //    oim.Update();
            //                if (add)
            //                {
            //                    context.AddTotb_out_invoice(oim);
            //                }
            //                context.SaveChanges();
            //            }
            //        }
            //    }
            //    BindList();
            //}
            //catch (Exception ex)
            //{
            //    Helper.Logs.WriteErrorLog(ex);
            //    MessageBox.Show(ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
            //this.Cursor = Cursors.Default;
        }

        private void toolStripComboBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }

        private void toolStripComboBox_Staff_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }

        private void toolStripTextBox1_Click_1(object sender, EventArgs e)
        {
           
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            ToolStripTextBox TB = (ToolStripTextBox)sender;
            string text = TB.Text;
            TB.Text = Helper.CharacterHelper.ToDBC(text);
        }
    }
}
