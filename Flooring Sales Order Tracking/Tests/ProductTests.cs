using Data;
using Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class ProductTests
    {
        TestProductRepo testProductRepo = new TestProductRepo();
        LiveProductRepo liveProductRepo = new LiveProductRepo();

        Product woodProductTest = new Product
        {
            CostPerSquareFoot = 5.15M,
            LaborCostPerSquareFoot = 4.75M,
            ProductType = "Wood"

        };

        [TestCase("Wood,5.15,4.75")]
        public void StringToTestProductObjectTest(string data)
        {
            Product productObject = new Product();

            productObject = testProductRepo.ConvertStringToProductObject(data);

            Assert.AreEqual(woodProductTest.CostPerSquareFoot, productObject.CostPerSquareFoot);
            Assert.AreEqual(woodProductTest.LaborCostPerSquareFoot, productObject.LaborCostPerSquareFoot);
            Assert.AreEqual(woodProductTest.ProductType, productObject.ProductType);
        }

        [TestCase("Wood,5.15,4.75")]
        public void StringToLiveProductObjectTest(string data)
        {
            Product productObject = new Product();

            productObject = liveProductRepo.ConvertStringToProductObject(data);

            Assert.AreEqual(woodProductTest.CostPerSquareFoot, productObject.CostPerSquareFoot);
            Assert.AreEqual(woodProductTest.LaborCostPerSquareFoot, productObject.LaborCostPerSquareFoot);
            Assert.AreEqual(woodProductTest.ProductType, productObject.ProductType);
        }
    }
}
