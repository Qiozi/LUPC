using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Xml;
using MySql.Data.MySqlClient;
using LUComputers.DBProvider;

namespace LUComputers
{
    public partial class Demo : Form
    {
        public Demo()
        {
            InitializeComponent();
        }


        private void button_run_directdial_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                HttpHelper hh = new HttpHelper();


                //"http://www.etccomputer.ca/eShop/default.asp?categoryid=4"
                string s = this.richTextBox_source_code.Text;
                Regex re = new Regex(@"Manufacturer No.: <B>(.*)</B>|>\$ (.*)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);

                int count = 0;

                //decimal price = 0M;
                foreach (Match m in mc)
                {
                    count += 1;
                    if (count % 2 == 1)
                    {
                        this.richTextBox_result.Text += System.Web.HttpUtility.UrlDecode(m.Groups[1].Value) + "\t";//.Replace("%2F", "/").Replace("%20", " ").Replace("%2B", "+").Replace("%2D", "-").Replace("%28", "(") + "\t";

                        this.richTextBox_result.SelectionStart = this.richTextBox_result.Text.Length;
                        this.richTextBox_result.ScrollToCaret();
                        this.richTextBox_result.Update();

                    }
                    else
                    {
                        this.richTextBox_result.Text += m.Groups[2].Value + "\t\n";
                    }
                }

                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button_canada_computers_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                HttpHelper hh = new HttpHelper();


                //"http://www.etccomputer.ca/eShop/default.asp?categoryid=4"
                string s = this.richTextBox_source_code.Text;
                //s = hh.HttpGet("http://192.168.1.101:8088/application/3/ln.gk.index.xml");
                //this.richTextBox_result.Text = s;
                //Regex re = new Regex(@"<a href=""\/index\.php\?cPath=(43_[\d|_]*)&amp;[A-Za-z0-9=]+"">.*?(?=<\/a>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                Regex re = new Regex(@"<a href=""\/index\.php\?cPath=(43_[\d|_]*)&amp;[A-Za-z0-9=]+"">.*?(?=<\/a>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

                MatchCollection mc = re.Matches(s);

                foreach (Match m in mc)
                {
                    string cateIds = m.Groups[1].Value;
                    string webUrl = "/index.php?cPath=" + cateIds;
                    string cateName = m.Groups[0].Value.Split(new char[] { '>' })[1];

                    this.richTextBox_result.Text += string.Format("{0}\t{1}", cateIds, cateName) + "\t\n";// +m.Groups[1].Value + "\t\n";


                }

                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button_canada_computers_detail_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                HttpHelper hh = new HttpHelper();


                //"<td align=right class=priceHeading><b>&nbsp;$316.99</b></td>"
                //<td align=right class=priceHeading>&nbsp;<b>$305.99</b></td>
                string s = this.richTextBox_source_code.Text.Replace("&nbsp;", "");
                Regex re = new Regex(@"(?is)(?<=<table[^>]*?class=""productListing""[^>]*?>(?:(?!</?table).)*)<tr[^>]*?>.*?</tr>(?:\s*<tr[^>]*?>(?:\s*<td[^>]*?>(.*?)</td>)*\s*</tr>)*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);

                foreach (Match m in mc)
                {

                    //this.richTextBox_result.Text += string.Format("{0}", m.Groups[1].Value) + "\t\n";// +m.Groups[1].Value + "\t\n";

                    string[] strs = m.Groups[0].Value.Split(new string[] { "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var subs in strs)
                    {
                        Regex sube = new Regex(@"<a[^>]*href=(['""]?)(?<url>[^""\s>]*)\1[^>]*>(?<text>[\s\S]*?)</a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection subMC = sube.Matches(subs);
                        string title = "";
                        foreach (Match subm in subMC)
                        {
                            if (subm.Groups["text"].Value.IndexOf("<img") == -1)
                                title = subm.Groups["text"].Value;
                        }

                        string mfp = "";
                        sube = new Regex(@"<div[^>]*>Part no:(?<text>[\s\S]*?)</div>");
                        subMC = sube.Matches(subs);
                        foreach (Match subm in subMC)
                        {
                            mfp = subm.Groups["text"].Value.Trim();
                        }

                        string price = "";
                        decimal instantRebate = 0M;
                        if (subs.IndexOf("<s>") > -1)
                        {
                            sube = new Regex(@"<s>\$(?<text>[\s\S]*?)<\/s>");
                            subMC = sube.Matches(subs);

                            Regex cRe = new Regex(@"after \$(?<text>[\s\S]*?) instant rebate");
                            MatchCollection cMc = cRe.Matches(subs);
                            foreach (Match mmm in cMc)
                            {
                                decimal.TryParse(mmm.Groups["text"].Value, out instantRebate);
                            }
                        }
                        else
                        {
                            sube = new Regex(@"<td[^>]*>\$(?<text>[\s\S]*?)</td>");
                            subMC = sube.Matches(subs);
                        }
                        foreach (Match subm in subMC)
                        {
                            price = subm.Groups["text"].Value.Trim();
                        }

                        this.richTextBox_result.Text += title + "\t\n";
                        this.richTextBox_result.Text += mfp + "\t\n";
                        this.richTextBox_result.Text += price + "\t" + instantRebate.ToString() + "\t\n";

                    }

                }


                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void button_canada_computer_3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpHelper hh = new HttpHelper();
            string _canada_computer_url = "http://www.canadacomputers.com/index.php?do=ShowProdList&cmd=pl&id=CPU.84";

            // for (int i = 0; i < rms.Length; i++)
            {
                string s = hh.HttpGet(_canada_computer_url);//hh.HttpGet(rms[i].rival_url);
                Regex re = new Regex(@"index.php\?do=ShowProduct&cmd=pd&pid=[0-9]{6}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);
                // SetURL(rms[i].rival_url);

                //int count = 0;

                foreach (Match m in mc)
                {
                    string part_url = string.Format("http://www.canadacomputers.com/{0}", m.Groups[0].Value);// + "\t\n";// +m.Groups[1].Value + "\t\n";
                    this.richTextBox_result.Text += part_url + "\r\n";
                    this.richTextBox_result.Update();
                    string part_code = hh.HttpGet(part_url).ToLower().Replace("&nbsp;", ""); ;
                    Regex part_re = new Regex(@"<td class=""smallHeading"">(.*)</td>|<td align=right class=priceHeading><b>\$(.*)<\/b><\/td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection mcc = part_re.Matches(part_code);

                    int count = 0;
                    decimal price = 0M;
                    foreach (Match mm in mcc)
                    {
                        try
                        {
                            count += 1;
                            if (count % 2 == 1)
                            {
                                string[] scontent = mm.Groups[1].Value.Split(new string[] { "item code:", "part number:" }, StringSplitOptions.RemoveEmptyEntries);
                                this.richTextBox_result.Text += string.Format("{0}\t{1}", scontent[0], scontent[1]) + "\t";// +m.Groups[1].Value + "\t\n";

                            }
                            else
                            {
                                decimal.TryParse(mm.Groups[2].Value, out price);
                                this.richTextBox_result.Text += mm.Groups[2].Value + "\t\n";
                            }
                        }
                        catch (Exception ex)
                        {
                            this.richTextBox_result.Text += part_url + "\t\t" + ex.Message;
                        }
                    }
                }
            }
            MessageBox.Show("OK");
            this.Cursor = Cursors.Default;
        }

        private void btn_tigerdirect1_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                HttpHelper hh = new HttpHelper();

                //"http://www.etccomputer.ca/eShop/default.asp?categoryid=4"
                string s = this.richTextBox_source_code.Text;
                Regex re = new Regex(@"""\/applications\/SearchTools\/item-details.asp\?EdpNo=(.*)"" class=""buy""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);

                foreach (Match m in mc)
                {
                    this.richTextBox_result.Text += string.Format("http://www.tigerdirect.ca/applications/SearchTools/item-details.asp?EdpNo={0}", m.Groups[1].Value) + "\t\n";// +m.Groups[1].Value + "\t\n";                    
                }

                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_tigerdirect2_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            try
            {
                HttpHelper hh = new HttpHelper();
                //
                //<span class=""font_right_size3""><strong>\$(.*)</strong>
                string s = this.richTextBox_source_code.Text;
                Regex re = new Regex(@"<span class=""font_right_size3""><strong>\$(.*)</strong>|Mfg Part No:[\s]*<b>(.*)</b><br>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);

                //foreach (Match m in mc)
                //{

                //    this.richTextBox_result.Text += string.Format("{0}", m.Groups[0].Value) + "\t\n";// +m.Groups[1].Value + "\t\n";

                //}
                int count = 0;
                decimal price = 0M;
                foreach (Match m in mc)
                {
                    count += 1;
                    if (count % 2 == 1)
                    {

                        decimal.TryParse(m.Groups[1].Value, out price);
                        this.richTextBox_result.Text += price.ToString() + "\t\n";
                    }
                    else
                    {
                        this.richTextBox_result.Text += string.Format("{0}", m.Groups[2].Value) + "\t";// +m.Groups[1].Value + "\t\n";
                    }
                }
                this.richTextBox_result.Text += count.ToString();
                MessageBox.Show("OK");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Cursor = Cursors.Default;
        }

        private void btn_tigerdirect3_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpHelper hh = new HttpHelper();
            //string _canada_computer_url = "http://www.tigerdirect.ca/applications/Category/category_cpu.asp?name=CPUs";
            string _canada_computer_url = "http://www.tigerdirect.com/applications/category/category_slc.asp?page=4&Nav=|c:3429|&Sort=0&Recs=10";
            // for (int i = 0; i < rms.Length; i++)
            {
                string s = hh.HttpGet(_canada_computer_url);

                Regex re = new Regex(@"""\/applications\/SearchTools\/item-details.asp\?EdpNo=(.*)"" class=""buy""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);
                if (mc.Count == 0)
                {
                    // <a href="../SearchTools/item-details.asp?EdpNo=4011665&Sku=G180-17001">
                    re = new Regex(@"<a href=""\.\.\/SearchTools\/item-details.asp\?EdpNo=(.*)&", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    mc = re.Matches(s);
                }
                if (mc.Count == 0)
                {
                    // <a href="../SearchTools/item-details.asp?EdpNo=4011665&Sku=G180-17001">
                    re = new Regex(@"<a href=""\/applications\/SearchTools\/item-details.asp\?EdpNo=(.*)&", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    mc = re.Matches(s);
                }
                // SetURL(rms[i].rival_url);

                //int count = 0;

                foreach (Match m in mc)
                {
                    string part_url = string.Format("http://www.tigerdirect.ca/applications/SearchTools/item-details.asp?EdpNo={0}", m.Groups[1].Value);// + "\t\n";// +m.Groups[1].Value + "\t\n";
                    this.richTextBox_result.Text += part_url + "\r\n";
                    this.richTextBox_result.Update();
                    string part_code = hh.HttpGet(part_url);
                    Regex part_re = new Regex(@"<td class=""font_right_prod""><strong>(.*)</strong></td>|<span class=""font_right_size3""><strong>\$(.*)</strong>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                    MatchCollection mcc = part_re.Matches(part_code);

                    int count = 0;
                    decimal price = 0M;
                    foreach (Match mm in mcc)
                    {
                        count += 1;
                        if (count % 3 == 1)
                        {
                            this.richTextBox_result.Text += string.Format("{0}", mm.Groups[1].Value) + "\t";// +m.Groups[1].Value + "\t\n";
                        }
                        else if (count % 3 == 2) { }
                        else
                        {
                            decimal.TryParse(mm.Groups[2].Value, out price);
                            this.richTextBox_result.Text += mm.Groups[2].Value + "\t\n";
                        }
                    }
                    this.richTextBox_result.Text += count.ToString();

                }
            }
            MessageBox.Show("OK");
            this.Cursor = Cursors.Default;
        }

        private void Demo_Load(object sender, EventArgs e)
        {

        }

        private void btn_comtronic_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            HttpHelper hh = new HttpHelper();
            // string url = "http://www.comtronic.ca/cci/home.php?do=ShowDealerProdList&cmd=list&id=NB&cname=Notebook";

            string s = this.richTextBox_source_code.Text;// hh.HttpGet(url);

            //Regex re = new Regex(@"<a href=""home.php?do=ShowDealerProdList&cmd=list&id=CP.47&cname=PC Case"" target=""_self"" class=""menu"">", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //MatchCollection mc = re.Matches(s);

            Regex re = new Regex(@"<a href=""home.php\?do=ShowDealerProdList\&cmd=list\&id=(.*)\&cname=(.*)"" target=""_self"" class=""menu"">", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(s);
            this.richTextBox_source_code.Text = s;
            foreach (Match m in mc)
            {
                //this.richTextBox_result.Text += m.Groups[0].Value + "\r\n";
            }

            re = new Regex(@"<a name=""(.*)"">|<font face=""Tahoma"" size=""1""><b>\$(.*)</b></font></td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            mc = re.Matches(s);
            this.richTextBox_source_code.Text = s;
            int i = 0;
            foreach (Match m in mc)
            {
                i += 1;
                if (i % 2 == 1)
                    this.richTextBox_result.Text += m.Groups[1].Value + "|";// +m.Groups[1].Value + "\r\n";
                else
                {
                    decimal price;
                    decimal.TryParse(m.Groups[2].Value, out price);
                    this.richTextBox_result.Text += price.ToString() + "\r\n";
                }
            }

            MessageBox.Show("OK");
            this.Cursor = Cursors.Default;
        }

        private void button_newegg_Click(object sender, EventArgs e)
        {
            HttpHelper hh = new HttpHelper();
            //string url = "http://www.newegg.ca/Product/ProductList.aspx?Submit=ene&bop=And&Pagesize=100";
            string s = this.richTextBox_source_code.Text;
            Regex re = new Regex(@"<li><b>Model #: </b>(.*)</li>|<li><b>Item #: </b>(.*)</li>|<li class=""ckoutAmt"">Your Price:\$(.*)</li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(s);
            this.richTextBox_source_code.Text = s;

            int i = 0;
            foreach (Match m in mc)
            {
                i += 1;

                if (i % 3 == 1)
                {
                    this.richTextBox_result.Text += m.Groups[1].Value + "\t";

                }
                else if (i % 3 == 2)
                {
                    this.richTextBox_result.Text += m.Groups[2].Value + "\t";
                }
                else
                {
                    this.richTextBox_result.Text += m.Groups[3].Value + "\r\n";
                }
            }

            re = new Regex(@"page=\d{3}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            mc = re.Matches(s);
            int maxpage = 1;
            foreach (Match m in mc)
            {
                int current_max_page;

                int.TryParse(m.Groups[0].Value.ToLower().Replace("page=", ""), out current_max_page);

                if (current_max_page > maxpage)
                    maxpage = current_max_page;

            }
            this.richTextBox_result.Text += maxpage.ToString();

            MessageBox.Show("OK");
        }

        private void btn_ncix_Click(object sender, EventArgs e)
        {


        }

        private void btn_shopbot_Click(object sender, EventArgs e)
        {
            //string url = "http://www.shopbot.ca/m/?m=X53U-RS21-CA";

            //HttpHelper hh = new HttpHelper();
            string s = "";
            //if (this.richTextBox_source_code.Text != "")
            //    s = this.richTextBox_source_code.Text;
            //else
            //    s = hh.HttpGet(url);
            //s = new Watch.Shopbot().ShopbotReplace(s);

            s = this.richTextBox_source_code.Text;

            string[] strs = s.Split(new string[] { "div class=\"pNameShort\">", "<span class=\"phrase shopname-store-label\">" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < strs.Length - 1; i++)
            {
                try
                {
                    //this.richTextBox_result.Text += str + "\r\t";
                    if (strs[i].ToLower().IndexOf("<div class=\"price\" data-price=") > -1)
                    {
                        //this.richTextBox_result.Text += strs[i] + "\r\n\r\n\r\n\r\n\r\n";
                        if (strs[i].Substring(strs[i].Length - 6, 6) == "<br />")
                        {
                            string ltdName = "";
                            for (int j = strs[i].Length - 13; j >= 0; j--)
                            {
                                if (strs[i][j] == '>')
                                {

                                    ltdName = strs[i].Substring(j + 1).Replace("</span><br />", "");
                                    this.richTextBox_result.Text += ltdName + "\t";

                                    string priceStr = strs[i].Substring(strs[i].IndexOf("<div class=\"price\" data-price=\"") + "<div class=\"price\" data-price=\"".Length);
                                    this.richTextBox_result.Text += priceStr.Split(new char[] { '"' })[0] + "\r\n\r\n\r\n\r\n\r\n";

                                    break;
                                }
                            }
                        }
                        if (strs[i].Substring(strs[i].Length - 7, 7) == "</span>")
                        {
                            string ltdName = "";
                            for (int j = strs[i].Length - 12; j >= 0; j--)
                            {
                                if (strs[i][j] == '"')
                                {

                                    ltdName = strs[i].Substring(j + 1).Replace("\" /></span>", "");
                                    this.richTextBox_result.Text += ltdName + "\t";

                                    string priceStr = strs[i].Substring(strs[i].IndexOf("<div class=\"price\" data-price=\"") + "<div class=\"price\" data-price=\"".Length);
                                    this.richTextBox_result.Text += priceStr.Split(new char[] { '"' })[0] + "\r\n\r\n\r\n\r\n\r\n";

                                    break;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex) { throw ex; }
            }


            //Regex re = new Regex(@"<span class=""price"">\$([0-9.,]+)</span> - <span class=""compare_link"">Buy it Now from (.*)</span>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //MatchCollection mc = re.Matches(s);
            //foreach (Match m in mc)
            //{
            //    try
            //    {
            //        this.richTextBox_result.Text += m.Groups[1].Value.ToString().Replace("</span>","") + "\r\t";
            //        this.richTextBox_result.Text += m.Groups[2].Value.ToString().Replace("</span>", "") + "\r\n";
            //    }
            //    catch (Exception ex) { throw ex; }
            //}
            MessageBox.Show("OK");
        }

        private void btn_export_csv_Click(object sender, EventArgs e)
        {

            string s = this.richTextBox_source_code.Text;

            Regex re = new Regex(@"<title>(.+?)<\/title>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            Regex re2 = new Regex(@"<span class=""notranslate"" id=""prcIsum"" itemprop=""price""  style="""">(.+?)</span>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection mc = re.Matches(s);
            MatchCollection mc2 = re2.Matches(s);

            foreach (Match m in mc2)
            {
                try
                {
                    this.richTextBox_result.Text += mc[0].Groups[1].Value.ToString() + "\r\n";
                    this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";
                }
                catch (Exception ex) { throw ex; }
            }
            MessageBox.Show("OK");
        }

        private void btn_etc_Click(object sender, EventArgs e)
        {

            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.Rival_ETC);

            string s = this.richTextBox_source_code.Text;
            //string[] ss = s.Split(new string[]{" id=\"vertnav\"", "<!--<div class=\"block\">"},StringSplitOptions.None);
            //MessageBox.Show(ss.Length.ToString());
            //s = ss[1];
            // Regex re = new Regex(@"<span itemprop=""price"" class=""bin g\-b"">C \$([0-9.,]+)<\/span>"); 
            // Regex re2 = new Regex(@"<a itemprop=""name"" id=""src([0-9.,]+)""");
            Regex re3 = new Regex(@"<img alt=""([\s\w\#\&\- \.,;:\/\(\)]+)"" title=""([\s\w\#\&\- \.,;:\/\(\)]+)"" src=""([\s\w\#\&\- \.,;:\/\(\)]+)"" itemprop=""image"" border=""0"">");
            //MatchCollection mc = re.Matches(s);
            // MatchCollection mc2 = re2.Matches(s);
            MatchCollection mc3 = re3.Matches(s);

            for (int i = 0; i < mc3.Count; i++)
            {
                try
                {

                    //int part_id = 0;
                    //int.TryParse(m.Groups[1].Value.ToString(), out part_id);
                    //string name = m.Groups[3].Value.ToString();
                    //decimal price = 0M;
                    //decimal.TryParse(m.Groups[4].Value.ToString().Replace("$", ""), out price);

                    //this.richTextBox_result.Text += part_id.ToString() + "\r\n";
                    //this.richTextBox_result.Text += price.ToString() + "\r\n";
                    // this.richTextBox_result.Text += mc2[i].Groups[1].Value.ToString() + "\t";
                    // this.richTextBox_result.Text += mc[i].Groups[1].Value.ToString() + "\t";
                    this.richTextBox_result.Text += mc3[i].Groups[1].Value.ToString() + "\r\n";

                }
                catch (Exception ex) { throw ex; }
            }
            for (int i = 0; i < mc3.Count; i++)
            {
                this.richTextBox_result.Text += mc3[i].Groups[1].Value.ToString() + "\r\n";
            }


            MessageBox.Show("OK");
        }

        private void button_currency_convert_Click(object sender, EventArgs e)
        {
            string url = "http://ca.finance.yahoo.com/currency/convert?amt=1&from=CAD&to=USD&c=0";

            HttpHelper hh = new HttpHelper();
            string s = hh.HttpGet(url);

            this.richTextBox_source_code.Text = s;
            //string s = this.richTextBox_source_code.Text;

            Regex re = new Regex(@"<td class=""yfnc_tabledata1""><b>([0-9.,]+)</b></td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(s);
            //SetRichText(url + "\r\n");

            //this.richTextBox_result.Text = mc.Count.ToString() + "\r\n";
            int n = 0;
            decimal cad = 1M;
            decimal usd = 1M;
            foreach (Match m in mc)
            {
                n += 1;
                if (n == 1)
                {
                    decimal.TryParse(m.Groups[1].Value, out cad);

                }
                if (n == 2)
                {
                    decimal.TryParse(m.Groups[1].Value, out usd);
                }
            }
        }

        private void button1_supercom_Click(object sender, EventArgs e)
        {
            string str = string.Empty;
            DataTable dt = Config.ExecuteDateTable("show tables");
            foreach (DataRow dr in dt.Rows)
            {
                var tableName = dr[0].ToString();
                DataTable subDt = Config.ExecuteDateTable("SHOW FIELDS FROM " + tableName + " where type = 'datetime'");
                foreach (DataRow subdr in subDt.Rows)
                {
                    str += string.Format(@" select {0} from {1} where {0} like '0000%'
union "
                        , subdr["field"].ToString()
                        , tableName);
                }
            }
            richTextBox_source_code.Text = str;
        }

        private void button_alcmicro_Click(object sender, EventArgs e)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_ALC);



            string s = this.richTextBox_source_code.Text;


            Regex r = new Regex("style=\"[A-Za-z0-9;:\\-\\#]+\"");

            Regex r1 = new Regex("align=\"[A-Za-z]+\"");
            Regex r2 = new Regex("onmouseout=\"[A-Za-z0-9;:\\-\\s\\.\\#=']+\"");
            Regex r3 = new Regex("onmouseover=\"[A-Za-z0-9;:\\-\\s\\.\\#=']+\"");
            s = r.Replace(s.Replace("\r\n", "").Replace("\n", "").Replace("\r", ""), "");//"style=\"(.*)\"", "");
            s = r1.Replace(s, "");
            s = r2.Replace(s, "");
            s = r3.Replace(s, "");
            s = s.Replace("<td >", "<td>");
            s = s.Replace("<td  >", "<td>");
            s = s.Replace("\t", "");

            Regex re = new Regex(@"<td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td><td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //Regex re = new Regex(@"<td>[A-Za-z0-9;:\-\#\s\.&""\$]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);//<td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>\$([0-9]+)</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td><td>[A-Za-z0-9;:\\-\\#]+</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);


            //this.richTextBox_result.Text = s;

            MatchCollection mc = re.Matches(s);

            foreach (Match m in mc)
            {
                try
                {
                    this.richTextBox_result.Text += m.Groups[0].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[1].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[2].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[3].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[4].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[5].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[6].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[7].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[8].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\t";
                    //this.richTextBox_result.Text += m.Groups[9].Value.ToString().Replace("<td>", "").Replace("</td>", "").Trim() + "\r\n";

                }
                catch (Exception ex) { throw ex; }
            }
            MessageBox.Show("OK");
        }

        private void button_cctv_Click(object sender, EventArgs e)
        {
            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.wholesaler_Smartvision_Direct);



            string s = this.richTextBox_source_code.Text;
            // Category
            // Regex re = new Regex(@"<a href='\.\/index.php\?query=list&ca_id=([0-9]+)' ><span class=""basic_verdana style7 style8""><span class=""style13 style1"">(.*)<\/span><\/span><\/a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // SKU
            // Regex re = new Regex(@"<(font) color=666666><b><a href='\.\/index.php\?ca_id=([0-9]+)&it_id=([0-9]+)&query=item'>([\s\w-]+)<\/a><\/b><\/\1>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // Name
            //Regex re = new Regex(@"<td width=200 ><div aligh=left><font color=666666>([^<]+)</font></div></td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            // price
            //Regex re = new Regex(@"<td width=100><div align=right><b><font color=3333CC>\$&nbsp;([0-9.,]+)</fonr></b></div></td>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            // SKU, Name, Price
            Regex re = new Regex(@"<(font) color=666666><b><a href='\.\/index.php\?ca_id=([0-9]+)&it_id=([0-9]+)&query=item'>([\s\w-/]+)<\/a><\/b><\/\1>|<td width=200 ><div aligh=left><font color=666666>([^<]+)</font></div></td>|<td width=100><div align=right><b><font color=3333CC>\$&nbsp;([0-9.,]+)</fonr></b></div></td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(s);

            Regex pre = new Regex(@"<a href='\./index.php\?query=list&ca_id=([0-9]+)&pg=([0-9]+)'>Next<\/a>", RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection pmc = pre.Matches(s);

            if (pmc.Count > 0)
                this.richTextBox_result.Text += "http://www.smartvisiondirect.com/index.php?query=list&ca_id=" + pmc[0].Groups[1].Value.ToString() + "&pg=" + pmc[1].Groups[2].Value.ToString() + "\r\n";
            //SetRichText(url + "\r\n");

            int c = 0;
            foreach (Match m in mc)
            {
                try
                {
                    // MessageBox.Show(m.Groups.Count.ToString());
                    if (c % 3 == 0)
                    {
                        // mfp#
                        this.richTextBox_result.Text += m.Groups[4].Value.ToString() + "\t";
                    }
                    if (c % 3 == 1)
                    {
                        // name
                        this.richTextBox_result.Text += m.Groups[5].Value.ToString() + "\t";
                    }
                    if (c % 3 == 2)
                    {
                        // price
                        this.richTextBox_result.Text += m.Groups[6].Value.ToString() + "\r\n";
                    }
                    c += 1;


                }
                catch (Exception ex) { throw ex; }
            }
            //MessageBox.Show(mc.Count.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string url = Config.ETCCategoryURL;

            LtdHelper LH = new LtdHelper();
            int ltd_id = LH.LtdHelperValue(Ltd.Rival_ETC);

            //string url = "http://www.etccomputer.ca/index.php";
            //HttpHelper hh = new HttpHelper();
            //string s = hh.HttpGet(url);


            //            Regex re = new Regex(@"<td width=""400"" align=""left""><A HREF=""product_details.asp\?productid=(\d{4}|\d{3}|\d{2}|\d{5})""(.*)class=""list_lname"">(.*)</A></td>
            //<td width=""70"" align=""right""><font class=""list_price"">\$([0-9.,]+)</td>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            //            MatchCollection mc = re.Matches(s);
            //SetRichText(url + "\r\n");
            string s = this.richTextBox_source_code.Text;
            //string[] ss = s.Split(new string[] { " id=\"vertnav\"", "<!--<div class=\"block\">" }, StringSplitOptions.None);
            //MessageBox.Show(ss.Length.ToString());
            //s = ss[1];
            Regex re = new Regex(@"<span class=""vertnav-cat""><a href=""http:\/\/etccomputer\.ca\/([^<].*)\.html""><span>([^<].*)<\/span><\/a>");
            MatchCollection mc = re.Matches(s);


            foreach (Match m in mc)
            {
                try
                {

                    //int part_id = 0;
                    //int.TryParse(m.Groups[1].Value.ToString(), out part_id);
                    //string name = m.Groups[3].Value.ToString();
                    //decimal price = 0M;
                    //decimal.TryParse(m.Groups[4].Value.ToString().Replace("$", ""), out price);

                    //this.richTextBox_result.Text += part_id.ToString() + "\r\n";
                    //this.richTextBox_result.Text += price.ToString() + "\r\n";

                    this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";

                }
                catch (Exception ex) { throw ex; }
            }
            MessageBox.Show("OK");
        }

        private void button_etc_detail_Click(object sender, EventArgs e)
        {
            HttpHelper hh = new HttpHelper();
            //string s = hh.HttpGet(url);
            string s = hh.HttpGet("http://www.etccomputer.ca/laptops/asus-k52f-b1-p6000-15-6-w7hp.html"); // this.richTextBox_source_code.Text;
            Regex re = new Regex(@"<h1>([\s\w- \.,/\(\)]+)<\/h1>");
            MatchCollection mc = re.Matches(s);
            foreach (Match m in mc)
            {
                try
                {
                    this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";

                }
                catch (Exception ex) { throw ex; }
            }
            re = new Regex(@"<span class=""price"">\$([0-9\.,]+)</span>");
            mc = re.Matches(s);
            foreach (Match m in mc)
            {
                this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";
            }

            re = new Regex(@"<input type=""hidden"" name=""product"" value=""([0-9.,]+)"" />");
            mc = re.Matches(s);
            foreach (Match m in mc)
            {
                this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";
            }

            re = new Regex(@"<p><strong>SKU: <\/strong>([\s\w]+)<\/p>");
            mc = re.Matches(s);
            foreach (Match m in mc)
            {
                this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";
            }
            string[] ss = s.Split(new string[] { "<th class=\"label\">Model</th>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
            string model = ss[1].Trim().Replace("</td>", "").Replace("<td class=\"data\">", "").Replace("<td class=\"data last\">", "").Trim();
            if (model.Length > 50)
                model = "";
            this.richTextBox_result.Text += model;

            //string[]  ss = s.Split(new string[] { "<th class=\"label\">Model</th>", "</tr>" }, StringSplitOptions.RemoveEmptyEntries);
            //this.richTextBox_result.Text += ss[1].Replace("</td>","").Replace("<td class=\"data\">", "").Trim();

            //re = new Regex(@"<span class=""price"">\$([0-9.,]+)</span>");
            //mc = re.Matches(s);
            //foreach (Match m in mc)
            //{
            //    this.richTextBox_result.Text += m.Groups[0].Value.ToString() + "\r\n";
            //}


            MessageBox.Show("OK");
        }

        private void button_taobao_Click(object sender, EventArgs e)
        {
            HttpHelper hh = new HttpHelper();
            //string s = hh.HttpGet(url);
            //string s = hh.HttpGet("http://item.taobao.com/item.htm?spm=1.7274553.701.2.JklN6y&scm=12306.999.0.0&id=40031386537"); // this.richTextBox_source_code.Text;

            string s = this.richTextBox_source_code.Text;
            Regex re = new Regex(@"<dl.*>(.*)</dl>", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
            MatchCollection mc = re.Matches(s.Replace("\r", "").Replace("\n", ""));

            string ss = mc[0].Groups[0].Value.ToString();

            Regex re2 = new Regex(@"<li data-value=""([0-9:]+)"" title=""([\s\w\-\[\]\(\)]+)""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            mc = re2.Matches(ss);

            foreach (Match m in mc)
            {
                try
                {
                    this.richTextBox_result.Text += m.Groups[1].Value.ToString() + "\r\n";
                    this.richTextBox_result.Text += m.Groups[2].Value.ToString() + "\r\n";
                }
                catch (Exception ex) { throw ex; }
            }


            Regex sre = new Regex(@"<script>[\s\S]*?</script>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            mc = sre.Matches(s);

            foreach (Match m in mc)
            {
                try
                {
                    if (m.Groups[0].Value.IndexOf("skuMap") > -1)
                    {

                        string pricejson = m.Groups[0].Value.ToString().Replace("\r", "").Replace("\n", "");
                        // this.richTextBox_result.Text += pricejson+ "\r\n";
                        Regex subre = new Regex(@""";[0-9]+:[0-9]+;"":|""skuId"":""[0-9]+"",|""oversold"":""[a-z]+""|""price"":""[0-9,]\.+""|""stock"":""[0-9]+""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                        MatchCollection submc = subre.Matches(pricejson.Replace(" ", ""));

                        foreach (Match mm in submc)
                        {
                            this.richTextBox_result.Text += mm.Groups[0].Value.ToString() + "\r\n";
                        }

                    }
                }
                catch (Exception ex) { throw ex; }
            }

        }

    }
}
