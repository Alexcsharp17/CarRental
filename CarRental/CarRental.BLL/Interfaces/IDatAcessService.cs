using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
    /// <summary>
    /// Specify the methods which should be implemented in DAtaAcess Service
    /// </summary>
    public interface IDatAcessService
    {
        IEnumerable<CarDTO> Cars { get; }
        IEnumerable<UserDTO> Users { get; }
        void CreateUser(UserDTO userdto);
        IEnumerable<OrderDTO> Orders { get; }
        IEnumerable<ExceptionDetailDTO> Exceptions { get; }
        void CreateOrder(OrderDTO orderdto);
        OrderDTO FindOrder(int id);
        IEnumerable<OrderDTO> FindOrders(string userId);
        void DeleteOrder(int id);
        void DeleteOrderSoft(int id);
        void CreateCar(CarDTO cardto);
        CarDTO FindCar(int id);
        void EditUser(UserDTO user);
        IEnumerable<CarDTO> FindCars(string name = null, string manufactorer = null, string carType = null, string fuelType = null, string transmission = null, int LowPrice = 0, int UppPrice = int.MaxValue);
        void DeleteCar(int id);
        void DeleteCarSoft(int id);
        void DeleteExceptions();
        void DeleteException(int id);
        
    }
}
