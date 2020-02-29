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
        }
    }
}