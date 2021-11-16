using Assessment.Models;
using BLL.Factories;
using BLL.Managers;
using Data;
using Models;
using Models.Interfaces;
using Models.Responses;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class TaxTests
    {
        TestTaxRepo testTaxRepo = new TestTaxRepo();
        LiveTaxRepo liveTaxRepo = new LiveTaxRepo();

        Taxes pennTaxObject = new Taxes
        {
            State = "Pennsylvania",
            StateAbbreviation = "PA",
            TaxRate = 6.75M
        };

        [TestCase(4)]
        public void LoadTaxObjectsTest(int taxListCount)
        {
            string path = Path.GetFullPath(@"C:\Repos\online-net-atm1150\OOP_Assessment\Data\Text Files\Test");
            TestTaxRepo testTaxRepo = new TestTaxRepo(path);

            List<Taxes> listTaxes = testTaxRepo.LoadTaxes();
            int count = listTaxes.Count();

            Assert.AreEqual(taxListCount, count);
        }

        [TestCase("PA,Pennsylvania,6.75")]
        public void StringToTestTaxObjectTest(string stringTax)
        {
            TaxesResponse response = testTaxRepo.ConvertStringToTaxObject(stringTax);

            Assert.AreEqual(pennTaxObject.TaxRate, response.Taxes.TaxRate);
            Assert.AreEqual(pennTaxObject.State, response.Taxes.State);
            Assert.AreEqual(pennTaxObject.StateAbbreviation, response.Taxes.StateAbbreviation);
        }

        [TestCase("PA,Pennsylvania,6.75")]
        public void StringToLiveTaxObjectTest(string stringTax)
        {
            TaxesResponse response = liveTaxRepo.ConvertStringToTaxObject(stringTax);

            Assert.AreEqual(pennTaxObject.TaxRate, response.Taxes.TaxRate);
            Assert.AreEqual(pennTaxObject.State, response.Taxes.State);
            Assert.AreEqual(pennTaxObject.StateAbbreviation, response.Taxes.StateAbbreviation);
        }
    }
}
