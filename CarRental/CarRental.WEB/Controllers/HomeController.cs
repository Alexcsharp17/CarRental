using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Controllers
{
   
    public  class HomeController : Controller
    {
        public  const int pageSize = 10;
       
        private IDatAcessService DatAcessService;
        private IOrderService OrderService;
       public HomeController(IDatAcessService serv,IOrderService ords)
        {
            DatAcessService = serv;
            OrderService = ords;

        }

        
        public ActionResult Test(int? id)
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
                //throw new exception("некорректное значение для параметра id");
            }
            ViewBag.hash = this.GetHashCode().ToString();
            try
            {
                OrderDTO o = OrderService.FindOrder(1);
                ViewBag.car = o.Car.Name;
            }
            catch (Exception ex)
            {
                string mes = ex.Message;
            }

          
            return View();
        }
        
      
        public  ActionResult Index(string id=null,int page=1,string sort = null  )
        {
            page = page < Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize)) ? page :
                Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize));


            List<CarDTO> crs = new List<CarDTO>();
            if (id == null)
            {
                crs =  DatAcessService.Cars
                    .Where(c=>c.IsDeleted==false)
                   .OrderBy(car => car.Id)
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

            

            IEnumerable<CarDTO> cars = crs.Where(c=>c.IsDeleted==false).OrderBy(x=>x.Popular);
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
            List<int> popCar = new List<int>();
            int[] ids = cars.OrderBy(x => x.Popular).Select(x => x.Id).ToArray();
            if (ids.Length > 3)
            {
                for (int i = 0; i < ids.Length / 3; i++)
                {
                    popCar.Add(i);
                }
            }
            else
            {
                popCar.Add(ids.FirstOrDefault());
            }
           
            
            

            ViewBag.Sort = new List<string>() { "ascending", "descending" };
            ViewBag.popCar = popCar;
            ViewBag.cars = cars;
            return View("Index",model);
        }
        public PartialViewResult RendCars( int page = 1,string id=null,string sort="")
        {
            page = page < Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize)) ? page :
               Convert.ToInt32(Math.Ceiling((double)DatAcessService.Cars.Count() / pageSize));
            List<CarDTO> crs = new List<CarDTO>();
          
            if (id == null)
            {
                return PartialView("~/Views/Home/EmptySear.cshtml");
            }
            else
            {
                id = id.Substring(0, id.Length - 1);
                string[] strid = id.Split(Convert.ToChar("I"));


                for (int i = 0; i < strid.Length; i++)
                {
                    crs.Add(DatAcessService.FindCar(Convert.ToInt32(strid[i])));
                }              
            }
            int totCars = crs.Count();
            crs = crs.Skip((page - 1) * pageSize)
                  .Take(pageSize).ToList();
            IEnumerable<CarDTO> cars = crs.Where(c => c.IsDeleted == false);
            CarsListViewModel model = new CarsListViewModel
            {
                Cars = cars
                ,
                PagingInfo = new PagingInfo
                {
                    TotalItems = totCars,
                    CurrentPage = page,
                    ItemsPerPage = pageSize,

                }

            };
            ViewBag.Sort = new List<string>() { "ascending", "descending" };
            ViewBag.s = sort;
            List<int> popCar = new List<int>();
            int[] ids = cars.OrderBy(x => x.Popular).Select(x => x.Id).ToArray();

            for (int i = 0; i < ids.Length / 3; i++)
            {
                popCar.Add(i);
            }

            ViewBag.popCar = popCar;
            return PartialView(model);
        }
       public PartialViewResult RendMenu()
        {
            var userID = User.Identity.GetUserId();
            var orders = DatAcessService.FindOrders(userID);
            var fines = orders.Where(o => o.Status == "fine");
            UserDTO user = DatAcessService.Users.FirstOrDefault(u => u.Id == userID);
            var fillpfof = false;
            
            if(User.Identity.IsAuthenticated) { 
            //if(user.Name==null || user.PassportNumb==0)
            //{
            //    fillpfof = true;
            //}
            ViewBag.Fines = fines.Count();
            ViewBag.Orders = orders.Count();
            ViewBag.Fillprof = fillpfof;
            }
            
            return PartialView();
        }

    }
}