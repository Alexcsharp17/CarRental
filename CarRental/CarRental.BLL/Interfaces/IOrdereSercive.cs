using CarRental.BLL.DTO;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
    public interface IOrderService : IDisposable
    {
       IEnumerable<OrderDTO> Orders { get; }
        void CreateOrder(OrderDTO order);
        OrderDTO FindOrder(int id);
        IEnumerable<OrderDTO> FindOrders(string userId);
         void DeleteOrder(int id);
        void DeleteOrderSoft(int id);
    }
}
