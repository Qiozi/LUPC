using AutoDownPrice.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoDownPrice
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

        public Form1()
        {
            #region ftp info
            _ftpInfo.ASI = new Helper.FInfo()
            {
                Ip = "4.79.60.130",//"67.91.114.176",
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
            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Download();
            this.Close();
            Application.Exit();
        }

        void CopyTOBak(string filename, string oldFilename)
        {
            var path = Path.Combine(SettingsInfo.tmpSavePath, "bak");
            if (!System.IO.Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var newFilename = path.TrimEnd('\\') + "\\" + DateTime.Now.ToString("yyyyMMddHH") + "_" + filename;
            File.Copy(oldFilename, newFilename);
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
                            CopyTOBak(_ftpInfo.Dandh.SaveFilename, _ftpInfo.Dandh.SavePath + "\\" + _ftpInfo.Dandh.SaveFilename);
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
            //if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
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
                            CopyTOBak(_ftpInfo.ASI.SaveFilename, _ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
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
            //else
            //{
            //    // 删除正式文件
            //    File.Delete(_ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
            //    // 更新正式文件
            //    File.Copy(_ftpInfo.ASI.SavePath + "\\Bak" + _ftpInfo.ASI.SaveFilename,
            //        _ftpInfo.ASI.SavePath + "\\" + _ftpInfo.ASI.SaveFilename);
            //}
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
                            CopyTOBak(_ftpInfo.Synnex.SaveFilename, _ftpInfo.Synnex.SavePath + "\\" + _ftpInfo.Synnex.SaveFilename);
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
            var cmdDownEbayPrice = ((int)Cmd.DowneBayPrice).ToString();

            var query = LUComputers.DBProvider.Config.ExecuteDateTable("Select * from tb_timer where cmd='" + cmdDownEbayPrice + "'");
            if (query.Rows.Count == 0)
            {
                LUComputers.DBProvider.Config.ExecuteNonQuery(string.Format(@"INSERT INTO `tb_timer`
(`CmdContent`,
`Cmd`,
`regdate`,
`Status`)
VALUES
(
'{0}',
'{1}',
now(),
1);
                ", "", cmdDownEbayPrice));
            }

            else
            {
                LUComputers.DBProvider.Config.ExecuteNonQuery("Update tb_timer set regdate=now(), Status =1 where cmd='" + cmdDownEbayPrice + "'");
            }

            Thread.Sleep(10000);
            var cmdReadPriceFile = ((int)Cmd.ReadPriceFile).ToString();

            query = LUComputers.DBProvider.Config.ExecuteDateTable("Select * from tb_timer where cmd='" + cmdReadPriceFile + "'");
            if (query.Rows.Count == 0)
            {
                LUComputers.DBProvider.Config.ExecuteNonQuery(string.Format(@"INSERT INTO tb_timer`
(`CmdContent`,
`Cmd`,
`regdate`,
`Status`)
VALUES
(
'{0}',
'{1}',
now(),
1);
                ", "", cmdReadPriceFile));
            }

            else
            {
                LUComputers.DBProvider.Config.ExecuteNonQuery("Update tb_timer set regdate=now(), Status =1 where cmd='" + cmdReadPriceFile + "'");
            }
            #endregion
        }
    }
}
