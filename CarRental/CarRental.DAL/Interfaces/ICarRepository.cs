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
    public interface ICarRepository
    {
        IEnumerable<Car> Cars { get; }
        void CreateCar(Car car);
        Car FindCar(int id);
        IEnumerable<Car> FindCars(string name = null, string manufactorer = null, string carType = null, string fuelType = null, string transmission = null, string capacities = null, string fuelCons = null, string engSizes = null, string doors = null, int LowPrice = 0, int UppPrice = int.MaxValue);
        Car DeleteCar(int id);
        Car DeleteCarSoft(int id);
    }
}
