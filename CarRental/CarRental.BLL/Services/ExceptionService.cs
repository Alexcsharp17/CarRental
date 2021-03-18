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
    public class ExceptionService : IExceptionService
    {
        private IUoW<ApplicationContext> db { get; }
        public ExceptionService(IUoW<ApplicationContext> db)
        {
            this.db = db ?? new UoW();
        }

        public ExceptionService()
        {
            db = new UoW();
        }
        void IExceptionService.AddEx(DTO.ExceptionDetailDTO ex)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExceptionDetailDTO, ExceptionDetail>());
            var mapper = new Mapper(config);
            var exep = mapper.Map<ExceptionDetail>(ex);
            db.Exceptions.Create(exep);
            db.Commit();
        }
        void IExceptionService.DeleteEx(int id)
        {
            db.Exceptions.Delete(id);
            db.Commit();
        }
        void IExceptionService.DeleteAllEx()
        {
            IEnumerable<int> exeptions = db.Exceptions.GetAll().Select(x => x.Id);
            foreach (var e in exeptions)
            {
                db.Exceptions.Delete(e);
            }
            db.Commit();
        }
        IEnumerable<ExceptionDetailDTO> IExceptionService.GetExceptions()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<ExceptionDetail, ExceptionDetailDTO>());
            var mapper = new Mapper(config);
            var exep = mapper.Map<List<ExceptionDetailDTO>>(db.Exceptions.GetAll());
            return exep;

        }
        void IDisposable.Dispose()
        {
            db.Dispose();
        }

    }
}
