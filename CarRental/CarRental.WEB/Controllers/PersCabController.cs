using CarRental.BLL.Attributes;
using CarRental.BLL.Calculations;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;

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
    
    [Authorize(Roles = "user,admin,manager")]
    public class PersCabController : Controller
    {
        public int pageSize = 8;
        private IDatAcessService DatAcessService;

        public PersCabController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }



        public ActionResult Index(string res = null, int page = 1)
        {
            User.Identity.GetUserId();
            
            var ords = DatAcessService.Orders.Where(o => o.User_Id == User.Identity.GetUserId());
            if (ords.Count() == 0)
            {
                return View("EmptyCart");
            }
            foreach (var o in ords)
            {
                o.Car = DatAcessService.FindCar(o.CarId);
            }
            PersonalCabViewModel model = new PersonalCabViewModel()
            {
                User = DatAcessService.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId()),
                Orders = ords
            };
            ViewBag.Res = res;
            ViewBag.CarItem = ords.FirstOrDefault().CarItem;
            return View(model);
        }
        public ActionResult Fines()
        {
           var us= User.Identity.GetUserId();
            var ords = DatAcessService.Orders.Where(o => o.User_Id == User.Identity.GetUserId() && o.Status=="fine");
            if (ords.Count() == 0)
            {
                return View("EmptyFines");
            }
            foreach (var o in ords)
            {
                o.Car = DatAcessService.FindCar(o.CarId);
            }
            return View(ords);
        }
        public ActionResult Prof()
        {
            var usrs = DatAcessService.Users.Where(u => u.Id == User.Identity.GetUserId()).First();
          
            var us = DatAcessService.Users.FirstOrDefault(u => u.Id == User.Identity.GetUserId());
            return View(us);
        }
        [HttpGet]
        public ActionResult Checkout(int id=0)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
           CarDTO car= DatAcessService.FindCar(id);
            List<OrderDTO> orders = DatAcessService.GetCarOrders(id);
            List<string> dates = new List<string>();
            foreach (var ord in orders)
            {
                for (DateTime i = ord.StartTime.Date; DateTime.Compare(i, ord.EndTime) <= 0; i = i.AddDays(1))
                {                   
                    dates.Add(i.ToString("MM/dd/yyyy"));
                }
            }


            if (car== null)
            {
                return HttpNotFound();
            }
            OrderDTO order = new OrderDTO();
            order.Car = car;
            order.CarId = car.Id;         
            order.User_Id= User.Identity.GetUserId();
            order.PassportNumb = DatAcessService.Users.FirstOrDefault(u => u.Id == order.User_Id).PassportNumb;
            ViewBag.FullOrdered = dates;

            return View(order);
        }
        [HttpPost]
        public ActionResult Checkout(OrderDTO order,string start_time=null,
            string end_time=null,string start_place=null,string end_place=null)
        {
           
            List<OrderDTO> orders = DatAcessService.GetCarOrders(order.CarId);
            List<DateTime> dates = new List<DateTime>();
            foreach (var ord in orders)
            {
                for (DateTime i = ord.StartTime.Date; DateTime.Compare(i, ord.EndTime) <= 0; i = i.AddDays(1))
                {
                    dates.Add(i);
                }
            }
           
            ViewBag.FullOrdered = dates;

            if (ModelState.IsValid)
            {
                var car = DatAcessService.FindCar(order.CarId);
                order.OrdSum = PriceCalc.PricePerDays(car.Price,(order.EndTime - order.StartTime));
                order.Status = "Pending";               
                order.StartPlace = start_place;
                order.EndPlace = end_place;
                order.Id = 0;              

                DatAcessService.CreateOrder(order);
                return RedirectToAction("Index", "PersCab", new { res = "added" });
            }
           
            order.Car= DatAcessService.FindCar(order.CarId); 
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