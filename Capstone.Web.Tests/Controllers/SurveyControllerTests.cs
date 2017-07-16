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


namespace Capstone.Web.Tests.Controllers
{
    [TestClass]
    public class SurveyControllerTests
    {
        private string connectionString = @"Data Source=localhost\sqlexpress;Initial Catalog=NationalParkGeek;Integrated Security=True";
        private TransactionScope tran;

        //To allow for successful testing of SurveySubmit HttpPost
        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();
                
                cmd = new SqlCommand("INSERT INTO survey_result VALUES ('GNP', 'test@test.com', 'test', 'test');", connection);
                cmd.ExecuteNonQuery();
            }

        }

        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod()]
        public void HomeController_IndexAction_ReturnIndexView()
        {
            //Arrange
            SurveyController controller = new SurveyController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void HomeController_SurveySubmitAction_ReturnSurveySubmitView()
        {
            //Arrange
            SurveyController controller = new SurveyController();

            //Act
            ViewResult result = controller.SurveySubmit() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("SurveySubmit", result.ViewName);
        }

        [TestMethod()]
        public void HomeController_SurveySubmitAction_RedirectToIndexView()
        {
            //Arrange
            SurveySqlDAL mockDal = new SurveySqlDAL(connectionString);
            SurveyController controller = new SurveyController();
            SurveyForm model = new SurveyForm()
            {
                ParkCode = "gnp",
                EmailAddress = "test@test.com",
                State = "test",
                ActivityLevel = "test"
            };

            //Act
            mockDal.SubmitSurvey(model);
            RedirectToRouteResult result = controller.SurveySubmit(model) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
