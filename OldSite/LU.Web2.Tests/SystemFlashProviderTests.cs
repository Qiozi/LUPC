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
    public class SystemFlashProviderTests
    {
        [TestMethod()]
        public void GetSystemForFlashTest()
        {
            var begin = DateTime.Now;
            var adj = 0M;
            var query = SystemFlashProvider.GetSystemForFlash(LU.Web2.Tests.Util.DBContext, 220958, out adj );
            var tt = DateTime.Now - begin;

            Assert.IsTrue(query.Count > 0 && query[0].SubParts.Count > 0);
        }

        
    }
}