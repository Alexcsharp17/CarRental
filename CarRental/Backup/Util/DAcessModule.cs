using CarRental.BLL.Interfaces;
using CarRental.BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarRental.WEB.Util
{
    public class DAcessModule : NinjectModule
    {
        public override void Load()
        {

            Bind<IDatAcessService>().To<DatAcessService>();
            Bind<ILogService>().To<LogService>();
            Bind<IExceptionService>().To<ExceptionService>();
            Bind<IOrderService>().To<OrderService>();
            Bind<ICarService>().To<CarService>();

        }
    }
}