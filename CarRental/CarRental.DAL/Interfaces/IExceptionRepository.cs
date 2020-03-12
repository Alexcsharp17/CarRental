using CarRental.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.DAL.Interfaces
{
  public  interface IExceptionRepository
    {
        IEnumerable<ExceptionDetail> Exceptions { get; }
        void AddExeption(ExceptionDetail ex);
        void DeleteException(int id);
        void DeleteExceptions();
    }
}
