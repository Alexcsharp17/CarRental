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
            var user = new ApplicationUser { Email = "dungeon.master@gmail.com", UserName = "VanDarkholme" };
            var leatherMan = new ApplicationUser { Email = "leather.man@gmail.com", UserName = "leather.man@gmail.com" };

            string password = "ad46D_ewr3";
            var result = userManager.Create(admin, password);
            var result2 = userManager.Create(user, password);
            var result3 = userManager.Create(leatherMan, password);

            // If user was created succesfully
            if (result.Succeeded)
            {
                // Add roles for user
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
            }
            // If user was created succesfully
            if (result2.Succeeded)
            {
                // Add roles for user
                userManager.AddToRole(user.Id, role2.Name);
            }

            // If user was created succesfully
            if (result3.Succeeded)
            {
                // Add roles for user
                userManager.AddToRole(leatherMan.Id, role2.Name);
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