using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarRental.BLL.Services;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Infrastracture;
using Ninject.Modules;
using CarRental.WEB.Util;
using Ninject;

[assembly: OwinStartup(typeof(CarRental.WEB.App_Start.Startup))]
namespace CarRental.WEB.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
            return serviceCreator.CreateUserService("DefaultConnection");
        }

      
    }
}