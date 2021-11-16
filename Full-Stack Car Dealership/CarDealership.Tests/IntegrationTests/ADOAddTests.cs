using CarDealership.Data;
using CarDealership.Data.ADO;
using CarDealership.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Tests.IntegrationTests
{
    [TestFixture]
    public class ADOAddTests
    {
        [SetUp]
        public void Init()
        {
            using (var cn = new SqlConnection(Settings.GetConnectionString()))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cn;
                cn.Open();

                cmd.ExecuteNonQuery();
            }
        }

        //tests for adding to tables
        [Test]
        public void CanAddPurchase()
        {
            Purchase purchase = new Purchase();
            var repo = new PurchaseRepositoryADO();

            purchase.PurchaseType = "Cash";
            purchase.PurchasePrice = 25000;
            purchase.CustomerID = 1;
            purchase.UserID = "11111111-1111-1111-1111-111111111111";
            purchase.VehicleID = 1;
            purchase.IsAvailable = false;

            repo.Insert(purchase);

            Assert.AreEqual(4, purchase.PurchaseID);
        }

        [Test]
        public void CanAddVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehicleRepositoryADO();

            vehicleToAdd.VehicleYear = 2016;
            vehicleToAdd.MakeID = 3;
            vehicleToAdd.ModelID = 1;
            vehicleToAdd.Price = 24000;
            vehicleToAdd.Mileage = 1000;
            vehicleToAdd.Vin = "1GNDU23W27D181467";
            vehicleToAdd.MSRP = 26000;
            vehicleToAdd.VehicleDescription = "Beauty";
            vehicleToAdd.VehicleType = "New";
            vehicleToAdd.ImageFileName = "placeholder.png";
            vehicleToAdd.BodyStyleID = 1;
            vehicleToAdd.TransmissionID = 2;
            vehicleToAdd.BodyColorID = 3;
            vehicleToAdd.InteriorColorID = 2;
            vehicleToAdd.IsAvailable = true;
            vehicleToAdd.IsFeatured = true;

            repo.Insert(vehicleToAdd);

            Assert.AreEqual(9, vehicleToAdd.VehicleID);
        }

        [Test]
        public void CanAddSpecial()
        {
            Special specialToAdd = new Special();
            var repo = new SpecialRepositoryADO();

            specialToAdd.Title = "The Greatest Sale Ever!";
            specialToAdd.SpecialDescription = "We're offering 50% off all used vehicles until the end of the day!";
            specialToAdd.UserID = "00000000-0000-0000-0000-000000000000";

            repo.Insert(specialToAdd);

            Assert.AreEqual(4, specialToAdd.SpecialID);
        }

        [Test]
        public void CanAddContact()
        {
            Contact contact = new Contact();
            var repo = new ContactRepositoryADO();

            contact.Email = "abc@gmail.com";
            contact.FullName = "John Doe";
            contact.Message = "test message";
            contact.Vin = "1KKUKKE30AR251408";

            repo.Insert(contact);

            Assert.AreEqual(6, contact.ContactID);
        }

        [Test]
        public void CanAddCustomer()
        {
            Customer customer = new Customer();
            var repo = new CustomerRepositoryADO();

            customer.FullName = "John Doe";
            customer.Email = "bca@yahoo.com";
            customer.Phone = "111-111-1111";
            customer.Street1 = "12 road st";
            customer.Street2 = null;
            customer.City = "New York";
            customer.State = "AK";
            //look into making this an int but for now just leave it and get back to it later if time permits
            customer.ZipCode = "11111";

            repo.Insert(customer);

            Assert.AreEqual(4, customer.CustomerID);
        }

        [Test]
        public void CanAddMake()
        {
            Make make = new Make();
            var repo = new MakeRepositoryADO();

            make.MakeName = "Ford";
            make.UserID = "00000000-0000-0000-0000-000000000000";

            repo.Insert(make);

            Assert.AreEqual(5, make.MakeID);
        }

        [Test]
        public void CanAddModel()
        {
            Model model = new Model();
            var repo = new ModelRepositoryADO();

            model.ModelName ="F150";
            model.MakeID = 2;
            model.UserID = "00000000-0000-0000-0000-000000000000";
        }
    }
}
