using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace AutoExecute
{
    public class MainRun
    {
        private static Thread _ServiceThread;
        private static bool _IsRun;//是否運行
        private static IPAddress _LocalIP;//IP
        private static int _Port;//监听端口
        static ManualResetEvent _allDone;//同步消息

        private static Thread _SaveOnlineThread;

        public MainRun() { }

        /// <summary>
        /// 启动服务
        /// </summary>
        public static void OnStart()
        {

            _LocalIP = IPAddress.Parse(config.ServerIP);// IPAddress.Parse(System.Configuration.ConfigurationSettings.AppSettings["ip"].ToString());
            _Port = int.Parse(config.ServerPort);// 5001;// int.Parse(System.Configuration.ConfigurationSettings.AppSettings["port"].ToString());

            _IsRun = false;
            _allDone = new ManualResetEvent(false);
            _ServiceThread = new Thread(new ThreadStart(ThreadMethod));

            if (!_IsRun)
            {
                _IsRun = true;
                Logs.WriteLog("Start");
                _ServiceThread.Start();
                
            }
        }



        /// <summary>
        /// 停止服务
        /// </summary>
        public static void OnStop()
        {
            if (_IsRun)
            {
                _IsRun = false;
                _allDone.Set();
            }
        }

        /// <summary>
        /// 运行侦听线珵
        /// </summary>
        private static void ThreadMethod()
        {
            IPEndPoint localEP = new IPEndPoint(_LocalIP, _Port);
            Socket listener = new Socket(localEP.Address.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEP);
                //listener.Listen(8192);
                listener.Listen(1024);
                Logs.WriteLog("开始监听。地址:" + localEP.ToString());
                while (_IsRun)
                {
                    _allDone.Reset();
                    listener.BeginAccept(
                        new AsyncCallback(AcceptCallback),
                        listener);
                    _allDone.WaitOne();
                }
            }
            catch (Exception e)
            {
                Logs.WriteErrorLog(e);
            }
        }

        /// <summary>
        /// 异步处理接收的数据
        /// </summary>
        /// <param name="ar"></param>
        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            Socket Client = listener.EndAccept(ar);

            _allDone.Set();
            //SslStream stream = null;
            NetworkStream stream = null;
            try
            {
                stream = new NetworkStream(Client, true);
                string RequestString = Util.ReadMessage(stream);
                //處理信息
                ClsCommand RequestCmd = new ClsCommand(RequestString);
                ClsCommand RespondCmd = null;

                Logs.WriteLog(RequestCmd.Command + " " + RequestCmd.GetArgumentValue(CommandString.Arg_RunCmd));
                RespondCmd = ExecuteCommand(RequestCmd);

                //發送信息
                Byte[] data = System.Text.Encoding.Unicode.GetBytes(RespondCmd.CommandFull + "lngkend");
                stream.Write(data, 0, data.Length);
                stream.Flush();
                stream.Close(5 * 1000);
                stream.Close();
                Client.Close();
            }
            catch (System.Exception e)
            {
                Logs.WriteErrorLog(e);
                if (stream == null)
                {
                    stream.Flush();
                    stream.Close();
                }
                Client.Disconnect(true);
                Client.Dispose();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RequestCmd"></param>
        /// <returns></returns>
        private static ClsCommand ExecuteCommand(ClsCommand RequestCmd)
        {
            ClsCommand ResponseCmd = null;
            Logs.WriteLog(RequestCmd.Command);
            switch (RequestCmd.Command)
            {

                case CommandString.CMD_AutuRunCmd:
                    ResponseCmd = CMD_AutuRunCmd(RequestCmd);
                    break;


                default:
                    ResponseCmd = new ClsCommand(
                        RequestCmd.Receiver,
                        RequestCmd.Sender,
                        RequestCmd.Command,
                        CommandString.Arg_Return + ClsCommand.cstSplitArgValue + "No Command = " + RequestCmd.Command);
                    break;
            }
            return ResponseCmd;
        }
      
        /// <summary>
        /// 自动运行。
        /// </summary>
        /// <param name="RequestCmd"></param>
        /// <returns></returns>
        static private ClsCommand CMD_AutuRunCmd(ClsCommand RequestCmd)
        {
            try
            {
                string cmd = RequestCmd.GetArgumentValue(CommandString.Arg_RunCmd);

                foreach (var l in config.CmdList)
                {
                    if (l.Key == cmd)
                    {
                        System.Diagnostics.Process pro = new System.Diagnostics.Process();
                        pro.StartInfo.FileName = l.Value;
                        pro.Start();
                        Logs.WriteLog("Run " + l.Value);
                        return new ClsCommand(RequestCmd.Receiver, RequestCmd.Sender, RequestCmd.Command, CommandString.Arg_Return + ClsCommand.cstSplitArgValue + CommandString.True);
                    }
                }
            }
            catch (Exception ex)
            {
                Logs.WriteErrorLog(ex);
            }
            Logs.WriteLog(RequestCmd.GetArgumentValue(CommandString.Arg_RunCmd));
            return new ClsCommand(RequestCmd.Receiver, RequestCmd.Sender, RequestCmd.Command, CommandString.Arg_Return + ClsCommand.cstSplitArgValue + CommandString.False);
        }
    }
}
