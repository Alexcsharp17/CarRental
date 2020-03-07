using CarRental.BLL.Attributes;
using CarRental.BLL.Calculations;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.BLL.Mappers;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles = "user,admin,manager")]
    public class PersCabController : Controller
    {
        public int pageSize = 8;
        private IDatAcessService DatAcessService;
        
        public PersCabController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        


        public ActionResult Index(string res=null, int page = 1)
        {
            User.Identity.GetUserId();
            var ords = DatAcessService.Orders.Where(o => o.User_Id == User.Identity.GetUserId());
               if (ords.Count()==0)
                {
                    return View("EmptyCart");
                }    
               foreach(var o in ords)
                {
                    o.CarDTO = DatAcessService.FindCar(o.CarId);
                }
            PersonalCabViewModel model = new PersonalCabViewModel()
            {
                User = DatAcessService.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId()),
                Orders = ords
            };
            ViewBag.Res = res;
            return View(model);
        }
        [HttpGet]
        public ActionResult Checkout(int id=0)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
           CarDTO car= DatAcessService.FindCar(id);
           
            if (car== null)
            {
                return HttpNotFound();
            }
            OrderDTO order = new OrderDTO();
            order.CarDTO = car;
            order.CarId = car.CarId;
          
            order.User_Id= User.Identity.GetUserId();
            
            order.PassportNumb = DatAcessService.Users.FirstOrDefault(u => u.Id == order.User_Id).PassportNumb;
            return View(order);
        }
        [HttpPost]
        public ActionResult Checkout(OrderDTO order)
        {
            if (ModelState.IsValid)
            {
                var car = DatAcessService.FindCar(order.CarId);
                order.OrdSum = PriceCalc.PricePerDays(car.Price,(order.EndTime - order.StartTime).Days);
                order.Status = "Pending";
                DatAcessService.CreateOrder(order);
                return RedirectToAction("Index", "PersCab", new { res = "added" });
            }
            order.CarDTO= DatAcessService.FindCar(order.CarId); 
            return View(order);
        }
        public ActionResult DeleteOrder(int Id)
        {
            DatAcessService.DeleteOrder(Id);
            return RedirectToAction("Index", "PersCab", new { res = "deleted" });
        }
        [HttpGet]
        public ActionResult EditProf(string usId)
        {
            var user = DatAcessService.Users.FirstOrDefault(u => u.Id == usId);
            return View(user);
        }
        [HttpPost]
        public ActionResult EditProf(UserDTO user)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateUser(user);
                return RedirectToAction("Index", "PersCAb");
            }
            return View(user);
        }
    }
}