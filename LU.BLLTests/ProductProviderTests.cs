using Microsoft.VisualStudio.TestTools.UnitTesting;
using LU.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.Tests
{
    [TestClass()]
    public class ProductProviderTests
    {
        [TestMethod()]
        public void GetProductsTest()
        {
            var query = LU.BLL.ProductProvider.GetProducts()
            Assert.Fail();
        }
    }
}