using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Services
{
    /// <summary>
    /// This is Factory metod, which is used for dependency invercion
    /// </summary>
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new UnitOfWork(connection));
        }
    }
}
