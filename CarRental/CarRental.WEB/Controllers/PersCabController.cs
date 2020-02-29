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

            var ords = DatAcessService.Orders.Where(o => o.User_Id == User.Identity.GetUserId());
               if (ords == null)
                {
                    return View("EmptyCart");
                }    
               foreach(var o in ords)
                {
                    o.CarDTO = DatAcessService.FindCar(o.CarId);
                }
           
            return View(ords);
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
            return View(order);
        }
        [HttpPost]
        public ActionResult Checkout(OrderDTO order)
        {
            if (ModelState.IsValid)
            {
                order.Status = "Pending";
                DatAcessService.CreateOrder(order);
                return RedirectToAction("Index", "PersCab", new { res = "added" });
            }
            return View(order);
        }
        public ActionResult DeleteOrder(int Id)
        {
            DatAcessService.DeleteOrder(Id);
            return RedirectToAction("Index", "PersCab", new { res = "deleted" });
        }
    }
}