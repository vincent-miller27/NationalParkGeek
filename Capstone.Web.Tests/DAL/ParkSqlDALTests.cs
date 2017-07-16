using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class ParkSqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private TransactionScope tran;
        private int numberOfParks = 0;

        //To allow for successful testing of SurveySubmit HttpPost
        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();
                
                cmd = new SqlCommand("SELECT COUNT(*) FROM park;", connection);
                numberOfParks = (int)cmd.ExecuteScalar();
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetParksTest()
        {
            //Arrange
            ParkSqlDAL parkSqlDal = new ParkSqlDAL(connectionString);

            //Act
            List<Park> parks = parkSqlDal.GetParks();

            //Assert
            Assert.IsNotNull(parks);
            Assert.AreEqual(numberOfParks, parks.Count);
        }

        [TestMethod]
        public void GetSpecificParkTest()
        {
            //Arrange
            ParkSqlDAL parkSqlDal = new ParkSqlDAL(connectionString);

            //Act
            Park park = parkSqlDal.GetSpecificPark("GNP");

            //Assert
            Assert.IsNotNull(park);
        }
    }
}
