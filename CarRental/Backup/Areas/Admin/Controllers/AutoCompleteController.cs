using CarRental.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRental.WEB.Areas.Admin.Controllers
{
    public class AutoCompleteController : Controller
    {
        private IDatAcessService DatAcessService;
        public AutoCompleteController(IDatAcessService serv)
        {
            DatAcessService = serv;
            cars = DatAcessService.Cars.Select(c => c.Name).Distinct();
            manufact = DatAcessService.Cars.Select(c => c.Manufacturer).Distinct();
            cartypes = DatAcessService.Cars.Select(c => c.CarType).Distinct();
            fueltypes = DatAcessService.Cars.Select(c => c.FuelType).Distinct();
        }
        static IEnumerable<string> cars = new List<string>();
        static IEnumerable<string> manufact = new List<string>();
        static IEnumerable<string> cartypes = new List<string>();
        static IEnumerable<string> fueltypes = new List<string>();
        public ActionResult AutocomCars(string term)
        {
            var models = cars.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();


            return Json(models, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocomManuf(string term)
        {
            var models = manufact.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();


            return Json(models, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocomCarTypes(string term)
        {
            var models = cartypes.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();


            return Json(models, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutocomFuel(string term)
        {
            var models = fueltypes.Where(a => a.ToLower().Contains(term.ToLower()))
                            .Select(a => new { value = a })
                            .Distinct();


            return Json(models, JsonRequestBehavior.AllowGet);
        }

    }
}