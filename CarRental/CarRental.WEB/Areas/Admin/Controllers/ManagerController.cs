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

namespace CarRental.WEB.Areas.Admin.Controllers
{
    [ExceptionLogger]
    [Authorize(Roles = "admin, manager")]
    public class ManagerController : Controller
    {
        static IEnumerable<string> userss = new List<string>();
        public int pageSize = 6;
        private IDatAcessService DatAcessService;
        public ManagerController(IDatAcessService serv)
        {
            DatAcessService = serv;
            var us = DatAcessService.Users.Select(u => u.Name);
            userss = us;
        }
        public ActionResult OrderIndex()
        {
            var ord = DatAcessService.Orders;
            foreach (var o in ord)
            {
                o.Car = DatAcessService.FindCar(o.CarId);
            }
            return View(ord);
        }


        public PartialViewResult GetOrders(string id = null)
        {
            List<OrderDTO> ordrs = new List<OrderDTO>();
            if (id == null)
            {
                return PartialView("~/Views/Home/EmptySear.cshtml");
            }
            else
            {
                id = id.Substring(0, id.Length - 1);
                string[] strid = id.Split(Convert.ToChar("|"));
                for (int i = 0; i < strid.Length; i++)
                {
                    ordrs.Add(DatAcessService.Orders.FirstOrDefault(o => o.Id == Convert.ToInt32(strid[i])));
                }
            }
            IEnumerable<OrderDTO> orders = ordrs;
            foreach (var o in orders)
            {
                o.Car = DatAcessService.FindCar(o.CarId);
            }
            return PartialView("~/Views/Home/RendOrders.cshtml", orders);
        }
        //Manager can change order status and leave a comment.
        [HttpGet]
        public ActionResult EditStatus(int id)
        {

            OrderDTO order = DatAcessService.FindOrder(id);
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
                return RedirectToAction("OrderIndex", "Manager");
            }
            else
            {
                return View(order);
            }
        }

        public ActionResult DeleteOrder(int ordId)
        {
            var order = DatAcessService.Orders.Where(o => o.Id == ordId);
            if (order == null)
            {
                return HttpNotFound();
            }
            DatAcessService.DeleteOrder(ordId);
            return RedirectToAction("OrderIndex", new { res = "deleted" });
        }
        public ActionResult Details(int id)
        {
            if (DatAcessService.FindOrder(id) == null)
            {
                return HttpNotFound();
            }
            var ord = DatAcessService.FindOrder(id);
            ord.Car = DatAcessService.FindCar(ord.CarId);
            return View(ord);
        }
        public ActionResult AutocompleteUsName(string term)
        {
            var models = userss.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();
            return Json(models, JsonRequestBehavior.AllowGet);
        }
    }
}