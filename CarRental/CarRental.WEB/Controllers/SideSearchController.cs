using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Controllers
{
    public class SideSearchController : Controller
    {
        private IUserService DataAcessService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        [HttpGet]
        public ActionResult CarSearch()
        {
            CarSearchModel mod = new CarSearchModel();
            return PartialView("~/Views/SideSearch/CarSearch.cshtml");
          
        }
        [HttpPost]
        public ActionResult CarSearch(CarSearchModel model=null)
        {
            if (model != null)
            {
                IEnumerable<CarDTO> c = DataAcessService.FindCars(model.Name,
                    model.Manufactorer, model.CarType, model.LowPrice, model.UppPrice);
                string ids="";
                
                 foreach(var car in c)
                {
                    ids = ids + car.CarId + "I";
                }
               
                return RedirectToAction("Index", "Home", new{id=ids
            }); /*new                {
                   cars=cars
                });*/
            }
            return View();
        }
    }
}