using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.EF
{
  public  class CarRentalDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            Car c1 = new Car { CarId = 1, Name = "Fiat 500", AirConditon =true, AutomaticTransm=false,DriveUnit="front",Capacity=5,Manufacturer="Fiat",Price=24,CarType="econom" , Door=4,FuelType="Petrol",EngSize=2,FuelConsump=4 ,Image= "/Content/img/cars/Duster.jpg" }; 
            Car c2 = new Car { CarId = 2, Name = "Ford Focus", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4,Image = "/Content/img/cars/car1.jpg" };
            Car c3 = new Car { CarId = 3, Name = "Volkswagen Polo", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 5,Image = "/Content/img/cars/car2.jpg" };
            Car c4 = new Car { CarId = 4, Name = "Volkswagen Golf", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" };
            Car c5 = new Car { CarId = 5, Name = "Vauxhall Corsa", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Vauxhall", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Logan.jpg" };
            Car c6 = new Car { CarId = 6, Name = "Toyota Aygo", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 1, Image = "/Content/img/cars/opel-vivaro.jpg" };
            Car c7 = new Car { CarId = 7, Name = "Ford Fiesta", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 1, Image = "/Content/img/cars/Duster.jpg" };
            Car c8 = new Car { CarId = 8, Name = "Opel Corsa", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 1, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" };
            Car c9 = new Car { CarId = 9, Name = "Opel Crossland X", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" };
            Car c10 = new Car { CarId = 10, Name = "Fiat Tipo", AirConditon = false, AutomaticTransm = true, DriveUnit = "four", Capacity = 5, Manufacturer = "Fiat", Price = 43, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/mercedes-sprinter.jpg" };
            Car c11 = new Car { CarId = 11, Name = "Hyundai i10", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Hyundai", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 6, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" };
            Car c12 = new Car { CarId = 12, Name = "Toyota iQ", AirConditon = true, AutomaticTransm = false, DriveUnit = "rear", Capacity = 2, Manufacturer = "Toyota", Price = 54 , CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 7, FuelConsump = 3, Image = "/Content/img/cars/Logan.jpg" };
            Car c13 = new Car { CarId = 13, Name = "Peugeot 208", AirConditon = true, AutomaticTransm = true, DriveUnit = "rear", Capacity = 5, Manufacturer = "Peugeot", Price = 32,  CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 4, Image = "/Content/img/cars/Peugeot 301.jpg" };
            Car c14= new Car { CarId = 14, Name = "Opel Astra", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" };
            Car c15 = new Car { CarId = 15, Name = "Fiat 500", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Fiat", Price = 24, CarType = "econom", Door = 4, FuelType = "Dysel", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" };
            Car c16 = new Car { CarId = 16, Name = "Ford Fiesta8", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/car1.jpg" };
            Car c17 = new Car { CarId = 17, Name = "Volkswagen x", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 5, Image = "/Content/img/cars/car2.jpg" };
            Car c18 = new Car { CarId = 18, Name = "Volkswagen Tempo", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" };
            Car c19 = new Car { CarId = 19, Name = "Alpha2", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Alpha", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Logan.jpg" };
            Car c20 = new Car { CarId = 20, Name = "Toyota BM", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Dysel", EngSize = 3, FuelConsump = 1, Image = "/Content/img/cars/opel-vivaro.jpg" };
            Car c21 = new Car { CarId = 21, Name = "Ford M1", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 1, Image = "/Content/img/cars/Duster.jpg" };
            Car c22 = new Car { CarId = 22, Name = "Opel Gustavo", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 1, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" };
            Car c23 = new Car { CarId = 23, Name = "Opel VX", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" };
            Car c24 = new Car { CarId = 24, Name = "Fiat 3", AirConditon = false, AutomaticTransm = true, DriveUnit = "four", Capacity = 5, Manufacturer = "Fiat", Price = 43, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/mercedes-sprinter.jpg" };
            Car c25 = new Car { CarId = 25, Name = "Hyundai i15", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Hyundai", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 6, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" };
            Car c26 = new Car { CarId = 26, Name = "Toyota Quba", AirConditon = true, AutomaticTransm = false, DriveUnit = "rear", Capacity = 2, Manufacturer = "Toyota", Price = 54, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 7, FuelConsump = 3, Image = "/Content/img/cars/Logan.jpg" };
            Car c27 = new Car { CarId = 27, Name = "Peugeot 55", AirConditon = true, AutomaticTransm = true, DriveUnit = "rear", Capacity = 5, Manufacturer = "Peugeot", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 4, Image = "/Content/img/cars/Peugeot 301.jpg" };
            Car c28 = new Car { CarId = 28, Name = "Opel Remi", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" };

            context.Cars.Add(c1);
            context.Cars.Add(c2);
            context.Cars.Add(c3);
            context.Cars.Add(c4);
            context.Cars.Add(c5);
            context.Cars.Add(c6);
            context.Cars.Add(c7);
            context.Cars.Add(c8);
            context.Cars.Add(c9);
            context.Cars.Add(c10);
            context.Cars.Add(c11);
            context.Cars.Add(c12);
            context.Cars.Add(c13);
            context.Cars.Add(c14);
            context.Cars.Add(c15);
            context.Cars.Add(c16);
            context.Cars.Add(c17);
            context.Cars.Add(c18);
            context.Cars.Add(c19);
            context.Cars.Add(c20);
            context.Cars.Add(c21);
            context.Cars.Add(c22);
            context.Cars.Add(c23);
            context.Cars.Add(c24);
            context.Cars.Add(c25);
            context.Cars.Add(c26);
            context.Cars.Add(c27);
            context.Cars.Add(c28);

        }
    }
}
