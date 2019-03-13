using System;
using System.Net;
using System.IO;
using System.Text;

namespace Util
{
    public class FTPClient
    {

        string ftpServerIP;

        string ftpUserID;

        string ftpPassword;

        FtpWebRequest reqFTP;

        //private Boolean logined;

        public void Connect(string path)
        {
            reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(path));
            reqFTP.UseBinary = true;
            reqFTP.UsePassive = false;
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

        }

        public FTPClient(string ftpServerIp, string ftpUserID, string ftpPassword)
        {
            this.ftpServerIP = ftpServerIp;
            this.ftpUserID = ftpUserID;
            this.ftpPassword = ftpPassword;
        }


        //上面的代码实现了从ftp服务器上载文件的功能
        public void Upload(string filename, string path)
        {

            FileInfo fileInf = new FileInfo(filename);

            string uri = "ftp://" + ftpServerIP + path +fileInf.Name;

            //连接 
            Connect(uri);


            // 默认为true，连接不会被关闭

            // 在一个命令之后被执行

            // reqFTP.KeepAlive = false;



            // 指定执行什么命令
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;



            // 上传文件时通知服务器文件的大小

            reqFTP.ContentLength = fileInf.Length;


            // 缓冲大小设置为kb 
            int buffLength = 2048;


            byte[] buff = new byte[buffLength];

            int contentLen;


            // 打开一个文件流(System.IO.FileStream) 去读上传的文件
            FileStream fs = fileInf.OpenRead();

            try
            {

                // 把上传的文件写入流
                Stream strm = reqFTP.GetRequestStream();


                // 每次读文件流的kb 
                contentLen = fs.Read(buff, 0, buffLength);


                // 流内容没有结束
                while (contentLen != 0)
                {

                    // 把内容从file stream 写入upload stream 
                    strm.Write(buff, 0, contentLen);

                    contentLen = fs.Read(buff, 0, buffLength);

                }

                // 关闭两个流
                strm.Close();
                fs.Close();
                reqFTP.Abort();
            }

            catch (Exception ex)
            {

                throw ex;//(ex.Message, "Upload Error");

            }

        }

        public void MakeDir(string dirName)
        {

            try
            {
                string uri = "ftp://" + ftpServerIP + "/" + dirName;
                Connect(uri);//连接       
                reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                response.Close();
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="fileName"></param>
        public void Download(string filePath, string fileName)
        {
            Download(filePath, fileName,  "");
        }
        public void Download(string filePath, string fileName,string serverPath)
        {
           
            try
            {
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                Uri u = new Uri("ftp://" + ftpServerIP.Trim() + "/" + (serverPath == "" ? "" : serverPath + "/") + fileName);
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(u);
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
                reqFTP.Abort();
                
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);//.Insert("FtpWeb", "Download Error --> " + ex.Message);
            }
        }

        public string[] GetFileList(string mask)
        {
            //string[] downloadFiles;
            StringBuilder result = new StringBuilder();

            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    if (mask.Trim() != string.Empty && mask.Trim() != "*.*")
                    {
                        string mask_ = mask.Substring(0, mask.IndexOf("*"));
                        if (line.Substring(0, mask_.Length) == mask_)
                        {
                            result.Append(line);
                            result.Append("\n");
                        }
                    }
                    else
                    {
                        result.Append(line);
                        result.Append("\n");
                    }
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                Util.Logs.WriteErrorLog(ex);
            }
            return null;
        }
    }
}
