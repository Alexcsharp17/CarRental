using CarRental.BLL.Attributes;
using CarRental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles = "admin, manager")]
    public class DashboardController : Controller
    {
        private IDatAcessService DatAcessService;
        public DashboardController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        public ActionResult Index()
        {
           
            ViewBag.Orders = DatAcessService.Orders.Count();
            ViewBag.Users = DatAcessService.Users.Count();
            ViewBag.Exceptions = DatAcessService.Exceptions.Count();
            ViewBag.Cars = DatAcessService.Cars.Count();
            return View();
        }
    }
}