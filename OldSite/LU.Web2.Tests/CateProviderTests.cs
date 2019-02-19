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
    public class CateProviderTests
    {
        [TestMethod()]
        public void GetCatesTest()
        {
            var query = LU.BLL.CateProvider.GetCates(LU.Web2.Tests.Util.DBContext);
            Assert.IsTrue(query.Count>0);
        }
    }
}