using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using System.Web.Mvc;
using CarRental.DAL.Entities;


namespace CarRental.UnitTests.Attributes
{
    [TestClass]
    public class AttributeTest
    {
        [TestMethod]
        public void TestDataValidator()
        {   //Assert
            DateValidAttribute atr = new DateValidAttribute();
            OrderDTO o1 = new OrderDTO
            { StartTime = new DateTime(2019, 1, 7),
             EndTime= new DateTime(2020, 2, 4)
            };
            OrderDTO o2 = new OrderDTO
            {
                StartTime = new DateTime(2020, 2, 4),
                EndTime = new DateTime(2019, 1, 7)
            };

            //Act
            bool res1 = atr.IsValid(o1 as object);
            bool res2 = atr.IsValid(o2 as object);

            //Assume
            Assert.AreEqual(res1, true);
            Assert.AreEqual(res2, false);

        }

       

    }
}
