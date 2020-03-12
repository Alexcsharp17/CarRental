using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Mappers;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    /// <summary>
    /// Main purpose of this sevice is basicly to set a communication
    /// between WEB layer and DAL. Since we can't get dirrect acess to UnitOFWork
    /// (It's stored in DAL and are direct acess will ruine multi layer architecture).
    ///DatAcessService proide acess to UnitOfWors methods from Web layer.
    /// </summary>
    public class DatAcessService : IDatAcessService
    {
        IUnitOfWork Database { get; set; }

        public DatAcessService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public IEnumerable<CarDTO> Cars
        {
            get
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());

                var mapper = new Mapper(config);

                var cars = mapper.Map<List<CarDTO>>(Database.Cars.ToList());

                return (cars);
            }

        }
        void IDatAcessService.CreateCar(CarDTO cardto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>());

            var mapper = new Mapper(config);
            var car = mapper.Map<Car>(cardto);
            Database.CreateCar(car);
        }

        CarDTO IDatAcessService.FindCar(int id)
        {
            Car car = Database.FindCar(id);
            if (car == null)
            {
                return null;
            }
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());

            var mapper = new Mapper(config);

            var cardto = mapper.Map<CarDTO>(Database.FindCar(id));

            return (cardto);
        }
        IEnumerable<CarDTO> IDatAcessService.FindCars(string name = null, string manufactorer = null,
            string carType = null, string fuelType = null, string transmission = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
    
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());

                var mapper = new Mapper(config);

                var cars = mapper.Map<List<CarDTO>>(Database.FindCars(name, manufactorer, carType,fuelType,transmission, LowPrice, UppPrice).ToList());
            var cc = cars.Count();
            
            return cars;
        }
        void IDatAcessService.DeleteCar(int id)
        {
            Database.DeleteCar(id);

        }
        void IDatAcessService.DeleteCarSoft(int id)
        {
            Database.DeleteCarSoft(id);
        }
        public IEnumerable<OrderDTO> Orders
        {
          get
            {               
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
                var mapper = new Mapper(config);
                var orders = mapper.Map<List<OrderDTO>>(Database.Orders.ToList());
                return orders;        
            }
        }

        void IDatAcessService.CreateOrder(OrderDTO orderdto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>());
            var mapper = new Mapper(config);
            var order = mapper.Map<Order>(orderdto);           
            Database.CreateOrder(order);
        }

        OrderDTO IDatAcessService.FindOrder(int id)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
            var mapper = new Mapper(config);
            var order = mapper.Map<OrderDTO>(Database.FindOrder(id));
            return order;
        }
        IEnumerable<OrderDTO> IDatAcessService.FindOrders(string userId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
            var mapper = new Mapper(config);
            var orders = mapper.Map<List<OrderDTO>>(Database.FindOrders(userId).ToList());
            return orders;
        }
        void IDatAcessService.DeleteOrder(int id)
        {
            Database.DeleteOrder(id);

        }
        void IDatAcessService.DeleteOrderSoft(int id)
        {
            Database.DeleteOrderSoft(id);
        }

        public IEnumerable<UserDTO> Users
        {
            get
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ApplicationUser, UserDTO>());

                var mapper = new Mapper(config);

                var users = mapper.Map<List<UserDTO>>(Database.Users.ToList());                
                return (users);
            }
        }
        public void EditUser(UserDTO user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, ApplicationUser>());

            var mapper = new Mapper(config);

            var us = mapper.Map<ApplicationUser>(user);
            Database.EditUser(us);
        }
        void IDatAcessService.CreateUser(UserDTO userdto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, ApplicationUser>());

            var mapper = new Mapper(config);
            var user = mapper.Map<ApplicationUser>(userdto);
            Database.CreateUser(user);
        }
        public IEnumerable<ExceptionDetailDTO> Exceptions{
            get {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<ExceptionDetail, ExceptionDetailDTO>());

                var mapper = new Mapper(config);

                var exepts = mapper.Map<List<ExceptionDetailDTO>>(Database.Exceptions.ToList());

                return exepts;
                }
         }
        void IDatAcessService.DeleteExceptions()
        {
            Database.DeleteExceptions();
        }
        void IDatAcessService.DeleteException(int id)
        {
            Database.DeleteException(id);
        }

    }
}
