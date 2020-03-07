using System;
using CarRental.BLL.Calculations;
using CarRental.BLL.Services;
using CarRental.DAL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CarRental.UnitTests.Controllers
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void PriceCalcIsCorrect()
        {
            Assert.AreEqual(50, PriceCalc.PricePerDays(50, 2));
            Assert.AreEqual(45, PriceCalc.PricePerDays(50, 5));
            Assert.AreEqual(35, PriceCalc.PricePerDays(50, 22));
            Assert.AreEqual(30, PriceCalc.PricePerDays(50, 27));
        }
        
    }
}
