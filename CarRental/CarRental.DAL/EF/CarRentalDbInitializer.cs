using CarRental.DAL.Entities;
using CarRental.DAL.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace CarRental.DAL.EF
{
    public class CarRentalDbInitializer : CreateDatabaseIfNotExists<ApplicationContext>
    {
        protected override void Seed(ApplicationContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            //Create a list of cars for database first initialization
            //If json deserializer doesnt return cars then add that default list
            List<Car> cars = new List<Car>();
            cars.Add(new Car { Id = 1, Name = "Fiat 500", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Fiat", Price = 24, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" });
            cars.Add(new Car { Id = 2, Name = "Ford Focus", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/car1.jpg" });
            cars.Add(new Car { Id = 3, Name = "Volkswagen Polo", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 5, Image = "/Content/img/cars/car2.jpg" });
            cars.Add(new Car { Id = 4, Name = "Volkswagen Golf", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add(new Car { Id = 5, Name = "Vauxhall Corsa", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Vauxhall", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add(new Car { Id = 6, Name = "Toyota Aygo", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 1, Image = "/Content/img/cars/opel-vivaro.jpg" });
            //cars.Add(new Car { Id = 7, Name = "Ford Fiesta", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 1, Image = "/Content/img/cars/Duster.jpg" });
            //cars.Add(new Car { Id = 8, Name = "Opel Corsa", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 1, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" });
            //cars.Add(new Car { Id = 9, Name = "Opel Crossland X", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" });
            //cars.Add( new Car { Id = 10, Name = "Fiat Tipo", AirConditon = false, AutomaticTransm = true, DriveUnit = "four", Capacity = 5, Manufacturer = "Fiat", Price = 43, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/mercedes-sprinter.jpg" });
            //cars.Add( new Car { Id = 11, Name = "Hyundai i10", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Hyundai", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 6, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 12, Name = "Toyota iQ", AirConditon = true, AutomaticTransm = false, DriveUnit = "rear", Capacity = 2, Manufacturer = "Toyota", Price = 54, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 7, FuelConsump = 3, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 13, Name = "Peugeot 208", AirConditon = true, AutomaticTransm = true, DriveUnit = "rear", Capacity = 5, Manufacturer = "Peugeot", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 4, Image = "/Content/img/cars/Peugeot 301.jpg" });
            //cars.Add( new Car { Id = 14, Name = "Opel Astra", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" });
            //cars.Add( new Car { Id = 15, Name = "Fiat 500", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Fiat", Price = 24, CarType = "econom", Door = 4, FuelType = "Dysel", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" });
            //cars.Add( new Car { Id = 16, Name = "Ford Fiesta8", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Ford", Price = 35, CarType = "econom", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/car1.jpg" });
            //cars.Add( new Car { Id = 17, Name = "Volkswagen x", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Volkswagen", CarType = "econom", Price = 34, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 5, Image = "/Content/img/cars/car2.jpg" });
            //cars.Add( new Car { Id = 18, Name = "Volkswagen Tempo", AirConditon = false, AutomaticTransm = true, DriveUnit = "rear", Capacity = 2, Manufacturer = "Volkswagen", CarType = "econom", Price = 37, Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 19, Name = "Alpha2", AirConditon = false, AutomaticTransm = false, DriveUnit = "rear", Capacity = 3, Manufacturer = "Alpha", CarType = "business", Price = 25, Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 20, Name = "Toyota BM", AirConditon = false, AutomaticTransm = true, DriveUnit = "front", Capacity = 5, Manufacturer = "Toyota", Price = 23, CarType = "business", Door = 4, FuelType = "Dysel", EngSize = 3, FuelConsump = 1, Image = "/Content/img/cars/opel-vivaro.jpg" });
            //cars.Add( new Car { Id = 21, Name = "Ford M1", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Ford", Price = 42, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 1, Image = "/Content/img/cars/Duster.jpg" });
            //cars.Add( new Car { Id = 22, Name = "Opel Gustavo", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Opel", Price = 37, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 1, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" });
            //cars.Add( new Car { Id = 23, Name = "Opel VX", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "business", Door = 4, FuelType = "Petrol", EngSize = 4, FuelConsump = 4, Image = "/Content/img/cars/Duster.jpg" });
            //cars.Add( new Car { Id = 24, Name = "Fiat 3", AirConditon = false, AutomaticTransm = true, DriveUnit = "four", Capacity = 5, Manufacturer = "Fiat", Price = 43, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/mercedes-sprinter.jpg" });
            //cars.Add( new Car { Id = 25, Name = "Hyundai i15", AirConditon = true, AutomaticTransm = true, DriveUnit = "front", Capacity = 4, Manufacturer = "Hyundai", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 6, FuelConsump = 2, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 26, Name = "Toyota Quba", AirConditon = true, AutomaticTransm = false, DriveUnit = "rear", Capacity = 2, Manufacturer = "Toyota", Price = 54, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 7, FuelConsump = 3, Image = "/Content/img/cars/Logan.jpg" });
            //cars.Add( new Car { Id = 27, Name = "Peugeot 55", AirConditon = true, AutomaticTransm = true, DriveUnit = "rear", Capacity = 5, Manufacturer = "Peugeot", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 3, FuelConsump = 4, Image = "/Content/img/cars/Peugeot 301.jpg" });
            //cars.Add( new Car { Id = 28, Name = "Opel Remi", AirConditon = true, AutomaticTransm = false, DriveUnit = "front", Capacity = 5, Manufacturer = "Opel", Price = 32, CarType = "premium", Door = 4, FuelType = "Petrol", EngSize = 2, FuelConsump = 4, Image = "/Content/img/cars/opel-vivaro.jpg" });

            List<Car> deserCars = GetDataFromJson();

            //Add cars to database    
            List<Car> carstodb = deserCars != null ? deserCars : cars;
            context.Cars.AddRange(carstodb);

            //Create set of roles
            var role1 = new IdentityRole { Name = "admin" };
            var role2 = new IdentityRole { Name = "user" };
            var role3 = new IdentityRole { Name = "manager" };

            //Add roles to database
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);

            // Create users
            var admin = new ApplicationUser { Email = "somemail@mail.ru", UserName = "somemail@mail.ru" };
            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);

            // If user was created succesfully
            if (result.Succeeded)
            {
                // Add roles for user
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
            }

            base.Seed(context);



        }
        public List<Car> GetDataFromJson()
        {
            List<Car> cars = new List<Car>();

            //Map path is used because in Asp.Net default directory for files
            //located in another folder
            var path = HttpContext.Current.Server.MapPath(@"~/App_Data/cars.json");

            string file = File.ReadAllText(path);
            cars = JsonSerializer.Deserialize<List<Car>>(file);
            return cars;
        }
    }
}