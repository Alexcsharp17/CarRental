using CarRental.DAL.Entities;
using CarRental.DAL.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    /// <summary>
    /// Interface specify the service provided by class,
    /// which implement this interface.(Class Should give acess to operations with database 
    /// entities such as CRUD functionality etc.)
    /// </summary>
    public interface IUnitOfWork : IDisposable, ICarRepository ,IOrderRepository,IExceptionRepository
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        List<ApplicationUser> GetUsers();
        Task SaveAsync();
        void EditUser(ApplicationUser user);
        IEnumerable<ApplicationUser> Users{get;}      
        void CreateUser(ApplicationUser user);
        void CreateLog(Log l);
        List<Order> GetCarOrders(int carId);
        List<CarItem> CarItems();
    }
}
