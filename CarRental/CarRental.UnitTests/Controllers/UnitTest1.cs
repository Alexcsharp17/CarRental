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
            Assert.AreEqual(Convert.ToInt32(Math.Round((50/24)*(DateTime.Now.AddDays(2)-DateTime.Now).TotalHours)), PriceCalc.PricePerDays(50, (DateTime.Now.AddDays(2) - DateTime.Now)));
          


        }
        
    }
}
