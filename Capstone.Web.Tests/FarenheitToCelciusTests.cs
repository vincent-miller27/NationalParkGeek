using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Models;

namespace Capstone.Web.Tests
{
    [TestClass]
    public class FarenheitToCelciusTests
    {
        [TestMethod]
        public void FarenheitToCelciusTest1()
        {
            Weather w = new Weather();

            int result = w.FarenheitToCelcius(32);

            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void FarenheitToCelciusTest2()
        {
            Weather w = new Weather();

            int result = w.FarenheitToCelcius(67);

            Assert.AreEqual(19, result);
        }

        [TestMethod]
        public void FarenheitToCelciusTest3()
        {
            Weather w = new Weather();

            int result = w.FarenheitToCelcius(127);

            Assert.AreEqual(52, result);
        }

        [TestMethod]
        public void FarenheitToCelciusTest4()
        {
            Weather w = new Weather();

            int result = w.FarenheitToCelcius(3);

            Assert.AreEqual(-16, result);
        }

        [TestMethod]
        public void FarenheitToCelciusTest5()
        {
            Weather w = new Weather();

            int result = w.FarenheitToCelcius(212);

            Assert.AreEqual(100, result);
        }
    }
}
