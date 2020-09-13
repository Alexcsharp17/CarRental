using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
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
        IUnitOfWork Database { get;  }

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
      public  void CreateCar(CarDTO cardto)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<CarDTO, Car>());

            var mapper = new Mapper(config);
            var car = mapper.Map<Car>(cardto);
            Database.CreateCar(car);
        }

       public CarDTO FindCar(int id)
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
       public IEnumerable<CarDTO> FindCars(string name = null, string manufactorer = null,
            string carType = null, string fuelType = null, string transmission = null, string capacities = null, string fuelCons=null, string engSizes=null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
    
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Car, CarDTO>());

                var mapper = new Mapper(config);

                var cars = mapper.Map<List<CarDTO>>(Database.FindCars(name, manufactorer, carType,fuelType,transmission,capacities, fuelCons,engSizes, LowPrice, UppPrice).ToList());
           
            
            return cars;
        }
       public void DeleteCar(int id)
        {
            Database.DeleteCar(id);

        }
       public void DeleteCarSoft(int id)
        {
            Database.DeleteCarSoft(id);
        }
        public IEnumerable<OrderDTO> Orders
        {
          get
            {               
              
                var config = new MapperConfiguration(cfg => {
                    cfg.CreateMap<Order, OrderDTO>();
                    cfg.CreateMap<Car, CarDTO>().ForMember(x => x.Popular, opt => opt.Ignore());
                    cfg.CreateMap<CarItem, CarItemDTO>();
                });
       
                var mapper = config.CreateMapper();
                var orders = mapper.Map<List<Order>, List<OrderDTO>>(Database.Orders.ToList());
                return orders;
            }
        }
        public List<OrderDTO> GetCarOrders(int carId)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Car, CarDTO>().ForMember(x => x.Popular, opt => opt.Ignore());
                cfg.CreateMap<CarItem, CarItemDTO>();
            });
           var mapper = config.CreateMapper();
            return mapper.Map<List<Order>, List<OrderDTO>>(Database.GetCarOrders(carId));
        }
        public List<CarItemDTO> CarItems()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CarItem, CarItemDTO>();

            }); 
            var mapper = new Mapper(config);
            return mapper.Map<List<CarItem>,List<CarItemDTO>>(Database.CarItems());
        }

      public  void CreateOrder(OrderDTO orderdto)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<OrderDTO, Order>();

            }); var mapper = new Mapper(config);
            var order = mapper.Map<Order>(orderdto);
            Database.CreateOrder(order);


        }

       public OrderDTO FindOrder(int id)
        {


            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Car, CarDTO>().ForMember(x => x.Popular, opt => opt.Ignore());
                cfg.CreateMap<CarItem, CarItemDTO>();
            });
            var mapper = config.CreateMapper();
            var order = mapper.Map<Order, OrderDTO>(Database.FindOrder(id));

            return order;
        }
       public IEnumerable<OrderDTO> FindOrders(string userId)
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Car, CarDTO>().ForMember(x => x.Popular, opt => opt.Ignore());
                cfg.CreateMap<CarItem, CarItemDTO>();
            });

            var mapper = config.CreateMapper();
            var orders = mapper.Map<List<Order>, List<OrderDTO>>(Database.FindOrders(userId).ToList());

            return orders;          
        }
       public void DeleteOrder(int id)
        {
            Database.DeleteOrder(id);

        }
       public void DeleteOrderSoft(int id)
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
        public void CreateUser(UserDTO userdto)
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
         public void DeleteExceptions()
        {
            Database.DeleteExceptions();
        }
       public void DeleteException(int id)
        {
            Database.DeleteException(id);
        }

        public void Dispose()
        {
            this.Dispose();
        }
    }
}
