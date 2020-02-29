using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{

    public class MainController : Controller
    {
        private IDatAcessService DatAcessService;
        public MainController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        
        
        
        public ActionResult Index()
        {
            var cars = DatAcessService.Cars;
            return View(cars);
        }
        [Authorize(Roles ="admin, manager")]
        [HttpGet]
        public ActionResult CreateCar()
        {
           
            return View();
        }
        [Authorize(Roles = "admin, manager")]
        [HttpPost]
        public ActionResult CreateCar(CarDTO car)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateCar(car);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View(car);
            }
        }

       
       

        // GET: Admin/Main/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

       

        // GET: Admin/Main/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var car = DatAcessService.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Admin/Main/Edit/5
        [HttpPost]
        public ActionResult Edit(CarDTO car)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateCar(car);
                return RedirectToAction("Index", "Main");
            }
            else
            {
                return View(car);
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
           var car= DatAcessService.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Admin/Main/Delete/5
        [HttpPost]
        public ActionResult Delete(CarDTO car)
        {
            DatAcessService.DeleteCar(car.CarId);
            return RedirectToAction("Index", "Main");
        }
        [HttpGet]
        public ActionResult DleteSoft(int id)
        {
            var car = DatAcessService.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }
        [HttpPost]
        public ActionResult Deletesoft(CarDTO car)
        {
            DatAcessService.DeleteCarSoft(car.CarId);
            return RedirectToAction("Index", "Main");
        }

    }
}
