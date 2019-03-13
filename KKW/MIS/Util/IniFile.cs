using System.Text;
using System.Runtime.InteropServices;


namespace Util
{
    public class IniFile
    {
        /// <summary>
        /// 标记名称
        /// </summary>
        public const string Section_Server = "LUComputer";        

        public const string Arg_Host = "HostIP";

        public const string Arg_Port = "Port";

        public const string Arg_Username = "Username";

        public const string Arg_PageSize = "OrderListPageSize";

        public string path;

        // 初始化
        public IniFile()
        {
            path = System.IO.Path.GetFullPath(".\\Config.ini");
        }
        public IniFile(string INIPath)
        {
            path = INIPath;
        }

        // 寫入ini檔
        public void WriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        // 讀出ini檔里的值
        public string ReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
            return temp.ToString();
        }

        // 讀出ini某個Section里所有的KeyName
        //public string[] ReadAllKey(string Section)
        //{
        //    const int MAX = 250 * 100;
        //    byte[] temp = new byte[MAX];

        //    int varReturn = GetPrivateProfileString(Section, null, "", temp, MAX, this.path);
        //    ASCIIEncoding ascii = new ASCIIEncoding();
        //    string varSections = ascii.GetString(temp);

        //    return varSections.Split(new char[1] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
        //}

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(
            string section,
            string key,
            string val,
            string filePath
            );
        //void WritePrivateProfileString(
        //    string section,
        //    string key,
        //    string val,
        //    string filePath
        //    )
        //{
        //    if (File.Exists(filePath))
        //    {
        //        List<string> cont = new List<string>();
        //        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

        //        bool add = false;
        //        //bool between = false;
        //        //bool sectionExist = false;
        //        foreach (var s in lines)
        //        {
        //            if (s.IndexOf("=") > -1)
        //            {
        //                if (s.Split(new char[] { '=' })[0] == key)
        //                {
        //                    cont.Add(key + "=" + val);
        //                    add = true;
        //                }
        //                else
        //                    cont.Add(s);
        //            }
        //            //if (s.IndexOf("]") > -1 && s != "[" + section + "]")
        //            //{
        //            //    between = false;
        //            //    if (sectionExist)
        //            //    {
        //            //        cont.Add(key + "=" + val);
        //            //        sectionExist = false;
        //            //    }
        //            //}

        //            //if (!between)
        //            //    cont.Add(s);
        //            //else
        //            //{
        //            //    if (s.IndexOf("=") > -1)
        //            //    {
        //            //        if (s.Split(new char[] { '=' })[0] == key)
        //            //        {
        //            //            cont.Add(key + "=" + val);
        //            //            add = true;
        //            //        }
        //            //        else
        //            //            cont.Add(s);
        //            //    }
        //            //}
        //            //if (s == "[" + section + "]")
        //            //{
        //            //    sectionExist = true;
        //            //    between = true;
        //            //}

        //        }
        //        //if (!add && !sectionExist)
        //        //{
        //        //    cont.Add("[" + section + "]");
        //        //    cont.Add(key + "=" + val);
        //        //}
        //        if (!add)
        //        {
        //            cont.Add(key + "=" + val);
        //        }
        //        StreamWriter sw = new StreamWriter(filePath, false, Encoding.UTF8);
        //        foreach (var s in cont)
        //            sw.WriteLine(s);
        //        sw.Close();
        //        sw.Dispose();
        //    }
        //}
        //[DllImport("kernel32")]
        //private static extern int GetPrivateProfileString(
        //    string section,
        //    string key,
        //    string def,
        //    byte[] retVal,
        //    int size,
        //    string filePath
        //    );

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(
            string section,
            string key,
            string def,
            StringBuilder retVal,
            int size,
            string filePath
            );

        //string GetPrivateProfileString(
        //    string section,
        //    string key,
        //    string def,
        //    StringBuilder retVal,
        //    int size,
        //    string filePath
        //    )
        //{
        //    if (File.Exists(filePath))
        //    {
        //        string[] lines = File.ReadAllLines(filePath,Encoding.UTF8);
        //        List<string> cont = new List<string>();
        //        //bool between = false;
        //        foreach (var s in lines)
        //        {
        //            //if (between)
        //            //    cont.Add(s);
        //            //if (s == "[" + section + "]")
        //            //{
        //            //    between = true;
        //            //}
        //            //if (s.IndexOf("]") > -1 && s != "[" + section + "]")
        //            //    between = false;
        //            if (s.Split(new char[] { '=' })[0] == key)
        //            {
        //                retVal.Append(s.Split(new char[] { '=' })[1]);

        //                return s.Split(new char[] { '=' })[1];
        //            }
        //        }
        //        //foreach (var s in cont)
        //        //{
        //        //    if (s.IndexOf("=") > -1)
        //        //    {
        //        //        if (s.Split(new char[] { '=' })[0] == key)
        //        //        {
        //        //            retVal.Append(s.Split(new char[] { '=' })[1]);

        //        //            return 1;
        //        //        }
        //        //    }
        //        //}
        //    }
        //    return null;
        //}
    }
}
