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
    public interface IDatAcessService : ICarService, IOrderService
    {

        IEnumerable<UserDTO> Users { get; }
        void CreateUser(UserDTO userdto);
        IEnumerable<ExceptionDetailDTO> Exceptions { get; }
        void EditUser(UserDTO user);
        void DeleteExceptions();
        void DeleteException(int id);
        List<OrderDTO> GetCarOrders(int carId);
        List<CarItemDTO> CarItems();

    }
}
