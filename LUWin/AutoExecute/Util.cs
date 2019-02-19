using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace AutoExecute
{
    public class Util
    {
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
    }
}
