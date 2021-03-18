using AutoMapper;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CarRental.BLL.Attributes
{
    public class LogFilterAttribute : FilterAttribute, IActionFilter
    {
        private ILogService logService;
        public LogFilterAttribute(ILogService service)
        {
            logService = service;
        }
        public LogFilterAttribute()
        {
            logService = new LogService();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var UserName = filterContext.HttpContext.User.Identity.Name;
            var URL = filterContext.HttpContext.Request.RawUrl;
            var Browser = filterContext.HttpContext.Request.Browser;
            var exeption = filterContext.Exception;
            LogDTO l = new LogDTO
            {
                UserName = UserName == "" ? "-" : UserName,
                BrowserName = Browser.Browser,
                BrowserVersion = Browser.MinorVersionString,
                JavasriptVersion = Browser.JScriptVersion.ToString(),
                IsMobile = Browser.IsMobileDevice,
                Platform = Browser.Platform,
                Exeption = exeption is null ? "-" : exeption.Message,
                URL = URL,
                Date = DateTime.Now
            };

            logService.AddLog(l);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}