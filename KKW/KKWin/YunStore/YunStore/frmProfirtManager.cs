using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using YunStore.BLL;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmProfirtManager : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        Guid _currGid = Guid.Empty;
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
            this.buttonCreate.Text = "修改";
            this.buttonCreate.Enabled = true;
            this.buttonDel.Enabled = true;
            this.buttonNew.Enabled = true;
        }

        void AddStatus()
        {
            this.buttonCreate.Enabled = true;
            this.buttonDel.Enabled = false;
            this.buttonNew.Enabled = false;
        }

        void NewStatus()
        {
            _currGid = Guid.Empty;
            this.buttonCreate.Text = "添加";
            ClearControl();
            this.buttonDel.Enabled = false;
            this.buttonCreate.Enabled = true;
            this.buttonNew.Enabled = false;
        }

        void Del()
        {
            if (_currGid == Guid.Empty)
            {
                MessageBox.Show("Error: gid is null.");
                return;
            }

            var query = _context
                .tb_profit
                .Single(me => me.Gid.Equals(_currGid));
            _context.tb_profit.Remove(query);

            _context.SaveChanges();
            _currGid = Guid.Empty;
            MessageBox.Show("删除成功");

            BindList();
            NewStatus();
        }

        void BindList()
        {
            this.listView1.Items.Clear();

            var query = _context
                .tb_profit
                .OrderByDescending(me => me.ProfitDate)
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

        void BindInfo(Guid gid)
        {
            if (Guid.Empty == gid)
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
                this.numericUpDownShuaDian.Value = query.ShuaDianFeiYou;
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
                this.numericUpDownSale_TianMao.Value = query.Sale_TianMao;
                this.numericUpDownSale_TianMao_ShuaDian.Value = query.Sale_TianMao_ShuaDian;
                this.numericUpDownSale_TianMao_Alipay.Value = query.Sale_TianMao_Alipay;
                this.numericUpDownSale_Taobao_Alipay.Value = query.Sale_TaoBao_Alipay;

                this.numericUpDownSale_TaoBao.Value = query.Sale_Taobao;
                this.numericUpDownSale_TaoBao_ShuaDian.Value = query.Sale_TaoBao_ShuaDian;
                this.numericUpDownShangNiChengBen.Value = query.ShangNiChengBen;
                this.numericUpDownShuiDianFeiYong.Value = query.ShuiDianFeiYong;
                this.numericUpDownShuiWuFeiYong.Value = query.ShuiWuFeiYong;
                this.numericUpDownSiJiFeiYong.Value = query.SiJiFeiYong;
                this.numericUpDownXinRuiChengBen.Value = query.XinRuiChengBen;
                this.numericUpDownYingXiaoChengBen1.Value = query.YingXiaoChengBen1;
                this.numericUpDownYunCangChengBen1.Value = query.YunCangChengBen1;
                this.numericUpDownZhiTongChe.Value = query.ZhiTongChe;
                this.numericUpDownZuanZhanFei.Value = query.ZuanZhanFei;
                this.numericUpDownChaoJiTuiJian.Value = query.ChaoJiTuiJian;
                this.numericUpDownSheBaoGongJiJin.Value = query.SheBaoGongJiJin;
                this.numericUpDownMaoLi.Value = query.MaoLi;

                this.numericUpDownYouYiDingGouFei.Value = query.YouYiDingGouFei;
                this.numericUpDownAiZhiNengTouFang.Value = query.AiZhiNengTouFang;
                this.numericUpDownCangKuFaHuoShangPinZongChengBen.Value = query.CangKuFaHuoShangPinZongChengBen;
                this.numericUpDownHuJiaoFeiYong.Value = query.HuJiaoFeiYong;
                this.numericUpDownMiYaKeChengBen.Value = query.MiYaKeChengBen;
                this.numericUpDownCaiZhaoChengBen.Value = query.CaiZhaoChengBen;
                this.numericUpDownKuangQuanShuiChengBen.Value = query.KuangQuanShuiChengBen;
                this.numericUpDownYiSuiBiaoQian.Value = query.YiSuiBiaoQian;
                this.numericUpDownJiaoDai.Value = query.JiaoDai;
                this.numericUpDownMiaoCangOther.Value = query.MiaoCangOther;
                this.numericUpDownQiPaoZhu.Value = query.QiPaoZhu;
                this.textBoxRemark.Text = query.Remark;

                this.numericUpDownBanGongZaFei1.Value = query.BanGongZaFei1;
            }
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (Guid.Empty != _currGid)
            {
                if (MessageBox.Show("您确认删除当前记录？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var query = _context.tb_profit.Single(me => me.Gid.Equals(_currGid));
                    _context.tb_profit.Remove(query);
                    _currGid = Guid.Empty;
                    _context.SaveChanges();
                    _context.Dispose();
                    _context = new DB.qstoreEntities();
                    MessageBox.Show("数据已删除");
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
            if (MessageBox.Show("您确认保存？", "询问", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DB.tb_profit newModel;
                if (Guid.Empty != _currGid)
                {
                    newModel = _context.tb_profit.Single(me => me.Gid.Equals(_currGid));
                }
                else
                {
                    var currDate = this.comboBoxProfitDate.SelectedItem.ToString();
                    var count = _context.tb_profit.Count(me => me.ProfitDate.Equals(currDate));
                    if (count > 0)
                    {
                        MessageBox.
                            Show(currDate + " 月的利润已保存.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    newModel = new DB.tb_profit();
                    newModel.Gid = Guid.NewGuid();
                    newModel.Regdate = new Util().GetCurrDateTime;
                    newModel.StaffGid = Config.StaffGid;
                    newModel.StaffName = Config.StaffName ?? "";
                }

                newModel.BanGongYongPin = this.numericUpDownBanGongYongPin.Value;
                newModel.CaiWuJiZhangFei = this.numericUpDownCaiWuJiZhangFei.Value;
                newModel.Sale_TianMao = this.numericUpDownSale_TianMao.Value;
                newModel.Sale_TianMao_ShuaDian = this.numericUpDownSale_TianMao_ShuaDian.Value;
                newModel.Sale_Taobao = this.numericUpDownSale_TaoBao.Value;
                newModel.Sale_TaoBao_ShuaDian = this.numericUpDownSale_TaoBao_ShuaDian.Value;
                newModel.Sale_TaoBao_Alipay = this.numericUpDownSale_Taobao_Alipay.Value;
                newModel.Sale_TianMao_Alipay = this.numericUpDownSale_TianMao_Alipay.Value;

                newModel.CangChuFeiYuFaHuoFeiYong = this.numericUpDownCangChuFeiYuFaHuoFeiYong.Value;
                newModel.CDianZhiTongChe = numericUpDownCDianZhiTongChe.Value;
                newModel.ChanPinChengBen1 = numericUpDownChanPinChengBen1.Value;
                newModel.DaiFuFeiYong = numericUpDownDaiFuFeiYong.Value;
                newModel.DaiYunYingFeiYong = numericUpDownDaiYunYingFeiYong.Value;
                newModel.DingZhiXiangFeiYong = numericUpDownDingZhiXiangFeiYong.Value;
                newModel.EKOFaHuoShangPinChengBen = numericUpDownEKOFaHuoShangPinChengBen.Value;
                newModel.FangWuZuJin = numericUpDownFangWuZuJin.Value;
                newModel.ShuaDianFeiYou = numericUpDownShuaDian.Value;
                newModel.FeiDiLaChengBen = numericUpDownFeiDiLaChengBen.Value;

                newModel.GongZi = numericUpDownGongZi.Value;
                newModel.GuDingChengBen1 = numericUpDownGuDingChengBen1.Value;
                newModel.HaoCaiFei = numericUpDownHaoCaiFei.Value;
                newModel.HongWeiWuLiuChengBen = numericUpDownHongWeiWuLiuChengBen.Value;
                newModel.JinHuoChengBen = numericUpDownJinHuoChengBen.Value;
                newModel.NutFangDiuQi = numericUpDownNutFangDiuQi.Value;
                newModel.Profit = decimal.Parse(textBoxProfit.Text);
                newModel.ProfitDate = this.comboBoxProfitDate.SelectedItem.ToString();
                newModel.QiTaFeiYong = numericUpDownQiTaFeiYong.Value;
                newModel.QiTaZaFei = numericUpDownQiTaZaFei.Value;
                newModel.RenLiChengBen1 = numericUpDownRenLiChengBen1.Value;
                newModel.ShangNiChengBen = numericUpDownShangNiChengBen.Value;
                newModel.ShuiDianFeiYong = numericUpDownShuiDianFeiYong.Value;
                newModel.ShuiWuFeiYong = numericUpDownShuiWuFeiYong.Value;
                newModel.SiJiFeiYong = numericUpDownSiJiFeiYong.Value;
                newModel.XinRuiChengBen = numericUpDownXinRuiChengBen.Value;
                newModel.YingXiaoChengBen1 = numericUpDownYingXiaoChengBen1.Value;
                newModel.YunCangChengBen1 = numericUpDownYunCangChengBen1.Value;
                newModel.ZhiTongChe = numericUpDownZhiTongChe.Value;
                newModel.ZuanZhanFei = numericUpDownZuanZhanFei.Value;
                newModel.ChaoJiTuiJian = numericUpDownChaoJiTuiJian.Value;

                // 新增（2021.10.23)
                newModel.MiYaKeChengBen = this.numericUpDownMiYaKeChengBen.Value;
                newModel.CaiZhaoChengBen = this.numericUpDownCaiZhaoChengBen.Value;
                newModel.KuangQuanShuiChengBen = this.numericUpDownKuangQuanShuiChengBen.Value;
                newModel.YiSuiBiaoQian = this.numericUpDownYiSuiBiaoQian.Value;
                newModel.JiaoDai = this.numericUpDownJiaoDai.Value;
                newModel.MiaoCangOther = this.numericUpDownMiaoCangOther.Value;
                newModel.QiPaoZhu = this.numericUpDownQiPaoZhu.Value;
                newModel.BanGongZaFei1 = this.numericUpDownBanGongZaFei1.Value;

                newModel.SheBaoGongJiJin = numericUpDownSheBaoGongJiJin.Value;
                newModel.Remark = this.textBoxRemark.Text.Trim();

                //新增(2021.10.15)
                newModel.YouYiDingGouFei = numericUpDownYouYiDingGouFei.Value;
                newModel.AiZhiNengTouFang = numericUpDownAiZhiNengTouFang.Value;
                newModel.CangKuFaHuoShangPinZongChengBen = numericUpDownCangKuFaHuoShangPinZongChengBen.Value;
                newModel.HuJiaoFeiYong = numericUpDownHuJiaoFeiYong.Value;

                // 营业额—成本=毛利，这个公式你创建下，
                newModel.MaoLi = (newModel.Sale_TianMao + newModel.Sale_Taobao) - newModel.ChanPinChengBen1;

                numericUpDownMaoLi.Value = newModel.MaoLi;

                if (Guid.Empty == _currGid)
                    _context.tb_profit.Add(newModel);

                _context.SaveChanges();
                _context = new DB.qstoreEntities();

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
                this.numericUpDownSiJiFeiYong.Value +
                this.numericUpDownSheBaoGongJiJin.Value;

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
                numericUpDownCDianZhiTongChe.Value +
                numericUpDownShuaDian.Value +
                numericUpDownChaoJiTuiJian.Value +
                numericUpDownAiZhiNengTouFang.Value;

            StatProfit();
        }
        private void numericUpDownShuaDian_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();

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

        private void numericUpDownChaoJiTuiJian_ValueChanged(object sender, EventArgs e)
        {
            YingXiaoChengBen1Change();
        }
        private void numericUpDownAiZhiNengTouFang_ValueChanged(object sender, EventArgs e)
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
                numericUpDownNutFangDiuQi.Value +
                numericUpDownCangKuFaHuoShangPinZongChengBen.Value +
                numericUpDownMiYaKeChengBen.Value +
                numericUpDownCaiZhaoChengBen.Value +
                numericUpDownKuangQuanShuiChengBen.Value
                ;

            StatProfit();
        }

        private void numericUpDownMiYaKeChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownCaiZhaoChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }

        private void numericUpDownKuangQuanShuiChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
        }
        private void numericUpDownCangKuFaHuoShangPinZongChengBen_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownChanPinChengBen1Change();
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
        /// 秒仓成本
        /// </summary>
        void numericUpDownYunCangChengBen1Change()
        {
            this.numericUpDownYunCangChengBen1.Value =
                numericUpDownCangChuFeiYuFaHuoFeiYong.Value +
                numericUpDownHaoCaiFei.Value +
                numericUpDownDingZhiXiangFeiYong.Value +
                numericUpDownHuJiaoFeiYong.Value +
                numericUpDownYiSuiBiaoQian.Value +
                numericUpDownQiPaoZhu.Value +
                numericUpDownJiaoDai.Value +
                numericUpDownMiaoCangOther.Value +
                numericUpDownYouYiDingGouFei.Value
                ;

            StatProfit();
        }
        private void numericUpDownYiSuiBiaoQian_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        private void numericUpDownQiPaoZhu_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        private void numericUpDownJiaoDai_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        private void numericUpDownMiaoCangOther_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }
        private void numericUpDownYouYiDingGouFei_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
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

            this.numericUpDownSale_TaoBao.Value = this.numericUpDownSale_Taobao_Alipay.Value - this.numericUpDownSale_TaoBao_ShuaDian.Value;
            this.numericUpDownSale_TianMao.Value = this.numericUpDownSale_TianMao_Alipay.Value - this.numericUpDownSale_TianMao_ShuaDian.Value;

            var profit = (this.numericUpDownSale_TianMao.Value + this.numericUpDownSale_TaoBao.Value) -
                     numericUpDownRenLiChengBen1.Value -
                     numericUpDownGuDingChengBen1.Value -
                     numericUpDownYingXiaoChengBen1.Value -
                     numericUpDownChanPinChengBen1.Value -
                     numericUpDownYunCangChengBen1.Value -
                     numericUpDownBanGongZaFei1.Value;

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
                    _currGid = Guid.Parse(this.listView1.SelectedItems[0].Tag.ToString());

                    BindInfo(_currGid);

                    ModifyStatus();

                }
            }
        }

        private void numericUpDownSheBaoGongJiJin_ValueChanged(object sender, EventArgs e)
        {
            RenLiChengBen1Change();
        }

        private void numericUpDownHuJiaoFeiYong_ValueChanged(object sender, EventArgs e)
        {
            numericUpDownYunCangChengBen1Change();
        }

        /// <summary>
        /// 办公杂费
        /// </summary>
        void BanGongZaFei1Change()
        {
            this.numericUpDownBanGongZaFei1.Value =
               numericUpDownDaiFuFeiYong.Value
               ;

            StatProfit();
        }

        private void numericUpDownDaiFuFeiYong_ValueChanged(object sender, EventArgs e)
        {
            BanGongZaFei1Change();
        }
    }
}
