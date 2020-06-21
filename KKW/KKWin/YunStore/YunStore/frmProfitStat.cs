using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmProfitStat : Form
    {
        DB.qstoreEntities _context = new DB.qstoreEntities();
        List<int> _yearList = new List<int>();

        public frmProfitStat()
        {
            InitializeComponent();
            this.Shown += FrmProfitStat_Shown;
        }

        private void FrmProfitStat_Shown(object sender, EventArgs e)
        {
            BindYear();
            BindList();
        }

        void BindList()
        {
            this.listView1.Items.Clear();
            if (this.comboBox1.SelectedItem != null)
            {
                var year = this.comboBox1.SelectedItem.ToString().Substring(0, 4);

                var query = _context
                    .tb_profit
                    .Where(me =>
                        me.ProfitDate.StartsWith(year))
                        .OrderBy(me => me.ProfitDate)
                        .ToList();
                if (query.Count > 0)
                {
                    query.Add(new DB.tb_profit
                    {
                        ProfitDate = "合计：",
                        BanGongYongPin = query.Sum(me => me.BanGongYongPin),
                        CaiWuJiZhangFei = query.Sum(me => me.CaiWuJiZhangFei),
                        CangChuFeiYuFaHuoFeiYong = query.Sum(me => me.CangChuFeiYuFaHuoFeiYong),
                        CDianZhiTongChe = query.Sum(me => me.CDianZhiTongChe),
                        ChanPinChengBen1 = query.Sum(me => me.ChanPinChengBen1),
                        ChaoJiTuiJian = query.Sum(me => me.ChaoJiTuiJian),
                        DaiFuFeiYong = query.Sum(me => me.DaiFuFeiYong),
                        DaiYunYingFeiYong = query.Sum(me => me.DaiYunYingFeiYong),
                        DingZhiXiangFeiYong = query.Sum(me => me.DingZhiXiangFeiYong),
                        EKOFaHuoShangPinChengBen = query.Sum(me => me.EKOFaHuoShangPinChengBen),
                        FangWuZuJin = query.Sum(me => me.FangWuZuJin),
                        FeiDiLaChengBen = query.Sum(me => me.FeiDiLaChengBen),
                        Gid = Guid.Empty,
                        GongZi = query.Sum(me => me.GongZi),
                        GuDingChengBen1 = query.Sum(me => me.GuDingChengBen1),
                        HaoCaiFei = query.Sum(me => me.HaoCaiFei),
                        HongWeiWuLiuChengBen = query.Sum(me => me.HongWeiWuLiuChengBen),
                        JinHuoChengBen = query.Sum(me => me.JinHuoChengBen),
                        NutFangDiuQi = query.Sum(me => me.NutFangDiuQi),
                        Profit = query.Sum(me => me.Profit),
                        QiTaFeiYong = query.Sum(me => me.QiTaFeiYong),
                        QiTaZaFei = query.Sum(me => me.QiTaZaFei),
                        RenLiChengBen1 = query.Sum(me => me.RenLiChengBen1),
                        Sale_TianMao = query.Sum(me => me.Sale_TianMao),
                        Sale_TianMao_ShuaDian = query.Sum(me => me.Sale_TianMao_ShuaDian),
                        Sale_Taobao = query.Sum(me => me.Sale_Taobao),
                        Sale_TaoBao_ShuaDian = query.Sum(me => me.Sale_TaoBao_ShuaDian),
                        ShangNiChengBen = query.Sum(me => me.ShangNiChengBen),
                        ShuiDianFeiYong = query.Sum(me => me.ShuiDianFeiYong),
                        ShuiWuFeiYong = query.Sum(me => me.ShuiWuFeiYong),
                        SiJiFeiYong = query.Sum(me => me.SiJiFeiYong),
                        XinRuiChengBen = query.Sum(me => me.XinRuiChengBen),
                        YingXiaoChengBen1 = query.Sum(me => me.YingXiaoChengBen1),
                        YunCangChengBen1 = query.Sum(me => me.YunCangChengBen1),
                        ZhiTongChe = query.Sum(me => me.ZhiTongChe),
                        ZuanZhanFei = query.Sum(me => me.ZuanZhanFei),
                        MaoLi = query.Sum(me => me.MaoLi),
                        SheBaoGongJiJin = query.Sum(me => me.SheBaoGongJiJin)
                    });
                }
                for (var i = 0; i < query.Count; i++)
                {
                    var item = query[i];
                    var li = new ListViewItem(item.ProfitDate);
                    li.Tag = item.Gid.ToString();
                    li.SubItems.Add(Util.FormatPrice(item.Profit));
                    li.SubItems.Add(Util.FormatPrice(item.MaoLi));
                    li.SubItems.Add(Util.FormatPrice(item.Sale_TianMao));
                    li.SubItems.Add(Util.FormatPrice(item.Sale_TianMao_ShuaDian));
                    li.SubItems.Add(Util.FormatPrice(item.Sale_Taobao));
                    li.SubItems.Add(Util.FormatPrice(item.Sale_TaoBao_ShuaDian));
                    li.SubItems.Add(Util.FormatPrice(item.RenLiChengBen1));
                    li.SubItems.Add(Util.FormatPrice(item.GuDingChengBen1));
                    li.SubItems.Add(Util.FormatPrice(item.YingXiaoChengBen1));
                    li.SubItems.Add(Util.FormatPrice(item.ChanPinChengBen1));
                    li.SubItems.Add(Util.FormatPrice(item.YunCangChengBen1));
                    li.SubItems.Add(Util.FormatPrice(item.GongZi));
                    li.SubItems.Add(Util.FormatPrice(item.SiJiFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.DaiYunYingFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.BanGongYongPin));
                    li.SubItems.Add(Util.FormatPrice(item.ShuiDianFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.FangWuZuJin));
                    li.SubItems.Add(Util.FormatPrice(item.ShuiWuFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.QiTaZaFei));
                    li.SubItems.Add(Util.FormatPrice(item.DaiFuFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.CaiWuJiZhangFei));
                    li.SubItems.Add(Util.FormatPrice(item.ZhiTongChe));
                    li.SubItems.Add(Util.FormatPrice(item.ZuanZhanFei));
                    li.SubItems.Add(Util.FormatPrice(item.QiTaZaFei));
                    li.SubItems.Add(Util.FormatPrice(item.CDianZhiTongChe));
                    li.SubItems.Add(Util.FormatPrice(item.ChaoJiTuiJian));
                    li.SubItems.Add(Util.FormatPrice(item.JinHuoChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.ShangNiChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.FeiDiLaChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.XinRuiChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.HongWeiWuLiuChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.EKOFaHuoShangPinChengBen));
                    li.SubItems.Add(Util.FormatPrice(item.NutFangDiuQi));
                    li.SubItems.Add(Util.FormatPrice(item.CangChuFeiYuFaHuoFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.HaoCaiFei));
                    li.SubItems.Add(Util.FormatPrice(item.DingZhiXiangFeiYong));
                    li.SubItems.Add(Util.FormatPrice(item.SheBaoGongJiJin));
                    if (item.ProfitDate.IndexOf("合计") > -1)
                    {
                        li.ForeColor = Color.Green;
                    }
                    this.listView1.Items.Add(li);
                }
            }

        }

        void BindYear()
        {
            var query = _context
                .tb_profit
                .Select(me => me.ProfitDate)
                .Distinct()
                .ToList();
            this.comboBox1.Items.Clear();
            foreach (var item in query)
            {
                var year = int.Parse(item.Substring(0, 4));
                if (!_yearList.Contains(year))
                {
                    this._yearList.Add(year);
                    this.comboBox1.Items.Add(year.ToString() + "年");
                }

                if (year == DateTime.Now.Year)
                {
                    this.comboBox1.SelectedItem = (year.ToString() + "年");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }
    }
}
