using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    public class CarService : ICarService
    {
        private IUoW<ApplicationContext> db { get; }
        public CarService(IUoW<ApplicationContext> db)
        {
            this.db = db ?? new UoW();
        }

        public CarService()
        {
            db = new UoW();
        }

        public IEnumerable<CarDTO> Cars
        {
            get
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());
                var mapper = new Mapper(config);
                return mapper.Map<List<CarDTO>>(db.Cars.GetAll());
            }
        }
        public void CreateCar(CarDTO c)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>());
            var mapper = new Mapper(config);
            db.Cars.Create(mapper.Map<Car>(c));
            db.Commit();
        }
        public CarDTO FindCar(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<CarDTO>(db.Cars.Get(id));

        }
        public IEnumerable<CarDTO> FindCars(string name = null, string manufactorer = null,
            string carType = null, string fuelType = null, string transmission = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {

            IEnumerable<Car> cars = db.Cars.GetAll();
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
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<CarDTO>>(cars);
           
        }
        public void DeleteCar(int id)
        {
            if (db.Cars.Get(id) != null)
            {
                db.Cars.Delete(id);
                db.Commit();
            }
        }
        public void DeleteCarSoft(int id)
        {
            Car c = db.Cars.Get(id);

            if (c != null)
            {
                c.IsDeleted = true;
                db.Cars.Update(c);
                db.Commit();
            }
          
        }
        void IDisposable.Dispose()
        {
            db.Dispose();
        }
    }
}
