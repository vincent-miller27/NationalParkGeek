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
    public class SurveySqlDALTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private TransactionScope tran;
        private int numberOfParksWithSurvey = 0;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                cmd = new SqlCommand("SELECT COUNT(DISTINCT parkCode) FROM survey_result", connection);
                numberOfParksWithSurvey = (int)cmd.ExecuteScalar();
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetSurveysTest()
        {
            //Arrange
            SurveySqlDAL surveySqlDal = new SurveySqlDAL(connectionString);

            //Act
            List<Survey> surveys = surveySqlDal.GetSurveys();

            //Assert
            Assert.IsNotNull(surveys);
            Assert.AreEqual(numberOfParksWithSurvey, surveys.Count);
        }

        [TestMethod()]
        public void SubmitSurveyTest()
        {
            //Arrange
            SurveySqlDAL surveySqlDAL = new SurveySqlDAL(connectionString);
            SurveyForm model = new SurveyForm()
            {
                ParkCode = "gnp",
                EmailAddress = "test@test.com",
                State = "test",
                ActivityLevel = "test"
            };

            //Act
            bool didWork = surveySqlDAL.SubmitSurvey(model);

            //Assert
            Assert.AreEqual(true, didWork);
        }
    }
}
