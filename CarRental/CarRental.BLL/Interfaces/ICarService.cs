using CarRental.BLL.DTO;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
   public interface ICarService : IDisposable
    {
        IEnumerable<CarDTO> Cars { get; }
        void CreateCar(CarDTO car);
        CarDTO FindCar(int id);
        IEnumerable<CarDTO> FindCars(string name = "", string manufactorer = "",
            string carType = "", string fuelType = "", string transmission = "", int LowPrice = 0, int UppPrice = int.MaxValue);
        void DeleteCar(int id);
        void DeleteCarSoft(int id);

    }
}
