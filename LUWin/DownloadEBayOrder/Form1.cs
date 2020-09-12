using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Xml;
using System.IO;
using System.Linq;
using System.Threading;
using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Diagnostics;
using DownloadEBayOrder.BLL;

namespace DownloadEBayOrder
{
    public partial class Form1 : Form
    {
        static bool _run = true;

        public static Logs log = new Logs(Application.StartupPath);
        DateTime _watchEbayPartDatetime = DateTime.MinValue;
        DownloadEbayPartPrice _depp = new DownloadEbayPartPrice();
        DownloadOrder _order = new DownloadOrder();
        GenerateNewSiteProductInfo _generateProdInfo = new GenerateNewSiteProductInfo();
        eBayHelper _ebayHelper = new eBayHelper();
        GeneratePartHtmlFile _generateHtmlFile = new GeneratePartHtmlFile();
        GenerateNewSiteHomeData _generateHomeData = new GenerateNewSiteHomeData();
        BLL.ChangeWord _changeword = new ChangeWord();

        public Form1()
        {

            //var a = 5;// 瓶
            //var b = 5;// 盖
            //var c = 5;// 空

            //while (b >= 4 || c >= 2)
            //{
            //    if (b >= 4)
            //    {
            //        a++;
            //        c++;
            //        b = b - 4;
            //        b++;
            //    }
            //    if (c >= 2)
            //    {
            //        a++;
            //        c++;
            //        b++;
            //        c = c - 2;
            //    }
            //}
            //MessageBox.Show(a.ToString());

            InitializeComponent();
            this.Shown += new EventHandler(Form1_Shown);
            //this.fileSystemWatcher1.Path = Config.WatchPath;
            _depp.SetStatus("Hello World");
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            //button1_Click(null, null);
            //this.Close();
            _order.WatchE += new Events.MyEventHandler(_depp_WatchE);
            _depp.WatchE += new Events.MyEventHandler(_depp_WatchE);
            _generateProdInfo.WatchE += new Events.MyEventHandler(_depp_WatchE);
            _ebayHelper.WatchE += new Events.MyEventHandler(_depp_WatchE);
            _generateHtmlFile.WatchE += new Events.MyEventHandler(_depp_WatchE);
            _changeword.WatchE += new Events.MyEventHandler(_depp_WatchE);

            Thread t = new Thread(RunTimer);
            t.IsBackground = false;
            t.Start();

            Thread t2 = new Thread(ReadTimer);
            t2.IsBackground = false;
            t2.Start();

            _order.SetStatus("Hello World");
        }

        void _depp_WatchE(object sender, Events.MyEventArgs e)
        {
            this.label1.Invoke(new MethodInvoker(delegate
            {
                label1.Text = e._urlEventModel.comment;
            }));
        }

        static string LoadEBayOrderItemDescription(int OrderID)
        {
            #region settings
            string devID = Config.devID;
            string appID = Config.appID;
            string certID = Config.certID;

            //Get the Server to use (Sandbox or Production)
            string serverUrl = Config.serverUrl;

            //Get the User Token to Use
            string userToken = Config.userToken;

            //SiteID = 0  (US) - UK = 3, Canada = 2, Australia = 15, ....
            //SiteID Indicates the eBay site to associate the call with
            int siteID = Config.siteID;
            #endregion

            #region Load The XML Document Template and Set the Neccessary Values
            //Load the XML Document to Use for this Request
            XmlDocument xmlDoc = new XmlDocument();

            ////Get XML Document from Embedded  Resources
            //xmlDoc.Load((Server.MapPath("/q_admin/ebayMaster/Online/Xml/GeteBayOfficialTimeRequest.xml")));

            ////Set the various node values   attr1858_26443
            //xmlDoc["GeteBayOfficialTimeRequest"]["RequesterCredentials"]["eBayAuthToken"].InnerText = userToken;

            string sendXml = string.Format(@"<?xml version=""1.0"" encoding=""utf-8""?>
<GetOrdersRequest xmlns=""urn:ebay:apis:eBLBaseComponents"">
  <RequesterCredentials>
    <eBayAuthToken>{0}</eBayAuthToken>
  </RequesterCredentials>
  <OrderIDArray>
        <OrderID>{1}</OrderID>
  </OrderIDArray>
  <OutputSelector>{2}</OutputSelector>
  <OrderRole>Seller</OrderRole>
  <OrderStatus>All</OrderStatus>
</GetOrdersRequest>
", userToken, OrderID, "Item.Description,Item.BuyItNowPrice");

            xmlDoc.LoadXml(sendXml);
            //Get XML into a string for use in encoding
            string xmlText = xmlDoc.InnerXml;
            //eBay.Service.Call.AddItemCall aic = new eBay.Service.Call.AddItemCall();


            //Put the data into a UTF8 encoded  byte array
            UTF8Encoding encoding = new UTF8Encoding();
            int dataLen = encoding.GetByteCount(xmlText);
            byte[] utf8Bytes = new byte[dataLen];
            Encoding.UTF8.GetBytes(xmlText, 0, xmlText.Length, utf8Bytes, 0);
            #endregion

            #region Setup The Request (inc. HTTP Headers
            //Create a new HttpWebRequest object for the ServerUrl
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serverUrl);

            //Set Request Method (POST) and Content Type (text/xml)
            request.Method = "POST";
            request.ContentType = "text/xml";
            request.ContentLength = utf8Bytes.Length;

            //Add the Keys to the HTTP Headers
            request.Headers.Add("X-EBAY-API-DEV-NAME: " + devID);
            request.Headers.Add("X-EBAY-API-APP-NAME: " + appID);
            request.Headers.Add("X-EBAY-API-CERT-NAME: " + certID);

            //Add Compatability Level to HTTP Headers
            //Regulates versioning of the XML interface for the API
            request.Headers.Add("X-EBAY-API-COMPATIBILITY-LEVEL: 735");

            //Add function name, SiteID and Detail Level to HTTP Headers
            request.Headers.Add("X-EBAY-API-CALL-NAME:GetOrders");
            request.Headers.Add("X-EBAY-API-SITEID: " + siteID.ToString());

            //Time out = 15 seconds,  set to -1 for no timeout.
            //If times-out - throws a WebException with the
            //Status property set to WebExceptionStatus.Timeout.
            request.Timeout = 150000;

            #endregion

            #region Send The Request
            Stream str = null;
            try
            {
                //Set the request Stream
                str = request.GetRequestStream();
                //Write the equest to the Request Steam
                str.Write(utf8Bytes, 0, utf8Bytes.Length);
                str.Close();
                //Get response into stream
                WebResponse resp = request.GetResponse();
                str = resp.GetResponseStream();
            }
            catch (WebException wEx)
            {
                //Error has occured whilst requesting
                //Display error message and exit.
                if (wEx.Status == WebExceptionStatus.Timeout)
                    throw new Exception("Request Timed-Out.");
                else
                    throw new Exception(wEx.Message);

                //MessageBox.Show("Press Enter to Continue...\r\n" + wEx.Message);

            }
            #endregion

            #region Process Response
            // Get Response into String
            StreamReader sr = new StreamReader(str);
            string sssss = sr.ReadToEnd();
            xmlDoc.LoadXml(sssss);
            sr.Close();
            str.Close();

            if (!Directory.Exists(BLL.Variable.itemDescriptionPath))
                Directory.CreateDirectory(BLL.Variable.itemDescriptionPath);
            string filefullname = BLL.Variable.itemDescriptionPath + "\\" + OrderID.ToString() + ".xml";
            if (File.Exists(filefullname))
                File.Delete(filefullname);
            System.IO.StreamWriter sw = new System.IO.StreamWriter(filefullname);
            sw.Write(sssss);
            sw.Close();
            sw.Dispose();
            return sssss;
            #endregion
        }

        /// <summary>
        /// 发送当日营业额
        /// </summary>
        void SendTodayTotal()
        {
            try
            {
                SaveCmd(Enums.Cmd.SendTodaySellTotal, Enums.TimerStatus.End);
                nicklu2Entities DB = new nicklu2Entities();
                var ebaySold = DB.tb_order_helper.Where(h =>
                    h.order_date.Value.Year == DateTime.Now.Year
                    && h.order_date.Value.Month == DateTime.Now.Month
                    && h.order_date.Value.Day == DateTime.Now.Day
                    && h.order_source.Value == 3
                    && h.pre_status_serial_no != 6
                    && h.pre_status_serial_no != 6
                    && h.pre_status_serial_no != 5
                    && h.pre_status_serial_no != 5
                    && h.tag.Value == 1
                    ).Select(h => h.grand_total).Sum();
                var webSold = DB.tb_order_helper.Where(h =>
                    (h.order_date.Value.Year == DateTime.Now.Year
                    && h.order_date.Value.Month == DateTime.Now.Month
                    && h.order_date.Value.Day == DateTime.Now.Day
                    && h.order_source.Value != 3
                    && h.pre_status_serial_no != 6
                    && h.pre_status_serial_no != 6
                    && h.pre_status_serial_no != 5
                    && h.pre_status_serial_no != 5
                    && h.tag.Value == 1
                    )).Select(h => h.grand_total).Sum();

                string str = string.Format("{2} sold : web(${0}), ebay(${1});", webSold.ToString(), ebaySold, DateTime.Now.ToString("yyyy-MM-dd"));

                EmailHelper.Send("terryeah@gmail.com", str, str);
            }
            catch
            {
                EmailHelper.Send("wu.th@qq.com", "price total is error.", "price total is error.");
            }
        }

        /// <summary>
        /// 读取线程执行状态
        /// </summary>
        void ReadTimer()
        {
            while (true)
            {
                if (_run)
                {
                    Thread.Sleep(3000);
                    listView1.Invoke(new MethodInvoker(delegate
                    {
                        listView1.Items.Clear();
                        nicklu2Entities DB = new nicklu2Entities();
                        var query = DB.tb_timer.OrderByDescending(p => p.regdate).ToList();

                        foreach (var item in query)
                        {
                            var status = ((Enums.TimerStatus)Enum.Parse(typeof(Enums.TimerStatus), item.Status.ToString())).ToString();
                            Enums.Cmd cmd = Enums.CmdUtil.GetCmd(item.Cmd.Trim().ToLower(), item.CmdContent);
                            ListViewItem li = new ListViewItem(cmd.ToString());
                            li.Tag = item.Cmd;
                            li.SubItems.Add(status);
                            li.SubItems.Add(item.regdate.ToString());
                            listView1.Items.Add(li);
                        }
                        this.btnStop.Enabled = _run;
                        this.btnRun.Enabled = !_run;

                    }));

                    Timing();
                }
            }
        }



        /// <summary>
        /// 已定设的运行命令
        /// </summary>
        void Timing()
        {
            // onsale
            if (DateTime.Now.ToString("HH:mm") == "20:00")
            {
                SaveCmd(Enums.Cmd.ReStoreOnsale, Enums.TimerStatus.Ready);
            }

            // down order
            foreach (var m in Config.DownloadTimes)
            {
                if (DateTime.Now.ToString("HHmm") == m.Substring(0, 4))
                {
                    if (Directory.Exists(Config.WatchPath))
                    {
                        SaveCmd(Enums.Cmd.DownOrder, Enums.TimerStatus.Ready);
                    }
                }
            }
        }

        /// <summary>
        /// 运行子线程
        /// </summary>
        void RunTimer()
        {
            while (true)
            {
                Thread.Sleep(3000);

                if (DateTime.Now.ToString("HH:mm") == "03:20")
                {
                    _run = true; // Run
                }

                if (!_run)
                {
                    continue;
                }

                using (nicklu2Entities DB = new nicklu2Entities())
                {

                    ModifyByUrl(DB);

                    var query = DB.tb_timer.OrderBy(p => p.regdate).ToList();
                    foreach (var item in query)
                    {
                        Enums.Cmd cmd = Enums.CmdUtil.GetCmd(item.Cmd.Trim().ToLower(), item.CmdContent);
                        if (query.Count(p => p.Status.Equals((int)Enums.TimerStatus.Running)) > 0)
                        {

                            continue;
                        }
                        try
                        {
                            switch (cmd)
                            {
                                case Enums.Cmd.DownOrder:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        DownEbayOrder(DB);
                                    }
                                    break;
                                case Enums.Cmd.SendTodaySellTotal:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        SendTodayTotal();
                                    }
                                    break;
                                case Enums.Cmd.DowneBayPrice:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        DownloadEbayPrice();
                                    }
                                    break;
                                case Enums.Cmd.DownSingleOrder:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        DownSingleOrder(DB, int.Parse(item.CmdContent));
                                    }
                                    break;
                                case Enums.Cmd.GeneratePartHtmlFileForApp:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        GeneratePartHtmlFileForApp(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewGeneratePartHtmlFile:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewGeneratePartHtmlFile(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewHomeCateList:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewHomeCateList(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewPartKeyword:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewPartKeyword(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewSiteHomeData:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewSiteHomeData(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewSyslistAndPartlist:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewSyslistAndPartlist(DB);
                                    }
                                    break;
                                case Enums.Cmd.NewSystemKeyword:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        NewSystemKeyword();
                                    }
                                    break;
                                case Enums.Cmd.ReStoreOnsale:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        ReStoreOnsale();
                                    }

                                    break;

                                case Enums.Cmd.ModifyPartEbayPrice:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        ModifyPartEbayPrice(DB);
                                    }
                                    break;
                                case Enums.Cmd.ModifyAllDesc:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        _ebayHelper.ModifyAllDesc(DB, log);
                                        SaveCmd(Enums.Cmd.ModifyAllDesc, Enums.TimerStatus.End);
                                    }
                                    break;
                                case Enums.Cmd.ModifyAllPartDesc:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        _ebayHelper.ModifyAllPartsDesc(DB, log);
                                        SaveCmd(Enums.Cmd.ModifyAllPartDesc, Enums.TimerStatus.End);
                                    }
                                    break;
                                case Enums.Cmd.ModifyAllSystemDesc:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        _ebayHelper.ModifyAllSysDesc(DB, log);
                                        SaveCmd(Enums.Cmd.ModifyAllSystemDesc, Enums.TimerStatus.End);
                                    }
                                    break;
                                case Enums.Cmd.ModifySystemEbayPrice:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        ModifySystemEbayPrice(DB);
                                    }
                                    break;
                                case Enums.Cmd.ModifyEbayPricePartAndSys:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        ModifyEbayPricePartAndSys(DB);
                                    }
                                    break;
                                case Enums.Cmd.ReadPriceFile:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        ReStoreOnsale();

                                        item.Status = (int)Enums.TimerStatus.End;
                                        DB.SaveChanges();
                                        MethodInvoker MethInvo = new MethodInvoker(ReadPriceFile);
                                        BeginInvoke(MethInvo);
                                    }
                                    break;
                                case Enums.Cmd.GenerateSysPartsFile:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        GenerateSysPartsFile(DB);
                                    }
                                    break;
                                case Enums.Cmd.RemovePartQuantityIsZone:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        RemovePartQuantityIsZone(DB);
                                    }
                                    break;
                                case Enums.Cmd.SysToCategory:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        SysToCategory(DB);
                                    }
                                    break;
                                case Enums.Cmd.ForEbayDownOnSaleDetail:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        BLL.DownEbayOnsaleDetail.Do();
                                        item.Status = (int)Enums.TimerStatus.End;
                                        DB.SaveChanges();
                                    }
                                    break;
                                case Enums.Cmd.ChangeWord:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        _changeword.Do();
                                        item.Status = (int)Enums.TimerStatus.End;
                                        DB.SaveChanges();
                                    }
                                    break;
                                case Enums.Cmd.WriteAllProductForSearch:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        BLL.WriteAllProductForSearch.Done(DB);
                                        item.Status = (int)Enums.TimerStatus.End;
                                        DB.SaveChanges();
                                    }
                                    break;
                                case Enums.Cmd.DoneCacheUrl:
                                    if (item.Status == (int)Enums.TimerStatus.Ready)
                                    {
                                        item.Status = (int)Enums.TimerStatus.Running;
                                        DB.SaveChanges();
                                        ModifyByUrl(DB);
                                        item.Status = (int)Enums.TimerStatus.End;
                                        DB.SaveChanges();
                                    }
                                    break;
                                default:

                                    break;
                            }
                        }
                        catch (Exception ex)
                        {
                            log.WriteErrorLog(ex);
                        }
                    }

                    DB.Dispose();
                }
            }
        }

        void ModifyByUrl(nicklu2Entities context)
        {
            var date = DateTime.Now.AddSeconds(-5);
            var query = context.tb_timer_href.Take(10).ToList();//.Where (me=>me.Regdate<date).ToList();
            int index = 0;
            foreach (var item in query)
            {
                index++;
                log.WriteLog(item.Url);
                _order.SetStatus("(" + index.ToString() + "/" + query.Count.ToString() + "): " + item.UrlToken);
                using (WebClient client = new WebClient())
                {
                    client.OpenRead(string.Concat("http://ftp.lucomputers.com", item.UrlToken));
                    client.Dispose();
                }
            }
        }


        void SysToCategory(nicklu2Entities context)
        {
            BLL.SysGoToCategory.Run(context);
            SaveCmd(Enums.Cmd.SysToCategory, Enums.TimerStatus.End);
        }

        void GenerateSysPartsFile(nicklu2Entities context)
        {
            _ebayHelper.WriteSystemParts(context, log);
            SaveCmd(Enums.Cmd.GenerateSysPartsFile, Enums.TimerStatus.End);
        }
        void RemovePartQuantityIsZone(nicklu2Entities context)
        {
            _ebayHelper.RemovePartQuantityIsZone(context, log);
            SaveCmd(Enums.Cmd.RemovePartQuantityIsZone, Enums.TimerStatus.End);
        }

        void ReStoreOnsale()
        {
            try
            {
                SaveCmd(Enums.Cmd.ReStoreOnsale, Enums.TimerStatus.End);
                BLL.WebClientHelper.GetPage("http://ftp.lucomputers.com/ChangeOnSalePriceToProduct.aspx?QioziCommand=update");
            }
            catch (Exception ex)
            {
                log.WriteErrorLog(ex);
            }
        }

        void NewSystemKeyword()
        {
            BLL.SysKeyword.Run();
            BLL.SysKeyword.Run(412);
            BLL.SysKeyword.Run(413);
            BLL.SysKeyword.Run(414);
            SaveCmd(Enums.Cmd.NewSystemKeyword, Enums.TimerStatus.End);
        }

        void NewSyslistAndPartlist(nicklu2Entities context)
        {
            _generateProdInfo.GenerateListData(context);
            _generateProdInfo.DeletePartPriceCanche();
            SaveCmd(Enums.Cmd.NewSyslistAndPartlist, Enums.TimerStatus.End);
        }

        void NewSiteHomeData(nicklu2Entities context)
        {
            _generateHomeData.Run(context);
            SaveCmd(Enums.Cmd.NewSiteHomeData, Enums.TimerStatus.End);
        }

        void NewPartKeyword(nicklu2Entities context)
        {
            _generateProdInfo.PartKey(context, log);
            SaveCmd(Enums.Cmd.NewPartKeyword, Enums.TimerStatus.End);
        }

        void NewHomeCateList(nicklu2Entities context)
        {
            _generateHomeData.GenerateHomeCateFile(context);
            SaveCmd(Enums.Cmd.NewHomeCateList, Enums.TimerStatus.End);
        }

        void NewGeneratePartHtmlFile(nicklu2Entities context)
        {
            _generateHtmlFile.Run(context, log);
            SaveCmd(Enums.Cmd.NewGeneratePartHtmlFile, Enums.TimerStatus.End);
        }

        void GeneratePartHtmlFileForApp(nicklu2Entities context)
        {
            _generateHtmlFile.ForApp51ccoe(context, log);
            SaveCmd(Enums.Cmd.GeneratePartHtmlFileForApp, Enums.TimerStatus.End);
        }
        /// <summary>
        /// 修改零件价格
        /// </summary>
        void ModifyPartEbayPrice(nicklu2Entities context)
        {
            _ebayHelper.ModifyPartEbayPrice(context, log);
            SaveCmd(Enums.Cmd.ModifyPartEbayPrice, Enums.TimerStatus.End);
        }

        void ModifySystemEbayPrice(nicklu2Entities context)
        {
            _ebayHelper.ModifySystemEbayPrice(context, log);
            SaveCmd(Enums.Cmd.ModifySystemEbayPrice, Enums.TimerStatus.End);
        }

        void ModifyEbayPricePartAndSys(nicklu2Entities context)
        {
            ModifyPartEbayPrice(context);
            ModifySystemEbayPrice(context);
            SaveCmd(Enums.Cmd.ModifyEbayPricePartAndSys, Enums.TimerStatus.End);
        }

        void ReadPriceFile()
        {
            ProcessStartInfo pro = new ProcessStartInfo();
            pro.FileName = @"C:\Program Files (x86)\LUComputer\WatchPrice\LUComputers.exe";
            Process.Start(pro);
        }

        void DownSingleOrder(nicklu2Entities context, int orderId)
        {
            this._order.Download(context, orderId, log);
            SaveCmd(Enums.Cmd.DownSingleOrder, Enums.TimerStatus.End);
        }
        /// <summary>
        ///  下载eBay订单
        /// </summary>
        void DownEbayOrder(nicklu2Entities context)
        {
            this._order.Download(context, 0, log);
            SaveCmd(Enums.Cmd.DownOrder, Enums.TimerStatus.End);

            #region 更新null的产品名称
            var dt = Config.ExecuteDataTable(@"select sys_tmp_detail from tb_order_product_sys_detail 
order by sys_tmp_detail desc ");

            foreach (DataRow dr in dt.Rows)
            {
                Config.ExecuteDataTable(string.Format(@"update tb_order_product_sys_detail op, tb_product pm set op.product_name = pm.product_name where op.sys_tmp_detail = '{0}' and op.product_serial_no = pm.product_serial_no"
, dr["sys_tmp_detail"].ToString()));
            }

            #endregion
        }

        /// <summary>
        /// 
        /// </summary>
        void DownloadEbayPrice()
        {
            try
            {
                //System.IO.StreamWriter sw22 = new System.IO.StreamWriter("C:\\Workspaces\\WebAdmin\\soft_img\\match_ebay_price.txt", false);
                //System.IO.StreamWriter sw2 = new System.IO.StreamWriter("C:\\Workspaces\\Web\\soft_img\\match_ebay_price.txt", false);
                //sw2.WriteLine(DateTime.Now.ToString() + " Runing");
                //sw2.Close();
                //sw22.WriteLine(DateTime.Now.ToString() + " Runing");
                //sw22.Close();

                DownloadEbayPartPrice.RunStatus = true;
                string storePath = BLL.Variable.ebayPartPricePath;
                if (!Directory.Exists(storePath))
                {
                    Directory.CreateDirectory(storePath);
                }
                _depp.GetMySelling(1, storePath);
                _depp.ReadPage(storePath);
                _depp.UpdateDB();


                // 关闭不在线的系统
                Config.ExecuteNonQuery(@"update tb_ebay_system set showit=0;
update tb_ebay_system set showit=1 where id in (select sys_sku from tb_ebay_selling where sys_sku>0);");

                DownloadEbayPartPrice.RunStatus = false;
                System.IO.StreamWriter sw = new System.IO.StreamWriter("C:\\Workspaces\\Web\\soft_img\\match_ebay_price.txt", false);
                System.IO.StreamWriter sw222 = new System.IO.StreamWriter("C:\\Workspaces\\WebAdmin\\soft_img\\match_ebay_price.txt", false);
                sw.WriteLine(DateTime.Now.ToString() + " OK");
                sw.Close();
                sw222.WriteLine(DateTime.Now.ToString() + " OK");
                sw222.Close();

                EmailHelper.SendCheckDupl();
                // button3_Click(null, null);
            }
            catch (Exception e)
            {
                log.WriteErrorLog(e);
            }
            SaveCmd(Enums.Cmd.DowneBayPrice, Enums.TimerStatus.End);
            // SaveCmd(Enums.Cmd.ForEbayDownOnSaleDetail, Enums.TimerStatus.Ready);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void buttonDownloadEbayPrice_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.DowneBayPrice, Enums.TimerStatus.Ready);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonGeneratePartHtmlFile_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //BLL.GeneratePartHtmlFile.OldSite(DB, log);
            //this.Cursor = Cursors.Default;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.GeneratePartHtmlFileForApp, Enums.TimerStatus.Ready);
        }

        #region generate new code.

        private void button5_Click(object sender, EventArgs e)
        {
            nicklu2Entities DB = new nicklu2Entities();
            this.Cursor = Cursors.WaitCursor;
            for (int i = 0; i < 30000; i++)
            {
                //tb_store_order_code m = tb_store_order_code.Createtb_store_order_code(0);
                var m = new tb_store_order_code();
                m.OrderCode = NewOrderCode(DB);
                //DB.AddTotb_store_order_code(m);
                DB.tb_store_order_code.Add(m);
                DB.SaveChanges();
            }
            this.Cursor = Cursors.Default;
            this.button5.Text = DateTime.Now.ToString();
        }

        public int NewOrderCode(nicklu2Entities context)
        {
            int newcode = SixCode;
            var oldcode = context.tb_order_helper.FirstOrDefault(p => p.order_code.HasValue && p.order_code.Value.Equals(newcode));

            if (oldcode != null)
                return NewOrderCode(context);
            else
            {
                var newoldCode = context.tb_store_order_code.FirstOrDefault(p => p.OrderCode.HasValue && p.OrderCode.Value.Equals(newcode));
                if (newoldCode != null)
                    return NewOrderCode(context);

                return newcode;
            }
        }
        public static int SixCode
        {
            get
            {
                System.Threading.Thread.Sleep(10);
                Random rnd = new Random();
                return rnd.Next(100000, 999999);
            }
        }
        public static int EightCode
        {
            get
            {
                System.Threading.Thread.Sleep(10);
                Random rnd = new Random();
                return rnd.Next(10000000, 99999999);
            }
        }
        #endregion

        /// <summary>
        /// 生成零件界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.NewGeneratePartHtmlFile, Enums.TimerStatus.Ready);
        }

        private void btnSysToCategory_Click(object sender, EventArgs e)
        {
            //this.Cursor = Cursors.WaitCursor;
            //BLL.SysGoToCategory.Run(DB);
            //this.Cursor = Cursors.Default;

            //var btn = sender as Button;
            //btn.Text = DateTime.Now.ToString();
            SaveCmd(Enums.Cmd.SysToCategory, Enums.TimerStatus.Ready);
        }

        private void btnPartKeyword_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.NewPartKeyword, Enums.TimerStatus.Ready);
        }

        private void btnUpdateEbayPrice_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ModifyPartEbayPrice, Enums.TimerStatus.Ready);
        }

        private void btnUpdateSystemPrice_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(ModifySystemEbayPrice);
            //t.Start();
            SaveCmd(Enums.Cmd.ModifySystemEbayPrice, Enums.TimerStatus.Ready);
        }

        private void btnGenerateSysFile_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(WriteSystemParts);
            //t.Start();
            SaveCmd(Enums.Cmd.GenerateSysPartsFile, Enums.TimerStatus.Ready);
        }

        private void btnPartStock_Click(object sender, EventArgs e)
        {
            //Thread t = new Thread(RemovePartQuantityIsZone);
            //t.Start();
            SaveCmd(Enums.Cmd.RemovePartQuantityIsZone, Enums.TimerStatus.Ready);
        }

        //void RemovePartQuantityIsZone()
        //{

        //}

        private void btnDownOrder_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.DownOrder, Enums.TimerStatus.Ready);
        }


        /// <summary>
        /// 生成新网站首页界面数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateNewSiteHomeData_Click(object sender, EventArgs e)
        {
            //BLL.GenerateNewSiteHomeData.Run(DB);
            SaveCmd(Enums.Cmd.NewSiteHomeData, Enums.TimerStatus.Ready);
        }

        private void btnSysKeyword_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.NewSystemKeyword, Enums.TimerStatus.Ready);
        }

        private void btnNewSiteHomtCateInfo_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.NewHomeCateList, Enums.TimerStatus.Ready);
        }

        private void btnGenerateListInfo_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.NewSyslistAndPartlist, Enums.TimerStatus.Ready);
        }

        private void btnSendTodayTotal_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.SendTodaySellTotal, Enums.TimerStatus.Ready);
        }

        /// <summary>
        /// 保存命令状态
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="status"></param>
        void SaveCmd(Enums.Cmd cmd, Enums.TimerStatus status)
        {
            nicklu2Entities DB = new nicklu2Entities();
            var cmdStr = ((int)cmd).ToString();
            tb_timer t = DB.tb_timer.FirstOrDefault(p => p.Cmd.Equals(cmdStr));
            if (t == null)
            {
                t = new tb_timer
                {
                    Cmd = cmdStr,
                    CmdContent = cmdStr.ToString(),
                    regdate = DateTime.Now,
                    Status = (int)status
                };
                DB.tb_timer.Add(t);
            }
            else
            {
                // if (status != Enums.TimerStatus.Running)
                {
                    t.regdate = DateTime.Now;
                    t.Status = (int)status;
                }
            }
            DB.SaveChanges();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            _run = false;
            Process.GetCurrentProcess().Kill();
            Application.Exit();
        }

        private void btnWatchPriceApp_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ReadPriceFile, Enums.TimerStatus.Ready);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _run = false;
            this.btnRun.Enabled = !_run;
            this.btnStop.Enabled = _run;
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            _run = true;
            this.btnRun.Enabled = !_run;
            this.btnStop.Enabled = _run;
        }

        private void btnDownEbayOnSale_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ForEbayDownOnSaleDetail, Enums.TimerStatus.Ready);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonChangeWordWin_Click(object sender, EventArgs e)
        {
            frmChangeKeyword frm = new frmChangeKeyword();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.Show();
        }

        private void buttonChangeWord_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ChangeWord, Enums.TimerStatus.Ready);
        }

        private void buttonChangeWatchStatus_Click(object sender, EventArgs e)
        {
            var filename = "C:\\Program Files (x86)\\LUComputer\\WatchPrice\\LUComputers.exe.config";
            var fileContent = File.ReadAllText(filename);
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            sw.Write(fileContent.Replace("<add key=\"AutoRun\" value=\"qiozi\" />", "<add key=\"AutoRun\" value=\"qqiozi\" />"));
            sw.Close();
            sw.Dispose();
            MessageBox.Show("OK");
        }

        private void buttonChangeWatchStatus1_Click(object sender, EventArgs e)
        {
            var filename = "C:\\Program Files (x86)\\LUComputer\\WatchPrice\\LUComputers.exe.config";
            var fileContent = File.ReadAllText(filename);
            StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
            sw.Write(fileContent.Replace("<add key=\"AutoRun\" value=\"qqiozi\" />", "<add key=\"AutoRun\" value=\"qiozi\" />"));
            sw.Close();
            sw.Dispose();
            MessageBox.Show("OK");
        }

        private void buttonGenerateAllPartForSearch_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.WriteAllProductForSearch, Enums.TimerStatus.Ready);
        }

        private void buttonOrderAddTest_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                nicklu2Entities context = new nicklu2Entities();
                //MessageBox.Show(openFileDialog1.FileName);
                this._order.DownloadTest(context, 22450, log, openFileDialog1.FileName);
            }
        }

        private void buttonModifyAllDesc_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ModifyAllDesc, Enums.TimerStatus.Ready);
        }

        private void buttonModifyPartsDesc_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ModifyAllPartDesc, Enums.TimerStatus.Ready);
        }

        private void buttonModifySystemDesc_Click(object sender, EventArgs e)
        {
            SaveCmd(Enums.Cmd.ModifyAllSystemDesc, Enums.TimerStatus.Ready);
        }
    }

    public class SysInfo
    {
        public SysInfo() { }

        public int SysSKU { get; set; }
        public string eBayId { get; set; }
        public decimal eBayPrice { get; set; }
        public string eBayTitle { get; set; }
        public string Price { get; set; }
        public string Discount { get; set; }
        public string Sold { get; set; }
    }

    public class ebayModifyPriceItem
    {
        public int LuSku { get; set; }

        public string eBayItemId { get; set; }

        public decimal Cost { get; set; }

        public int Qty { get; set; }

        public decimal OldSold { get; set; }

        public decimal NewSold { get; set; }

        public string Type { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum eBayModifyPriceType
    {
        None,
        Normal,
        Modify,
        Delete
    }

    public class PriceItem
    {
        public decimal ebayPrice { get; set; }

        public decimal shipping_fee { get; set; }

        public decimal profit { get; set; }

        public decimal ebay_fee { get; set; }
    }

    public class SystemPriceItem
    {
        //        [{            
        //            'ebayPrice':'563.99'
        //            ,'shipping_fee':'34.90'
        //            ,'profit':'31.91'
        //            ,'ebay_fee':'41.44'
        //            ,'warn':'False'
        //            ,'cost':'455.87'
        //}]
        public decimal ebayPrice { get; set; }
        public decimal shipping_fee { get; set; }
        public decimal profit { get; set; }
        public decimal ebay_fee { get; set; }
        public decimal cost { get; set; }
        public bool warn { get; set; }
    }
}
