using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
  public  interface IDatAcessService
    {
        IEnumerable<CarDTO> Cars { get; }
        IEnumerable<UserDTO> Users { get; }
        IEnumerable<OrderDTO> Orders { get; }
        void CreateOrder(OrderDTO orderdto);
        OrderDTO FindOrder(int id);
        IEnumerable<OrderDTO> FindOrders(string userId);
        void DeleteOrder(int id);
        void DeleteOrderSoft(int id);
        void CreateCar(CarDTO cardto);
        CarDTO FindCar(int id);
        IEnumerable<CarDTO> FindCars(string name = null, string manufactorer = null, string carType = null, int LowPrice = 0, int UppPrice = int.MaxValue);
        void DeleteCar(int id);
        void DeleteCarSoft(int id);
    }
}
