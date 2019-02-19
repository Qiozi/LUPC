using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Socket
{
    public class CommandString
    {
        /// <summary>
        /// 中央服務器
        /// </summary>
        public const string CentralServer = "CentralServer";
        /// <summary>
        /// 站端服務器
        /// </summary>
        public const string StationServer = "StationServer";
        /// <summary>
        /// 接收客户端消息
        /// </summary>
        public const string CMD_ReceiveInfo = "CMD_ReceiveInfo";
        /// <summary>
        /// 给客户端发送消息
        /// </summary>
        public const string CMD_SendMessage = "StationLogin";
        /// <summary>
        /// 对比版本
        /// </summary>
        public const string CMD_CompareVersion = "CompareVersion";

        public const string Arg_Version = "Arg_Version";

        public const string Arg_Msg = "Arg_Msg";

        public const string False = "false";
        public const string True = "true";
        /// <summary>
        /// 返回值
        /// </summary>
        public const string Arg_Return = "Return";
    }
}
