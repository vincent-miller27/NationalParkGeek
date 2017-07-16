using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;
using Moq;
using System.Web.Routing;
using System.Web;

namespace Capstone.Web.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void HomeController_IndexAction_ReturnIndexView()
        {
            //Arrange
            HomeController controller = new HomeController();

            //Act
            ViewResult result = controller.Index() as ViewResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod()]
        public void HomeController_ParkDetailAction_ReturnParkDetailView()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Mock Session Object
            Mock<HttpSessionStateBase> mockSession = new Mock<HttpSessionStateBase>();

            // Mock Http Context Request for Controller
            // because Session doesn't exist unless MVC actually receives a "request"
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();

            // When the Controller calls this.Session it will get a mock session
            mockContext.Setup(c => c.Session).Returns(mockSession.Object);

            // Assign the context property on the controller to our mock context which uses our mock session
            controller.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), controller);


            // Act
            ViewResult result = controller.ParkDetail("gnp") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkDetail", result.ViewName);
        }

        [TestMethod()]
        public void HomeController_ParkDetailAction_RedirectToParkDetailView()
        {
            //Arrange
            HomeController controller = new HomeController();

            // Mock Session Object
            Mock<HttpSessionStateBase> mockSession = new Mock<HttpSessionStateBase>();

            // Mock Http Context Request for Controller
            // because Session doesn't exist unless MVC actually receives a "request"
            Mock<HttpContextBase> mockContext = new Mock<HttpContextBase>();

            // When the Controller calls this.Session it will get a mock session
            mockContext.Setup(c => c.Session).Returns(mockSession.Object);

            // Assign the context property on the controller to our mock context which uses our mock session
            controller.ControllerContext = new ControllerContext(mockContext.Object, new RouteData(), controller);

            //Act
            RedirectToRouteResult result = controller.ParkDetail(false) as RedirectToRouteResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("ParkDetail", result.RouteValues["action"]);
        }
    }
}