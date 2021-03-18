using CarRental.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.BLL.Interfaces
{
    public interface IExceptionService : IDisposable
    {
        void AddEx(ExceptionDetailDTO obj);
        void DeleteEx(int id);
        void DeleteAllEx();
        IEnumerable<ExceptionDetailDTO> GetExceptions();

    }
}
