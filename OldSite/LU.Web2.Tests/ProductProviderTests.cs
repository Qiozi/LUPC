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
    public class ProductProviderTests
    {
        [TestMethod()]
        public void GetSysCommentsTest()
        {
            var query = LU.BLL.ProductProvider.GetSysComments(LU.Web2.Tests.Util.DBContext);
            Assert.IsTrue(query.Count > 0);
        }

        [TestMethod()]
        public void GetHomeSystemTest()
        {
            var quer = ProductProvider.GetHomeSystem(Web2.Tests.Util.DBContext, Model.Enums.CountryType.CAD);
            Assert.IsTrue(quer[0].Parts.Count > 0);
            Assert.IsTrue(quer.Count ==4);
        }
    }
}