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
            Car c1 = new Car { CarId = 1, Name = "Fiat 500", AirConditon =true, AutomaticTransm=false,DriveUnit="front",Capacity=5,Manufacturer="Fiat",Price=24,CarType="econom" , Door=4,FuelType="Petrol",EngSize=2,FuelConsump=4 };
            Car c2 = new Car { CarId = 2, Name = "Ford Focus", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c3 = new Car { CarId = 3, Name = "Volkswagen Polo", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c4 = new Car { CarId = 4, Name = "Volkswagen Golf", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c5 = new Car { CarId = 5, Name = "Vauxhall Corsa", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Vauxhall", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c6 = new Car { CarId = 6, Name = "Toyota Aygo", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c7 = new Car { CarId = 7, Name = "Ford Fiesta", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c8 = new Car { CarId = 8, Name = "Opel Corsa", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c9 = new Car { CarId = 9, Name = "Opel Crossland X", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c10 = new Car { CarId = 10, Name = "Fiat Tipo", AirConditon = false, AutomaticTransm = true, DriveUnit = "four", Capacity = 5, Manufacturer = "Fiat", Price = 43, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c11 = new Car { CarId = 11, Name = "Hyundai i10", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Hyundai", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c12 = new Car { CarId = 12, Name = "Toyota iQ", AirConditon = true, AutomaticTransm = false, DriveUnit = "rear", Capacity = 2, Manufacturer = "Toyota", Price = 54 , CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c13 = new Car { CarId = 13, Name = "Peugeot 208", AirConditon = true, AutomaticTransm = true, DriveUnit = "rear", Capacity = 5, Manufacturer = "Peugeot", Price = 32,  CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };
            Car c14= new Car { CarId = 14, Name = "Opel Astra", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4 };

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

           
        }
    }
}
