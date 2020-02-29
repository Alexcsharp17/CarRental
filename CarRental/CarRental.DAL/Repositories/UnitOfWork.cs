using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Identity;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
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
            get {
                
                return db.Cars;
            }
        }
        public IEnumerable<ApplicationUser> Users
        {
            get { return db.Users; }
        }
        public IEnumerable<Order> Orders
        {
            get { return db.Orders; }
        }
        public void CreateCar(Entities.Car car)
        {
            if (car.CarId == 0)
                db.Cars.Add(car);
            else
            {
                Car dbEntry = db.Cars.Find(car.CarId); 
                if (dbEntry != null)
                {
                    dbEntry.Name = car.Name;
                    dbEntry.AirConditon = car.AirConditon;
                    dbEntry.AutomaticTransm = car.AutomaticTransm;
                    dbEntry.Capacity = car.Capacity;
                    dbEntry.Manufacturer = car.Manufacturer;
                    dbEntry.Price = car.Price;

                }

            }
            db.SaveChanges();
        }
        public void CreateOrder(Entities.Order order)
        {
            if (order.OrderId == 0)
                db.Orders.Add(order);
            else
            {
                Order dbEntry = db.Orders.Find(order.OrderId);
                if (dbEntry != null)
                {
                    dbEntry.CarId = order.CarId;
                    dbEntry.Driver = order.Driver;
                    dbEntry.StartTime = order.StartTime;
                    dbEntry.EndTime = order.EndTime;
                    dbEntry.OrderId = order.OrderId;
                }
            }
            db.SaveChanges();
        }


        public Car FindCar(int id)
        {
            var cars = db.Cars.FirstOrDefault(c => c.CarId == id);
            return (cars);
        }
        public Order FindOrder(int id)
        {
            var orders = db.Orders.FirstOrDefault(o => o.OrderId == id);
            return (orders);
        }
        public IEnumerable<Order> FindOrders(string userId)
        {
            var orders = db.Orders.Where(o => o.User_Id == userId);
            return (orders);
        }
        IEnumerable<Car> ICarRepository.FindCars(string name= null, string manufactorer = null, string carType = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
            IEnumerable<Car> cars= db.Cars;
            if (name != null)
            {
                cars = cars.Where(c => c.Name.Contains(name));
            }
            if (manufactorer != null)
            {
                cars = cars.Where(c => c.Manufacturer.Contains(manufactorer));
            }
            if(UppPrice!=0 && LowPrice != 0){
                cars = cars.Where(c => c.Price < UppPrice && c.Price > LowPrice);
            }
            else if (UppPrice != 0)
            {
                cars = cars.Where(c => c.Price < UppPrice);
            }
            else if(LowPrice != 0)
            {
                cars = cars.Where(c => c.Price > LowPrice);
            }
            
            return (cars);
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
    }
}
