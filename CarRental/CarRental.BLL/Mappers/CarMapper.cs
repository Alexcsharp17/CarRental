using CarRental.BLL.DTO;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Mappers
{
   public class CarMapper
    {
        public static CarDTO CarDTO(Car car)
        {
            CarDTO dto = new CarDTO
            { AirConditon = car.AirConditon,
                DriveUnit = car.DriveUnit,
                AutomaticTransm = car.AutomaticTransm,
                Capacity = car.Capacity,
                CarId=car.CarId,
                Manufacturer=car.Manufacturer,
                Name=car.Name,
                Price=car.Price,
                Image=car.Image,
                CarType=car.CarType,
                IsDeleted=car.IsDeleted,
                Door=car.Door,
                FuelConsump=car.FuelConsump,
                FuelType=car.FuelType
           };
            return dto;

        }
        public static Car DTOCar(CarDTO dto)
        {
            Car car = new Car
            {
                AirConditon = dto.AirConditon,
                DriveUnit = dto.DriveUnit,
                AutomaticTransm = dto.AutomaticTransm,
                Capacity = dto.Capacity,
                CarId = dto.CarId,
                Manufacturer = dto.Manufacturer,
                Name = dto.Name,
                Price = dto.Price,
                Image=dto.Image,
                CarType=dto.CarType,
                IsDeleted=dto.IsDeleted,
                Door=dto.Door,
                FuelConsump=dto.FuelConsump,
                FuelType=dto.FuelType,
                EngSize=dto.EngSize
                
                
            };
            return car;
        }
    }
}
