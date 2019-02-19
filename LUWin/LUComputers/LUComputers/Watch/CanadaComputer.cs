using System;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using LUComputers.DBProvider;

namespace LUComputers.Watch
{
   public class CanadaComputer
    {
       ltd_infoEntities1 DB = new ltd_infoEntities1();

       string webSite = "http://www.canadacomputers.com";

        public event Events.UrlEventHandler WatchE;

        protected void InvokeWatchE(Events.UrlEventArgs e)
        {
            Events.UrlEventHandler watch = WatchE;
            if (watch != null) watch(this, e);
        }

        public void SetStatus(string url, string comment, string result)
        {
            LUComputers.Events.UrlEventArgs ua = new LUComputers.Events.UrlEventArgs(new Events.UrlEventModel()
            {
                comment = comment,
                url = url,
                ltd = Ltd.wholesaler_CanadaComputers,
                result = result
            });
            InvokeWatchE(ua);
        }

        public void SetStatus(string urlName, string comment)
        {
            SetStatus(null, urlName, comment);
        }

        public void SetStatus(string comment)
        {
            SetStatus(null, null, comment);
        }


        public CanadaComputer() { }

        /// <summary>
        /// 比较结果
        /// </summary>
        public void ViewCompare()
        {
            SetStatus(null, "Compare CanadaComputers begin.");
            Helper.Compare.ViewCompare(Ltd.wholesaler_CanadaComputers);
            SetStatus(string.Format(@"{0}\{1}", Application.StartupPath, "_s.html"), "Compare CanadaComputers end.");

        }

        void Watch(string cateIDs, string storePath)
        {
            bool isList = false;
            int count = 0;
            int pageCount = 0;
            int pageSize = 20;
            bool isExist = false;
            var canadaComputer = DB.tb_canadacomputer_weburls.SingleOrDefault(c => c.CateIDs.Equals(cateIDs));
            canadaComputer.Regdate = DateTime.Now;

            string url = string.Format(webSite + canadaComputer.WebUrl);
            HttpHelper hh = new HttpHelper();
            string s = "";
            SetStatus(url, null);
            if (canadaComputer.IsList)
            {
                if (!File.Exists(storePath + canadaComputer.CateIDs + "-L-1.htm"))
                {
                    s = hh.HttpGet(url);
                }
                else
                {
                    s = File.ReadAllText(storePath + canadaComputer.CateIDs + "-L-1.htm");
                    isExist = true;
                }
            }
            else
            {
                if (!File.Exists(storePath + canadaComputer.CateIDs + ".htm"))
                {
                    s = hh.HttpGet(url);
                }
                else
                {
                    s = File.ReadAllText(storePath + canadaComputer.CateIDs + ".htm");
                }
            }

            Regex itemCountRe = new Regex(@"<b>([0-9]+)<\/b> items in", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection itemCountMC = itemCountRe.Matches(s);
            
            foreach (Match mm in itemCountMC)
            {
                string itemCount = mm.Groups[1].Value;
               
                int.TryParse(itemCount, out count);
                if (count > 0)
                {
                    isList = true;
                    pageCount = count % pageSize == 0 ? count / pageSize : (count / pageSize) + 1;

                    if (count != canadaComputer.ItemQty)
                    {
                        canadaComputer.ItemQty = count;
                        DB.SaveChanges();
                    }
                }
            }

            string filename = isList ? storePath + canadaComputer.CateIDs + "-L-1.htm" : storePath + canadaComputer.CateIDs + ".htm";
            if (!isExist)
            {
                StreamWriter sw = new StreamWriter(filename, false);
                sw.Write(s);
                sw.Close();
                sw.Dispose();
            }

            if (pageCount > 1)
            {
                for (int i = 0; i < pageCount; i++)
                {
                    string subCont = "";
                    filename = storePath + canadaComputer.CateIDs + "-L-" + (i + 1) + ".htm";
                    if (!File.Exists(filename))
                    {
                        url = string.Format(webSite + canadaComputer.WebUrl + "&page=" + (i + 1).ToString());
                        SetStatus(url + " " + canadaComputer.CateName, null);

                        subCont = hh.HttpGet(url);
                        StreamWriter sw = new StreamWriter(filename, false);
                        sw.Write(subCont);
                        sw.Close();
                        sw.Dispose();
                    }
                }
            }

            Regex re = new Regex(@"<a href=""\/index\.php\?cPath=(" + canadaComputer.CateIDs + @"_[\d|_]*)"">.*?(?=<\/a>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            MatchCollection mc = re.Matches(s);
            if (mc.Count == 0)
            {
                re = new Regex(@"<a href=""\/index\.php\?cPath=(" + canadaComputer.CateIDs + @"_[\d|_]*)&amp;[A-Za-z0-9=]+"">.*?(?=<\/a>)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                mc = re.Matches(s);
            }
            foreach (Match m in mc)
            {
                string newCateIds = m.Groups[1].Value;
                if (newCateIds == canadaComputer.CateIDs)
                    continue;
                

                string webUrl = "/index.php?cPath=" + newCateIds;
                string cateName = m.Groups[0].Value.Split(new char[] { '>' })[1];
                if (cateName.IndexOf("<") > -1 || cateName.IndexOf(";")>-1) continue;
                var q = DB.tb_canadacomputer_weburls.SingleOrDefault(c => c.CateIDs.Equals(newCateIds));
                if (q == null)
                {
                    tb_canadacomputer_weburls cana = tb_canadacomputer_weburls.Createtb_canadacomputer_weburls(0, cateName, webUrl, newCateIds, isList, count);

                    DB.AddTotb_canadacomputer_weburls(cana);
                    cana.Regdate = DateTime.Now;
                    DB.SaveChanges();

                    SetStatus(webUrl + " " +cateName, null);

                    Watch(newCateIds, storePath);
                }
            }


        }

        /// <summary>
        /// 把文件内存保存到数据库并执行相关操作
        /// </summary>
        /// <param name="fullFilename"></param>
        /// <returns></returns>
        public bool WatchRun(bool IsDirectWatch, string storePath)
        {

            LtdHelper lh = new LtdHelper();
            int ltd_id = lh.LtdHelperValue(Ltd.wholesaler_CanadaComputers);

            #region path
            //string storePath = storePath;

            if (!Directory.Exists(storePath))
            {
                Directory.CreateDirectory(storePath);
            }

            //storePath += "\\" + DateTime.Now.ToString("yyyyMMdd");
            //if (!Directory.Exists(storePath))
            //    Directory.CreateDirectory(storePath);
            //storePath += "\\";

            #endregion

            string table_name = CreateTable(Ltd.wholesaler_CanadaComputers);

            if (!IsDirectWatch)
            {
                string url = string.Format("http://www.canadacomputers.com/");

                HttpHelper hh = new HttpHelper();
                string s = hh.HttpGet(url);
                StreamWriter sw = new StreamWriter(storePath + "0.htm",false);
                sw.Write(s);
                sw.Close();
                sw.Dispose();

                Regex re = new Regex(@"<li><a href=""\/index\.php\?cPath=(.*)"">(.*)<\/a><\/li>", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                MatchCollection mc = re.Matches(s);

                foreach (Match m in mc)
                {
                    string cateIds = m.Groups[1].Value;
                    string webUrl = "/index.php?cPath=" + cateIds;
                    string cateName = m.Groups[2].Value;
                    if (cateName.IndexOf("<") > -1) continue;
                    var q = DB.tb_canadacomputer_weburls.SingleOrDefault(c => c.WebUrl.Equals(webUrl));
                    if (q == null)
                    {
                        tb_canadacomputer_weburls cana = tb_canadacomputer_weburls.Createtb_canadacomputer_weburls(0, cateName, webUrl, cateIds, false, 0);

                        DB.AddTotb_canadacomputer_weburls(cana);
                        DB.SaveChanges();
                    }
                    SetStatus(webUrl + " " + cateName, null);
                }
            }
            var list = DB.tb_canadacomputer_weburls.Where(c => c.ID > 0 && (IsDirectWatch ? c.IsList.Equals(true) : true));

            List<string> idsList = new List<string>();
            foreach (var m in list)
                idsList.Add(m.CateIDs);
            foreach (var m in idsList)
            {
                
                Watch(m, storePath);
            }

            SetStatus("canada watch end.");

            DirectoryInfo dir = new DirectoryInfo(storePath);
            FileInfo[] fis = dir.GetFiles();
            foreach (var f in fis)
            {
                if (f.LastWriteTime.ToShortDateString() != DateTime.Now.ToShortDateString())
                {
                    File.Delete(f.FullName);
                    SetStatus("delete file: "+ f.Name);
                }
            }

            //string path = ".\\CanadaComputer\\" + DateTime.Now.ToString("yyyyMMdd");
            //string zipFilename = "CanadaComputer" + DateTime.Now.ToString("yyyyMMdd") + ".7z";
            //Util.T7zip.T7zipTools.ZipFileRecurse(path, zipFilename);
            //if (File.Exists(zipFilename))
            //{
            //    FileInfo fi = new FileInfo(zipFilename);
            //    File.Move(".\\" + zipFilename, (string.IsNullOrEmpty(Config.zipFileStorePath) ? "C:\\Users\\Qiozi\\Desktop\\dbTemp\\" : Config.zipFileStorePath) + zipFilename);
            //    SetStatus("move file to dbTemp");
            //}
            //else
            //    SetStatus("not move file to dbTemp");

           

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        /// <returns></returns>
        public string CreateTable(Ltd ltd)
        {
            LtdHelper LH = new LtdHelper();
            DataTable dt = Config.ExecuteDateTable("show tables like '%" + (LH.FilterText(ltd.ToString())) + "_part_%'");
            for (int i = 0; i < dt.Rows.Count - G.SaveTableCount; i++)
            {
                Config.ExecuteNonQuery("drop table " + dt.Rows[i][0].ToString() + ";");
            }

           
            int ltd_id = LH.LtdHelperValue(ltd);
            string date_short_name = DateTime.Now.ToString("yyyyMMdd");
            string talbe_name = "store_" + (LH.FilterText(ltd.ToString())) + "_part_" + date_short_name;

            //
            // record table Name and Datatime.
            //
            if (Config.ExecuteScalarInt("select count(*) from tb_other_inc_run_date where date_format(run_date,'%Y%m%d')='" + date_short_name + "' and other_inc_id='" + ltd_id.ToString() + "'") == 0)
            {
                Config.ExecuteNonQuery(string.Format(@"
insert into tb_other_inc_run_date 
	( other_inc_id, run_date,db_table_name)
	values
	( '{0}', now(),'{1}')
", ltd_id, talbe_name));
            }
            //
            // create table
            //
            Config.ExecuteNonQuery(@"
 drop table  IF EXISTS `" + talbe_name + @"`;


 CREATE TABLE `" + talbe_name + @"` (              
                    `serial_no` int(6) NOT NULL auto_increment,  
		            `luc_sku`   int(6) default Null,
                    `part_sku` varchar(30) default NULL,        
                    `part_cost` decimal(8,2) default NULL,            
                    `store_quantity` int(6) default 0,    
                    `mfp` varchar(50) default NULL,  
		            `part_name` varchar(220) default Null,
                    `regdate` timestamp NULL default CURRENT_TIMESTAMP,              
                    PRIMARY KEY  (`serial_no`)         
                    ) ENGINE=InnoDB DEFAULT CHARSET=latin1           

");
            return talbe_name;
        }       
    }
}
