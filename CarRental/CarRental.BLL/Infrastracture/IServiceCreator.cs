using CarRental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Infrastracture
{
    /// <summary>
    /// Specify method which shoud create Data acess Service
    /// </summary>
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
