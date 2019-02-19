using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.Helper
{
	public class FFHelper
	{
        public FFHelper() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentPath"></param>
        /// <returns></returns>
        public string GetLastStorePath(string parentPath)
        {
            if (!System.IO.Directory.Exists(parentPath))
            {
                System.IO.Directory.CreateDirectory(parentPath);
            }

            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(parentPath);
            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();
            //string last_dir = "";
            int max_dir = 0;
            for (int i = 0; i < dirs.Length; i++)
            {
                int folder_name;
                int.TryParse(dirs[i].Name, out folder_name);
                if (folder_name > max_dir)
                    max_dir = folder_name;
            }

            if (max_dir > 0)
            {
                return parentPath + max_dir.ToString() + "\\";
            }
            else
            {
                string path = parentPath + DateTime.Now.ToString("yyyyMMdd") + "\\";
                System.IO.Directory.CreateDirectory(path);
                return path;
            }
        }

	}
}
