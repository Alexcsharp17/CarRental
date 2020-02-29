using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Mappers;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    public class UserService : IUserService
    {
        IUnitOfWork Database { get; set; }

        public UserService(IUnitOfWork uow)
        {
            Database = uow;
        }
       

        public async Task<OperationDetails> Create(UserDTO userDto)
        {
            ApplicationUser user = await Database.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                user = new ApplicationUser { Email = userDto.Email, UserName = userDto.Email };
                var result = await Database.UserManager.CreateAsync(user, userDto.Password);
                if (result.Errors.Count() > 0)
                    return new OperationDetails(false, result.Errors.FirstOrDefault(), "");
                // добавляем роль
                await Database.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                // создаем профиль клиента
                ClientProfile clientProfile = new ClientProfile { Id = user.Id, Address = userDto.Address, Name = userDto.Name };
                Database.ClientManager.Create(clientProfile);
                await Database.SaveAsync();
                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

        public async Task<ClaimsIdentity> Authenticate(UserDTO userDto)
        {
            ClaimsIdentity claim = null;
            // находим пользователя
            ApplicationUser user = await Database.UserManager.FindAsync(userDto.Email, userDto.Password);
            // авторизуем его и возвращаем объект ClaimsIdentity
            if (user != null)
                claim = await Database.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        // начальная инициализация бд
        public async Task SetInitialData(UserDTO adminDto, List<string> roles)
        {
            foreach (string roleName in roles)
            {
                var role = await Database.RoleManager.FindByNameAsync(roleName);
                if (role == null)
                {
                    role = new ApplicationRole { Name = roleName };
                    await Database.RoleManager.CreateAsync(role);
                }
            }
            await Create(adminDto);
        }

        public void Dispose()
        {
            Database.Dispose();
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
        void IUserService.CreateCar(CarDTO cardto)
        {
            Car car = CarMapper.DTOCar(cardto);
            Database.CreateCar(car);
        }

        CarDTO IUserService.FindCar(int id)
        {
           Car car= Database.FindCar(id);
            CarDTO dto = Mappers.CarMapper.CarDTO(car);
            return (dto);
        }
        IEnumerable<CarDTO> IUserService.FindCars(string name = null, string manufactorer = null,
            string carType = null, int LowPrice = 0, int UppPrice = int.MaxValue)
        {
            List <CarDTO> dtos= new List<CarDTO>();
           IEnumerable<Car> Cars= Database.FindCars(name, manufactorer, carType, LowPrice, UppPrice);
            foreach(var c in Cars)
            {
                var dto= CarMapper.CarDTO(c);
                dtos.Add(dto);
            }
            return dtos;
        }
        void IUserService.DeleteCar(int id)
        {
            Database.DeleteCar(id);
            
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

        void IUserService.CreateOrder(OrderDTO orderdto)
        {
            Order order = OrderMapper.DTOOrder(orderdto);
            Database.CreateOrder(order);
        }

        OrderDTO IUserService.FindOrder (int id)
        {
          return OrderMapper.OrderDTO(Database.FindOrder(id));
        }
        IEnumerable<OrderDTO> IUserService.FindOrders(string userId)
        {
            var orders= Database.FindOrders(userId);
            List<OrderDTO> dtos = new List<OrderDTO>();
            foreach(var o in orders)
            {
                dtos.Add(OrderMapper.OrderDTO(o));
            }
            return dtos;
        }
        void IUserService.DeleteOrder(int id)
        {
            Database.DeleteOrder(id);
            
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
