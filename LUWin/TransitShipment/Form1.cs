using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Linq;

namespace TransitShipment
{
    public enum Cmd
    {
        None,
        DownOrder,
        DownSingleOrder,
        DowneBayPrice,
        GeneratePartHtmlFileForApp,
        NewSiteHomeData,
        NewGeneratePartHtmlFile,
        NewSystemKeyword,
        NewHomeCateList,
        NewSyslistAndPartlist,
        NewPartKeyword,
        /// <summary>
        ///  发送当天的营业额到邮箱
        /// </summary>
        SendTodaySellTotal,
        /// <summary>
        /// 
        /// </summary>
        ReStoreOnsale,
        ModifyPartEbayPrice,
        ModifySystemEbayPrice,
        ModifyEbayPricePartAndSys,
        ReadPriceFile,
        GenerateSysPartsFile,
        RemovePartQuantityIsZone,
        SysToCategory
    }
    public partial class Form1 : Form
    {
        Helper.FtpInfo _ftpInfo = new Helper.FtpInfo();
        List<string> fileList = new List<string>();
        bool _EndASI = false;
        bool _EndDanah = false;
        bool _EndSynnex = false;
        //string _costFilePath = @"C:\Program Files\LUComputer\WatchPrice\CostFile\";

        public Form1()
        {
            Util.Logs.WriteLog("Begin " + DateTime.Now.ToString());

            #region ftp info
            _ftpInfo.ASI = new Helper.FInfo()
            {
                Ip = "67.104.19.215",//"67.91.114.176",
                Port = 21,
                Pwd = "Wu70972Sn",
                Uid = "70972",
                SaveFilename = "70972.csv",
                SavePath = SettingsInfo.tmpSavePath
            };

            _ftpInfo.Dandh = new Helper.FInfo()
            {
                Ip = "ftp.dandh.com",
                Uid = "8018640000",
                Port = 21,
                Pwd = "comben",
                SaveFilename = "ITEMLIST",
                SavePath = SettingsInfo.tmpSavePath
            };

            _ftpInfo.Synnex = new Helper.FInfo()
            {
                Ip = "ftp.synnex.ca",
                Uid = "c1151315",
                Pwd = "20luc315",
                Port = 21,
                SaveFilename = "c1151315.ap",
                SavePath = SettingsInfo.tmpSavePath
            };

            fileList.Add(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
            fileList.Add(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
            fileList.Add(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);

            #endregion

            InitializeComponent();

            this.Shown += new EventHandler(Form1_Shown);
        }

        void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
                if (SettingsInfo.AutoRun)
                {
                    Download();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Download();
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);
            }
        }

        void Download()
        {

            Util.FTPClient ftp;

            #region Dandh
            {
                Util.Logs.WriteLog("DanDh Begin" + DateTime.Now.ToString());

                try
                {
                    if (File.Exists(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename))
                    {
                        FileInfo dhFi2 = new FileInfo(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
                        if (dhFi2.Length > 50L)
                        {

                            if (File.Exists(_ftpInfo.Dandh.SavePath + "\\Bak" + _ftpInfo.Dandh.SaveFilename))
                            {
                                File.Delete(_ftpInfo.Dandh.SavePath + "\\Bak" + _ftpInfo.Dandh.SaveFilename);
                            }
                            // 备分正式文件
                            File.Move(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename,
                                _ftpInfo.Dandh.SavePath + "\\Bak" + _ftpInfo.Dandh.SaveFilename);
                        }
                    }

                    if (File.Exists(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename))
                    {
                        File.Delete(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
                    }
                    ftp = new Util.FTPClient(_ftpInfo.Dandh.Ip
                           , _ftpInfo.Dandh.Uid
                           , _ftpInfo.Dandh.Pwd);
                    ftp.Download(_ftpInfo.Dandh.SavePath, _ftpInfo.Dandh.SaveFilename);
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                    _EndDanah = false;
                }

                FileInfo dhFi = new FileInfo(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
                if (dhFi.Length < 50L)
                {
                    // 删除正式文件
                    File.Delete(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
                    // 更新正式文件
                    File.Move(_ftpInfo.Dandh.SavePath + "\\Bak" + _ftpInfo.Dandh.SaveFilename,
                        _ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
                }
            }
            #endregion

            #region ASI
            {
                Util.Logs.WriteLog("asi Begin" + DateTime.Now.ToString());


                try
                {
                    if (File.Exists(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename))
                    {
                        FileInfo dhFi2 = new FileInfo(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
                        if (dhFi2.Length > 50L)
                        {
                            if (File.Exists(_ftpInfo.ASI.SavePath + "\\Bak" + _ftpInfo.ASI.SaveFilename))
                            {
                                File.Delete(_ftpInfo.ASI.SavePath + "\\Bak" + _ftpInfo.ASI.SaveFilename);
                            }
                            // 备分正式文件
                            File.Move(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename,
                                _ftpInfo.ASI.SavePath + "\\Bak" + _ftpInfo.ASI.SaveFilename);
                        }
                    }
                    if (File.Exists(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename))
                    {
                        File.Delete(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
                    }
                    ftp = new Util.FTPClient(_ftpInfo.ASI.Ip
                         , _ftpInfo.ASI.Uid
                         , _ftpInfo.ASI.Pwd);
                    ftp.Download(_ftpInfo.ASI.SavePath, _ftpInfo.ASI.SaveFilename);
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                    _EndASI = false;
                }

                FileInfo asiFi = new FileInfo(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
                if (asiFi.Length < 50L)
                {
                    // 删除正式文件
                    File.Delete(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
                    // 更新正式文件
                    File.Move(_ftpInfo.ASI.SavePath + "\\Bak" + _ftpInfo.ASI.SaveFilename,
                        _ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
                }
            }
            #endregion

            #region Synnex
            {
                Util.Logs.WriteLog("Synnex Begin " + DateTime.Now.ToString());
                try
                {
                    if (File.Exists(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename))
                    {
                        FileInfo dhFi2 = new FileInfo(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                        if (dhFi2.Length > 50L)
                        {
                            if (File.Exists(_ftpInfo.Synnex.SavePath + "\\Bak" + _ftpInfo.Synnex.SaveFilename))
                            {
                                File.Delete(_ftpInfo.Synnex.SavePath + "\\Bak" + _ftpInfo.Synnex.SaveFilename);
                            }
                            // 备分正式文件
                            File.Move(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename,
                                _ftpInfo.Synnex.SavePath + "\\Bak" + _ftpInfo.Synnex.SaveFilename);
                        }
                    }
                    if (File.Exists(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename))
                    {
                        File.Delete(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                    }

                    ftp = new Util.FTPClient(_ftpInfo.Synnex.Ip
                           , _ftpInfo.Synnex.Uid
                           , _ftpInfo.Synnex.Pwd);
                    ftp.Download(_ftpInfo.Synnex.SavePath, _ftpInfo.Synnex.SaveFilename);
                }
                catch (Exception ex)
                {
                    Util.Logs.WriteErrorLog(ex);
                    _EndSynnex = false;
                }
                FileInfo syFi = new FileInfo(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                if (syFi.Length < 50L)
                {
                    // 删除正式文件
                    File.Delete(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                    // 更新正式文件
                    File.Move(_ftpInfo.Synnex.SavePath + "\\Bak" + _ftpInfo.Synnex.SaveFilename,
                        _ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                }
            }
            #endregion

            #region 更新价格命令
            nicklu2Entities db = new nicklu2Entities();

            var cmdDownEbayPrice = ((int)Cmd.DowneBayPrice).ToString();

            tb_timer t2 = db.tb_timer.FirstOrDefault(t => t.Cmd.Equals(cmdDownEbayPrice));
            if (t2 == null)
            {
                t2 = tb_timer.Createtb_timer(0, "", cmdDownEbayPrice, DateTime.Now, 1);
                db.AddTotb_timer(t2);
            }
            else
            {
                t2.Status = 1;
                t2.regdate = DateTime.Now;
            }
            db.SaveChanges();

            Thread.Sleep(10000);
            var cmdReadPriceFile = ((int)Cmd.ReadPriceFile).ToString();

            tb_timer t1 = db.tb_timer.FirstOrDefault(d => d.Cmd.Equals(cmdReadPriceFile));
            if (t1 == null)
            {
                t1 = tb_timer.Createtb_timer(0, "", ((int)Cmd.ReadPriceFile).ToString(), DateTime.Now, 1);
                db.AddTotb_timer(t1);
            }
            else
            {
                t1.Status = 1;
                t1.regdate = DateTime.Now;
            }

            db.SaveChanges();
            #endregion
        }

        void RemoteFileForList(string fullname)
        {
            fileList.Remove(fullname);
            File.Delete(fullname);
        }

        private void button_asi_Click(object sender, EventArgs e)
        {
            ASI.Run(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
        }

        private void button_dandh_Click(object sender, EventArgs e)
        {
            Dandh.DanDhWatch(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename, false);
        }

        private void button_synnex_Click(object sender, EventArgs e)
        {
            Util.FTPClient ftp;

            Util.Logs.WriteLog("Synnex Begin " + DateTime.Now.ToString());
            try
            {
                if (!File.Exists(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename))
                {
                    ftp = new Util.FTPClient(_ftpInfo.Synnex.Ip
                       , _ftpInfo.Synnex.Uid
                       , _ftpInfo.Synnex.Pwd);
                    ftp.Download(_ftpInfo.Synnex.SavePath, _ftpInfo.Synnex.SaveFilename);

                }
                int i = 0;
                while (true)
                {
                    i += 1;
                    this.button_synnex.Text = i.ToString();
                    this.button_synnex.Update();
                    try
                    {
                        FileStream fi = File.OpenRead(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                        fi.Close();
                        break;
                    }
                    catch (IOException ex)
                    {
                        Util.Logs.WriteErrorLog(ex);
                        Thread.Sleep(1000);
                    }
                }

                if (File.Exists(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename))
                {
                    Util.T7zip.T7zipTools.ZipFile(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename,
                        _ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename.Replace("ap", "7z"));

                    ftp = new Util.FTPClient(_ftpInfo.WebManage.Ip
                          , _ftpInfo.WebManage.Uid
                          , _ftpInfo.WebManage.Pwd);
                    //ftp.Connect("");
                    ftp.Upload(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename.Replace("ap", "7z"), _ftpInfo.WebManage.SavePath);
                    RemoteFileForList(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
                    File.Delete(_ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename.Replace("ap", "7z"));
                }
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);
            }
        }
    }
}
