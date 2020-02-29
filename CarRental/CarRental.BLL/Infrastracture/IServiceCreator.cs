using CarRental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Infrastracture
{
    public interface IServiceCreator
    {
        IUserService CreateUserService(string connection);
    }
}
