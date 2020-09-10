using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Enums;
using CarRental.BLL.Interfaces;
using CarRental.WEB.Areas.Admin.Models;
using CarRental.WEB.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Controllers
{
   
    public class SideSearchController : Controller
    {
        static IEnumerable<string> carss = new List<string>();
        static IEnumerable<string> userss = new List<string>();
        private IDatAcessService DatAcessService;
        private ICarService CarService;
        private IOrderService OrderService;
        public SideSearchController(IDatAcessService serv, ICarService carserv, IOrderService ordserv)
        {
            DatAcessService = serv;
            CarService = carserv;
            OrderService = ordserv;
            var cr = CarService.Cars.Select(c => c.Name).Distinct();       
            carss = cr;
            var us = DatAcessService.Users.Select(u => u.Name);
            userss = us;
        }
        [HttpGet]
        public ActionResult CarSearch()
        {
                     
            var uniqueManuf = CarService.Cars
                              .Select(c => c.Manufacturer)
                              .Distinct();
            
            var uniqueCarTyp= CarService.Cars
                              .Select(c => c.CarType)
                              .Distinct();
            
            var uniqueFuelTyp= CarService.Cars
                              .Select(c => c.FuelType)
                              .Distinct();
            var uniqueTransm = CarService.Cars
                              .Select(c => c.AutomaticTransm)
                              .Distinct();
            var uniqueEngSize = CarService.Cars.OrderBy(c=>c.EngSize)
                             .Select(c => c.EngSize)
                             .Distinct();
            var unEngSize = new List<string>();
            foreach (var item in uniqueEngSize)
            {
                unEngSize.Add(item.ToString());
            }
            var uniqueFuelConsump = CarService.Cars.OrderBy(c=>c.FuelConsump)
                            .Select(c => c.FuelConsump)
                            .Distinct();
            var unFuelCon = new List<string>();
            foreach (var item in uniqueFuelConsump)
            {
                unFuelCon.Add(item.ToString());
            }

            var uniqueCapac = CarService.Cars.OrderBy(c => c.Capacity)
                              .Select(c => c.Capacity)
                              .Distinct();
                              
            var unCap = new List<string>();
            foreach (var item in uniqueCapac)
            {
                unCap.Add(item.ToString());
            }

            var MaxPrice = CarService.Cars
                          .Select(c => c.Price)
                          .Max();
            var MinPrice = CarService.Cars
                            .Select(c => c.Price)
                            .Min();

            CarSearchModel mod = new CarSearchModel() {
                manufactorers = uniqueManuf.ToList(),
                CarTypes = uniqueCarTyp.ToList(),
                FuelTypes = uniqueFuelTyp.ToList(),
                Capacities = unCap,
                FuelConsump= unFuelCon,
                EngSizes=unEngSize
            };
            

            foreach (var t in uniqueTransm)
            {
                mod.Transmissions.Add(t.ToString());
               
            }

            ViewBag.MaxPrice = MaxPrice;
            ViewBag.MinPrice = MinPrice;
            mod.UppPrice = MaxPrice;
            mod.LowPrice = MinPrice;
            return PartialView("~/Views/SideSearch/CarSearch.cshtml",mod);
          
        }
        [HttpPost]
        public ActionResult CarSearch(CarSearchModel model=null, string[] manufss=null,
            string[] typess=null,string[] transmissionss=null, string[] fueltypess=null, string[] capacity=null, string[] engSize= null, string[] fuelConsump=null)
        {            
            string manufactorers = "";
            if (manufss != null)
            {
                foreach (var m in manufss)
                {
                    manufactorers += m;
                }
            }
            
            string types = "";
            if (typess != null)
            {
                foreach (var m in typess)
                {
                    types += m;
                }
            }
            string fueltypes = "";
            if (fueltypess != null)
            {
                foreach (var ft in fueltypess)
                {
                    fueltypes += ft;
                }
            }
            string transmissions = "";
            if (transmissionss != null)
            {
                foreach (var tr in transmissionss)
                {
                    transmissions += tr;
                }
            }
            string capacities = "";

            if (capacity != null)
            {
                foreach (var tr in capacity)
                {
                    capacities += tr;
                }
            }
            string engSizes = "";
            if (engSize!= null)
            {
                foreach (var i in engSize)
                {
                    engSizes += i;
                }
            }
            string fuelCons = "";
            if (fuelConsump != null)
            {
                foreach (var i in fuelConsump)
                {
                    fuelCons += i;
                }
            }

            if (model != null)
            {
                IEnumerable<CarDTO> c = DatAcessService.FindCars(model.Name,
                    manufactorers,types,fueltypes,transmissions, capacities , fuelCons, engSizes, model.LowPrice, model.UppPrice);
                
                if (model.Sort != null)
                {
                    if (model.Sort == "ascending")
                    {
                        c = c.OrderBy(car => car.Price);
                    }
                    else if (model.Sort == "descending")
                    {
                        c = c.OrderByDescending(car => car.Price);
                    }
                }
                
                string ids="";
                
                 foreach(var car in c)
                {
                    ids = ids + car.Id + "I";
                }
               
                return RedirectToAction("RendCars", "Home", new{id=ids,page=model.Page!=0?model.Page:1,sort=model.Sort
            }); 
            }
            return View();
        }
       [HttpGet]
        public ActionResult UserSearch()
        {        
            return PartialView();
        }
        [HttpPost]
        public ActionResult UserSearch(string name=null,string[] roles=null,
            string[] statuses = null)
        {
            IEnumerable<UserDTO> users = DatAcessService.Users;
            if (name != null && name != "")
            {

                users = users.Where(u => u.Name.Contains(name));

            }
            if (statuses != null)
            {
                string st = "";
                foreach (var s in statuses)
                {
                    st += s;
                }
                users = users.Where(u => st.ToString().Contains(u.Banned.ToString().ToLower()));
            }
            //if(roles != null)
            //{

            //    foreach(var r in roles)
            //    {
            //        foreach(var u in users)
            //        {
            //           var users = UserService.GetRoles()
            //        }
            //    }
            //    users = users.Where(u => role.Contains(u.Role));
            //}

            string ids = "";

            foreach (var u in users)
            {
                ids = ids + u.Id + "|";
            }

            return RedirectToAction("GetUsers", "UserManag", new
            {
                id = ids
            });
        }
        [HttpGet]
        public ActionResult OrderSearch()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult OrderSearch(string[] statuses=null,
            string usid = null)
        {
            IEnumerable<OrderDTO> orders = OrderService.Orders;
            if (usid != null && usid != "")
            {

                orders = orders.Where(o =>usid.Contains(o.User_Id));

            }
            if (statuses != null)
            {
                string st = "";
                foreach (var s in statuses)
                {
                    st += s;
                }
                orders = orders.Where(u => st.ToString().ToLower().Contains(u.Status.ToString().ToLower()));
            }
   
            string ids = "";

            foreach (var o in orders)
            {
                ids = ids + o.Id + "|";
            }

            return RedirectToAction("GetOrders", "Manager", new
            {
                id = ids
            });
        }

        public ActionResult AutocompleteSearch(string term)
        {
            var models = carss.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();         
            return Json(models, JsonRequestBehavior.AllowGet);
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