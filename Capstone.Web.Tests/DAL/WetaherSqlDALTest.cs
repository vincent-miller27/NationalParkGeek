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
    public class WetaherSqlDALTest
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private TransactionScope tran;
        private int numberOfForecasts = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(*) FROM weather w JOIN park p on p.parkCode = w.parkCode WHERE p.parkCode = 'gnp';", connection);
                numberOfForecasts = (int)cmd.ExecuteScalar();
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetForecastsTest()
        {
            //Arrange
            WeatherSqlDAL weatherSqlDal = new WeatherSqlDAL(connectionString, true);

            //Act
            List<Weather> forecasts = weatherSqlDal.GetForecast("gnp");

            //Assert
            Assert.IsNotNull(forecasts);
            Assert.AreEqual(numberOfForecasts, forecasts.Count);
        }
    }
}
