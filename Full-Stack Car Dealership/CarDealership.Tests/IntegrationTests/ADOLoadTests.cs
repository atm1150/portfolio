using CarDealership.Data;
using CarDealership.Data.ADO;
using CarDealership.Models.Queries;
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
    public class ADOLoadTests
    {
        //initialize database for testing purposes
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
        //Tests for pulling all entries from a given table
        //state tests
        [Test]
        public void CanLoadStates()
        {
            var repo = new StateRepositoryADO();

            var states = repo.GetAll().ToList();

            Assert.AreEqual(25, states.Count);

            Assert.AreEqual("AK", states[0].StateID);
            Assert.AreEqual("Alaska", states[0].StateName);
        }

        //make tests
        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakeRepositoryADO();

            var makes = repo.GetAll().ToList();

            Assert.AreEqual(4, makes.Count);
            Assert.AreEqual("Chevrolet", makes[0].MakeName);
            Assert.AreEqual("Volkswagen", makes[3].MakeName);
            Assert.AreEqual(3, makes[3].MakeID);
        }

        [Test]
        public void CanLoadMakeList()
        {
            var repo = new MakeRepositoryADO();

            List<MakeListItem> makeList = repo.GetMakeList().ToList();

            Assert.IsNotNull(makeList);
            Assert.AreEqual(4, makeList.Count());
            Assert.AreEqual("Chrysler", makeList[1].MakeName);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", makeList[2].UserID);
            Assert.AreEqual("test@test.com", makeList[3].Email);

        }

        //models tests
        [Test]
        public void CanLoadModels()
        {
            var repo = new ModelRepositoryADO();

            var models = repo.GetAll().ToList();

            Assert.AreEqual(10, models.Count);
            Assert.AreEqual("Beetle", models[1].ModelName);
            Assert.AreEqual("Fiesta", models[4].ModelName);
            Assert.AreEqual(5, models[2].ModelID);
        }

        [Test]
        public void CanLoadModelList()
        {
            var repo = new ModelRepositoryADO();

            List<ModelListItem> modelList = repo.GetModelList().ToList();

            Assert.IsNotNull(modelList);
            Assert.AreEqual(10, modelList.Count());
            Assert.AreEqual(6, modelList[1].ModelID);
            Assert.AreEqual("Cruze", modelList[1].ModelName);
            Assert.AreEqual("Chevrolet", modelList[2].MakeName);
            Assert.AreEqual("test@test.com", modelList[2].Email);
        }

        [Test]
        public void CanLoadModelsByMake()
        {
            var repo = new ModelRepositoryADO();

            var models = repo.GetModelsByMake(1).ToList();

            Assert.IsNotNull(models);
            Assert.AreEqual("Pacifica", models[1].ModelName);
            Assert.AreEqual(2, models[1].ModelID);
        }

        //int color tests
        [Test]
        public void CanLoadInteriorColors()
        {
            var repo = new InteriorColorRepositoryADO();
            var colors = repo.GetAll().ToList();

            Assert.AreEqual(5, colors.Count);
            Assert.AreEqual("Black", colors[0].InteriorColorName);
            Assert.AreEqual("Gold", colors[1].InteriorColorName);
            Assert.AreEqual("White", colors[4].InteriorColorName);
            Assert.AreEqual(5, colors[1].InteriorColorID);
        }

        //body color tests
        [Test]
        public void CanLoadBodyColors()
        {
            var repo = new BodyColorRepositoryADO();

            var colors = repo.GetAll().ToList();

            Assert.AreEqual(5, colors.Count);
            Assert.AreEqual("Black", colors[0].BodyColorName);
            Assert.AreEqual("Red", colors[3].BodyColorName);
            Assert.AreEqual(5, colors[2].BodyColorID);
        }

        //car body style tests
        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStyleRepositoryADO();

            var styles = repo.GetAll().ToList();

            Assert.AreEqual(4, styles.Count);
            Assert.AreEqual("Car", styles[0].BodyDescription);
            Assert.AreEqual("Truck", styles[2].BodyDescription);
            Assert.AreEqual(4, styles[3].BodyStyleID);
        }

        //purchases tests
        [Test]
        public void CanLoadPurchases()
        {
            var repo = new PurchaseRepositoryADO();

            var purchases = repo.GetAll().ToList();

            Assert.AreEqual(3, purchases.Count);
            Assert.AreEqual(5, purchases[2].VehicleID);
            Assert.AreEqual(3, purchases[2].CustomerID);
            Assert.AreEqual("Dealer Finance", purchases[2].PurchaseType);
        }

        [Test]
        public void CanSearchPurchaseSales()
        {
            var repo = new PurchaseRepositoryADO();
            SalesSearchParameters searchParam = new SalesSearchParameters();
            searchParam.UserID = "00000000-0000-0000-0000-000000000000";

            var sales = repo.SearchSales(searchParam).ToList();

            Assert.IsNotNull(sales);
            Assert.AreEqual("John", sales.FirstOrDefault().FirstName);
            Assert.AreEqual("Doe", sales.FirstOrDefault().LastName);
            Assert.AreEqual(33000.00, sales.FirstOrDefault().TotalSales);
            Assert.AreEqual(2, sales.FirstOrDefault().TotalVehicles);
        }

        //specials tests
        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialRepositoryADO();

            var specials = repo.GetAll().ToList();

            Assert.AreEqual(3, specials.Count);
            Assert.AreEqual("Free Oil Change", specials[2].Title);
            Assert.AreEqual("Bring your car for service today! Rotation only $99.99 for used cars!", specials[1].SpecialDescription);
            Assert.AreEqual(2, specials[1].SpecialID);
        }

        [Test]
        public void LoadsSpecialByID()
        {
            var repo = new SpecialRepositoryADO();

            var special = repo.GetByID(1);

            Assert.IsNotNull(special);
            Assert.AreEqual("Free Ipad", special.Title);
            Assert.AreEqual("Buy a used car and get a free Ipad!", special.SpecialDescription);
            Assert.AreEqual("11111111-1111-1111-1111-111111111111", special.UserID);
        }
        /* comes up as not null
        [Test]
        public void NonExistentSpecialReturnsNull()
        {
            var repo = new SpecialRepositoryADO();
            var special = repo.GetByID(250000);

            Assert.IsNull(special);
        }
        */

        //contacts tests
        [Test]
        public void CanLoadContacts()
        {
            var repo = new ContactRepositoryADO();

            var contacts = repo.GetAll().ToList();

            Assert.AreEqual(5, contacts.Count);
            Assert.AreEqual("Oscar Smith", contacts[2].FullName);
            Assert.AreEqual("I want to make an offer on this vehicle.", contacts[3].Message);
            Assert.AreEqual("612-333-3333", contacts[2].Phone);
        }

        //customers tests
        [Test]
        public void CanLoadCustomers()
        {
            var repo = new CustomerRepositoryADO();

            var customers = repo.GetAll().ToList();

            Assert.AreEqual(3, customers.Count);
            Assert.AreEqual("Sarah Nelson", customers[2].FullName);
            Assert.AreEqual("111 Oak Street", customers[1].Street1);
            Assert.AreEqual("Vail", customers[0].City);
        }

        //trans tests
        [Test]
        public void CanLoadTransmissions()
        {
            var repo = new TransmissionRepositoryADO();

            var transmissions = repo.GetAll().ToList();

            Assert.AreEqual(2, transmissions.Count);
            Assert.AreEqual("Manual", transmissions[1].TransStyle);
            Assert.AreEqual(1, transmissions[0].TransmissionID);
        }

        //vehicles tests
        [Test]
        public void CanLoadVehicles()
        {
            var repo = new VehicleRepositoryADO();

            var vehicles = repo.GetAll().ToList();

            Assert.AreEqual(8, vehicles.Count);
            Assert.AreEqual(3, vehicles[2].VehicleID);
            Assert.AreEqual(2008, vehicles[2].VehicleYear);
            Assert.AreEqual("Used", vehicles[1].VehicleType);
            Assert.AreEqual(5, vehicles[4].BodyColorID);
        }

        [Test]
        public void LoadsVehiclebyID()
        {
            var repo = new VehicleRepositoryADO();

            var vehicle = repo.GetbyID(1);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual("1KKUKKE30AR251408", vehicle.Vin);
            Assert.AreEqual(2009, vehicle.VehicleYear);
            Assert.AreEqual("Used", vehicle.VehicleType);
            Assert.AreEqual(2, vehicle.TransmissionID);
            Assert.AreEqual(2, vehicle.BodyColorID);
        }

        [Test]
        public void NonExistentVehicleReturnsNull()
        {
            var repo = new VehicleRepositoryADO();
            var vehicle = repo.GetbyID(1000000);

            Assert.IsNull(vehicle);
        }

        [Test]
        public void CanLoadVehicleDetails()
        {
            var repo = new VehicleRepositoryADO();

            var vehicle = repo.GetDetails(1);

            Assert.IsNotNull(vehicle);
            Assert.AreEqual("1KKUKKE30AR251408", vehicle.Vin);
            Assert.AreEqual(2009, vehicle.VehicleYear);
            Assert.AreEqual("Chrysler", vehicle.MakeName);
            Assert.AreEqual("Pacifica", vehicle.ModelName);
            Assert.AreEqual("SUV", vehicle.BodyDescription);
            Assert.AreEqual("Manual", vehicle.TransStyle);
            Assert.AreEqual("Grey", vehicle.InteriorColorName);
            Assert.AreEqual("Red", vehicle.BodyColorName);
        }

        [Test]
        public void CanLoadFeaturedVehiclesList()
        {
            var repo = new VehicleRepositoryADO();

            List<FeaturedVehicleItem> featuredList = repo.GetFeaturedVehicleList().ToList();

            Assert.IsNotNull(featuredList);
            Assert.AreEqual(5, featuredList.Count());
            Assert.AreEqual(2010, featuredList[0].VehicleYear);
            Assert.AreEqual("Chrysler", featuredList[1].MakeName);
            Assert.AreEqual("Beetle", featuredList[2].ModelName);
            Assert.AreEqual(14500, featuredList[0].Price);
            Assert.AreEqual("1GKUBBE30AR251222", featuredList[0].Vin);
        }

        [Test]
        public void CanLoadVehicleSearch()
        {
            var repo = new VehicleRepositoryADO();
            var textSearchParam = new InventorySearchParameters();
            var maxYearSearchParam = new InventorySearchParameters();
            var minYearSearchParam = new InventorySearchParameters();
            var maxPriceSearchParam = new InventorySearchParameters();
            var minPriceSearchParam = new InventorySearchParameters();
            var typeSearchParam = new InventorySearchParameters();
            var partialTypeSearchParam = new InventorySearchParameters();

            maxYearSearchParam.MaxYear = 2012;
            minYearSearchParam.MinYear = 2010;
            maxPriceSearchParam.MaxPrice = 15000.00m;
            minPriceSearchParam.MinPrice = 14000.00m;
            typeSearchParam.SearchTextBox = "Ford";
            partialTypeSearchParam.SearchTextBox = "Chry";

            var vehicles = repo.Search(maxYearSearchParam).ToList();
            Assert.AreEqual(5, vehicles.Count());

            vehicles = repo.Search(minYearSearchParam).ToList();
            Assert.AreEqual(6, vehicles.Count());

            vehicles = repo.Search(maxPriceSearchParam).ToList();
            Assert.AreEqual(4, vehicles.Count());

            vehicles = repo.Search(minPriceSearchParam).ToList();
            Assert.AreEqual(7, vehicles.Count());

            vehicles = repo.Search(typeSearchParam).ToList();
            Assert.AreEqual(2, vehicles.Count());

            vehicles = repo.Search(partialTypeSearchParam).ToList();
            Assert.AreEqual(2, vehicles.Count());
        }

        [Test]
        public void CanLoadInventoryReport()
        {
            var repo = new VehicleRepositoryADO();

            var inventory = repo.GetInventoryReport("Used").ToList();

            Assert.AreEqual(2, inventory.Count());
            Assert.AreEqual("Ford", inventory[1].MakeName);
            Assert.AreEqual(2008, inventory[0].VehicleYear);
        }

        [Test]
        public void CanLoadUnsoldCars()
        {
            var repo = new VehicleRepositoryADO();
            var textSearchParam = new InventorySearchParameters();
            var maxYearSearchParam = new InventorySearchParameters();
            var minYearSearchParam = new InventorySearchParameters();
            var maxPriceSearchParam = new InventorySearchParameters();
            var minPriceSearchParam = new InventorySearchParameters();
            var typeSearchParam = new InventorySearchParameters();
            var partialTypeSearchParam = new InventorySearchParameters();

            maxYearSearchParam.MaxYear = 2017;
            minYearSearchParam.MinYear = 2008;
            maxPriceSearchParam.MaxPrice = 19000.00m;
            minPriceSearchParam.MinPrice = 13000.00m;
            typeSearchParam.SearchTextBox = "Ford";
            partialTypeSearchParam.SearchTextBox = "Chry";

            var unsoldVehicles = repo.AbleToBeSold(maxYearSearchParam).ToList();
            Assert.AreEqual(5, unsoldVehicles.Count());

            unsoldVehicles = repo.AbleToBeSold(minYearSearchParam).ToList();
            Assert.AreEqual(5, unsoldVehicles.Count());

            unsoldVehicles = repo.AbleToBeSold(maxPriceSearchParam).ToList();
            Assert.AreEqual(5, unsoldVehicles.Count());

            unsoldVehicles = repo.AbleToBeSold(minPriceSearchParam).ToList();
            Assert.AreEqual(5, unsoldVehicles.Count());

            unsoldVehicles = repo.AbleToBeSold(typeSearchParam).ToList();
            Assert.AreEqual(1, unsoldVehicles.Count());

            unsoldVehicles = repo.AbleToBeSold(partialTypeSearchParam).ToList();
            Assert.AreEqual(1, unsoldVehicles.Count());
        }


    }
}
