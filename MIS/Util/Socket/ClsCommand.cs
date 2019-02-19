using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Socket
{
    public class ClsCommand
    {
        public const char cstSplit = ';';   // 命令的分隔字元
        public const char cstSplitArg = '@'; //命令的參數的分割字元
        public const char cstSplitArgValue = '#';//命令的參數的Key和Value 分割字元

        public string CommandFull = "";   // 完整的命令字串

        public string Sender = "";
        public string Receiver = "";
        public string Command = "";
        public string arg1 = "";

        //參數分割后的列表
        public List<KeyValuePair<string, string>> Arguments = new List<KeyValuePair<string, string>>();
        public ClsCommand(string CommandString)
        {

            CommandFull = CommandString.Trim();
            string[] cmds = CommandFull.Split(cstSplit);
            if (cmds.Length >= 3)
            {
                Sender = cmds[0].Trim();
                Receiver = cmds[1].Trim();
                Command = cmds[2].Trim();
            }
            else
            {
                Sender = "Error";
                Receiver = "Error";
                Command = "Error";
            }

            if (cmds.Length >= 4)
                arg1 = cmds[3].Trim();
            if (arg1 != "")
            {
                string[] args = arg1.Split(cstSplitArg);
                for (int i = 0; i < args.Length; i++)
                {
                    string[] arg = args[i].Split(cstSplitArgValue);
                    if (arg.Length == 2)
                    {
                        KeyValuePair<string, string> itme = new KeyValuePair<string, string>(arg[0], arg[1]);
                        Arguments.Add(itme);
                    }
                }
            }

        }

        // 輸入命令的組成字串
        public ClsCommand(
            string parSender,
            string parReceiver,
            string parCommand,
            string parArg1)
        {
            Sender = parSender.Trim();
            Receiver = parReceiver.Trim();
            Command = parCommand.Trim();
            arg1 = parArg1.Trim();

            CommandFull =
                Sender + cstSplit +
                Receiver + cstSplit +
                Command + cstSplit +
                arg1;
            if (arg1 != "")
            {
                string[] args = arg1.Split(cstSplitArg);
                for (int i = 0; i < args.Length; i++)
                {
                    string[] arg = args[i].Split(cstSplitArgValue);
                    if (arg.Length == 2)
                    {
                        KeyValuePair<string, string> itme = new KeyValuePair<string, string>(arg[0], arg[1]);
                        Arguments.Add(itme);
                    }
                }
            }
        }


        /// <summary>
        /// 添加參數的 Key和Value 對
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="Value"></param>
        public void AddArguments(string Key, string Value)
        {
            KeyValuePair<string, string> itme = new KeyValuePair<string, string>(Key.Trim(), Value.Trim());
            Arguments.Add(itme);
            arg1 += cstSplitArg + Key.Trim() + cstSplitArgValue + Value.Trim();
        }

        /// <summary>
        /// 得到參數中的 Value
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string GetArgumentValue(string Key)
        {
            string Result = "";
            for (int i = 0; i < Arguments.Count; i++)
            {
                if (Arguments[i].Key == Key)
                {
                    Result = Arguments[i].Value;
                    break;
                }
            }
            return Result;
        }

        #region 过滤用于切割的字符
        /// <summary>
        /// 把 [[at]] 转换成 @
        /// 把 [[#]]  转换成 # 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeString(string str)
        {
            if (str.IndexOf("[[at]]") > 0)
                str = str.Replace("[[at]]", "@");
            if (str.IndexOf("[[#]]") > 0)
                str = str.Replace("[[#]]", "#");
            return str;
        }

        /// <summary>
        /// 把 @ 转换成 [[at]]
        /// 把 # 转换成 [[#]]
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecodeString(string str)
        {
            if (str.IndexOf("#") > 0)
                str = str.Replace("#", "[[#]]");
            if (str.IndexOf("@") > 0)
                str = str.Replace("@", "[[at]]");
            return str;
        }
        #endregion
    }
}
