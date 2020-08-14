using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
    public interface IUoW<TContext> : IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// Save changes in the database
        /// </summary>
        void Commit();
        /// <summary>
        /// Context field
        /// </summary>
        TContext context { get; }
        IRepository<Car, TContext> Cars { get; }
        IRepository<Order, TContext> Orders { get; }
        IRepository<Log, TContext> Logs { get; }
        IRepository<ExceptionDetail, TContext> Exceptions { get; }
        IDbSet<ApplicationUser> Users { get; }
    }
}
