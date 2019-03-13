using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;

namespace Util.Socket
{
    public class Work
    {
        public static bool RemoteConnected = false;
        /// <summary>
        /// 讀取传送的 Message
        /// </summary>
        /// <param name="sslStream"></param>
        /// <returns></returns>
        public static string ReadMessage(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];
            StringBuilder messageData = new StringBuilder();
            int bytes = -1;
            do
            {
                // Read the client's test message.               
                bytes = stream.Read(buffer, 0, buffer.Length);
                Decoder decoder = Encoding.Unicode.GetDecoder();
                char[] chars = new char[decoder.GetCharCount(buffer, 0, bytes)];
                decoder.GetChars(buffer, 0, bytes, chars, 0);

                messageData.Append(chars);
                if (messageData.ToString().IndexOf("lngkend") > -1)
                {
                    break;
                }
            } while (true);
            return messageData.Remove(messageData.Length - 7, 7).ToString().TrimEnd('\0');
        }

        /// <summary>
        /// 使用SSL连接发送 ClsCommand 请求到指定的服务器端口
        /// 返回结果
        /// </summary>
        /// <param name="Request"></param>
        /// <param name="ServerName"></param>
        /// <param name="Port"></param>
        /// <returns></returns>
        public static ClsCommand SendSslMessage(ClsCommand Request, string ServerName, int Port)
        {
            ClsCommand response = null;
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(ServerName, Port);
                client.ReceiveTimeout = 15000;
                RemoteConnected = true;  //  远程连接成功

                //JessonYang 取消SSL 加密通讯
                //創建支持 SSL 的流
                //SslStream stream = new SslStream(
                //    client.GetStream(),
                //    false,
                //    new RemoteCertificateValidationCallback(ValidateServerCertificate),
                //    null
                //    );
                //stream.AuthenticateAsClient("EunovationEnterpriseBox");          
                NetworkStream stream = client.GetStream();

                Byte[] data = System.Text.Encoding.Unicode.GetBytes(Request.CommandFull + "lngkend");
                stream.Write(data, 0, data.Length);

                String responseData = ReadMessage(stream);
                stream.Flush();
                stream.Close();
                response = new ClsCommand(responseData);
                client.Close();
            }
            catch (System.Exception exp)
            {
                RemoteConnected = false;
                Logs.WriteErrorLog(exp);
                response = new ClsCommand(Request.Receiver, Request.Sender, Request.Command, CommandString.Arg_Return + ClsCommand.cstSplitArgValue + CommandString.False);
            }
            return response;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public static void SendMsg(string msg, string ip, int port)
        {
            ClsCommand RequestCmd = new ClsCommand(
                   CommandString.StationServer,
                   CommandString.CentralServer,
                   CommandString.CMD_ReceiveInfo,
                   CommandString.Arg_Msg + ClsCommand.cstSplitArgValue +msg 
                   );

            ClsCommand response = SendSslMessage(RequestCmd, ip, port);
        }
    }
}
