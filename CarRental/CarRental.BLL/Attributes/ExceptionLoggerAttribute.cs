using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CarRental.DAL.EF;
using CarRental.DAL.Entities;
using CarRental.DAL.Interfaces;
using CarRental.DAL.Repositories;

namespace CarRental.BLL.Attributes
{
    public class ExceptionLoggerAttribute : FilterAttribute, IExceptionFilter
    {
      
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionDetail exceptionDetail = new ExceptionDetail()
            {
                ExceptionMessage = filterContext.Exception.Message,
                StackTrace = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };

            using (ApplicationContext db = new ApplicationContext("DefaultConnection"))
            {
                db.ExceptionDetails.Add(exceptionDetail);
                db.SaveChanges();
            }

            filterContext.ExceptionHandled = true;
        }
    }
}