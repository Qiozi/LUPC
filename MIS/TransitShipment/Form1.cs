using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace TransitShipment
{
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

            _ftpInfo.WebManage = new Helper.FInfo()
            {
                Ip = "61.172.246.23",
                Uid = "qiozi",
                Pwd = "qiozi123456",
                Port = 21,
                SavePath = "/web/"
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

            Util.Logs.WriteLog("DanDh Begin" + DateTime.Now.ToString());

            try
            {
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

            #endregion

            #region ASI

            try
            {


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
            #endregion

            #region Synnex

            Util.Logs.WriteLog("Synnex Begin " + DateTime.Now.ToString());
            try
            {
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
            #endregion
        }

        void RemoteFileForList(string fullname)
        {
             fileList.Remove(fullname);
             File.Delete(fullname);
        }

        private void button_asi_Click(object sender, EventArgs e)
        {
            //Util.FTPClient ftp;

            //try
            //{


            //    if (File.Exists(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename))
            //    {
            //        File.Delete(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
            //    }
            //    ftp = new Util.FTPClient(_ftpInfo.ASI.Ip
            //         , _ftpInfo.ASI.Uid
            //         , _ftpInfo.ASI.Pwd);
            //    ftp.Download(_ftpInfo.ASI.SavePath, _ftpInfo.ASI.SaveFilename);
            //}
            //catch (Exception ex)
            //{
            //    Util.Logs.WriteErrorLog(ex);
            //    _EndASI = false;
            //}

            ASI.Run(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
        }

        private void button_dandh_Click(object sender, EventArgs e)
        {
            //Util.FTPClient ftp;

            //Util.Logs.WriteLog("DanDh Begin" + DateTime.Now.ToString());

            //try
            //{
            //    if (File.Exists(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename))
            //    {
            //        File.Delete(_ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
            //    }
            //    ftp = new Util.FTPClient(_ftpInfo.Dandh.Ip
            //           , _ftpInfo.Dandh.Uid
            //           , _ftpInfo.Dandh.Pwd);
            //    ftp.Download(_ftpInfo.Dandh.SavePath, _ftpInfo.Dandh.SaveFilename);
            //}
            //catch (Exception ex)
            //{
            //    Util.Logs.WriteErrorLog(ex);
            //    _EndDanah = false;
            //}

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
                int i=0;
                while (true)
                {
                    i+=1;
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
