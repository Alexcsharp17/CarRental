using CarRental.BLL.Attributes;
using CarRental.BLL.DTO;
using CarRental.BLL.Interfaces;
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
    [ExceptionLogger]
    public class SideSearchController : Controller
    {
        static IEnumerable<string> carss = new List<string>();
        private IDatAcessService DatAcessService;
        public SideSearchController(IDatAcessService serv)
        {
            DatAcessService = serv;
            var cr = DatAcessService.Cars.Select(c => c.Name).Distinct();
            carss = cr;
        }
        [HttpGet]
        public ActionResult CarSearch()
        {
            CarSearchModel mod = new CarSearchModel();
           
            
           
            var uniqueManuf = DatAcessService.Cars
                              .Select(c => c.Manufacturer)
                              .Distinct();
            
            var uniqueCarTyp= DatAcessService.Cars
                              .Select(c => c.CarType)
                              .Distinct();
            var uniqueFuelTyp= DatAcessService.Cars
                              .Select(c => c.FuelType)
                              .Distinct();
            var uniqueTransm = DatAcessService.Cars
                              .Select(c => c.AutomaticTransm)
                              .Distinct();

            var MaxPrice = DatAcessService.Cars
                          .Select(c => c.Price)
                          .Max();
            var MinPrice = DatAcessService.Cars
                            .Select(c => c.Price)
                            .Min();
            int i = 0;
            foreach(var t in uniqueManuf)
            {
                mod.manufactorers.Add(t);
                i++;
            }
          
             i = 0;
            
            foreach (var t in uniqueCarTyp)
            {
                mod.CarTypes.Add(t);
                i++;
            }
            i = 0;

            foreach (var t in uniqueTransm)
            {
                mod.Transmissions.Add(t.ToString());
                i++;
            }
            i = 0;

            foreach (var t in uniqueFuelTyp)
            {
                mod.FuelTypes.Add(t);
                i++;
            }


            ViewBag.MaxPrice = MaxPrice;
            ViewBag.MinPrice = MinPrice;
            mod.UppPrice = MaxPrice;
            mod.LowPrice = MinPrice;
            return PartialView("~/Views/SideSearch/CarSearch.cshtml",mod);
          
        }
        [HttpPost]
        public ActionResult CarSearch(CarSearchModel model=null, string[] manufss=null,
            string[] typess=null,string[] transmissionss=null, string[]fueltypess=null)
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

            if (model != null)
            {
                IEnumerable<CarDTO> c = DatAcessService.FindCars(model.Name,
                    manufactorers,types,fueltypes,transmissions, model.LowPrice, model.UppPrice);
                
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
                    ids = ids + car.CarId + "I";
                }
               
                return RedirectToAction("RendCars", "Home", new{id=ids
            }); 
            }
            return View();
        }
        public ActionResult AutocompleteSearch(string term)
        {
            var models = carss.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();
          

            return Json(models, JsonRequestBehavior.AllowGet);
        }

       

    }
}