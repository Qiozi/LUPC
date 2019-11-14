using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.BLL;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmProfirtManager : Form
    {
        DB.kkwEntities _context = new DB.kkwEntities();
        string _currGid = string.Empty;
        List<string> _monthList = new List<string>();

        public frmProfirtManager()
        {
            InitializeComponent();
            this.Shown += FrmProfirtManager_Shown;
        }

        private void FrmProfirtManager_Shown(object sender, EventArgs e)
        {
            BindCtrl();
            BindList();

            NewStatus(); ClearControl();
        }
        void BindCtrl(string currDate = "")
        {
            _monthList = new List<string>();
            for (var i = DateTime.Now.Year; i >= 2019; i--)
            {
                if (i < DateTime.Now.Year)
                {
                    for (var j = 12; j >= 1; j--)
                    {
                        _monthList.Add(string.Format("{0}-{1}", i.ToString(), j.ToString("00")));
                    }
                }
                else
                {
                    for (var j = DateTime.Now.Month; j >= 1; j--)
                    {
                        _monthList.Add(string.Format("{0}-{1}", i.ToString(), j.ToString("00")));
                    }
                }
            }

            foreach (var item in _monthList)
                this.comboBoxProfitDate.Items.Add(item);

            this.comboBoxProfitDate.SelectedItem = string.IsNullOrEmpty(currDate)
                ? DateTime.Now.ToString("yyyy-MM")
                : currDate;
        }

        void ClearControl()
        {
            this.textBoxProfit.Text = "0";
            this.comboBoxProfitDate.SelectedItem = DateTime.Now.ToString("yyyy-MM");

            for (var i = 0; i < this.Controls.Count; i++)
            {
                if (Controls[i] is GroupBox)
                {
                    for (var j = 0; j < Controls[i].Controls.Count; j++)
                    {
                        if (Controls[i].Controls[j] is NumericUpDown)
                        {
                            NumericUpDown ctr = Controls[i].Controls[j] as NumericUpDown;
                            ctr.Value = 0M;
                        }
                    }
                }
            }
        }

        void ModifyStatus()
        {
            this.buttonSave.Enabled = false;
            this.buttonDel.Enabled = true;
            this.buttonNew.Enabled = true;
        }

        void AddStatus()
        {
            this.buttonSave.Enabled = true;
            this.buttonDel.Enabled = false;
            this.buttonNew.Enabled = false;
        }

        void NewStatus()
        {
            _currGid = string.Empty;
            ClearControl();
            this.buttonDel.Enabled = false;
            this.buttonSave.Enabled = true;
            this.buttonNew.Enabled = false;
        }

        void Del()
        {
            if (string.IsNullOrEmpty(_currGid))
            {
                MessageBox.Show("Error: gid is null.");
                return;
            }

            var query = _context
                .tb_profit
                .Single(me => me.Gid.Equals(_currGid));
            _context.tb_profit.Remove(query);

            _context.SaveChanges();

            MessageBox.Show("删除成功");

            BindList();
            NewStatus();
        }

        void BindList()
        {
            this.listView1.Items.Clear();

            var query = _context
                .tb_profit
                .OrderByDescending(me => me.Regdate)
                .ToList();

            foreach (var item in query)
            {
                var li = new ListViewItem(item.ProfitDate);
                li.Tag = item.Gid;
                li.SubItems.Add(item.Profit.ToString());
                li.SubItems.Add(Util.FormatDateTime(item.Regdate));

                this.listView1.Items.Add(li);
            }
        }

        void BindInfo(string gid)
        {
            if (string.IsNullOrEmpty(gid))
            {
                ClearControl();
            }
            else
            {
                var query = _context
                    .tb_profit
                    .Single(me => me.Gid.Equals(gid));

                this.textBoxProfit.Text = query.Profit.ToString();
                this.comboBoxProfitDate.SelectedItem = query.ProfitDate;

                this.numericUpDownBanGongYongPin.Value = query.BanGongYongPin;
                this.numericUpDownCaiWuJiZhangFei.Value = query.CaiWuJiZhangFei;
                this.numericUpDownCangChuFeiYuFaHuoFeiYong.Value = query.CangChuFeiYuFaHuoFeiYong;
                this.numericUpDownCDianZhiTongChe.Value = query.CDianZhiTongChe;
                this.numericUpDownChanPinChengBen1.Value = query.ChanPinChengBen1;
                this.numericUpDownDaiFuFeiYong.Value = query.DaiFuFeiYong;
                this.numericUpDownDaiYunYingFeiYong.Value = query.DaiYunYingFeiYong;
                this.numericUpDownDingZhiXiangFeiYong.Value = query.DingZhiXiangFeiYong;
                this.numericUpDownEKOFaHuoShangPinChengBen.Value = query.EKOFaHuoShangPinChengBen;
                this.numericUpDownFangWuZuJin.Value = query.FangWuZuJin;
                this.numericUpDownFeiDiLaChengBen.Value = query.FeiDiLaChengBen;
                this.numericUpDownGongZi.Value = query.GongZi;
                this.numericUpDownGuDingChengBen1.Value = query.GuDingChengBen1;
                this.numericUpDownHaoCaiFei.Value = query.HaoCaiFei;
                this.numericUpDownHongWeiWuLiuChengBen.Value = query.HongWeiWuLiuChengBen;
                this.numericUpDownJinHuoChengBen.Value = query.JinHuoChengBen;
                this.numericUpDownNutFangDiuQi.Value = query.NutFangDiuQi;
                this.numericUpDownQiTaFeiYong.Value = query.QiTaFeiYong;
                this.numericUpDownQiTaZaFei.Value = query.QiTaZaFei;
                this.numericUpDownRenLiChengBen1.Value = query.RenLiChengBen1;
                this.numericUpDownSale.Value = query.Sale;
                this.numericUpDownShangNiChengBen.Value = query.ShangNiChengBen;
                this.numericUpDownShuiDianFeiYong.Value = query.ShuiDianFeiYong;
                this.numericUpDownShuiWuFeiYong.Value = query.ShuiWuFeiYong;
                this.numericUpDownSiJiFeiYong.Value = query.SiJiFeiYong;
                this.numericUpDownXinRuiChengBen.Value = query.XinRuiChengBen;
                this.numericUpDownYingXiaoChengBen1.Value = query.YingXiaoChengBen1;
                this.numericUpDownYunCangChengBen1.Value = query.YunCangChengBen1;
                this.numericUpDownZhiTongChe.Value = query.ZhiTongChe;
                this.numericUpDownZuanZhanFei.Value = query.ZuanZhanFei;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_currGid))
            {
                if (MessageBox.Show("您确认删除当前记录？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var query = _context.tb_profit.Single(me => me.Gid.Equals(_currGid));
                    _context.tb_profit.Remove(query);
                    _currGid = string.Empty;
                    _context.SaveChanges();
                    _context.Dispose();
                    _context = new DB.kkwEntities();

                    NewStatus();
                    ClearControl();
                    BindList();
                }
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            NewStatus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确认保存？ 数据保存后不能修改，只能删除。", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var currDate = this.comboBoxProfitDate.SelectedItem.ToString();
                var count = _context.tb_profit.Count(me => me.ProfitDate.Equals(currDate));
                if (count > 0)
                {
                    MessageBox.
                        Show(currDate + " 月的利润已保存.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxProfit.Text) ||
                    decimal.Parse(textBoxProfit.Text) == 0M)
                {
                    MessageBox.
                      Show(currDate + " 月的利润没有值 ，或不能为0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var newModel = new DB.tb_profit
                {
                    Regdate = new Util().GetCurrDateTime,
                    StaffGid = Config.StaffGid ?? "",
                    StaffName = Config.StaffName ?? "",
                    BanGongYongPin = this.numericUpDownBanGongYongPin.Value,
                    CaiWuJiZhangFei = this.numericUpDownCaiWuJiZhangFei.Value,
                    Sale = this.numericUpDownSale.Value,
                    CangChuFeiYuFaHuoFeiYong = this.numericUpDownCangChuFeiYuFaHuoFeiYong.Value,
                    CDianZhiTongChe = numericUpDownCDianZhiTongChe.Value,
                    ChanPinChengBen1 = numericUpDownChanPinChengBen1.Value,
                    DaiFuFeiYong = numericUpDownDaiFuFeiYong.Value,
                    DaiYunYingFeiYong = numericUpDownDaiYunYingFeiYong.Value,
                    DingZhiXiangFeiYong = numericUpDownDingZhiXiangFeiYong.Value,
                    EKOFaHuoShangPinChengBen = numericUpDownEKOFaHuoShangPinChengBen.Value,
                    FangWuZuJin = numericUpDownFangWuZuJin.Value,
                    FeiDiLaChengBen = numericUpDownFeiDiLaChengBen.Value,
                    Gid = Guid.NewGuid().ToString(),
                    GongZi = numericUpDownGongZi.Value,
                    GuDingChengBen1 = numericUpDownGuDingChengBen1.Value,
                    HaoCaiFei = numericUpDownHaoCaiFei.Value,
                    HongWeiWuLiuChengBen = numericUpDownHongWeiWuLiuChengBen.Value,
                    JinHuoChengBen = numericUpDownJinHuoChengBen.Value,
                    NutFangDiuQi = numericUpDownNutFangDiuQi.Value,
                    Profit = decimal.Parse(textBoxProfit.Text),
                    ProfitDate = this.comboBoxProfitDate.SelectedItem.ToString(),
                    QiTaFeiYong = numericUpDownQiTaFeiYong.Value,
                    QiTaZaFei = numericUpDownQiTaZaFei.Value,
                    RenLiChengBen1 = numericUpDownRenLiChengBen1.Value,
                    ShangNiChengBen = numericUpDownShangNiChengBen.Value,
                    ShuiDianFeiYong = numericUpDownShuiDianFeiYong.Value,
                    ShuiWuFeiYong = numericUpDownShuiWuFeiYong.Value,
                    SiJiFeiYong = numericUpDownSiJiFeiYong.Value,
                    XinRuiChengBen = numericUpDownXinRuiChengBen.Value,
                    YingXiaoChengBen1 = numericUpDownYingXiaoChengBen1.Value,
                    YunCangChengBen1 = numericUpDownYunCangChengBen1.Value,
                    ZhiTongChe = numericUpDownZhiTongChe.Value,
                    ZuanZhanFei = numericUpDownZuanZhanFei.Value
                };

                _context.tb_profit.Add(newModel);
                _context.SaveChanges();
                _context = new DB.kkwEntities();

                MessageBox.Show("数据已保存");
                NewStatus();
                BindList();
            }
        }

        /// <summary>
        /// 人力成本
        /// </summary>
        void RenLiChengBen1Change()
        {
            this.numericUpDownRenLiChengBen1.Value =
                this.numericUpDownGongZi.Value +
                this.numericUpDownDaiYunYingFeiYong.Value +
                this.numericUpDownSiJiFeiYong.Value;

            StatProfit();
        }

        private void numericUpDownGongZi_ValueChanged(object sender, EventArgs e)
        {
            RenLiChengBen1Change();
        }

        private void numericUpDownSiJiFeiYong_ValueChanged(object sender, EventArgs e)
        {
            RenLiChengBen1Change();
        }

        private void numericUpDownDaiYunYingFeiYong_ValueChanged(object sender, EventArgs e)
        {
            RenLiChengBen1Change();
        }

        /// <summary>
        /// 固定成本
        /// </summary>
        void GuDingChengBen1Change()
        {
            this.numericUpDownGuDingChengBen1.Value =
                this.numericUpDownBanGongYongPin.Value +
                this.numericUpDownShuiDianFeiYong.Value +
                this.numericUpDownFangWuZuJin.Value +
                this.numericUpDownShuiWuFeiYong.Value +
                this.numericUpDownQiTaZaFei.Value +
                this.numericUpDownDaiFuFeiYong.Value +
                this.numericUpDownCaiWuJiZhangFei.Value;

            StatProfit();
        }

        private void numericUpDownBanGongYongPin_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownShuiDianFeiYong_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownFangWuZuJin_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownShuiWuFeiYong_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownQiTaZaFei_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownDaiFuFeiYong_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        private void numericUpDownCaiWuJiZhangFei_ValueChanged(object sender, EventArgs e)
        {
            GuDingChengBen1Change();
        }

        /// <summary>
        /// 营销成本
        /// </summary>
        void YingXiaoChengBen1Change()
        {
            numericUpDownYingXiaoChengBen1.Value =
                numericUpDownZhiTongChe.Value +
                numericUpDownZuanZhanFei.Value +
                numericUpDownQiTaFeiYong.Value +
                numericUpDownCDianZhiTongChe.Value;

            StatProfit();
        }

        private void numericUpDownZhiTongChe_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();
        }

        private void numericUpDownZuanZhanFei_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();
        }

        private void numericUpDownQiTaFeiYong_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();
        }

        private void numericUpDownCDianZhiTongChe_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();
        }

        /// <summary>
        /// 产品成本
        /// </summary>
        void numericUpDownChanPinChengBen1Change()
        {
            numericUpDownChanPinChengBen1.Value =
                numericUpDownJinHuoChengBen.Value +
                numericUpDownShangNiChengBen.Value +
                numericUpDownFeiDiLaChengBen.Value +
                numericUpDownXinRuiChengBen.Value +
                numericUpDownHongWeiWuLiuChengBen.Value +
                numericUpDownEKOFaHuoShangPinChengBen.Value +
                numericUpDownNutFangDiuQi.Value;

            StatProfit();
        }

        private void numericUpDownJinHuoChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownShangNiChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownFeiDiLaChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownXinRuiChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownHongWeiWuLiuChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownEKOFaHuoShangPinChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownNutFangDiuQi_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        /// <summary>
        /// 云仓成本
        /// </summary>
        void numericUpDownYunCangChengBen1Change()
        {
            this.numericUpDownYunCangChengBen1.Value =
                numericUpDownCangChuFeiYuFaHuoFeiYong.Value +
                numericUpDownHaoCaiFei.Value +
                numericUpDownDingZhiXiangFeiYong.Value;

            StatProfit();
        }

        private void numericUpDownCangChuFeiYuFaHuoFeiYong_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        private void numericUpDownHaoCaiFei_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        private void numericUpDownDingZhiXiangFeiYong_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }
        /// <summary>
        /// 计算利润
        /// </summary>
        void StatProfit()
        {
            var profit = this.numericUpDownSale.Value -
                     numericUpDownRenLiChengBen1.Value -
                     numericUpDownGuDingChengBen1.Value -
                     numericUpDownYingXiaoChengBen1.Value -
                     numericUpDownChanPinChengBen1.Value -
                     numericUpDownYunCangChengBen1.Value;

            this.textBoxProfit.Text = profit.ToString();
        }

        private void numericUpDownSale_ValueChanged(object sender, EventArgs e)
        {
            StatProfit();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems != null)
            {
                if (this.listView1.SelectedItems.Count == 1)
                {
                    _currGid = this.listView1.SelectedItems[0].Tag.ToString();

                    BindInfo(_currGid);

                    ModifyStatus();

                }
            }
        }
    }
}
