using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using YunStore.DB;
using YunStore.Toolkits;

namespace YunStore
{
    public partial class frmProfitDetail : Form
    {
        qstoreEntities _context = new DB.qstoreEntities();
        string _currMonth = string.Empty;
        List<tb_profit> _dbList = new List<tb_profit>();
        List<tb_profit> _leftList = new List<tb_profit>();

        public frmProfitDetail()
        {
            InitializeComponent();
            this.Shown += FrmProfitDetail_Shown;
        }

        private void FrmProfitDetail_Shown(object sender, EventArgs e)
        {
            _dbList = _context
                .tb_profit
                .OrderByDescending(me => me.ProfitDate)
                .ToList();

            var currYear = string.Empty;
            for (var i = 0; i < _dbList.Count; i++)
            {
                var item = _dbList[i];

                if (currYear == string.Empty ||
                    currYear != item.ProfitDate.Substring(0, 4))
                {
                    // 添加年份。
                    currYear = item.ProfitDate.Substring(0, 4);
                    _leftList.Add(new tb_profit
                    {
                        ProfitDate = currYear,
                        Profit = _dbList.Where(me => me.ProfitDate.StartsWith(currYear)).Sum(me => (decimal?)me.Profit).GetValueOrDefault()
                    });
                }

                _leftList.Add(item);
            }
            BindList();
        }

        void BindList()
        {
            this.listView1.Items.Clear();

            foreach (var item in _leftList)
            {
                var li = new ListViewItem(item.ProfitDate);
                li.Tag = item.ProfitDate;
                li.SubItems.Add(item.Profit.ToString());
                li.SubItems.Add(Util.FormatDateTime(item.Regdate));

                this.listView1.Items.Add(li);
            }
        }

        void BindDetail(string currDate)
        {
            this.listView2.Items.Clear();
            if (currDate.Length == 4) // 年
            {
                var items = _dbList.Where(me => me.ProfitDate.StartsWith(currDate));
                var li1 = new ListViewItem("年份");
                li1.SubItems.Add(currDate);
                li1.SubItems.Add("纯利润");
                li1.SubItems.Add(Util.FormatPrice(items.Sum(me=>me.Profit)));
                this.listView2.Items.Add(li1);

                var li2 = new ListViewItem("营业额");
                li2.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale)));
                li2.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2);

                var li3 = new ListViewItem("人力成本");
                li3.SubItems.Add(Util.FormatPrice(items.Sum(me => me.RenLiChengBen1)));
                li3.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li3);

                var li4 = new ListViewItem("工资");
                li4.SubItems.Add(Util.FormatPrice(items.Sum(me => me.GongZi)));
                li4.SubItems.Add("司机费用");
                li4.SubItems.Add(Util.FormatPrice(items.Sum(me => me.SiJiFeiYong)));
                listView2.Items.Add(li4);

                var li5 = new ListViewItem("代运营费用");
                li5.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DaiYunYingFeiYong)));
                listView2.Items.Add(li5);

                var li6 = new ListViewItem("固定成本");
                li6.SubItems.Add(Util.FormatPrice(items.Sum(me => me.GuDingChengBen1)));
                li6.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li6);

                var li7 = new ListViewItem("办公用品");
                li7.SubItems.Add(Util.FormatPrice(items.Sum(me => me.BanGongYongPin)));
                li7.SubItems.Add("水电费用");
                li7.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ShuiDianFeiYong)));
                listView2.Items.Add(li7);

                var li8 = new ListViewItem("房屋租金");
                li8.SubItems.Add(Util.FormatPrice(items.Sum(me => me.FangWuZuJin)));
                li8.SubItems.Add("税务费用");
                li8.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ShuiWuFeiYong)));
                listView2.Items.Add(li8);

                var li9 = new ListViewItem("其他杂费");
                li9.SubItems.Add(Util.FormatPrice(items.Sum(me => me.QiTaZaFei)));
                li9.SubItems.Add("代付费用");
                li9.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DaiFuFeiYong)));
                listView2.Items.Add(li9);

                var li10 = new ListViewItem("财务记账费");
                li10.SubItems.Add(Util.FormatPrice(items.Sum(me => me.CaiWuJiZhangFei)));
                listView2.Items.Add(li10);

                var li11 = new ListViewItem("营销成本");
                li11.SubItems.Add(Util.FormatPrice(items.Sum(me => me.YingXiaoChengBen1)));
                li11.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li11);

                var li12 = new ListViewItem("直通车");
                li12.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ZhiTongChe)));
                li12.SubItems.Add("钻展费");
                li12.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ZuanZhanFei)));
                listView2.Items.Add(li12);

                var li13 = new ListViewItem("其他费用");
                li13.SubItems.Add(Util.FormatPrice(items.Sum(me => me.QiTaFeiYong)));
                li13.SubItems.Add("c店直通车");
                li13.SubItems.Add(Util.FormatPrice(items.Sum(me => me.CDianZhiTongChe)));
                listView2.Items.Add(li13);

                var li13_2 = new ListViewItem("超级推荐");
                li13_2.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ChaoJiTuiJian)));
                listView2.Items.Add(li13_2);

                var li14 = new ListViewItem("产品总成本");
                li14.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ChanPinChengBen1)));
                li14.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li14);

                var li15 = new ListViewItem("进货成本v");
                li15.SubItems.Add(Util.FormatPrice(items.Sum(me => me.JinHuoChengBen)));
                li15.SubItems.Add("尚尼成本");
                li15.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ShangNiChengBen)));
                listView2.Items.Add(li15);

                var li16 = new ListViewItem("菲迪拉成本");
                li16.SubItems.Add(Util.FormatPrice(items.Sum(me => me.FeiDiLaChengBen)));
                li16.SubItems.Add("新锐成本");
                li16.SubItems.Add(Util.FormatPrice(items.Sum(me => me.XinRuiChengBen)));
                listView2.Items.Add(li16);

                var li17 = new ListViewItem("宏伟物流成本");
                li17.SubItems.Add(Util.FormatPrice(items.Sum(me => me.HongWeiWuLiuChengBen)));
                li17.SubItems.Add("eko发货商品成本");
                li17.SubItems.Add(Util.FormatPrice(items.Sum(me => me.EKOFaHuoShangPinChengBen)));
                listView2.Items.Add(li17);

                var li18 = new ListViewItem("nut防丢器");
                li18.SubItems.Add(Util.FormatPrice(items.Sum(me => me.NutFangDiuQi)));
                listView2.Items.Add(li18);

                var li19 = new ListViewItem("云仓（总费用）");
                li19.SubItems.Add(Util.FormatPrice(items.Sum(me => me.YunCangChengBen1)));
                li19.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li19);

                var li20 = new ListViewItem("仓储费与发货费用");
                li20.SubItems.Add(Util.FormatPrice(items.Sum(me => me.CangChuFeiYuFaHuoFeiYong)));
                li20.SubItems.Add("耗材费");
                li20.SubItems.Add(Util.FormatPrice(items.Sum(me => me.HaoCaiFei)));
                listView2.Items.Add(li20);

                var li21 = new ListViewItem("定纸箱费用（第一月）");
                li21.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DingZhiXiangFeiYong)));
                listView2.Items.Add(li21);
            }
            else// 月
            {
                var item = _dbList.FirstOrDefault(me => me.ProfitDate.Equals(currDate));
                var li1 = new ListViewItem("月份");
                li1.SubItems.Add(item.ProfitDate);
                li1.SubItems.Add("纯利润");
                li1.SubItems.Add(Util.FormatPrice(item.Profit));                
                this.listView2.Items.Add(li1);             

                var li2 = new ListViewItem("营业额");
                li2.SubItems.Add(Util.FormatPrice(item.Sale));
                li2.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2);

                var li3 = new ListViewItem("人力成本");
                li3.SubItems.Add(Util.FormatPrice(item.RenLiChengBen1));
                li3.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li3);

                var li4 = new ListViewItem("工资");
                li4.SubItems.Add(Util.FormatPrice(item.GongZi));
                li4.SubItems.Add("司机费用");
                li4.SubItems.Add(Util.FormatPrice(item.SiJiFeiYong));
                listView2.Items.Add(li4);

                var li5 = new ListViewItem("代运营费用");
                li5.SubItems.Add(Util.FormatPrice(item.DaiYunYingFeiYong));
                listView2.Items.Add(li5);

                var li6 = new ListViewItem("固定成本");
                li6.SubItems.Add(Util.FormatPrice(item.GuDingChengBen1));
                li6.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li6);

                var li7 = new ListViewItem("办公用品");
                li7.SubItems.Add(Util.FormatPrice(item.BanGongYongPin));
                li7.SubItems.Add("水电费用");
                li7.SubItems.Add(Util.FormatPrice(item.ShuiDianFeiYong));
                listView2.Items.Add(li7);

                var li8 = new ListViewItem("房屋租金");
                li8.SubItems.Add(Util.FormatPrice(item.FangWuZuJin));
                li8.SubItems.Add("税务费用");
                li8.SubItems.Add(Util.FormatPrice(item.ShuiWuFeiYong));
                listView2.Items.Add(li8);

                var li9 = new ListViewItem("其他杂费");
                li9.SubItems.Add(Util.FormatPrice(item.QiTaZaFei));
                li9.SubItems.Add("代付费用");
                li9.SubItems.Add(Util.FormatPrice(item.DaiFuFeiYong));
                listView2.Items.Add(li9);

                var li10 = new ListViewItem("财务记账费");
                li10.SubItems.Add(Util.FormatPrice(item.CaiWuJiZhangFei));
                listView2.Items.Add(li10);

                var li11 = new ListViewItem("营销成本");
                li11.SubItems.Add(Util.FormatPrice(item.YingXiaoChengBen1));
                li11.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li11);
                
                var li12 = new ListViewItem("直通车");
                li12.SubItems.Add(Util.FormatPrice(item.ZhiTongChe));
                li12.SubItems.Add("钻展费");
                li12.SubItems.Add(Util.FormatPrice(item.ZuanZhanFei));
                listView2.Items.Add(li12);

                var li13 = new ListViewItem("其他费用");
                li13.SubItems.Add(Util.FormatPrice(item.QiTaFeiYong));
                li13.SubItems.Add("c店直通车");
                li13.SubItems.Add(Util.FormatPrice(item.CDianZhiTongChe));
                listView2.Items.Add(li13);

                var li13_2 = new ListViewItem("超级推荐");
                li13_2.SubItems.Add(Util.FormatPrice(item.ChaoJiTuiJian));
                listView2.Items.Add(li13_2);

                var li14 = new ListViewItem("产品总成本");
                li14.SubItems.Add(Util.FormatPrice(item.ChanPinChengBen1));
                li14.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li14);

                var li15 = new ListViewItem("进货成本v");
                li15.SubItems.Add(Util.FormatPrice(item.JinHuoChengBen));
                li15.SubItems.Add("尚尼成本");
                li15.SubItems.Add(Util.FormatPrice(item.ShangNiChengBen));
                listView2.Items.Add(li15);

                var li16 = new ListViewItem("菲迪拉成本");
                li16.SubItems.Add(Util.FormatPrice(item.FeiDiLaChengBen));
                li16.SubItems.Add("新锐成本");
                li16.SubItems.Add(Util.FormatPrice(item.XinRuiChengBen));
                listView2.Items.Add(li16);

                var li17 = new ListViewItem("宏伟物流成本");
                li17.SubItems.Add(Util.FormatPrice(item.HongWeiWuLiuChengBen));
                li17.SubItems.Add("eko发货商品成本");
                li17.SubItems.Add(Util.FormatPrice(item.EKOFaHuoShangPinChengBen));
                listView2.Items.Add(li17);

                var li18 = new ListViewItem("nut防丢器");
                li18.SubItems.Add(Util.FormatPrice(item.NutFangDiuQi));
                listView2.Items.Add(li18);

                var li19 = new ListViewItem("云仓（总费用）");
                li19.SubItems.Add(Util.FormatPrice(item.YunCangChengBen1));
                li19.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li19);

                var li20 = new ListViewItem("仓储费与发货费用");
                li20.SubItems.Add(Util.FormatPrice(item.CangChuFeiYuFaHuoFeiYong));
                li20.SubItems.Add("耗材费");
                li20.SubItems.Add(Util.FormatPrice(item.HaoCaiFei));
                listView2.Items.Add(li20);

                var li21 = new ListViewItem("定纸箱费用（第一月）");
                li21.SubItems.Add(Util.FormatPrice(item.DingZhiXiangFeiYong));
                listView2.Items.Add(li21);
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.listView1.SelectedItems != null)
            {
                if (this.listView1.SelectedItems.Count == 1)
                {
                    _currMonth = this.listView1.SelectedItems[0].Tag.ToString();

                    BindDetail(_currMonth);
                }
            }
        }
    }
}
