using CarRental.BLL.DTO;
using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Mappers
{
   public class OrderMapper
    {
        public static Order DTOOrder(OrderDTO dto)
        {
            Order order = new Order {
                CarId= dto.CarId,
                Driver=dto.Driver,
                IsDeleted=dto.IsDeleted,
                StartTime=dto.StartTime,
                EndTime=dto.EndTime,
                OrderId=dto.OrderId,
                User_Id=dto.User_Id,
                Status=dto.Status,
                PassportNumb=dto.PassportNumb,
                ManagComment=dto.ManagComment,
                OrdSum=dto.OrdSum

            };
            return order;
        }
        public static OrderDTO OrderDTO(Order order)
        {
            OrderDTO dto = new OrderDTO
            {
                CarId = order.CarId,
                Driver = order.Driver,
                IsDeleted = order.IsDeleted,
                StartTime = order.StartTime,
                EndTime = order.EndTime,
                OrderId = order.OrderId,
                User_Id = order.User_Id,
                Status=order.Status,
                PassportNumb=order.PassportNumb,
                ManagComment=order.ManagComment,
                OrdSum=order.OrdSum
            };
            return dto;
        }
    }
}
