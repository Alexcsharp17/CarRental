using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Controllers
{
    public class HomeController : Controller
    {
        public int pageSize = 2;
       
        private IDatAcessService DatAcessService;
       public HomeController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Index(string id=null,int page=1  )
        {
            
            List<CarDTO> crs = new List<CarDTO>();
            if (id == null)
            {
                crs = DatAcessService.Cars
                   .OrderBy(car => car.CarId)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize).ToList();
             }
            else
            {
                id = id.Substring(0, id.Length - 1);
                string[] strid =id.Split(Convert.ToChar("I"));
               
                
                for(int i=0;i<strid.Length;i++)
                {
                    crs.Add(DatAcessService.FindCar( Convert.ToInt32(strid[i])));
                }
            }

            IEnumerable<CarDTO> cars = crs;
                CarsListViewModel model = new CarsListViewModel
            {
               Cars=cars
                ,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = DatAcessService.Cars.Count()
                }

            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Car()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Car(CarDTO car)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateCar(car);
               return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(car);
            }
        }
        
    }
}