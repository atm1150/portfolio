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
    public class ADODeleteTests
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
        public void CanDeleteVehicle()
        {
            Vehicle vehicle = new Vehicle();
            var repo = new VehicleRepositoryADO();

            //make sure entry had data before making sure the delete function works
            vehicle = repo.GetbyID(1);
            Assert.IsNotNull(vehicle);

            repo.Delete(1);

            vehicle = repo.GetbyID(1);

            Assert.IsNull(vehicle);
        }

        [Test]
        public void CanDeleteSpecial()
        {
            Special special = new Special();
            var repo = new SpecialRepositoryADO();
            List<Special> specials = repo.GetAll().ToList();

            //making sure data existed in an entry before testing the functionality of delete
            special.SpecialID = 1;
            Assert.AreEqual(3, specials.Count());

            repo.Delete(1);

            /*special = repo.GetByID(1);
            Assert.IsNull(special); */
            specials = repo.GetAll().ToList();

            Assert.AreEqual(2, specials.Count());
        }
    }
}

