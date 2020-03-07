using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Models;
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
        public int pageSize = 6;
        private IDatAcessService DatAcessService;
        public ManagerController(IDatAcessService serv)
        {
            DatAcessService = serv;
        }
        
        // GET: Admin/Manager
        public ActionResult GetOrders(int page=1)
        {
            var ord = DatAcessService.Orders.Where(o=>o.IsDeleted==false);
            if (ord == null)
            {
                return View("Empty store");
            }
            foreach(var o in ord)
            {
                o.CarDTO = DatAcessService.FindCar(o.CarId);
            }
            OrderListViewModel model = new OrderListViewModel() {
                Orders=ord,
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = DatAcessService.Orders.Count()
                }

            };
           
            return View(model);
        }
        //Manager can change order status and leave a comment.
        [HttpGet]
        public ActionResult EditStatus(int id)
        {
            var order = DatAcessService.FindOrder(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Admin/Main/Edit/5
        [HttpPost]
        public ActionResult EditStatus(OrderDTO order)
        {
            if (ModelState.IsValid)
            {
                DatAcessService.CreateOrder(order);
                return RedirectToAction("GetOrders", "Manager");
            }
            else
            {
                return View(order);
            }
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
        public ActionResult Details(int id)
        {
            if (DatAcessService.FindOrder(id) == null)
            {
                return HttpNotFound();
            }
            var ord = DatAcessService.FindOrder(id);
             ord.CarDTO = DatAcessService.FindCar(ord.CarId);
            return View(ord);
        }
    }
}