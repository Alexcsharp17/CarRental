using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Identity;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    /// <summary>
    /// Class represent Unit of Work , which specified in 
    /// UnitOfWork pattern. Using ninject i bind this class
    /// to IUnitOfWork Interface. That allows me to get acess
    /// to it's class methods and properties.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;

        public UnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
        }
        public List<ApplicationUser> GetUsers()
        {
            return db.Users.ToList();
        }
        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                }
                this.disposed = true;
            }
        }

        public IEnumerable<Car> Cars
        {
            get
            {

                return db.Cars.ToList();
            }
        }
        public IEnumerable<ApplicationUser> Users
        {
            get { return db.Users; }
        }
        public IEnumerable<Order> Orders
        {
            get { return db.Orders.ToList(); }
        }
        public void CreateCar(Entities.Car car)
        {
            if (car.Id == 0)
                db.Cars.Add(car);
            else
            {
                Car dbEntry = db.Cars.Find(car.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = car.Name;
                    dbEntry.AirConditon = car.AirConditon;
                    dbEntry.AutomaticTransm = car.AutomaticTransm;
                    dbEntry.Capacity = car.Capacity;
                    dbEntry.Manufacturer = car.Manufacturer;
                    dbEntry.Price = car.Price;
                    dbEntry.Image = car.Image;
                    dbEntry.Door = car.Door;
                    dbEntry.IsDeleted = car.IsDeleted;
                    dbEntry.FuelConsump = car.FuelConsump;
                    dbEntry.FuelType = car.FuelType;
                    dbEntry.EngSize = car.EngSize;
                }

            }
            db.SaveChanges();
        }
        public void CreateOrder(Entities.Order order)
        {
            if (order.Id == 0)
                db.Orders.Add(order);
            else
            {
                Order dbEntry = db.Orders.Find(order.Id);
                if (dbEntry != null)
                {
                    dbEntry.CarId = order.CarId;
                    dbEntry.Driver = order.Driver;
                    dbEntry.StartTime = order.StartTime;
                    dbEntry.EndTime = order.EndTime;
                    dbEntry.OrdSum = order.OrdSum;
                    dbEntry.Id = order.Id;
                    dbEntry.Status = order.Status;
                    dbEntry.ManagComment = order.ManagComment;
                }
            }
            db.SaveChanges();
        }


        public Car FindCar(int id)
        {
            var car = db.Cars.FirstOrDefault(c => c.Id == id);
            return car;
        }
        public Order FindOrder(int id)
        {
            var order = db.Orders.FirstOrDefault(o => o.Id == id);

            return (order);
        }
        public IEnumerable<Order> FindOrders(string userId)
        {
            var orders = db.Orders.Where(o => o.User_Id == userId);
            return (orders);
        }
        IEnumerable<Car> ICarRepository.FindCars(string name = null, string manufactorer = null,
            string carType = null, string fuelType = null, string transmission = null, string capacities = null, string fuelCons = null, string engSizes = null, string doors = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
            IEnumerable<Car> cars = db.Cars.ToList();
            if (name != null)
            {
                cars = cars.Where(c => c.Name.ToLower().Contains(name.ToLower()));
            }
            if (manufactorer != "")
            {
                cars = cars.Where(c => manufactorer.ToLower().Contains(c.Manufacturer.ToLower()));

            }
            if (carType != "")
            {
                cars = cars.Where(c => carType.ToLower().Contains(c.CarType.ToLower()));
            }
            if (fuelType != "")
            {
                cars = cars.Where(c => fuelType.ToLower().Contains(c.FuelType.ToLower()));
            }
            if (transmission != "")
            {
                cars = cars.Where(c => transmission.ToLower().Contains(c.AutomaticTransm.ToString().ToLower()));
            }
            if (capacities != "")
            {
                cars = cars.Where(c => capacities.ToLower().Contains(c.Capacity.ToString().ToLower()));
            }
            if (engSizes != "")
            {
                cars = cars.Where(c => engSizes.ToLower().Contains(c.EngSize.ToString().ToLower()));
            }
            if (fuelCons != "")
            {
                cars = cars.Where(c => fuelCons.ToLower().Contains(c.FuelConsump.ToString().ToLower()));
            }
            if (doors != "")
            {
                cars = cars.Where(c => doors.ToLower().Contains(c.Door.ToString().ToLower()));
            }
            if (UppPrice != 0 && LowPrice != 0)
            {
                cars = cars.Where(c => c.Price <= UppPrice && c.Price >= LowPrice);
            }
            else if (UppPrice != 0)
            {
                cars = cars.Where(c => c.Price < UppPrice);
            }
            else if (LowPrice != 0)
            {
                cars = cars.Where(c => c.Price > LowPrice);
            }

            return cars;
        }
        public Car DeleteCar(int id)
        {
            Car dbEntry = db.Cars.Find(id);
            if (dbEntry != null)
            {
                db.Cars.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }
        public Car DeleteCarSoft(int id)
        {
            Car dbEntry = db.Cars.Find(id);

            if (dbEntry != null)
            {
                dbEntry.IsDeleted = true;

                db.SaveChanges();
            }
            return dbEntry;
        }
        public Order DeleteOrder(int id)
        {
            Order dbEntry = db.Orders.Find(id);

            if (dbEntry != null)
            {
                db.Orders.Remove(dbEntry);
                db.SaveChanges();
            }
            return dbEntry;
        }
        public Order DeleteOrderSoft(int id)
        {
            Order dbEntry = db.Orders.Find(id);

            if (dbEntry != null)
            {
                dbEntry.IsDeleted = true;

                db.SaveChanges();
            }
            return dbEntry;
        }
        public void AddExeption(ExceptionDetail ex)
        {
            db.ExceptionDetails.Add(ex);
            db.SaveChanges();
        }
        public IEnumerable<ExceptionDetail> Exceptions
        {
            get { return db.ExceptionDetails.ToList(); }
        }
        public void DeleteException(int id)
        {
            ExceptionDetail dbEntry = db.ExceptionDetails.Find(id);
            if (dbEntry != null)
            {
                db.ExceptionDetails.Remove(dbEntry);

                db.SaveChanges();
            }
        }
        public void DeleteExceptions()
        {
            var ex = db.ExceptionDetails;
            foreach (var e in ex)
            {
                db.ExceptionDetails.Remove(e);
            }
            db.SaveChanges();
        }

        public void EditUser(ApplicationUser user)
        {
            var us = db.Users.Find(user.Id);

            us.UserName = user.UserName;
            us.Banned = user.Banned;
            us.PassportNumb = user.PassportNumb;
            us.Name = user.Name;
            us.PhoneNumber = user.PhoneNumber;
            us.RepairInvoice = user.RepairInvoice;
            db.Users.Add(us);

        }
        public void CreateUser(ApplicationUser user)
        {
            ApplicationUser dbEntry = db.Users.Find(user.Id);
            if (dbEntry != null)
            {
                dbEntry.UserName = user.UserName;
                dbEntry.PassportNumb = user.PassportNumb;
                dbEntry.PhoneNumber = user.PhoneNumber;
                dbEntry.Banned = user.Banned;

            }

            db.SaveChanges();
        }
        public List<Order> GetCarOrders(int carId)
        {
            return db.Orders.Where(o => o.CarId == carId).ToList();
        }

        public void CreateLog(Log log)
        {
            db.Logs.Add(log);
            db.SaveChanges();
        }
    }
}
