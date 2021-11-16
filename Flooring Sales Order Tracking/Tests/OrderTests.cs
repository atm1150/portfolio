using Assessment.Models;
using Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class OrderTests
    {
        LiveOrderRepo liveOrderRepo = new LiveOrderRepo();
        TestOrderRepo testOrderRepo = new TestOrderRepo();

        List<Order> orderList = new List<Order>();

        static Order sampleOrder = new Order
        {
            OrderNumber = 1,
            CustomerName = "Wise",
            State = "OH",
            TaxRate = 6.25m,
            ProductType = "Wood",
            Area = 100m,
            CostPerSquareFoot = 5.15m,
            LaborCostPerSquareFoot = 4.75m,
        };

        string stringSample = "1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88";

        private static Order ReturnOrder()
        {
            return sampleOrder;
        }

        [TestCase("1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88")]
        public void TestConvertLineToOrderObject(string data)
        {
            Order orderObject = testOrderRepo.ConvertLineToOrderObject(data);

            Assert.AreEqual(sampleOrder.OrderNumber, orderObject.OrderNumber);
            Assert.AreEqual(sampleOrder.CustomerName, orderObject.CustomerName);
            Assert.AreEqual(sampleOrder.State, orderObject.State);
            Assert.AreEqual(sampleOrder.TaxRate, orderObject.TaxRate);
            Assert.AreEqual(sampleOrder.ProductType, orderObject.ProductType);
            Assert.AreEqual(sampleOrder.Area, orderObject.Area);
            Assert.AreEqual(sampleOrder.CostPerSquareFoot, orderObject.CostPerSquareFoot);
            Assert.AreEqual(sampleOrder.LaborCostPerSquareFoot, orderObject.LaborCostPerSquareFoot);
            Assert.AreEqual(sampleOrder.MaterialCost, orderObject.MaterialCost);
            Assert.AreEqual(sampleOrder.LaborCost, orderObject.LaborCost);
            Assert.AreEqual(sampleOrder.Tax, orderObject.Tax);
            Assert.AreEqual(sampleOrder.Total, orderObject.Total);
        }

        [TestCase("1,Wise,OH,6.25,Wood,100.00,5.15,4.75,515.00,475.00,61.88,1051.88")]
        public void LiveConvertLineToOrderObject(string data)
        {
            Order orderObject = liveOrderRepo.ConvertLineToOrderObject(data);

            Assert.AreEqual(sampleOrder.OrderNumber, orderObject.OrderNumber);
            Assert.AreEqual(sampleOrder.CustomerName, orderObject.CustomerName);
            Assert.AreEqual(sampleOrder.State, orderObject.State);
            Assert.AreEqual(sampleOrder.TaxRate, orderObject.TaxRate);
            Assert.AreEqual(sampleOrder.ProductType, orderObject.ProductType);
            Assert.AreEqual(sampleOrder.Area, orderObject.Area);
            Assert.AreEqual(sampleOrder.CostPerSquareFoot, orderObject.CostPerSquareFoot);
            Assert.AreEqual(sampleOrder.LaborCostPerSquareFoot, orderObject.LaborCostPerSquareFoot);
            Assert.AreEqual(sampleOrder.MaterialCost, orderObject.MaterialCost);
            Assert.AreEqual(sampleOrder.LaborCost, orderObject.LaborCost);
            Assert.AreEqual(sampleOrder.Tax, orderObject.Tax);
            Assert.AreEqual(sampleOrder.Total, orderObject.Total);
        }

        //[TestCaseSource(nameof(ReturnOrder))]
        [Test]
        public void TestObjecttoLine()
        {
            string result = testOrderRepo.ConvertOrderObjectToLine(sampleOrder);

            Assert.AreEqual(stringSample, result);
        }

        //[TestCaseSource(nameof(sampleOrder))]
        [Test]
        public void LiveObjecttoLine()
        {
            
            string result = liveOrderRepo.ConvertOrderObjectToLine(sampleOrder);

            Assert.AreEqual(stringSample, result);
        }
    }
}
