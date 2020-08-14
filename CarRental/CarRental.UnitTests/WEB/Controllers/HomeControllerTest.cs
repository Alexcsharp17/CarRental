using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Controllers;
using CarRental.WEB.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CarRental.UnitTests.WEB.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {
            //Arrenge
            var mock = new Mock<IDatAcessService>();
            var mock2 = new Mock<IOrderService>();
            var expected = CarSamples();
            
            mock.Setup(x => x.Cars).Returns(expected.cars);
            mock.Setup(x => x.FindCar(1)).Returns(CarSamples(new int[] {1 }).cars.FirstOrDefault());
            mock.Setup(x => x.FindCar(2)).Returns(CarSamples(new int[] {2 }).cars.FirstOrDefault());

            HomeController controller = new HomeController(mock.Object,mock2.Object);
            string expModelName = "Index";
           
            //Act
            ViewResult result = controller.Index(sort:"ascending") as ViewResult;
            string idsArr = "1I2I";
          
            ViewResult result2 = controller.Index(idsArr) as ViewResult;

            //Assert
            Assert.IsTrue(expected.cars != null);
            Assert.IsNotNull(result);
            Assert.AreEqual(expModelName, result.ViewName);

            if (expected.popcars.Count()>3)
            {
                for (int i = 0; i < expected.popcars.Count; i++)
                {
                    Assert.AreEqual(expected.popcars[i], result.ViewBag.popCar[i]);
                }
                Assert.AreEqual(result.ViewBag.popCar.Count, expected.popcars.Count);
            }
            
           

        }

        public (List<CarDTO> cars, List<int> popcars) CarSamples(int[] idss = null)
        {
         
            List<CarDTO> cars = new List<CarDTO>()
            {
                new CarDTO { Id = 1, Name = "Ford Focus", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/car1.jpg",Popular=10 },
                new CarDTO { Id = 2, Name = "Volkswagen Polo", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 5, Image = "/Content/img/cars/car2.jpg",Popular=9 },
                new CarDTO { Id = 4, Name = "Volkswagen Golf", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg", Popular=8 },
                new CarDTO { Id = 5, Name = "Vauxhall Corsa", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Vauxhall", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Logan.jpg", Popular=7 },
                new CarDTO { Id = 6, Name = "Toyota Aygo", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 1, Image = "/Content/img/cars/opel-vivaro.jpg", Popular=6 },
                new CarDTO { Id = 7, Name = "Ford Fiesta", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 1, Image = "/Content/img/cars/Duster.jpg" , Popular=5},
                new CarDTO { Id = 8, Name = "Opel Corsa", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 1, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg", Popular=4 },
                new CarDTO { Id = 9, Name = "Opel Crossland X", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" , Popular=3 },
            };
            List<int> popcars = new List<int>();
            int[] ids = cars.OrderBy(x => x.Popular).Select(x => x.Id).ToArray();

            for (int i = 0; i < ids.Length / 3; i++)
            {
                popcars.Add(i);
            }
            if (idss != null)
            {
                List<CarDTO> carsorted = new List<CarDTO>();
                foreach (var i in ids)
                {
                    carsorted.Add(cars.FirstOrDefault(c => c.Id == i));
                }
            }
            return (cars,popcars);
        }
       
    }
}
