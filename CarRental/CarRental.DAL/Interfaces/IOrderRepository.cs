using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    /// <summary>
    /// Specify the method which should be implemented in Unit of Work
    /// or Other repository.
    /// </summary>
    public interface IOrderRepository
    {
        IEnumerable<Order> Orders { get; }
        void CreateOrder(Order item);
        Order FindOrder(int id);
        IEnumerable<Order> FindOrders(string userId);
        Order DeleteOrderSoft(int id);
        Order DeleteOrder(int id);
    }
}
