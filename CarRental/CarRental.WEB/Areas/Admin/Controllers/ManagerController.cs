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
    public class ManagerController : Controller
    {
        private IDatAcessService DatAcessService;
        public ManagerController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        
        // GET: Admin/Manager
        public ActionResult GetOrders()
        {
            var ord = DatAcessService.Orders.Where(o=>o.IsDeleted==false);
            if (ord == null)
            {
                return View("Empty store");
            }
            return View(ord);
        }
        [HttpGet]
        public ActionResult EditOrder(int ordId)
        {
           var order= DatAcessService.Orders.Where(o => o.OrderId == ordId);
            if (order == null){
                return HttpNotFound();
            }
            return View(order);
        }

        [HttpPost]
        public ActionResult EditOrder(OrderDTO order)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateOrder(order);
                return RedirectToAction("GetOrders", new { res = "edited" });
            }
            else return View(order);
        }
        public ActionResult DeleteOrder(int ordId){
            var order = DatAcessService.Orders.Where(o => o.OrderId == ordId);
            if (order == null)
            {
                return HttpNotFound();
            }
            DatAcessService.DeleteOrder(ordId);
            return RedirectToAction("GetOrders", new { res = "deleted" });
        }
        public ActionResult DeleteOrderSoft(int ordId)
        {
            var order = DatAcessService.Orders.Where(o => o.OrderId == ordId);
            if (order == null)
            {
                return HttpNotFound();
            }
            DatAcessService.DeleteOrderSoft(ordId);
            return RedirectToAction("GetOrders", new { res = "deleted" });
        }
    }
}