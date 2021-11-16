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
    public class ADOUpdateTests
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

        [Test]
        public void CanUpdateVehicle()
        {
            Vehicle vehicleToAdd = new Vehicle();
            var repo = new VehicleRepositoryADO();

            vehicleToAdd.VehicleYear = 2015;
            vehicleToAdd.MakeID = 3;
            vehicleToAdd.ModelID = 1;
            vehicleToAdd.Price = 18000;
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

            vehicleToAdd.VehicleDescription = "Smooth Ride!";
            vehicleToAdd.MSRP = 35000;
            vehicleToAdd.ImageFileName = "placeholder2.png";
            vehicleToAdd.IsFeatured = false;

            repo.Update(vehicleToAdd);

            var updatedVehicle = repo.GetbyID(9);

            Assert.AreEqual("Smooth Ride!", updatedVehicle.VehicleDescription);
            Assert.AreEqual("placeholder2.png", updatedVehicle.ImageFileName);
            Assert.AreEqual("New", updatedVehicle.VehicleType);
            Assert.AreEqual(9, updatedVehicle.VehicleID);
        }


    }
}
