using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KKWStore
{
    public partial class frmPay : Form
    {
        db.qstoreEntities context = new db.qstoreEntities();
        DataTable _currQueryDT = new DataTable();

        public frmPay()
        {
            InitializeComponent();
            this.Shown += new EventHandler(frmPay_Shown);
        }

        void frmPay_Shown(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.Year, DateTime.Now.Month, "01"));
            dateTimePicker2.Value = DateTime.Parse(string.Format("{0}-{1}-{2}", DateTime.Now.AddMonths(1).Year, DateTime.Now.AddMonths(1).Month, "01")).AddDays(-1);
            BindList();
            BindCateName();
        }

        void BindCateName()
        {
            DataTable dt = db.SqlExec.ExecuteDataTable("Select distinct CateName from tb_expend order by CateName asc");
            foreach (DataRow dr in dt.Rows)
            {
                comboBox1.Items.Add(dr["CateName"].ToString());
            }
        }

        void BindList()
        {
            var strWhere = "  ";

            if (comboBoxOrder.SelectedItem != null && comboBoxOrder.SelectedItem.ToString() == "金额")
            {
                strWhere += " order by OutTotal desc ";
            }
            else if (comboBoxOrder.SelectedItem != null && comboBoxOrder.SelectedItem.ToString() == "名称")
            {
                strWhere += " order by Title desc ";
            }
            else
            {
                strWhere += " order by Regdate desc ";
            }

            var key = this.textBox1.Text.Trim();
            var cateName = this.comboBox1.Text.Trim();
            _currQueryDT = db.SqlExec.ExecuteDataTable(string.Format(@"Select * from tb_expend where CateName like '%" + cateName + @"%' and Title like '%" + key + @"%' and date_format(OutBeginDate,'%Y%m%d')>= '{0}' and date_format(OutEndDate, '%Y%m%d') <= '{1}' {2}", this.dateTimePicker1.Value.ToString("yyyyMMdd"), this.dateTimePicker2.Value.ToString("yyyyMMdd"), strWhere));
            listView1.Items.Clear();
            label_total.Text = string.Empty;
            var total = 0M;
            for (int i = 0; i < _currQueryDT.Rows.Count; i++)
            {
                DataRow dr = _currQueryDT.Rows[i];
                ListViewItem li = new ListViewItem(dr["CateName"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["Title"].ToString());
                li.SubItems.Add(dr["OutTotal"].ToString());
                li.SubItems.Add(string.Concat(DateTime.Parse(dr["OutBeginDate"].ToString()).ToString("yyyy-MM-dd"), " 到 ", DateTime.Parse(dr["OutEndDate"].ToString()).ToString("yyyy-MM-dd")));
                li.SubItems.Add(DateTime.Parse(dr["Regdate"].ToString()).ToString("yyyy-MM-dd"));

                listView1.Items.Add(li);

                decimal _total = 0M;
                decimal.TryParse(dr["OutTotal"].ToString(), out _total);
                total += _total;
            }
            label_total.Text = total.ToString("#,###,##0.00");
        }

        private void button5_Click(object sender, EventArgs e)
        {

            //if (this.dateTimePicker1.Value.Month != this.dateTimePicker2.Value.Month )
            //{
            //    MessageBox.Show("请选择同一个月的日期", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    dateTimePicker1.Focus();
            //    return;
            //}

            if (comboBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入/选择类别", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("请输入名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Focus();
                return;
            }
            if (numericUpDown1.Value <= 0)
            {
                MessageBox.Show("请输入金额", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                numericUpDown1.Focus();
                return;
            }

            //ExpendModel em = new ExpendModel();
            var em = new db.tb_expend();
            em.CateName = comboBox1.Text;
            em.Title = this.textBox1.Text.Trim();
            em.OutBeginDate = dateTimePicker1.Value;
            em.OutEndDate = dateTimePicker2.Value;
            em.Regdate = DateTime.Now;
            em.OutTotal = numericUpDown1.Value;
            //em.Create();
            em.CreateName = Helper.Config.CurrentUser.user_name;
            context.tb_expend.Add(em);
            context.SaveChanges();
            db.SqlExec.ExecuteNonQuery("update tb_expend set  average = outtotal/(To_Days(outenddate)-To_Days(outbegindate)+1) where average is null or average =0 ");

            Helper.BalanceHelper.SavePayBalance(context, em.OutTotal.Value, DateTime.Now
                , string.Concat(em.CateName, " - ", em.Title)
                , Helper.Config.CurrentUser.id
                , Helper.Config.CurrentUser.user_name
                , enums.PayType.Fee);
            MessageBox.Show("数据已添加", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            BindList();
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BindList();
            this.Cursor = Cursors.Default;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmStat fs = new frmStat();
            fs.StartPosition = FormStartPosition.CenterParent;
            fs.ShowDialog();
        }

        private void 删除选中的当条纪录ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Helper.Config.CurrentUser.id == Helper.Config.SysAdminId)
            {
                if (listView1.SelectedItems != null)
                {
                    if (MessageBox.Show("确定删除选中纪录", "询问", MessageBoxButtons.YesNo) == System.Windows.Forms.DialogResult.No)
                        return;
                    for (int i = 0; i < listView1.SelectedItems.Count; i++)
                    {
                        int id;
                        int.TryParse(listView1.SelectedItems[i].Tag.ToString(), out id);

                        var query = context.tb_expend.Single(p => p.ID.Equals(id));
                        Helper.BalanceHelper.SavePayBalance(context, 0 - query.OutTotal.Value, DateTime.Now
                           , string.Concat("删除费用支付纪录： ", query.CateName, " - ", query.Title)
                           , Helper.Config.CurrentUser.id
                           , Helper.Config.CurrentUser.user_name
                           , enums.PayType.Fee);

                        db.SqlExec.ExecuteNonQuery("delete from tb_expend where id='" + id.ToString() + "'");
                    }
                    MessageBox.Show("操作完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    BindList();
                }
                else
                {
                    MessageBox.Show("请选择需要删除的纪录.", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("只有管理员可以删除");
            }
        }

        private void btn_search2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            BindList2();
            this.Cursor = Cursors.Default;
        }

        void BindList2()
        {
            var cateName = this.comboBox1.Text;
            var key = this.textBox1.Text.Trim();
            _currQueryDT = db.SqlExec.ExecuteDataTable(string.Format(@"Select * from tb_expend where CateName like '%" + cateName + @"%' and Title like '%" + key + @"%' and date_format(OutEndDate,'%Y%m%d')>= '{0}' and date_format(OutBeginDate,'%Y%m%d')<= '{1}' order by id desc", this.dateTimePicker1.Value.ToString("yyyyMMdd"), this.dateTimePicker2.Value.ToString("yyyyMMdd")));

            listView1.Items.Clear();
            for (int i = 0; i < _currQueryDT.Rows.Count; i++)
            {
                DataRow dr = _currQueryDT.Rows[i];
                ListViewItem li = new ListViewItem(dr["CateName"].ToString());
                li.Tag = dr["id"].ToString();
                li.SubItems.Add(dr["Title"].ToString());
                li.SubItems.Add(dr["OutTotal"].ToString());
                li.SubItems.Add(string.Concat(DateTime.Parse(dr["OutBeginDate"].ToString()).ToString("yyyy-MM-dd"), " 到 ", DateTime.Parse(dr["OutEndDate"].ToString()).ToString("yyyy-MM-dd"), "(平均每天:￥", dr["Average"].ToString(), ")"));
                li.SubItems.Add(DateTime.Parse(dr["Regdate"].ToString()).ToString("yyyy-MM-dd"));

                listView1.Items.Add(li);
            }
        }
        /// <summary>
        ///     CateName        类型
        ///     Title           名称
        ///     OutTotal        金额
        ///     OutBeginDate    开始日期
        ///     OutEndDate      结束日期
        ///     Regdate         输入日期
        ///     CreateName      创建人
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            var newDt = new DataTable();
            newDt.Columns.Add("类型");
            newDt.Columns.Add("名称");
            newDt.Columns.Add("金额");
            newDt.Columns.Add("开始日期");
            newDt.Columns.Add("结束日期");
            newDt.Columns.Add("输入日期");
            newDt.Columns.Add("创建人");

            var newClone = _currQueryDT.Clone();

            for (int i = 0; i < _currQueryDT.Rows.Count; i++)
            {
                var dr = newDt.NewRow();
                dr["类型"] = _currQueryDT.Rows[i]["CateName"].ToString();
                dr["名称"] = _currQueryDT.Rows[i]["Title"].ToString();
                dr["金额"] = _currQueryDT.Rows[i]["OutTotal"].ToString();
                dr["开始日期"] = _currQueryDT.Rows[i]["OutBeginDate"].ToString();
                dr["结束日期"] = _currQueryDT.Rows[i]["OutEndDate"].ToString();
                dr["输入日期"] = _currQueryDT.Rows[i]["Regdate"].ToString();
                dr["创建人"] = _currQueryDT.Rows[i]["CreateName"].ToString();

                newDt.Rows.Add(dr);
            }
            Helper.NPOIExcel.ToExcel(newDt, "费用支出", Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "\\费用支出.xls");
            MessageBox.Show("文件已放桌面");
        }
    }
}
