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
    public class LogService : ILogService
    {
        private IUoW<ApplicationContext> db { get; }
        public LogService(IUoW<ApplicationContext> db)
        {
            this.db = db ?? new UoW();
        }

        public LogService()
        {
            db = new UoW();
        }

        void ILogService.AddLog(LogDTO log)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<LogDTO, Log>());
            var mapper = new Mapper(config);
            var l = mapper.Map<Log>(log);

            try
            {
                db.Logs.Create(l);
                db.Commit();

            }
            catch
            {

            }
        }
        public IEnumerable<LogDTO> GetLogs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Log, LogDTO>());
            var mapper = new Mapper(config);
            var logs = mapper.Map<List<LogDTO>>(db.Logs.GetAll());

            return logs;
        }

        void IDisposable.Dispose()
        {
            db.Dispose();
        }
    }
}


