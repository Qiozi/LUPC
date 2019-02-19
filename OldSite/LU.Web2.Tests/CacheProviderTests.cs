using Microsoft.VisualStudio.TestTools.UnitTesting;
using LU.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.CateTests
{
    [TestClass()]
    public class CacheProviderTests
    {

        [TestMethod()]
        public void GetCatesTest2()
        {
            // 不缓存数据读取操作
            var beginDate = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                CateProvider.GetCates(LU.Web2.Tests.Util.DBContext);
            }
            var timespan = DateTime.Now - beginDate;
            System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\temp.txt", true, Encoding.UTF8);
            sw.WriteLine(string.Concat("不缓存：", timespan));
            sw.WriteLine("####################################");
            sw.Close();
            Assert.IsTrue(true);
        }
        [TestMethod()]
        public void GetCatesTest1()
        {
            // 缓存数据读取操作
            var beginDate = DateTime.Now;
            for (int i = 0; i < 100; i++)
            {
                CacheProvider.GetAllCates(LU.Web2.Tests.Util.DBContext);
            }
            var timespan = DateTime.Now - beginDate;
            System.IO.StreamWriter sw = new System.IO.StreamWriter("D:\\temp.txt", true, Encoding.UTF8);
            sw.WriteLine("####################################");
            sw.WriteLine(string.Concat("缓存数据：", timespan));
            sw.Close();
            Assert.IsTrue(true);
        }
        [TestMethod()]
        public void GetAllProdBaseInfosTest()
        {
            var list = CacheProvider.GetAllProdBaseInfos(LU.Web2.Tests.Util.DBContext, 350);
            Assert.IsTrue(list.Count > 0);
        }
    }
}