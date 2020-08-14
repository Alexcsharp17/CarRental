using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Repositories
{
    public class UoW : IUoW<ApplicationContext>
    {
        public ApplicationContext context { get; }
        public IRepository<Car, ApplicationContext> Cars { get; }
        public IRepository<Order, ApplicationContext> Orders { get; }
        public IRepository<Log, ApplicationContext> Logs { get; }
        public IRepository<ExceptionDetail, ApplicationContext> Exceptions { get; }
        public IDbSet<ApplicationUser> Users => context.Users;
        public IDbSet<IdentityRole> Roles => context.Roles;
        public UoW()
        {
            //Creating repositories
            context = new ApplicationContext();
            Cars = new Repository<Car, ApplicationContext>(context);
            Orders = new Repository<Order, ApplicationContext>(context);
            Logs = new Repository<Log, ApplicationContext>(context);
            Exceptions = new Repository<ExceptionDetail, ApplicationContext>(context);
        }

        //Save changes
        public void Commit()
        {
            context.SaveChanges();
        }

        //Dispose
        private bool disposed = false;
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

