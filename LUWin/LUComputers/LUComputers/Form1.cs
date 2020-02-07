using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Collections;
using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using LUComputers.DBProvider;


namespace LUComputers
{
    public partial class Form1 : Form
    {
        #region 通知主线程关闭程序
        static bool AutoIsCloseWin = false;

        public delegate void CallBackDelegate(string message);

        CallBackDelegate cbd = MainCloseWin;

        private static void MainCloseWin(string message)
        {
            // synnex onsale 
            Watch.Synnex.OnSale();

            #region 更新价格命令
            Config.RemoteExecuteNonQuery(@"
delete from tb_timer where cmd='13'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '13', now());
delete from tb_timer where cmd='14'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '14', now()); 


delete from tb_timer where cmd='5'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '5', now()); 

delete from tb_timer where cmd='6'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '6', now()); 

delete from tb_timer where cmd='8'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '8', now()); 

delete from tb_timer where cmd='9'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '9', now()); 



");
            // 更新所有产品描述
            if (DateTime.Now.DayOfWeek == DayOfWeek.Monday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Wednesday ||
                DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            {
                Config.RemoteExecuteNonQuery(@"
delete from tb_timer where cmd='25'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '25', now());");
            }

            Thread.Sleep(1000);
            Config.RemoteExecuteNonQuery(@"delete from tb_timer where cmd='3'; 
insert tb_timer 
	( CmdContent, Cmd, regdate)
	values
	( '', '3', now()); ");
            #endregion

            Application.Exit();
        }
        #endregion

        public static bool RunASIDandhSynnex = System.Configuration.ConfigurationManager.AppSettings["AutoRun"].ToString() == "qiozi";

        static bool EndAsi = false;
        static bool EndDandh = false;
        static bool EndSynnex = false;
        static bool EndEprom = false;

        static ArrayList al = new ArrayList();

        /// <summary>
        /// 
        /// </summary>
        string cost_file_path
        {
            get { return Config.cost_file_path; }
        }
        /// <summary>
        /// 
        /// </summary>
        string _canadaComputerPath
        {
            get
            {
                return cost_file_path + "PriceFile\\Canadacomputer\\";
            }
        }

        public string _shopbotPath
        {
            get { return cost_file_path + "PriceFile\\Shopbot\\"; }
        }

        bool Run_Watch = true;
        string LogsErrorFilename = DateTime.Now.ToString("yyyyMMdd") + "Error.Log";
        string CHANGE_STOCK_URL = Config.http_url + "q_admin/netcmd/changepartprice.aspx?qiozicommand=qiozi@msn.com";
        //string CHANGE_EXPORT_FILE_URL = Config.http_url + "q_admin/sale_product_export_file.aspx?menu_id=2&cmd=9867054";
        //const string SEND_EMAIL_TO_BESON_URL = "http://manager.lucomputers.com/q_admin/netcmd/sendemailtobenson.aspx?qiozicommand=qiozi@msn.com";


        // Watch.Supercom Supercom = new LUComputers.Watch.Supercom();
       
        Watch.ASI ASI = new LUComputers.Watch.ASI();
        Helper.SaveNewMatch SNM = new LUComputers.Helper.SaveNewMatch();
        Watch.Synnex synnex = new LUComputers.Watch.Synnex();
        Watch.Eprom eprom = new LUComputers.Watch.Eprom();
        Watch.UpdateStock UpdateServer = new Watch.UpdateStock();
        //  Watch.CanadaComputer canadaComputers = new Watch.CanadaComputer();

        LtdHelper LH = new LtdHelper();
        ValidLuSku validLUC = new ValidLuSku();

        public Form1()
        {
            InitializeComponent();
        }


        public Form1(string[] args)
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            string watchFolderName = AppDomain.CurrentDomain.BaseDirectory + "\\CostFile";
            if (!Directory.Exists(watchFolderName))
            {
                Directory.CreateDirectory(watchFolderName);
                Thread.Sleep(100);
            }

            if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");

            fileSystemWatcher1.Path = AppDomain.CurrentDomain.BaseDirectory + "\\Logs";

            fileSystemWatcherLtdFile.Path = watchFolderName;

            ASI.WatchE += new LUComputers.Events.UrlEventHandler(etc_WatchE);
            synnex.WatchE += new LUComputers.Events.UrlEventHandler(etc_WatchE);
            eprom.WatchE += new LUComputers.Events.UrlEventHandler(etc_WatchE);
            validLUC.WatchE += new LUComputers.Events.UrlEventHandler(etc_WatchE);
            UpdateServer.WatchE += new LUComputers.Events.UrlEventHandler(etc_WatchE);

            LoadThread();

            if (RunASIDandhSynnex)
            {
                toolStripButton7_Click(null, null);
            }
        }
        /// <summary>
        /// 显示执行过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void etc_WatchE(object sender, LUComputers.Events.UrlEventArgs e)
        {
            this.listBox1.Invoke(new MethodInvoker(delegate
            {
                if (e._urlEventModel.url != null)
                {
                    SetListBox(this.listBox1, e._urlEventModel.url);
                    Helper.Logs.WriteLog(e._urlEventModel.url);
                    Uri u = new Uri(string.Format(@"{0}", e._urlEventModel.url));
                    this.webBrowser1.Url = u;
                }
                if (e._urlEventModel.comment != null)
                    SetListBox(this.listBox1, e._urlEventModel.ltd.ToString() + ":::" + e._urlEventModel.comment);
                if (e._urlEventModel.result != null)
                    this.textBox_result.Text += e._urlEventModel.ltd.ToString() + ":::" + e._urlEventModel.result + "\r\n";
            }));

        }

        #region Threads
        //Thread threadASI = null;
        //Thread threadD2A = null;
        //Thread threadEprom = null;
        //Thread threadSynnex = null;
        //Thread threadMMAX = null;
        //Thread threadSuppromAll = null;
        Thread ThreadRun = null;

        void LoadThread()
        {
            ThreadRun = new System.Threading.Thread(WorkDone);
            ThreadRun.IsBackground = true;
            ThreadRun.Start(cbd);
        }

        void WorkDone(object o)
        {
            while (!AutoIsCloseWin)
            {
                if (RunASIDandhSynnex)
                {
                    if (EndAsi && EndDandh && EndSynnex && EndEprom)
                    {
                        EndAsi = false;
                        EndDandh = false;
                        this.CompareAndUpdate();
                    }
                }

                Thread.Sleep(1000);
            }

            CallBackDelegate cdb = o as CallBackDelegate;
            cdb("close");
        }

        #endregion

        #region toolScriptMenuItem
        private void LoadLtdMenuItem()
        {
            LH = new LtdHelper();
            DataTable dt = LH.LtdHelperValidToDataTable();

            ToolStripItem[] tsis = new ToolStripItem[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];

                ToolStripMenuItem tsmi = new ToolStripMenuItem();
                tsmi.Name = dr["id"].ToString();
                tsmi.Text = dr["text"].ToString();
                tsmi.Click += new EventHandler(tsmi_Click);

                tsis[i] = tsmi;
            }
            //this.optionsToolStripMenuItem.DropDownItems.AddRange(tsis);
        }

        void tsmi_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            MessageBox.Show(tsmi.Name);
        }

        #endregion

        #region RunInc
        private void RunIncFace(Ltd ltd)
        {

        }
        #endregion

        private void cCloseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void demoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Demo d = new Demo();
            d.Show();

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MDIParent1 m = new MDIParent1();
            m.WindowState = FormWindowState.Maximized;
            m.Show();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ConvertGallery cg = new ConvertGallery();
            cg.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Helper.ProcessHelper.SubControlCurrentValue > Helper.ProcessHelper.SubControlMaxSum)
                    Helper.ProcessHelper.SubControlMaxSum = Helper.ProcessHelper.SubControlCurrentValue;

                if (Helper.ProcessHelper.ParentControlCurrentValue > Helper.ProcessHelper.ParentControlMaxSum)
                    Helper.ProcessHelper.ParentControlMaxSum = Helper.ProcessHelper.ParentControlCurrentValue;

            }
            catch { }

            this.toolStripStatusLabel1.Text = DateTime.Now.ToString();
            this.toolStripStatusLabel1.Width = 300;
        }

        bool _is_run_ok = true;
        public bool IS_RUN_OK
        {
            get { return _is_run_ok; }
            set { _is_run_ok = value; }
        }

        #region 

        private void timer_com_click(object sender, EventArgs e)
        {
            if (al != null)
            {
                //this.richTextBox1.Text += " yes \r\n";
                for (int i = 0; i < al.Count; i++)
                {
                    Helper.ProcessHelper.SubControlCurrentValue = al.Count - i;
                    if (i == al.Count - 1)
                    {
                        Helper.ProcessHelper.ParentControlCurrentValue = 6;

                        Config.ExecuteNonQuery(string.Format("update tb_other_inc set last_run_date=now() where id='{0}'", new LtdHelper().LtdHelperValue(Ltd.Rival_Ncix)));

                        this.webBrowser1.Url = new Uri(Config.http_url + "/q_admin/netcmd/changepartprice.aspx?qiozicommand=qiozi@msn.com");

                        this.timer_com.Enabled = false;
                    }

                    this.webBrowser1.Url = new Uri(al[i].ToString());

                    al.RemoveAt(i);
                    this.timer_com.Enabled = false;
                    break;
                }
                //al = null;
            }
            else
            {

            }
        }

        #endregion

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Uri u = new Uri(this.toolStripTextBox1.Text);
            webBrowser1.Url = u;
        }

        public void SetListBox(ListBox listBox1, string str)
        {
            try
            {
                listBox1.Items.Add(DateTime.Now.ToString() + ":::::" + str);

                listBox1.SelectedItem = listBox1.Items[listBox1.Items.Count - 1];

                listBox1.Update();
            }
            catch { }
        }

        /// <summary>
        /// 加载远程数据
        /// </summary>
        void loadRemoteSKU()
        {
            var sql = string.Empty;
            DataTable dt = Config.RemoteExecuteDateTable("select distinct lu_sku, other_inc_sku, other_inc_type, prodType from tb_other_inc_match_lu_sku");
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sb.Append("," + string.Format("('{0}','{1}','{2}','{3}')"
                    , dt.Rows[i]["lu_sku"].ToString()
                    , dt.Rows[i]["other_inc_sku"].ToString()
                    , dt.Rows[i]["other_inc_type"].ToString()
                    , dt.Rows[i]["prodType"].ToString()));
            }
            if (sb.ToString().Length > 10)
            {
                sql = sb.ToString().Substring(1);

                if (sql.Length > 100)
                {
                    Config.ExecuteNonQuery(string.Format("delete from tb_other_inc_match_lu_sku;"));
                    Watch.MatchStore MS = new LUComputers.Watch.MatchStore();
                    string table_name = MS.CreateTable();
                    string sql1 = @"Insert into tb_other_inc_match_lu_sku
(lu_sku, other_inc_sku, other_inc_type, prodType) values " + sql;
                    Config.ExecuteNonQuery(sql1);

                    string sql2 = @"Insert into " + table_name + @"
(lu_sku, other_inc_sku, other_inc_type, prodType) values " + sql;
                    Config.ExecuteNonQuery(sql2);
                }

                //
                // load valid lu sku 每隔5天执行一次
                //

                if (Config.ExecuteScalarInt("select count(*) from tb_other_inc_valid_lu_sku") == 0 || DateTime.Now.Day % Config.restart_day == 0)
                {
                    SetListBox(this.listBox1, "load valid part form web site.");

                    DataTable dt2 = Config.RemoteExecuteDateTable(string.Format(@"
delete from tb_other_inc_valid_lu_sku;
insert into tb_other_inc_valid_lu_sku 
	( lu_sku, manufacturer_part_number, is_valid, is_ncix_remain, price, cost, discount, ltd_stock, menu_child_serial_no, brand,adjustment, prodType)
select product_serial_no, manufacturer_part_number, 1, 0, product_current_price
, product_current_cost, product_current_discount, ltd_stock, menu_child_serial_no, producter_serial_no,adjustment, prodType from tb_product 
where tag=1 and is_non=0 and split_line=0 and manufacturer_part_number<>''
 and manufacturer_part_number<>'' and manufacturer_part_number <> 'NULL' 
 and menu_child_serial_no in ({0}) and menu_child_serial_no not in ({1});
select lu_sku product_serial_no, manufacturer_part_number, price, cost, discount, ltd_stock, menu_child_serial_no, brand,adjustment, prodType from tb_other_inc_valid_lu_sku;
",
 string.Join(",", new ProdCategoryHelper().GetAllValidCategory()),
 string.Join(",", new LUComputers.DBProvider.ProdCategoryHelper().NotWatchCategoryIds())));
                    sb = new System.Text.StringBuilder();
                    for (int i = 0; i < dt2.Rows.Count; i++)
                    {
                        sb.Append("," + string.Format(" ({0},'{1}','{2}','{3}','{4}','{5}', '{6}', '{7}', '{8}', '{9}')"
                             , dt2.Rows[i]["product_serial_no"].ToString()
                             , dt2.Rows[i]["manufacturer_part_number"].ToString()
                             , dt2.Rows[i]["price"].ToString()
                             , dt2.Rows[i]["cost"].ToString()
                             , dt2.Rows[i]["discount"].ToString()
                             , dt2.Rows[i]["ltd_stock"].ToString()
                             , dt2.Rows[i]["menu_child_serial_no"].ToString()
                             , dt2.Rows[i]["brand"].ToString()
                             , dt2.Rows[i]["adjustment"].ToString()
                             , dt2.Rows[i]["prodType"].ToString()
                             ));
                    }

                    if (sb.ToString().Length > 0)
                    {
                        sql = sb.ToString().Substring(1) + ";";

                        Config.ExecuteNonQuery(string.Format("delete from tb_other_inc_valid_lu_sku;"));
                        string sql1 = @"insert into tb_other_inc_valid_lu_sku 
	(lu_sku, manufacturer_part_number, price, 
	cost, 
	discount, 
	ltd_stock,
    menu_child_serial_no,
    brand
    ,adjustment
    ,prodType
	) values " + sql;
                        Watch.LU WL = new LUComputers.Watch.LU();
                        string table_name = WL.CreateTable();
                        string sql2 = string.Format(@"insert into {0} 
	(luc_sku, manufacturer_part_number, price, 
	cost, 
	discount, 
	ltd_stock,
    menu_child_serial_no,
    brand
    ,adjustment
   , prodType
	) values ", table_name) + sql;
                        Config.ExecuteNonQuery(sql1);
                        Config.ExecuteNonQuery(sql2);

                    }


                    SetListBox(this.listBox1, "stat total.");

                    WebClient client = new WebClient();

                    var u = new Uri(Config.http_url + "q_admin/netcmd/stattotal.aspx?Cmd=Qiozi@msn.com&t=" + DateTime.Now.ToString());
                    client.DownloadString(u);


                    // 执行完数据加载
                    if (RunASIDandhSynnex)
                    {
                        toolStripButton6_Click(null, null);
                    }
                }
            }
        }


        /// <summary>
        /// 从远程加载数据
        /// </summary>
        public void LoadFromLUSite()
        {
            try
            {
                new Watch.LU().LoadPartPriceSettings();

                Config.ExecuteNonQuery("delete from tb_other_inc_valid_lu_sku");

                Config.ExecuteNonQuery("delete from tb_dont_update");

                DataTable dt = Config.RemoteExecuteDateTable("Select luc_sku from tb_part_not_change_price");

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    Config.ExecuteNonQuery("insert into tb_dont_update(luc_sku) values ('" + dr[0].ToString() + "');");
                    validLUC.SetStatus(null, "load not change sku:" + dr[0].ToString());
                }

                validLUC.SetStatus(null, "load Match LU SKU form web site.");
                //Uri u = new Uri(Config.http_url + "q_admin/netcmd/export_valid_lu_sku_and_manufacture.aspx?cmd=qiozi@msn.com&cmdtype=exportMatchSku&t=" + DateTime.Now.ToString());
                //this.webBrowser1.Url = u;

                // 从远程加载数据

                this.loadRemoteSKU();

                return;
            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
                validLUC.SetStatus(ex.Message);
            }
        }

        private void toolStripButton_SendMail_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure?", "Send email.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                StreamReader sr = new StreamReader(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html"));
                string str = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();

                Helper.EmailHelper EH = new LUComputers.Helper.EmailHelper();
                if (EH.SendToEmail("terryeah@gmail.com", str, "LUCOMPUTERS WEB DATE UPDATES"))
                {
                    MessageBox.Show("Mail is send.");
                }
                else
                {
                    MessageBox.Show("Mail Send is faild.");
                }
            }
        }

        private void viewDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ltd_View_Detail lvd = new Ltd_View_Detail();
            lvd.Show();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// 启动线程 从远程加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LUToolStripMenuItemWatch_Click(object sender, EventArgs e)
        {
            Thread thread = new System.Threading.Thread(LoadFromLUSite);
            thread.IsBackground = true;
            thread.Start();
        }

        private void LUToolStripMenuItemCompare_Click(object sender, EventArgs e)
        {
            try
            {
                Helper.Compare.ViewCompare(Ltd.lu);
                this.UpdateServer.SetStatus(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html"), "Compare Supercom end.");
            }
            catch { }
        }

        private void d2aToolStripMenuItemWatch_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(WatchD2A);
            t.Start();
        }
        /// <summary>
        /// d2a
        /// </summary>
        void WatchD2A()
        {
            try
            {
                string filepath = cost_file_path + "\\D2A.xls";
                if (File.Exists(filepath))
                {
                    List<D2aModel> list = ToDT(filepath);
                    File.WriteAllText("C:\\Workspaces\\PriceStore\\test.json", Newtonsoft.Json.JsonConvert.SerializeObject(list), Encoding.UTF8);

                    ReadD2a(list);

                    eprom.ViewCompare(Ltd.wholesaler_d2a);
                    UpdateLtdInfoToRemote(Ltd.wholesaler_d2a);

                    // 只保留 CPU ，内存与win, office.
                    Config.RemoteExecuteNonQuery(@"select *from tb_other_inc_part_info where other_inc_id=17 and luc_sku not in 
(Select Product_Serial_no from tb_product where menu_child_serial_no in (22,29,118, 119,120));");
                    
                    eprom.SetStatus(null, null, Ltd.wholesaler_d2a, "End.");
                }
                else
                    throw new Exception("D2A file isn't exist.");
            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
            }
            RunTimer.WatcherInfos.D2A.end = true;
            RunTimer.WatcherInfos.D2A.begin = false;
            RunTimer.WatcherInfos.D2A.running = false;
        }
        /// <summary>
        /// d2a
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        List<D2aModel> ToDT(string filename)
        {
            List<D2aModel> list = new List<D2aModel>();
            FileStream sr = new FileStream(filename, FileMode.Open, FileAccess.Read);
            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(sr);

            //获取excel的第一个sheet
            ISheet sheet = workbook.GetSheet("PriceList");

            //获取sheet的首行
            IRow headerRow = sheet.GetRow(17);

            //一行最后一个方格的编号 即总的列数
            int cellCount = headerRow.LastCellNum;

            //for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            //{
            //    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue ?? "");
            //    table.Columns.Add(column);
            //}
            //最后一列的标号  即总的行数
            int rowCount = sheet.LastRowNum;

            for (int i = 16; i < sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                // sheet.GetRow(0).GetCell(0).is
                if (row == null) continue;
                if (row.GetCell(1) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(1).ToString();
                    decimal cost1;
                    decimal.TryParse(row.GetCell(3).ToString().Replace("$", ""), out cost1);
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
                if (row.GetCell(5) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(5).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(7).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }

                if (row.GetCell(9) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(9).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(11).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }

                if (row.GetCell(17) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(17).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(19).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
                if (row.GetCell(21) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(21).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(23).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
                if (row.GetCell(25) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(25).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(27).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
                if (row.GetCell(29) != null)
                {
                    D2aModel m1 = new D2aModel();
                    m1.mfp = row.GetCell(29).ToString();
                    decimal cost1 = 0M;
                    try
                    {
                        decimal.TryParse(row.GetCell(31).ToString().Replace("$", ""), out cost1);
                    }
                    catch { }
                    m1.cost = cost1;
                    m1.stock = 5;
                    if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
                        list.Add(m1);
                }
            }
            //ISheet sheet2 = workbook.GetSheetAt(1);

            //for (int i = 11; i < sheet2.LastRowNum; i++)
            //{
            //    IRow row = sheet2.GetRow(i);
            //    // sheet.GetRow(0).GetCell(0).is
            //    if (row == null) continue;
            //    if (row.GetCell(2) != null)
            //    {
            //        D2aModel m1 = new D2aModel();
            //        m1.mfp = row.GetCell(2).ToString();
            //        decimal cost1 = 0;
            //        try
            //        {
            //            decimal.TryParse(row.GetCell(3).ToString(), out cost1);
            //        }
            //        catch { }
            //        m1.cost = cost1;
            //        m1.stock = 5;
            //        if (!string.IsNullOrEmpty(m1.mfp) && cost1 != 0)
            //            list.Add(m1);
            //    }

            //}

            workbook = null;
            sheet = null;
            //sheet2 = null;
            sr.Close();
            sr.Dispose();
            return list;
        }

        /// <summary>
        /// d2a 
        /// </summary>
        /// <param name="list"></param>
        void ReadD2a(List<D2aModel> list)
        {
            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_d2a);

            DataTable luSkuDT = Config.ExecuteDateTable(string.Format("select lu_sku,manufacturer_part_number from tb_other_inc_valid_lu_sku where prodType='{0}' ", "NEW"));

            string table_name = new Watch.Eprom().CreateTable(Ltd.wholesaler_d2a);
            {
                for (int i = 0; i < list.Count; i++)
                {
                    int luc_sku = Watch.LU.GetSKUByltdSku(list[i].mfp, ltd_id);

                    if (luc_sku == 0)
                    {
                        luc_sku = Watch.LU.GetSKUByMfp(list[i].mfp, luSkuDT);

                        if (luc_sku != 0)
                        {
                            Config.ExecuteDateTable(string.Format(@"insert into tb_other_inc_match_lu_sku (lu_sku , other_inc_sku, other_inc_type) values 
                                        ('{0}', '{1}', '{2}')", luc_sku, list[i].mfp, ltd_id));
                        }
                    }

                    string stock_str = list[i].stock.ToString();
                    if (stock_str != "")
                    {

                        Config.ExecuteNonQuery(string.Format(@"insert into {0} (part_sku, part_cost, store_quantity, mfp, part_name, luc_sku) values 
                    ( '{1}', '{2}', '{3}', '{4}', '{5}', '{6}')", table_name
                                                             , list[i].mfp
                                                             , list[i].cost
                                                             , list[i].stock
                                                             , list[i].mfp
                                                             , list[i].mfp
                                                             , luc_sku));


                    }
                }
            }

            Config.ExecuteNonQuery("delete from tb_other_inc_part_info where other_inc_id='" + ltd_id + "'");

            Config.ExecuteNonQuery(string.Format(@"insert into tb_other_inc_part_info 
	(luc_sku, other_inc_id, other_inc_sku, manufacture_part_number, 
	other_inc_price, 
	other_inc_store_sum, 
	tag, 	 
	last_regdate
	)
select luc_sku, {1}, part_sku, mfp, part_cost, store_quantity, 1, now() from {0}", table_name, ltd_id));

            Helper.SaveNewMatch SNM = new Helper.SaveNewMatch();
            LtdHelper LH = new LtdHelper();
            string table_name2 = LH.GetLastStoreTableNameGroup(Ltd.wholesaler_d2a);
            SNM.UpdateToRemote(Ltd.wholesaler_d2a, table_name2);
        }

        /// <summary>
        /// 上传D2A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void d2aToolStripMenuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_d2a);
        }

        /// <summary>
        /// 比较 D2A
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void d2aToolStripMenuItemCompare_Click(object sender, EventArgs e)
        {
            Helper.Compare.ViewCompare(Ltd.wholesaler_d2a);
        }

        private void mmaxToolStripMenuItemWatch_Click(object sender, EventArgs e)
        {
            //RunTimer.WatcherInfos.MMAX.begin = true;
            Thread t = new Thread(WatchMMAX);
            t.Start();
        }

        void WatchMMAX()
        {
            //if (RunTimer.WatcherInfos.MMAX.begin)
            {
                try
                {
                    string filepath = cost_file_path + "\\MMAX.xls";
                    if (eprom.Run(Ltd.wholesaler_MMAX, filepath))
                    {
                        eprom.ViewCompare(Ltd.wholesaler_MMAX);
                        UpdateLtdInfoToRemote(Ltd.wholesaler_MMAX);
                        eprom.SetStatus(null, null, Ltd.wholesaler_MMAX, "End.");
                    }
                    else
                        throw new Exception("MMAX file isn't exist.");
                }
                catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }
                RunTimer.WatcherInfos.MMAX.begin = false;
                RunTimer.WatcherInfos.MMAX.running = false;
            }
        }

        private void mmaxToolStripMenuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_MMAX);
        }

        private void mmaxToolStripMenuItemCompare_Click(object sender, EventArgs e)
        {
            Helper.Compare.ViewCompare(Ltd.wholesaler_MMAX);
        }

        /// <summary>
        /// 执行asi 所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aSIToolStripMenuItemWatch_Click(object sender, EventArgs e)
        {
            Thread threadASI = new Thread(new ThreadStart(WatchASI));
            threadASI.IsBackground = true;
            threadASI.Start();
        }

        void WatchASI()
        {
            try
            {
                string filepath = cost_file_path + "\\70972.csv";
                string tableName = "";
                if (ASI.Run(filepath, checkBoxSaveAll.Checked, ref tableName))
                {
                    ASI.ViewCompare();

                    string tableNameNoDate = TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_asi.ToString()));

                    string table_name = Find.LastTableName(tableNameNoDate); // new LtdHelper().GetLastStoreTableNameGroup(Ltd.wholesaler_dandh);

                    SNM.UpdateToRemote(Ltd.wholesaler_asi, table_name);

                    ASI.SetStatus("end");
                    Config.ExecuteNonQuery("Update tb_other_inc set run_end=1 where id=3");// asi
                    RunTimer.WatcherInfos.ASI.end = true;

                    EndAsi = true;
                }
            }
            catch (Exception ex)
            {
                ASI.SetStatus(null, ex.Message);
                Helper.Logs.WriteErrorLog(ex);
            }
            RunTimer.WatcherInfos.ASI.begin = false;
            RunTimer.WatcherInfos.ASI.running = false;

        }

        private void aSIToolStripMenuItemUpdate_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_asi);
        }

        private void aSIToolStripMenuItemCompare_Click(object sender, EventArgs e)
        {
            Helper.Compare.ViewCompare(Ltd.wholesaler_asi);
        }

        private void ncixUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.Rival_Ncix);
        }

        private void directDialUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.Rival_DirectDial);
        }

        private void tigerDirectUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.Rival_TigerDirect);
        }

        private void canadacomputersUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_CanadaComputers);
        }

        //private void exportFileToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //   // Uri u = new Uri(CHANGE_EXPORT_FILE_URL);
        //    ChangeWebBrowserUrl(u);
        //}

        #region Methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="u"></param>
        public void ChangeWebBrowserUrl(Uri u)
        {
            this.webBrowser1.Url = u;
        }

        private void UpdateLtdInfoToRemote(Ltd ltd)
        {
            //SetListBox(this.listBox1, ltd.ToString() + " upload begin.");
            SNM.SetStatus(null, ltd.ToString() + " upload begin.");
            string table_name = LH.GetLastStoreTableNameGroup(ltd);
            SNM.UpdateToRemote(ltd, table_name);
            SNM.SetStatus(null, ltd.ToString() + " upload end.");
            //SetListBox(this.listBox1, ltd.ToString() + " upload end.");
        }


        public void ViewCompareResult(Ltd[] ltds)
        {
            HttpHelper HH = new HttpHelper();
            Watch.ComparePrice CP = new LUComputers.Watch.ComparePrice();

            string htmls = "";

            var sw = new StreamWriter(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html"));

            for (int i = 0; i < ltds.Length; i++)
            {
                SetListBox(listBox1, string.Concat(ltds[i].ToString(), " View compare result."));
                htmls += CP.GETHTMLResult(ltds[i]);
            }

            htmls = HH.GetHttpHead(htmls);

            sw.Write(htmls);
            sw.Close();
            sw.Dispose();

            Uri u = new Uri(string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html"));
            this.webBrowser1.Url = u;
        }

        #endregion

        private void RunChangeStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uri u = new Uri(CHANGE_STOCK_URL);
            ChangeWebBrowserUrl(u);
        }

        private void sendEmailFormWebsiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Uri u = new Uri(SEND_EMAIL_TO_BESON_URL);
            //ChangeWebBrowserUrl(u);
        }

        private void viewDetail2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ltd_View_Detail2 lvd = new Ltd_View_Detail2();
            lvd.ShowDialog();
        }

        private void toolStripButton_view_all_ltd_stat_info_Click(object sender, EventArgs e)
        {
            ViewCompareResult(new Ltd[] { Ltd.lu
            , Ltd.wholesaler_asi
            ,Ltd.wholesaler_Synnex
            ,Ltd.wholesaler_d2a
            });
        }

        #region Update stock to server.
        private void runStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new System.Threading.Thread(new ThreadStart(statStock));
            thread.IsBackground = true;
            thread.Start();
        }

        void statStock()
        {
            this.UpdateServer.StatStock();
        }

        private void stockUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new System.Threading.Thread(new ThreadStart(UpdateStockTOServer));
            thread.IsBackground = true;
            thread.Start();

        }
        void UpdateStockTOServer()
        {
            this.UpdateServer.UpdateRemote();
        }
        #endregion

        private void alc_updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_ALC);
        }

        private void alc_compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Helper.Compare.ViewCompare(Ltd.wholesaler_ALC);
        }

        private void aLLToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Thread thread = new Thread(new ThreadStart(ShopbotWatch));
            thread.IsBackground = true;
            thread.Start();
        }

        void ShopbotWatch()
        {
            //shopbotToolStripMenuItemWatch_Click(null, null);
            //shopbotToolStripMenuItemMatch_Click(null, null);
            ncixUpdateToolStripMenuItem_Click(null, null);
            directDialUpdateToolStripMenuItem_Click(null, null);
            tigerDirectUpdateToolStripMenuItem_Click(null, null);
        }

        private void runStockAndUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(StockToServerAll));
            thread.IsBackground = true;
            thread.Start();
        }

        void StockToServerAll()
        {
            statStock();
            UpdateStockTOServer();

            if (RunASIDandhSynnex)
            {
                RunASIDandhSynnex = false;
                sendEmailAsiDandhSynnex();
                AutoIsCloseWin = true;      // 关闭程序
                                            //syn.Post(MainCloseWin, null);
            }
        }

        private void aLLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //newEggToolStripMenuItemWatch_Click(null, null);
            //newEggToolStripMenuItemMatch_Click(null, null);
            //newEggToolStripMenuItemCompare_Click(null, null);
            //newEggToolStripMenuItemUpdate_Click(null, null);
            //Thread thread = new Thread(new ThreadStart(WatchNewEgg));
            //thread.IsBackground = true;
            //thread.Start();
        }

        private void aLLToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //toolStripMenuItem7_Click(null, null);
            //eTCToolStripMenuItemCompare_Click(null, null);
            //eTCToolStripMenuItemUpdate_Click(null, null);
        }

        private void aLLNetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            aLLToolStripMenuItem_Click(null, null);
            aLLToolStripMenuItem1_Click(null, null);
            aLLToolStripMenuItem2_Click(null, null);
        }
          

        private void changeNotebookPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void watchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SynnexWatch sw = new SynnexWatch();
            sw.ShowDialog();
            // compareToolStripMenuItem_Click(null, null);
        }

        private void updataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            UpdateLtdInfoToRemote(Ltd.wholesaler_Synnex);
        }

        private void compareToolStripMenuItem_Click(object sender, EventArgs e)
        {
            synnex.SetStatus(null, "Compare...");
            synnex.ViewCompare();
            synnex.SetStatus(null, "Compare End...");
        }

        private void loadNotebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SynnexWatch sw = new SynnexWatch();
            sw.ShowDialog();
        }

        private void viewAllNotebookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view_all_notebook van = new view_all_notebook();
            van.WindowState = FormWindowState.Maximized;
            van.ShowDialog();
        }

        private void fTPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 执行 synnex 所有
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aLLToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(SynnexWatchR);
            t.Start();
        }

        void SynnexWatchR()
        {
            try
            {
                eprom.SetStatus(null, null, Ltd.wholesaler_Synnex, "Synnex is begin. " + DateTime.Now.ToString());
                string filepath = cost_file_path + "\\c1151315.ap";
                if (synnex.WatchRun(filepath, checkBoxSaveAll.Checked))
                {
                    synnex.ViewCompare();

                    string tableNameNoDate = LUComputers.DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(Ltd.wholesaler_Synnex.ToString()));
                    // string table_name = new LtdHelper().GetLastStoreTableNameGroup(Ltd.wholesaler_Synnex);
                    string table_name = DBProvider.Find.LastTableName(tableNameNoDate);

                    SNM.UpdateToRemote(Ltd.wholesaler_Synnex, table_name);
                    synnex.SetStatus(null, null, "end");
                    Config.ExecuteNonQuery("Update tb_other_inc set run_end=1 where id=20");// synnex
                }
                else
                    synnex.SetStatus(null, "Synnex is ERROR...");

                synnex.SetStatus(null, null, "is all OK: " + DateTime.Now.ToString());
                RunTimer.WatcherInfos.Synnex.end = true;
                EndSynnex = true;

            }
            catch (Exception ex)
            {
                synnex.SetStatus(null, ex.Message);
                Helper.Logs.WriteErrorLog(ex);
            }
            RunTimer.WatcherInfos.Synnex.begin = false;
            RunTimer.WatcherInfos.Synnex.running = false;
        }

        private void matchPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='0' ");
            //is_run_over_send_mail = false;
            Thread thread = new Thread(new ThreadStart(MatchBasePrice));
            thread.IsBackground = true;
            thread.Start();

            Thread thread2 = new Thread(new ThreadStart(MatchBasePrice2));
            thread2.IsBackground = true;
            thread2.Start();
        }

        void MatchBasePrice()
        {
            try
            {
                if (validLUC.MatchBasePrice(1))
                {
                    validLUC.OK1 = true;
                    if (validLUC.OK1 && validLUC.OK2)
                    {
                        validLUC.UpdatePriceToRemote();
                        //validLUC.SetStatus(Config.http_url + "q_admin/sale_product_export_file.aspx?menu_id=2&cmd=9867054"
                        //    , null, null);
                        StockToServerAll();
                    }
                }

                validLUC.SetStatus("Match base price end 1");
            }
            catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }
        }

        void MatchBasePrice2()
        {
            try
            {
                if (validLUC.MatchBasePrice(2))
                {
                    validLUC.OK2 = true;
                    if (validLUC.OK1 && validLUC.OK2)
                    {
                        validLUC.UpdatePriceToRemote();
                        //validLUC.SetStatus(Config.http_url + "q_admin/sale_product_export_file.aspx?menu_id=2&cmd=9867054"
                        //    , null, null);
                        StockToServerAll();
                    }
                }
                validLUC.SetStatus("Match base price end 2");
            }
            catch (Exception ex) { Helper.Logs.WriteErrorLog(ex); }
        }

        #region Watcher Logs

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name == LogsErrorFilename)
            {
                Thread thread = new Thread(new ThreadStart(WatcherLogs));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (e.Name == LogsErrorFilename)
            {
                Thread thread = new Thread(new ThreadStart(WatcherLogs));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        void WatcherLogs()
        {
            this.textBox1.Invoke(new MethodInvoker(delegate
            {
                this.textBox1.Text = "";
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + LogsErrorFilename))
                    return;
                string[] lines = File.ReadAllLines(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\" + LogsErrorFilename);
                foreach (var s in lines)
                {
                    this.textBox1.Text += s + "\r\n";
                }

            }));
        }
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.ExitThread();
        }

        #region Watcher Ltd File
        private void fileSystemWatcherLtdFile_Changed(object sender, FileSystemEventArgs e)
        {
            FileInfo fi = null;
            switch (e.Name)
            {
                //case "70972.csv":                   
                //    fi = new FileInfo(e.FullPath);
                //    if (fi.Length == 0) return;
                //    if (fi.Length != RunTimer.WatcherInfos.ASI.length)
                //    {
                //        RunTimer.WatcherInfos.ASI.length = fi.Length;
                //    }
                //    else
                //    {
                //        if (RunTimer.WatcherInfos.ASI.begin) return;
                //        Thread.Sleep(1000);
                //        RunTimer.WatcherInfos.ASI.end = false;
                //        RunTimer.WatcherInfos.ASI.begin = true;   

                //    }
                //    break;
                //case "ITEMLIST":
                //    fi = new FileInfo(e.FullPath);
                //    if (fi.Length == 0) return;
                //    if (fi.Length != RunTimer.WatcherInfos.DanDh.length)
                //    {
                //        RunTimer.WatcherInfos.DanDh.length = fi.Length;
                //    }
                //    else
                //    {
                //        if (RunTimer.WatcherInfos.DanDh.begin) return;
                //        Thread.Sleep(1000);
                //        RunTimer.WatcherInfos.DanDh.end = false;
                //        RunTimer.WatcherInfos.DanDh.begin = true;                        
                //    }
                //    break;

                case "eprom_price_list.csv":
                    fi = new FileInfo(e.FullPath);
                    if (fi.Length == 0) return;
                    Thread.Sleep(3000);
                    if (fi.Length != RunTimer.WatcherInfos.Eprom.length)
                    {
                        RunTimer.WatcherInfos.Eprom.length = fi.Length;
                    }
                    else
                    {
                        if (RunTimer.WatcherInfos.Eprom.begin) return;

                        RunTimer.WatcherInfos.Eprom.end = false;
                        RunTimer.WatcherInfos.Eprom.begin = true;
                    }
                    break;

                case "MMAX.xls":
                    fi = new FileInfo(e.FullPath);
                    if (fi.Length == 0) return;
                    Thread.Sleep(3000);
                    if (fi.Length != RunTimer.WatcherInfos.MMAX.length)
                    {
                        RunTimer.WatcherInfos.MMAX.length = fi.Length;
                    }
                    else
                    {
                        if (RunTimer.WatcherInfos.MMAX.begin) return;

                        RunTimer.WatcherInfos.MMAX.end = false;
                        RunTimer.WatcherInfos.MMAX.begin = true;
                    }
                    break;

                case "D2A.xls":
                    fi = new FileInfo(e.FullPath);
                    if (fi.Length == 0) return;
                    Thread.Sleep(3000);
                    if (fi.Length != RunTimer.WatcherInfos.D2A.length)
                    {
                        RunTimer.WatcherInfos.D2A.length = fi.Length;
                    }
                    else
                    {
                        if (RunTimer.WatcherInfos.D2A.begin) return;

                        RunTimer.WatcherInfos.D2A.end = false;
                        RunTimer.WatcherInfos.D2A.begin = true;
                    }
                    break;

                    //case "priceList.xls":
                    //    fi = new FileInfo(e.FullPath);
                    //    if (fi.Length == 0) return;
                    //    if (fi.Length != RunTimer.WatcherInfos.SupercomALL.length)
                    //    {
                    //        RunTimer.WatcherInfos.SupercomALL.length = fi.Length;
                    //    }
                    //    else
                    //    {
                    //        if (RunTimer.WatcherInfos.SupercomALL.begin) return;
                    //        Thread.Sleep(1000);
                    //        RunTimer.WatcherInfos.SupercomALL.end = false;
                    //        RunTimer.WatcherInfos.SupercomALL.begin = true;                        
                    //    }
                    //    break;

                    //case "c1151315.ap":
                    //     fi = new FileInfo(e.FullPath);
                    //    if (fi.Length == 0) return;
                    //    if (fi.Length != RunTimer.WatcherInfos.Synnex.length)
                    //    {
                    //        RunTimer.WatcherInfos.Synnex.length = fi.Length;
                    //    }
                    //    else
                    //    {
                    //        if (RunTimer.WatcherInfos.Synnex.begin) return;
                    //        Thread.Sleep(1000);
                    //        RunTimer.WatcherInfos.Synnex.end = false;
                    //        RunTimer.WatcherInfos.Synnex.begin = true;                        
                    //    }
                    //    break;
            }
        }
        #endregion

        /// <summary>
        /// 比较数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void matchPriceSendMailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ViewCompareResult(new Ltd[] {
              Ltd.wholesaler_asi
            , Ltd.wholesaler_Synnex
            , Ltd.wholesaler_d2a});
            var str = "_s.html is not exist.";
            if (File.Exists(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html")))
            {
                StreamReader sr = new StreamReader(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, "_s.html"));
                str = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            var eh = new LUComputers.Helper.EmailHelper();
            if (eh.SendToEmail("terryeah@gmail.com", str, "LUCOMPUTERS WEB DATE UPDATES"))
            {
                validLUC.SetStatus("Mail is send.");
            }
            else
            {
                validLUC.SetStatus("Mail Send is faild.");
            }

        }

        private void changeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.validLUC.ChangeConnPartPrice();
        }

        private void supercomASIDandhSynnexToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RunASIDandhSynnex = true;
        }

        /// <summary>
        /// 比较统计并上传数据
        /// </summary>
        private void CompareAndUpdate()
        {
            matchPriceSendMailToolStripMenuItem_Click(null, null);

            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='0' ");

            matchPriceToolStripMenuItem_Click(null, null);

            Watch.Synnex.OnSale();
        }

        /// <summary>
        /// 常用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(RunALL);
            t.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        void RunALL()
        {
            WatchD2A();
            WatchASI();
            SynnexWatchR();
        }

        /// <summary>
        /// 下载网站价格
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Config.RemoteExecuteNonQuery(@"
delete from tb_other_inc_match_lu_sku where lu_sku='' or lu_sku is null;
delete from tb_other_inc_part_info where  luc_sku='' or luc_sku is null;");
            LUToolStripMenuItemWatch_Click(null, null);
        }

        /// <summary>
        /// CanadaComputers Watch/Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void watchUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Thread thread = new Thread(CanadaComputerRun);
            //thread.IsBackground = true;
            //thread.Start();
        }

        /// <summary>
        /// CanadaComputers View
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void viewToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void matchToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Thread thread = new Thread(CanadaComputersMatch);
            //thread.IsBackground = true;
            //thread.Start();
        }

        /// <summary>
        /// 把数据发送给我
        /// </summary>
        public void sendEmailAsiDandhSynnex()
        {
            new Helper.EmailHelper().SendToEmail("terryeah@gmail.com", "read title", string.Format("Synnex ({0}), ASI ({1})", GetQtyLtd(Ltd.wholesaler_Synnex), GetQtyLtd(Ltd.wholesaler_asi)));
        }

        /// <summary>
        /// 把数据发送给我
        /// </summary>
        public void sendEmailCanadacomputer()
        {
            int fileCount = 0;
            string path = AppDomain.CurrentDomain.BaseDirectory + "\\CanadaComputer\\" + DateTime.Now.ToString("yyyyMMdd");
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                fileCount = dir.GetFiles().Length;
            }

            new Helper.EmailHelper().SendToEmail("terryeah@gmail.com", "read title", string.Format("Canadacomputer ({0})", fileCount.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        /// <returns></returns>
        public int GetQtyLtd(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            int id = LH.LtdHelperValue(ltd);

            string tableNameNoDate = DBProvider.TableName.GetPriceTableNamePart(new LtdHelper().FilterText(ltd.ToString()));

            string tablename = Config.ExecuteScalar(string.Format(@"select table_name 
from information_schema.tables 
where table_schema='ltd_info' and table_name like '{0}%' order by table_name desc limit 1", tableNameNoDate)).ToString();
            return Config.ExecuteScalarInt("Select count(*) from " + tablename);
        }

        void Shutdown()
        {

            // Info.Arguments = "mysql -uroot -p1234qwer ltd_info < "+ dbFilename + "";
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.StartInfo.FileName = @"C:\WINDOWS\system32\cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = false;
            proc.Start();
            proc.StandardInput.WriteLine("C:");
            proc.StandardInput.WriteLine("CHDIR C:\\WINDOWS\\system32\\");
            proc.StandardInput.WriteLine("shutdown -s -t 60");
            proc.StandardInput.WriteLine("exit");

        }

        private void checkBoxShowdown_Click(object sender, EventArgs e)
        {

        }

        private void checkBoxIsRemote_Click(object sender, EventArgs e)
        {
            string filename = AppDomain.CurrentDomain.BaseDirectory + "\\isRemote.txt";

            try
            {
                StreamWriter sw = new StreamWriter(filename, false, Encoding.UTF8);
                sw.Write(this.checkBoxIsRemote.Checked.ToString());
                sw.Close();
            }
            catch { }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            //ShowPdfobx spf = new ShowPdfobx();
            //spf.StartPosition = FormStartPosition.CenterParent;
            //spf.ShowDialog();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(ShopbotSplitFlow);
            t.Start();
        }

        void ShopbotSplitFlow()
        {
            //shopbot.SplitFlowData();
        }

        /// <summary>
        /// up shopbot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(UploadShopbot);
            t.Start();
        }

        void UploadShopbot()
        {
            //shopbot.UpdateShopbotToRemote();
        }

        private void viewToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SupercomALL_View SAV = new SupercomALL_View();
            SAV.Show();
        }

        private void toolStripMenuItemASIView_Click(object sender, EventArgs e)
        {
            ltd_ASI_View fav = new ltd_ASI_View();
            fav.StartPosition = FormStartPosition.CenterParent;
            fav.ShowDialog();
        }

        private void tESTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Test f = new Test();
            f.StartPosition = FormStartPosition.CenterParent;
            f.ShowDialog();
        }

        private void onSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            this.Cursor = Cursors.Default;
            var allCount = Watch.Synnex.OnSale();
            MessageBox.Show(allCount.ToString() + " OK");
        }

        private void onSaleToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            Watch.LU.ChangeWebIndexPagePart();
            MessageBox.Show("OK");
        }

        private void readFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string filename = "";
            if (RunASIDandhSynnex)
                filename = cost_file_path + "\\d2a.xls";
            ltdD2aReadFile f = new ltdD2aReadFile(filename);
            f.StartPosition = FormStartPosition.CenterParent;
            if (f.ShowDialog() == System.Windows.Forms.DialogResult.Yes)
            {
                d2aToolStripMenuItemCompare_Click(null, null);
                d2aToolStripMenuItemUpdate_Click(null, null);
            }
            else
            {

            }
        }

        private void oneUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='1' ");
            string table_name = LH.GetLastStoreTableNameGroup(Ltd.wholesaler_d2a);
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='0' where lu_sku in (select luc_sku from " + table_name + " where luc_sku >0)");
            matchPriceToolStripMenuItem_Click(null, null);
        }

        private void oneUpdateToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='1' ");
            string table_name = LH.GetLastStoreTableNameGroup(Ltd.wholesaler_asi);
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='0' where lu_sku in (select luc_sku from " + table_name + " where luc_sku >0)");
            matchPriceToolStripMenuItem_Click(null, null);
        }

        private void oneUpdateToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='1' ");
            string table_name = LH.GetLastStoreTableNameGroup(Ltd.wholesaler_Synnex);
            Config.ExecuteNonQuery("Update tb_other_inc_valid_lu_sku set isOk='0' where lu_sku in (select luc_sku from " + table_name + " where luc_sku >0)");
            matchPriceToolStripMenuItem_Click(null, null);
        }

        private void checkBoxSaveAll_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void watchToolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }
    }
}
