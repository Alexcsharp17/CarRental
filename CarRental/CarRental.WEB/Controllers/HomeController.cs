using CarRental.BLL.Attributes;
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
    [ExceptionLogger]
    public class HomeController : Controller
    {
        public int pageSize = 6;
       
        private IDatAcessService DatAcessService;
       public HomeController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }

        
        public ActionResult Test(int id)
        {
            if (id > 3)
            {
                int[] mas = new int[2];
                mas[6] = 4;
            }
            else if (id < 3)
            {
                throw new Exception("id не может быть меньше 3");
            }
            else
            {
                throw new Exception("Некорректное значение для параметра id");
            }
            return View();
        }

        public ActionResult About()
        {
            
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
      
        public ActionResult Index(string id=null,int page=1,string sort = null  )
        {
            page = page < Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize)) ? page :
                Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize));


            List<CarDTO> crs = new List<CarDTO>();
            if (id == null)
            {
                crs = DatAcessService.Cars
                    .Where(c=>c.IsDeleted==false)
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
                crs=crs.Skip((page - 1) * pageSize)
                   .Take(pageSize).ToList();
            }
            IEnumerable<CarDTO> cars = crs.Where(c=>c.IsDeleted==false);
            if (sort != null)
            {
                if (sort == "ascending")
                {
                    cars = cars.OrderBy(c => c.Price);
                }
                else if (sort == "descending")
                {
                    cars = cars.OrderByDescending(c => c.Price);
                }
            }
            CarsListViewModel model = new CarsListViewModel
            {

               Cars=cars
                ,
                PagingInfo = new PagingInfo
                {   
                    TotalItems = DatAcessService.Cars.Count(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                   
                }

            };
            ViewBag.Sort = new List<string>() { "ascending", "descending" };
            return View(model);
        }
        public PartialViewResult RendCars( int page = 1,string id=null)
        {
            page = page < Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize)) ? page :
               Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize));
            List<CarDTO> crs = new List<CarDTO>();
            if (id == null)
            {
                crs = DatAcessService.Cars
                    .Where(c => c.IsDeleted == false)
                   .OrderBy(car => car.CarId)
                   .Skip((page - 1) * pageSize)
                   .Take(pageSize).ToList();
            }
            else
            {
                id = id.Substring(0, id.Length - 1);
                string[] strid = id.Split(Convert.ToChar("I"));


                for (int i = 0; i < strid.Length; i++)
                {
                    crs.Add(DatAcessService.FindCar(Convert.ToInt32(strid[i])));
                }
                crs = crs.Skip((page - 1) * pageSize)
                   .Take(pageSize).ToList();
            }
            IEnumerable<CarDTO> cars = crs.Where(c => c.IsDeleted == false);
            CarsListViewModel model = new CarsListViewModel
            {
                Cars = cars
                ,
                PagingInfo = new PagingInfo
                {
                    TotalItems = DatAcessService.Cars.Count(),
                    CurrentPage = page,
                    ItemsPerPage = pageSize,

                }

            };
            ViewBag.Sort = new List<string>() { "ascending", "descending" };
           
            return PartialView(model);
        }
       

    }
}