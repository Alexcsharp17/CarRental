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
 public   class DatAcessService : IDatAcessService
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
                var cars = Database.Cars.ToList();
                List<CarDTO> cardto = new List<CarDTO>();
                foreach (Car c in cars)
                {
                    CarDTO car = CarMapper.CarDTO(c);

                    cardto.Add(car);
                }
                IEnumerable<CarDTO> carDTOs = cardto;
                return (carDTOs);
            }

        }
        void IDatAcessService.CreateCar(CarDTO cardto)
        {
            Car car = CarMapper.DTOCar(cardto);
            Database.CreateCar(car);
        }

        CarDTO IDatAcessService.FindCar(int id)
        {
            Car car = Database.FindCar(id);
            CarDTO dto = Mappers.CarMapper.CarDTO(car);
            return (dto);
        }
        IEnumerable<CarDTO> IDatAcessService.FindCars(string name = null, string manufactorer = null,
            string carType = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
            List<CarDTO> dtos = new List<CarDTO>();
            IEnumerable<Car> Cars = Database.FindCars(name, manufactorer, carType, LowPrice, UppPrice);
            foreach (var c in Cars)
            {
                var dto = CarMapper.CarDTO(c);
                dtos.Add(dto);
            }
            return dtos;
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
                var orders = Database.Orders.ToList();
                List<OrderDTO> orderdto = new List<OrderDTO>();
                foreach (Order o in orders)
                {
                    OrderDTO order = OrderMapper.OrderDTO(o);

                    orderdto.Add(order);
                }
                IEnumerable<OrderDTO> OrderDTOs = orderdto;
                return (OrderDTOs);
            }
        }

        void IDatAcessService.CreateOrder(OrderDTO orderdto)
        {
            Order order = OrderMapper.DTOOrder(orderdto);
            Database.CreateOrder(order);
        }

        OrderDTO IDatAcessService.FindOrder(int id)
        {
            return OrderMapper.OrderDTO(Database.FindOrder(id));
        }
        IEnumerable<OrderDTO> IDatAcessService.FindOrders(string userId)
        {
            var orders = Database.FindOrders(userId);
            List<OrderDTO> dtos = new List<OrderDTO>();
            foreach (var o in orders)
            {
                dtos.Add(OrderMapper.OrderDTO(o));
            }
            return dtos;
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
                var users = Database.Users.ToList();
                List<UserDTO> userdto = new List<UserDTO>();
                foreach (ApplicationUser u in users)
                {
                    UserDTO user = new UserDTO();

                    user.Address = u.ClientProfile.Address;
                    user.Address = u.Email;
                    user.Id = u.Id;
                    user.UserName = u.UserName;
                    user.UserName = u.ClientProfile.Name;
                    userdto.Add(user);
                }
                IEnumerable<UserDTO> UserDTOs = userdto;
                return (UserDTOs);
            }
        }

    }
}
