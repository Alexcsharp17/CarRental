using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Infrastracture;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Areas.Admin.Models;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Generator;

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
        public ActionResult ExceptionDetails(int id)
        {
          ExceptionDetailDTO exc= DatAcessService.Exceptions.FirstOrDefault(e => e.Id == id);
            return View(exc);
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
        public PartialViewResult FileUpload(HttpPostedFileBase uploadFile, int img = 1)
        {
           
            if (uploadFile != null && uploadFile.ContentLength > 0 && uploadFile.ContentLength<10000000)
            {
                string filePath = Path.Combine(Server.MapPath("/Content/Img/cars"), Path.GetFileName(uploadFile.FileName)+img.ToString());

                uploadFile.SaveAs(filePath);
                ViewBag.Name=uploadFile.FileName;
                ViewBag.filePath = filePath;
            }           
            return PartialView();
        }
      public PartialViewResult FileDelete(string route)
        {
            var filePath = Server.MapPath("~" + route);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
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
           
            var filePath = Server.MapPath("~" + car.Image);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            DatAcessService.DeleteCar(car.Id);
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
            DatAcessService.DeleteCarSoft(car.Id);
            return RedirectToAction("Index", "Main");
        }
        public ActionResult GetStats()
        {
            var thirtyDayOrd = DatAcessService.Orders.Where(x => DateTime.Compare(DateTime.Now, x.StartTime) >= 0 && ( DateTime.Now - x.StartTime).TotalDays < 150);
            var sum = DatAcessService.Orders.Where(x => (DateTime.Now - x.StartTime).TotalDays < 30).Select(x => x.OrdSum).Sum();
            ViewBag.ords = thirtyDayOrd.Count();
            ViewBag.sum = sum;

            var orders =thirtyDayOrd.GroupBy(o => o.StartTime.Date)
                        .Select(g => new { date = g.Key, count = g.Count() })
                        .OrderBy(g => g.date);
            var cars = thirtyDayOrd.GroupBy(o => o.Car.Name)
                        .Select(g => new { name = g.Key, count = g.Count() });

            StatisticsModel model = new StatisticsModel();
  
            foreach (var o in orders)
            {
                model.DaySales.Add(o.date.ToString(), o.count);
            }
            foreach (var car in cars)
            {
                model.CarSales.Add(car.name.ToString(), car.count);
            }
            return View(model);
        }
       public ActionResult CreateReport()
        {
            string writePath = @"D:\MonthRevenue.doc";
            using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine("                  Month Report data");

                int ords = DatAcessService.Orders.Where(x => (DateTime.Now - x.StartTime).TotalDays < 30).Count();
                double sum = DatAcessService.Orders.Where(x => (DateTime.Now - x.StartTime).TotalDays < 30).Select(x => x.OrdSum).Sum();
                string orders = "Total orders: "+ords;
                string summ = "Month income: " + sum + "$";
                
                sw.WriteLine(orders);
                sw.WriteLine(summ);
            }
            string writePath2 = @"D:\Prices.doc";
            using (StreamWriter sw2 = new StreamWriter(writePath2, false, System.Text.Encoding.Default))
            {
                sw2.WriteLine("                             Car prices");
                sw2.WriteLine();
                sw2.WriteLine("Car Name"+ new String(' ',30-"Car Name".Length)+"Car price");
                sw2.WriteLine();
                var cars = DatAcessService.Cars;
                foreach(var c in cars)
                {
                sw2.WriteLine(c.Name + new String(' ', 30-c.Name.Length) + c.Price + "$");
                }

               
            }
            return RedirectToAction("GetStats");
          }

        

    }
}
