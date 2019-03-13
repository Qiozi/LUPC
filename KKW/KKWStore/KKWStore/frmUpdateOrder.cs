using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmUpdateOrder : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();

        private delegate void SetPos(int ipos, int index, int total);
        public frmUpdateOrder()
        {
            InitializeComponent();
            this.Shown += FrmUpdateOrder_Shown;
        }

        private void FrmUpdateOrder_Shown(object sender, EventArgs e)
        {
            GetAllRecordTotal();
        }

        void GetAllRecordTotal()
        {
            var total = context.tb_order.Count();
            this.label2.Text = string.Concat("Total: ", total);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFilename.Text))
            {
                MessageBox.Show("please select file.");
                return;
            }
            //this.buttonUpdate.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            var thread = new Thread(new ThreadStart(SleepT));
            thread.Start();
            this.Cursor = Cursors.Default;
        }



        private void textBoxFilename_Click(object sender, EventArgs e)
        {

        }

        private void textBoxFilename_DoubleClick(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBoxFilename.Text = openFileDialog1.FileName;
            }
        }

        private void SleepT()
        {

            var dt = Helper.NPOIExcel.ToDataTable(textBoxFilename.Text, 4);
            var totalRecord = dt.Rows.Count;

            // 删除 已存在的
            var traceNos = new List<string>();
            foreach (DataRow dr in dt.Rows)
            {
                traceNos.Add(dr["交易号"].ToString().Trim());
            }

            // label1.Text = "Deleting";
            var delIndex = 0;
            var query = context.tb_order.Where(me => traceNos.Contains(me.TraceNo)).ToList();
            foreach (var item in query)
            {
                delIndex++;
                context.tb_order.Remove(item);
                if (delIndex % 1000 == 0)
                {
                    context.SaveChanges();
                    delIndex = 0;
                }
            }
            context.SaveChanges();


            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var dr = dt.Rows[i];

                SetTextMessage(100 * (i + 1) / totalRecord, i + 1, totalRecord);

                if (string.IsNullOrEmpty(dr["类型"].ToString()))
                {
                    continue;
                }
                DateTime createTime;
                DateTime.TryParse(dr["交易创建时间"].ToString(), out createTime);
                DateTime payTime;
                DateTime.TryParse(dr["付款时间"].ToString(), out payTime);

                DateTime modifyTime;
                DateTime.TryParse(dr["最近修改时间"].ToString(), out modifyTime);

                var total = 0M;
                decimal.TryParse(dr["金额（元）"].ToString(), out total);

                var serviceFee = 0M;
                decimal.TryParse(dr["服务费（元）"].ToString(), out serviceFee);

                var returnFee = 0M;
                decimal.TryParse(dr["成功退款（元）"].ToString(), out returnFee);

                var traceNo = dr["交易号"].ToString().Trim();

                var model = new db.tb_order
                {
                    TraceNo = dr["交易号"].ToString().Trim(),
                    BrandNo = dr["商户订单号"].ToString().Trim(),
                    CreateTime = createTime,
                    PayTime = payTime,
                    ModifyTime = modifyTime,
                    Source = dr["交易来源地"].ToString().Trim(),
                    OrderType = dr["类型"].ToString().Trim(),
                    CustomerName = dr["交易对方"].ToString().Trim(),
                    ProdName = dr["商品名称"].ToString().Trim(),
                    Total = total,
                    InOut = dr["收/支"].ToString().Trim(),
                    OrderStatus = dr["交易状态"].ToString().Trim(),
                    ServiceFee = serviceFee,
                    ReturnFee = returnFee,
                    Note = dr["备注"].ToString().Trim(),
                    MoneyStatus = dr["资金状态"].ToString().Trim(),
                    Flag = 0,
                    Regdate = DateTime.Now,
                    Staff = Helper.Config.CurrentUser.user_name
                };

                if (model.CustomerName == "重庆市阿里巴巴小额贷款有限公司")
                {
                    continue;
                }
                context.tb_order.Add(model);
                context.SaveChanges();
            }

            //this.buttonUpdate.Enabled = true;
        }

        private void SetTextMessage(int ipos, int index, int total)
        {
            if (this.InvokeRequired)
            {
                SetPos setpos = new SetPos(SetTextMessage);
                this.Invoke(setpos, new object[] { ipos, index, total });
            }
            else
            {
                this.label1.Text = index.ToString() + "/" + total.ToString();
                this.progressBar1.Value = Convert.ToInt32(ipos);
            }
        }
    }
}
