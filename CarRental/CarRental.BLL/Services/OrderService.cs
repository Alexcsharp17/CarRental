using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IUoW<ApplicationContext> db { get; }
        public OrderService(IUoW<ApplicationContext> db)
        {
            this.db = db ?? new UoW();
        }

        public OrderService()
        {
            db = new UoW();
        }

        public IEnumerable<OrderDTO>  Orders
        {
            get {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
                var mapper = new Mapper(config);
                return mapper.Map<List<OrderDTO>>(db.Orders.GetAll());
            }
        }
        
        public void CreateOrder(OrderDTO o)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<OrderDTO, Order>());
            var mapper = new Mapper(config);
            db.Orders.Create(mapper.Map<Order>(o));
            db.Commit();
        }
        public OrderDTO FindOrder(int id)
        {
            
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<Car, CarDTO>().ForMember(x=>x.Popular,opt=>opt.Ignore());
            });
            config.AssertConfigurationIsValid();
            var mapper = config.CreateMapper();            
            var order= mapper.Map<Order,OrderDTO>(db.Orders.Get(id));
            
            return order;
            
        }
        public IEnumerable<OrderDTO> FindOrders(string userId)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Order, OrderDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<OrderDTO>>(db.Orders.GetAll().Where(o=>o.User_Id==userId));
        }
        public void DeleteOrder(int id)
        {
            db.Exceptions.Delete(id);
            db.Commit();
        }
        public void DeleteOrderSoft(int id)
        {
            Order o = db.Orders.Get(id);
            o.IsDeleted = true;
            db.Orders.Update(o);
            db.Commit();
        }

        void IDisposable.Dispose()
        {
            db.Dispose();
        }
    }
}
