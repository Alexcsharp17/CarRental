using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles ="admin")]
    public class MainController : Controller
    {
        private IDatAcessService DatAcessService;
        public MainController(IDatAcessService serv)
        {
            DatAcessService = serv;
           
        }
      
      

        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        public ActionResult GetExceptions()
        {
            var exceptions = DatAcessService.Exceptions;
            return View(exceptions);
        }
        public ActionResult DeleteException(int id)
        {
            DatAcessService.DeleteException(id);
            return RedirectToAction("GetExceptions");
        }
        public ActionResult DeleteExceptions()
        {
            DatAcessService.DeleteExceptions();
            return RedirectToAction("GetExceptions");
        }

        public ActionResult GetCars()
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
                return RedirectToAction("GetCars", "Main");
            }
            else
            {

                return View(car);
            }
        }
        public ActionResult CarDetail(int id)
        {
           var car= DatAcessService.FindCar(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        [HttpPost]
        public PartialViewResult FileUpload(HttpPostedFileBase uploadFile)
        {
           
            if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.ContentLength<10000000)
            {
                string filePath = Path.Combine(Server.MapPath("/Content/Img/cars"), Path.GetFileName(uploadFile.FileName));
                uploadFile.SaveAs(filePath);
                ViewBag.Name=uploadFile.FileName;
                ViewBag.filePath = filePath;
            }
           
            return PartialView();
        }


        // GET: Admin/Main/Details/5
        public ActionResult CarDetails(int id)
        {
            return View();
        }

       

        // GET: Admin/Main/Edit/5
        [HttpGet]
        public ActionResult EditCar(int id)
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
        public ActionResult EditCar(CarDTO car)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateCar(car);
                return RedirectToAction("GetCars", "Main");
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
            var filePath = Server.MapPath("~"+car.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
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
            return RedirectToAction("GetCars", "Main");
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
