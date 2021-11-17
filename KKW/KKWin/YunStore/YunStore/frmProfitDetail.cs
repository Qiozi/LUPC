﻿using System;
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
                var remark = string.Empty;
                
                this.labelRemark.Text = string.Format("备注（{0}）", currDate);

                foreach (var item in items)
                {
                    remark += "###################################### 日期：(" + item.ProfitDate + ") ###############################################\r\n\r\n" + item.Remark + "\r\n\r\n";
                }
                this.textBoxRemark.Text = remark;

                var li1 = new ListViewItem("年份");
                li1.SubItems.Add(currDate);
                li1.SubItems.Add("纯利润");
                li1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Profit)));
                this.listView2.Items.Add(li1);

                var li1_1 = new ListViewItem("毛 利");
                li1_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.MaoLi)));
                this.listView2.Items.Add(li1_1);


                var li2 = new ListViewItem("营业额-天猫");
                li2.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_TianMao)));
                li2.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2);

                var li2_12 = new ListViewItem("支付宝到帐-天猫");
                li2_12.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_TianMao_Alipay)));
                li2_12.BackColor = Color.WhiteSmoke;
                li2_12.SubItems.Add("刷单-天猫");
                li2_12.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_TianMao_ShuaDian)));
                listView2.Items.Add(li2_12);



                var li2_1 = new ListViewItem("营业额-淘宝");
                li2_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_Taobao)));
                li2_1.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2_1);

                var li2_1_1 = new ListViewItem("支付宝到帐-淘宝");
                li2_1_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_TaoBao_Alipay)));
                li2_1_1.BackColor = Color.WhiteSmoke;
                li2_1_1.SubItems.Add("刷单-淘宝");
                li2_1_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.Sale_TaoBao_ShuaDian)));
                listView2.Items.Add(li2_1_1);

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
                li5.SubItems.Add("社保，公积金");
                li5.SubItems.Add(Util.FormatPrice(items.Sum(me => me.SheBaoGongJiJin)));
                listView2.Items.Add(li5);

                var li5_1 = new ListViewItem("办公杂费");
                li5_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.BanGongZaFei1)));
                li5_1.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li5_1);

                var li5_2 = new ListViewItem("个人代付费用");
                li5_2.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DaiFuFeiYong)));
                listView2.Items.Add(li5_2);

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
                //li9.SubItems.Add("个人代付费用");
                //li9.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DaiFuFeiYong)));
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
                li13_2.SubItems.Add("刷单佣金费");
                li13_2.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ShuaDianFeiYou)));
                listView2.Items.Add(li13_2);

                var li13_3 = new ListViewItem("AI智能投放");
                li13_3.SubItems.Add(Util.FormatPrice(items.Sum(me => me.AiZhiNengTouFang)));
                listView2.Items.Add(li13_3);

                var li14 = new ListViewItem("产品总成本");
                li14.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ChanPinChengBen1)));
                li14.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li14);

                var li15 = new ListViewItem("进货成本");
                li15.SubItems.Add(Util.FormatPrice(items.Sum(me => me.JinHuoChengBen)));
                li15.SubItems.Add("尚尼成本");
                li15.SubItems.Add(Util.FormatPrice(items.Sum(me => me.ShangNiChengBen)));
                listView2.Items.Add(li15);

                var li16 = new ListViewItem("菲迪拉成本");
                li16.SubItems.Add(Util.FormatPrice(items.Sum(me => me.FeiDiLaChengBen)));
                li16.SubItems.Add("新锐成本");
                li16.SubItems.Add(Util.FormatPrice(items.Sum(me => me.XinRuiChengBen)));
                listView2.Items.Add(li16);

                var li17 = new ListViewItem("eko发货商品成本");
                li17.SubItems.Add(Util.FormatPrice(items.Sum(me => me.EKOFaHuoShangPinChengBen)));
                listView2.Items.Add(li17);

                var li18 = new ListViewItem("nut防丢器");
                li18.SubItems.Add(Util.FormatPrice(items.Sum(me => me.NutFangDiuQi)));
                listView2.Items.Add(li18);

                var li18_1 = new ListViewItem("米雅可成本");
                li18_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.MiYaKeChengBen)));
                li18_1.SubItems.Add("菜罩成本");
                li18_1.SubItems.Add(Util.FormatPrice(items.Sum(me => me.CaiZhaoChengBen)));
                listView2.Items.Add(li18_1);


                var li19 = new ListViewItem("仓库耗材");
                li19.SubItems.Add(Util.FormatPrice(items.Sum(me => me.YunCangChengBen1)));
                li19.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li19);

                var li20 = new ListViewItem("耗材费");
                li20.SubItems.Add(Util.FormatPrice(items.Sum(me => me.HaoCaiFei)));
                li20.SubItems.Add("矿泉水成本");
                li20.SubItems.Add(Util.FormatPrice(items.Sum(me => me.KuangQuanShuiChengBen)));
                listView2.Items.Add(li20);

                var li21 = new ListViewItem("定纸箱费用（第一月）");
                li21.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DingZhiXiangFeiYong)));
                li21.SubItems.Add("护角费用");
                li21.SubItems.Add(Util.FormatPrice(items.Sum(me => me.HuJiaoFeiYong)));
                listView2.Items.Add(li21);

                var li22 = new ListViewItem("易碎标签");
                li22.SubItems.Add(Util.FormatPrice(items.Sum(me => me.YiSuiBiaoQian)));
                li22.SubItems.Add("气泡柱");
                li22.SubItems.Add(Util.FormatPrice(items.Sum(me => me.QiPaoZhu)));
                listView2.Items.Add(li22);

                var li23 = new ListViewItem("胶带");
                li23.SubItems.Add(Util.FormatPrice(items.Sum(me => me.JiaoDai)));
                li10.SubItems.Add("优易订购费");
                li10.SubItems.Add(Util.FormatPrice(items.Sum(me => me.YouYiDingGouFei)));
                listView2.Items.Add(li23);


                var li24 = new ListViewItem("其他");
                li24.SubItems.Add(Util.FormatPrice(items.Sum(me => me.MiaoCangOther)));
                listView2.Items.Add(li24);


                var li28 = new ListViewItem("秒仓费用");
                li28.SubItems.Add(Util.FormatPrice(items.Sum(me => me.MiaoCangFeiYong1)));
                li28.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li28);

                var li29= new ListViewItem("定纸箱费用（第一月）");
                li29.SubItems.Add(Util.FormatPrice(items.Sum(me => me.DingZhiXiangFeiYong)));                
                listView2.Items.Add(li29);


                var li33 = new ListViewItem("仓储费与发货费用");
                li33.SubItems.Add(Util.FormatPrice(items.Sum(me => me.CangChuFeiYuFaHuoFeiYong)));
                li33.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li33);

                var li34 = new ListViewItem("宏伟物流成本");
                li34.SubItems.Add(Util.FormatPrice(items.Sum(me => me.HongWeiWuLiuChengBen)));
                listView2.Items.Add(li34);

            }
            else// 月
            {
                var item = _dbList.FirstOrDefault(me => me.ProfitDate.Equals(currDate));

                this.labelRemark.Text = string.Format("备注（{0}）", currDate);
                this.textBoxRemark.Text ="############################日期：(" + currDate + ")########################################################\r\n\r\n" + item.Remark + "\r\n\r\n";
               

                var li1 = new ListViewItem("月份");
                li1.SubItems.Add(item.ProfitDate);
                li1.SubItems.Add("纯利润");
                li1.SubItems.Add(Util.FormatPrice(item.Profit));
                this.listView2.Items.Add(li1);

                var li1_1 = new ListViewItem("毛 利");
                li1_1.SubItems.Add(Util.FormatPrice(item.MaoLi));
                this.listView2.Items.Add(li1_1);

                var li2 = new ListViewItem("营业额-天猫");
                li2.SubItems.Add(Util.FormatPrice(item.Sale_TianMao));
                li2.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2);

                var li2_1_2 = new ListViewItem("支付宝到帐-天猫");
                li2_1_2.SubItems.Add(Util.FormatPrice(item.Sale_TianMao_Alipay));
                li2_1_2.SubItems.Add("刷单-天猫");
                li2_1_2.SubItems.Add(Util.FormatPrice(item.Sale_TianMao_ShuaDian));
                listView2.Items.Add(li2_1_2);


                var li2_1 = new ListViewItem("营业额-淘宝");
                li2_1.SubItems.Add(Util.FormatPrice(item.Sale_Taobao));
                li2_1.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li2_1);

                var li2_1_1 = new ListViewItem("支付宝到帐-淘宝");
                li2_1_1.SubItems.Add(Util.FormatPrice(item.Sale_TaoBao_Alipay));
                li2_1_1.SubItems.Add("刷单-淘宝");
                li2_1_1.SubItems.Add(Util.FormatPrice(item.Sale_TaoBao_ShuaDian));
                listView2.Items.Add(li2_1_1);


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
                li5.SubItems.Add("社保，公积金");
                li5.SubItems.Add(Util.FormatPrice(item.SheBaoGongJiJin));
                listView2.Items.Add(li5);

                var li5_1 = new ListViewItem("办公杂费");
                li5_1.SubItems.Add(Util.FormatPrice(item.BanGongZaFei1));
                li5_1.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li5_1);

                var li5_2 = new ListViewItem("个人代付费用");
                li5_2.SubItems.Add(Util.FormatPrice(item.DaiFuFeiYong));
                listView2.Items.Add(li5_2);

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
                li13_2.SubItems.Add("刷单佣金费");
                li13_2.SubItems.Add(Util.FormatPrice(item.ShuaDianFeiYou));
                listView2.Items.Add(li13_2);


                var li13_3 = new ListViewItem("AI智能投放");
                li13_3.SubItems.Add(Util.FormatPrice(item.AiZhiNengTouFang));
                listView2.Items.Add(li13_3);

                var li14 = new ListViewItem("产品总成本");
                li14.SubItems.Add(Util.FormatPrice(item.ChanPinChengBen1));
                li14.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li14);

                var li15 = new ListViewItem("进货成本");
                li15.SubItems.Add(Util.FormatPrice(item.JinHuoChengBen));
                li15.SubItems.Add("尚尼成本");
                li15.SubItems.Add(Util.FormatPrice(item.ShangNiChengBen));
                listView2.Items.Add(li15);

                var li16 = new ListViewItem("菲迪拉成本");
                li16.SubItems.Add(Util.FormatPrice(item.FeiDiLaChengBen));
                li16.SubItems.Add("新锐成本");
                li16.SubItems.Add(Util.FormatPrice(item.XinRuiChengBen));
                listView2.Items.Add(li16);

                var li17 = new ListViewItem("eko发货商品成本");
                li17.SubItems.Add(Util.FormatPrice(item.EKOFaHuoShangPinChengBen));
                listView2.Items.Add(li17);

                var li18 = new ListViewItem("nut防丢器");
                li18.SubItems.Add(Util.FormatPrice(item.NutFangDiuQi));
               
                listView2.Items.Add(li18);

                var li18_1 = new ListViewItem("米雅可成本");
                li18_1.SubItems.Add(Util.FormatPrice(item.MiYaKeChengBen));
                li18_1.SubItems.Add("菜罩成本");
                li18_1.SubItems.Add(Util.FormatPrice(item.CaiZhaoChengBen));
                listView2.Items.Add(li18_1);


                var li19 = new ListViewItem("仓库耗材");
                li19.SubItems.Add(Util.FormatPrice(item.YunCangChengBen1));
                li19.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li19);

                var li20 = new ListViewItem("耗材费");
                li20.SubItems.Add(Util.FormatPrice(item.HaoCaiFei));
                li20.SubItems.Add("矿泉水成本");
                li20.SubItems.Add(Util.FormatPrice(item.KuangQuanShuiChengBen));
                listView2.Items.Add(li20);

                var li21 = new ListViewItem("定纸箱费用（第一月）");
                li21.SubItems.Add(Util.FormatPrice(item.DingZhiXiangFeiYong));
                li21.SubItems.Add("护角费用");
                li21.SubItems.Add(Util.FormatPrice(item.HuJiaoFeiYong));
                listView2.Items.Add(li21);

                var li22 = new ListViewItem("易碎标签");
                li22.SubItems.Add(Util.FormatPrice(item.YiSuiBiaoQian));
                li22.SubItems.Add("气泡柱");
                li22.SubItems.Add(Util.FormatPrice(item.QiPaoZhu));
                listView2.Items.Add(li22);

                var li23 = new ListViewItem("胶带");
                li23.SubItems.Add(Util.FormatPrice(item.JiaoDai));
                li23.SubItems.Add("优易订购费");
                li23.SubItems.Add(Util.FormatPrice(item.YouYiDingGouFei));
                listView2.Items.Add(li23);

                var li24 = new ListViewItem("其他");
                li24.SubItems.Add(Util.FormatPrice(item.MiaoCangOther));
                listView2.Items.Add(li24);

                var li28 = new ListViewItem("秒仓费用");
                li28.SubItems.Add(Util.FormatPrice(item.MiaoCangFeiYong1));
                li28.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li28);

                var li29 = new ListViewItem("仓储费与发货费用");
                li29.SubItems.Add(Util.FormatPrice(item.CangChuFeiYuFaHuoFeiYong));               
                listView2.Items.Add(li29);


                var li30 = new ListViewItem("物流费用");
                li30.SubItems.Add(Util.FormatPrice(item.WuLiuChengBen1));
                li30.BackColor = Color.WhiteSmoke;
                listView2.Items.Add(li30);

                var li33 = new ListViewItem("宏伟物流成本");
                li33.SubItems.Add(Util.FormatPrice(item.HongWeiWuLiuChengBen));              
                listView2.Items.Add(li33);
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
